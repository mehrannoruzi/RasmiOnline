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
    public partial class OrderController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected OrderController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult DetailedAdd()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailedAdd);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult CompleteOrder()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CompleteOrder);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SubmitCompleteOrder()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SubmitCompleteOrder);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Search()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Search);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Detail()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detail);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult ShowList()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.ShowList);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ConfirmDraft()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ConfirmDraft);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Pay()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Pay);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PayAllFactor()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PayAllFactor);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SubmitPayAllFactor()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SubmitPayAllFactor);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public OrderController Actions { get { return MVC.Order; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Order";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Order";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string DetailedAdd = "DetailedAdd";
            public readonly string CompleteOrder = "CompleteOrder";
            public readonly string SubmitCompleteOrder = "SubmitCompleteOrder";
            public readonly string Search = "Search";
            public readonly string History = "History";
            public readonly string Detail = "Detail";
            public readonly string ShowList = "ShowList";
            public readonly string ConfirmDraft = "ConfirmDraft";
            public readonly string Pay = "Pay";
            public readonly string PayAllFactor = "PayAllFactor";
            public readonly string SubmitPayAllFactor = "SubmitPayAllFactor";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string DetailedAdd = "DetailedAdd";
            public const string CompleteOrder = "CompleteOrder";
            public const string SubmitCompleteOrder = "SubmitCompleteOrder";
            public const string Search = "Search";
            public const string History = "History";
            public const string Detail = "Detail";
            public const string ShowList = "ShowList";
            public const string ConfirmDraft = "ConfirmDraft";
            public const string Pay = "Pay";
            public const string PayAllFactor = "PayAllFactor";
            public const string SubmitPayAllFactor = "SubmitPayAllFactor";
        }


        static readonly ActionParamsClass_DetailedAdd s_params_DetailedAdd = new ActionParamsClass_DetailedAdd();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DetailedAdd DetailedAddParams { get { return s_params_DetailedAdd; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DetailedAdd
        {
            public readonly string client = "client";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_CompleteOrder s_params_CompleteOrder = new ActionParamsClass_CompleteOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CompleteOrder CompleteOrderParams { get { return s_params_CompleteOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CompleteOrder
        {
            public readonly string orderId = "orderId";
            public readonly string block = "block";
        }
        static readonly ActionParamsClass_SubmitCompleteOrder s_params_SubmitCompleteOrder = new ActionParamsClass_SubmitCompleteOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SubmitCompleteOrder SubmitCompleteOrderParams { get { return s_params_SubmitCompleteOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SubmitCompleteOrder
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Search s_params_Search = new ActionParamsClass_Search();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Search SearchParams { get { return s_params_Search; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Search
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Detail s_params_Detail = new ActionParamsClass_Detail();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Detail DetailParams { get { return s_params_Detail; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Detail
        {
            public readonly string orderId = "orderId";
        }
        static readonly ActionParamsClass_ShowList s_params_ShowList = new ActionParamsClass_ShowList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ShowList ShowListParams { get { return s_params_ShowList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ShowList
        {
            public readonly string userId = "userId";
        }
        static readonly ActionParamsClass_ConfirmDraft s_params_ConfirmDraft = new ActionParamsClass_ConfirmDraft();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ConfirmDraft ConfirmDraftParams { get { return s_params_ConfirmDraft; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ConfirmDraft
        {
            public readonly string orderId = "orderId";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Pay s_params_Pay = new ActionParamsClass_Pay();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Pay PayParams { get { return s_params_Pay; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Pay
        {
            public readonly string code = "code";
            public readonly string orderId = "orderId";
            public readonly string paymentGatewayId = "paymentGatewayId";
        }
        static readonly ActionParamsClass_PayAllFactor s_params_PayAllFactor = new ActionParamsClass_PayAllFactor();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PayAllFactor PayAllFactorParams { get { return s_params_PayAllFactor; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PayAllFactor
        {
            public readonly string orderId = "orderId";
        }
        static readonly ActionParamsClass_SubmitPayAllFactor s_params_SubmitPayAllFactor = new ActionParamsClass_SubmitPayAllFactor();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SubmitPayAllFactor SubmitPayAllFactorParams { get { return s_params_SubmitPayAllFactor; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SubmitPayAllFactor
        {
            public readonly string orderId = "orderId";
            public readonly string paymentType = "paymentType";
            public readonly string paymentGatewayId = "paymentGatewayId";
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
                public readonly string _Layout = "_Layout";
                public readonly string CompleteOrder = "CompleteOrder";
                public readonly string ConfirmDraft = "ConfirmDraft";
                public readonly string Detail = "Detail";
                public readonly string Detail_Mobile = "Detail.Mobile";
                public readonly string DetailedAdd = "DetailedAdd";
                public readonly string DetailedAdd_Mobile = "DetailedAdd.Mobile";
                public readonly string History = "History";
                public readonly string NotFound = "NotFound";
                public readonly string Pay = "Pay";
                public readonly string Pay_Mobile = "Pay.Mobile";
                public readonly string PayAllFactor = "PayAllFactor";
                public readonly string Search = "Search";
            }
            public readonly string _Layout = "~/Views/Order/_Layout.cshtml";
            public readonly string CompleteOrder = "~/Views/Order/CompleteOrder.cshtml";
            public readonly string ConfirmDraft = "~/Views/Order/ConfirmDraft.cshtml";
            public readonly string Detail = "~/Views/Order/Detail.cshtml";
            public readonly string Detail_Mobile = "~/Views/Order/Detail.Mobile.cshtml";
            public readonly string DetailedAdd = "~/Views/Order/DetailedAdd.cshtml";
            public readonly string DetailedAdd_Mobile = "~/Views/Order/DetailedAdd.Mobile.cshtml";
            public readonly string History = "~/Views/Order/History.cshtml";
            public readonly string NotFound = "~/Views/Order/NotFound.cshtml";
            public readonly string Pay = "~/Views/Order/Pay.cshtml";
            public readonly string Pay_Mobile = "~/Views/Order/Pay.Mobile.cshtml";
            public readonly string PayAllFactor = "~/Views/Order/PayAllFactor.cshtml";
            public readonly string Search = "~/Views/Order/Search.cshtml";
            static readonly _PartialsClass s_Partials = new _PartialsClass();
            public _PartialsClass Partials { get { return s_Partials; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _PartialsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _AddOrderItem = "_AddOrderItem";
                    public readonly string _AddOrderItem_Mobile = "_AddOrderItem.Mobile";
                    public readonly string _DetailedAddTimeLine = "_DetailedAddTimeLine";
                    public readonly string _DetailedSummary = "_DetailedSummary";
                    public readonly string _DetailedSummary_Mobile = "_DetailedSummary.Mobile";
                    public readonly string _GetOrderPaymentInfo = "_GetOrderPaymentInfo";
                    public readonly string _ListForMobile = "_ListForMobile";
                    public readonly string _OrderList = "_OrderList";
                    public readonly string _SearchList = "_SearchList";
                    public readonly string _SearchOrder = "_SearchOrder";
                    public readonly string _SelectDeliveryType = "_SelectDeliveryType";
                    public readonly string _SelectPaymentType = "_SelectPaymentType";
                    public readonly string _Show = "_Show";
                    public readonly string _Summary = "_Summary";
                    public readonly string _SummaryHeader = "_SummaryHeader";
                }
                public readonly string _AddOrderItem = "~/Views/Order/Partials/_AddOrderItem.cshtml";
                public readonly string _AddOrderItem_Mobile = "~/Views/Order/Partials/_AddOrderItem.Mobile.cshtml";
                public readonly string _DetailedAddTimeLine = "~/Views/Order/Partials/_DetailedAddTimeLine.cshtml";
                public readonly string _DetailedSummary = "~/Views/Order/Partials/_DetailedSummary.cshtml";
                public readonly string _DetailedSummary_Mobile = "~/Views/Order/Partials/_DetailedSummary.Mobile.cshtml";
                public readonly string _GetOrderPaymentInfo = "~/Views/Order/Partials/_GetOrderPaymentInfo.cshtml";
                public readonly string _ListForMobile = "~/Views/Order/Partials/_ListForMobile.cshtml";
                public readonly string _OrderList = "~/Views/Order/Partials/_OrderList.cshtml";
                public readonly string _SearchList = "~/Views/Order/Partials/_SearchList.cshtml";
                public readonly string _SearchOrder = "~/Views/Order/Partials/_SearchOrder.cshtml";
                public readonly string _SelectDeliveryType = "~/Views/Order/Partials/_SelectDeliveryType.cshtml";
                public readonly string _SelectPaymentType = "~/Views/Order/Partials/_SelectPaymentType.cshtml";
                public readonly string _Show = "~/Views/Order/Partials/_Show.cshtml";
                public readonly string _Summary = "~/Views/Order/Partials/_Summary.cshtml";
                public readonly string _SummaryHeader = "~/Views/Order/Partials/_SummaryHeader.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_OrderController : RasmiOnline.Dashboard.Controllers.OrderController
    {
        public T4MVC_OrderController() : base(Dummy.Instance) { }

        [NonAction]
        partial void DetailedAddOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string client);

        [NonAction]
        public override System.Web.Mvc.ActionResult DetailedAdd(string client)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailedAdd);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "client", client);
            DetailedAddOverride(callInfo, client);
            return callInfo;
        }

        [NonAction]
        partial void DetailedAddOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Entity.Order model);

        [NonAction]
        public override System.Web.Mvc.JsonResult DetailedAdd(RasmiOnline.Domain.Entity.Order model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.DetailedAdd);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            DetailedAddOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void CompleteOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int orderId, string block);

        [NonAction]
        public override System.Web.Mvc.ActionResult CompleteOrder(int orderId, string block)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CompleteOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderId", orderId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "block", block);
            CompleteOrderOverride(callInfo, orderId, block);
            return callInfo;
        }

        [NonAction]
        partial void SubmitCompleteOrderOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Dto.CompleteOrderModel model);

        [NonAction]
        public override System.Web.Mvc.JsonResult SubmitCompleteOrder(RasmiOnline.Domain.Dto.CompleteOrderModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SubmitCompleteOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SubmitCompleteOrderOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void SearchOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, RasmiOnline.Domain.Dto.FilterOrderModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Search(RasmiOnline.Domain.Dto.FilterOrderModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Search);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SearchOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void HistoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult History()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.History);
            HistoryOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DetailOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int orderId);

        [NonAction]
        public override System.Web.Mvc.ActionResult Detail(int orderId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detail);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderId", orderId);
            DetailOverride(callInfo, orderId);
            return callInfo;
        }

        [NonAction]
        partial void ShowListOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, System.Guid userId);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult ShowList(System.Guid userId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.ShowList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ShowListOverride(callInfo, userId);
            return callInfo;
        }

        [NonAction]
        partial void ConfirmDraftOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int orderId);

        [NonAction]
        public override System.Web.Mvc.ActionResult ConfirmDraft(int orderId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ConfirmDraft);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderId", orderId);
            ConfirmDraftOverride(callInfo, orderId);
            return callInfo;
        }

        [NonAction]
        partial void ConfirmDraftOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Entity.Order model);

        [NonAction]
        public override System.Web.Mvc.JsonResult ConfirmDraft(RasmiOnline.Domain.Entity.Order model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ConfirmDraft);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ConfirmDraftOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void PayOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, string code);

        [NonAction]
        public override System.Web.Mvc.ViewResult Pay(string code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Pay);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            PayOverride(callInfo, code);
            return callInfo;
        }

        [NonAction]
        partial void PayOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int orderId, int paymentGatewayId, string code);

        [NonAction]
        public override System.Web.Mvc.JsonResult Pay(int orderId, int paymentGatewayId, string code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Pay);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderId", orderId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "paymentGatewayId", paymentGatewayId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            PayOverride(callInfo, orderId, paymentGatewayId, code);
            return callInfo;
        }

        [NonAction]
        partial void PayAllFactorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int orderId);

        [NonAction]
        public override System.Web.Mvc.ActionResult PayAllFactor(int orderId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PayAllFactor);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderId", orderId);
            PayAllFactorOverride(callInfo, orderId);
            return callInfo;
        }

        [NonAction]
        partial void SubmitPayAllFactorOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int orderId, RasmiOnline.Domain.Enum.PaymentType paymentType, int paymentGatewayId);

        [NonAction]
        public override System.Web.Mvc.JsonResult SubmitPayAllFactor(int orderId, RasmiOnline.Domain.Enum.PaymentType paymentType, int paymentGatewayId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SubmitPayAllFactor);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderId", orderId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "paymentType", paymentType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "paymentGatewayId", paymentGatewayId);
            SubmitPayAllFactorOverride(callInfo, orderId, paymentType, paymentGatewayId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114