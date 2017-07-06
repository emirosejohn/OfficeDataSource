using System.Web.Mvc;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}