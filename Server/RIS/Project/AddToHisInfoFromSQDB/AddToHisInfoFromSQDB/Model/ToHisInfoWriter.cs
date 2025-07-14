using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using AddToHisInfoFromSQDB.Data;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace AddToHisInfoFromSQDB.Model
{
	internal class ToHisInfoWriter
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
			"o.orderno = :orderno AND " +
			"o.systemkbn = '0' "; //HIS発生オーダのみ

		private const string PARAM_ORDERNO = "orderno";

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// TOHISINFOへ書き込み
		/// </summary>
		/// <param name="list"></param>
		/// <param name="cnRIS"></param>
		public void Write(List<MasterStudy> list, IDbConnection cn)
		{
			IDbTransaction tr = cn.BeginTransaction();
			_log.Debug("BeginTransactionしました");
			try
			{
				Write(list, cn, tr);
				MasterstudySentToRISHelper.DeleteToDB(list.LastOrDefault(), cn, tr);
				MasterstudySentToRISHelper.InsertToDB(list, cn, tr);
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
		private void Write(List<MasterStudy> list, IDbConnection cn,
			IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
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
		private void Write(MasterStudy masterStudy, IDbCommand command)
		{
			#region パラメータ設定
			command.Parameters.Clear();
			IDataParameter param = command.CreateParameter();
			param.SetInputString(PARAM_ORDERNO, masterStudy.AccessionNumber);
			command.Parameters.Add(param);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.Debug("TO_HIS_IONFOテーブル新規作成します");
			int cnt = command.ExecuteNonQuery();
			if (cnt == 0)
			{
				_log.InfoFormat("ORDERMAINTABLEに存在しない、またはRIS発生オーダのORDERNOではデータ作成できません={0}", masterStudy.AccessionNumber);
			}
			_log.DebugFormat("TO_HIS_IONFOテーブル新規作成しました={0}件", cnt);
		}
	}
}
