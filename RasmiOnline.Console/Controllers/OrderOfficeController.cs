namespace RasmiOnline.Console.Controllers
{
    using Gnu.Framework.Core;
    using Gnu.Framework.Core.Authentication;
    using Business.Protocol;
    using Domain;
    using Domain.Entity;
    using Domain.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using RasmiOnline.Domain.Dto;

    public partial class OrderOfficeController : Controller
    {
        readonly ISettingBusiness _settingBusiness;
        readonly IOrderBusiness _orderBusiness;
        readonly IAddressBusiness _addressBusiness;
        readonly IOfficeAddressBusiness _officeAddressBusiness;
        readonly IPaymentGatewayBusiness _paymentGatewayBusiness;
        readonly Lazy<IPricingItemBusiness> _pricingItemBusiness;
        readonly Lazy<IOrderDetailBusiness> _orderDetailBusiness;
        readonly Lazy<ITransactionBusiness> _transactionBusiness;
        #region Constructor
        public OrderOfficeController(IOrderBusiness orderBusiness,
            ISettingBusiness settingBusiness,
            IAddressBusiness addressBusiness,
            IPaymentGatewayBusiness paymentGatewayBusiness,
            IOfficeAddressBusiness officeAddressBusiness,
            Lazy<IPricingItemBusiness> pricingItemBusiness,
            Lazy<IOrderDetailBusiness> orderDetailBusiness,
             Lazy<ITransactionBusiness> transactionBusiness)
        {
            _transactionBusiness = transactionBusiness;
            _settingBusiness = settingBusiness;
            _orderBusiness = orderBusiness;
            _addressBusiness = addressBusiness;
            _pricingItemBusiness = pricingItemBusiness;
            _officeAddressBusiness = officeAddressBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _paymentGatewayBusiness = paymentGatewayBusiness;
        }
        #endregion


        #region Private Methods
        private IEnumerable<SelectListItem> GetOrderStatusList() =>
            EnumConvertor.GetEnumElements<OrderStatus>().Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Name
            });
        void LoadRelatedInfo(bool paymentGateway = false, LangType langType = LangType.Fa_En)
        {
            if (paymentGateway)
                ViewBag.PaymentGatewaies = _paymentGatewayBusiness.GetAll(true);

            ViewBag.Setting = _settingBusiness.Get();

            //ViewBag.OfficeAddress = _officeAddressBusiness.Get(langType);
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
        #endregion
        public virtual ActionResult Manage()
        {
            ViewBag.TodayOrderCount = _orderBusiness.TodayOrderCount((User as ICurrentUserPrincipal).UserId);
            ViewBag.AllUnReadPayment = _transactionBusiness.Value.AllUnReadOfficePaymentCount((User as ICurrentUserPrincipal).UserId);
            ViewBag.OlderOrderCount = _orderBusiness.OlderOrderCount();
            return View(_orderBusiness.GetOrderDetails((User as ICurrentUserPrincipal).UserId));
        }

        [HttpGet]
        public virtual ViewResult Add()
        {
            LoadRelatedInfo();
            ViewBag.RemoveFromServer = false;
            ViewBag.ShowPricingItems = true;
            //ViewBag.OfficeUserId = GetOfficeList();
            return View(_pricingItemBusiness.Value.Get(true));
        }

        [HttpPost]
        public virtual JsonResult Add(Order model)
        {
            model.OfficeUserId = (User as ICurrentUserPrincipal).UserId;
            return Json(_orderBusiness.InsertBehalfOfUser(model, int.Parse(AppSettings.UserRoleId)));
        }

        [HttpGet, Route("OrderOffice/Edit/{orderId}")]
        public virtual ViewResult Edit(int orderId)
        {
            ViewBag.Editable = true;
            ViewBag.Client = false;
            ViewBag.OrderStatuses = GetOrderStatusList();
            ViewBag.TotalPayment = _transactionBusiness.Value.GetTotalPayedPrice(orderId);
            ViewBag.LangTypes = EnumExtension.FilterEnumWithAttributeOf<LangType, VisibleAttribute>().Select(x => new SelectListItem
            {
                Text = x.GetDescription(),
                Value = x.GetDisplayName()
            }).ToList();
            var order = _orderBusiness.Find(orderId, (User as ICurrentUserPrincipal).UserId, "all");
            ViewBag.LangType = order.LangType;
            if (order == null)
                return View(MVC.Shared.Views.Error);
            var details = _orderDetailBusiness.Value.GetAll(orderId);
            order.OrderItems.ForEach((item) => { item.OrderDetails = details.Where(x => x.OrderItemId == item.OrderItemId).ToList(); });
            ViewBag.Location = order.AddressId == null ?  "35.701076,51.391528" : order.Address.Location;
            return View(order);
        }

        [HttpPost, Route("OrderOffice/Edit/{orderId}")]
        public virtual JsonResult Edit(int orderId, LangType langType, List<OrderItem> items)
        {
            var rep = _orderBusiness.Update(orderId, langType, items);
            if (!rep.IsSuccessful) return Json(new { IsSuccessful = false, rep.Message });
            rep.Result.OrderItems.ForEach(x => x.Order = null);
            return Json(new
            {
                IsSuccessful = true,
                rep.Message,
                Result = rep.Result.OrderItems
            });
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public virtual JsonResult Update(Order model)
        {
            var updateRep = _orderBusiness.BriefUpdate(model);
            return Json(updateRep);
        }

        [ChildActionOnly]
        public virtual PartialViewResult GetAllTodayOrderList() => PartialView(MVC.OrderAdmin.Views.Partials._AllTodayOrder, _orderBusiness.AllTodayOrder((User as ICurrentUserPrincipal).UserId));

        [ChildActionOnly]
        public virtual PartialViewResult GetAllOlderOrderList() => PartialView(MVC.OrderAdmin.Views.Partials._AllTodayOrder, _orderBusiness.AllOlderOrder((User as ICurrentUserPrincipal).UserId));


        [HttpGet]
        public virtual PartialViewResult GetOrderList(OrderStatus? orderStatus = OrderStatus.WaitForPricing) => PartialView(MVC.OrderOffice.Views.Partials._OrderListView, _orderBusiness.GetAllOrder(officeUserId: (User as ICurrentUserPrincipal).UserId, orderStatus: orderStatus));

        [HttpGet]
        public virtual PartialViewResult GetOrderListMobile(OrderStatus? orderStatus = OrderStatus.WaitForPricing) => PartialView(MVC.OrderOffice.Views.Partials._OrderListViewMobile, _orderBusiness.GetAllOrder(officeUserId: (User as ICurrentUserPrincipal).UserId, orderStatus: orderStatus));

        [HttpGet]
        public virtual PartialViewResult GetMobileOrderType() => PartialView(MVC.OrderOffice.Views.Partials._OrderBoardMobile, _orderBusiness.GetOrderDetails((User as ICurrentUserPrincipal).UserId));
    }
}