using Email;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Core.SharedContext;

namespace OfficeLocationMicroservice.Core
{
    public static class MasterFactory
    {
        public static ISystemLog SystemLog { get; set; }
        public static IOfficeDataTableGateway OfficeDataTableGateway { get; set; }
        public static ICountryWebApiGateway CountryWebApiGateway { get; set; }
        public static EmailClient EmailClient { get; set; }

        public static OfficeLocationRepository GetOfficeLocationRepository()
        {
            return new OfficeLocationRepository(OfficeDataTableGateway);
        }

        public static CountryRepository GetCountryRepository()
        {
            return new CountryRepository(CountryWebApiGateway);
        }
    }
}