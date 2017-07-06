using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OfficeLocationMicroservice.WebUi.Controllers
{
    public class OfficeLocationAPIController : ApiController
    {
        /*
    *  public OfficeLocationAPIController(IRegionSchemeRepository regionSchemeRepository,
           IRegionSchemeByCodeTypeRepository regionSchemeByCodeTypeRepository )
       {
           //fill interfaces model is dependent on
           //plus repository
           _regionSchemeRepository = regionSchemeRepository;
           _regionSchemeByCodeTypeRepository = regionSchemeByCodeTypeRepository;
           _schemegateway = new SchemeGateway(new APISettings());
       }

         /*
     * private TreeModel GetModel()
        {
            SchemeDto[] DTOs = _schemegateway.GetAll();
            TreeModel model = new TreeModel();

            string[] slugs = new string[DTOs.Length];
            int t = 0;
            foreach (SchemeDto dto in DTOs)
            {
                slugs[t++] = dto.Slug;
            }
            //this sets RegionSchemes in model so can access
            model.RegionSchemes = _regionSchemeRepository.GetAll(slugs);
            return model;
        }

        public TreeModel GetData()
        {
            return GetModel();
        }

    */
    }


}
