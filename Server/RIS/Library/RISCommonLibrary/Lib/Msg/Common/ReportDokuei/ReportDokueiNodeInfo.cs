using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.ReportDokuei
{
	/// <summary>
	/// 既読情報部のノード定義
	/// </summary>
	public class ReportDokueiNodeInfo
	{
		public static NodeInfo REPORTDOKUEI_ROOT = new NodeInfo("REPORTDOKUEI", "既読情報部", NodeTypeEnum.ntAggregate, -1);

		public static NodeInfo REPORTDOKUEI_PROCESSING_TYPE = new NodeInfo("PROCESSING_TYPE", "処理タイプ", NodeTypeEnum.ntData, 2);
		public static NodeInfo REPORTDOKUEI_PATIENT_NO = new NodeInfo("PATIENT_NO", "患者番号", NodeTypeEnum.ntData, 10);
		public static NodeInfo REPORTDOKUEI_DATE = new NodeInfo("REPORTDOKUEI_DATE", "処理年月日", NodeTypeEnum.ntData, 8);
		public static NodeInfo REPORTDOKUEI_TIME = new NodeInfo("REPORTDOKUEI_TIME", "処理時刻", NodeTypeEnum.ntData, 4);
		public static NodeInfo REPORTDOKUEI_USERID = new NodeInfo("REPORTDOKUEI_USERID", "ユーザID", NodeTypeEnum.ntData, 6);
		public static NodeInfo REPORTDOKUEI_RESERVE = new NodeInfo("RESERVE", "予備", NodeTypeEnum.ntData, 10);
	}
}
