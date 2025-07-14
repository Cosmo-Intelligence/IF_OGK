using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportInterface.Data;
using System.Data;
using ReportInterface.Exceptions;
using RISCommonLibrary.Lib.Utils;
using ReportInterface.Model.Executor;

namespace ReportInterface.Model
{
	internal class ToReportManager
	{
        #region const

        #region 要求種別

        /// <summary>
        /// オーダ発生 OrderExecutor
        /// </summary>
        public const string TOREPORTINFO_REQUESTTYPE_ORDER = "OI01";

        /// <summary>
        /// オーダキャンセル OrderCancelExecutor
        /// </summary>
        public const string TOREPORTINFO_REQUESTTYPE_ORDER_CANCEL = "OI99";

        /// <summary>
        /// 受付 ReceiptExceutor
        /// </summary>
        public const string TOREPORTINFO_REQUESTTYPE_RECEIPT = "RC01";

        /// <summary>
        /// 受付キャンセル ReceiptCancelExecutor
        /// </summary>
        public const string TOREPORTINFO_REQUESTTYPE_RECEIPT_CANCEL = "RC99";

        /// <summary>
        /// 検査完了 ExamFinishExecutor
        /// </summary>
        public const string TOREPORTINFO_REQUESTTYPE_EXAM_FINISH = "OP01";

        /// <summary>
        /// 検査完了(再) ExamFinishExecutor
        /// </summary>
        public const string TOREPORTINFO_REQUESTTYPE_EXAM_FINISH_RE = "OP02";

		#endregion
		
		#endregion

		#region field

		/// <summary>
		/// ログ
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// 実行クラスリスト
		/// </summary>
		private List<IExecutor> _executorFactory = new List<IExecutor>();

		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ToReportManager()
		{
			RegistExecutorFactory();
		}

		public void Execute(ToReportInfo t, IDbConnection cn)
		{
			_log.InfoFormat("IF処理開始します。RequestID={0}", t.RequestID);
			try
			{
				_log.Debug("処理作成者を作成します");
				IExecutor executor = GetExecutorByRequestType(t.RequestType);
				if (executor == null)
				{
					throw new ReportRequestTypeNotDefineException(string.Format("未定義のREQUESTTYPEです={0}", t.RequestType));
				}

				_log.InfoFormat("処理対象種別={0}", t.RequestType);
				executor.Execute(t, cn);

				_log.Info("TOREPORTINFO更新処理を行います");
				UpdateDBNormal(t, cn);

				_log.InfoFormat("IF処理終了します。RequestID={0}", t.RequestID);
			}
			catch (ReportWarningException rex)
			{
				_log.WarnFormat(rex.Message);
				_log.Info("データベース更新処理を行います");
				UpdateDBNormal(t, cn);
				_log.InfoFormat("IF処理終了します。RequestID={0}", t.RequestID);
			}
			catch (Exception ex)
			{
				string errMessage = MiscUtils.BuildErrMessage(ex, null);
				_log.ErrorFormat("エラーが発生しました。{0}", errMessage);
				_log.Info("データベース更新処理を行います");
				UpdateDBException(t, cn);
				_log.InfoFormat("IF処理エラー終了します。RequestID={0}", t.RequestID);
			}
		}

		#region DB更新

		/// <summary>
		/// 正常更新時
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		private void UpdateDBNormal(ToReportInfo target, IDbConnection connection)
		{
			_log.Debug("UpdateDB開始します");
			ToReportInfoHelper.SetDataFromResponseMsg(target);
			ToReportInfoHelper.UpdateToDB(target, connection);
			_log.Debug("UpdateDB終了しました");
		}

		/// <summary>
		/// 例外時DB更新
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		private void UpdateDBException(ToReportInfo target, IDbConnection connection)
		{
			_log.Debug("UpdateDBException開始します");
			ToReportInfoHelper.SetDataFromResponseMsgByException(target);
			ToReportInfoHelper.UpdateToDB(target, connection);
			_log.Debug("UpdateDBException開始します");
		}
		#endregion

		private void RegistExecutorFactory()
		{
			_executorFactory.Add(new OrderExecutor());
			_executorFactory.Add(new OrderCancelExecutor());
			_executorFactory.Add(new ReceiptExceutor());
			_executorFactory.Add(new ReceiptCancelExecutor());
			_executorFactory.Add(new ExamFinishExecutor());
		}

		private IExecutor GetExecutorByRequestType(string requestType)
		{
			return _executorFactory.FirstOrDefault(e => e.CanExecute(requestType));
		}

	}
}
