using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;
using StatisticsPrint.Data;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Properties;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Geppo.Data;
using StatisticsPrint.Model.Print.Common.Data;
using System;

namespace StatisticsPrint.Model.Print.Geppo
{
	/// <summary>
	/// 医療技術部診療検査科　月報印刷
	/// </summary>
	internal class GeppoPrint : PrintBase
	{
		#region プロパティ
		/// <summary>
		/// ドキュメント名
		/// </summary>
		public override string DocumentName
		{
			get
			{
				return "医療技術部診療検査科　月報";
			}
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public override string FormFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["FormFileGeppo"].StringToString();
			}
		}
		#endregion

		#region メソッド

		#region public
		
		/// <summary>
		/// 印刷する
		/// </summary>
		/// <param name="condition"></param>
		public override void Print(ConditionPrint condition)
		{
			if (condition == null)
			{
				return;
			}

			GeppoItem data = GetResultData(condition);
			if (data.Count < 1)
			{
				_log.Debug("出力データがありません");
				MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			using (COReportsHelper helper = new COReportsHelper())
			{
				IPrintWriter writer = new GeppoWriter()
				{
					Condition = condition,
					Data = data
				};

				PrintExecute(condition, helper, writer);
			}
		}

		#endregion

		#region private
		
		/// <summary>
		/// 取得結果
		/// </summary>
		/// <param name="condition"></param>
		/// <returns></returns>
		private GeppoItem GetResultData(ConditionPrint condition)
		{
			IDbConnection cn = ODPConnectionUtils.Connect(Settings.Default.ConnectionString);
			try
			{	GeppoItem r;
				using (IDbCommand command = cn.CreateCommand())
				{
					r = GetResultDataByCommand(condition, command);
				}
				WriteResultToDB(condition, r, cn);
				return r;
			}
			finally
			{
				cn.Close();
			}
		}

		/// <summary>
		/// コマンドオブジェクトから結果を取得する
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		private GeppoItem GetResultDataByCommand(ConditionPrint condition, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetGeppoData";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", condition.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_GEPPO_CURSOR");
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_GEPPO_PREV_CURSOR");
			command.Parameters.Add(p);

			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;

			GeppoItem data = new GeppoItem();

			#region Geppo
			
			using (OracleRefCursor cur = (OracleRefCursor)cmd.Parameters["P_GEPPO_CURSOR"].Value)
			{
				using (OracleDataReader dr = cur.GetDataReader())
				{

					GeppoPrintDataSetter dataSetter = new GeppoPrintDataSetter();
					dataSetter.CreateAllBunrui(data.GeppoItemBunruiList);

					int count = 0;
					while (dr.Read())
					{
						//MiscUtils.WriteDataReaderLogForLog4net(dr, _log);
						count++;
						dataSetter.AddResultData(dr, data);
					}
					_log.DebugFormat("Geppo{0}件取得しました", count);
					data.Count = count;
					if (count < 1)
					{
						return data;
					}
					//画面で入力された放射線門数追加
					dataSetter.AddHoshasen(((ConditionPrintGeppo)condition).Monsu, data);
					dataSetter.CreateAllBunrui(data.GeppoItemBunruiList);
				}
			}
			#endregion

			#region GeppoPrev

			using (OracleRefCursor cur = (OracleRefCursor)cmd.Parameters["P_GEPPO_PREV_CURSOR"].Value)
			{
				using (OracleDataReader dr = cur.GetDataReader())
				{
					GeppoPrintDataSetterPrev dataSetter = new GeppoPrintDataSetterPrev();
					int count = 0;
					while (dr.Read())
					{
						//MiscUtils.WriteDataReaderLogForLog4net(dr, _log);
						count++;
						dataSetter.AddResultData(dr, data);
					}
					_log.DebugFormat("GeppoPrev{0}件取得しました", count);
					data.CountPrev = count;
				}
			}
			#endregion

			return data;
		}


		/// <summary>
		/// STATISTICS_MONTHLY_DATAへ書き込み
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="data"></param>
		/// <param name="cn"></param>
		public void WriteResultToDB(ConditionPrint condition, GeppoItem data, IDbConnection cn)
		{
			IDbTransaction tr = cn.BeginTransaction();
			_log.Debug("BeginTransactionしました");
			try
			{
				GeppoSTATISTICS_MONTHLY_DATAHelper.DeleteToDB(condition.Nengetsu, data, cn, tr);
				GeppoSTATISTICS_MONTHLY_DATAHelper.InsertToDB(condition.Nengetsu, data, cn, tr);
				tr.Commit();
				_log.Debug("Commitしました");
			}
			catch (Exception)
			{
				tr.Rollback();
				_log.Debug("Rollbackしました");
				throw;
			}
		}


		#endregion

		#endregion メソッド

	}
}
