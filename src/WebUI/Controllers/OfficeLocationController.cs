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
            OfficeLocationModel locationModel = new OfficeLocationModel();

            model.ShowOfficeEdit = false;
            locationModel.Offices = _officeLocationRepository.GetAll();
            locationModel.Countries = _countryRepository.GetAllCountries();


        }

        public ActionResult Edit(int id)
        {
            OfficeLocation toEditOffice = _officeLocationRepository.GetById(id);
            OfficeEditModel officeEditModel = new OfficeEditModel
            {
                OfficeId = toEditOffice.Id,
                NewOfficeName = toEditOffice.Name,
                NewAddress = toEditOffice.Address,
                NewCountry = toEditOffice.Country,
                NewSwitchboard = toEditOffice.Switchboard,
                NewFax = toEditOffice.Fax,
                NewTimeZone = toEditOffice.TimeZone,
                NewOperating = toEditOffice.Operating
            };
            return View(officeEditModel);
        }
    }
}
 