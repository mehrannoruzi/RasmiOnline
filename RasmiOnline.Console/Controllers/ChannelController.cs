namespace RasmiOnline.Console.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Gnu.Framework.AspNet.Mvc;
    using Gnu.Framework.Core;
    using Business.Protocol;
    using Properties;
    using Domain.Dto;
    using Domain.Entity;
    using Domain.Enum;
    using Domain;
    using Gnu.Framework.Core.Authentication;

    public partial class ChannelController : Controller
    {
        #region Contructor
        private readonly IChannelBusiness _channelBusiness;

        public ChannelController(IChannelBusiness channelBusiness)
        {
            _channelBusiness = channelBusiness;
        }
        #endregion


        [HttpGet]
        public virtual PartialViewResult Add()
        {
            return PartialView(MVC.Channel.Views.Partial._Channel, new Channel { });
        }

        [HttpPost]
        public virtual JsonResult Add(Channel model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            var rep = _channelBusiness.Insert(model);
            if (!rep.IsSuccessful) return Json(rep);
            return Json(new
            {
                IsSuccessful = true,
                Result = MvcExtention.RenderViewToString(this, MVC.Channel.Views.Partial._ListChannel, _channelBusiness.GetAll())
            });
        }

        [HttpGet]
        public virtual ActionResult Edit(int ChannelId)
        {
            var rep = _channelBusiness.Find(ChannelId);
            if (!rep.IsSuccessful) return Content($"<div class='empty-row'>{LocalMessage.ThereIsNoRecord}</div>");
            return PartialView(MVC.Channel.Views.Partial._Channel, rep.Result);
        }

        [HttpPost]
        public virtual JsonResult Edit(Channel model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            return Json(_channelBusiness.Update(model));
        }

        [HttpPost]
        public virtual JsonResult Visit(string refrence) => Json(_channelBusiness.UpdateClick(refrence));


        [HttpGet]
        public virtual ViewResult Manage() => View();

        [ChildActionOnly]
        public virtual PartialViewResult ListOfChannel() => PartialView(MVC.Channel.Views.Partial._ListChannel, _channelBusiness.GetAll());

        [ChildActionOnly]
        public virtual PartialViewResult GetAllUserChannel() => PartialView(MVC.Channel.Views.Partial._ListUserChannel, _channelBusiness.GetAllUserChannel((User as ICurrentUserPrincipal).UserId));

        
    }
}