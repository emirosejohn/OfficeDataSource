using System.Web.Mvc;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationController : Controller
    {

        /*  public TreeController(ISchemeGateway sg, IRegionSchemeRepository rsr)
        {
            //fill the interfaces that the model is dependent on
            //plus repository
            _schemegateway = sg;
            _regionSchemeRepository = rsr;
        }

    */

        public ActionResult Index()
        {
            //fill model
            return View();
        }
    }
}
 