using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using RISBizLibrary.Config;
using ARISReceive.Properties;

namespace ARISReceive
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
		static void Main()
		{
			string[] args = Environment.GetCommandLineArgs();

			SettingsAdapter.Instance.RegistSettings(Settings.Default); //dllでSettingsを参照できるようにする

			if (IsGuiMode(args))
			{
				//ThreadExceptionイベントハンドラを追加
				Application.ThreadException +=
					new System.Threading.ThreadExceptionEventHandler(
						Application_ThreadException);

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new ARISReceiveFormHospital());
				return;
			}

			//UnhandledExceptionイベントハンドラを追加
			System.AppDomain.CurrentDomain.UnhandledException +=
				new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new ARISReceiveServiceHospital() 
			};
			ServiceBase.Run(ServicesToRun);
		}

		/// <summary>
		/// サービスモードか？GUIモードか？
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks>コマンドラインにguiが設定されてきたらGUIモード</remarks>
		private static bool IsGuiMode(string[] args)
		{
			if (args == null)
			{
				return false;
			}
			if (args.Count() < 2)
			{
				return false;
			}
			if (string.Compare(args[1], "gui", true) != 0)
			{
				return false;
			}
			return true;
		}

		private static void Application_ThreadException(object sender,
			System.Threading.ThreadExceptionEventArgs e)
		{
			_log.ErrorFormat("未補足の例外が発生しました。{0}", e.Exception);
		}

		//UnhandledExceptionイベントハンドラ
		private static void CurrentDomain_UnhandledException(object sender,
			UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex == null)
			{
				return;
			}
			_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
		}
	}
}
