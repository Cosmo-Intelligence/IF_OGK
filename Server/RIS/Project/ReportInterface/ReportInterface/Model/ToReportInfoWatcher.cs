using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportInterface.Data;
using ReportInterface.Model;
using System.Configuration;
using System.Threading;
using RISCommonLibrary.Lib.Utils;
using ReportInterface.Properties;
using System.Data;
using ReportInterface.Utils;
using RISODPLibrary.Lib.Utils;

namespace ReportInterface.Model
{
	/// <summary>
	/// 監視するぞ
	/// </summary>
	internal class ToReportInfoWatcher
	{
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// ToReport処理クラス
		/// </summary>
		private ToReportManager _toReportManager = new ToReportManager();

		/// <summary>
		/// 古いログ削除クラス
		/// </summary>
		private DeleteOldLogHelper _deleteOldLogHelper = new DeleteOldLogHelper();

		/// <summary>
		/// ループ処理続行フラグ
		/// </summary>
		private bool _isContinue = true;

		/// <summary>
		/// 同期オブジェクト
		/// </summary>
		private Object _thisLock = new Object();

		/// <summary>
		/// ループ処理を続けるか？
		/// </summary>
		public bool IsContinue
		{
			get
			{
				return _isContinue;
			}
			private set
			{
				lock (_thisLock)
				{
					_isContinue = value;
				}
			}
		}

		public void StopLoop()
		{
			try
			{
				if (!IsContinue)
				{
					return;
				}
				_log.Debug("プログラムへ停止指示します");
				IsContinue = false;
			}
			catch (Exception e)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", e);
				throw;
			}
		}

		public void Execute()
		{
			Execute(null);
		}

		public void Execute(Action func)
		{
			try
			{
				IsContinue = true;
				while (IsContinue)
				{
					ExcecuteOne();

					if (func != null)
					{
						func();
					}
					int sleepTime = ConfigurationManager.AppSettings["ThreadSleepTime"].StringToInt32();
					Thread.Sleep(sleepTime);
				}
			}
			catch (Exception e)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", e);
				throw;
			}
		}

		public void ExcecuteOne()
		{
			try
			{
				String connectionStringRIS = Settings.Default.ConnectionStringRIS;
                IDbConnection connectionRIS = ODPConnectionUtils.Connect(connectionStringRIS);
				try
				{
					ToReportInfo t = ToReportInfoHelper.GetDataFromDB(connectionRIS); //ToReportInfo取得
					if (t == null)
					{
						return;
					}
					_log.Debug("古いログファイルを削除します");
					_deleteOldLogHelper.DeleteOldLog();
					_log.Debug("TOHISINFOに書き込みます");
					_toReportManager.Execute(t, connectionRIS);
				}
				finally
				{
					connectionRIS.Close();
				}
			}
			catch (Exception ex)
			{
				_log.Error("致命的なエラーが発生しました。");
				_log.Error(ex.ToString());
			}
		}

	}
}
