using System.Configuration;
using Email;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.IntegrationTests
{
    class DataConnectionStringsForIntegrationTests : IOfficeLocationDatabaseSettings, ICountryWebApiSettings, IEmailSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["OfficeLocationDatabase"].ConnectionString; }
        }

        public string CountryWebApiUrl
        {
            get { return ConfigurationManager.AppSettings["CountryWebApiUrl"];  }
        }

        public string EmailSubject { get; }
        public string EmailServerName { get; }
        public string EmailTo { get; }
        public string EmailFrom { get; }
    }
}
