namespace RasmiOnline.Console.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;
    using RasmiOnline.Console.Properties;

    public partial class RoleController : Controller
    {
        #region Contructor
        private readonly IRoleBusiness _roleBusiness;
        private readonly Lazy<IUserBusiness> _lazyUserBusiness;
        private readonly IUserInRoleBusiness _userInRoleBusiness;
        private readonly IViewInRoleBusiness _viewInRoleBusiness;

        public RoleController(IRoleBusiness roleBusiness, IUserInRoleBusiness userInRoleBusiness, IViewInRoleBusiness viewInRoleBusiness, Lazy<IUserBusiness> lazyUserBusiness)
        {
            _roleBusiness = roleBusiness;
            _lazyUserBusiness = lazyUserBusiness;
            _userInRoleBusiness = userInRoleBusiness;
            _viewInRoleBusiness = viewInRoleBusiness;
        }
        #endregion

        #region Private Methods

        [NonAction]
        private void GetIsActive(bool? isActive = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();
            if (showPleaseSelect) list.Add(new ItemTextValueModel<string, bool?> { Value = null, Key = LocalMessage.All });

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.ActiveStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isActive)
            }).ToList();
        }

        [NonAction]
        private void GetIsDefault(bool? isDefault = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();
            if (showPleaseSelect) list.Add(new ItemTextValueModel<string, bool?> { Value = null, Key = LocalMessage.All });

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.DefaultStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isDefault)
            }).ToList();
        }

        [NonAction]
        private void GetViews(int roleId)
        {
            var views = _viewInRoleBusiness.GetFilteredViewsFullPath(roleId);
            ViewBag.Views = views.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString()
            }).ToList();
        }
        #endregion

        #region Role

        [HttpGet]
        public virtual ActionResult Search(FilterRoleModel model)
        {
            GetIsActive();
            GetIsDefault();

            var result = _roleBusiness.Search(model);
            if (!Request.IsAjaxRequest()) return View(result);

            return PartialView(MVC.Role.Views.Partials._SearchList, result.Result);
        }

        [HttpGet]
        public virtual PartialViewResult GetForm(int id = default(int))
        {
            if (id > 0)
            {
                var findedRole = _roleBusiness.GetRole(id);
                GetIsActive(findedRole.Result.IsActive, false);
                GetIsDefault(findedRole.Result.IsDefault, false);
                return PartialView(MVC.Role.Views.Partials._Form, findedRole);
            }

            GetIsActive(showPleaseSelect: false);
            GetIsDefault(showPleaseSelect: false);
            return PartialView(MVC.Role.Views.Partials._Form, new ActionResponse<Role> { IsSuccessful = true, Result = new Role() });
        }

        [HttpPost]
        public virtual JsonResult FormSubmited(Role model)
        {
            var response = new ActionResponse<bool>();

            if (model.IsNull() || !ModelState.IsValid)
            {
                response.Message = LocalMessage.InvalidFormData;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            IActionResponse<int> bizResult = model.RoleId <= 0 ? bizResult = _roleBusiness.Insert(model) : bizResult = _roleBusiness.Update(model);
            response.Result = response.IsSuccessful = bizResult.IsSuccessful;
            response.Message = bizResult.Message;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult Delete(int id = default(int))
            => Json((id > 0 ? _roleBusiness.Delete(id) : new ActionResponse<bool> { Message = LocalMessage.BadRequest }), JsonRequestBehavior.AllowGet);

        #endregion

        #region User In Role
        [HttpGet]
        public virtual PartialViewResult GetAddUserInRole(int id = default(int))
        {
            GetIsActive(showPleaseSelect: false);
            return PartialView(MVC.Role.Views.Partials.UserInRole._AddUserInRole, new ActionResponse<AddUserInRoleModel> { IsSuccessful = true, Result = new AddUserInRoleModel { ExpireDateSh = PersianDateTime.Now.AddYears(10).ToString(PersianDateTimeFormat.Date), RoleId = id } });
        }

        [HttpGet]
        public virtual PartialViewResult GetUserRoleList(long mobileNumber)
            => PartialView(MVC.Role.Views.Partials.UserInRole._UserInRoleList, _userInRoleBusiness.GetUserInRole(mobileNumber));

        [HttpGet]
        public virtual JsonResult FindUser(int roleId, long mobileNumber)
        {
            if (roleId <= 0)
                return Json(new ActionResponse<string> { Message = LocalMessage.InvalidSelectedRole }, JsonRequestBehavior.AllowGet);

            if (!MobileNumberExtension.IsValidMobileNumber(mobileNumber))
                return Json(new ActionResponse<string> { Message = LocalMessage.InvalidMobileNumber }, JsonRequestBehavior.AllowGet);

            return Json(_userInRoleBusiness.GetUserInRoleChecked(roleId, mobileNumber), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult AddUserInRole(AddUserInRoleModel model)
        {
            if (model.IsNull() || !ModelState.IsValid)
                return Json(new ActionResponse<int> { Message = LocalMessage.InvalidFormData }, JsonRequestBehavior.AllowGet);

            var user = _lazyUserBusiness.Value.Find(model.SearchMobileNumber);
            if (user.IsNull())
                return Json(new ActionResponse<int> { Message = LocalMessage.UserNotFound }, JsonRequestBehavior.AllowGet);

            return Json(_userInRoleBusiness.Insert(new UserInRole
            {
                ExpireDateSh = model.ExpireDateSh,
                IsActive = model.IsActive,
                RoleId = model.RoleId,
                UserId = user.UserId
            }), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult DeleteUserInRole(int id = default(int))
            => Json((id > 0 ? _userInRoleBusiness.Delete(id) : new ActionResponse<bool> { Message = LocalMessage.BadRequest }), JsonRequestBehavior.AllowGet);


        #endregion

        #region View In Role
        [HttpGet]
        public virtual PartialViewResult GetAddViewInRole(int id = default(int))
        {
            GetIsDefault(showPleaseSelect: false);
            GetViews(id);
            return PartialView(MVC.Role.Views.Partials.ViewInRole._AddViewInRole, new ActionResponse<ViewInRole> { IsSuccessful = true, Result = new ViewInRole { ExpireDateSh = PersianDateTime.Now.AddYears(10).ToString(PersianDateTimeFormat.Date), RoleId = id } });
        }

        [HttpGet]
        public virtual PartialViewResult GetViewInRoleList(int id = default(int))
            => PartialView(MVC.Role.Views.Partials.ViewInRole._ViewInRoleList, _viewInRoleBusiness.GetViewsInRole(id));

        [HttpPost]
        public virtual JsonResult AddViewInRole(ViewInRole model)
        {
            if (model.IsNull() || !ModelState.IsValid)
                return Json(new ActionResponse<int> { Message = LocalMessage.InvalidFormData }, JsonRequestBehavior.AllowGet);

            return Json(_viewInRoleBusiness.Insert(model), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult DeleteViewInRole(int id = default(int))
            => Json((id > 0 ? _viewInRoleBusiness.Delete(id) : new ActionResponse<bool> { Message = LocalMessage.BadRequest }), JsonRequestBehavior.AllowGet);


        #endregion
    }
}