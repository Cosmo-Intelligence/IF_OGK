using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace RISCommonLibrary.Lib.Msg.Hospitalize
{
	/// <summary>
	/// 入退院情報メッセージルート
	/// </summary>
	public class HospitalizeRoot : CommonRootNode, IPatientAttribute
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
		public HospitalizeRoot()
			: base(HospitalizeNodeInfo.HOSPITALIZE_ROOT)
		{

			PatientAttribute = new PatientAttributeAggregate();
			Add(PatientAttribute);
		}
		#endregion

	}
}
