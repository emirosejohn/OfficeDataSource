using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core;

namespace OfficeLocationMicroservice.Database
{
    public abstract class BaseDataTableGateway
    {
        protected ISystemLog SystemLog;
        
        private readonly IOfficeLocationDatabaseSettings _officeLocationDatabaseSettings;

        protected BaseDataTableGateway(
            IOfficeLocationDatabaseSettings officeLocationDatabaseSettings,
            ISystemLog systemLog) 
        {
            _officeLocationDatabaseSettings = officeLocationDatabaseSettings;
        }

        protected BaseDataTableGateway(
            ISystemLog systemLog)
        {
            SystemLog = systemLog;
        }

        //protected override string DefaultConnectionString
        protected string DefaultConnectionString
        {
            get { return _officeLocationDatabaseSettings.OfficeLocationDatabaseConnectionString; }
        }


        protected void ConnectionExecuteWithLog(
            Action<SqlConnection> connectionAction,
            string logSql)
        {
            using (var sqlConnection = new SqlConnection(DefaultConnectionString))
            {
                SystemLog.Info("Executing: " + logSql);

                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }


        protected void ConnectionExecuteWithLog(
            Action<SqlConnection> connectionAction,
            string logSql,
            object values)
        {
            var propertyInfos = values.GetType().GetProperties();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Executing:");
            stringBuilder.AppendLine(logSql);
            stringBuilder.AppendLine("Values:");

            foreach (var info in propertyInfos)
            {
                stringBuilder.Append("@" + info.Name + " = ");
                var value = info.GetValue(values, null);

                if (value == null)
                    stringBuilder.AppendLine("NULL,");
                else
                    stringBuilder.AppendLine(value.ToString());
            }

            SystemLog.Info(stringBuilder.ToString());

            using (var sqlConnection = new SqlConnection(DefaultConnectionString))
            {
                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }



    }



}