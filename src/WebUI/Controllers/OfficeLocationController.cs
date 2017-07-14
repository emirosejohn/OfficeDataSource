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
        private readonly OfficeLocationRepository _officeLocationRepository;
        private readonly CountryRepository _countryRepository;

        public OfficeLocationController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
            _countryRepository = MasterFactory.GetCountryRepository();
        }

        //for tests
        public OfficeLocationController(
            OfficeLocationRepository officeLocationRepository,
            CountryRepository countryRepository)
        {
            _officeLocationRepository = officeLocationRepository;
            _countryRepository = countryRepository;
        }

        public ActionResult Index()
        {
            OfficeModel officeModel = new OfficeModel();

            officeModel.Offices = _officeLocationRepository.GetAll();
            officeModel.Countries = _countryRepository.GetAllCountries().AsEnumerable();
            officeModel.OperatingOptions = WebHelper.GenerateOperatingOptions();
            officeModel.NewOffice = new OfficeLocation();


            return View(officeModel);
        }
        
        [HttpPost]
        public ActionResult Save(OfficeModel offcieModel)
        {
            if (offcieModel.NewOffice != null)
            {
                var officelocation =  _officeLocationRepository.Update(offcieModel.NewOffice);
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
 