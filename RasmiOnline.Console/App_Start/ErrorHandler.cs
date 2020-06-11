namespace RasmiOnline.Console
{
    using System.Web;
    using System.Web.Mvc;
    using System.Security.Authentication;
    using System.Web.Security;

    public sealed class ErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (filterContext.Exception is AuthenticationException || authCookie == null)
                    HttpContext.Current.Response.Redirect("/OAuth/Index?activePanel=signin", true);
                else if (filterContext.Exception is InvalidCredentialException)
                    HttpContext.Current.Response.Redirect("/OAuth/UnAuthorized", true);
                //else
                //    HttpContext.Current.Response.Redirect("/Panel/Error", true);
            }
            else
                base.OnException(filterContext);
        }
    }
}