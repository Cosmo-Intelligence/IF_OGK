using System;
using System.Configuration;
using System.Text;
using ARISSend.Model.MessageCreator;
using RISBizLibrary.Data;
using RISCommonLibrary.Lib.Compornent;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.MessageLog;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Msg.ResponseWithPatient;
using RISCommonLibrary.Lib.Utils;

namespace ARISSend.Model
{
    internal class ReportDokueiInfoTcpClient: IDisposable
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private SimpleTcpClient _tcpClient;

		public bool Connected
		{
			get
			{
				if (_tcpClient == null)
				{
					return false;
				}
				return _tcpClient.Connected;
			}
		}

		#region コンストラクタ
		
		public ReportDokueiInfoTcpClient()
        {
			InitMessageLogger();
		}
		
		#endregion

		#region IDisposable メンバ

		public void Dispose()
        {
			DisConnectTcp();
		}

        #endregion

		public void SendRecv(BaseMsgData msgData)
        {
			if (!Connected)
			{
				throw new RISIfSocketException("接続が確立されていません");
			}
			if (_tcpClient.Available > 0)
			{
				string buffer = _tcpClient.Read();
				throw new RISIfSocketException(String.Format("送信前に不正なバッファが見つかりました。={0}", buffer));
			}
			_log.Debug("ソケット送信します");

			DateTime logDateTime = DateTime.Now; //電文ログ日時
			MessageLogger.Instance.UpdateCurrentDir(logDateTime);

			#region 送信電文ログ
			string sendString = msgData.Request.TextMessage;
			msgData.Request.ReflectOriginalMessage(); //オリジナルメッセージ設定
			String sendFileLogName = GetFileName(logDateTime,
				MsgUtils.GetTelegraphKind(sendString), "SEND");
			_log.InfoFormat("ソケット送信電文ログファイル名={0}", sendFileLogName);
			MessageLogger.Instance.WriteLog(sendFileLogName, sendString);
			#endregion

			_tcpClient.Send(sendString);
			_log.Debug("ソケット受信します");
			string receivedString = _tcpClient.Read();

			if (string.IsNullOrEmpty(receivedString))
			{
				throw new RISIfSocketException("受信結果が0バイトでした");
			}

			#region 受信電文ログ
			string recievedTelegraphKind = MsgUtils.GetTelegraphKind(receivedString); //電文種別取得
			string recvFileLogName = GetFileName(logDateTime, recievedTelegraphKind, "RECV");
			_log.InfoFormat("ソケット受信電文ログファイル名={0}", recvFileLogName);
			MessageLogger.Instance.WriteLog(recvFileLogName, receivedString);
			#endregion

			if (_tcpClient.Available > 0)
			{
				string buffer = _tcpClient.Read();
				throw new RISIfSocketException(String.Format("受信後に不正なバッファが見つかりました。={0}", buffer));
			}

			ValidateMsgCommon(receivedString);
			msgData.Response = CreateMsgByTextMessage(receivedString);
			msgData.Response.TextMessage = receivedString;
        }

		/// <summary>
		/// 電文共通チェック
		/// </summary>
		/// <remarks>
		/// 電文長エラーの場合は、MsgLengthExceptionが生成される
		/// </remarks>
		private void ValidateMsgCommon(string receiveMsg)
		{
			string trimedMsg = MsgUtils.TrimEnclosure(receiveMsg);
			string dataLengthString = MsgUtils.GetDataLength(trimedMsg);

			int dataLength;
			if (!int.TryParse(dataLengthString, out dataLength))
			{
				throw new MsgLengthException(string.Format("長さ設定が判別できませんでした。{0}", dataLengthString));
			}

			int msgLength = trimedMsg.GetSJISLength();
			if (msgLength != dataLength)
			{
				throw new MsgLengthException(string.Format(
					"長さ設定が実際の長さと異なっています。長さ設定={0},実際の長さ={1}", dataLength, msgLength));
			}
		}

		/// <summary>
		/// 電文ログファイル名取得
		/// </summary>
		/// <param name="logDateTime"></param>
		/// <param name="telegraphKind"></param>
		/// <param name="sendOrRecv"></param>
		/// <returns></returns>
		private String GetFileName(DateTime logDateTime, String telegraphKind, String sendOrRecv)
		{
			const String FORMAT_FILENAME = "{0:yyyyMMddHHmmssfff}_{1}_{2}.log";
			return String.Format(FORMAT_FILENAME, logDateTime, telegraphKind, sendOrRecv);
		}

		/// <summary>
		/// 電文ログクラス初期化
		/// </summary>
		private void InitMessageLogger()
		{
			MessageLogger.Instance.RootDir = ConfigurationManager.AppSettings["MessageLogRootDir"].StringToString();
			MessageLogger.Instance.TargetDirFormat = ConfigurationManager.AppSettings["MessageLogWriteDirDateFormat"].StringToString();
			MessageLogger.Instance.Enc = Encoding.GetEncoding(ConfigurationManager.AppSettings["MessageLogEncode"].StringToString());
		}

		/// <summary>
		/// 応答用の電文作成
		/// </summary>
		/// <param name="textMessage"></param>
		/// <returns></returns>
		public BaseResponseMsg CreateMsgByTextMessage(string textMessage)
		{
			//長すぎる場合は、""を返す
			string processingType = MsgUtils.GetProcessingType(textMessage);
			if (MsgConst.PROCESSING_TYPE_PATIENT == processingType)
			{
				return new ResponseWithPatientMsg();
			}
			return new ResponseMsg();
		}

		public void ConnectTcp(ISocketParameter socketParameter)
		{
			if (_tcpClient != null)
			{
				DisConnectTcp();
			}
			_tcpClient = new SimpleTcpClient();

			String host = socketParameter.Host;
			Int32 port = socketParameter.Port;

			Int32 connectionTimeout = socketParameter.ConnectionTimeout;
			Int32 sendTimeout = socketParameter.SendTimeout;
			Int32 receiveTimeout = socketParameter.ReceiveTimeout;

			_log.DebugFormat("ソケット接続します。Host={0}, Port={1}, ConnectionTimeout={2}, SendTimeout={3}, ReceiveTimeout={4}",
				host, port, connectionTimeout, sendTimeout, receiveTimeout);
			_tcpClient.Connect(host, port, connectionTimeout, sendTimeout, receiveTimeout);
		}

		public void DisConnectTcp()
		{
			_log.Debug("DisConnectTcpします");
			if (_tcpClient == null)
			{
				return;
			}
			_log.Debug("ソケット切断します");
			_tcpClient.Dispose();
			_tcpClient = null;
			_log.Debug("DisConnectTcpしました");
		}
	}
}
