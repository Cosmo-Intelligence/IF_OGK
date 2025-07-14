using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using RISCommonLibrary.Lib.Utils;

namespace RISODPLibrary.Lib.Utils
{
    public static class ODPConnectionUtils
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string PROVIDER_NAME_ORACLE_CLIENT = "Oracle.DataAccess.Client";

        public static OracleConnection Connect(String connectionString)
        {
            OracleConnection cn = new OracleConnection(connectionString);
            cn.Open();
            return cn;
        }

        public static OracleConnectionStringBuilder GetConnectionStringBuilder()
        {
            return ConnectionUtils.GetConnectionStringBuilder(PROVIDER_NAME_ORACLE_CLIENT) as OracleConnectionStringBuilder;
        }

        public static string GetConnectionStringWithoutPassword(String connectionString)
        {
            OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
            ocsb.ConnectionString = connectionString;
            ocsb.Remove("Password");
            return ocsb.ConnectionString;
        }


    }
}
