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
        private OfficeModel _officeModel;


        public OfficeLocationController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
            _countryRepository = MasterFactory.GetCountryRepository();
            _officeModel = new OfficeModel();
        }

        //for tests
        public OfficeLocationController(
            IOfficeLocationRepository officeLocationRepository,
            CountryRepository countryRepository)
        {
            _officeLocationRepository = officeLocationRepository;
            _officeModel = new OfficeModel();
            _countryRepository = countryRepository;
        }

        public ActionResult Index()
        {
            OfficeModel officeModel = new OfficeModel();

            officeModel.ShowOfficeEdit = false;
            officeModel.Offices = _officeLocationRepository.GetAll();
            officeModel.Countries = _countryRepository.GetAllCountries();
            _officeModel = officeModel;

            return View(officeModel);
        }
	
        public ActionResult Edit(int id)
        {
            OfficeLocation toEditOffice = _officeLocationRepository.GetById(id);
            OfficeEditModel officeEditModel = new OfficeEditModel
            {
                OfficeId = toEditOffice.OfficeId,
                NewOfficeName = toEditOffice.Name,
                NewAddress = toEditOffice.Address,
                NewCountry = toEditOffice.Country,
                NewSwitchboard = toEditOffice.Switchboard,
                NewFax = toEditOffice.Fax,
                NewTimeZone = toEditOffice.TimeZone,
                NewOperating = toEditOffice.Operating
            };
            OfficeModel model = _officeModel;
            model.Offices = _officeLocationRepository.GetAll();
            model.OfficeEdit = officeEditModel;
            model.ShowOfficeEdit = true;
            // _officeLocationRepository.Update(locationModel.EditedOffice);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Save(OfficeLocationModel locationModel)
        {
            if (locationModel.EditedOffice != null)
            {
                _officeLocationRepository.Update(locationModel.EditedOffice);
            }

            return RedirectToAction("Index");
         }

    }
}
 