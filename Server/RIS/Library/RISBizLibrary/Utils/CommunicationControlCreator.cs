using System;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;

namespace RISBizLibrary.Utils
{
	/// <summary>
	/// 通信制御部作成クラス
	/// </summary>
	public static class CommunicationControlCreator
	{
		/// <summary>
		/// 通信制御部作成
		/// </summary>
		/// <param name="agg"></param>
		/// <param name="errStatus"></param>
		public static void CreateCommunicationControlAgg(this CommunicationControlAggregate agg)
		{
			agg.CONTINUANCE_FLAG.Data = MsgConst.CONTINUANCE_FLAG_NOT_EXIST;
			agg.SEND_DATE.Data = DateTime.Now.ToString("yyyyMMdd");
			agg.SEND_TIME.Data = DateTime.Now.ToString("HHmmss");
		}

		#region 要求
		/// <summary>
		/// 接続要求通信制御部作成
		/// </summary>
		/// <param name="agg"></param>
		/// <param name="errStatus"></param>
		public static void CreateForOpen(this CommunicationControlAggregate agg)
		{
			CreateCommunicationControlAgg(agg);
			agg.TELEGRAPH_KIND.Data = MsgConst.TELEGRAPH_KIND_REQUEST_OPEN;
			agg.ERR_STATUS.Data = MsgConst.ERR_STATUS_NORMAL;
		}

		/// <summary>
		/// 切断要求通信制御部作成
		/// </summary>
		/// <param name="agg"></param>
		/// <param name="errStatus"></param>
		public static void CreateForClose(this CommunicationControlAggregate agg)
		{
			CreateCommunicationControlAgg(agg);
			agg.TELEGRAPH_KIND.Data = MsgConst.TELEGRAPH_KIND_REQUEST_CLOSE;
			agg.ERR_STATUS.Data = MsgConst.ERR_STATUS_NORMAL;
		}
		#endregion

		#region 応答

		/// <summary>
		/// ステータスによる通信制御部作成
		/// </summary>
		/// <param name="agg"></param>
		/// <param name="status"></param>
		public static void CreateByStatus(this CommunicationControlAggregate agg, string status)
		{
			CreateCommunicationControlAgg(agg);
			agg.TELEGRAPH_KIND.Data = GetTELEGRAPH_KINDByERR_STATUS(status);
			agg.ERR_STATUS.Data = status;
		}

		/// <summary>
		/// ステータスによる通信制御部作成
		/// </summary>
		/// <param name="agg"></param>
		/// <param name="status"></param>
		/// <param name="ORDER_NO"></param>
		public static void CreateByStatus(this CommunicationControlAggregate agg,
			string status, string ORDER_NO)
		{
			CreateByStatus(agg, status);
			agg.ORDER_NO.Data = ORDER_NO;
		}

		/// <summary>
		/// エラーステータスから電文種別を取得する
		/// </summary>
		/// <param name="errStatus"></param>
		/// <returns></returns>
		public static string GetTELEGRAPH_KINDByERR_STATUS(string errStatus)
		{
			if (errStatus == MsgConst.ERR_STATUS_NORMAL)
			{
				return MsgConst.TELEGRAPH_KIND_RESPONSE_OK;
			}
			return MsgConst.TELEGRAPH_KIND_RESPONSE_NG;
		}

		#endregion
	}
}
