using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
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
            OfficeLocationModel locationModel = new OfficeLocationModel();

            locationModel.Offices = _officeLocationRepository.GetAll();
            locationModel.Countries = _countryRepository.getAllCountries();


            return View(locationModel);
        }
    }
}
 