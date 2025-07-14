using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Order
{
	/// <summary>
	/// オーダ情報部のノード定義
	/// </summary>
	class OrderNodeInfo
	{
		public static NodeInfo ORDER_ROOT = new NodeInfo("ORDER", "オーダ情報部", NodeTypeEnum.ntAggregate, -1);

		public static NodeInfo ORDER_REQUEST_SECTION_CODE = new NodeInfo("REQUEST_SECTION_CODE", "依頼科コード", NodeTypeEnum.ntData, 2);
		public static NodeInfo ORDER_REQUEST_DOCTOR_CODE = new NodeInfo("REQUEST_DOCTOR_CODE", "依頼医師コード", NodeTypeEnum.ntData, 6);
		public static NodeInfo ORDER_REQUEST_DOCTOR_NAME = new NodeInfo("REQUEST_DOCTOR_NAME", "依頼医師名", NodeTypeEnum.ntData, 40);
		public static NodeInfo ORDER_ORDER_KIND = new NodeInfo("ORDER_KIND", "オーダ区分", NodeTypeEnum.ntData, 1);
		public static NodeInfo ORDER_ORDER_REGISTER_DATE = new NodeInfo("ORDER_REGISTER_DATE", "オーダ登録日", NodeTypeEnum.ntData, 8);
		public static NodeInfo ORDER_ORDER_REGISTER_TIME = new NodeInfo("ORDER_REGISTER_TIME", "オーダ登録時間", NodeTypeEnum.ntData, 4);
		public static NodeInfo ORDER_EXAM_START_SCHEDULED_DATE = new NodeInfo("EXAM_START_SCHEDULED_DATE", "実施開始予定日", NodeTypeEnum.ntData, 8);
		public static NodeInfo ORDER_EXAM_START_SCHEDULED_TIME = new NodeInfo("EXAM_START_SCHEDULED_TIME", "実施開始予定時間", NodeTypeEnum.ntData, 4);
		public static NodeInfo ORDER_EXAM_ROOM = new NodeInfo("EXAM_ROOM", "検査室", NodeTypeEnum.ntData, 3);
		public static NodeInfo ORDER_REMARK_NECESSITY = new NodeInfo("REMARK_NECESSITY", "所見要否", NodeTypeEnum.ntData, 1);
		public static NodeInfo ORDER_RESERVE1 = new NodeInfo("RESERVE1", "予備", NodeTypeEnum.ntData, 10);
		public static NodeInfo ORDER_EXAM_PURPOSE = new NodeInfo("EXAM_PURPOSE", "検査目的", NodeTypeEnum.ntData, 200);
		#region 明細
		public static NodeInfo ORDER_ORDER_DETAIL_SUMM = new NodeInfo("ORDER_DETAIL_SUMM", "オーダー明細行繰返し回数", NodeTypeEnum.ntArray, 3);
		public static NodeInfo ORDER_ORDER_DETAIL_LIST = new NodeInfo("ORDER_DETAIL_LIST", "オーダー明細行リスト", NodeTypeEnum.ntAggregate, -1);

		public static NodeInfo ORDER_ITEM_KIND = new NodeInfo("ITEM_KIND", "項目区分", NodeTypeEnum.ntData, 2);
		public static NodeInfo ORDER_COMMENT_KIND = new NodeInfo("COMMENT_KIND", "コメント区分", NodeTypeEnum.ntData, 2);
		public static NodeInfo ORDER_ITEM_CODE = new NodeInfo("ITEM_CODE", "項目コード", NodeTypeEnum.ntData, 6);
		public static NodeInfo ORDER_ACTING_COMMENT = new NodeInfo("ACTING_COMMENT", "行為コメント", NodeTypeEnum.ntData, 50);
		#endregion
		public static NodeInfo ORDER_RESERVE2 = new NodeInfo("RESERVE2", "予備", NodeTypeEnum.ntData, 50);
	}
}
