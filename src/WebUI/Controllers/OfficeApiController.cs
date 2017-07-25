using System.Linq;
using System.Web.Http;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeApiController : ApiController
    {
        private readonly OfficeLocationRepository _officeLocationRepository;

        public OfficeApiController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
        }

        public OfficeApiController(OfficeLocationRepository officeLocationRepository)
        {
            _officeLocationRepository = officeLocationRepository;
        }

        [Route("api/Offices")]
        public OfficeLocation[] GetOffices()
         {
             return _officeLocationRepository.GetAll();
         }

        [Route("api/GetOffice")]
        public OfficeLocation[] GetOffice(string operating = null, int? id = null)
        {
            var officeLocations = _officeLocationRepository.GetAll();
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
