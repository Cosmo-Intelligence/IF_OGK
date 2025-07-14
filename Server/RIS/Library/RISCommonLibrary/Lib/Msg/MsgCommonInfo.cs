using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	class MsgCommonInfo
	{
		public static NodeInfo H2R_DENBUN_SYBT = new NodeInfo("DENBUN_SYBT", "電文種別", NodeTypeEnum.ntData, 2);
		public static NodeInfo H2R_SAKUSEI_DATE = new NodeInfo("SAKUSEI_DATE", "作成日", NodeTypeEnum.ntData, 8);
		public static NodeInfo H2R_SAKUSEI_TIME = new NodeInfo("SAKUSEI_TIME", "作成時刻", NodeTypeEnum.ntData, 6);
		public static NodeInfo H2R_S_SYS_CD = new NodeInfo("S_SYS_CD", "送信側システムコード", NodeTypeEnum.ntData, 2);
		public static NodeInfo H2R_R_SYS_CD = new NodeInfo("R_SYS_CD", "受信側システムコード", NodeTypeEnum.ntData, 2);
		public static NodeInfo H2R_HEADER_CNT = new NodeInfo("HEADER_CNT", "ｼｽﾃﾑ間共通ﾍｯﾀﾞ件数", NodeTypeEnum.ntData, 8);
		public static NodeInfo H2R_SYORI_KBN = new NodeInfo("SYORI_KBN", "処理区分", NodeTypeEnum.ntData, 1);
		public static NodeInfo H2R_SYORI_DATE = new NodeInfo("SYORI_DATE", "処理日", NodeTypeEnum.ntData, 8);
		public static NodeInfo H2R_SYORI_TIME = new NodeInfo("SYORI_TIME", "処理時刻", NodeTypeEnum.ntData, 6);
	}
}
