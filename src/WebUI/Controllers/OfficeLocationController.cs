using System.Diagnostics;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationController : Controller
    {
        private readonly IOfficeLocationRepository _officeLocationRepository;
        private readonly CountryRepository _countryRepository;

        public OfficeLocationController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
            _countryRepository = MasterFactory.GetCountryRepository();
        }

        //for tests
        public OfficeLocationController(
            IOfficeLocationRepository officeLocationRepository,
            CountryRepository countryRepository)
        {
            _officeLocationRepository = officeLocationRepository;
            _countryRepository = countryRepository;
        }

        public ActionResult Index()
        {
            OfficeModel officeModel = new OfficeModel();

            officeModel.ShowOfficeEdit = false;
            officeModel.Offices = _officeLocationRepository.GetAll();
            officeModel.Countries = _countryRepository.GetAllCountries();
            officeModel.OfficeEdit = new OfficeLocation();

            return View(officeModel);
        }
	
        public ActionResult Edit(int id)
        {
            OfficeLocation toEditOffice = _officeLocationRepository.GetById(id);

            var officeModel = new OfficeModel();

            officeModel.Offices = _officeLocationRepository.GetAll();
            officeModel.OfficeEdit = toEditOffice;
            officeModel.ShowOfficeEdit = true;
            officeModel.Countries = _countryRepository.GetAllCountries();

            return View(officeModel);
        }
        
        [HttpPost]
        public ActionResult Save(OfficeLocation locationModel)
        {
            if (locationModel != null)
            {
                _officeLocationRepository.Update(locationModel);
            }

            return RedirectToAction("Index");
         }

    }
}
 