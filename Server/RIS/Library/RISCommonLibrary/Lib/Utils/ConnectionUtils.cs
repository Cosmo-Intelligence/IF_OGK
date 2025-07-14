using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Threading;

namespace RISCommonLibrary.Lib.Utils
{
    public static class ConnectionUtils
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static IDbConnection Connect(String connectionString, String providerName)
        {
			return ConnectInner(connectionString, providerName);
		}

		public static IDbConnection ConnectNoRetry(String connectionString, String providerName)
        {
			return ConnectInner(connectionString, providerName);
        }

		public static DbConnectionStringBuilder GetConnectionStringBuilder(String providerName)
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
			return factory.CreateConnectionStringBuilder();
		}

		public static DbProviderFactory GetProviderFactory(String providerName)
		{
			return DbProviderFactories.GetFactory(providerName);
		}

		private static IDbConnection ConnectInner(String connectionString, String providerName)
        {
			DbProviderFactory factory = ConnectionUtils.GetProviderFactory(providerName);

            IDbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;

            connection.Open();
            return connection;
        }


	}
}
