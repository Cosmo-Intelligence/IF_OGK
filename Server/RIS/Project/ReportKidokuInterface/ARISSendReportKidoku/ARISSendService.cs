using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using ARISSend.Model;
using RISCommonLibrary.Lib.Utils;

namespace ARISSend
{
	public partial class ARISSendService : ServiceBase
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
		private ServiceLooper _looper;

		#endregion

		public ARISSendService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			System.Diagnostics.FileVersionInfo ver =
					System.Diagnostics.FileVersionInfo.GetVersionInfo(
				System.Reflection.Assembly.GetExecutingAssembly().Location);
			_log.InfoFormat("サービスを開始します。ファイルバージョン={0}", ver.FileVersion);

			_looper = new ServiceLooper();

			_loopThread = new Thread(new ThreadStart(_looper.Execute));
			_loopThread.Start();

			_log.Info("サービスを開始しました");
		}

		protected override void OnStop()
		{
			_log.Info("サービスを停止します");
			_looper.StopLoop();

			int millisecondsTimeout = ConfigurationManager.AppSettings["ThreadStopTimeout"].StringToInt32();
			if (!_loopThread.Join(millisecondsTimeout))
			{
				_log.WarnFormat("制限時間内に処理が終了しませんでした。強制終了します");
				_loopThread.Abort();
			}
			_looper.Dispose();
			_looper = null;
			_log.Info("サービスを停止しました");
		}

	}
}
