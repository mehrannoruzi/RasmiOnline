namespace RasmiOnline.Console
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "OAuth", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Statistics", action = "GetDashboardStatistics", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "JustAction",
                url: "{action}",
                defaults: new { controller = "Portal", action = "Home", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Complete",
                url: "{controller}/{action}",
                defaults: new { controller = "Portal", action = "Home" }
            );
        }
    }
}
