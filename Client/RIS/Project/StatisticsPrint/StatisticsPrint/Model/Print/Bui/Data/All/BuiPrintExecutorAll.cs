using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.Bui.Data;
using StatisticsPrint.Model.Print.Bui.Data.ALL;

namespace StatisticsPrint.Model.Print.Bui.All
{
	/// <summary>
	/// 撮影検査数一覧（部位分類）印刷
	/// </summary>
	/// <remarks>All</remarks>
	internal class BuiPrintExecutorAll : BuiPrintExecutor
	{

		#region メソッド

		#region public
		/// <summary>
		/// 書き込みクラス生成
		/// </summary>
		/// <returns></returns>
		public override BuiWriter CreateBuiWriter()
		{
			return new BuiWriterAll();
		}

		#endregion

		#region protected

		/// <summary>
		/// 合計行フィールド名
		/// </summary>
		/// <returns></returns>
		protected override TotalFieldName CreateTotalFieldName()
		{
			return new TotalFieldNameAll();
		}

		/// <summary>
		/// コマンドオブジェクトから結果を取得する
		/// </summary>
		/// <param name="conditionPlace"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		/// <remarks>All選択時</remarks>
		protected override BuiItem GetResultDataByCommand(ConditionPrintPlace conditionPlace, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetBuiDataAll";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", conditionPlace.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_BUI_ALL_CURSOR");
			command.Parameters.Add(p);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;
			using (OracleRefCursor cur1 = (OracleRefCursor)cmd.Parameters["P_BUI_ALL_CURSOR"].Value)
			{
				using (OracleDataReader dr1 = cur1.GetDataReader())
				{
					BuiItem data = new BuiItem();

					int count = 0;
					while (dr1.Read())
					{
						count++;
						AddResultData(dr1, data);
					}
					_log.DebugFormat("{0}件取得しました", count);
					return data;
				}
			}
		}

		protected override void AddResultData(OracleDataReader dr1, BuiItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr1, _log);

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");

			#region 実施場所ごと

			string placeID = dr1.GetStringByDB("PLACE_ID");
			List<IBuiItemChild> list = data.ChildItemList;

			IBuiItemChild placeItem = list.FirstOrDefault(k => k.TARGET_ID == placeID);
			if (placeItem == null)
			{
				placeItem = data.AddForAll(dr1.GetStringByDB("PLACE_ID"), dr1.GetStringByDB("PLACE_NAME"),
					dr1.GetStringByDB("PLACE_SHOWORDER"));
			}
			#endregion

			AddKensaTypeList(dr1, placeItem.KensaTypeItemList);
		}

		#endregion

		#region private

		#endregion

		#endregion メソッド

	}
}
