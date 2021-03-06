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
namespace T4MVC
{
    public class RoleController
    {

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

}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
