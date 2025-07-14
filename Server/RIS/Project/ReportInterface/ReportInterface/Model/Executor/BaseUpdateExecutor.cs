using System.Configuration;
using System.Data;
using ReportInterface.Exceptions;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ReportInterface.Model.Executor
{
	internal class BaseUpdateExecutor : BaseExecutor
	{

		protected virtual void ValidateStatusForReport(string odrid, System.Data.IDbConnection cn)
		{
			_log.Debug("ReportDBステータスチェックします");

			const string SQL_SELECT =
			"SELECT " +
				"e.status, " +
				"e.rptid " +
			"FROM " +
				"examinfo e " +
			"WHERE " +
				"e.odrid = :odrid ";

			const string FIELD_STATUS = "status";
			const string FIELD_RPTID = "rptid";

			const string PARAM_ODRID = "odrid";

			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_ODRID, odrid);
				command.Parameters.Add(param);

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new ReportDataNotFoundException(string.Format(
							"odridデータがReportDBに見つかりませんでした。odrid={0}", odrid));
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					string status = reader.GetStringByDB(FIELD_STATUS);
					string rptid = reader.GetStringByDB(FIELD_RPTID);

					ValidateStatusForReport(status, rptid);
				}
				finally
				{
					reader.Close();
				}
			}
		}

		protected virtual void ValidateStatusForReport(string status, string rptid)
		{
            // ■y_higuchi 2019.03 - 仕様変更 - 更新条件の変更 - add - ここから
            const string EXAMINFO_STATUS_RESERVE = "0"; //予約
            const string EXAMINFO_STATUS_RECEIPT_DONE = "10"; //受付済
            const string EXAMINFO_STATUS_INSPECT_DONE = "20"; //検査済
            const string EXAMINFO_STATUS_INTERPRET_WAITE = "30"; //読影待
            if ((status == EXAMINFO_STATUS_RESERVE)
                || (status == EXAMINFO_STATUS_RECEIPT_DONE)
                || (status == EXAMINFO_STATUS_INSPECT_DONE)
                || (status == EXAMINFO_STATUS_INTERPRET_WAITE))
            {
                // 更新してＯＫ
            }
            else
            {
                // 更新ＮＧ
                throw new ReportStatusException(
                    string.Format(
                    "すでに確定待ち・確定されているオーダです。STATUS={0}", status));
            }
            // ■y_higuchi 2019.03 - 仕様変更 - 更新条件の変更 - add - ここまで - del - ここから
            //const string EXAMINFO_STATUS_WAIT_FIX = "7"; //確定待ち
            //const string EXAMINFO_STATUS_FIX = "9"; //確定
            //if ((status == EXAMINFO_STATUS_WAIT_FIX) ||
            //			(status == EXAMINFO_STATUS_FIX))
            //{
            //	throw new ReportStatusException(
            //		string.Format(
            //		"すでに確定待ち・確定されているオーダです。STATUS={0}", status));
            //}
            // ■y_higuchi 2019.03 - 仕様変更 - 更新条件の変更 - del - ここまで
        }

    }
}
