namespace RasmiOnline.Console.Controllers
{
    using System;
    using System.Web;
    using Domain.Entity;
    using System.Web.Mvc;
    using SharedPreference;
    using Gnu.Framework.Core;
    using System.Web.Security;
    using RasmiOnline.Domain.Enum;
    using Gnu.Framework.AspNet.Cache;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Console.Properties;
    using Gnu.Framework.Core.Authentication;
    using Gnu.Framework.Core.Security;

    public partial class OAuthController : Controller
    {
        #region Constructor
        readonly Lazy<ICacheProvider> _cache;
        readonly Lazy<IOrderBusiness> _orderBusiness;
        readonly IUserBusiness _userBusiness;
        readonly IVerificationCodeBusiness _verificationCodeBusiness;
        readonly Lazy<IMessageBusiness> _messageBusiness;
        public OAuthController(IUserBusiness userBusiness, Lazy<IOrderBusiness> orderBusiness, Lazy<ICacheProvider> cache, IVerificationCodeBusiness verificationCodeBusiness, Lazy<IMessageBusiness> messageBusiness)
        {
            _cache = cache;
            _userBusiness = userBusiness;
            _verificationCodeBusiness = verificationCodeBusiness;
            _messageBusiness = messageBusiness;
            _orderBusiness = orderBusiness;

        }
        #endregion

        #region Private Methods
        [NonAction]
        private void CreateTicket(User user, bool rememberMe)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                HttpCookie _AuthCookie = new HttpCookie($"_{FormsAuthentication.FormsCookieName}", (User as ICurrentUserPrincipal).UserId.ToString())
                {
                    Expires = authCookie.Expires
                };
                HttpContext.Response.Cookies.Add(_AuthCookie);
            }

            var currentUser = new CurrentUserPrincipal();
            currentUser.UserId = user.UserId;
            currentUser.FullName = $"{user.FirstName} {user.LastName}";
            currentUser.UserName = user.Email.ToString();
            currentUser.CustomField = new UserExtraData { MobileNumber = user.MobileNumber };
            var expDateTime = rememberMe ? DateTime.Now.AddHours(int.Parse(AppSettings.AuthTimeoutWithRemeberMeInHours)) : DateTime.Now.AddMinutes(int.Parse(AppSettings.AuthTimeoutInMiutes));
            string userData = currentUser.SerializeToJson();
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.MobileNumber.ToString(), DateTime.Now, expDateTime, true, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            {
                Expires = expDateTime,
                HttpOnly = true
            };
            //FormsAuthentication.set
            HttpContext.Response.Cookies.Add(cookie);
        }

        #endregion

        [HttpGet]
        public virtual ActionResult Index(string block = "signin")
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Order.ActionNames.History, MVC.Order.Name);
            ViewBag.ActiveBlock = block;
            return View();
        }

        #region Sign Up
        [HttpPost]
        public virtual JsonResult SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = LocalMessage.ValidationFailed });
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNumber = long.Parse(model.MobileNumber),
                Password = model.SignUpPassword,
                Email = model.Email,
                ReferralCode = model.ReferralCode
            };
            user.Password = model.SignUpPassword;
            var rep = _userBusiness.InsertCustomer(user, int.Parse(AppSettings.UserRoleId));
            if (!rep.IsSuccessful) return Json(new { IsSuccessful = false, Message = rep.Message });

            #region Get Current User Actions & Default View
            var menuRep = _userBusiness.GetAvailableActions(rep.Result);
            if (menuRep == null)
                return Json(new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = LocalMessage.ThereIsNoView
                });
            #endregion
            user.UserId = rep.Result;
            CreateTicket(user, false);
            var _userHasOrder = _orderBusiness.Value.HasOrder(user.UserId);
            return Json(new
            {
                IsSuccessful = true,
                Result = _userHasOrder ? Url.Action(menuRep.DefaultUserAction.Action, menuRep.DefaultUserAction.Controller, new { }) : Url.Action(MVC.Order.ActionNames.DetailedAdd, MVC.Order.Name),
            });
        }

        #region verification
        [HttpPost]
        public virtual JsonResult SendSms(Guid userId)
        {
            var findRep = _userBusiness.Find(userId);
            if (findRep == null)
                return Json(new { Message = LocalMessage.Error, IsSuccessful = false });
            var getCodeRep = _verificationCodeBusiness.GetCode(userId, findRep.MobileNumber.ToString());
            if (!getCodeRep.IsSuccessful)
                return Json(new { getCodeRep.Message, IsSuccessful = false });
            return Json(_messageBusiness.Value.Insert(new Message
            {
                Content = string.Format(LocalMessage.VerificationCodeSms, getCodeRep.Result.Code),
                Receiver = findRep.MobileNumber.ToString(),
                Type = MessagingType.Sms
            }));
        }

        [HttpPost]
        public virtual JsonResult ConfirmMobileNumber(Guid userId, string code)
        {
            var rep = _verificationCodeBusiness.VerifyCode(userId, code);
            if (!rep.IsSuccessful) return Json(rep);
            var activationRep = _userBusiness.Activate(userId);
            if (!activationRep.IsSuccessful) return Json(activationRep);
            #region Get Current User Actions & Default View
            var menuRep = _userBusiness.GetAvailableActions(userId);
            if (menuRep == null)
                return Json(new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = LocalMessage.ThereIsNoView
                });
            #endregion

            CreateTicket(activationRep.Result, false);
            var _userHasOrder = _orderBusiness.Value.HasOrder(activationRep.Result.UserId);
            return Json(new
            {
                IsSuccessful = true,
                Result = _userHasOrder ? Url.Action(menuRep.DefaultUserAction.Action, menuRep.DefaultUserAction.Controller, new { }) : Url.Action(MVC.Order.ActionNames.DetailedAdd, MVC.Order.Name),
            });
        }
        #endregion
        #endregion


        [HttpPost]
        public virtual JsonResult SignIn(SignInModel model)
        {
            #region Check UN & PW
            if (!ModelState.IsValid)
                return Json(new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = LocalMessage.ValidationFailed
                });
            var chkRep = _userBusiness.CheckUserCredentials(model.UserName, model.Password);
            if (!chkRep.IsSuccessful)
                return Json(new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = chkRep.Message
                });
            #endregion

            #region Get Current User Actions & Default View
            var menuRep = _userBusiness.GetAvailableActions(chkRep.Result.UserId);
            if (menuRep == null)
                return Json(new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = LocalMessage.ThereIsNoView
                });
            #endregion

            CreateTicket(chkRep.Result, model.RememberMe);

            if (menuRep.DefaultUserAction.RoleId != int.Parse(AppSettings.UserRoleId))
                return Json(new
                {
                    IsSuccessful = true,
                    Result = Url.Action(menuRep.DefaultUserAction.Action, menuRep.DefaultUserAction.Controller, new { }),
                });
            var _userHasOrder = _orderBusiness.Value.HasOrder(chkRep.Result.UserId);
            return Json(new
            {
                IsSuccessful = true,
                Result = _userHasOrder ? Url.Action(menuRep.DefaultUserAction.Action, menuRep.DefaultUserAction.Controller, new { }) : Url.Action(MVC.Order.ActionNames.DetailedAdd, MVC.Order.Name),
            });
        }



        [HttpGet]
        public virtual ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            if (User.Identity.IsAuthenticated)
            {
                _cache.Value.InvalidateItem($"Menu_{(User as ICurrentUserPrincipal).UserId.ToString().Replace("-", "_")}");
                _cache.Value.InvalidateItem($"UserActions_{(User as ICurrentUserPrincipal).UserId.ToString().Replace("-", "_")}");
            }
            return Redirect(GlobalVariable.SiteRootUrl);
        }

        /// <summary>
        /// recover password process includes:
        /// 1- inserting verificationCode
        /// 2- inserting message
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual JsonResult RecoverPassword(string recoveryUserName)
        {
            var user = _userBusiness.Find(recoveryUserName);
            if (user == null)
                return Json(new { IsSuccessful = false, Message = LocalMessage.UserNotFound });
            var pw = Randomizer.RandomInteger(6);
            user.Password = HashGenerator.Hash(pw.ToString().Trim(), HashAlgorithms.SHA1);
            var updateRep = _userBusiness.Update(user);
            if (!updateRep.IsSuccessful) return Json(new { updateRep.Message, IsSuccessful = false });

            return Json(_messageBusiness.Value.Insert(new Message
            {
                Receiver = user.MobileNumber.ToString(),
                Content = string.Format(LocalMessage.RecoverPasswordSms, "www.RasmiOnline.com", pw.ToString()),
                Type = MessagingType.Sms
            }));
        }


        #region Login Into UserPanel

        private ActionResponse<string> SignInUserPanel(Guid userId)
        {
            #region Check UN & PW
            if (Guid.Empty == userId)
                return new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = LocalMessage.ValidationFailed
                };

            var chkRep = _userBusiness.Find(userId);
            if (chkRep.IsNull())
                return new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = LocalMessage.UserNotFound
                };
            #endregion
            #region Get Current User Actions & Default View
            var menuRep = _userBusiness.GetAvailableActions(chkRep.UserId);
            if (menuRep == null)
                return new ActionResponse<string>
                {
                    IsSuccessful = false,
                    Message = LocalMessage.ThereIsNoView
                };
            #endregion
            CreateTicket(chkRep, false);

            if (menuRep.DefaultUserAction.RoleId != int.Parse(AppSettings.UserRoleId))
                return new ActionResponse<string>
                {
                    IsSuccessful = true,
                    Result = Url.Action(menuRep.DefaultUserAction.Action, menuRep.DefaultUserAction.Controller, new { }),
                };
            var _userHasOrder = _orderBusiness.Value.HasOrder(chkRep.UserId);
            return new ActionResponse<string>
            {
                IsSuccessful = true,
                Result = _userHasOrder ? Url.Action(menuRep.DefaultUserAction.Action, menuRep.DefaultUserAction.Controller, new { }) : Url.Action(MVC.Order.ActionNames.DetailedAdd, MVC.Order.Name),
            };
        }

        [HttpPost]
        public virtual JsonResult SignInPanel(Guid userId) => Json(SignInUserPanel(userId));

        [HttpGet]
        public virtual ActionResult SignOutPanel()
        {
            if (User.Identity.IsAuthenticated)
            {
                _cache.Value.InvalidateItem($"Menu_{(User as ICurrentUserPrincipal).UserId.ToString().Replace("-", "_")}");
                _cache.Value.InvalidateItem($"UserActions_{(User as ICurrentUserPrincipal).UserId.ToString().Replace("-", "_")}");
                FormsAuthentication.SignOut();
            }
            HttpCookie authCookie = Request.Cookies[$"_{FormsAuthentication.FormsCookieName}"];
            if (authCookie == null)
                return Redirect(GlobalVariable.SiteRootUrl);

            var result = SignInUserPanel(Guid.Parse(authCookie.Value));

            var userIdCookie = new HttpCookie($"_{FormsAuthentication.FormsCookieName}", Guid.Empty.ToString());
            userIdCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(userIdCookie);
            if (result.IsSuccessful)
                return Redirect(result.Result);
            return Redirect(GlobalVariable.SiteRootUrl);
        }
        #endregion
    }
}