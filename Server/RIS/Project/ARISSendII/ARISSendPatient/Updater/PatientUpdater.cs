using System.Configuration;
using System.Data;
using RISBizLibrary.Updater.Table;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISSend.Updater
{
	/// <summary>
	/// 患者系のみ更新する
	/// </summary>
	public class PatientUpdater
	{
		#region field

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#endregion

		#region method

		#region insert

		/// <summary>
		/// InsertOrUpdate
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="cn"></param>
		/// <param name="tr"></param>
		public void InsertOrUpdate(BaseMsg msg, IDbConnection cn, IDbTransaction tr)
		{
			_log.Debug("InsertOrUpdate開始します");
			_log.Debug("患者情報更新処理を行います");
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;

				_log.Debug("PATIENTINFO更新処理を行います");
				PATIENTINFOUpdater patientInfoUpdater = new PATIENTINFOUpdater();
				patientInfoUpdater.Execute(msg, command);

				_log.Debug("PATIENTCOMMENTS更新処理を行います");
				PATIENTCOMMENTSUpdater patientCommentsUpdater = new PATIENTCOMMENTSUpdater();
				patientCommentsUpdater.Execute(msg, command);
			}
			_log.Debug("InsertOrUpdate終了しました");
		}
		
		#endregion

		#endregion
	}
}
