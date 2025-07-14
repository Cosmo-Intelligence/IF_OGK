using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StatisticsPrint.View;
using System.Threading;

namespace StatisticsPrint
{
	static class Program
	{

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

			// ThreadExceptionイベント・ハンドラを登録する
			Application.ThreadException += new
			  ThreadExceptionEventHandler(Application_ThreadException);

			// UnhandledExceptionイベント・ハンドラを登録する
			Thread.GetDomain().UnhandledException += new
			  UnhandledExceptionEventHandler(Application_UnhandledException);


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new menuForm());
			_log.Info("プログラムを終了します");
		}

		// 未処理例外をキャッチするイベント・ハンドラ
		// （Windowsアプリケーション用）
		public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			_log.Error(e.Exception.ToString());
			MessageBox.Show(e.Exception.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		// 未処理例外をキャッチするイベント・ハンドラ
		// （主にコンソール・アプリケーション用）
		public static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex == null)
			{
				return;
			}
			_log.Error(ex.ToString());
			MessageBox.Show(ex.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

	}
}
