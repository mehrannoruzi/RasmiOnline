namespace RasmiOnline.Console.Controllers
{
    using System.Web.Mvc;
    using Business.Protocol;
    using Gnu.Framework.AspNet.Mvc;
    public partial class OrderItemController : Controller
    {
        #region Contructor
        private readonly IOrderItemBusiness _orderItemBusiness;
        public OrderItemController(IOrderItemBusiness orderItemBusiness)
        {
            _orderItemBusiness = orderItemBusiness;
        }
        #endregion

        [HttpGet]
        public virtual PartialViewResult Get(int orderItem) => PartialView(MVC.OrderItem.Views.Partials._List, _orderItemBusiness.Get(orderItem));

        [HttpPost]
        public virtual JsonResult Delete(int id)
        {
            var rep = _orderItemBusiness.Delete(id);
            if (!rep.IsSuccessful) return Json(new { IsSuccessful = false, rep.Message });
            return Json(new
            {
                IsSuccessful = true,
                rep.Message,
                Result = MvcExtention.RenderViewToString(this, MVC.OrderItem.Views.Partials._EditableList, _orderItemBusiness.Get(rep.Result.OrderId))
            });
        }

    }
}