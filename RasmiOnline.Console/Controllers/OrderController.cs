namespace RasmiOnline.Console.Controllers
{
    using System;
    using Properties;
    using Domain.Dto;
    using Domain.Enum;
    using System.Linq;
    using Domain.Entity;
    using System.Web.Mvc;
    using PaymentStrategy;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using Gnu.Framework.AspNet.Mvc;
    using System.Collections.Generic;
    using Gnu.Framework.Core.Authentication;
    using Domain;

    public partial class OrderController : Controller
    {
        readonly IPaymentGatewayBusiness _paymentGatewayBusiness;
        readonly IOfficeAddressBusiness _officeAdressBusiness;
        readonly IOrderBusiness _orderBusiness;
        readonly IAddressBusiness _addressBusiness;
        readonly ISettingBusiness _settingBusiness;
        readonly Lazy<IPricingItemBusiness> _pricingItemBusiness;
        readonly Lazy<IShortLinkBusiness> _shortLinkBusiness;

        #region Constructor
        public OrderController(IOrderBusiness orderBusiness,
            IPaymentGatewayBusiness paymentGatewayBusiness,
            IOfficeAddressBusiness officeAdressBusiness,
            ISettingBusiness settingBusiness,
            IAddressBusiness addressBusiness,
            Lazy<IPricingItemBusiness> pricingItemBusiness,
            Lazy<IShortLinkBusiness> shortLinkBusiness)
        {
            _paymentGatewayBusiness = paymentGatewayBusiness;
            _officeAdressBusiness = officeAdressBusiness;
            _orderBusiness = orderBusiness;
            _addressBusiness = addressBusiness;
            _settingBusiness = settingBusiness;
            _pricingItemBusiness = pricingItemBusiness;
            _shortLinkBusiness = shortLinkBusiness;
        }
        #endregion

        #region Private Methods
        void LoadRelatedInfo(bool paymentGateway = false, LangType langType = LangType.Fa_En)
        {
            if (paymentGateway)
                ViewBag.PaymentGatewaies = _paymentGatewayBusiness.GetAll(true);

            ViewBag.Setting = _settingBusiness.Get();

            ViewBag.OfficeAddress = _officeAdressBusiness.Get(langType);
            #region GetLangType

            var list = new List<ItemTextValueModel<string, string>>();
            EnumExtension.FilterEnumWithAttributeOf<LangType, VisibleAttribute>().ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Key = item.GetLocalizeDescription(), Value = item.GetDisplayName() });
            });

            ViewBag.LangTypes = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == "1"
            }).ToList();
            #endregion
        }

        [NonAction]
        private void GetDeliverTyps(DeliveryType? deliverType = null)
        {
            var list = new List<ItemTextValueModel<string, DeliveryType?>>();
            list.Add(new ItemTextValueModel<string, DeliveryType?> { Value = null, Key = LocalMessage.PleaseSelect });

            var result = EnumConvertor.GetEnumElements<DeliveryType>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, DeliveryType?>() { Key = item.Description, Value = (DeliveryType)Enum.Parse(typeof(DeliveryType), item.Name) });
            });

            ViewBag.DeliverPaymentByMyselfStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == deliverType)
            }).ToList();
        }

        [NonAction]
        private void GetOrderStatuses(OrderStatus? orderStatus = null)
        {
            var list = new List<ItemTextValueModel<string, OrderStatus?>>();
            list.Add(new ItemTextValueModel<string, OrderStatus?> { Key = LocalMessage.PleaseSelect, Value = null });

            var result = EnumConvertor.GetEnumElements<OrderStatus>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, OrderStatus?>() { Key = item.Description, Value = (OrderStatus)Enum.Parse(typeof(OrderStatus), item.Name) });
            });

            ViewBag.OrderStatuses = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == orderStatus
            }).ToList();
        }
        #endregion

        #region Add
        /// <summary>
        /// for adding order with complete info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult DetailedAdd(string client)
        {
            LoadRelatedInfo();
            ViewBag.RemoveFromServer = false;
            ViewBag.ShowPricingItems = true;
            ViewBag.Client = string.IsNullOrEmpty(client);

            if (Request.Browser.IsMobileDevice) return View(MVC.Order.Views.DetailedAdd_Mobile, _pricingItemBusiness.Value.Get(true));
            else return View(_pricingItemBusiness.Value.Get(true));
        }

        [HttpPost]
        public virtual JsonResult DetailedAdd(Order model)
        {
            #region Init Some of Model Props
            model.UserId = (User as ICurrentUserPrincipal).UserId;
            model.OrderStatus = OrderStatus.WaitForPricing;
            #endregion
            var rep = _orderBusiness.Insert(model);
            if (!rep.IsSuccessful) return Json(new { IsSuccessful = false, rep.Message });
            return Json(new
            {
                IsSuccessful = true,
                Result = new
                {
                    Header = MvcExtention.RenderViewToString(this, MVC.Order.Views.Partials._SummaryHeader, new SummaryHeader { BGColor = "#fff", Color = "#000", Border = "1px solid #50c542", IconColor = "#50c542", Message = string.Format(Request.Browser.IsMobileDevice ? LocalMessage.AddSummaryHeaderTextForMobile : LocalMessage.AddSummaryHeaderText, rep.Result.OrderId) }),
                    Summary = MvcExtention.RenderViewToString(this, Request.Browser.IsMobileDevice ? MVC.Order.Views.Partials._DetailedSummary_Mobile : MVC.Order.Views.Partials._DetailedSummary, rep.Result)
                }
            });
        }
        #endregion

        [HttpGet, AllowAnonymous, Route("Order/CompleteOrder/{orderId:int}")]
        public virtual ActionResult CompleteOrder(int orderId, string block = "discount")
        {
            ViewBag.Block = block;

            var order = _orderBusiness.Find(orderId, "OrderItems");
            if (order == null)
                return View(MVC.Shared.Views.Error);
            LoadRelatedInfo(true, order.LangType);
            ViewBag.LangType = order.LangType;
            var userId = (User as ICurrentUserPrincipal).UserId;
            if (userId != order.UserId)
                return RedirectToAction(MVC.Order.ActionNames.History, MVC.Order.Name);
            return View(order);
        }

        [HttpPost]
        public virtual JsonResult SubmitCompleteOrder(CompleteOrderModel model)
        {
            #region Checking
            var gatewayRep = _paymentGatewayBusiness.GetPaymentGateway(model.PaymentGatewayId);
            if (!gatewayRep.IsSuccessful)
                return Json(new { IsSuccessful = false, Message = LocalMessage.RecordsNotFound });
            if (model.AddressId != null)
            {
                var addrRep = _addressBusiness.Find((User as ICurrentUserPrincipal).UserId, model.AddressId ?? 0);
                if (!addrRep.IsSuccessful)
                    return Json(new { IsSuccessful = false, Message = LocalMessage.RecordsNotFound });
            }
            #endregion
            #region Fill Some Props of Model
            model.UserId = (User as ICurrentUserPrincipal).UserId;
            #endregion
            var rep = _orderBusiness.CompleteOrder(model);
            if (!rep.IsSuccessful) return Json(rep);
            if (model.PaymentType == PaymentType.InPerson)
            {
                return Json(new { rep.IsSuccessful, Result = Url.Action(MVC.Attachment.ActionNames.UploadAfterTransacttion, MVC.Attachment.Name, new { rep.Result.OrderId }) });
            }
            var result = PaymentFactory.GetInstance(gatewayRep.Result.BankName).Do(gatewayRep.Result, new TransactionModel
            {
                OrderId = model.OrderId,
                PaymentGatewayId = model.PaymentGatewayId,
                Price = rep.Result.TotalPrice(),
                UserId = (User as ICurrentUserPrincipal).UserId
            });
            return Json(result);
        }

        [HttpGet]
        public virtual ActionResult Search(FilterOrderModel model)
        {
            GetDeliverTyps(model.DeliverType);
            GetOrderStatuses(model.OrderStatus);

            var result = _orderBusiness.Search(model);
            if (!Request.IsAjaxRequest()) return View(result);

            return PartialView(MVC.Order.Views.Partials._SearchList, result.Result);
        }

        [HttpGet]
        public virtual ActionResult History() => View(_orderBusiness.GetAllOrder((User as ICurrentUserPrincipal).UserId));

        /// <summary>
        /// Get details of order such as transactions and Attachments
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet, Route("Order/Detail/{orderId:int}")]
        public virtual ActionResult Detail(int orderId)
        {
            ViewBag.RemoveFromServer = false;
            ViewBag.Editable = false;

            var order = _orderBusiness.Find(orderId, "all");

            var officeAddress = _officeAdressBusiness.Get(order.LangType);
            if (officeAddress == null)
            {
                ViewBag.Location = officeAddress.Location;
            }
            else
            {
                ViewBag.Location = "35.701076,51.391528";
            }

            var userId = (User as ICurrentUserPrincipal).UserId;
            if (userId != order.UserId)
                return RedirectToAction(MVC.Order.ActionNames.History, MVC.Order.Name);
            return View(order);
        }

        [HttpGet, AllowAnonymous]
        public virtual PartialViewResult ShowList(Guid userId) => PartialView(MVC.Order.Views.Partials._ListForMobile, _orderBusiness.GetAllOrder(userId));

        [HttpGet, Route("Order/ConfirmDraft/{orderId:int}")]
        public virtual ActionResult ConfirmDraft(int orderId)
        {
            #region Checking
            var order = _orderBusiness.Find(orderId, "Attachments");
            if (order == null)
                return View(MVC.Shared.Views.Error);
            var userId = (User as ICurrentUserPrincipal).UserId;
            if (userId != order.UserId)
                return RedirectToAction(MVC.Order.ActionNames.History, MVC.Order.Name);
            #endregion
            return View(order);
        }

        [HttpPost]
        public virtual JsonResult ConfirmDraft([Bind(Include = "OrderId, IsConfirmedByClient, ConfirmComment")]Order model)
        {
            if (!ModelState.IsValid)
                return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            var order = _orderBusiness.Find(model.OrderId);
            if (order == null)
                return Json(new { IsSuccessful = false, Message = LocalMessage.RecordsNotFound });
            order.IsConfirmedByClient = model.IsConfirmedByClient;
            order.ConfirmComment = model.ConfirmComment;
            if (model.IsConfirmedByClient)
                order.OrderStatus = OrderStatus.DeliveryFiles;
            return Json(_orderBusiness.Update(order));
        }

        [HttpGet, AllowAnonymous, Route("Order/Pay/{code}")]
        public virtual ViewResult Pay(string code)
        {
            var shortlink = _shortLinkBusiness.Value.Find(code);
            if (shortlink == null)
                return View(MVC.Order.Views.NotFound);

            var order = _orderBusiness.Find(shortlink.OrderId, "OrderItems, Transactions");
            if (order.OrderId != shortlink.OrderId)
                return View(MVC.Order.Views.NotFound);
            ViewBag.OrderId = order.OrderId;
            ViewBag.LangType = order.LangType;
            LoadRelatedInfo(true, order.LangType);
            return View(order);
        }

        [HttpPost, Route("Order/Pay/{code}")]
        public virtual JsonResult Pay(int orderId, int paymentGatewayId, string code)
        {
            var shortlink = _shortLinkBusiness.Value.Find(code);
            if (shortlink == null)
                return Json(new { IsSuccessful = false, Message = LocalMessage.RecordsNotFound });

            var gatewayRep = _paymentGatewayBusiness.GetPaymentGateway(paymentGatewayId);
            if (!gatewayRep.IsSuccessful)
                return Json(new { IsSuccessful = false, Message = LocalMessage.RecordsNotFound });
            var order = _orderBusiness.Find(orderId, "OrderItems,Transactions");
            var result = PaymentFactory.GetInstance(gatewayRep.Result.BankName).Do(gatewayRep.Result, new TransactionModel
            {
                OrderId = orderId,
                PaymentGatewayId = paymentGatewayId,
                Price = order.TotalPrice() - order.Transactions.Where(x => x.IsSuccess).Sum(x => x.Price),
                UserId = shortlink.UserId
            });
            return Json(result);
        }

        [HttpGet, Route("Order/PayAllFactor/{orderId:int}")]
        public virtual ActionResult PayAllFactor(int orderId)
        {
            var order = _orderBusiness.Find(orderId, "OrderItems,Transactions");
            if (order == null)
                return View(MVC.Shared.Views.Error);

            LoadRelatedInfo(true, order.LangType);
            var userId = (User as ICurrentUserPrincipal).UserId;
            if (userId != order.UserId)
                return RedirectToAction(MVC.Order.ActionNames.History, MVC.Order.Name);
            return View(order);
        }

        [HttpPost]
        public virtual JsonResult SubmitPayAllFactor(int orderId, PaymentType paymentType, int paymentGatewayId)
        {
            if (paymentType == PaymentType.InPerson)
            {
                return Json(new { IsSuccessful = true, Result = Url.Action(MVC.Attachment.ActionNames.UploadAfterTransacttion, MVC.Attachment.Name, new { orderId }) });
            }
            var gatewayRep = _paymentGatewayBusiness.GetPaymentGateway(paymentGatewayId);
            if (!gatewayRep.IsSuccessful)
                return Json(new { IsSuccessful = false, Message = LocalMessage.RecordsNotFound });
            var order = _orderBusiness.Find(orderId, "OrderItems,Transactions");
            var result = PaymentFactory.GetInstance(gatewayRep.Result.BankName).Do(gatewayRep.Result, new TransactionModel
            {
                OrderId = orderId,
                PaymentGatewayId = paymentGatewayId,
                Price = order.TotalPrice() - order.Transactions.Where(x => x.IsSuccess).Sum(x => x.Price),
                UserId = (User as ICurrentUserPrincipal).UserId
            });
            return Json(result);
        }

    }
}