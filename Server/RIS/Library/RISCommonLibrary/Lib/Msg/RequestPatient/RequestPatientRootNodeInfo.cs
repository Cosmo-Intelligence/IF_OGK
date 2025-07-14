using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.RequestPatient
{
	/// <summary>
	/// 患者情報要求のノード定義
	/// </summary>
	class RequestPatientRootNodeInfo
	{
		public static NodeInfo REQUEST_PATIENT_ROOT = new NodeInfo("REQUEST_PATIENT_ROOT", "患者情報要求", NodeTypeEnum.ntAggregate, -1);
	}
}
