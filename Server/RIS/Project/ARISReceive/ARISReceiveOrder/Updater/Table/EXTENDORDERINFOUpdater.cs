using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.Order;
using System.Globalization;
using RISCommonLibrary.Lib.Utils;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using ARISReceive.Data;
using ARISReceive.Properties;
using RISCommonLibrary.Lib.Msg;
using RISBizLibrary.Updater.Table;
using RISBizLibrary.Data;
using System.Configuration;

namespace ARISReceive.Updater.Table
{
	internal class EXTENDORDERINFOUpdater : BaseUpdater
	{

		#region const

		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO extendorderinfo " +
		"( " +
			"ris_id, " +
			"order_date, " +
			"updatedate, " +
			"updatetime, " +
			"his_hakko_date, " +
			"his_update_date, " +
			"ri_order_flg, " +
			"yoteikaikei_flg, " +
			"isitatiai_flg, " +
			"portable_flg, " +
			"kanja_syokai_flg, " +
			"exam_timing, " +
			"addendum01 " +
			") " +
			"SELECT " +
			":ris_id, " +
			":order_date, " +
			"TO_NUMBER(TO_CHAR(SYSDATE, 'YYYYMMDD')), " +
			"TO_NUMBER(TO_CHAR(SYSDATE, 'HH24MISS')), " +
			":his_hakko_date, " +
			":his_update_date, " +
			":ri_order_flg, " +
			":yoteikaikei_flg, " +
			":isitatiai_flg, " +
			"b.portableflag, " +
			":kanja_syokai_flg, " +
			":exam_timing, " +
			":addendum01 " +
		"FROM " +
			"DUAL " +
			"LEFT OUTER JOIN buimaster b " +
			"ON " +
				"b.bui_id = :bui_id ";

		#endregion

		#region param

		/// <summary>
		/// ris_id
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// order_date
		/// </summary>
		private const string PARAM_NAME_ORDER_DATE = "order_date";

		/// <summary>
		/// his_hakko_date
		/// </summary>
		private const string PARAM_NAME_HIS_HAKKO_DATE = "his_hakko_date";

		/// <summary>
		/// his_update_date
		/// </summary>
		private const string PARAM_NAME_HIS_UPDATE_DATE = "his_update_date";

		/// <summary>
		/// ri_order_flg
		/// </summary>
		private const string PARAM_NAME_RI_ORDER_FLG = "ri_order_flg";

		/// <summary>
		/// yoteikaikei_flg
		/// </summary>
		private const string PARAM_NAME_YOTEIKAIKEI_FLG = "yoteikaikei_flg";

		/// <summary>
		/// isitatiai_flg
		/// </summary>
		private const string PARAM_NAME_ISITATIAI_FLG = "isitatiai_flg";

		/// <summary>
		/// bui_id
		/// </summary>
		private const string PARAM_NAME_BUI_ID = "bui_id";

		/// <summary>
		/// kanja_syokai_flg
		/// </summary>
		private const string PARAM_NAME_KANJA_SYOKAI_FLG = "kanja_syokai_flg";

		/// <summary>
		/// exam_timing
		/// </summary>
		private const string PARAM_NAME_EXAM_TIMING = "exam_timing";

		/// <summary>
		/// addendum01
		/// </summary>
		private const string PARAM_NAME_ADDENDUM01 = "addendum01";

		#endregion

		#endregion

		#region field
		#endregion
	
		public override string TargetSQL
		{
			get
			{
				return INSERT_SQL;
			}
		}

		public override void SetParams(BaseMsgData data, System.Data.IDbCommand command)
		{
			command.Parameters.Clear();
			OrderMsgData orderData = (OrderMsgData)data;
			SetStringToCommand(PARAM_NAME_RIS_ID, orderData.RIS_ID, command); //RIS識別ID YYYYMMDDNNNNNNNN YYYYMMDD：KENSA_DATE ｎNNNNNNN：ｼｰｹﾝｽﾃｰﾌﾞﾙの連番（常に8桁）

			OrderAggregate order = orderData.Request.MsgBody.Order;
			CommunicationControlAggregate control = orderData.Request.MsgBody.CommunicationControl;

			string orderDateString = DateTimeUtils.GetDateTimeStringExact(
				order.ORDER_REGISTER_DATE.TrimData + order.ORDER_REGISTER_TIME.TrimData, 
				"yyyyMMddHHmm", "yyyy/MM/dd HH:mm:ss");
			SetDateTimeToCommand(PARAM_NAME_ORDER_DATE, orderDateString, command); //ｵｰﾀﾞ発行日時
			SetDateTimeToCommand(PARAM_NAME_HIS_HAKKO_DATE, orderDateString, command); //HIS発行日時　（RISｵｰﾀﾞ時は、登録日付）

			string hisDateString = DateTimeUtils.GetDateTimeStringExact(
				control.SEND_DATE.TrimData + control.SEND_TIME.TrimData,
				"yyyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");
			SetDateTimeToCommand(PARAM_NAME_HIS_UPDATE_DATE, hisDateString, command); //RISｵｰﾀﾞ登録操作実施端末名称



			SetStringToCommand(PARAM_NAME_RI_ORDER_FLG, 
				GetRI_ORDER_FLG(data, ConfigurationManager.AppSettings["RIKensaTypeIDList"].StringToString()), command); //RIｵｰﾀﾞ区分 1：RI注射　2：RI検査　0 その他検査
			SetStringToCommand(PARAM_NAME_YOTEIKAIKEI_FLG, RQRISDBConst.EXTENDORDERINFO_YOTEIKAIKEI_FLG_ACCOUNT, command); //予定会計送信種別ﾌﾗｸﾞ 0：会計なし　1：会計あり
			SetStringToCommand(PARAM_NAME_ISITATIAI_FLG, RQRISDBConst.EXTENDORDERINFO_ISITATIAI_FLG_NOT_EXIST, command); //放科医師立会い区分 0：立会いなし　1：立会いあり
			SetStringToCommand(PARAM_NAME_BUI_ID, orderData.BuiIDFirst, command); //部位IDをポータブルフラグのために設定する
			SetStringToCommand(PARAM_NAME_KANJA_SYOKAI_FLG, RQRISDBConst.EXTENDORDERINFO_KANJA_SYOKAI_FLG_NORMAL, command); //患者紹介F 0：通常　1：紹介患者
			SetStringToCommand(PARAM_NAME_EXAM_TIMING, order.ORDER_KIND.TrimData, command); //緊急度　[1:通常、2:事後、5:至急,6:病診,7:手術室]
			SetStringToCommand(PARAM_NAME_ADDENDUM01, control.ORDER_NO.TrimData, command); //オーダNo(フル桁)
			
		}

		/// <summary>
		/// RIオーダ区分 1：RI注射　2：RI検査　0 その他検査
		/// </summary>
		/// <param name="data"></param>
		/// <param name="riKensaTypeIDList"></param>
		/// <returns></returns>
		private string GetRI_ORDER_FLG(BaseMsgData data, string riKensaTypeIDList)
		{
			OrderMsgData orderData = (OrderMsgData)data;
			if (string.IsNullOrEmpty(riKensaTypeIDList))
			{
				return RQRISDBConst.EXTENDORDERINFO_RI_ORDER_FLG_OTHER;
			}
			string[] riArray = riKensaTypeIDList.Split(new []{','});
			if (riArray.Length < 1)
			{
				return RQRISDBConst.EXTENDORDERINFO_RI_ORDER_FLG_OTHER;
			}
			if (!riArray.Contains(orderData.KensaTypeID))
			{
				return RQRISDBConst.EXTENDORDERINFO_RI_ORDER_FLG_OTHER;
			}
			return RQRISDBConst.EXTENDORDERINFO_RI_ORDER_FLG_NEEDLE;
		}
	}
}
