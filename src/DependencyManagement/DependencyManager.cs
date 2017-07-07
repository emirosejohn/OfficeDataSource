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
            IDatabaseSettings databaseSettings)
        {
            BootstrapAll(systemLog, databaseSettings);
        }

        public static void BootstrapForSystem(
            string logName,
            IDatabaseSettings databaseSettings)
        {
            LoggingBootstrapper.StartupLog(logName);

            var logForNetSystemLog = new LogForNetSystemLog();

            BootstrapAll(logForNetSystemLog, databaseSettings);
        }

        private static void BootstrapAll(
            ISystemLog logForNetSystemLog,
            IDatabaseSettings databaseSettings)
        {
            MasterFactory.OfficeDataTableGateway =
                new OfficeDataTableGateway(databaseSettings, logForNetSystemLog );
        }
    }
}
