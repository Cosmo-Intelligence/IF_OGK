using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.ResponseWithPatient
{
	/// <summary>
	/// 応答電文患者情報付きのノード定義
	/// </summary>
	class ResponseWithPatientRootNodeInfo
	{
		public static NodeInfo RESPONSE_WITH_PATIENT_ROOT = new NodeInfo("RESPONSE_WITH_PATIENT_ROOT", "応答患者情報", NodeTypeEnum.ntAggregate, -1);
	}
}
