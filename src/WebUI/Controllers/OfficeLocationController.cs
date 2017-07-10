using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.OfficeLocationContext;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Database;
using OfficeLocationMicroservice.Database.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Helpers;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationController : Controller
    {
        private readonly IOfficeLocationRepository _officeLocationRepository;


        public OfficeLocationController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
        }

        //for tests
        public OfficeLocationController(IOfficeLocationRepository officeLocationRepository)
        {
            _officeLocationRepository = officeLocationRepository;
        }

        public ActionResult Index()
        {
            OfficeModel model = new OfficeModel();

            model.ShowOfficeEdit = false;
            model.Offices = _officeLocationRepository.GetAll();
            
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return null;
        }
    }
}
 