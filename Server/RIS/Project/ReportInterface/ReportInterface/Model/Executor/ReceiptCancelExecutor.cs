using System;
using System.Configuration;
using System.Data;
using ReportInterface.Data;
using ReportInterface.Properties;
using ReportInterface.Exceptions;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ReportInterface.Model.Executor
{
	internal class ReceiptCancelExecutor : BaseUpdateExecutor, IExecutor
	{
		#region field

		#endregion

		#region IExecutor メンバ

		public void Execute(ReportInterface.Data.ToReportInfo t, System.Data.IDbConnection cn)
		{
			_log.Info("受付キャンセル処理します");
			WriteToReport(t);
			_log.Info("受付キャンセル処理しました");
		}

		public bool CanExecute(string requestType)
		{
			return string.Compare(ToReportManager.TOREPORTINFO_REQUESTTYPE_RECEIPT_CANCEL, requestType) == 0;
		}

		#endregion

		private void WriteToReport(ToReportInfo t)
		{
			String connectionStringReport = Settings.Default.ConnectionStringReport;
			IDbConnection connectionReport = ODPConnectionUtils.Connect(connectionStringReport);
			try
			{
				ValidateStatusForReport(t.MessageID1, connectionReport);
				using (IDbCommand command = connectionReport.CreateCommand())
				{
					ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
					WriteToReportOrder(t, command);
				}
			}
			finally
			{
				connectionReport.Close();
			}
		}

		private void WriteToReportOrder(ToReportInfo t, IDbCommand command)
		{
			#region sql

			const string UPDATE_SQL =
			"UPDATE examinfo e " +
			"SET " +
				"e.status = :status " +
			"WHERE " +
				"e.odrid = :odrid ";
			#endregion

			#region パラメータ

			const string PARAM_NAME_STATUS = "status";
			const string PARAM_NAME_ODRID = "odrid";
			#endregion

			command.CommandText = UPDATE_SQL;

			command.Parameters.Clear();

			#region パラメータ設定

			command.SetStringToCommand(PARAM_NAME_ODRID, t.MessageID1);
			command.SetStringToCommand(PARAM_NAME_STATUS, GetStatus());

			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("SQL実行します");
			int count = Convert.ToInt32(command.ExecuteNonQuery());
			_log.DebugFormat("対象{0}件", count);
		}

		private string GetStatus()
		{
            const string EXAMINFO_STATUS_RECEIPT_CANCEL = "0";
            return EXAMINFO_STATUS_RECEIPT_CANCEL;
		}
	}
}
