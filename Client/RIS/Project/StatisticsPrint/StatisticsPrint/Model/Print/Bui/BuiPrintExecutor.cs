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
using StatisticsPrint.Properties;


namespace StatisticsPrint.Model.Print.Bui
{
	/// <summary>
	/// 撮影検査数一覧（部位分類）印刷
	/// </summary>
	/// <remarks>個別</remarks>
	internal class BuiPrintExecutor
	{
		#region フィールド名
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private TotalFieldName _totalFieldName;
		#endregion

		#region コンストラクタ

		public BuiPrintExecutor()
		{
			_totalFieldName = CreateTotalFieldName();
		}

		#endregion

		#region メソッド

		#region public

		/// <summary>
		/// 取得結果
		/// </summary>
		/// <param name="condition"></param>
		/// <returns></returns>
		public BuiItem GetResultData(ConditionPrint condition)
		{
			ConditionPrintPlace conditionPlace = condition as ConditionPrintPlace;
			IDbConnection cn = ODPConnectionUtils.Connect(Settings.Default.ConnectionString);
			try
			{
				using (IDbCommand command = cn.CreateCommand())
				{
					return GetResultDataByCommand(conditionPlace, command);
				}
			}
			finally
			{
				cn.Close();
			}
		}

		/// <summary>
		/// 書き込みクラス生成
		/// </summary>
		/// <returns></returns>
		public virtual BuiWriter CreateBuiWriter()
		{
			return new BuiWriter();
		}

		#endregion

		#region protected

		/// <summary>
		/// 合計行フィールド名
		/// </summary>
		/// <returns></returns>
		protected virtual TotalFieldName CreateTotalFieldName()
		{
			return new TotalFieldName();
		}

		/// <summary>
		/// コマンドオブジェクトから結果を取得する
		/// </summary>
		/// <param name="conditionPlace"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		/// <remarks>Allと個別の違いは最上位のGROUOP BYとストアド引数</remarks>
		protected virtual BuiItem GetResultDataByCommand(ConditionPrintPlace conditionPlace, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetBuiData";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", conditionPlace.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			p.SetInputString("P_PLACE_ID", conditionPlace.PlaceID);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_BUI_CURSOR");
			command.Parameters.Add(p);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;
			using (OracleRefCursor cur = (OracleRefCursor)cmd.Parameters["P_BUI_CURSOR"].Value)
			{
				using (OracleDataReader dr = cur.GetDataReader())
				{
					BuiItem data = new BuiItem();

					int count = 0;
					while (dr.Read())
					{
						count++;
						AddResultData(dr, data);
					}
					_log.DebugFormat("{0}件取得しました", count);
					return data;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dr1"></param>
		/// <param name="data"></param>
		/// <remarks>Allと個別の違いは最上位のGROUOP BYとストアド引数</remarks>
		protected virtual void AddResultData(OracleDataReader dr1, BuiItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr1, _log);

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");

			#region 検査種別ごと

			string kensasituID = dr1.GetStringByDB("KENSASITU_ID");
			List<IBuiItemChild> list = data.ChildItemList;

			IBuiItemChild examroomItem = list.FirstOrDefault(k => k.TARGET_ID == kensasituID);
			if (examroomItem == null)
			{
				examroomItem = data.Add(dr1.GetStringByDB("KENSASITU_ID"), dr1.GetStringByDB("KENSASITU_NAME"),
					dr1.GetStringByDB("KENSASITU_SHOWORDER"));
			}
			#endregion

			AddKensaTypeList(dr1, examroomItem.KensaTypeItemList);
		}

		protected void AddKensaTypeList(OracleDataReader dr1, List<BuiItemKensaType> kensaTypeList)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr1, _log);

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");

			#region 検査種別ごと

			string kensaTypeID = dr1.GetStringByDB("KENSATYPE_ID");
			BuiItemKensaType kensaTypeItem = kensaTypeList.FirstOrDefault(k => k.KENSATYPE_ID == kensaTypeID);
			if (kensaTypeItem == null)
			{
				kensaTypeItem = new BuiItemKensaType()
				{
					KENSATYPE_ID = dr1.GetStringByDB("KENSATYPE_ID"),
					KENSATYPE_NAME = dr1.GetStringByDB("KENSATYPE_NAME"),
					KENSATYPE_SHOWORDER = dr1.GetStringByDB("KENSATYPE_SHOWORDER")
				};
				kensaTypeList.Add(kensaTypeItem);
			}

			if (_totalFieldName.TotalName == gyoSyosai)
			{
				kensaTypeItem.Total = dr1.GetStringByDB("CNT");
				return;
			}

			if (_totalFieldName.TotalNameDay == gyoSyosai)
			{
				kensaTypeItem.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
				return;
			}

			#endregion

			AddBuiList(dr1, kensaTypeItem.BuiItemList);
		}

		protected void AddBuiList(OracleDataReader dr1, List<BuiItemBui> buiList)
		{
			string buiID = dr1.GetStringByDB("BUI_ID");
			BuiItemBui buiItem = buiList.FirstOrDefault(b => b.BUI_ID == buiID);
			if (buiItem == null)
			{
				buiItem = new BuiItemBui();
				buiItem.BUI_ID = dr1.GetStringByDB("BUI_ID");
				buiItem.BUI_NAME = dr1.GetStringByDB("BUI_NAME");
				buiItem.BUI_SHOWORDER = dr1.GetStringByDB("BUI_SHOWORDER");
				buiList.Add(buiItem);
			}

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");
			if (_totalFieldName.TotalNameBuiBunrui == gyoSyosai)
			{
				buiItem.Total = dr1.GetStringByDB("CNT");
				return;
			}

			buiItem.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
		}
		#endregion

		#region private

		#endregion

		#endregion メソッド

	}
}
