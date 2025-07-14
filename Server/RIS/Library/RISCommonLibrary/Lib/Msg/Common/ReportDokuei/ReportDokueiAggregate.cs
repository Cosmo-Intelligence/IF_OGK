using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.ReportDokuei
{
	/// <summary>
	/// 既読情報部
	/// </summary>
	public class ReportDokueiAggregate : AggregateNode
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
		/// 処理年月日
		/// </summary>
		public DataNode REPORTDOKUEI_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// 処理時刻
		/// </summary>
		public DataNode REPORTDOKUEI_TIME
		{
			get;
			set;
		}

		/// <summary>
		/// ユーザID
		/// </summary>
		public DataNode REPORTDOKUEI_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 予備
		/// </summary>
		public DataNode RESERVE
		{
			get;
			set;
		}
		
		#endregion

		#region constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportDokueiAggregate()
			: base(ReportDokueiNodeInfo.REPORTDOKUEI_ROOT)
		{
			PROCESSING_TYPE = AddChildNode(new DataNode(ReportDokueiNodeInfo.REPORTDOKUEI_PROCESSING_TYPE));
			PATIENT_NO = AddChildNode(new DataNode(ReportDokueiNodeInfo.REPORTDOKUEI_PATIENT_NO));
			REPORTDOKUEI_DATE = AddChildNode(new DataNode(ReportDokueiNodeInfo.REPORTDOKUEI_DATE));
			REPORTDOKUEI_TIME = AddChildNode(new DataNode(ReportDokueiNodeInfo.REPORTDOKUEI_TIME));
			REPORTDOKUEI_CODE = AddChildNode(new DataNode(ReportDokueiNodeInfo.REPORTDOKUEI_USERID));
			RESERVE = AddChildNode(new DataNode(ReportDokueiNodeInfo.REPORTDOKUEI_RESERVE));
		}
		#endregion
	}
}
