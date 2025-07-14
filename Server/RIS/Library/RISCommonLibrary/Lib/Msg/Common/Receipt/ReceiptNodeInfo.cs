using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Receipt
{
	/// <summary>
	/// 受付(進捗)情報部のノード定義
	/// </summary>
	class ReceiptNodeInfo
	{
		public static NodeInfo RECEIPT_ROOT = new NodeInfo("RECEIPT", "受付(進捗)情報部", NodeTypeEnum.ntAggregate, -1);

		public static NodeInfo RECEIPT_PROCESSING_TYPE = new NodeInfo("PROCESSING_TYPE", "処理タイプ", NodeTypeEnum.ntData, 2);
		public static NodeInfo RECEIPT_PATIENT_NO = new NodeInfo("PATIENT_NO", "患者番号", NodeTypeEnum.ntData, 10);
		public static NodeInfo RECEIPT_RECEIPT_DATE = new NodeInfo("RECEIPT_DATE", "受付日", NodeTypeEnum.ntData, 8);
		public static NodeInfo RECEIPT_RECEIPT_TIME = new NodeInfo("RECEIPT_TIME", "受付時間", NodeTypeEnum.ntData, 4);
		public static NodeInfo RECEIPT_RECEIPTOR_CODE = new NodeInfo("RECEIPTOR_CODE", "受付者コード", NodeTypeEnum.ntData, 6);
		public static NodeInfo RECEIPT_RESERVE = new NodeInfo("RESERVE", "予備", NodeTypeEnum.ntData, 10);
	}
}
