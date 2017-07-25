using System.Linq;
using System.Web.Http;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeApiController : ApiController
    {
        private readonly OfficeLocationFacade _officeLocationFacade;

        public OfficeApiController()
        {
            _officeLocationFacade = MasterFactory.GetOfficeLocationFacade();
        }

        public OfficeApiController(OfficeLocationFacade officeLocationFacade)
        {
            _officeLocationFacade = officeLocationFacade;
        }

    [Route("api/Offices")]
        public OfficeLocation[] GetOffices()
         {
             return _officeLocationFacade.GetAll();
         }

        [Route("api/GetOffice")]
        public OfficeLocation[] GetOffice(string operating = null, int? id = null)
        {
            var officeLocations = _officeLocationFacade.GetAll();
            if (id != null)
            {
                officeLocations = officeLocations.Where(x => x.OfficeId == id).ToArray();
            }

            if (operating != null)
            {
                officeLocations = officeLocations.Where(x => x.Operating == operating).ToArray();
            }

            return officeLocations;
        }

        [Route("api/GetOfficeName")]
        public string[] GetOfficeName(string operating = null, int? id = null)
        {
            var officeLocations = GetOffice(operating, id);
            return officeLocations.Select(x => x.Name).ToArray();
        }
    }
}
