namespace RasmiOnline.Console.Filter
{
    using System.Web.Mvc;

    public class MandatoryWww : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                string url = filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri.ToLowerInvariant();

                if (!url.Contains("www"))
                {
                    url = url.Replace("http://", "https://www.");
                    url = url.Replace("https://", "https://www.");
                    filterContext.Result = new RedirectResult("https://panel.rasmionline.com", true);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}