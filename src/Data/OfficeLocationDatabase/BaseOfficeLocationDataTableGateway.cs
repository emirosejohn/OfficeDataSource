using OfficeLocationMicroservice.Core.Services.SharedContext;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Data.OfficeLocationDatabase
{
    public class BaseOfficeLocationDataTableGateway : BaseDataTableGateway
    {
        private readonly IOfficeLocationDatabaseSettings _officeLocationDatabaseSettings;

        protected BaseOfficeLocationDataTableGateway(
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings,
            ISystemLog systemLog) : base(systemLog)
        {
            _officeLocationDatabaseSettings = officeLocationDatabaseSettings;
        }

        protected override string DefaultConnectionString
        {
            get { return _officeLocationDatabaseSettings.ConnectionString; }
        }

    }
}
