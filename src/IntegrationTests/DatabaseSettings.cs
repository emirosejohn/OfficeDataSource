using System.Configuration;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.IntegrationTests
{
    class DatabaseSettings : IOfficeLocationDatabaseSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["OfficeLocationDatabase"].ConnectionString; }
        }
    }
}
