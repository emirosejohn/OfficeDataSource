using OfficeLocationMicroservice.Core.SharedContext.Services;
using OfficeLocationMicroservice.DependencyManagement;

namespace OfficeLocationMicroservice.IntegrationTests
{
    public class RepositoryTestsBase
    {
        public ISystemLog SystemLog;

        public bool AlreadyInit { get; set; }

        public RepositoryTestsBase()
        {
            var dataConnectionStrings = new DataConnectionStringsForIntegrationTests();

            if (!AlreadyInit)
            {
                SystemLog = new SystemLogForIntegrationTests();

                DependencyManager.BootstrapForTests(SystemLog, dataConnectionStrings, dataConnectionStrings, dataConnectionStrings, 
                    dataConnectionStrings);

                AlreadyInit = true;
            }
        }
    }
}
