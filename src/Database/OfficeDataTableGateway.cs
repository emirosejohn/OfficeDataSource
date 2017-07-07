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
    public class OfficeDataTableGateway: BaseDataTableGateway, IOfficeDataTableGateway
    {
        private readonly IDatabaseSettings _settings;

        public OfficeDataTableGateway( IDatabaseSettings settings, ISystemLog systemLog) : base(settings, systemLog)
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
    [OfficeLocation].[Office]
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
    [OfficeLocation].[Office]";

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
