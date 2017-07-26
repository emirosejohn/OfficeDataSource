using System.Configuration;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade.Email;
using OfficeLocationMicroservice.Core.SharedContext.Services.CountryWebApi;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.IntegrationTests
{
    class DataConnectionStringsForIntegrationTests : 
        IOfficeLocationDatabaseSettings, 
        ICountryWebApiSettings,
        IEmailSettings,
        IGroupNameConstants
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["OfficeLocationDatabase"].ConnectionString; }
        }

        public string CountryWebApiUrl
        {
            get { return ConfigurationManager.AppSettings["CountryWebApiUrl"];  }
        }

        public string EmailSubject
        {
            get { return ConfigurationManager.AppSettings["EmailSubject"]; }
        }
        public string EmailServerName
        {
            get { return ConfigurationManager.AppSettings["EmailServerName"]; }
        }
        public string EmailTo
        {
            get { return ConfigurationManager.AppSettings["EmailTo"]; }
        }
        public string EmailFrom
        {
            get { return ConfigurationManager.AppSettings["EmailFrom"]; }
        }

        public string AdminGroup
        {
            get { return ConfigurationManager.AppSettings["AdminGroup"]; }
        }
    }
}
