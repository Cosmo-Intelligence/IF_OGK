using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Exam.Detail
{
	/// <summary>
	/// オーダ情報部繰り返し明細部
	/// </summary>
	public class ExamDetailAggregate : AggregateNode
	{
		#region property
		/// <summary>
		/// 項目区分
		/// </summary>
		public DataNode ITEM_KIND
		{
			get;
			set;
		}

		/// <summary>
		/// コメント区分
		/// </summary>
		public DataNode COMMENT_KIND
		{
			get;
			set;
		}

		/// <summary>
		/// 項目コード
		/// </summary>
		public DataNode ITEM_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// コメントテキスト
		/// </summary>
		public DataNode COMMENT_TEXT
		{
			get;
			set;
		}

		/// <summary>
		/// 数量
		/// </summary>
		public DataNode FIGURE
		{
			get;
			set;
		}

		/// <summary>
		/// ロス
		/// </summary>
		public DataNode LOSS
		{
			get;
			set;
		}

		/// <summary>
		/// 単位
		/// </summary>
		public DataNode UNIT
		{
			get;
			set;
		}

		/// <summary>
		/// 分割数
		/// </summary>
		public DataNode PARTITION_NUMBER
		{
			get;
			set;
		}

		/// <summary>
		/// 部分中止
		/// </summary>
		public DataNode SOME_CANCEL
		{
			get;
			set;
		}

		/// <summary>
		/// 予備2
		/// </summary>
		public DataNode RESERVE2
		{
			get;
			set;
		}

		#endregion	

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ExamDetailAggregate()
			: base(ExamNodeInfo.EXAM_EXAM_DETAIL_AGG)
		{
			ITEM_KIND = AddChildNode(new DataNode(ExamNodeInfo.EXAM_ITEM_KIND));
			COMMENT_KIND = AddChildNode(new DataNode(ExamNodeInfo.EXAM_COMMENT_KIND));
			ITEM_CODE = AddChildNode(new DataNode(ExamNodeInfo.EXAM_ITEM_CODE));
			COMMENT_TEXT = AddChildNode(new DataNode(ExamNodeInfo.EXAM_COMMENT_TEXT));
			FIGURE = AddChildNode(new DataNode(ExamNodeInfo.EXAM_FIGURE));
			LOSS = AddChildNode(new DataNode(ExamNodeInfo.EXAM_LOSS));
			UNIT = AddChildNode(new DataNode(ExamNodeInfo.EXAM_UNIT));
			PARTITION_NUMBER = AddChildNode(new DataNode(ExamNodeInfo.EXAM_PARTITION_NUMBER));
			SOME_CANCEL = AddChildNode(new DataNode(ExamNodeInfo.EXAM_SOME_CANCEL));
			RESERVE2 = AddChildNode(new DataNode(ExamNodeInfo.EXAM_RESERVE2));
		}

	}
}
