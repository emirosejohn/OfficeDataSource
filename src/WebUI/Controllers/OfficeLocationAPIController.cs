using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationAPIController : ApiController
    {
        private readonly IOfficeDataTableGateway _officeDataTableGateway;
        private readonly IOfficeLocationRepository _officeLocationRepository;

        public OfficeLocationAPIController(IOfficeDataTableGateway officeDataTableGateway, IOfficeLocationRepository officeLocationRepository)
        {
            //fill interfaces model is dependent on
            //plus repository
            _officeDataTableGateway = officeDataTableGateway;
            _officeLocationRepository = officeLocationRepository;
            //_schemegateway = new SchemeGateway(new APISettings());
        }
        
        private OfficeModel GetModel()
        {
            OfficeDto[] DTOs = _officeDataTableGateway.GetAll();
            OfficeModel model = new OfficeModel();

            //need to set Offices in model so can access
            model.Offices = _officeLocationRepository.GetAll();
            return model;
        }

        public OfficeModel GetData()
        {
            return GetModel();
        }
        
    }


}
