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
    public partial class OfficeAddressController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected OfficeAddressController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult EditStatus()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.EditStatus);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public OfficeAddressController Actions { get { return MVC.OfficeAddress; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "OfficeAddress";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "OfficeAddress";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Add = "Add";
            public readonly string Edit = "Edit";
            public readonly string Manage = "Manage";
            public readonly string ListOfAddress = "ListOfAddress";
            public readonly string EditStatus = "EditStatus";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Add = "Add";
            public const string Edit = "Edit";
            public const string Manage = "Manage";
            public const string ListOfAddress = "ListOfAddress";
            public const string EditStatus = "EditStatus";
        }


        static readonly ActionParamsClass_Add s_params_Add = new ActionParamsClass_Add();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Add AddParams { get { return s_params_Add; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Add
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string officeAddressId = "officeAddressId";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_EditStatus s_params_EditStatus = new ActionParamsClass_EditStatus();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditStatus EditStatusParams { get { return s_params_EditStatus; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditStatus
        {
            public readonly string OfficeAddressId = "OfficeAddressId";
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
                public readonly string Manage = "Manage";
            }
            public readonly string Manage = "~/Views/OfficeAddress/Manage.cshtml";
            static readonly _PartialClass s_Partial = new _PartialClass();
            public _PartialClass Partial { get { return s_Partial; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _PartialClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _Address = "_Address";
                    public readonly string _ListAddress = "_ListAddress";
                    public readonly string _OfficeAddress = "_OfficeAddress";
                }
                public readonly string _Address = "~/Views/OfficeAddress/Partial/_Address.cshtml";
                public readonly string _ListAddress = "~/Views/OfficeAddress/Partial/_ListAddress.cshtml";
                public readonly string _OfficeAddress = "~/Views/OfficeAddress/Partial/_OfficeAddress.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_OfficeAddressController : RasmiOnline.Dashboard.Controllers.OfficeAddressController
    {
        public T4MVC_OfficeAddressController() : base(Dummy.Instance) { }

        [NonAction]
        partial void AddOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult Add()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Add);
            AddOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AddOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Entity.OfficeAddress model);

        [NonAction]
        public override System.Web.Mvc.JsonResult Add(RasmiOnline.Domain.Entity.OfficeAddress model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Add);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AddOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int officeAddressId);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int officeAddressId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "officeAddressId", officeAddressId);
            EditOverride(callInfo, officeAddressId);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Entity.OfficeAddress model);

        [NonAction]
        public override System.Web.Mvc.JsonResult Edit(RasmiOnline.Domain.Entity.OfficeAddress model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ManageOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Manage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Manage);
            ManageOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ListOfAddressOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult ListOfAddress()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.ListOfAddress);
            ListOfAddressOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void EditStatusOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int OfficeAddressId);

        [NonAction]
        public override System.Web.Mvc.JsonResult EditStatus(int OfficeAddressId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.EditStatus);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "OfficeAddressId", OfficeAddressId);
            EditStatusOverride(callInfo, OfficeAddressId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
