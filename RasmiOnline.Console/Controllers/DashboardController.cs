namespace RasmiOnline.Console.Controllers
{
    using System.Web.Mvc;

    //[RoutePrefix("Portal/SellOrder"), Route("{action}")]
    public partial class DashboardController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}