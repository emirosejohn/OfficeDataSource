using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IDatabaseSettings _settings;

        public OfficeDataTableGateway(IOfficeLocationDatabaseSettings officeLocationDatabaseSettings, ISystemLog systemLog, IDatabaseSettings settings) : base(officeLocationDatabaseSettings, systemLog)
        {
            _settings = settings;
        }

        public OfficeDataTableGateway(ISystemLog systemLog, IDatabaseSettings settings) : base(systemLog)
        {
            _settings = settings;
        }

        public OfficeDto GetByName(string name)
        {
            const string query = @"SELECT [Name]
    ,[Address]
    ,[Country]
    ,[Switchboard]
    ,[Fax]
    ,[TimeZone]
    ,[Operating]
FROM
    [OfficeLocation].[Offices]
WHERE Name = @name";
            var data = GetFromDatabase(con => con.Query<OfficeDto>(query, new { name }).ToArray());
            return data.SingleOrDefault();

        }

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

       

        protected T[] GetFromDatabase<T>(Func<IDbConnection, T[]> connectionFunc)
        {
            T[] data;
            using (IDbConnection con = new SqlConnection(_settings.ConnectionString))
            {
                con.Open();
                data = connectionFunc(con);
                con.Close();
            }
            return data;
        }
    }
}
