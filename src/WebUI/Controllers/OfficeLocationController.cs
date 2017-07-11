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
        private OfficeModel _officeModel;


        public OfficeLocationController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
            _officeModel = new OfficeModel();
        }

        //for tests
        public OfficeLocationController(IOfficeLocationRepository officeLocationRepository)
        {
            _officeLocationRepository = officeLocationRepository;
            _officeModel = new OfficeModel();
        }

        public ActionResult Index()
        {
            OfficeModel model = new OfficeModel();

            model.ShowOfficeEdit = false;
            model.Offices = _officeLocationRepository.GetAll();
            _officeModel = model;
            
            return View(model);
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
            OfficeModel model = _officeModel;
            model.Offices = _officeLocationRepository.GetAll();
            model.OfficeEdit = officeEditModel;
            model.ShowOfficeEdit = true;
            return View(model);
        }
    }
}
 