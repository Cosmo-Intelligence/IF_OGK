using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ServiceWatcher
{
	static class Program
	{
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			System.Diagnostics.FileVersionInfo ver =
					System.Diagnostics.FileVersionInfo.GetVersionInfo(
				System.Reflection.Assembly.GetExecutingAssembly().Location);
			_log.InfoFormat("プログラムを開始します。ファイルバージョン={0}", ver.FileVersion);

			//ThreadExceptionイベントハンドラを追加
			Application.ThreadException +=
					new System.Threading.ThreadExceptionEventHandler(
					Application_ThreadException);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new serviceWatcherForm());
			_log.Info("プログラムを終了します");
		}

		private static void Application_ThreadException(object sender,
			System.Threading.ThreadExceptionEventArgs e)
		{
			_log.ErrorFormat("未補足の例外が発生しました。{0}", e.Exception);
		}


	}
}
