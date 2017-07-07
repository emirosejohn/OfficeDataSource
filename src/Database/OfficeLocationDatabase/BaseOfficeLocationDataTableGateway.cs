using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.SharedContext;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Database.OfficeLocationDatabase
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
