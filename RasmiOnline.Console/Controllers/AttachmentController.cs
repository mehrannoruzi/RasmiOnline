namespace RasmiOnline.Console.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using RasmiOnline.Business.Protocol;
    using System.Collections.Generic;
    using Domain.Enum;
    using System;
    using Properties;
    using Gnu.Framework.Core.Authentication;

    public partial class AttachmentController : Controller
    {
        #region Constructor
        readonly IAttachmentBusiness _attachmentBusiness;
        readonly Lazy<IOrderBusiness> _orderBusiness;
        readonly Lazy<IOrderItemBusiness> _orderItemBusiness;

        public AttachmentController(IAttachmentBusiness attachmentBusiness, Lazy<IOrderBusiness> orderBusiness, Lazy<IOrderItemBusiness> orderItemBusiness)
        {
            _attachmentBusiness = attachmentBusiness;
            _orderBusiness = orderBusiness;
            _orderItemBusiness = orderItemBusiness;
        }
        #endregion


        [HttpGet, AllowAnonymous]
        public virtual FileResult Download(int attachmentId, int orderId)
        {
            var file = _attachmentBusiness.Find(attachmentId, orderId);
            if (file == null || !System.IO.File.Exists(Server.MapPath(file.Address))) return File("", System.Net.Mime.MediaTypeNames.Application.Octet, "FileDoNotExist");
            return File(System.IO.File.ReadAllBytes(Server.MapPath(file.Address)), System.Net.Mime.MediaTypeNames.Application.Octet, file.OriginalFileName);
        }

        [HttpGet, AllowAnonymous]
        public virtual PartialViewResult GetOrderAttachments(int orderId, AttachmentType attachmentType) => PartialView(MVC.Attachment.Views.Partials._List, _attachmentBusiness.Get(orderId, attachmentType));

        [HttpPost]
        public virtual JsonResult Delete(int id) => Json(_attachmentBusiness.Delete(id));

        #region Add Attachments
        [HttpGet, Route("Upload/{orderId:int}")]
        public virtual ActionResult UploadAfterTransacttion(int orderId)
        {
            var order = _orderBusiness.Value.Find(orderId, "OrderItems,OrderNames,Attachments");
            #region Checking
            if (order == null)
                return View(MVC.Shared.Views.Error);
            var userId = (User as ICurrentUserPrincipal).UserId;
            if (userId != order.UserId)
                return RedirectToAction(MVC.Order.ActionNames.History, MVC.Order.Name);
            #endregion
            return View(order);
        }

        [HttpPost, AllowAnonymous]
        public virtual JsonResult AddOrderAttachments(int orderId, AttachmentType attachmentType)
        {
            var files = new List<HttpPostedFileBase>();
            foreach (string fileName in Request.Files)
                files.Add(Request.Files[fileName]);
            var order = _orderBusiness.Value.Find(orderId);
            if (order == null)
                return Json(new { IsSuccessful = false, Message = LocalMessage.RecordsNotFound });
            return Json(_attachmentBusiness.Insert(order, attachmentType, files));
        }
        #endregion


    }
}