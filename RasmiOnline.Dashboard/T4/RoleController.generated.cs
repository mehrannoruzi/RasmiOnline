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
    public partial class RoleController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RoleController(Dummy d) { }

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
        public virtual System.Web.Mvc.PartialViewResult GetUserRoleList()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetUserRoleList);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult FindUser()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.FindUser);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult AddUserInRole()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddUserInRole);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult AddViewInRole()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddViewInRole);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RoleController Actions { get { return MVC.Role; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Role";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Role";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Search = "Search";
            public readonly string GetForm = "GetForm";
            public readonly string FormSubmited = "FormSubmited";
            public readonly string Delete = "Delete";
            public readonly string GetAddUserInRole = "GetAddUserInRole";
            public readonly string GetUserRoleList = "GetUserRoleList";
            public readonly string FindUser = "FindUser";
            public readonly string AddUserInRole = "AddUserInRole";
            public readonly string DeleteUserInRole = "DeleteUserInRole";
            public readonly string GetAddViewInRole = "GetAddViewInRole";
            public readonly string GetViewInRoleList = "GetViewInRoleList";
            public readonly string AddViewInRole = "AddViewInRole";
            public readonly string DeleteViewInRole = "DeleteViewInRole";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Search = "Search";
            public const string GetForm = "GetForm";
            public const string FormSubmited = "FormSubmited";
            public const string Delete = "Delete";
            public const string GetAddUserInRole = "GetAddUserInRole";
            public const string GetUserRoleList = "GetUserRoleList";
            public const string FindUser = "FindUser";
            public const string AddUserInRole = "AddUserInRole";
            public const string DeleteUserInRole = "DeleteUserInRole";
            public const string GetAddViewInRole = "GetAddViewInRole";
            public const string GetViewInRoleList = "GetViewInRoleList";
            public const string AddViewInRole = "AddViewInRole";
            public const string DeleteViewInRole = "DeleteViewInRole";
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
        static readonly ActionParamsClass_GetAddUserInRole s_params_GetAddUserInRole = new ActionParamsClass_GetAddUserInRole();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetAddUserInRole GetAddUserInRoleParams { get { return s_params_GetAddUserInRole; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetAddUserInRole
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetUserRoleList s_params_GetUserRoleList = new ActionParamsClass_GetUserRoleList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetUserRoleList GetUserRoleListParams { get { return s_params_GetUserRoleList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetUserRoleList
        {
            public readonly string mobileNumber = "mobileNumber";
        }
        static readonly ActionParamsClass_FindUser s_params_FindUser = new ActionParamsClass_FindUser();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_FindUser FindUserParams { get { return s_params_FindUser; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_FindUser
        {
            public readonly string roleId = "roleId";
            public readonly string mobileNumber = "mobileNumber";
        }
        static readonly ActionParamsClass_AddUserInRole s_params_AddUserInRole = new ActionParamsClass_AddUserInRole();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddUserInRole AddUserInRoleParams { get { return s_params_AddUserInRole; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddUserInRole
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_DeleteUserInRole s_params_DeleteUserInRole = new ActionParamsClass_DeleteUserInRole();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteUserInRole DeleteUserInRoleParams { get { return s_params_DeleteUserInRole; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteUserInRole
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetAddViewInRole s_params_GetAddViewInRole = new ActionParamsClass_GetAddViewInRole();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetAddViewInRole GetAddViewInRoleParams { get { return s_params_GetAddViewInRole; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetAddViewInRole
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetViewInRoleList s_params_GetViewInRoleList = new ActionParamsClass_GetViewInRoleList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetViewInRoleList GetViewInRoleListParams { get { return s_params_GetViewInRoleList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetViewInRoleList
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_AddViewInRole s_params_AddViewInRole = new ActionParamsClass_AddViewInRole();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddViewInRole AddViewInRoleParams { get { return s_params_AddViewInRole; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddViewInRole
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_DeleteViewInRole s_params_DeleteViewInRole = new ActionParamsClass_DeleteViewInRole();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteViewInRole DeleteViewInRoleParams { get { return s_params_DeleteViewInRole; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteViewInRole
        {
            public readonly string id = "id";
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
            public readonly string Search = "~/Views/Role/Search.cshtml";
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
                    public readonly string _SearchList = "_SearchList";
                    public readonly string _SearchRole = "_SearchRole";
                }
                public readonly string _Form = "~/Views/Role/Partials/_Form.cshtml";
                public readonly string _SearchList = "~/Views/Role/Partials/_SearchList.cshtml";
                public readonly string _SearchRole = "~/Views/Role/Partials/_SearchRole.cshtml";
                static readonly _UserInRoleClass s_UserInRole = new _UserInRoleClass();
                public _UserInRoleClass UserInRole { get { return s_UserInRole; } }
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public partial class _UserInRoleClass
                {
                    static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                    public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                    public class _ViewNamesClass
                    {
                        public readonly string _AddUserInRole = "_AddUserInRole";
                        public readonly string _UserInRoleList = "_UserInRoleList";
                    }
                    public readonly string _AddUserInRole = "~/Views/Role/Partials/UserInRole/_AddUserInRole.cshtml";
                    public readonly string _UserInRoleList = "~/Views/Role/Partials/UserInRole/_UserInRoleList.cshtml";
                }
                static readonly _ViewInRoleClass s_ViewInRole = new _ViewInRoleClass();
                public _ViewInRoleClass ViewInRole { get { return s_ViewInRole; } }
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public partial class _ViewInRoleClass
                {
                    static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                    public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                    public class _ViewNamesClass
                    {
                        public readonly string _AddViewInRole = "_AddViewInRole";
                        public readonly string _ViewInRoleList = "_ViewInRoleList";
                    }
                    public readonly string _AddViewInRole = "~/Views/Role/Partials/ViewInRole/_AddViewInRole.cshtml";
                    public readonly string _ViewInRoleList = "~/Views/Role/Partials/ViewInRole/_ViewInRoleList.cshtml";
                }
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RoleController : RasmiOnline.Dashboard.Controllers.RoleController
    {
        public T4MVC_RoleController() : base(Dummy.Instance) { }

        [NonAction]
        partial void SearchOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, RasmiOnline.Domain.Dto.FilterRoleModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Search(RasmiOnline.Domain.Dto.FilterRoleModel model)
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
        partial void FormSubmitedOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Entity.Role model);

        [NonAction]
        public override System.Web.Mvc.JsonResult FormSubmited(RasmiOnline.Domain.Entity.Role model)
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
        partial void GetAddUserInRoleOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetAddUserInRole(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetAddUserInRole);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetAddUserInRoleOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetUserRoleListOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, long mobileNumber);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetUserRoleList(long mobileNumber)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetUserRoleList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "mobileNumber", mobileNumber);
            GetUserRoleListOverride(callInfo, mobileNumber);
            return callInfo;
        }

        [NonAction]
        partial void FindUserOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int roleId, long mobileNumber);

        [NonAction]
        public override System.Web.Mvc.JsonResult FindUser(int roleId, long mobileNumber)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.FindUser);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "roleId", roleId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "mobileNumber", mobileNumber);
            FindUserOverride(callInfo, roleId, mobileNumber);
            return callInfo;
        }

        [NonAction]
        partial void AddUserInRoleOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Dto.AddUserInRoleModel model);

        [NonAction]
        public override System.Web.Mvc.JsonResult AddUserInRole(RasmiOnline.Domain.Dto.AddUserInRoleModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddUserInRole);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AddUserInRoleOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void DeleteUserInRoleOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.JsonResult DeleteUserInRole(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.DeleteUserInRole);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteUserInRoleOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetAddViewInRoleOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetAddViewInRole(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetAddViewInRole);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetAddViewInRoleOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetViewInRoleListOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetViewInRoleList(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetViewInRoleList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetViewInRoleListOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AddViewInRoleOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, RasmiOnline.Domain.Entity.ViewInRole model);

        [NonAction]
        public override System.Web.Mvc.JsonResult AddViewInRole(RasmiOnline.Domain.Entity.ViewInRole model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddViewInRole);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AddViewInRoleOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void DeleteViewInRoleOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.JsonResult DeleteViewInRole(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.DeleteViewInRole);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteViewInRoleOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
