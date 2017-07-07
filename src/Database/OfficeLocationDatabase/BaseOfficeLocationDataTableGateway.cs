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
        private readonly IDatabaseSettings _databaseSettings;

        protected BaseOfficeLocationDataTableGateway(
            IDatabaseSettings databaseSettings,
            ISystemLog systemLog) : base(systemLog)
        {
            _databaseSettings = databaseSettings;
        }

        protected override string DefaultConnectionString
        {
            get { return _databaseSettings.ConnectionString; }
        }

    }
}
