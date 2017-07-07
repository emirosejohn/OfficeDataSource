using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Database;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationController : Controller
    {
        private readonly IOfficeLocationRepository _officeLocationRepository;
        private readonly IOfficeDataTableGateway _officeDataTableGateway;

        public OfficeLocationController()
        {
            
            _officeDataTableGateway = new OfficeDataTableGateway( new DatabaseSettings(), new SystemLog());
            _officeLocationRepository = new OfficeLocationRepository(_officeDataTableGateway);
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
 