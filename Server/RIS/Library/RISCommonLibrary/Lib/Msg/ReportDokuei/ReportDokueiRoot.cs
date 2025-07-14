using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.ReportDokuei;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace RISCommonLibrary.Lib.Msg.ReportDokuei
{
	/// <summary>
	/// 受付情報メッセージルート
	/// </summary>
	public class ReportDokueiRoot : CommonRootNode, IReportDokuei
	{
		#region field
		
		#endregion

		#region property
		
		/// <summary>
		/// 患者属性部
		/// </summary>
		public ReportDokueiAggregate ReportDokuei
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportDokueiRoot()
			: base(ReportDokueiRootNodeInfo.REPORTDOKUEI_ROOT)
		{

			ReportDokuei = new ReportDokueiAggregate();
			Add(ReportDokuei);
		}
		#endregion

	}
}
