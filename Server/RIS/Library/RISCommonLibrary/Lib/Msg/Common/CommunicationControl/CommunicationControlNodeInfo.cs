using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.CommunicationControl
{
	/// <summary>
	/// 通信制御部のノード定義
	/// </summary>
	public class CommunicationControlNodeInfo
	{
		public static NodeInfo COMMUNICATION_CONTROL_ROOT = new NodeInfo("COMMUNICATION_CONTROL", "通信制御部", NodeTypeEnum.ntAggregate, -1);

		public static NodeInfo COMMUNICATION_CONTROL_DATA_LENGTH = new NodeInfo("DATA_LENGTH", "データ長", NodeTypeEnum.ntData, 5);
		public static NodeInfo COMMUNICATION_CONTROL_TELEGRAPH_KIND = new NodeInfo("TELEGRAPH_KIND", "電文種別", NodeTypeEnum.ntData, 2);
		public static NodeInfo COMMUNICATION_CONTROL_CONTINUANCE_FLAG = new NodeInfo("CONTINUANCE_FLAG", "継続フラグ", NodeTypeEnum.ntData, 1);
		public static NodeInfo COMMUNICATION_CONTROL_SEND_DATE = new NodeInfo("SEND_DATE", "送信日付", NodeTypeEnum.ntData, 8);
		public static NodeInfo COMMUNICATION_CONTROL_SEND_TIME = new NodeInfo("SEND_TIME", "送信時間", NodeTypeEnum.ntData, 6);
		public static NodeInfo COMMUNICATION_CONTROL_ERR_STATUS = new NodeInfo("ERR_STATUS", "エラーステータス", NodeTypeEnum.ntData, 2);
		public static NodeInfo COMMUNICATION_CONTROL_ORDER_NO = new NodeInfo("ORDER_NO", "オーダ番号", NodeTypeEnum.ntData, 18);
		public static NodeInfo COMMUNICATION_CONTROL_RESERVE = new NodeInfo("RESERVE", "予備", NodeTypeEnum.ntData, 10);
	}
}
