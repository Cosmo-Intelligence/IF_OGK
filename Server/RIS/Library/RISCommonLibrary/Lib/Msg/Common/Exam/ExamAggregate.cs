using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.Exam.Detail;

namespace RISCommonLibrary.Lib.Msg.Common.Exam
{
	/// <summary>
	/// オーダ情報部
	/// </summary>
	public class ExamAggregate : AggregateNode
	{
		#region property

		/// <summary>
		/// 処理タイプ
		/// </summary>
		public DataNode PROCESSING_TYPE
		{
			get;
			set;
		}

		/// <summary>
		/// 患者番号
		/// </summary>
		public DataNode PATIENT_NO
		{
			get;
			set;
		}

		/// <summary>
		/// 実施日
		/// </summary>
		public DataNode EXAM_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// 実施時間
		/// </summary>
		public DataNode EXAM_TIME
		{
			get;
			set;
		}

		/// <summary>
		/// 実施コメント
		/// </summary>
		public DataNode EXAM_COMMENT
		{
			get;
			set;
		}

		/// <summary>
		/// 撮影室コード
		/// </summary>
		public DataNode SHOOT_ROOM
		{
			get;
			set;
		}

		/// <summary>
		/// 実施者コード
		/// </summary>
		public DataNode EXAMINATOR_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 技師コメント
		/// </summary>
		public DataNode ENGINEER_COMMENT
		{
			get;
			set;
		}

		/// <summary>
		/// 看護士コメント
		/// </summary>
		public DataNode NURSE_COMMENT
		{
			get;
			set;
		}

		/// <summary>
		/// 明細行繰返し回数
		/// </summary>
		public DataNode EXAM_DETAIL_SUMM
		{
			get;
			set;
		}

		/// <summary>
		/// 所見コード
		/// </summary>
		public DataNode REMARK_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 予備1
		/// </summary>
		public DataNode RESERVE1
		{
			get;
			set;
		}

		/// <summary>
		/// 明細行繰返しクラス
		/// </summary>
		public ExamDetailDynamicArray DETAIL_DYNAMIC_SUMM
		{
			get;
			set;
		}

		/// <summary>
		/// 予備3
		/// </summary>
		public DataNode RESERVE3
		{
			get;
			set;
		}

		
		#endregion

		#region constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ExamAggregate()
			: base(ExamNodeInfo.EXAM_ROOT)
		{
			PROCESSING_TYPE = AddChildNode(new DataNode(ExamNodeInfo.EXAM_PROCESSING_TYPE));
			PATIENT_NO = AddChildNode(new DataNode(ExamNodeInfo.EXAM_PATIENT_NO));
			EXAM_DATE = AddChildNode(new DataNode(ExamNodeInfo.EXAM_EXAM_DATE));
			EXAM_TIME = AddChildNode(new DataNode(ExamNodeInfo.EXAM_EXAM_TIME));
			EXAM_COMMENT = AddChildNode(new DataNode(ExamNodeInfo.EXAM_EXAM_COMMENT));
			SHOOT_ROOM = AddChildNode(new DataNode(ExamNodeInfo.EXAM_SHOOT_ROOM));
			EXAMINATOR_CODE = AddChildNode(new DataNode(ExamNodeInfo.EXAM_EXAMINATOR_CODE));
			ENGINEER_COMMENT = AddChildNode(new DataNode(ExamNodeInfo.EXAM_ENGINEER_COMMENT));
			NURSE_COMMENT = AddChildNode(new DataNode(ExamNodeInfo.EXAM_NURSE_COMMENT));
			EXAM_DETAIL_SUMM = AddChildNode(new DataNodeFormat(ExamNodeInfo.EXAM_EXAM_DETAIL_SUMM));
			REMARK_CODE = AddChildNode(new DataNode(ExamNodeInfo.EXAM_REMARK_CODE));
			RESERVE1 = AddChildNode(new DataNode(ExamNodeInfo.EXAM_RESERVE1));
			DETAIL_DYNAMIC_SUMM = new ExamDetailDynamicArray();
			DETAIL_DYNAMIC_SUMM.EXAM_DETAIL_SUMM = EXAM_DETAIL_SUMM;
			//イベント実装中止
			//DETAIL_DYNAMIC_SUMM.OnAdd += AddEventHandler;
			Add(DETAIL_DYNAMIC_SUMM);
			RESERVE3 = AddChildNode(new DataNode(ExamNodeInfo.EXAM_RESERVE3));
		}
		#endregion

		#region method

		private void AddEventHandler(object sender, AddEventArgs aea)
		{
			EXAM_DETAIL_SUMM.Data = aea.ChangedData;
		}

		#endregion

	}
}
