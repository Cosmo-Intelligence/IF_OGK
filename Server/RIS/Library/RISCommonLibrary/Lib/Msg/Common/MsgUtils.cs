using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Utils;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace RISCommonLibrary.Lib.Msg.Common
{
	/// <summary>
	/// メッセージユーティリティ
	/// </summary>
	public class MsgUtils
	{
		/// <summary>
		/// 囲い文字を削除する
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string TrimEnclosure(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return "";
			}
			return s.Trim(new char[] { MsgConst.MSG_ENCLOSURE_START, 
					MsgConst.MSG_ENCLOSURE_END});
		}

		/// <summary>
		/// 前ゼロパディングするフォーマットを取得する
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string GetFormatZeroPading(int length)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{0:d");
			sb.Append(length.ToString());
			sb.Append("}");
			return sb.ToString();
		}

		/// <summary>
		/// 電文長
		/// </summary>
		/// <param name="trimSrc"></param>
		/// <returns></returns>
		public static string GetDataLength(string src)
		{
			string trimSrc = TrimEnclosure(src);
			return MBCSHelper.Copy(trimSrc, 1,
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_DATA_LENGTH.FieldLength);
		}

		/// <summary>
		/// 電文種別取得
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static string GetTelegraphKind(string src)
		{
			string trimSrc = TrimEnclosure(src);
			return MBCSHelper.Copy(trimSrc,
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_DATA_LENGTH.FieldLength + 1,
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_TELEGRAPH_KIND.FieldLength);
		}

		/// <summary>
		/// 処理タイプ取得
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static string GetProcessingType(string src)
		{
			//オーダ番号の開始位置
			int pos = 
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_DATA_LENGTH.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_TELEGRAPH_KIND.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_CONTINUANCE_FLAG.FieldLength + 
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_SEND_DATE.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_SEND_TIME.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ERR_STATUS.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ORDER_NO.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_RESERVE.FieldLength;

			string trimSrc = TrimEnclosure(src);
			return MBCSHelper.Copy(trimSrc, pos + 1,
				PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_PROCESSING_TYPE.FieldLength);
		}

		/// <summary>
		/// オーダNo
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static string GetOrderNo(string src)
		{
			//オーダ番号の開始位置
			int pos = 
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_DATA_LENGTH.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_TELEGRAPH_KIND.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_CONTINUANCE_FLAG.FieldLength + 
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_SEND_DATE.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_SEND_TIME.FieldLength +
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ERR_STATUS.FieldLength;

			string trimSrc = TrimEnclosure(src);
			return MBCSHelper.Copy(trimSrc, pos + 1,
				CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ORDER_NO.FieldLength);
		}

	}
}
