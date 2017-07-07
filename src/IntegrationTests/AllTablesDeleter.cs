using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Dimensional.TinyReturns.IntegrationTests;

namespace OfficeLocationMicroservice.IntegrationTests
{
    class AllTablesDeleter
    {
        public class TableInfoDto
        {

            public TableInfoDto()
            {

            }
            //going to be [OfficeLocation].[Office] unless add more tables
            public TableInfoDto(string schemaName, string tableName)
            {
                SchemaName = schemaName;
                TableName = tableName;
            }

            public string TableName { get; set; }
            public string SchemaName { get; }

            protected bool Equals(TableInfoDto other)
            {
                return string.Equals(SchemaName, other.SchemaName) && string.Equals(TableName, other.TableName);
            }
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TableInfoDto)obj);
            }
            public override int GetHashCode()
            {
                unchecked
                {
                    return ((SchemaName != null ? SchemaName.GetHashCode() : 0) * 397) ^ (TableName != null ? TableName.GetHashCode() : 0);
                }
            }
        }

        public void DeleteAllDataFromTables(
            string connectionString,
            TableInfoDto[] tablesToSkip)
        {
            //Idk what the sql string shoudl do here.

            const string sql = @"


";
            IEnumerable<TableInfoDto> tableInfoDtos = null;
            SqlDatabaseHelper.ConnectionExecuteWithLog(
                connectionString,
                connection =>
                {
                    tableInfoDtos = connection.Query<TableInfoDto>(sql);
                },
                sql);

            var stringBuilder = new StringBuilder();

            foreach (var tableInfoDto in tableInfoDtos)
            {
                if (tablesToSkip.Any(skip => skip.Equals(tableInfoDto)))
                    continue;

                stringBuilder.AppendFormat("DELETE FROM [{0}].[{1}]", tableInfoDto.SchemaName, tableInfoDto.TableName);
            }

            SqlDatabaseHelper.ConnectionExecuteWithLog(
                connectionString,
                connection =>
                {
                    connection.Execute(stringBuilder.ToString());
                },
                stringBuilder.ToString());
        }
    }
}
