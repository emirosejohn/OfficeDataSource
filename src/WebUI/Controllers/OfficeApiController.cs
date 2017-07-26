using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade;
using OfficeLocationMicroservice.WebUi.Models;
using Swashbuckle.Swagger.Annotations;

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
        /// <summary>
        ///  Get all offices
        /// </summary>
        /// <remarks>
        /// Get a list of offices
        /// </remarks>
        /// <returns></returns>
        [Route("api/Offices")]
        [SwaggerResponse(200, "DefaultResponse", typeof(OfficeLocation[]))]
        public HttpResponseMessage GetOffices()
         {
             return Request.CreateResponse(_officeLocationFacade.GetAll());
         }

        /// <summary>
        ///  Get the specified list of offices
        /// </summary>
        /// <remarks>
        /// Get a list of offices matching the given parameters.
        /// </remarks>
        /// <returns></returns>
        [Route("api/GetOffice")]
        [SwaggerResponse(200, "DefaultResponse", typeof(OfficeLocation[]))]
        public HttpResponseMessage GetOffice(string operating = null, int? id = null, string countrySlug = null)
        {
            return Request.CreateResponse(GetOfficeLocations(operating,id, countrySlug));
        }

        /// <summary>
        ///  Get the specified list of office names
        /// </summary>
        /// <remarks>
        /// Get the names of offices matching the given parameters.
        /// </remarks>
        /// <returns></returns>
        [Route("api/GetOfficeName")]
        [SwaggerResponse(200, "DefaultResponse", typeof(string[]))]
        public HttpResponseMessage GetOfficeName(string operating = null, int? id = null, string countrySlug = null)
        {
            var officeLocations = GetOfficeLocations(operating, id, countrySlug).Select(x => x.Name).ToArray(); ;
            return Request.CreateResponse(officeLocations);
        }


        private OfficeLocation[] GetOfficeLocations(string operating, int? id, string countrySlug)
        {
            var officeLocations = _officeLocationFacade.GetAll();
            if (id != null)
            {
                officeLocations = officeLocations.Where(x => x.OfficeId == id).ToArray();
            }

            if (operating != null)
            {
                officeLocations = officeLocations.Where(x => x.Operating.ToUpper() == operating.ToUpper()).ToArray();
            }

            if (countrySlug != null)
            {
                officeLocations = officeLocations.Where(x => x.Country.Slug.ToUpper() == countrySlug.ToUpper()).ToArray();
            }

            return officeLocations;
        }
    }
}
