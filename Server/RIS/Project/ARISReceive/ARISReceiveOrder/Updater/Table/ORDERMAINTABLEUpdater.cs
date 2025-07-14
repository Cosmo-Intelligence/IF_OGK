using System.Collections.Generic;
using System.Linq;
using ARISReceive.Data;
using RISBizLibrary.Updater.Table;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.Order;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Order;
using RISBizLibrary.Data;

namespace ARISReceive.Updater.Table
{
	/// <summary>
	/// ORDERMAINTABLEを更新する
	/// </summary>
	internal class ORDERMAINTABLEUpdater : BaseUpdater
	{
		#region const
		
		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO ordermaintable " +
		"( " +
			"ris_id, " +
			"systemkbn, " +
			"dokuei_flg, " +
			"irai_doctor_renraku, " +
			"order_section_id, " +
			"irai_doctor_no, " +
			"irai_doctor_name, " +
			"studyinstanceuid, " +
			"orderno, " +
			"accessionno, " +
			"kensa_date, " +
			"kensa_starttime, " +
			"kensatype_id, " +
			"kensasitu_id, " +
			"kensakiki_id, " +
			"syotisitu_flg, " +
			"kanja_id, " +
			"kensa_date_age, " +
			"denpyo_nyugaikbn, " +
			"denpyo_byoutou_id, " +
			"denpyo_byositu_id, " +
			"irai_section_id " +
		") " +
		"SELECT " +
			":ris_id, " +
			":systemkbn, " +
			":dokuei_flg, " +
			"s.doctor_tel, " +
			":order_section_id, " +
			":irai_doctor_no, " +
			":irai_doctor_name, " +
			"RIS_SERVER_PACKAGE.GET_NEW_STUDYINSTANCEUID(:ris_id), " +
			":orderno, " +
			":accessionno, " +
			":kensa_date, " +
			":kensa_starttime, " +
			":kensatype_id, " +
			":kensasitu_id, " +
			"RIS_SERVER_PACKAGE.STRTOKEN(e.kensakiki_id, ',', 1), " +
			":syotisitu_flg, " +
			":kanja_id, " +
			"RIS_SERVER_PACKAGE.YEARS_OF_AGE(TO_DATE(:birthday, 'YYYYMMDD')), " +
			":denpyo_nyugaikbn, " +
			":denpyo_byoutou_id, " +
			":denpyo_byositu_id, " +
			":irai_section_id " +
		"FROM " +
			"DUAL " +
			"LEFT OUTER JOIN sectiondoctormaster s " +
			"ON " +
				"s.doctor_id = :irai_doctor_no " +
			"LEFT OUTER JOIN examroommaster e " +
			"ON " +
				"e.examroom_id = :kensasitu_id ";
		#endregion

		#region param
		/// <summary>
		/// ris_id
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// systemkbn
		/// </summary>
		private const string PARAM_NAME_SYSTEMKBN = "systemkbn";

		/// <summary>
		/// dokuei_flg
		/// </summary>
		private const string PARAM_NAME_DOKUEI_FLG = "dokuei_flg";

		/// <summary>
		/// order_section_id
		/// </summary>
		private const string PARAM_NAME_ORDER_SECTION_ID = "order_section_id";

		/// <summary>
		/// irai_doctor_no
		/// </summary>
		private const string PARAM_NAME_IRAI_DOCTOR_NO = "irai_doctor_no";

		/// <summary>
		/// irai_doctor_name
		/// </summary>
		private const string PARAM_NAME_IRAI_DOCTOR_NAME = "irai_doctor_name";

		/// <summary>
		/// orderno
		/// </summary>
		private const string PARAM_NAME_ORDERNO = "orderno";

		/// <summary>
		/// accessionno
		/// </summary>
		private const string PARAM_NAME_ACCESSIONNO = "accessionno";

		/// <summary>
		/// kensa_date
		/// </summary>
		private const string PARAM_NAME_KENSA_DATE = "kensa_date";

		/// <summary>
		/// kensa_starttime
		/// </summary>
		private const string PARAM_NAME_KENSA_STARTTIME = "kensa_starttime";

		/// <summary>
		/// kensatype_id
		/// </summary>
		private const string PARAM_NAME_KENSATYPE_ID = "kensatype_id";

		/// <summary>
		/// kensasitu_id
		/// </summary>
		private const string PARAM_NAME_KENSASITU_ID = "kensasitu_id";

		/// <summary>
		/// syotisitu_flg
		/// </summary>
		private const string PARAM_NAME_SYOTISITU_FLG = "syotisitu_flg";

		/// <summary>
		/// kanja_id
		/// </summary>
		private const string PARAM_NAME_KANJA_ID = "kanja_id";

		/// <summary>
		/// denpyo_nyugaikbn
		/// </summary>
		private const string PARAM_NAME_DENPYO_NYUGAIKBN = "denpyo_nyugaikbn";

		/// <summary>
		/// denpyo_byoutou_id
		/// </summary>
		private const string PARAM_NAME_DENPYO_BYOUTOU_ID = "denpyo_byoutou_id";

		/// <summary>
		/// denpyo_byositu_id
		/// </summary>
		private const string PARAM_NAME_DENPYO_BYOSITU_ID = "denpyo_byositu_id";

		/// <summary>
		/// irai_section_id
		/// </summary>
		private const string PARAM_NAME_IRAI_SECTION_ID = "irai_section_id";

		/// <summary>
		/// bui_id
		/// </summary>
		private const string PARAM_NAME_BUI_ID = "bui_id";

		/// <summary>
		/// birthday
		/// </summary>
		private const string PARAM_NAME_BIRTHDAY = "birthday";

		#endregion

		#endregion

		#region ITableUpdater メンバ

		/// <summary>
		/// InsertSQL
		/// </summary>
		public override string TargetSQL
		{
			get
			{
				return INSERT_SQL;
			}
		}

		/// <summary>
		/// パラメータ設定
		/// </summary>
		/// <param name="order"></param>
		/// <param name="command"></param>
		public override void SetParams(BaseMsgData data, System.Data.IDbCommand command)
		{
			command.Parameters.Clear();
			#region パラメータ

			OrderMsgData orderData = (OrderMsgData)data;
			OrderRoot orderRoot = orderData.Request.MsgBody;
			OrderAggregate orderAgg = orderRoot.Order;
			PatientAttributeAggregate patientAgg = orderRoot.PatientAttribute;
			CommunicationControlAggregate controlAgg = orderRoot.CommunicationControl;

			SetStringToCommand(PARAM_NAME_RIS_ID, orderData.RIS_ID, command); //RIS識別ID YYYYMMDDNNNNNNNN YYYYMMDD：KENSA_DATE ｎNNNNNNN：ｼｰｹﾝｽﾃｰﾌﾞﾙの連番（常に8桁）
			SetStringToCommand(PARAM_NAME_SYSTEMKBN, RQRISDBConst.ORDERMAINTABLE_SYSTEMKBN_HIS, command); //ｵｰﾀﾞ区分 0：HISｵｰﾀﾞ　1：RISｵｰﾀﾞ
			SetStringToCommand(PARAM_NAME_DOKUEI_FLG, GetDOKUEI_FLG(orderAgg.REMARK_NECESSITY.TrimData), command); //読影要否F　0：不要　1：必要　2：省略　3：至急
			SetStringToCommand(PARAM_NAME_ORDER_SECTION_ID, orderAgg.REQUEST_SECTION_CODE.TrimData, command); //依頼部署ID
			SetStringToCommand(PARAM_NAME_IRAI_DOCTOR_NO, orderAgg.REQUEST_DOCTOR_CODE.TrimData, command); //依頼医ID　HISｵｰﾀﾞのみ
			SetStringToCommand(PARAM_NAME_IRAI_DOCTOR_NAME, orderAgg.REQUEST_DOCTOR_NAME.TrimData, command); //依頼医師名
			SetStringToCommand(PARAM_NAME_ORDERNO, orderData.OrderNoForRQRIS, command); //ｵｰﾀﾞ番号
			SetStringToCommand(PARAM_NAME_ACCESSIONNO, orderData.OrderNoForRQRIS, command); //ACCESSIONNO　※通常は、ODERNOと同値
			SetIntToCommand(PARAM_NAME_KENSA_DATE, orderAgg.EXAM_START_SCHEDULED_DATE.TrimData, command); //予定検査日付（YYYYMMDD）
			SetIntToCommand(PARAM_NAME_KENSA_STARTTIME, GetKENSA_STARTTIME(orderAgg.EXAM_START_SCHEDULED_TIME.TrimData), command); //検査開始時刻／予約時刻　(HHMMSS)
			SetStringToCommand(PARAM_NAME_KENSATYPE_ID, orderData.KensaTypeID, command); //検査種別ID
			SetStringToCommand(PARAM_NAME_KENSASITU_ID, orderAgg.EXAM_ROOM.TrimData, command); //予定検査室ID
			SetStringToCommand(PARAM_NAME_SYOTISITU_FLG, RQRISDBConst.ORDERMAINTABLE_SYOTISITU_FLG_NOT_USE, command); //処置室使用区分　0：使用しない　1：使用する
			SetStringToCommand(PARAM_NAME_KANJA_ID, patientAgg.PATIENT_NO.TrimData, command); //患者ID
			SetStringToCommand(PARAM_NAME_BIRTHDAY, patientAgg.BIRTH_DATE.TrimData, command); //予定検査時年齢　計算できない場合は、999
			SetStringToCommand(PARAM_NAME_DENPYO_NYUGAIKBN, GetDENPYO_NYUGAIKBN(patientAgg.PATIENT_INOUT.TrimData), command); //伝票入外区分　1：外来　2：入院　3：入院中外来
			SetStringToCommand(PARAM_NAME_DENPYO_BYOUTOU_ID, patientAgg.WARD_CODE.TrimData, command); //伝票病棟ID　HISオーダのみ
			SetStringToCommand(PARAM_NAME_DENPYO_BYOSITU_ID, patientAgg.ROOM_CODE.TrimData, command); //病室ID
			SetStringToCommand(PARAM_NAME_IRAI_SECTION_ID, orderAgg.REQUEST_SECTION_CODE.TrimData, command); //依頼科ID
			#endregion
		}
		#endregion

		/// <summary>
		/// 読影要否F　0：不要　1：必要　2：省略　3：至急
		/// </summary>
		/// <param name="value">【電】オーダ電文.10:所見要否</param>
		/// <returns></returns>
		/// <remarks>
		/// [電文値=RIS代入値]
		/// Null = 0
		/// 1 = 3
		/// 2 = 1
		/// 3 = 2
		/// 想定外は""
		/// </remarks>
		private string GetDOKUEI_FLG(string value)
		{
			// Null=0
			if (string.IsNullOrEmpty(value))
			{
				return RQRISDBConst.ORDERMAINTABLE_DOKUEI_FLG_UNNECESSARY;
			}

			// 1 = 3
			if (MsgConst.ORDER_REMARK_NECESSITY_IMMEDIATE == value)
			{
				return RQRISDBConst.ORDERMAINTABLE_DOKUEI_FLG_IMMEDIATE;
			}

			// 2 = 1
			if (MsgConst.ORDER_REMARK_NECESSITY_EXIST == value)
			{
				return RQRISDBConst.ORDERMAINTABLE_DOKUEI_FLG_NECESSARY;
			}

			/// 3 = 2
			if (MsgConst.ORDER_REMARK_NECESSITY_NOT_EXIST == value)
			{
				return RQRISDBConst.ORDERMAINTABLE_DOKUEI_FLG_ELISION;
			}
			return "";
		}

		/// <summary>
		/// 検査開始時刻／予約時刻　(HHMMSS)
		/// </summary>
		/// <param name="value">【電】オーダ電文：実施開始予定時間(HHMM)</param>
		/// <returns></returns>
		/// <remarks>
		/// </remarks>
		private string GetKENSA_STARTTIME(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return "";
			}
			if (value.Length == 4)
			{
				return value + "00";
			}
			return value;
		}

		/// <summary>
		/// 部位ID取得
		/// </summary>
		/// <param name="orderList"></param>
		/// <returns></returns>
		private string GetBUI_ID(OrderDetailArray orderDetailArray)
		{
			if (orderDetailArray == null)
			{
				return "";
			}
			if (orderDetailArray.Count < 1)
			{
				 return "";
			}

			List<OrderDetailAggregate> list = 
				orderDetailArray.Nodes.ConvertAll<OrderDetailAggregate>(n => n as OrderDetailAggregate);
			OrderDetailAggregate firstJA = 
				list.FirstOrDefault<OrderDetailAggregate>(
				l => l.ITEM_KIND.TrimData == MsgConst.ORDER_ITEM_KIND_ACT);
			if (firstJA == null)
			{
				return "";
			}
			return firstJA.ITEM_CODE.TrimData;
		}

		/// <summary>
		/// 伝票入外区分　1：外来　2：入院　3：入院中外来
		/// </summary>
		/// <param name="value">【電】患者属性電文：患者入外区分</param>
		/// <returns></returns>
		/// <remarks>
		/// 1の場合、2を設定
		/// 上記以外は1を設定
		/// </remarks>
		private string GetDENPYO_NYUGAIKBN(string value)
		{
			if (MsgConst.PATIENT_INOUT_ADMISSION == value)
			{
				return RQRISDBConst.ORDERMAINTABLE_DENPYO_NYUGAIKBN_ADMISSION;
			}
			return RQRISDBConst.ORDERMAINTABLE_DENPYO_NYUGAIKBN_CLINIC;
		}
	}
}
