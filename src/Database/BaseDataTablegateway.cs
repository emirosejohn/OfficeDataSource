using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimensional.TinyReturns.Core.SharedContext.Services;

namespace OfficeLocationMicroservice.Database
{
    public abstract class BaseDataTableGateway
    {
        protected ISystemLog SystemLog;
        protected BaseDataTableGateway(
            ISystemLog systemLog)
        {
            SystemLog = systemLog;
        }
        protected abstract string DefaultConnectionString { get; }

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
    }
}
