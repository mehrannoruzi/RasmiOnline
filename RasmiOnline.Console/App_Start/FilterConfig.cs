namespace RasmiOnline.Console
{
    using Filter;
    using System.Web.Mvc;
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler());
            // filters.Add(new MandatoryWww());
        }
    }
}
