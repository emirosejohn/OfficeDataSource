using System.Configuration;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.IntegrationTests
{
    class DataConnectionStringsForIntegrationTests : IOfficeLocationDatabaseSettings, ICountryWebApiSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["OfficeLocationDatabase"].ConnectionString; }
        }

        public string CountryWebApiUrl
        {
            get { return ConfigurationManager.AppSettings["CountryWebApiUrl"];  }
        }
    }
}
