using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Collections.Specialized;
using System.Collections;
using StatisticsPrint.Data;
using System.Data;
using RISODPLibrary.Lib.Utils;
using System.Configuration;
using RISCommonLibrary.Lib.Utils;

namespace StatisticsPrint.Model.FormHelper
{
	public class ConditionPlaceFormHelper
	{
		private const string FIELD_PLACE_ID = "place_id";
		private const string FIELD_PLACE_NAME = "place_name";

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// 実施場所取得
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="isAddAll"></param>
		/// <returns></returns>
		internal List<PlaceData> GetPlaceNameRoomList(IDbConnection cn, bool isAddAll)
		{
			const string SQL_SELECT =
			"SELECT " +
				"p.place_id, " +
				"p.place_name " +
			"FROM " +
				"place_master p " +
			"ORDER BY " +
				"p.showorder, " +
				"p.place_id ";

			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				#region パラメータ設定
				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				List<PlaceData> list = new List<PlaceData>();
				IDataReader reader = command.ExecuteReader();
				try
				{
					while (reader.Read())
					{
						//MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

						PlaceData target;
						if ((list.Count < 1) && (isAddAll))
						{
							target = new PlaceData();
							target.PLACE_ID = PlaceData.PLACE_ID_ALL;
							target.PLACE_NAME = PlaceData.PLACE_NAME_ALL;
							list.Add(target);
						}
						target = new PlaceData();
						target.PLACE_ID = reader.GetStringByDB(FIELD_PLACE_ID);
						target.PLACE_NAME = reader.GetStringByDB(FIELD_PLACE_NAME);
						list.Add(target);
					}

					return list;
				}
				finally
				{
					reader.Close();
				}
			}
		}

	}
}
