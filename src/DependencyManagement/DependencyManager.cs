using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.SharedContext;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Database.OfficeLocationDatabase;

namespace DependencyManagement
{
    public static class DependencyManager
    {

        public static void BootstrapForTests(
            ISystemLog systemLog,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings)
        {
            BootstrapAll(systemLog, officeLocationDatabaseSettings);
        }

        public static void BootstrapForSystem(
            string logName,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings)
        {
            LoggingBootstrapper.StartupLog(logName);

            var logForNetSystemLog = new LogForNetSystemLog();

            BootstrapAll(logForNetSystemLog, officeLocationDatabaseSettings);
        }

        private static void BootstrapAll(
            ISystemLog logForNetSystemLog,
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings)
        {
            MasterFactory.OfficeDataTableGateway =
                new OfficeDataTableGateway(officeLocationDatabaseSettings, logForNetSystemLog );
        }
    }
}
