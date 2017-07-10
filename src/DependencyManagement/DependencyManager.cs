using Logging;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;
using OfficeLocationMicroservice.Core.SharedContext;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Database;
using OfficeLocationMicroservice.Database.CountryWebApi;
using OfficeLocationMicroservice.Database.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.DependencyManagement
{
    public static class DependencyManager
    {

        public static void BootstrapForTests(
            ISystemLog systemLog,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings, 
            ICountryWebApiSettings countryWebApiSettings)
        {
            BootstrapAll(systemLog, officeLocationDatabaseSettings, countryWebApiSettings);
        }

        public static void BootstrapForSystem(
            string logName,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings,
            ICountryWebApiSettings countryWebApiSettings)
        {
            LoggingBootstrapper.StartupLog(logName);

            var logForNetSystemLog = new LogForNetSystemLog();

            BootstrapAll(logForNetSystemLog, officeLocationDatabaseSettings, countryWebApiSettings);
        }

        private static void BootstrapAll(
            ISystemLog logForNetSystemLog,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings, 
            ICountryWebApiSettings countryWebApiSettings)
        {
            var webServiceCaller = new WebApiServiceCaller(logForNetSystemLog);

            MasterFactory.OfficeDataTableGateway =
                new OfficeDataTableGateway(officeLocationDatabaseSettings, logForNetSystemLog );
            MasterFactory.CountryWebApiGateway=
                new CountryWebApiGateway(countryWebApiSettings, webServiceCaller);
        }
    }
}
