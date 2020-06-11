namespace RasmiOnline.Console
{
    using Controllers;
    using RasmiOnline.DependencyResolver.Ioc;
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class IocControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                var url = requestContext.HttpContext.Request.RawUrl;
                requestContext.RouteData.Values["controller"] = "Portal" /*MVC.Authentication.Name*/;
                requestContext.RouteData.Values["action"] = "Home"/*MVC.Authentication.ActionNames.SignIn*/;
                return IocInitializer.GetInstance(typeof(OrderController /*AuthenticationController*/)) as Controller;
            }
            return IocInitializer.GetInstance(controllerType) as Controller;
        }
    }
}