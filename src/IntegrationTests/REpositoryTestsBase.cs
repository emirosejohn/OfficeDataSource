using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.SharedContext;
using OfficeLocationMicroservice.DependencyManagement;

namespace OfficeLocationMicroservice.IntegrationTests
{
    public class RepositoryTestsBase
    {
        private DataConnectionStringsForIntegrationTests _dataConnectionStrings;

        public ISystemLog SystemLog;

        public bool AlreadyInit { get; set; }

        public RepositoryTestsBase()
        {
            _dataConnectionStrings = new DataConnectionStringsForIntegrationTests();

            if (!AlreadyInit)
            {
                SystemLog = new SystemLogForIntegrationTests();

                DependencyManager.BootstrapForTests(SystemLog, _dataConnectionStrings, _dataConnectionStrings, _dataConnectionStrings);

                AlreadyInit = true;
            }
        }
    }
}
