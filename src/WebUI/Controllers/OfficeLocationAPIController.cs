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
        private readonly IOfficeDataTableGateway _officeDataTableGateway;
        private readonly IOfficeLocationRepository _officeLocationRepository;

        public OfficeLocationApiController(IOfficeDataTableGateway officeDataTableGateway, IOfficeLocationRepository officeLocationRepository)
        {
            //fill interfaces model is dependent on
            //plus repository
            _officeDataTableGateway = officeDataTableGateway;
            _officeLocationRepository = officeLocationRepository;
            //_schemegateway = new SchemeGateway(new APISettings());
        }
        
        private OfficeLocationModel GetModel()
        {
            OfficeDto[] DTOs = _officeDataTableGateway.GetAll();
            OfficeLocationModel locationModel = new OfficeLocationModel();

            //need to set Offices in locationModel so can access
            locationModel.Offices = _officeLocationRepository.GetAll();
            return locationModel;
        }

        public OfficeLocationModel GetData()
        {
            return GetModel();
        }
        
    }


}
