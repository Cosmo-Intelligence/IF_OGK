using System;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.ConnectionClose;
using RISCommonLibrary.Lib.Msg.ConnectionOpen;
using RISCommonLibrary.Lib.Msg.Order;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Utils;
using RISBizLibrary.Utils;

namespace RISBizLibrary.Receive.Model
{
	/// <summary>
	/// メッセージを分配する
	/// </summary>
	public abstract class MessageHandler
	{
		#region field

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MessageHandler()
		{
			this.RegistFactory();
		}

		#endregion

		#region method
		
		/// <summary>
		/// 受信したメッセージを処理する
		/// </summary>
		/// <param name="receiveMsg"></param>
		/// <returns></returns>
		public string Execute(string receiveMsg)
		{
			BaseResponseMsg responseMsg;
			try
			{
				ValidateMsgCommon(receiveMsg);

				BaseMsg msg = MsgFactory.Instance.CreateMsgByTextMessage(receiveMsg);
				if (msg == null)
				{
					throw new MsgAnomalyException(string.Format(
						"このプログラムでは想定していない電文種別です。電文種別={0},電文長={1},オーダNo={2},処理タイプ={3}",
					MsgUtils.GetTelegraphKind(receiveMsg), MsgUtils.GetDataLength(receiveMsg),
					MsgUtils.GetOrderNo(receiveMsg), MsgUtils.GetProcessingType(receiveMsg)));
				}
				msg.TextMessage = receiveMsg;
				responseMsg = SetDBSetter(msg);
				responseMsg.MsgBody.CommunicationControl.CreateByStatus(MsgConst.ERR_STATUS_NORMAL,
					GetOrderNo(receiveMsg));
				return responseMsg.TextMessage;
			}
			catch (MsgHaveStatusException eStatus)
			{
				_log.Error(eStatus);
				_log.ErrorFormat("ErrStatus={0}", eStatus.ErrStatus);
				responseMsg = new ResponseMsg();
				responseMsg.MsgBody.CommunicationControl.CreateByStatus(eStatus.ErrStatus,
					GetOrderNo(receiveMsg));
				return responseMsg.TextMessage;
			}
			catch (Exception e)
			{
				_log.Error(e);
				responseMsg = new ResponseMsg();
				responseMsg.MsgBody.CommunicationControl.CreateByStatus(MsgConst.ERR_STATUS_ANOMALY, 
					GetOrderNo(receiveMsg));
				return responseMsg.TextMessage;
			}
		}

		/// <summary>
		/// DB設定クラス実行
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		protected abstract BaseResponseMsg SetDBSetter(BaseMsg msg);

		/// <summary>
		/// メッセージファクトリに登録する
		/// </summary>
		protected abstract void RegistFactory();

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
		/// 電文種別取得
		/// </summary>
		/// <param name="receiveMsg"></param>
		/// <returns></returns>
		public static string GetTELEGRAPH_KIND(string textMessage)
		{
			return MsgUtils.GetTelegraphKind(textMessage);
		}

		/// <summary>
		/// 受信メッセージからオーダNo部分を抜き取る
		/// </summary>
		/// <param name="receiveMsg"></param>
		/// <returns></returns>
		private string GetOrderNo(string receiveMsg)
		{
			return MsgUtils.GetOrderNo(receiveMsg);
		}

		#endregion

	}
}
