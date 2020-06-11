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
namespace RasmiOnline.Console.Controllers
{
    public partial class MessageController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected MessageController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult Submit()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Submit);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public MessageController Actions { get { return MVC.Message; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Message";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Message";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Send = "Send";
            public readonly string Submit = "Submit";
            public readonly string GetList = "GetList";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Send = "Send";
            public const string Submit = "Submit";
            public const string GetList = "GetList";
        }


        static readonly ActionParamsClass_Submit s_params_Submit = new ActionParamsClass_Submit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Submit SubmitParams { get { return s_params_Submit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Submit
        {
            public readonly string model = "model";
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
                public readonly string Send = "Send";
            }
            public readonly string Send = "~/Views/Message/Send.cshtml";
            static readonly _PartialsClass s_Partials = new _PartialsClass();
            public _PartialsClass Partials { get { return s_Partials; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _PartialsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _List = "_List";
                    public readonly string _SendForm = "_SendForm";
                }
                public readonly string _List = "~/Views/Message/Partials/_List.cshtml";
                public readonly string _SendForm = "~/Views/Message/Partials/_SendForm.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_MessageController : RasmiOnline.Console.Controllers.MessageController
    {
        public T4MVC_MessageController() : base(Dummy.Instance) { }

        [NonAction]
        partial void SendOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Send()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Send);
            SendOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SubmitOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Dto.SendMessageModel model);

        [NonAction]
        public override System.Web.Mvc.JsonResult Submit(RasmiOnline.Domain.Dto.SendMessageModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Submit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SubmitOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void GetListOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetList);
            GetListOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
