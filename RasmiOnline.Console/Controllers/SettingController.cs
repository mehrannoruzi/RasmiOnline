namespace RasmiOnline.Console.Controllers
{
    using System.Web.Mvc;
    using Gnu.Framework.Core;
    using Domain.Enum;
    using Domain.Entity;
    using Business.Protocol;
    using Properties;
    using Gnu.Framework.Core.Authentication;

    public partial class SettingController : Controller
    {
        #region Contructor
        private readonly ISettingBusiness _settingBusiness;
        private readonly IMessageBusiness _messageBusiness;
        private readonly IUserBusiness _userBusiness;

        public SettingController(ISettingBusiness settingBusiness, IMessageBusiness messageBusiness, IUserBusiness userBusiness)
        {
            _settingBusiness = settingBusiness;
            _messageBusiness = messageBusiness;
            _userBusiness = userBusiness;
        }
        #endregion

        [HttpGet]
        public virtual ActionResult Manage()
        {
            var response = new ActionResponse<Setting>();
            var result = _settingBusiness.Get();
            response.IsSuccessful = result.IsNotNull();
            response.Message = response.IsSuccessful ? string.Empty : LocalMessage.RecordsNotFound;
            response.Result = result;
            return View(response);
        }

        [HttpPost]
        public virtual JsonResult SubmitChange(Setting model)
        {
            if (model.IsNotNull().And(ModelState.IsValid))
                return Json(_settingBusiness.Update(model), JsonRequestBehavior.AllowGet);
            return Json(new ActionResponse<int> { Message = LocalMessage.BadRequest }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public virtual PartialViewResult GetSupportNumber() => PartialView(MVC.Setting.Views.Partials._SupportNumber, _settingBusiness.Get());

        [HttpPost]
        public virtual JsonResult SendAddressTo(string fullAddress)
        {
            var response = new ActionResponse<bool>();
            var currentUser = _userBusiness.Find((User as ICurrentUserPrincipal).UserId);
            var message = new Message
            {
                Type = MessagingType.Sms,
                Content = string.Format(LocalMessage.SendCompanyAddressToUser, currentUser.FullName, fullAddress),
                Receiver = currentUser.MobileNumber.ToString()
            };

            var result = _messageBusiness.Insert(message);
            response.IsSuccessful = response.Result = result.IsSuccessful;
            response.Message = result.Message;

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Setting Notification
        /// </summary>
        /// <returns></returns>
        public virtual ContentResult GetNotification() => Content(_settingBusiness.Get().ImportantNotice);
    }
}