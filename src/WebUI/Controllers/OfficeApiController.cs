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

        [Route("api/OfficeById")]
        public OfficeLocation GetOffice(int id)
        {
            return _officeLocationRepository.GetById(id);
        }
    }


}
