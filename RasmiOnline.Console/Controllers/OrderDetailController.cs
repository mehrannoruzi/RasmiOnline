namespace RasmiOnline.Console.Controllers
{
    using Business.Protocol;
    using Domain.Entity;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Gnu.Framework.AspNet.Mvc;
    using RasmiOnline.Domain.Enum;

    public partial class OrderDetailController : Controller
    {
        #region Constructor
        readonly IOrderDetailBusiness _orderDetailBusiness;
        readonly Lazy<IOrderItemBusiness> _orderItemBusiness;
        public OrderDetailController(IOrderDetailBusiness orderDetailBusiness, Lazy<IOrderItemBusiness> orderItemBusiness)
        {
            _orderDetailBusiness = orderDetailBusiness;
            _orderItemBusiness = orderItemBusiness;
        }
        #endregion

        [HttpPost]
        public virtual JsonResult AddOrUpdate(int orderId, IEnumerable<OrderDetail> orderDetails)
        {
            var rep = _orderDetailBusiness.AddOrUpdate(orderDetails);
            return Json(new
            {
                rep.Message,
                rep.IsSuccessful,
                Result = !rep.IsSuccessful ? null : MvcExtention.RenderViewToString(this, MVC.OrderDetail.Views.Partials._EditableList, _orderItemBusiness.Value.Get(orderId, OrderItemType.PricingItem, "OrderDetails"))
            });
        }

        [HttpPost]
        public virtual JsonResult Delete(int id)
        {
            return Json(new { IsSuccessful = true });
        }
    }
}