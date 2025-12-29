using System;
using System.Configuration;
using System.Data;
using ARISReceive.Data;
using ARISReceive.Updater.Table;
using RISBizLibrary.Updater.Table;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISReceive.Updater
{
	/// <summary>
	/// オーダ電文から更新する
	/// </summary>
	internal class OrderUpdater
	{
		#region field

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// 削除対象テーブル
		/// </summary>
		private string[] _deleteTables = new[] 
		{ 
			"ordermaintable", 
			"extendorderinfo", 
			"orderbuitable", 
			"orderindicatetable", 
			"exmaintable", 
			"order_comment"
		};

		#endregion

		#region method
		
		/// <summary>
		/// 削除(トランザクションを中で行う方)
		/// </summary>
		/// <param name="data"></param>
		/// <param name="cn"></param>
		public void Delete(OrderMsgData data, IDbConnection cn)
		{
			_log.Debug("Delete開始します");
			IDbTransaction tr = cn.BeginTransaction();
			_log.Debug("BeginTransactionしました");
			try
			{
				_log.Debug("オーダ削除処理を行います");
				Delete(data, cn, tr);
				InsertForDelete(data, cn, tr);
				tr.Commit();
				_log.Debug("Commitしました");
			}
			catch (Exception)
			{
				_log.Debug("Delete処理でエラーが発生しました");
				tr.Rollback();
				_log.Debug("Rollbackしました");
				throw;
			}
			_log.Debug("Delete終了しました");
		}

		/// <summary>
		/// 削除と新規
		/// </summary>
		/// <param name="data"></param>
		/// <param name="cn"></param>
		public void DeleteAndInsert(OrderMsgData data, IDbConnection cn)
		{
			_log.Debug("DeleteAndInsert開始します");
			IDbTransaction tr = cn.BeginTransaction();
			_log.Debug("BeginTransactionしました");
			try
			{
				_log.Debug("オーダ作成処理を行います");
				Delete(data, cn, tr);
				InsertOrUpdate(data, cn, tr);
				tr.Commit();
				_log.Debug("Commitしました");
			}
			catch (Exception)
			{
				_log.DebugFormat("DeleteAndInsert処理でエラーが発生しました。");
				tr.Rollback();
				_log.Debug("Rollbackしました");
				throw;
			}
			_log.Debug("DeleteAndInsert終了しました");
		}

		#region delete
		
		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="order"></param>
		/// <param name="cn"></param>
		private void Delete(OrderMsgData order, IDbConnection cn, IDbTransaction tr)
		{
			_log.Debug("Delete開始します");
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				foreach (string tableName in _deleteTables)
				{
					Delete(tableName, order.RIS_ID, command);
				}
			}
			_log.Debug("Delete終了しました");
		}

		/// <summary>
		/// 削除実行
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="ris_id"></param>
		/// <param name="command"></param>
		private void Delete(string tableName, string ris_id, IDbCommand command)
		{
			const string DELETE_SQL =
				"DELETE {0} t " +
				"WHERE " +
				"t.ris_id = :{1} ";

			const string param_name = "ris_id";
			
			command.CommandText = string.Format(DELETE_SQL, tableName, param_name);

			command.Parameters.Clear();
			IDataParameter param = command.CreateParameter();
			param.SetInputString(param_name, ris_id);
			command.Parameters.Add(param);

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("Delete実行します");
			int count = Convert.ToInt32(command.ExecuteNonQuery());
			_log.DebugFormat("対象{0}件", count);
		}

		#endregion

		#region insert

		/// <summary>
		/// InsertOrUpdate
		/// </summary>
		/// <param name="data"></param>
		/// <param name="cn"></param>
		/// <param name="tr"></param>
		private void InsertOrUpdate(OrderMsgData data, IDbConnection cn, IDbTransaction tr)
		{
			_log.Debug("InsertOrUpdate開始します");
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;

				_log.Debug("ORDERMAINTABLE更新処理を行います");
				ORDERMAINTABLEUpdater orderMainUpdater = new ORDERMAINTABLEUpdater();
				orderMainUpdater.Execute(data, command);

				_log.Debug("EXTENDORDERINFO更新処理を行います");
				EXTENDORDERINFOUpdater extendOrderInfoUpdater = new EXTENDORDERINFOUpdater();
				extendOrderInfoUpdater.Execute(data, command);

				_log.Debug("ORDERBUITABLE更新処理を行います");
				ORDERBUITABLEUpdater orderBuiTableUpdater = new ORDERBUITABLEUpdater();
				orderBuiTableUpdater.Execute(data, command);

				_log.Debug("ORDER_COMMENT更新処理を行います");
				ORDER_COMMENTUpdater orderCommentUpdater = new ORDER_COMMENTUpdater();
				orderCommentUpdater.Execute(data, command);

				_log.Debug("ORDERINDICATETABLE更新処理を行います");
				ORDERINDICATETABLEUpdater orderIndicateTableUpdater = new ORDERINDICATETABLEUpdater();
				orderIndicateTableUpdater.Execute(data, command);

				_log.Debug("PATIENTINFO更新処理を行います");
				PATIENTINFOUpdater patientInfoUpdater = new PATIENTINFOUpdater();
				patientInfoUpdater.Execute(data.Request, command);

				_log.Debug("PATIENTCOMMENTS更新処理を行います");
				PATIENTCOMMENTSUpdater patientCommentsUpdater = new PATIENTCOMMENTSUpdater();
				patientCommentsUpdater.Execute(data.Request, command);

				_log.Debug("EXMAINTABLE更新処理を行います");
				EXMAINTABLEUpdater exmainTableUpdater = new EXMAINTABLEUpdater();
				exmainTableUpdater.Execute(data, command);

				_log.Debug("FROMHISINFO更新処理を行います");
				FROMHISINFOUpdater fromHisInfoUpdater = new FROMHISINFOUpdaterOrder();
				fromHisInfoUpdater.Execute(data, command);

				if (data.Request.MsgBody.CommunicationControl.TELEGRAPH_KIND.TrimData == MsgConst.TELEGRAPH_KIND_ORDER_INFO)
				{
					_log.Debug("TOREPORTINFO更新処理を行います");
					TOREPORTINFOUpdater toReportInfoUpdater = new TOREPORTINFOUpdater();
					toReportInfoUpdater.Execute(data, command);

					// 大垣市民病院向けHIS通信機能 改造対応 2025.12.24 sta
					_log.Debug("TOHISINFO更新処理を行います");
					TOHISINFOUpdaterOrder toHisInfoUpdater = new TOHISINFOUpdaterOrder();
					toHisInfoUpdater.Execute(data, command);
					// 大垣市民病院向けHIS通信機能 改造対応 2025.12.24 end

				}

			}
			_log.Debug("InsertOrUpdate終了しました");
		}

		/// <summary>
		/// 削除用Insert
		/// </summary>
		/// <param name="data"></param>
		/// <param name="cn"></param>
		/// <param name="tr"></param>
		private void InsertForDelete(OrderMsgData data, IDbConnection cn, IDbTransaction tr)
		{
			_log.Debug("InsertForDelete開始します");
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;

				_log.Debug("FROMHISINFO更新処理を行います");
				FROMHISINFOUpdater fromHisInfoUpdater = new FROMHISINFOUpdaterOrder();
				fromHisInfoUpdater.Execute(data, command);

				_log.Debug("TOREPORTINFO更新処理を行います");
				TOREPORTINFOUpdater toReportInfoUpdater = new TOREPORTINFOUpdater();
				toReportInfoUpdater.Execute(data, command);
			}
			_log.Debug("InsertForDelete終了しました");
		}
		
		#endregion

		#endregion
	}
}
