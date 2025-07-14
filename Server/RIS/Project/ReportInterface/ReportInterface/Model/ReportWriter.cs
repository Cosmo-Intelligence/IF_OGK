using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportInterface.Data;
using System.Data;
using ReportInterface.Properties;
using System.Data.OracleClient;
using RISCommonLibrary.Lib.Utils;
using System.Configuration;

namespace ReportInterface.Model
{
	internal class ReportWriter
	{
		private const string INSERT_SQL = 
		"INSERT INTO tohisinfo " +
		"( " +
			"requestid, " +
			"requestdate, " +
			"ris_id, " +
			"requestuser, " +
			"requestterminalid, " +
			"requesttype, " +
			"messageid1, " +
			"messageid2, " +
			"transferstatus, " +
			"transferdate, " +
			"transferresult, " +
			"transfertext " +
		") " +
		"SELECT " +
			"FROMRISSEQUENCE.NEXTVAL requestid, " +
			"SYSDATE requestdate, " +
			"o.ris_id ris_id, " +
			"'PacsInfo' requestuser, " +
			"'RISサーバ' requestterminalid, " +
			"'10' requesttype, " +
			":orderno messageid1, " +
			"o.kanja_id messageid2, " +
			"'00' transferstatus, " +
			"NULL transferdate, " +
			"NULL transferresult, " +
			"NULL transfertext " +
		"FROM " +
			"ordermaintable o " +
		"WHERE " +
			"o.orderno = :orderno ";

		private const string PARAM_ORDERNO = "orderno";

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// TOHISINFOへ書き込み
		/// </summary>
		/// <param name="list"></param>
		/// <param name="cnRIS"></param>
		public void Write(List<ToReportInfo> list, IDbConnection cn)
		{
			IDbTransaction tr = cn.BeginTransaction();
			_log.Debug("BeginTransactionしました");
			try
			{
				Write(list, cn, tr);
				//TODO: 
				//MasterstudySentToRISHelper.DeleteToDB(list.LastOrDefault(), cn, tr);
				//MasterstudySentToRISHelper.InsertToDB(list, cn, tr);
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

		/// <summary>
		/// TOHISINFOへ書き込み
		/// </summary>
		/// <param name="list"></param>
		/// <param name="cn"></param>
		/// <param name="tr"></param>
		private void Write(List<ToReportInfo> list, IDbConnection cn,
			IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommand(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = INSERT_SQL;
				foreach (var item in list)
				{
					Write(item, command);
				}
			}
		}

		/// <summary>
		/// TOHISINFOへ書き込み
		/// </summary>
		/// <param name="masterStudy"></param>
		/// <param name="cn"></param>
		/// <param name="tr"></param>
		private void Write(ToReportInfo toReportInfo, IDbCommand command)
		{
			#region パラメータ設定
			command.Parameters.Clear();
			IDataParameter param = command.CreateParameter();
			
			//TODO: なおうｓ
			//param.SetInputString(PARAM_ORDERNO, toReportInfo.AccessionNumber);
			command.Parameters.Add(param);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.Debug("TO_HIS_IONFOテーブル新規作成します");
			int cnt = command.ExecuteNonQuery();
			if (cnt == 0)
			{
				//TODO: なおす
				//_log.WarnFormat("ORDERMAINTABLEに存在しないORDERNOのデータ作成はできません={0}", toReportInfo.AccessionNumber);
			}
			_log.DebugFormat("TO_HIS_IONFOテーブル新規作成しました={0}件", cnt);
		}
	}
}
