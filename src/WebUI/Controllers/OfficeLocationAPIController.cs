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
            _officeDataTableGateway = officeDataTableGateway;
            _officeLocationRepository = officeLocationRepository;
        }
        
        private OfficeModel GetModel()
        {
            OfficeModel officeModel = new OfficeModel();

            //need to set Offices in locationModel so can access
            officeModel.Offices = _officeLocationRepository.GetAll();
            return officeModel;
        }

        public OfficeModel GetData()
        {
            return GetModel();
        }
    }


}
