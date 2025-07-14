using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace RISCommonLibrary.Lib.Msg.ResponseWithPatient
{
	/// <summary>
	/// 応答電文患者情報メッセージルート
	/// </summary>
	public class ResponseWithPatientRoot : CommonRootNode, IPatientAttribute
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
		public ResponseWithPatientRoot()
			: base(ResponseWithPatientRootNodeInfo.RESPONSE_WITH_PATIENT_ROOT)
		{

			PatientAttribute = new PatientAttributeAggregate();
			Add(PatientAttribute);
		}
		#endregion
	}
}
