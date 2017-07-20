using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services;
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

        public ActionResult Index(bool? notificationFlag = null)
        {
            OfficeModel officeModel = new OfficeModel();

            officeModel.Offices = _officeLocationRepository.GetAll();

            officeModel.NewOffice = new OfficeLocation();

            officeModel.Countries = _countryRepository.GetAllCountries();

            officeModel.OperatingOptions = WebHelper.GenerateOperatingOptions();

            officeModel.NotificationFlag = notificationFlag;

            return View(officeModel);
        }
        
        [HttpPost]
        public ActionResult Save(OfficeModel officeModel)
        {


            if (officeModel.Offices != null)
            {
                foreach (var office in officeModel.Offices)
                {
                    if (office != null && office.HasChanged!=null)
                    {
                        if (Boolean.Parse(office.HasChanged))
                        {
                            var officelocation = _officeLocationRepository.Update(office);
                        }
                    }
                }
            }

            if (officeModel.NewOffice != null && officeModel.NewOffice.HasChanged != null)
            {
                if (Boolean.Parse(officeModel.NewOffice.HasChanged))
                {
                    var officelocation = _officeLocationRepository.Update(officeModel.NewOffice);
                }             
            }

            return RedirectToAction("Index", new {notificationFlag = true});
         }
    }

    public static class WebHelper
    {
        public static OperatingOption[] GenerateOperatingOptions()
        {
            var operatingOptions = new List<OperatingOption>();
            operatingOptions.Add(new OperatingOption()
            {
                Id = "Active",
                Name = "Active",
            });
            operatingOptions.Add(new OperatingOption()
            {
                Id = "Closed",
                Name = "Closed",
            });

            return operatingOptions.ToArray();
        }
    }
}
 