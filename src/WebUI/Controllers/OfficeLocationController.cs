using System;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services.OfficeWithEnumeration;
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

            var officeWithEnumerationGenerator = new OfficeWithEnumerationGenerator(
                _officeLocationRepository,
                _countryRepository);

            officeModel.Offices = officeWithEnumerationGenerator.GetAll();

            officeModel.NewOffice = officeWithEnumerationGenerator.NewOffice(new OfficeLocation());

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
                    if (office?.Office != null && office.HasChanged!=null)
                    {
                        if (Boolean.Parse(office.HasChanged))
                        {
                            var officelocation = _officeLocationRepository.Update(office.Office);
                        }
                    }
                }
            }

            if (officeModel.NewOffice?.Office != null && officeModel.NewOffice.HasChanged != null)
            {
                if (Boolean.Parse(officeModel.NewOffice.HasChanged))
                {
                    var officelocation = _officeLocationRepository.Update(officeModel.NewOffice.Office);
                }             
            }

            return RedirectToAction("Index", new {notificationFlag = true});
         }
    }

    public static class WebHelper
    {
    }
}
 