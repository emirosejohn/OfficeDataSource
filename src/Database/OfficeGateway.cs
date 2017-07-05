using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Database;

namespace OfficeLocationMicroservice.Database
{
    class OfficeGateway: BaseDataTableGateway, IOfficeGateway
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
            BaseDataTableGateway.ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<OfficeDto>(query).ToArray();
                },
            query);

            return result;
        }
        
    }
}
