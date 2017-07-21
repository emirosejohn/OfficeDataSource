using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using OfficeLocationMicroservice.Core.Services.SharedContext;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Data.OfficeLocationDatabase
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
               @"SELECT 
                 [OfficeId]
                ,[Name]
                ,[Address]
                ,[Country]
                ,[Switchboard]
                ,[Fax]
                ,[Operating]
            FROM
                [OfficeLocation].[Office]
            WHERE Name = @name";
            var data = GetFromDatabase(con => con.Query<OfficeDto>(query, new { name }).ToArray());
            return data.SingleOrDefault();

        }

        public OfficeDto GetById(int id)
        {
            const string query =
                @"SELECT [OfficeId]
                ,[Name]
                ,[Address]
                ,[Country]
                ,[Switchboard]
                ,[Fax]
                ,[Operating]
            FROM
                [OfficeLocation].[Office]
            WHERE OfficeId = @id";
        var data = GetFromDatabase(con => con.Query<OfficeDto>(query, new { id }).ToArray());
            return data.SingleOrDefault();

        }

        public OfficeDto[] GetAll()
        {
            const string query = @"
        SELECT 
             [OfficeId]
            ,[Name]
            ,[Address]
            ,[Country]
            ,[Switchboard]
            ,[Fax]
            ,[Operating]
        FROM
            [OfficeLocation].[Office]
        ORDER BY
            [Operating] DESC,
			[Name]";

            OfficeDto[] result = null;
            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<OfficeDto>(query).ToArray();
                },
            query);

            return result;
        }

        public int Insert(OfficeDto dto)
        {
            const string sql = @"
        Insert Into [OfficeLocation].[Office]
            (
             [Name]
            ,[Address]
            ,[Country]
            ,[Switchboard]
            ,[Fax]
            ,[Operating])
        Values
            (
             @Name
            ,@Address
            ,@Country
            ,@Switchboard
            ,@Fax
            ,@Operating)

SELECT CAST(SCOPE_IDENTITY() as int)
";
            int newId = 0;

            ConnectionExecuteWithLog(
                connection =>
                {
                    newId = connection.Query<int>(sql, dto).Single();
                },
                sql,
                dto);

            return newId;
        }

        public void Update(OfficeDto dto)
        {
            const string sql = @"
        Update [OfficeLocation].[Office]
        Set 
             [Name] = @Name
            ,[Address] = @Address
            ,[Country] = @Country
            ,[Switchboard] = @Switchboard
            ,[Fax] = @Fax
            ,[Operating] = @Operating
        where
            [OfficeId] = @OfficeId
";

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
