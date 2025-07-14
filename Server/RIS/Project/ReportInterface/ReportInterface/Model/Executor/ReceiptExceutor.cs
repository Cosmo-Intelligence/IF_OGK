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
	internal class ReceiptExceutor : BaseUpdateExecutor, IExecutor
	{
		#region field


		#endregion

		#region IExecutor メンバ

		public void Execute(ReportInterface.Data.ToReportInfo t, System.Data.IDbConnection cn)
		{
			_log.Info("受付処理します");

			BaseData data = ReadFromRISForPatient(t, cn);
			WriteToReport(t, data);

			_log.Info("受付処理しました");
		}

		public bool CanExecute(string requestType)
		{
			return string.Compare(ToReportManager.TOREPORTINFO_REQUESTTYPE_RECEIPT, requestType) == 0;
		}

		#endregion

		private BaseData ReadFromRISForPatient(ToReportInfo t, System.Data.IDbConnection cn)
		{
			_log.Debug("患者情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"p.kanasimei kana, " +
				"p.romasimei roma, " +
				"p.kanjisimei kanji, " +
				"p.sex sex, " +
				"TO_DATE(TO_CHAR(p.birthday,'FM00000000')) birthday, " +
				"TRUNC(p.tall, 1) tall, " +
				"TRUNC(p.weight, 1) weight " +
			"FROM " +
				"patientinfo p " +
			"WHERE " +
				"p.kanja_id = :kanja_id ";

			const string FIELD_KANA = "kana";
			const string FIELD_ROMA = "roma";
			const string FIELD_KANJI = "kanji";
			const string FIELD_SEX = "sex";
			const string FIELD_BIRTHDAY = "birthday";
			const string FIELD_TALL = "tall";
			const string FIELD_WEIGHT = "weight";

			const string PARAM_KANJA_ID = "kanja_id";

			using (IDbCommand command = cn.CreateCommand())
			{
				ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_KANJA_ID, t.MessageID2);
				command.Parameters.Add(param);

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new ReportDataNotFoundException(string.Format(
							"KANJA_IDデータが見つかりませんでした。KANJA_ID={0}", t.MessageID2));
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					#region 設定する
					DataReceipt data = new DataReceipt();

					data.KANA = reader.GetStringByDB(FIELD_KANA);
					data.ROMA = reader.GetStringByDB(FIELD_ROMA);
					data.KANJI = reader.GetStringByDB(FIELD_KANJI);
					data.SEX = reader.GetStringByDB(FIELD_SEX);
					data.BIRTHDAY = reader.GetStringByDB(FIELD_BIRTHDAY);
					data.TALL = reader.GetStringByDB(FIELD_TALL);
					data.WEIGHT = reader.GetStringByDB(FIELD_WEIGHT);

					#endregion

					return data;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		private void WriteToReport(ToReportInfo t, BaseData data)
		{
			String connectionStringReport = Settings.Default.ConnectionStringReport;
			IDbConnection connectionReport = ODPConnectionUtils.Connect(connectionStringReport);
			try
			{
				ValidateStatusForReport(t.MessageID1, connectionReport);
				using (IDbCommand command = connectionReport.CreateCommand())
				{
					ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
					WriteToReportOrder(t, data, command);
				}
			}
			finally
			{
				connectionReport.Close();
			}
		}

		private void WriteToReportOrder(ToReportInfo t, BaseData data, IDbCommand command)
		{
			#region sql

			const string UPDATE_SQL =
			"UPDATE examinfo e " +
			"SET " +
				"e.kana = :kana, " +
				"e.roma = :roma, " +
				"e.kanji = :kanji, " +
				"e.sex = :sex, " +
				"e.birthday = :birthday, " +
				"e.status = :status, " +
				"e.tall = :tall, " +
				"e.weight = :weight " +
			"WHERE " +
				"e.odrid = :odrid ";
			#endregion

			#region パラメータ

			const string PARAM_NAME_KANA = "kana";
			const string PARAM_NAME_ROMA = "roma";
			const string PARAM_NAME_KANJI = "kanji";
			const string PARAM_NAME_SEX = "sex";
			const string PARAM_NAME_BIRTHDAY = "birthday";
			const string PARAM_NAME_STATUS = "status";
			const string PARAM_NAME_ODRID = "odrid";
			const string PARAM_NAME_TALL = "tall";
			const string PARAM_NAME_WEIGHT = "weight";

			#endregion

			command.CommandText = UPDATE_SQL;

			DataReceipt dataReceipt = (DataReceipt)data;

			command.Parameters.Clear();

			#region パラメータ設定

			command.SetStringToCommand(PARAM_NAME_KANA, dataReceipt.KANA);
			command.SetStringToCommand(PARAM_NAME_ROMA, dataReceipt.ROMA);
			command.SetStringToCommand(PARAM_NAME_KANJI, dataReceipt.KANJI);
			command.SetStringToCommand(PARAM_NAME_SEX, dataReceipt.SEX);
			command.SetDateTimeToCommand(PARAM_NAME_BIRTHDAY, dataReceipt.BIRTHDAY);
			command.SetStringToCommand(PARAM_NAME_ODRID, t.MessageID1);
			command.SetStringToCommand(PARAM_NAME_STATUS, GetStatus());
			command.SetDecimalToCommand(PARAM_NAME_TALL, dataReceipt.TALL);
			command.SetDecimalToCommand(PARAM_NAME_WEIGHT, dataReceipt.WEIGHT);

			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("SQL実行します");
			int count = Convert.ToInt32(command.ExecuteNonQuery());
			_log.DebugFormat("対象{0}件", count);
		}

		private string GetStatus()
		{
            // ■y_higuchi 2019.03 - 仕様変更 - ステータス値の変更 - add - ここから
            const string EXAMINFO_STATUS_RECEIPT = "10";
            // ■y_higuchi 2019.03 - 仕様変更 - ステータス値の変更 - add - ここまで - del - ここから
            //const string EXAMINFO_STATUS_RECEIPT = "1";
            // ■y_higuchi 2019.03 - 仕様変更 - ステータス値の変更 - del - ここまで
            return EXAMINFO_STATUS_RECEIPT;
        }
    }
}
