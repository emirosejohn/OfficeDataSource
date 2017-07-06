using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Database;
using Dapper;

namespace OfficeLocationMicroservice.Database
{
    class OfficeDataTableGateway: BaseDataTableGateway, IOfficeDataTableGateway
    {
        public OfficeDto[] GetAll()
        {
            const string query = @"
SELECT 
    [Name]
    ,[Address]
    ,[Country]
    ,[Switchboard]
    ,[Fax]
    ,[TimeZone]
    ,[Operating]
FROM
    [OfficeLocation].[Offices]";

            OfficeDto[] result = null;
            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<OfficeDto>(query).ToArray();
                },
            query);

            return result;
        }

        public OfficeDataTableGateway(IOfficeLocationDatabaseSettings officeLocationDatabaseSettings, ISystemLog systemLog) : base(officeLocationDatabaseSettings, systemLog)
        {
        }

        public OfficeDataTableGateway(ISystemLog systemLog) : base(systemLog)
        {
        }
    }
}
