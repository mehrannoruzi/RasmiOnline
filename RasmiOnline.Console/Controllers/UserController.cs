namespace RasmiOnline.Console.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using System.Collections.Generic;
    using RasmiOnline.Console.Properties;
    using Gnu.Framework.Core.Authentication;
    using Domain.Entity;

    public partial class UserController : Controller
    {
        #region Contructor
        private readonly IUserBusiness _userBusiness;
        private readonly IRoleBusiness _roleBusiness;
        private readonly IAttachmentBusiness _attachmentBusiness;
        private readonly IMessageBusiness _messagingBusiness;
        readonly Lazy<IAddressBusiness> _addressBusiness;

        public UserController(IUserBusiness userBusiness, IAttachmentBusiness attachmentBusiness, IMessageBusiness messageBusiness, Lazy<IAddressBusiness> addressBusiness, IRoleBusiness roleBusiness)
        {
            _userBusiness = userBusiness;
            _attachmentBusiness = attachmentBusiness;
            _messagingBusiness = messageBusiness;
            _addressBusiness = addressBusiness;
            _roleBusiness = roleBusiness;
        }
        #endregion

        #region Private Methods
        [NonAction]
        private void GetUserRoles(string roleId = "0", bool forSearch = false)
        {
            var list = new List<ItemTextValueModel<string, string>>();
            list.Add(new ItemTextValueModel<string, string> { Key = (forSearch ? LocalMessage.All : LocalMessage.PleaseSelect), Value = "0" });
            var result = _roleBusiness.GetAll();
            result.Result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Value = item.RoleId.ToString(), Key = $"{item.RoleNameFa}-[{item.RoleNameEn}]" });
            });

            ViewBag.Roles = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == roleId
            }).ToList();
        }

        [NonAction]
        private void GetIsActive(bool? isActive = null)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();
            list.Add(new ItemTextValueModel<string, bool?> { Value = null, Key = LocalMessage.PleaseSelect });
            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.ActiveStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isActive)
            }).ToList();
        }
        #endregion

        [HttpGet]
        public virtual ActionResult Search(FilterUserModel model)
        {
            GetIsActive(model.IsActive);

            var result = _userBusiness.Search(model);
            if (!Request.IsAjaxRequest()) return View(result);

            return PartialView(MVC.User.Views.Partials._SearchList, result.Result);
        }

        [ChildActionOnly, AllowAnonymous]
        public virtual ContentResult GetMenu() => Content(_userBusiness.GetAvailableActions((User as ICurrentUserPrincipal).UserId)?.Menu);

        [HttpGet]
        public virtual new ActionResult Profile()
        {
            var currentUser = _userBusiness.Find((User as ICurrentUserPrincipal).UserId);
            ViewBag.RegisterDateMi = currentUser.RegisterDateMi;
            ViewBag.LastLoginDateMi = currentUser.LastLoginDateMi;

            return View(new ProfileModel().CopyFrom(currentUser));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual new JsonResult Profile(ProfileModel model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            var user = _userBusiness.Find((User as ICurrentUserPrincipal).UserId);
            user.UpdateWith(model);
            return Json(_userBusiness.Update(user));
        }

        public virtual ActionResult UserProfile(Guid userId, string opRes = null)
        {
            var user = _userBusiness.Find(userId);
            GetIsActive(user.IsActive);
            ViewBag.opRes = opRes;
            return View(user);
        }

        [HttpPost]
        public virtual JsonResult UpdateInfo(PersonalInfo model)
        {
            if (!ModelState.IsValid)
                return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            return Json(_userBusiness.Update(model));
        }


        [HttpPost, ValidateAntiForgeryToken]
        public virtual JsonResult ChangePassword(ChangeCurrentPassword model)
        {
            ModelState.Remove("U");
            ModelState.Remove("C");
            if (!ModelState.IsValid) return Json(new { Message = LocalMessage.ValidationFailed, IsSuccessful = false });
            return Json(_userBusiness.ChangeCurrentPassword((User as ICurrentUserPrincipal).UserId, model.CurrentPassword, model.Password));
        }


        [HttpPost, ValidateAntiForgeryToken]
        public virtual JsonResult ChangeUserPassword(ChangeUserPassword model)
        {
            ModelState.Remove("U");
            ModelState.Remove("C");
            if (!ModelState.IsValid) return Json(new { Message = LocalMessage.ValidationFailed, IsSuccessful = false });
            return Json(_userBusiness.ChangeUserPassword(model.UserId, model.Password));
        }

        [HttpGet]
        public virtual ViewResult Add()
        {
            GetIsActive();
            GetUserRoles();
            return View(new ActionResponse<AddUserModel> { IsSuccessful = true, Result = new AddUserModel() });
        }

        [HttpGet]
        public virtual JsonResult FindUser(string username)
        {
            var user = _userBusiness.Find(username);
            if (user != null)
                return Json(new ActionResponse<User>
                {
                    Result = new User { FirstName = user.FullName, LastName = user.LastName, UserId = user.UserId },
                    IsSuccessful = true
                }, JsonRequestBehavior.AllowGet);

            return Json(new ActionResponse<string>
            {
                Message = LocalMessage.UserNotFound
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult FormSubmited(AddUserModel model)
        {
            var response = new ActionResponse<bool>();

            if (model.RoleId <= 0)
            {
                response.Message = LocalMessage.InvalidSelectedRole;
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (!MobileNumberExtension.IsValidMobileNumber(model.MobileNumber))
            {
                response.Message = LocalMessage.InvalidMobileNumber;
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (model.Password != model.RepeatePassword)
            {
                response.Message = LocalMessage.ThePasswordDoesNotMatchIt;
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (model.IsNull() || !ModelState.IsValid)
            {
                response.Message = LocalMessage.InvalidFormData;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return Json(_userBusiness.Insert(model), JsonRequestBehavior.AllowGet);
        }

        public virtual ViewResult Referral() => View(_userBusiness.GetReferralUser((User as ICurrentUserPrincipal).UserId));
    }
}