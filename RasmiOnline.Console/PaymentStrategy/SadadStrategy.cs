using System;

namespace RasmiOnline.Console.PaymentStrategy
{
    using System;
    using Gnu.Framework.Core;
    using Domain.Dto;
    using Domain.Entity;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;
    using Properties;
    using Business.Observers;
    using DependencyResolver.Ioc;
    using Business.Protocol;
    using Gnu.Framework.Core.Log;
    using Newtonsoft.Json;
    using SharedPreference;
    using Gnu.Framework.Core.Authentication;
    using Domain.Enum;

    public class SadadStrategy : IPaymentStrategy
    {
        readonly IUserBusiness _userBusiness;
        readonly IOrderBusiness _orderBusiness;
        readonly ITransactionBusiness _transactionBusiness;
        readonly Lazy<IObserverManager> _observerManager;
        public SadadStrategy()
        {
            _userBusiness = IocInitializer.GetInstance<IUserBusiness>();
            _orderBusiness = IocInitializer.GetInstance<IOrderBusiness>();
            _transactionBusiness = IocInitializer.GetInstance<ITransactionBusiness>();
            _observerManager = IocInitializer.GetInstance<Lazy<IObserverManager>>();
        }

        public IActionResponse<string> Do(PaymentGateway gateway, TransactionModel model)
        {
            try
            {
                var transaction = _transactionBusiness.Do(new Transaction
                {
                    OrderId = model.OrderId,
                    Price = model.Price,
                    PaymentGatewayId = model.PaymentGatewayId,
                    Authority = "100",
                    Status = "100",
                    InsertDateMi = DateTime.Now,
                    InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date)
                });
                if (!transaction.IsSuccessful)
                    return new ActionResponse<string>
                    {
                        IsSuccessful = false,
                        Message = LocalMessage.Exception
                    };
                var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", gateway.Username, transaction.Result.TransactionId, model.Price * 10));
                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(gateway.MerchantId), new byte[8]);
                var signData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    TerminalId = gateway.Username,
                    MerchantId = gateway.Password,
                    Amount = model.Price * 10,
                    SignData = signData,
                    ReturnUrl = AppSettings.TransactionRedirectUrl_Sadad,
                    LocalDateTime = DateTime.Now,
                    OrderId = transaction.Result.TransactionId
                };

                //FileLoger.Info(JsonConvert.SerializeObject(data), GlobalVariable.LogPath);
                var res = CallApi<PayResultData>("https://sadad.shaparak.ir/VPG/api/v0/Request/PaymentRequest", data);
                res.Wait();
                //FileLoger.Info(JsonConvert.SerializeObject(res.Result), GlobalVariable.LogPath);
                if (res != null && res.Result != null)
                {
                    return new ActionResponse<string>
                    {
                        IsSuccessful = true,
                        Result = $"https://sadad.shaparak.ir/VPG/Purchase/Index?token={res.Result.Token}"
                    };
                }
                else
                {
                    return new ActionResponse<string>()
                    {
                        Message = LocalMessage.PaymentConnectionFailed
                    };
                }
            }
            catch (Exception ex)
            {
                FileLoger.Error(ex, GlobalVariable.LogPath);
                return new ActionResponse<string>()
                {
                    Message = LocalMessage.Exception
                };
            }
        }

        public IActionResponse<string> Verify(PaymentGateway gateway, Transaction model, object responseGateway = null)
        {
            try
            {
                var result = (SadadPurchaseResult)responseGateway;
                var dataBytes = Encoding.UTF8.GetBytes(result.Token);
                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;
                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(gateway.MerchantId), new byte[8]);
                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));
                var data = new
                {
                    token = result.Token,
                    SignData = signedData
                };

                var res = CallApi<VerifyResultData>("https://sadad.shaparak.ir/VPG/api/v0/Advice/Verify", data);
                FileLoger.Info(JsonConvert.SerializeObject(res.Result), GlobalVariable.LogPath);
                if (res != null && res.Result != null)
                {
                    if (res.Result.ResCode == "0")
                    {
                        model.IsSuccess = true;
                        model.TrackingId = res.Result.RetrivalRefNo;
                        model.Status = "1";
                        if (model.OrderId != 0)
                            _orderBusiness.UpdateStatus(model.OrderId);
                        _transactionBusiness.Update(model);

                        _observerManager.Value.Notify(ConcreteKey.Transaction_Add, new ObserverMessage
                        {
                            SmsContent = string.Format(LocalMessage.Transaction_Add_Sms, (HttpContext.Current.User as ICurrentUserPrincipal).FullName, model.OrderId),
                            BotContent = string.Format(LocalMessage.Transaction_Add_Bot, (HttpContext.Current.User as ICurrentUserPrincipal).FullName,
                                                model.OrderId, gateway.BankName.GetLocalizeDescription(),
                                                model.Price.ToString("0,0"),
                                                model.TrackingId),
                            Key = nameof(Transaction),
                            UserId = (HttpContext.Current.User as ICurrentUserPrincipal).UserId,
                        });

                        return new ActionResponse<string>
                        {
                            IsSuccessful = true,
                            Message = "عملیات پرداخت با موفقیت انجام شد",
                            Result = res.Result.RetrivalRefNo
                        };
                    }
                    return new ActionResponse<string>
                    {
                        IsSuccessful = false,
                        Message = "عملیات پرداخت از سمت درگاه تایید نشد، لطفا مجددا عملیات پرداخت را تکرار نمایید",
                        Result = "-----"
                    };
                }
                else
                {
                    model.IsSuccess = false;
                    model.TrackingId = result.VerifyResultData.RetrivalRefNo.ToString();
                    model.Status = "-1";
                    _transactionBusiness.Update(model);
                    return new ActionResponse<string>
                    {
                        IsSuccessful = false,
                        Message = "عملیات پرداخت از سمت درگاه تایید نشد، لطفا مجددا عملیات پرداخت را تکرار نمایید",
                        Result = "----"
                    };
                }
            }
            catch (Exception ex)
            {
                FileLoger.Error(ex, GlobalVariable.LogPath);
                return new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = "عملیات پرداخت از سمت درگاه تایید نشد، لطفا مجددا عملیات پرداخت را تکرار نمایید",
                    Result = "---"
                };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private static async Task<T> CallApi<T>(string apiUrl, object value)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync(apiUrl, value);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<T>();
                    result.Wait();
                    return result.Result;
                }
                return default(T);
            }
        }
    }

    public class PayResultData
    {
        public string ResCode { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
    }

    public class SadadPurchaseResult
    {
        public int OrderId { get; set; }
        public string Token { get; set; }
        public string ResCode { get; set; }
        public VerifyResultData VerifyResultData { get; set; }
    }
    public class VerifyResultData
    {
        public bool Succeed { get; set; }
        public string ResCode { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string RetrivalRefNo { get; set; }
        public string SystemTraceNo { get; set; }
        public string OrderId { get; set; }
    }
}