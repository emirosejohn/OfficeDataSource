using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationController : Controller
    {
        private readonly IOfficeLocationRepository _officeLocationRepository;
        private readonly IOfficeDataTableGateway _officeDataTableGateway;

        public OfficeLocationController(IOfficeLocationRepository officeLocationRepository, IOfficeDataTableGateway officeDataTableGateway)
        {
            _officeLocationRepository = officeLocationRepository;
            _officeDataTableGateway = officeDataTableGateway;
        }

        public ActionResult Index()
        {
            //fill model
            //OfficeDto[] DTOs = _officeDataTableGateway.GetAll();
            OfficeModel model = new OfficeModel();

            //need to set Offices in model so can access
            model.Offices = _officeLocationRepository.GetAll();
            
            return View(model);
        }
    }
}
 