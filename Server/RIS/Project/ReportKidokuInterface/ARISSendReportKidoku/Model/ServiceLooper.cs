using System;
using System.Configuration;
using System.Threading;
using ARISSend.Data;
using ARISSend.Properties;
using RISBizLibrary.Data;
using RISBizLibrary.Utils;
using RISCommonLibrary.Lib.Utils;
using Oracle.DataAccess.Client;
using RISODPLibrary.Lib.Utils;
using System.Data;

namespace ARISSend.Model
{
	/// <summary>
	/// キューテーブルからデータ取得し、処理を行う
	/// </summary>
    internal class ServiceLooper: IDisposable
    {
		/// <summary>
		/// log4netインスタンス
		/// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
		/// <summary>
		/// 送信処理クラス
		/// </summary>
		private ReportDokueiInfoSender _reportDokueiInfoSender = new ReportDokueiInfoSender();

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
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
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
					#region コメントをループ時減らす
					//_log.DebugFormat("スリープします={0}", sleepTime);
					#endregion
					Thread.Sleep(sleepTime);
				}
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

        public void ExcecuteOne()
        {
            try
			{
				#region コメントをループ時減らす
				//_log.Info("ARISSend処理を開始します");
				#endregion

				String connectionString = Settings.Default.ConnectionString;
				#region コメントをループ時減らす
				//_log.DebugFormat("接続文字列取得しました。{0}",
				//    OracleClientUtils.GetConnectionStringWithoutPassword(connectionString));
				//_log.Debug("DB接続します");
				#endregion
                IDbConnection connection = ODPConnectionUtils.Connect(connectionString);
				//ToHisInfo toHisInfo;
				ReportDokueiInfo reportInfo;
                try 
                {
					reportInfo = ReportDokueiInfoHelper.GetDataFromDB(connection);
					if (reportInfo == null)
					{
						#region コメントをループ時減らす
						//const string MSG_CUE_TABLE_NO_RECORD = "ARISSend処理を終了します。TOHISINFOテーブルに未送信のレコードはありませんでした";
						//_log.Info(MSG_CUE_TABLE_NO_RECORD);
						#endregion
						return;
					}
					_reportDokueiInfoSender.Send(reportInfo, connection);
				}
                finally
                {
					#region コメントをループ時減らす
					//_log.Debug("DB切断します");
					#endregion
					connection.Close();
                }
				#region コメントをループ時減らす
				//_log.Info("ARISSend処理を終了します");
				#endregion
			}
            catch (Exception ex)
            {
                _log.Error("致命的なエラーが発生しました。");
                _log.Error(ex.ToString());
            }
        }

		#region 既読
		
		internal void ConnectTcpReportDokuei()
		{
			try
			{
				//_reportDokueiInfoSender.ConnectTcp(RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT);
				_reportDokueiInfoSender.ConnectTcp();
			}
			catch (Exception ex)
			{
				_log.Error("致命的なエラーが発生しました。");
				_log.Error(ex.ToString());
			}
		}

		internal void DisConnectTcpReportDokuei()
		{
			try
			{
				//_reportDokueiInfoSender.DisConnectTcp(RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT);
				_reportDokueiInfoSender.DisConnectTcp();
			}
			catch (Exception ex)
			{
				_log.Error("致命的なエラーが発生しました。");
				_log.Error(ex.ToString());
			}
		}

		internal void RequestOpenReportDokuei()
		{
			try
			{
				//_reportDokueiInfoSender.RequestOpen(RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT);
				_reportDokueiInfoSender.RequestOpen();
			}
			catch (Exception ex)
			{
				_log.Error("致命的なエラーが発生しました。");
				_log.Error(ex.ToString());
			}
		}

		internal void RequestCloseReportDokuei()
		{
			try
			{
				//_reportDokueiInfoSender.RequestClose(RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT);
				_reportDokueiInfoSender.RequestClose();
			}
			catch (Exception ex)
			{
				_log.Error("致命的なエラーが発生しました。");
				_log.Error(ex.ToString());
			}
		}
		#endregion

		#region 実施
		
		//internal void ConnectTcpExam()
		//{
		//	try
		//	{
		//		_reportDokueiInfoSender.ConnectTcp(RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM);
		//	}
		//	catch (Exception ex)
		//	{
		//		_log.Error("致命的なエラーが発生しました。");
		//		_log.Error(ex.ToString());
		//	}
		//}

		////internal void DisConnectTcpExam()
		////{
		////	try
		////	{
		////		_reportDokueiInfoSender.DisConnectTcp(RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM);
		////	}
		////	catch (Exception ex)
		////	{
		////		_log.Error("致命的なエラーが発生しました。");
		////		_log.Error(ex.ToString());
		////	}
		////}

		//internal void RequestOpenExam()
		//{
		//	try
		//	{
		//		_reportDokueiInfoSender.RequestOpen(RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM);
		//	}
		//	catch (Exception ex)
		//	{
		//		_log.Error("致命的なエラーが発生しました。");
		//		_log.Error(ex.ToString());
		//	}
		//}

		//internal void RequestCloseExam()
		//{
		//	try
		//	{
		//		_reportDokueiInfoSender.RequestClose(RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM);
		//	}
		//	catch (Exception ex)
		//	{
		//		_log.Error("致命的なエラーが発生しました。");
		//		_log.Error(ex.ToString());
		//	}
		//}
		#endregion

		#region 患者
		
		//internal void ConnectTcpPatient()
		//{
		//	try
		//	{
		//		_reportDokueiInfoSender.ConnectTcp(RQRISDBConst.TOHISINFO_REQUESTTYPE_PATIENT);
		//	}
		//	catch (Exception ex)
		//	{
		//		_log.Error("致命的なエラーが発生しました。");
		//		_log.Error(ex.ToString());
		//	}
		//}

		//internal void DisConnectTcpPatient()
		//{
		//	try
		//	{
		//		_reportDokueiInfoSender.DisConnectTcp(RQRISDBConst.TOHISINFO_REQUESTTYPE_PATIENT);
		//	}
		//	catch (Exception ex)
		//	{
		//		_log.Error("致命的なエラーが発生しました。");
		//		_log.Error(ex.ToString());
		//	}
		//}

		//internal void RequestOpenPatient()
		//{
		//	try
		//	{
		//		_reportDokueiInfoSender.RequestOpen(RQRISDBConst.TOHISINFO_REQUESTTYPE_PATIENT);
		//	}
		//	catch (Exception ex)
		//	{
		//		_log.Error("致命的なエラーが発生しました。");
		//		_log.Error(ex.ToString());
		//	}
		//}

		//internal void RequestClosePatient()
		//{
		//	try
		//	{
		//		_reportDokueiInfoSender.RequestClose(RQRISDBConst.TOHISINFO_REQUESTTYPE_PATIENT);
		//	}
		//	catch (Exception ex)
		//	{
		//		_log.Error("致命的なエラーが発生しました。");
		//		_log.Error(ex.ToString());
		//	}
		//}
		#endregion

		#region IDisposable メンバ

		public void Dispose()
		{
			if (_reportDokueiInfoSender != null)
			{
				_reportDokueiInfoSender.Dispose();
				_reportDokueiInfoSender = null;
			}
		}

		#endregion
	}
}
