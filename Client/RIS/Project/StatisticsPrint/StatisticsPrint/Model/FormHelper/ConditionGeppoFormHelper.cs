using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Collections.Specialized;
using System.Collections;
using StatisticsPrint.Data;
using System.Data;
using System.Configuration;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;
using StatisticsPrint.Properties;

namespace StatisticsPrint.Model.FormHelper
{
	public class ConditionGeppoFormHelper
	{

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		/// <summary>
		/// 放射線治療（門数）取得
		/// </summary>
		/// <param name="selectedDate"></param>
		/// <returns></returns>
		internal string GetMonsu(DateTime selectedDate)
		{
			IDbConnection cn = ODPConnectionUtils.Connect(Settings.Default.ConnectionString);
			try
			{
				return GetMonsu(cn, selectedDate);
			}
			finally
			{
				cn.Close();
			}
		}

		/// <summary>
		/// 放射線治療（門数）取得
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="selectedDate"></param>
		/// <returns></returns>
		internal string GetMonsu(IDbConnection cn, DateTime selectedDate)
		{
			const string FIELD_EXAM_COUNT = "exam_count";
			const string PARAM_TARGET_MONTH = "target_month";
			const string CLASS_KEY_MONSU = "900";

			const string SQL_SELECT =
			"SELECT " +
				"s.{0} " +
			"FROM " +
				"statistics_monthly_data s " +
			"WHERE " +
				"s.target_month = :{1} AND " +
				"s.class_key = '{2}' ";

			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = string.Format(SQL_SELECT, FIELD_EXAM_COUNT, 
					PARAM_TARGET_MONTH, CLASS_KEY_MONSU);

				#region パラメータ設定
				IDataParameter p = command.CreateParameter();

				p.SetInputString(PARAM_TARGET_MONTH, selectedDate.BeginOfMonth().ToString("yyyyMM"));
				command.Parameters.Add(p);
				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						return "";
					}
					return reader.GetStringByDB(FIELD_EXAM_COUNT);
				}
				finally
				{
					reader.Close();
				}
			}
		}

	}
}
