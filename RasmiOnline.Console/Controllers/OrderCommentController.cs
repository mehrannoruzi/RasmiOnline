namespace RasmiOnline.Console.Controllers
{
    using Gnu.Framework.AspNet.Mvc;
    using Business.Protocol;
    using Properties;
    using Domain.Entity;
    using System.Web.Mvc;
    public partial class OrderCommentController : Controller
    {
        #region Constructor
        IOrderCommentBusiness _orderCommentBusiness;
        public OrderCommentController(IOrderCommentBusiness orderCommentBusiness)
        {
            _orderCommentBusiness = orderCommentBusiness;
        }
        #endregion

        [HttpPost]
        public virtual JsonResult Add(OrderComment model)
        {
            if (!ModelState.IsValid) return Json(new { ISuccessful = false, Message = LocalMessage.ValidationFailed });
            var rep = _orderCommentBusiness.Add(model);
            return Json(new
            {
                rep.IsSuccessful,
                rep.Message,
                Result = !rep.IsSuccessful ? null : MvcExtention.RenderViewToString(this, MVC.OrderComment.Views.Partials._ShowItem, rep.Result)
            });
        }

        [HttpPost]
        public virtual JsonResult Delete(int id) => Json(_orderCommentBusiness.Delete(id));
    }
}