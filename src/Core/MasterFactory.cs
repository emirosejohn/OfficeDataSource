using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade.Email;
using OfficeLocationMicroservice.Core.SharedContext.Services;
using OfficeLocationMicroservice.Core.SharedContext.Services.CountryWebApi;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core
{
    public static class MasterFactory
    {
        public static ISystemLog SystemLog { get; set; }
        public static IOfficeDataTableGateway OfficeDataTableGateway { get; set; }
        public static ICountryWebApiGateway CountryWebApiGateway { get; set; }
        public static IEmailClient EmailClient { get; set; }
        public static IGroupNameConstants GroupNameConstants { get; set; }


        public static OfficeLocationRepository GetOfficeLocationRepository()
        {
            var countryRepository = GetCountryRepository();
            return new OfficeLocationRepository(OfficeDataTableGateway, countryRepository);
        }

        public static OfficeLocationFacade GetOfficeLocationFacade()
        {
            var officeLocationRepository = GetOfficeLocationRepository();
            return new OfficeLocationFacade(officeLocationRepository, EmailClient);
        }

        public static CountryRepository GetCountryRepository()
        {
            return new CountryRepository(CountryWebApiGateway);
        }

    }
}

