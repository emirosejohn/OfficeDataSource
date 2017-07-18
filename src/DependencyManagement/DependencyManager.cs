using Email;
using Logging;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;
using OfficeLocationMicroservice.Core.Services.Email;
using OfficeLocationMicroservice.Core.Services.SharedContext;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Data;
using OfficeLocationMicroservice.Data.CountryWebApi;
using OfficeLocationMicroservice.Data.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.DependencyManagement
{
    public static class DependencyManager
    {

        public static void BootstrapForTests(
            ISystemLog systemLog,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings, 
            ICountryWebApiSettings countryWebApiSettings, 
            IEmailSettings emailSettings)
        {
            BootstrapAll(systemLog,
                officeLocationDatabaseSettings,
                countryWebApiSettings,
                emailSettings);
        }

        public static void BootstrapForSystem(
            string logName,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings,
            ICountryWebApiSettings countryWebApiSettings,
            IEmailSettings emailSettings)
        {
            LoggingBootstrapper.StartupLog(logName);

            var logForNetSystemLog = new LogForNetSystemLog();

            BootstrapAll(logForNetSystemLog,
                officeLocationDatabaseSettings,
                countryWebApiSettings, 
                emailSettings);
        }

        private static void BootstrapAll(
            ISystemLog logForNetSystemLog,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings, 
            ICountryWebApiSettings countryWebApiSettings,
            IEmailSettings emailSettings)
        {
            var webServiceCaller = new WebApiServiceCaller(logForNetSystemLog);

            MasterFactory.OfficeDataTableGateway =
                new OfficeDataTableGateway(officeLocationDatabaseSettings, logForNetSystemLog );
            MasterFactory.CountryWebApiGateway=
                new CountryWebApiGateway(countryWebApiSettings, webServiceCaller);
            MasterFactory.EmailClient = new EmailClient(emailSettings);

        }
    }
}
