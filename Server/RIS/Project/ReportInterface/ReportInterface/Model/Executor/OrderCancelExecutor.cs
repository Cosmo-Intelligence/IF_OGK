using System;
using System.Configuration;
using System.Data;
using ReportInterface.Exceptions;
using ReportInterface.Properties;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ReportInterface.Model.Executor
{
	internal class OrderCancelExecutor : BaseUpdateExecutor, IExecutor
	{
		#region IExecutor メンバ

		public void Execute(ReportInterface.Data.ToReportInfo t, System.Data.IDbConnection cn)
		{
			_log.Info("オーダキャンセル処理します");
			WriteToReport(t);
			_log.Info("オーダキャンセル処理しました");
		}

		public bool CanExecute(string requestType)
		{
			return string.Compare(ToReportManager.TOREPORTINFO_REQUESTTYPE_ORDER_CANCEL, requestType) == 0;
		}

        #endregion

        // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - del - ここから
        // protected override void ValidateStatusForReport(string status, string rptid)
		//{
		//	base.ValidateStatusForReport(status, rptid);
		//	if (!string.IsNullOrEmpty(rptid))
		//	{
        //      throw new ReportStatusException(
		//			string.Format(
		//			"すでにRPTIDが発番されているオーダです。RPTID={0}", rptid));
		//	}
		//}
        // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - del - ここまで

        private void WriteToReport(ReportInterface.Data.ToReportInfo t)
		{
			String connectionStringReport = Settings.Default.ConnectionStringReport;
            IDbConnection connectionReport = ODPConnectionUtils.Connect(connectionStringReport);
			try
			{
                // ステータスのチェック
                ValidateStatusForReport(t.MessageID1, connectionReport);

				using (IDbCommand command = connectionReport.CreateCommand())
				{
                    ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());

                    // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - add - ここから
                    String RPTID_AVAILABLE = 
                        " SELECT count(rpti.id) id_cnt, min(rpti.id) rpt_id FROM examinfo exai, reportinfo rpti" +
                        " WHERE (exai.odrid = :prm_odrid ) AND (exai.patid = :prm_patid ) AND (exai.rptid = rpti.id)";
                    command.CommandText = RPTID_AVAILABLE;
                    command.Parameters.Clear();
                    command.SetStringToCommand("prm_odrid", t.MessageID1);
                    command.SetStringToCommand("prm_patid", t.MessageID2);
                    //_log.Info(String.Format("SQL={0},ordid={1}",RPTID_AVAILABLE, t.MessageID1));
                    IDataReader reader = command.ExecuteReader();
                    try
                    {
                        if (!reader.Read())
                        {
                            throw new ReportDataNotFoundException(string.Format(
                                "odridデータがReportDBに見つかりませんでした。odrid={0}", t.MessageID1));
                        }
                        MiscUtils.WriteDataReaderLogForLog4net(reader, _log);
                        int id_cnt = int.Parse(reader.GetStringByDB("id_cnt"));
                        if (id_cnt > 0)
                        {
                            String rpt_id = reader.GetStringByDB("rpt_id");
                            throw new ReportStatusException(
                                  string.Format(
                                  "すでにRPTIDが発番されているオーダです。RPTID={0}", rpt_id));
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                    command.CommandText = "";
                    command.Parameters.Clear();
                    // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - add - ここまで

					WriteToReportOrder(t, command);
				}
			}
			finally
			{
				connectionReport.Close();
			}
		}

        /// <summary>
        /// ExamBuiInfoテーブルのレコード削除
        /// ExamInfoテーブルのレコード削除と同じタイミングで、関連するレコードを削除
        /// 2019.05.08 仕様変更により追加機能
        /// </summary>
        /// <param name="toRepInfo"></param>
        /// <param name="command"></param>
        private void Delete_ExamBuiInfo(ReportInterface.Data.ToReportInfo toRepInfo, IDbCommand command)
        {
            const string DELETE_SQL =
            " DELETE exambuiinfo exabi" +
            " WHERE ( exabi.id IN (" +
                    " SELECT exai.id" +
                    " FROM examinfo exai" +
                    " WHERE (exai.odrid = :prm_odrid)" +
                    " AND (exai.patid = :prm_patid)" +
                  " ))";
            command.CommandText = DELETE_SQL;
            command.Parameters.Clear();
            command.SetStringToCommand("prm_odrid", toRepInfo.MessageID1);
            command.SetStringToCommand("prm_patid", toRepInfo.MessageID2);
            MiscUtils.WriteDbCommandLogForLog4net(command, _log);
            _log.DebugFormat("SQL実行します");
            try
            {
                int count = Convert.ToInt32(command.ExecuteNonQuery());
                _log.DebugFormat("DELETE exambuiinfo 対象{0}件 - patid = {1}, ordid = {2}", count, toRepInfo.MessageID2, toRepInfo.MessageID1);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("DELETE exambuiinfo エラー：{0}", ex.Message);
                throw ex;
            }
        }

        private void WriteToReportOrder(ReportInterface.Data.ToReportInfo t, IDbCommand command)
		{
            // ■y_higuchi 2019.05- 仕様変更 - ExamBuiInfoテーブルのレコード削除 - add - ここから
            // 先に関連するExamBuiInfoのレコードを削除する
            Delete_ExamBuiInfo(t, command);
            // ■y_higuchi 2019.05- 仕様変更 - ExamBuiInfoテーブルのレコード削除 - add - ここまで

            #region sql
            const string DELETE_SQL =
			"DELETE examinfo e " +
			"WHERE " +
                // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - ここから
                // "e.rptid IS NULL AND " +
                " (e.patid = :prm_patid) AND " +
                // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - ここまで
                "e.odrid = :odrid ";
			#endregion

			#region パラメータ
			const string PARAM_NAME_ODRID = "odrid";
			#endregion

			command.CommandText = DELETE_SQL;

			command.Parameters.Clear();

			#region パラメータ設定

			command.SetStringToCommand(PARAM_NAME_ODRID, t.MessageID1);

            // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - add - ここから
            command.SetStringToCommand("prm_patid", t.MessageID2);
            // ■y_higuchi 2019.03 - 仕様変更 - 削除条件の変更 - add - ここまで

            #endregion

            MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("SQL実行します");
			int count = Convert.ToInt32(command.ExecuteNonQuery());
			_log.DebugFormat("対象{0}件", count);
		}


	}
}
