using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Exam
{
	/// <summary>
	/// 実施情報部のノード定義
	/// </summary>
	public class ExamNodeInfo
	{
		public static NodeInfo EXAM_ROOT = new NodeInfo("EXAM", "実施情報部", NodeTypeEnum.ntAggregate, -1);

		public static NodeInfo EXAM_PROCESSING_TYPE = new NodeInfo("PROCESSING_TYPE", "処理タイプ", NodeTypeEnum.ntData, 1);
		public static NodeInfo EXAM_PATIENT_NO = new NodeInfo("PATIENT_NO", "患者番号", NodeTypeEnum.ntData, 10);
		public static NodeInfo EXAM_EXAM_DATE = new NodeInfo("EXAM_DATE", "実施日", NodeTypeEnum.ntData, 8);
		public static NodeInfo EXAM_EXAM_TIME = new NodeInfo("EXAM_TIME", "実施時間", NodeTypeEnum.ntData, 4);
		public static NodeInfo EXAM_EXAM_COMMENT = new NodeInfo("EXAM_COMMENT", "実施コメント", NodeTypeEnum.ntData, 256);
		public static NodeInfo EXAM_SHOOT_ROOM = new NodeInfo("SHOOT_ROOM", "撮影室コード", NodeTypeEnum.ntData, 3);
		public static NodeInfo EXAM_EXAMINATOR_CODE = new NodeInfo("EXAMINATOR_CODE", "実施者コード", NodeTypeEnum.ntData, 30);
		public static NodeInfo EXAM_ENGINEER_COMMENT = new NodeInfo("ENGINEER_COMMENT", "技師コメント", NodeTypeEnum.ntData, 100);
		public static NodeInfo EXAM_NURSE_COMMENT = new NodeInfo("NURSE_COMMENT", "看護士コメント", NodeTypeEnum.ntData, 100);
		public static NodeInfo EXAM_EXAM_DETAIL_SUMM = new NodeInfo("EXAM_DETAIL_SUMM", "明細行繰返し回数", NodeTypeEnum.ntData, 3);
		public static NodeInfo EXAM_REMARK_CODE = new NodeInfo("REMARK_CODE", "所見コード", NodeTypeEnum.ntData, 6);
		public static NodeInfo EXAM_RESERVE1 = new NodeInfo("RESERVE1", "予備", NodeTypeEnum.ntData, 4);
		#region 明細
		//配列型だが電文中に存在しない固定入れなのでFieldLengthは0とする
		public static NodeInfo EXAM_EXAME_DETAIL_DYNAMIC_SUMM = new NodeInfo("DETAIL_DYNAMIC_SUMM", "属性数", NodeTypeEnum.ntArray, 0);

		public static NodeInfo EXAM_EXAM_DETAIL_AGG = new NodeInfo("EXAM_DETAIL_LIST", "明細行リスト", NodeTypeEnum.ntAggregate, -1);
		public static NodeInfo EXAM_ITEM_KIND = new NodeInfo("ITEM_KIND", "項目区分", NodeTypeEnum.ntData, 2);
		public static NodeInfo EXAM_COMMENT_KIND = new NodeInfo("COMMENT_KIND", "コメント区分", NodeTypeEnum.ntData, 2);
		public static NodeInfo EXAM_ITEM_CODE = new NodeInfo("ITEM_CODE", "項目コード", NodeTypeEnum.ntData, 6);
		public static NodeInfo EXAM_COMMENT_TEXT = new NodeInfo("COMMENT_TEXT", "コメントテキスト", NodeTypeEnum.ntData, 50);
		public static NodeInfo EXAM_FIGURE = new NodeInfo("FIGURE", "数量", NodeTypeEnum.ntData, 7);
		public static NodeInfo EXAM_LOSS = new NodeInfo("LOSS", "ロス", NodeTypeEnum.ntData, 7);
		public static NodeInfo EXAM_UNIT = new NodeInfo("UNIT", "単位", NodeTypeEnum.ntData, 2);
		public static NodeInfo EXAM_PARTITION_NUMBER = new NodeInfo("PARTITION_NUMBER", "分割数", NodeTypeEnum.ntData, 2);
		public static NodeInfo EXAM_SOME_CANCEL = new NodeInfo("SOME_CANCEL", "部分中止", NodeTypeEnum.ntData, 1);
		public static NodeInfo EXAM_RESERVE2 = new NodeInfo("RESERVE2", "予備", NodeTypeEnum.ntData, 9);
		#endregion
		public static NodeInfo EXAM_RESERVE3 = new NodeInfo("RESERVE3", "予備", NodeTypeEnum.ntData, 50);
	}
}
