using System;
using StatisticsPrint.Model.Print.Geppo.Data;
using Oracle.DataAccess.Client;
namespace StatisticsPrint.Model.Print.Geppo
{
	interface IGeppoPrintDataSetter
	{
		void AddResultData(OracleDataReader dr, GeppoItem data);
	}
}
