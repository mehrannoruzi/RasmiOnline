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
    public partial class PricingItemController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected PricingItemController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Search()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Search);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult FormSubmited()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.FormSubmited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult GetPricingItemTable()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetPricingItemTable);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public PricingItemController Actions { get { return MVC.PricingItem; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "PricingItem";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "PricingItem";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Get = "Get";
            public readonly string Search = "Search";
            public readonly string GetForm = "GetForm";
            public readonly string FormSubmited = "FormSubmited";
            public readonly string Delete = "Delete";
            public readonly string GetPricingItemTable = "GetPricingItemTable";
            public readonly string GetPricingItemAccordion = "GetPricingItemAccordion";
            public readonly string MustlyUsed = "MustlyUsed";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Get = "Get";
            public const string Search = "Search";
            public const string GetForm = "GetForm";
            public const string FormSubmited = "FormSubmited";
            public const string Delete = "Delete";
            public const string GetPricingItemTable = "GetPricingItemTable";
            public const string GetPricingItemAccordion = "GetPricingItemAccordion";
            public const string MustlyUsed = "MustlyUsed";
        }


        static readonly ActionParamsClass_Get s_params_Get = new ActionParamsClass_Get();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Get GetParams { get { return s_params_Get; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Get
        {
            public readonly string str = "str";
            public readonly string isPricingItem = "isPricingItem";
        }
        static readonly ActionParamsClass_Search s_params_Search = new ActionParamsClass_Search();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Search SearchParams { get { return s_params_Search; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Search
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_GetForm s_params_GetForm = new ActionParamsClass_GetForm();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetForm GetFormParams { get { return s_params_GetForm; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetForm
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_FormSubmited s_params_FormSubmited = new ActionParamsClass_FormSubmited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_FormSubmited FormSubmitedParams { get { return s_params_FormSubmited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_FormSubmited
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetPricingItemTable s_params_GetPricingItemTable = new ActionParamsClass_GetPricingItemTable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetPricingItemTable GetPricingItemTableParams { get { return s_params_GetPricingItemTable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetPricingItemTable
        {
            public readonly string templateId = "templateId";
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
                public readonly string Search = "Search";
            }
            public readonly string Search = "~/Views/PricingItem/Search.cshtml";
            static readonly _PartialsClass s_Partials = new _PartialsClass();
            public _PartialsClass Partials { get { return s_Partials; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _PartialsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _Form = "_Form";
                    public readonly string _MustlyUsed = "_MustlyUsed";
                    public readonly string _PriceItemAccordion = "_PriceItemAccordion";
                    public readonly string _PriceItemTable = "_PriceItemTable";
                    public readonly string _SearchList = "_SearchList";
                    public readonly string _SearchPricingItem = "_SearchPricingItem";
                }
                public readonly string _Form = "~/Views/PricingItem/Partials/_Form.cshtml";
                public readonly string _MustlyUsed = "~/Views/PricingItem/Partials/_MustlyUsed.cshtml";
                public readonly string _PriceItemAccordion = "~/Views/PricingItem/Partials/_PriceItemAccordion.cshtml";
                public readonly string _PriceItemTable = "~/Views/PricingItem/Partials/_PriceItemTable.cshtml";
                public readonly string _SearchList = "~/Views/PricingItem/Partials/_SearchList.cshtml";
                public readonly string _SearchPricingItem = "~/Views/PricingItem/Partials/_SearchPricingItem.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_PricingItemController : RasmiOnline.Dashboard.Controllers.PricingItemController
    {
        public T4MVC_PricingItemController() : base(Dummy.Instance) { }

        [NonAction]
        partial void GetOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string str, bool? isPricingItem);

        [NonAction]
        public override System.Web.Mvc.JsonResult Get(string str, bool? isPricingItem)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Get);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "str", str);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "isPricingItem", isPricingItem);
            GetOverride(callInfo, str, isPricingItem);
            return callInfo;
        }

        [NonAction]
        partial void SearchOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, RasmiOnline.Domain.Dto.FilterPricingItemModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Search(RasmiOnline.Domain.Dto.FilterPricingItemModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Search);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SearchOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void GetFormOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetForm(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetForm);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetFormOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void FormSubmitedOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Entity.PricingItem model);

        [NonAction]
        public override System.Web.Mvc.JsonResult FormSubmited(RasmiOnline.Domain.Entity.PricingItem model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.FormSubmited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            FormSubmitedOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.JsonResult Delete(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetPricingItemTableOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, RasmiOnline.Domain.Enum.CalculatorTemplate templateId);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetPricingItemTable(RasmiOnline.Domain.Enum.CalculatorTemplate templateId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetPricingItemTable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "templateId", templateId);
            GetPricingItemTableOverride(callInfo, templateId);
            return callInfo;
        }

        [NonAction]
        partial void GetPricingItemAccordionOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetPricingItemAccordion()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetPricingItemAccordion);
            GetPricingItemAccordionOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void MustlyUsedOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult MustlyUsed()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.MustlyUsed);
            MustlyUsedOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
