namespace RasmiOnline.Console.Controllers
{
    using Domain.Dto;
    using Properties;
    using System.Linq;
    using Domain.Enum;
    using Domain.Entity;
    using System.Web.Mvc;
    using PaymentStrategy;
    using Gnu.Framework.Core;
    using RasmiOnline.Business.Protocol;
    using System.Collections.Generic;
    using SharedPreference;
    using Gnu.Framework.Core.Log;
    using Newtonsoft.Json;
    using Gnu.Framework.Core.Authentication;

    public partial class TransactionController : Controller
    {
        #region Constructor
        readonly ITransactionBusiness _transactionBusiness;
        readonly IPaymentGatewayBusiness _paymentGatewayBusiness;
        readonly IBankCardBusiness _bankCardBusiness;
        readonly IOrderBusiness _orderBusiness;
        public TransactionController(IOrderBusiness orderBusiness, IBankCardBusiness bankCardBusiness, ITransactionBusiness transactionBusiness, IPaymentGatewayBusiness paymentGatewayBusiness)
        {
            _transactionBusiness = transactionBusiness;
            _paymentGatewayBusiness = paymentGatewayBusiness;
            _orderBusiness = orderBusiness;
            _bankCardBusiness = bankCardBusiness;
        }
        #endregion


        #region Private Methods
        [NonAction]
        private void GetPaymentGateways()
        {
            var paymentGateways = _paymentGatewayBusiness.GetAll();
            ViewBag.PaymentGateways = paymentGateways.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.PaymentGatewayId.ToString()
            }).ToList();
        }

        [NonAction]
        private void GetPaymentStatus()
        {
            var list = new List<ItemTextValueModel<string, bool?>>
            {
                new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Success },
                new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.UnSuccess }
            };

            ViewBag.PaymentStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString()
            }).ToList();
        }

        [NonAction]
        private void GetBankCard()
        {
            var BankCard = _bankCardBusiness.GetAll(true);
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem
            {
                Text = string.Empty,
                Value = "0"
            });

            result.AddRange(BankCard.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.BankCardId.ToString()
            }).ToList());
            ViewBag.BankCard = result;
        }
        #endregion

        [HttpPost]
        public virtual ViewResult PayVerify(PayRedirectModel model)
        {
            ViewBag.PaymentGateway = BankNames.Pay;
            if (model.IsNotNull())
            {
                var transaction = _transactionBusiness.Find(model.factorNumber);
                if (transaction.IsNull())
                {
                    ViewBag.ErrorMessage = LocalMessage.PaymentException;
                    return View(viewName: MVC.Transaction.Views.Failed, model: transaction);
                }

                var gateWay = _paymentGatewayBusiness.Find(transaction.PaymentGatewayId);
                if (gateWay.IsNull())
                    return View(viewName: MVC.Transaction.Views.Failed, model: LocalMessage.PaymentException);
                var result = PaymentFactory.GetInstance(gateWay.BankName).Verify(gateWay, transaction, model);
                if (result.IsSuccessful)
                    return View(viewName: MVC.Transaction.Views.Success, model: transaction);

                transaction.TrackingId = "0";
                ViewBag.ErrorMessage = result.Message;
                return View(viewName: MVC.Transaction.Views.Failed, model: transaction);
            }
            ViewBag.ErrorMessage = LocalMessage.RedirectException;
            return View(viewName: MVC.Transaction.Views.Failed, model: LocalMessage.RedirectException);

        }

        [HttpPost]
        public virtual ViewResult SadadVerify(SadadPurchaseResult model)
        {
            // FileLoger.Info(JsonConvert.SerializeObject(model), GlobalVariable.LogPath);

            ViewBag.PaymentGateway = BankNames.Melli;
            if (model.IsNotNull())
            {
                var transaction = _transactionBusiness.Find(model.OrderId);
                if (transaction.IsNull())
                {
                    ViewBag.ErrorMessage = LocalMessage.PaymentException;
                    return View(viewName: MVC.Transaction.Views.Failed, model: transaction);
                }

                var gateWay = _paymentGatewayBusiness.Find(transaction.PaymentGatewayId);
                if (gateWay.IsNull())
                    return View(viewName: MVC.Transaction.Views.Failed, model: LocalMessage.PaymentException);
                var result = PaymentFactory.GetInstance(gateWay.BankName).Verify(gateWay, transaction, model);
                if (result.IsSuccessful)
                    return View(viewName: MVC.Transaction.Views.Success, model: transaction);

                transaction.TrackingId = "0";
                ViewBag.ErrorMessage = result.Message;
                return View(viewName: MVC.Transaction.Views.Failed, model: transaction);
            }
            ViewBag.ErrorMessage = LocalMessage.RedirectException;
            return View(viewName: MVC.Transaction.Views.Failed, model: LocalMessage.RedirectException);

        }

        /// <summary>
        /// zarinpal redirection
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="Authority"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual ViewResult ZarinPalVerify(string Status, string Authority)
        {
            ViewBag.PaymentGateway = BankNames.ZarinPal;

            if (!string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(Authority))
            {
                var transaction = _transactionBusiness.Find(Authority.Trim());
                if (transaction.IsNull())
                {
                    ViewBag.ErrorMessage = LocalMessage.PaymentException;
                    return View(viewName: MVC.Transaction.Views.Failed, model: transaction);
                }

                var _order = _orderBusiness.Find(transaction.OrderId);
                var gateWay = _paymentGatewayBusiness.Find(transaction.PaymentGatewayId);
                if (gateWay.IsNull())
                {
                    ViewBag.ErrorMessage = LocalMessage.PaymentException;
                    return View(viewName: MVC.Transaction.Views.Failed, model: transaction);

                }
                var result = PaymentFactory.GetInstance(gateWay.BankName).Verify(gateWay, transaction);
                if (result.IsSuccessful)
                    return View(viewName: MVC.Transaction.Views.Success, model: transaction);

                ViewBag.ErrorMessage = result.Message;
                return View(viewName: MVC.Transaction.Views.Failed, model: transaction);
            }
            ViewBag.ErrorMessage = LocalMessage.RedirectException;

            return View(viewName: MVC.Transaction.Views.Failed, model: new Transaction());
        }

        [HttpGet, AllowAnonymous]
        public virtual PartialViewResult List(int orderId) => PartialView(MVC.Transaction.Views.Partials._List, _transactionBusiness.GetDetail(orderId));

        [HttpGet, AllowAnonymous]
        public virtual PartialViewResult AddableList(int orderId, bool removeable = false)
        {
            GetPaymentGateways();
            GetPaymentStatus();
            GetBankCard();
            return PartialView(MVC.Transaction.Views.Partials._AddableList,
                new GetTransactionModel
                {
                    OrderId = orderId,
                    Transactions = _transactionBusiness.GetDetail(orderId),
                    Removeable = removeable
                });
        }

        [HttpPost]
        public virtual JsonResult AddTransaction(Transaction model)
        {
            var response = new ActionResponse<int>();
            if (model.IsNull())
                return Json(new ActionResponse<bool> { Message = LocalMessage.BadRequest }, JsonRequestBehavior.AllowGet);

            if (model.OrderId < 1)
                return Json(new ActionResponse<bool> { Message = string.Format(LocalMessage.NotSelectedWithFormat, LocalMessage.InsertDate) }, JsonRequestBehavior.AllowGet);

            if (model.PaymentGatewayId < 1)
                return Json(new ActionResponse<bool> { Message = string.Format(LocalMessage.NotSelectedWithFormat, LocalMessage.PaymentGateway) }, JsonRequestBehavior.AllowGet);

            if (model.Price < 1)
                return Json(new ActionResponse<bool> { Message = string.Format(LocalMessage.NotSelectedWithFormat, LocalMessage.Price) }, JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(model.TrackingId))
                return Json(new ActionResponse<bool> { Message = string.Format(LocalMessage.NotSelectedWithFormat, LocalMessage.TrackingId) }, JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(model.InsertDateSh))
                return Json(new ActionResponse<bool> { Message = string.Format(LocalMessage.NotSelectedWithFormat, LocalMessage.InsertDate) }, JsonRequestBehavior.AllowGet);

            return Json(_transactionBusiness.Insert(model), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult RemoveTransaction(int orderId, int transactionId) => Json(_transactionBusiness.Remove(orderId, transactionId));

        [ChildActionOnly]
        public virtual PartialViewResult EcommerceScript(int orderId, int price)
        {
            ViewBag.Price = price;
            return PartialView(MVC.Transaction.Views.Partials._EcommerceScript, _orderBusiness.Find(orderId, "OrderItems"));
        }

        [ChildActionOnly]
        public virtual PartialViewResult GetAllUnReadPayment()
        {
            ViewBag.IsOffice = false;
            return PartialView(MVC.Transaction.Views.Partials._AllUnReadPayment, _transactionBusiness.AllUnReadPayment());
        }

        [ChildActionOnly]
        public virtual PartialViewResult GetAllUnReadOfficePayment()
        {
            ViewBag.IsOffice = true;
            return PartialView(MVC.Transaction.Views.Partials._AllUnReadPayment, _transactionBusiness.AllUnReadOfficePayment((User as ICurrentUserPrincipal).UserId));
        }
        [HttpPost]
        public virtual JsonResult Read(int transactionId, bool isOffice) => Json(_transactionBusiness.Read(transactionId, isOffice));
    }
}