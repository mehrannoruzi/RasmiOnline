namespace RasmiOnline.Console.PaymentStrategy
{
    using System;
    using System.Web;
    using Properties;
    using Domain.Dto;
    using System.Text;
    using Domain.Enum;
    using Domain.Entity;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using Business.Observers;
    using DependencyResolver.Ioc;
    using Gnu.Framework.Core.Log;
    using Gnu.Framework.Core.Authentication;
    using SharedPreference;

    public class PayStrategy : IPaymentStrategy
    {
        readonly IUserBusiness _userBusiness;
        readonly IOrderBusiness _orderBusiness;
        readonly ITransactionBusiness _transactionBusiness;
        readonly Lazy<IObserverManager> _observerManager;
        public PayStrategy()
        {
            _userBusiness = IocInitializer.GetInstance<IUserBusiness>();
            _orderBusiness = IocInitializer.GetInstance<IOrderBusiness>();
            _transactionBusiness = IocInitializer.GetInstance<ITransactionBusiness>();
            _observerManager = IocInitializer.GetInstance<Lazy<IObserverManager>>();
        }

        public IActionResponse<string> Do(PaymentGateway gateway, TransactionModel model)
        {
            var currentUser = _userBusiness.Find(model.UserId);
            if (currentUser == null)
            {
                return new ActionResponse<string>()
                {
                    Message = LocalMessage.UsernameIsWrong
                };
            }

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
            using (HttpClient http = new HttpClient())
            {
                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                var response = http.PostAsync($"https://pay.ir/payment/send?api={gateway.MerchantId.Trim()}&amount={model.Price * 10}&redirect={AppSettings.TransactionRedirectUrl_Pay}&factorNumber={transaction.Result.TransactionId}", content).Result;
                var deserializeResponse = JsonConvert.DeserializeObject<GatewayPayResponseModel>(response.Content.ReadAsStringAsync().Result);

                if (deserializeResponse.status == 1)
                {
                    return new ActionResponse<string>
                    {
                        IsSuccessful = true,
                        Result = $"https://pay.ir/payment/gateway/{deserializeResponse.transId}"
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
        }
        public IActionResponse<string> Verify(PaymentGateway gateway, Transaction model, object responseGateway = null)
        {
            try
            {
                var deserializeResponse = new GatewayPayResponseModel();
                if (responseGateway == null || ((PayRedirectModel)(responseGateway)).status == 0)
                    return new ActionResponse<string>
                    {
                        IsSuccessful = false,
                        Message = "عملیات پرداخت از سمت درگاه تایید نشد، لطفا مجددا عملیات پرداخت را تکرار نمایید",
                    };

                using (HttpClient http = new HttpClient())
                {
                    var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                    var response = http.PostAsync($"https://pay.ir/payment/verify?api={gateway.MerchantId.Trim()}&transId={((PayRedirectModel)(responseGateway)).transId}", content).Result;
                    FileLoger.Info("webservice response : " + response.Content.ReadAsStringAsync().Result, GlobalVariable.LogPath);
                    deserializeResponse = JsonConvert.DeserializeObject<GatewayPayResponseModel>(response.Content.ReadAsStringAsync().Result);
                }

                if (deserializeResponse != null && deserializeResponse.status == 1)
                {
                    model.IsSuccess = true;
                    model.TrackingId = ((PayRedirectModel)(responseGateway)).transId.ToString();
                    model.Status = deserializeResponse.status.ToString();
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
                        Result = model.TrackingId.ToString()
                    };
                }
                else
                {
                    model.IsSuccess = false;
                    model.TrackingId = model.TrackingId.ToString();
                    model.Status = deserializeResponse.status.ToString();
                    _transactionBusiness.Update(model);
                    return new ActionResponse<string>
                    {
                        IsSuccessful = false,
                        Message = "عملیات پرداخت از سمت درگاه تایید نشد، لطفا مجددا عملیات پرداخت را تکرار نمایید",
                        Result = model.TrackingId.ToString()
                    };
                }
            }
            catch (Exception e)
            {
                FileLoger.Error(e, GlobalVariable.LogPath);
                return new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = "عملیات پرداخت از سمت درگاه تایید نشد، لطفا مجددا عملیات پرداخت را تکرار نمایید",
                    Result = model.TrackingId.ToString()
                };
            }
        }
    }
}