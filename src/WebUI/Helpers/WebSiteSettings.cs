using System.Configuration;
using Email;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;
using OfficeLocationMicroservice.Core.Services.Email;
using OfficeLocationMicroservice.Core.Services.SharedContext;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.WebUi.Helpers
{
    public class WebSiteSettings
        : IOfficeLocationDatabaseSettings,
            ICountryWebApiSettings,
                IEmailSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["OfficeLocationDatabase"].ConnectionString; }
        }

        public string CountryWebApiUrl
        {
            get { return ConfigurationManager.AppSettings["CountryWebApiUrl"]; }
        }

        public string EmailSubject
        {
            get {return ConfigurationManager.AppSettings["EmailSubject"]; }
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
    }
}