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
    public class OfficeLocationApiController : ApiController
    {
        private readonly IOfficeLocationRepository _officeLocationRepository;

        public OfficeLocationApiController()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
        }
        
        public OfficeLocationModel Offices()
        {
            OfficeLocationModel locationModel = new OfficeLocationModel();

            locationModel.Offices = _officeLocationRepository.GetAll();

            return locationModel;
        }
        
    }


}
