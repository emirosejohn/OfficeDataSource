using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.SharedContext;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Database.OfficeLocationDatabase
{
    public class OfficeDataTableGateway: BaseOfficeLocationDataTableGateway, IOfficeDataTableGateway
    {
        private readonly IOfficeLocationDatabaseSettings _settings;

        public OfficeDataTableGateway(
            IOfficeLocationDatabaseSettings settings, ISystemLog systemLog)
            : base(settings, systemLog)
        {
            _settings = settings;
        }


        public OfficeDto GetByName(string name)
        {
            const string query =
               @"SELECT [Name]
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

        public void Insert(OfficeDto dto)
        {
            const string sql = @"
        Insert Into [OfficeLocation].[Office]
            ([Name]
            ,[Address]
            ,[Country]
            ,[Switchboard]
            ,[Fax]
            ,[TimeZone]
            ,[Operating])
        Values
            (@Name
            ,@Address
            ,@Country
            ,@Switchboard
            ,@Fax
            ,@TimeZone
            ,@Operating)";

            ConnectionExecuteWithLog(
                connection =>
                {
                    connection.Execute(sql, dto);
                },
                sql,
                dto);
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
