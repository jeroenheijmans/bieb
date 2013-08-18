using System.Web.Mvc;

namespace Bieb.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmptyDatabase()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Disclaimer()
        {
            return View();
        }

        public ActionResult SiteMap()
        {
            return View();
        }
    }
}
