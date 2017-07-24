using Email;
using Logging;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Services;
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
            IEmailSettings emailSettings,
            IGroupNameConstants groupNameConstants)
        {
            BootstrapAll(systemLog,
                officeLocationDatabaseSettings,
                countryWebApiSettings,
                emailSettings, 
                groupNameConstants);
        }

        public static void BootstrapForSystem(
            string logName,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings,
            ICountryWebApiSettings countryWebApiSettings,
            IEmailSettings emailSettings, 
            IGroupNameConstants groupNameConstants)
        {
            LoggingBootstrapper.StartupLog(logName);

            var logForNetSystemLog = new LogForNetSystemLog();

            BootstrapAll(logForNetSystemLog,
                officeLocationDatabaseSettings,
                countryWebApiSettings, 
                emailSettings, 
                groupNameConstants);
        }

        private static void BootstrapAll(
            ISystemLog logForNetSystemLog,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings, 
            ICountryWebApiSettings countryWebApiSettings,
            IEmailSettings emailSettings,
            IGroupNameConstants groupNameConstants)
        {
            var webServiceCaller = new WebApiServiceCaller(logForNetSystemLog);

            MasterFactory.OfficeDataTableGateway =
                new OfficeDataTableGateway(officeLocationDatabaseSettings, logForNetSystemLog );
            MasterFactory.CountryWebApiGateway=
                new CountryWebApiGateway(countryWebApiSettings, webServiceCaller);
            MasterFactory.EmailClient = new EmailClient(emailSettings);
            MasterFactory.GroupNameConstants = groupNameConstants;
        }
    }
}
