using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using AddToHisInfoFromSQDB.Model;
using System.Configuration;
using RISCommonLibrary.Lib.Utils;

namespace AddToHisInfoFromSQDB
{
	public partial class AddToHisInfoFromSQDBService : ServiceBase
	{
		#region field
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// ループスレッド
		/// </summary>
		private Thread _loopThread = null;

		/// <summary>
		/// サービス用ループクラス
		/// </summary>
		private MasterStudyWatcher _watcher;

		#endregion
	
		public AddToHisInfoFromSQDBService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			System.Diagnostics.FileVersionInfo ver =
					System.Diagnostics.FileVersionInfo.GetVersionInfo(
				System.Reflection.Assembly.GetExecutingAssembly().Location);
			_log.InfoFormat("サービスを開始します。ファイルバージョン={0}", ver.FileVersion);

			_watcher = new MasterStudyWatcher();

			_loopThread = new Thread(new ThreadStart(_watcher.Execute));
			_loopThread.Start();

			_log.Info("サービスを開始しました");
		}

		protected override void OnStop()
		{
			_log.Info("サービスを停止します");
			_watcher.StopLoop();

			int millisecondsTimeout = ConfigurationManager.AppSettings["ThreadStopTimeout"].StringToInt32();
			if (!_loopThread.Join(millisecondsTimeout))
			{
				_log.WarnFormat("制限時間内に処理が終了しませんでした。強制終了します");
				_loopThread.Abort();
			}
			_watcher = null;
			_log.Info("サービスを停止しました");
		}
	}
}
