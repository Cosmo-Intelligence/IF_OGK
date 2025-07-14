using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using ARISSend.Data;
using ARISSend.Model.MessageCreator;
using ARISSend.Updater;
using RISBizLibrary.Data;
using RISBizLibrary.Utils;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.ConnectionOpen;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Msg.ResponseWithPatient;
using RISCommonLibrary.Lib.Utils;
using System.Data;

namespace ARISSend.Model
{
	internal class ToHisInfoSender : IDisposable
    {
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// 古いログを削除するクラス
		/// </summary>
		private DeleteOldLogHelper _deleteOldLogHelper = new DeleteOldLogHelper();

		/// <summary>
		/// メッセージ作成クラスファクトリ
		/// </summary>
		private Dictionary<string, MessageCreator.IMessageCreator> _msgCreatorFactory;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ToHisInfoSender()
		{
			_msgCreatorFactory = new Dictionary<string, IMessageCreator>();
			SetDataMessageCreatorFactory(_msgCreatorFactory);
		}

		/// <summary>
		/// ソケット接続
		/// </summary>
		/// <param name="requestType"></param>
		public void ConnectTcp(string requestType)
		{
			IMessageCreator messageCreator = GetMessageCreator(requestType);
			if (messageCreator == null)
			{
				throw new RequestTypeNotDefineException(string.Format("未定義のREQUESTTYPEです={0}", requestType));
			}
			messageCreator.ConnectTcp();
		}

		/// <summary>
		/// ソケット切断
		/// </summary>
		/// <param name="requestType"></param>
		public void DisConnectTcp(string requestType)
		{
			IMessageCreator messageCreator = GetMessageCreator(requestType);
			if (messageCreator == null)
			{
				throw new RequestTypeNotDefineException(string.Format("未定義のREQUESTTYPEです={0}", requestType));
			}
			messageCreator.DisConnectTcp();
		}

		/// <summary>
		/// 接続要求電文
		/// </summary>
		public void RequestOpen(string requestType)
		{
			IMessageCreator messageCreator = GetMessageCreator(requestType);
			if (messageCreator == null)
			{
				throw new RequestTypeNotDefineException(string.Format("未定義のREQUESTTYPEです={0}", requestType));
			}
			messageCreator.RequestOpen();
		}

		/// <summary>
		/// 切断要求電文
		/// </summary>
		/// <param name="requestType"></param>
		public void RequestClose(string requestType)
		{
			IMessageCreator messageCreator = GetMessageCreator(requestType);
			if (messageCreator == null)
			{
				throw new RequestTypeNotDefineException(string.Format("未定義のREQUESTTYPEです={0}", requestType));
			}
			messageCreator.RequestClose();
		}

		/// <summary>
		/// 送信
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		public void Send(ToHisInfo target, IDbConnection connection)
		{
			BaseMsgCreator messageCreator = null;
			BaseSendMsgData msgData = null;
			_log.InfoFormat("IF処理開始します。RequestID={0}", target.RequestID);
			try
			{
				_log.Debug("古いログファイルを削除します");
				_deleteOldLogHelper.DeleteOldLog();

				_log.Debug("電文作成者を作成します");
				messageCreator = GetMessageCreator(target.RequestType) as BaseMsgCreator;
				if (messageCreator == null)
				{
					throw new RequestTypeNotDefineException(string.Format("未定義のREQUESTTYPEです={0}", target.RequestType));
				}

				_log.InfoFormat("電文を作成します。種別={0}", target.RequestType);
				msgData = messageCreator.CreateMsgData() as BaseSendMsgData;
				BaseMsg msg = messageCreator.CreateMsg(target, connection);
				msgData.Request = msg;

				_log.Info("送信処理を行います");
				SendInner(target, connection, messageCreator, msgData);

				_log.Info("データベース更新処理を行います");
				UpdateDBNormal(target, connection, msgData);

				_log.InfoFormat("IF処理終了します。RequestID={0}", target.RequestID);
			}
			catch (RequestTypeNotDefineException eRequest) //該当のTOHISINFOのRequestTypeが未定義
			{
				_log.Error(eRequest.Message);
				_log.Info("データベース更新処理を行います");
				UpdateDBRequestTypeNotDefine(target, connection);
				_log.InfoFormat("IF処理エラー終了します。RequestID={0}", target.RequestID);
			}
			catch (NodeOparationException eNode) //電文組み立て時エラー
			{
				BaseNode n = eNode.Node;
				_log.Error(GetErrMessageForNodeErr(eNode));
				_log.Info("データベース更新処理を行います");
				UpdateDBNodeOparation(target, connection, msgData);
				_log.InfoFormat("IF処理エラー終了します。RequestID={0}", target.RequestID);
			}
			catch (Exception ex)
			{
				string errMessage = MiscUtils.BuildErrMessage(ex, null);
				_log.ErrorFormat("エラーが発生しました。{0}", errMessage);
				_log.Info("データベース更新処理を行います");
				UpdateDBException(target, connection, msgData);
				DisconectBySysException(ex, messageCreator); //システム例外はソケット切断する
				_log.InfoFormat("IF処理エラー終了します。RequestID={0}", target.RequestID);
			}
		}

		/// <summary>
		/// 送信
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		public void SendInner(ToHisInfo target, IDbConnection connection,
				BaseMsgCreator messageCreator, BaseSendMsgData msgData)
		{
			int sendRetryCount = ConfigurationManager.AppSettings["SendRetryCount"].StringToInt32();

			int retryCount = 0;
			while (true)
			{
				try
				{
					#region 再接続処理
					if (!messageCreator.Connected)
					{
						messageCreator.ConnectTcp();
						BaseMsgData openData = messageCreator.RequestOpen();
						if (!CheckResponseWithRetry(openData.Response, messageCreator, sendRetryCount, 
							ref retryCount))
						{
							continue;
						}
					}
					#endregion

					messageCreator.SendSocket(msgData);
					if (!CheckResponseWithRetry(msgData.Response, messageCreator, sendRetryCount, 
						ref retryCount))
					{
						continue;
					}
					//正常終了
					return;
				}
				catch (RISIfSocketException eRISSocket)
				{
					string errMessage = MiscUtils.BuildErrMessage(eRISSocket, null);
					_log.ErrorFormat("ソケットエラーが発生しました。{0}", errMessage);

					retryCount = IncRetryCountAndDisConnect(retryCount, messageCreator);
					if (sendRetryCount < retryCount)
					{
						throw;
					}
					_log.WarnFormat("リトライを行います。{0}回目", retryCount);
				}
				catch (IOException eIO) 
				{
					if (!(eIO.InnerException is SocketException))
					{
						throw;
					}
					string errMessage = MiscUtils.BuildErrMessage(eIO, null);
					_log.ErrorFormat("IOエラーが発生しました。{0}", errMessage);
		
					retryCount = IncRetryCountAndDisConnect(retryCount, messageCreator);
					if (sendRetryCount < retryCount)
					{
						throw;
					}
					_log.WarnFormat("リトライを行います。{0}回目", retryCount);
				}
				catch (SocketException eSocket)
				{
					string errMessage = MiscUtils.BuildErrMessage(eSocket, null);
					_log.ErrorFormat("ソケットエラーが発生しました。{0}", errMessage);

					retryCount = IncRetryCountAndDisConnect(retryCount, messageCreator);
					if (sendRetryCount < retryCount)
					{
						throw;
					}
					_log.WarnFormat("リトライを行います。{0}回目", retryCount);
				}
			}
		}

		/// <summary>
		/// 応答結果を評価してリトライ判定する
		/// </summary>
		/// <param name="responseMsg"></param>
		/// <param name="messageCreator"></param>
		/// <param name="sendRetryCount"></param>
		/// <param name="retryCount"></param>
		/// <returns>
		/// <c>true</c>リトライしない
		/// <c>false</c>リトライ
		/// </returns>
		private bool CheckResponseWithRetry(BaseResponseMsg responseMsg, BaseMsgCreator messageCreator, int sendRetryCount, ref int retryCount)
		{
			string telegraphKind = responseMsg.MsgBody.CommunicationControl.TELEGRAPH_KIND.TrimData;
			_log.DebugFormat("電文受信結果={0}", telegraphKind);
			if (telegraphKind == MsgConst.TELEGRAPH_KIND_RESPONSE_OK)
			{
				return true;
			}
			_log.ErrorFormat("応答電文受信結果がエラーでした。TELEGRAPH_KIND={0},ERR_STATUS={1}",
				telegraphKind, responseMsg.MsgBody.CommunicationControl.ERR_STATUS.TrimData);
			retryCount = IncRetryCountAndDisConnect(retryCount, messageCreator);
			if (sendRetryCount < retryCount)
			{
			    throw new RISIfTelegraphKindException("応答電文受信結果エラーリトライ回数オーバー");
			}
			_log.WarnFormat("リトライを行います。{0}回目", retryCount);
			return false;
		}

		/// <summary>
		/// リトライカウント増加と切断処理
		/// </summary>
		/// <param name="currentRetryCount"></param>
		/// <param name="messageCreator"></param>
		/// <returns></returns>
		private int IncRetryCountAndDisConnect(int currentRetryCount, BaseMsgCreator messageCreator)
		{
			messageCreator.DisConnectTcp(); //切断する

			int retrySleep = ConfigurationManager.AppSettings["SendRetrySleep"].StringToInt32();
			_log.DebugFormat("リトライスリープします={0}", retrySleep);
			Thread.Sleep(retrySleep);

			return ++currentRetryCount;
		}

		/// <summary>
		/// TOHISINFO.RequestTypeによって電文作成クラスを返す
		/// </summary>
		/// <param name="requestType"></param>
		/// <returns></returns>
		private IMessageCreator GetMessageCreator(string requestType)
		{
			if (!_msgCreatorFactory.ContainsKey(requestType))
			{
				throw new RequestTypeNotDefineException(String.Format(
					"ToHisInfoテーブルのRequestTypeに想定していない値が設定されています={0}", requestType));
			}
			return _msgCreatorFactory[requestType];
		}

		/// <summary>
		/// メッセージファクトリに登録
		/// </summary>
		/// <param name="factory"></param>
		private void SetDataMessageCreatorFactory(Dictionary<string, MessageCreator.IMessageCreator> factory)
		{
			IMessageCreator examCreator = new ExamOrderMsgCreator();
			foreach (string requestType in examCreator.GetRequestTypes())
			{
				factory.Add(requestType, examCreator);
			}
			IMessageCreator receiptCreator = new ReceiptMsgCreator();
			foreach (string requestType in receiptCreator.GetRequestTypes())
			{
				factory.Add(requestType, receiptCreator);
			}
			IMessageCreator patientCreator = new PatientMsgCreator();
			factory.Add(patientCreator.GetRequestTypes()[0], patientCreator);
		}

		/// <summary>
		/// 接続要求
		/// </summary>
		/// <returns></returns>
		private BaseMsgData CreateMsgDataConnectionOpen()
		{
			BaseMsgData msgData = new ConnectionMsgData();
			ConnectionOpenMsg msg = new ConnectionOpenMsg();
			msg.MsgBody.CommunicationControl.CreateForOpen();
			msgData.Request = msg;
			return msgData;
		}

		/// <summary>
		/// 切断要求
		/// </summary>
		/// <returns></returns>
		private BaseMsgData CreateMsgDataConnectionClose()
		{
			BaseMsgData msgData = new ConnectionMsgData();
			ConnectionOpenMsg msg = new ConnectionOpenMsg();
			msg.MsgBody.CommunicationControl.CreateForClose();
			msgData.Request = msg;
			return msgData;
		}

		#region DB更新
		
		/// <summary>
		/// 正常更新時
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		/// <param name="msgData"></param>
		private void UpdateDBNormal(ToHisInfo target, IDbConnection connection, BaseSendMsgData msgData)
		{
			_log.Debug("UpdateDB開始します");
			//トランザクションは別にするらしい
			if ((msgData.Response is ResponseWithPatientMsg) &&
				(msgData.Response.MsgBody.CommunicationControl.TELEGRAPH_KIND.TrimData == 
					MsgConst.TELEGRAPH_KIND_RESPONSE_OK))
			{
				PatientUpdater updater = new PatientUpdater();
				updater.InsertOrUpdate(msgData.Response, connection, null);
			}

			ToHisInfoHelper.SetDataFromResponseMsg(target, msgData);
			ToHisInfoHelper.UpdateToDB(target, connection, msgData);
			_log.Debug("UpdateDB終了しました");
		}

		/// <summary>
		/// TOHISINFO.RequestType未定義エラー更新時
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		private void UpdateDBRequestTypeNotDefine(ToHisInfo target, IDbConnection connection)
		{
			_log.Debug("UpdateDBRequestTypeNotDefine開始します");
			ToHisInfoHelper.SetDataFromResponseMsgByRequestTypeNotDefine(target);
			ToHisInfoHelper.UpdateToDBRequestTypeNotDefine(target, connection);
			_log.Debug("UpdateDBRequestTypeNotDefine終了しました");
		}

		/// <summary>
		/// 電文組み立てエラー更新時
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		/// <param name="msgData"></param>
		private void UpdateDBNodeOparation(ToHisInfo target, IDbConnection connection, BaseSendMsgData msgData)
		{
			_log.Debug("UpdateDBNodeOparation開始します");
			ToHisInfoHelper.SetDataFromResponseMsg(target, msgData);
			ToHisInfoHelper.UpdateToDB(target, connection, msgData);
			_log.Debug("UpdateDBNodeOparation開始します");
		}

		/// <summary>
		/// 想定外例外時DB更新
		/// </summary>
		/// <param name="target"></param>
		/// <param name="connection"></param>
		/// <param name="msgData"></param>
		private void UpdateDBException(ToHisInfo target, IDbConnection connection, BaseSendMsgData msgData)
		{
			_log.Debug("UpdateDBException開始します");
			ToHisInfoHelper.SetDataFromResponseMsgByException(target, msgData);
			ToHisInfoHelper.UpdateToDB(target, connection, msgData);
			_log.Debug("UpdateDBException開始します");
		}
		#endregion

		/// <summary>
		/// 電文組み立てエラーメッセージ取得
		/// </summary>
		/// <param name="eNode"></param>
		/// <returns></returns>
		private string GetErrMessageForNodeErr(NodeOparationException eNode)
		{
			BaseNode n = eNode.Node;
			if (n != null)
			{
				return string.Format("メッセージフォーマットエラーです。{0},StartPos={1},Name={2},Path={3},Size={4}", eNode.Message,
					n.GetStartPos(), n.NameJ, n.Path, n.Size);
			}
			return string.Format("メッセージフォーマットエラーです。{0", eNode.Message);
		}

		/// <summary>
		/// システム例外のときにソケット接続を切断する
		/// </summary>
		/// <param name="e"></param>
		/// <param name="messageCreator"></param>
		private void DisconectBySysException(Exception e, BaseMsgCreator messageCreator)
		{
			if (e is RISIfExceptionUserException) //ユーザ例外はソケット切断しない
			{
				return;
			}
			if (messageCreator == null)
			{
				return;
			}
			_log.Debug("ソケット切断処理を行います");
			messageCreator.DisConnectTcp();
		}

		#region IDisposable メンバ

		public void Dispose()
		{
			if (_msgCreatorFactory == null)
			{
				return;
			}
			#region ディクショナリの値コレクションから重複しているMessageCreatorを取り除いたリストを作成
			Dictionary<string, IMessageCreator>.ValueCollection vc = _msgCreatorFactory.Values;
			IEnumerable<IMessageCreator> messageCreatorList = from mc in vc
															  group mc by mc into mcGroup
																  select mcGroup.First();
			#endregion
			foreach (IMessageCreator item in messageCreatorList)
			{
				BaseMsgCreator creator = item as BaseMsgCreator;
				if (creator == null)
				{
					continue;
				}
				try
				{
					if (creator.Connected)
					{
						creator.RequestClose();
					}
				}
				catch (Exception e)
				{ //例外発生してもここでは握りつぶしておく
					string errMessage = MiscUtils.BuildErrMessage(e, null);
					_log.ErrorFormat("終了処理でエラーが発生しました。{0}", errMessage);
				}
				creator.Dispose();
			}
		}

		#endregion
	}
}
