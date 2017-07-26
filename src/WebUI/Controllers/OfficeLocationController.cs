using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade;
using OfficeLocationMicroservice.WebUi.Helpers;
using OfficeLocationMicroservice.WebUi.Models;
using WebGrease.Css.Extensions;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationController : Controller
    {
        private readonly IUserWrapper _userWrapper;
        private readonly OfficeLocationFacade _officeLocationFacade;

        public OfficeLocationController()
        {
            _officeLocationFacade = MasterFactory.GetOfficeLocationFacade();
            _userWrapper = new UserWrapper(MasterFactory.GroupNameConstants);
        }

        //for tests
        public OfficeLocationController(
            OfficeLocationFacade officeLocationFacade,
            IUserWrapper userWrapper)
        {
            _officeLocationFacade = officeLocationFacade;
            _userWrapper = userWrapper;
        }

        public ActionResult Index(bool? notificationFlag = null, bool regularView = false)
        {
            OfficeModel officeModel = new OfficeModel();

            officeModel.User = _userWrapper;

            officeModel.Offices = _officeLocationFacade.GetAll().Select(x => new WebOfficeLocation(x)).ToArray();

            officeModel.Countries = _officeLocationFacade.GetAllCountries();
            officeModel.NewOffice = new WebOfficeLocation();
            officeModel.OperatingOptions = WebHelper.GenerateOperatingOptions();

            officeModel.NotificationFlag = notificationFlag;
            officeModel.RegularView = regularView;

            return View(officeModel);
        }

        [HttpPost]
        public ActionResult Save(OfficeModel officeModel)
        {
            if (_userWrapper.IsInAdminGroup())
            {
                if (officeModel.Offices != null)
                {
                    foreach (var office in officeModel.Offices)
                    {
                        if (office != null && office.HasChanged != null && office.Country!=null)
                        {
                            var country = _officeLocationFacade.GetAllCountries()
                                .Single(x => x.Slug == office.Country.Slug);
                            office.Country = country;

                            if (Boolean.Parse(office.HasChanged))
                            {
                                var officelocation = _officeLocationFacade.Update(office);
                            }
                        }
                    }
                }

                if (officeModel.NewOffice != null && officeModel.NewOffice.HasChanged != null)
                {
                    if (Boolean.Parse(officeModel.NewOffice.HasChanged) && officeModel.NewOffice.Country != null)
                    {
                        var country = _officeLocationFacade.GetAllCountries()
                            .Single(x => x.Slug == officeModel.NewOffice.Country.Slug);
                        officeModel.NewOffice.Country = country;

                        var officelocation = _officeLocationFacade.Update(officeModel.NewOffice);
                    }
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

            operatingOptions.Add(new OperatingOption());

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
 