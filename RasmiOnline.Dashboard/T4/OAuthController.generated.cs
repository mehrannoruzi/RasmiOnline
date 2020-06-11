// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace RasmiOnline.Dashboard.Controllers
{
    public partial class OAuthController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected OAuthController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SignUp()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SignUp);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SendSms()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SendSms);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult ConfirmMobileNumber()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ConfirmMobileNumber);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SignIn()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SignIn);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult RecoverPassword()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RecoverPassword);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SignInPanel()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SignInPanel);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public OAuthController Actions { get { return MVC.OAuth; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "OAuth";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "OAuth";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string SignUp = "SignUp";
            public readonly string SendSms = "SendSms";
            public readonly string ConfirmMobileNumber = "ConfirmMobileNumber";
            public readonly string SignIn = "SignIn";
            public readonly string SignOut = "SignOut";
            public readonly string RecoverPassword = "RecoverPassword";
            public readonly string SignInPanel = "SignInPanel";
            public readonly string SignOutPanel = "SignOutPanel";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string SignUp = "SignUp";
            public const string SendSms = "SendSms";
            public const string ConfirmMobileNumber = "ConfirmMobileNumber";
            public const string SignIn = "SignIn";
            public const string SignOut = "SignOut";
            public const string RecoverPassword = "RecoverPassword";
            public const string SignInPanel = "SignInPanel";
            public const string SignOutPanel = "SignOutPanel";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string block = "block";
        }
        static readonly ActionParamsClass_SignUp s_params_SignUp = new ActionParamsClass_SignUp();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SignUp SignUpParams { get { return s_params_SignUp; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SignUp
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_SendSms s_params_SendSms = new ActionParamsClass_SendSms();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SendSms SendSmsParams { get { return s_params_SendSms; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SendSms
        {
            public readonly string userId = "userId";
        }
        static readonly ActionParamsClass_ConfirmMobileNumber s_params_ConfirmMobileNumber = new ActionParamsClass_ConfirmMobileNumber();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ConfirmMobileNumber ConfirmMobileNumberParams { get { return s_params_ConfirmMobileNumber; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ConfirmMobileNumber
        {
            public readonly string userId = "userId";
            public readonly string code = "code";
        }
        static readonly ActionParamsClass_SignIn s_params_SignIn = new ActionParamsClass_SignIn();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SignIn SignInParams { get { return s_params_SignIn; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SignIn
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_RecoverPassword s_params_RecoverPassword = new ActionParamsClass_RecoverPassword();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RecoverPassword RecoverPasswordParams { get { return s_params_RecoverPassword; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RecoverPassword
        {
            public readonly string recoveryUserName = "recoveryUserName";
        }
        static readonly ActionParamsClass_SignInPanel s_params_SignInPanel = new ActionParamsClass_SignInPanel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SignInPanel SignInPanelParams { get { return s_params_SignInPanel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SignInPanel
        {
            public readonly string userId = "userId";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Index = "Index";
            }
            public readonly string Index = "~/Views/OAuth/Index.cshtml";
            static readonly _PartialsClass s_Partials = new _PartialsClass();
            public _PartialsClass Partials { get { return s_Partials; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _PartialsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _RecovePassword = "_RecovePassword";
                    public readonly string _SignIn = "_SignIn";
                    public readonly string _SignUp = "_SignUp";
                    public readonly string _Verification = "_Verification";
                }
                public readonly string _RecovePassword = "~/Views/OAuth/Partials/_RecovePassword.cshtml";
                public readonly string _SignIn = "~/Views/OAuth/Partials/_SignIn.cshtml";
                public readonly string _SignUp = "~/Views/OAuth/Partials/_SignUp.cshtml";
                public readonly string _Verification = "~/Views/OAuth/Partials/_Verification.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_OAuthController : RasmiOnline.Dashboard.Controllers.OAuthController
    {
        public T4MVC_OAuthController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string block);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(string block)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "block", block);
            IndexOverride(callInfo, block);
            return callInfo;
        }

        [NonAction]
        partial void SignUpOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Dashboard.SignUpModel model);

        [NonAction]
        public override System.Web.Mvc.JsonResult SignUp(RasmiOnline.Dashboard.SignUpModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SignUp);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SignUpOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void SendSmsOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, System.Guid userId);

        [NonAction]
        public override System.Web.Mvc.JsonResult SendSms(System.Guid userId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SendSms);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            SendSmsOverride(callInfo, userId);
            return callInfo;
        }

        [NonAction]
        partial void ConfirmMobileNumberOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, System.Guid userId, string code);

        [NonAction]
        public override System.Web.Mvc.JsonResult ConfirmMobileNumber(System.Guid userId, string code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ConfirmMobileNumber);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            ConfirmMobileNumberOverride(callInfo, userId, code);
            return callInfo;
        }

        [NonAction]
        partial void SignInOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Dashboard.SignInModel model);

        [NonAction]
        public override System.Web.Mvc.JsonResult SignIn(RasmiOnline.Dashboard.SignInModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SignIn);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SignInOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void SignOutOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SignOut()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SignOut);
            SignOutOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RecoverPasswordOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string recoveryUserName);

        [NonAction]
        public override System.Web.Mvc.JsonResult RecoverPassword(string recoveryUserName)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RecoverPassword);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "recoveryUserName", recoveryUserName);
            RecoverPasswordOverride(callInfo, recoveryUserName);
            return callInfo;
        }

        [NonAction]
        partial void SignInPanelOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, System.Guid userId);

        [NonAction]
        public override System.Web.Mvc.JsonResult SignInPanel(System.Guid userId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SignInPanel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            SignInPanelOverride(callInfo, userId);
            return callInfo;
        }

        [NonAction]
        partial void SignOutPanelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SignOutPanel()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SignOutPanel);
            SignOutPanelOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
