using System.Web.Http;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeController : ApiController
    {
        private readonly OfficeLocationRepository _officeLocationRepository;

        public OfficeController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
        }

        public OfficeController(OfficeLocationRepository officeLocationRepository)
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
