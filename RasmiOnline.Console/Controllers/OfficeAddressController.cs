namespace RasmiOnline.Console.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Gnu.Framework.AspNet.Mvc;
    using Gnu.Framework.Core;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Console.Properties;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using RasmiOnline.Domain.Enum;
    using Domain;

    public partial class OfficeAddressController : Controller
    {
        #region Contructor
        private readonly IOfficeAddressBusiness _officeAddressBusiness;

        public OfficeAddressController(IOfficeAddressBusiness officeAddressBusiness)
        {
            _officeAddressBusiness = officeAddressBusiness;
        }
        #endregion

        #region Private Method
        private void GetIsActive(bool isActive = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.ActivateStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isActive)
            }).ToList();
        }

        private void GetLangTypes(string langType = nameof(LangType.Fa_En))
        {
            var list = new List<ItemTextValueModel<string, string>>();
            EnumExtension.FilterEnumWithAttributeOf<LangType, VisibleAttribute>().ForEach(item =>
                            {
                                list.Add(new ItemTextValueModel<string, string>() { Key = item.GetLocalizeDescription(), Value = item.GetDisplayName() });
                            });

            ViewBag.LangTypes = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == langType
            }).ToList();
        }
        #endregion

        [HttpGet]
        public virtual PartialViewResult Add()
        {
            GetLangTypes();
            GetIsActive();
            return PartialView(MVC.OfficeAddress.Views.Partial._Address, new OfficeAddress
            {
                MobileNumber = "9",
                Location = "35.701076,51.391528" //default location
            });
        }

        [HttpPost]
        public virtual JsonResult Add(OfficeAddress model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            var rep = _officeAddressBusiness.Insert(model);
            if (!rep.IsSuccessful) return Json(rep);
            return Json(new
            {
                IsSuccessful = true,
                Result = MvcExtention.RenderViewToString(this, MVC.OfficeAddress.Views.Partial._ListAddress, _officeAddressBusiness.GetAll())
            });
        }

        [HttpGet]
        public virtual ActionResult Edit(int officeAddressId)
        {
            var rep = _officeAddressBusiness.Find(officeAddressId);
            if (!rep.IsSuccessful) return Content($"<div class='empty-row'>{LocalMessage.ThereIsNoRecord}</div>");

            GetLangTypes(rep.Result.LangType.ToString());
            GetIsActive(rep.Result.IsActive);

            return PartialView(MVC.OfficeAddress.Views.Partial._Address, rep.Result);
        }

        [HttpPost]
        public virtual JsonResult Edit(OfficeAddress model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            return Json(_officeAddressBusiness.Update(model));
        }

        [HttpGet]
        public virtual ViewResult Manage() => View();

        [ChildActionOnly]
        public virtual PartialViewResult ListOfAddress() => PartialView(MVC.OfficeAddress.Views.Partial._ListAddress, _officeAddressBusiness.GetAll());

        [HttpPost]
        public virtual JsonResult EditStatus(int OfficeAddressId) => Json(_officeAddressBusiness.ChangeActiveStatus(OfficeAddressId));
    }
}