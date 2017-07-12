using System.Collections.Generic;
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
            officeModel.Countries = _countryRepository.GetAllCountries().AsEnumerable();
            officeModel.OperatingOptions = WebHelper.GenerateOperatingOptions();
            officeModel.OfficeEdit = new OfficeLocation();


            return View(officeModel);
        }

        public ActionResult IndexBK()
        {
            OfficeModel officeModel = new OfficeModel();

            officeModel.ShowOfficeEdit = false;
            officeModel.Offices = _officeLocationRepository.GetAll();
            officeModel.Countries = _countryRepository.GetAllCountries().AsEnumerable();
            officeModel.OfficeEdit = new OfficeLocation();
            officeModel.OperatingOptions = WebHelper.GenerateOperatingOptions();

            return View(officeModel);
        }

        public ActionResult Edit(int id)
        {
            OfficeLocation toEditOffice = _officeLocationRepository.GetById(id);

            var officeModel = new OfficeModel();

            officeModel.Offices = _officeLocationRepository.GetAll();
            officeModel.OfficeEdit = toEditOffice;
            officeModel.ShowOfficeEdit = true;
            officeModel.Countries = _countryRepository.GetAllCountries().AsEnumerable();
            officeModel.OperatingOptions = WebHelper.GenerateOperatingOptions();

            return View(officeModel);
        }
        
        [HttpPost]
        public ActionResult Save(OfficeModel offcieModel)
        {
            if (offcieModel.OfficeEdit != null)
            {
                _officeLocationRepository.Update(offcieModel.OfficeEdit);
            }

            return RedirectToAction("Index");
         }
    }

    public static class WebHelper
    {
        public static IEnumerable<SelectListItem> GenerateOperatingOptions()
        {
            var selectedItems = new List<SelectListItem>();
            selectedItems.Add(new SelectListItem()
            {
                Value = "Active",
                Text= "Active"
            });
            selectedItems.Add(new SelectListItem()
            {
                Value = "Closed",
                Text = "Closed"
            });

            return selectedItems;
        }

    }
}
 