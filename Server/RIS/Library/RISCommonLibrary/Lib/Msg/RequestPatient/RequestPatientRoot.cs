using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace RISCommonLibrary.Lib.Msg.RequestPatient
{
	/// <summary>
	/// 患者情報要求メッセージルート
	/// </summary>
	public class RequestPatientRoot : CommonRootNode, IPatientAttribute
	{
		#region field
		
		#endregion

		#region property

		/// <summary>
		/// 患者属性部
		/// </summary>
		public PatientAttributeAggregate PatientAttribute
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RequestPatientRoot()
			: base(RequestPatientRootNodeInfo.REQUEST_PATIENT_ROOT)
		{
			PatientAttribute = new PatientAttributeAggregate();
			Add(PatientAttribute);
		}
		#endregion

	}
}
