using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Models;

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
