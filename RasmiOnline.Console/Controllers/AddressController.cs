namespace RasmiOnline.Console.Controllers
{
    using System;
    using System.Web.Mvc;
    using Business.Protocol;
    using RasmiOnline.Domain.Entity;
    using RasmiOnline.Console.Properties;
    using Gnu.Framework.Core.Authentication;
    using Gnu.Framework.AspNet.Mvc;

    //[RoutePrefix("Portal/SellOrder"), Route("{action}")]
    public partial class AddressController : Controller
    {
        #region Contructor
        private readonly IAddressBusiness _addressBusiness;
        public AddressController(IAddressBusiness addressBusiness)
        {
            _addressBusiness = addressBusiness;
        }
        #endregion

        [HttpGet]
        public virtual PartialViewResult Add() => PartialView(MVC.Address.Views.Partial._Address,new Address {
            MobileNumber = 9,
            Location= "35.701076,51.391528"//default location
            
        });

        [HttpPost]
        public virtual JsonResult Add(Address model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            model.UserId = (User as ICurrentUserPrincipal).UserId;
            var rep = _addressBusiness.Insert(model);
            if(!rep.IsSuccessful) return Json(rep);
            return Json(new {
                IsSuccessful = true,
                Result =MvcExtention.RenderViewToString(this,MVC.Address.Views.Partial._ListAddress, _addressBusiness.GetAll(model.UserId))
            });
        }

        [HttpGet]
        public virtual ActionResult Edit(int addressId)
        {
            var rep = _addressBusiness.Find((User as ICurrentUserPrincipal).UserId,addressId);
            if (!rep.IsSuccessful) return Content($"<div class='empty-row'>{LocalMessage.ThereIsNoRecord}</div>");
            return PartialView(MVC.Address.Views.Partial._Address, rep.Result);
        }

        [HttpPost]
        public virtual JsonResult Edit(Address model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            model.UserId = (User as ICurrentUserPrincipal).UserId;
            return Json(_addressBusiness.Update(model));
        }

        [HttpPost]
        public virtual JsonResult Delete(int addressId) => Json(_addressBusiness.Delete(addressId));

        [ChildActionOnly]
        public virtual PartialViewResult ListOfAddress(Guid userId) => PartialView(MVC.Address.Views.Partial._ListAddress, _addressBusiness.GetAll(userId));
    }
}