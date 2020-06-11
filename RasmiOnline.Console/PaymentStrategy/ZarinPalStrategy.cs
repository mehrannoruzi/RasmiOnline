namespace RasmiOnline.Console.PaymentStrategy
{
    using System;
    using ZarinPal;
    using System.Net;
    using Properties;
    using System.Web;
    using Domain.Dto;
    using Domain.Enum;
    using Domain.Entity;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using Business.Observers;
    using DependencyResolver.Ioc;
    using Gnu.Framework.Core.Authentication;

    public class ZarinPalStrategy : IPaymentStrategy
    {
        readonly PaymentGatewayImplementationServicePortTypeClient _zarinPal;
        readonly IUserBusiness _userBusiness;
        readonly ITransactionBusiness _transactionBusiness;
        readonly IOrderBusiness _orderBusiness;
        readonly Lazy<IObserverManager> _observerManager;
        public ZarinPalStrategy()
        {
            _zarinPal = new PaymentGatewayImplementationServicePortTypeClient();
            _userBusiness = IocInitializer.GetInstance<IUserBusiness>();
            _transactionBusiness = IocInitializer.GetInstance<ITransactionBusiness>();
            _orderBusiness = IocInitializer.GetInstance<IOrderBusiness>();
            _observerManager = IocInitializer.GetInstance<Lazy<IObserverManager>>();
        }
        public IActionResponse<string> Do(PaymentGateway gateway, TransactionModel model)
        {
            var currentUser = _userBusiness.Find(model.UserId);
            ServicePointManager.Expect100Continue = false;
            string uniqueIdentifier = string.Empty;

            var paymentRequest = _zarinPal.PaymentRequest(
                MerchantID: gateway.MerchantId,
                Amount: model.Price,
                Description: $"پرداخت سفارش {model.OrderId}",
                Email: currentUser.Email,
                Mobile: currentUser.MobileNumber.ToString(),
                CallbackURL: AppSettings.TransactionRedirectUrl_ZarinPal,
                Authority: out uniqueIdentifier);

            if (paymentRequest == 100)
            {
                var transaction = _transactionBusiness.Do(new Transaction
                {
                    OrderId = model.OrderId,
                    Price = model.Price,
                    PaymentGatewayId = model.PaymentGatewayId,
                    Authority = uniqueIdentifier,
                    Status = "100",
                    InsertDateMi = DateTime.Now,
                    InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date)
                });

                if (transaction.IsSuccessful)
                {
                    return new ActionResponse<string>
                    {
                        IsSuccessful = true,
                        Result = $"https://www.zarinpal.com/pg/StartPay/{uniqueIdentifier}"
                    };
                }
            }
            return new ActionResponse<string>
            {
                IsSuccessful = false,
                Message = LocalMessage.Exception
            };
        }

        public IActionResponse<string> Verify(PaymentGateway gateway, Transaction model, object responseGateway = null)
        {


            long trackingId = 0;
            var paymentVerify = _zarinPal.PaymentVerification(
                MerchantID: gateway.MerchantId,
                Authority: model.Authority.Trim(),
                Amount: model.Price,
                RefID: out trackingId
                );

            if (paymentVerify == 100)
            {
                model.IsSuccess = true;
                model.TrackingId = trackingId.ToString();
                model.Status = paymentVerify.ToString();
                if (model.OrderId != 0)
                    _orderBusiness.UpdateStatus(model.OrderId);
                _transactionBusiness.Update(model);

                _observerManager.Value.Notify(ConcreteKey.Transaction_Add, new ObserverMessage
                {
                    SmsContent = string.Format(LocalMessage.Transaction_Add_Sms, (HttpContext.Current.User as ICurrentUserPrincipal).FullName, model.OrderId),
                    BotContent = string.Format(LocalMessage.Transaction_Add_Bot, (HttpContext.Current.User as ICurrentUserPrincipal).FullName,
                                               model.OrderId, gateway.BankName.GetLocalizeDescription(),
                                               model.Price.ToString("0,0"),
                                               trackingId.ToString()),
                    Key = nameof(Transaction),
                    UserId = (HttpContext.Current.User as ICurrentUserPrincipal).UserId,
                });

                return new ActionResponse<string>
                {
                    IsSuccessful = true,
                    Message = "عملیات پرداخت با موفقیت انجام شد",
                    Result = trackingId.ToString()
                };
            }
            else
            {
                model.IsSuccess = false;
                model.TrackingId = trackingId.ToString();
                model.Status = paymentVerify.ToString();
                _transactionBusiness.Update(model);

                return new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = "عملیات پرداخت از سمت درگاه تایید نشد، لطفا مجددا عملیات پرداخت را تکرار نمایید",
                    Result = trackingId.ToString()
                };
            }
        }
    }
}