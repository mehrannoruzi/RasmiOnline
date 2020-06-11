namespace RasmiOnline.Dashboard.Controllers
{
    using Gnu.Framework.AspNet.Mvc;
    using RasmiOnline.Business.Protocol;
    using Properties;
    using RasmiOnline.Domain.Entity;
    using System.Web.Mvc;
    public partial class OrderNameController : Controller
    {
        #region Constructor
        IOrderNameBusiness _orderNameBusiness;
        public OrderNameController(IOrderNameBusiness orderNameBusiness)
        {
            _orderNameBusiness = orderNameBusiness;
        }
        #endregion

        [HttpPost]
        public virtual JsonResult Add(OrderName model)
        {
            if (!ModelState.IsValid) return Json(new { ISuccessful = false, Message = LocalMessage.ValidationFailed });
            var rep = _orderNameBusiness.Add(model);
            return Json(new
            {
                rep.IsSuccessful,
                rep.Message,
                Result = !rep.IsSuccessful ? null : MvcExtention.RenderViewToString(this, MVC.OrderName.Views.Partials._ShowItem, rep.Result)
            });
        }

        [HttpPost]
        public virtual JsonResult Delete(int id) => Json(_orderNameBusiness.Delete(id));
    }
}