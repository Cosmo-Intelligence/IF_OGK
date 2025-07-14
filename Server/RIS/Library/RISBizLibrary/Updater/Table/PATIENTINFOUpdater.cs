using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Utils;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute.Result;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg;
using RISBizLibrary.Data;
using System.Data;
using System.Configuration;
using RISODPLibrary.Lib.Utils;

namespace RISBizLibrary.Updater.Table
{
	public class PATIENTINFOUpdater: BaseUpdater
	{
		#region const

		/// <summary>
		/// 小数点位置
		/// </summary>
		private const int INDEX_POINT = 3;

		#region MergeSQL
		/// <summary>
		/// MergeSQL
		/// </summary>
		private const string MERGE_SQL =
		"MERGE INTO patientinfo p " +
		"USING " +
		"( " +
			"SELECT " +
				":kanja_id kanja_id, " +
				":kanjisimei kanjisimei, " +
				":kanasimei kanasimei, " +
				":birthday birthday, " +
				":sex sex, " +
				":jusyo1 jusyo1, " +
				":jusyo2 jusyo2, " +
				":kanja_nyugaikbn kanja_nyugaikbn, " +
				":section_id section_id, " +
				":byoutou_id byoutou_id, " +
				":byousitu_id byousitu_id, " +
				":tall tall, " +
				":weight weight, " +
				":blood blood, " +
				":allergymark allergymark, " +
				":allergy allergy, " +
				":his_updatedate his_updatedate, " +
				"SYSDATE ris_updatedate, " +
				":examdata examdata, " +
				":creatinineresult creatinineresult, " +
				":creatinineupdatedate creatinineupdatedate, " +
				":egfrresult egfrresult, " +
				":egfrupdatedate egfrupdatedate " +
			"FROM " +
				"DUAL " +
		") pn " +
		"ON " +
		"( p.kanja_id = pn.kanja_id) " +
		"WHEN MATCHED THEN " + //-- 既存レコードの更新
			"UPDATE SET " +
				"kanjisimei = pn.kanjisimei, " +
				"kanasimei = pn.kanasimei, " +
				"birthday = pn.birthday, " +
				"sex = pn.sex, " +
				"jusyo1 = pn.jusyo1, " +
				"jusyo2 = pn.jusyo2, " +
				"kanja_nyugaikbn = pn.kanja_nyugaikbn, " +
				"section_id = pn.section_id, " +
				"byoutou_id = pn.byoutou_id, " +
				"byousitu_id = pn.byousitu_id, " +
				"tall = pn.tall, " +
				"weight = pn.weight, " +
				"blood = pn.blood, " +
				"allergymark = pn.allergymark, " +
				"allergy = pn.allergy, " +
				"his_updatedate = pn.his_updatedate, " +
				"ris_updatedate = pn.ris_updatedate, " +
				"examdata = pn.examdata, " +
				"creatinineresult = pn.creatinineresult, " +
				"creatinineupdatedate = pn.creatinineupdatedate, " +
				"egfrresult = pn.egfrresult, " +
				"egfrupdatedate = pn.egfrupdatedate " +
		"WHEN NOT MATCHED THEN " + //新規レコードの作成
			"INSERT " +
			"( " +
				"kanja_id, " +
				"kanjisimei, " +
				"kanasimei, " +
				"birthday, " +
				"sex, " +
				"jusyo1, " +
				"jusyo2, " +
				"kanja_nyugaikbn, " +
				"section_id, " +
				"byoutou_id, " +
				"byousitu_id, " +
				"tall, " +
				"weight, " +
				"blood, " +
				"allergymark, " +
				"allergy, " +
				"his_updatedate, " +
				"ris_updatedate, " +
				"examdata, " +
				"creatinineresult, " +
				"creatinineupdatedate, " +
				"egfrresult, " +
				"egfrupdatedate " +
			") " +
			"VALUES " +
			"( " +
				"pn.kanja_id, " +
				"pn.kanjisimei, " +
				"pn.kanasimei, " +
				"pn.birthday, " +
				"pn.sex, " +
				"pn.jusyo1, " +
				"pn.jusyo2, " +
				"pn.kanja_nyugaikbn, " +
				"pn.section_id, " +
				"pn.byoutou_id, " +
				"pn.byousitu_id, " +
				"pn.tall, " +
				"pn.weight, " +
				"pn.blood, " +
				"pn.allergymark, " +
				"pn.allergy, " +
				"pn.his_updatedate, " +
				"pn.ris_updatedate, " +
				"pn.examdata, " +
				"pn.creatinineresult, " +
				"pn.creatinineupdatedate, " +
				"pn.egfrresult, " +
				"pn.egfrupdatedate " +
			") ";
		#endregion

		#region param

		/// <summary>
		/// kanja_id
		/// </summary>
		private const string PARAM_NAME_KANJA_ID = "kanja_id";

		/// <summary>
		/// kanjisimei
		/// </summary>
		private const string PARAM_NAME_KANJISIMEI = "kanjisimei";

		/// <summary>
		/// kanasimei
		/// </summary>
		private const string PARAM_NAME_KANASIMEI = "kanasimei";

		/// <summary>
		/// birthday
		/// </summary>
		private const string PARAM_NAME_BIRTHDAY = "birthday";

		/// <summary>
		/// sex
		/// </summary>
		private const string PARAM_NAME_SEX = "sex";

		/// <summary>
		/// jusyo1
		/// </summary>
		private const string PARAM_NAME_JUSYO1 = "jusyo1";

		/// <summary>
		/// jusyo2
		/// </summary>
		private const string PARAM_NAME_JUSYO2 = "jusyo2";

		/// <summary>
		/// kanja_nyugaikbn
		/// </summary>
		private const string PARAM_NAME_KANJA_NYUGAIKBN = "kanja_nyugaikbn";

		/// <summary>
		/// section_id
		/// </summary>
		private const string PARAM_NAME_SECTION_ID = "section_id";

		/// <summary>
		/// byoutou_id
		/// </summary>
		private const string PARAM_NAME_BYOUTOU_ID = "byoutou_id";

		/// <summary>
		/// byousitu_id
		/// </summary>
		private const string PARAM_NAME_BYOUSITU_ID = "byousitu_id";

		/// <summary>
		/// tall
		/// </summary>
		private const string PARAM_NAME_TALL = "tall";

		/// <summary>
		/// weight
		/// </summary>
		private const string PARAM_NAME_WEIGHT = "weight";

		/// <summary>
		/// blood
		/// </summary>
		private const string PARAM_NAME_BLOOD = "blood";

		/// <summary>
		/// allergymark
		/// </summary>
		private const string PARAM_NAME_ALLERGYMARK = "allergymark";

		/// <summary>
		/// allergy
		/// </summary>
		private const string PARAM_NAME_ALLERGY = "allergy";

		/// <summary>
		/// his_updatedate
		/// </summary>
		private const string PARAM_NAME_HIS_UPDATEDATE = "his_updatedate";

		/// <summary>
		/// ris_updatedate
		/// </summary>
		private const string PARAM_NAME_RIS_UPDATEDATE = "ris_updatedate";

		/// <summary>
		/// examdata
		/// </summary>
		private const string PARAM_NAME_EXAMDATA = "examdata";

		/// <summary>
		/// creatinineresult
		/// </summary>
		private const string PARAM_NAME_CREATININERESULT = "creatinineresult";

		/// <summary>
		/// creatinineupdatedate
		/// </summary>
		private const string PARAM_NAME_CREATININEUPDATEDATE = "creatinineupdatedate";

		/// <summary>
		/// egfrresult
		/// </summary>
		private const string PARAM_NAME_EGFRRESULT = "egfrresult";

		/// <summary>
		/// egfrupdatedate
		/// </summary>
		private const string PARAM_NAME_EGFRUPDATEDATE = "egfrupdatedate";

		#endregion

		#endregion

		#region field
		#endregion

		#region ITableUpdater メンバ

		public override string TargetSQL
		{
			get
			{
				return MERGE_SQL;
			}
		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="order"></param>
		/// <param name="cn"></param>
		private new void Execute(BaseMsgData data, System.Data.IDbCommand command)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="command"></param>
		public void Execute(BaseMsg msg, System.Data.IDbCommand command)
		{
			command.CommandText = TargetSQL;
			ExecuteAndSetParam(msg, command);
		}

		/// <summary>
		/// SQL実行手続き
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		private new void ExecuteAndSetParam(BaseMsgData data, System.Data.IDbCommand command)
		{
		}

		/// <summary>
		/// SQL実行手続き
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="command"></param>
		public void ExecuteAndSetParam(BaseMsg msg, System.Data.IDbCommand command)
		{
			SetParams(msg, command);
			ExecuteSQLInner(command);
		}

		private new void SetParams(BaseMsgData data, System.Data.IDbCommand command)
		{
		}

		public void SetParams(BaseMsg msg, System.Data.IDbCommand command)
		{
			command.Parameters.Clear();
			IPatientAttribute patient = msg.Body as IPatientAttribute;
			PatientAttributeAggregate patientAgg = patient.PatientAttribute;

			SetStringToCommand(PARAM_NAME_KANJA_ID,
				patientAgg.PATIENT_NO.TrimData, command); //患者ID
			SetStringToCommand(PARAM_NAME_KANJISIMEI, patientAgg.KANJI_NAME.TrimData, command); //患者漢字氏名
			SetStringToCommand(PARAM_NAME_KANASIMEI, patientAgg.KANA_NAME.TrimData, command); //患者かな氏名（半角ｶﾅ）
			SetIntToCommand(PARAM_NAME_BIRTHDAY, patientAgg.BIRTH_DATE.TrimData, command); //生年月日（YYYYMMDD）
			SetStringToCommand(PARAM_NAME_SEX, patientAgg.SEX.TrimData, command); //性別（M=男性　F=女性　O=不明）
			SetStringToCommand(PARAM_NAME_JUSYO1, patientAgg.ADDRESS1.TrimData, command); //住所１
			SetStringToCommand(PARAM_NAME_JUSYO2, patientAgg.ADDRESS2.TrimData, command); //住所２
			SetStringToCommand(PARAM_NAME_KANJA_NYUGAIKBN, GetKANJA_NYUGAIKBN(patientAgg), command); //患者入外区分　１：外来　２：入院
			SetStringToCommand(PARAM_NAME_SECTION_ID, patientAgg.ADMISSION_SECTION_CODE.TrimData, command); //入院科ID　（SECTIONMASTER.SECTION_ID）
			SetStringToCommand(PARAM_NAME_BYOUTOU_ID, patientAgg.WARD_CODE.TrimData, command); //入院病棟ID　（BYOUTOUMASTER.BYOUTOU_ID）
			SetStringToCommand(PARAM_NAME_BYOUSITU_ID, patientAgg.ROOM_CODE.TrimData, command); //入院病室ID　（BYOUSITUMASTER.BYOUSITU_ID）
			SetDecimalToCommand(PARAM_NAME_TALL, GetTALL(patientAgg), command); //身長（ｃｍ）
			SetDecimalToCommand(PARAM_NAME_WEIGHT, GetWEIGHT(patientAgg), command); //体重（ｋｇ）

			PatientResultHelper helper = new PatientResultHelper();
			helper.ResultValue = patientAgg.RESULT_VALUE.TrimData;
			string markerCharacter = GetMARKERCHARACTER(command.Connection, command.Transaction);

			SetStringToCommand(PARAM_NAME_BLOOD, GetBLOOD(helper), command); //血液型（A,B,O,AB）＋RHTYPE（＋,－）
			SetIntToCommand(PARAM_NAME_ALLERGYMARK, GetALLERGYMARK(helper), command); //ｱﾚﾙｷﾞｰ情報識別子 0：ｱﾚﾙｷﾞｰなし　1：ｱﾚﾙｷﾞｰあり
			SetStringToCommand(PARAM_NAME_ALLERGY, GetALLERGY(helper, markerCharacter), command); //ｱﾚﾙｷﾞｰ情報を、ｼｽﾃﾑ区切文字で複数設定
			SetDateTimeToCommand(PARAM_NAME_HIS_UPDATEDATE, GetHIS_UPDATEDATE(msg.Body as ICommunicationControl), command); //HIS最新更新日時
			
			SetStringToCommand(PARAM_NAME_EXAMDATA, GetEXAMDATA(helper, markerCharacter), command); //検査ﾃﾞｰﾀ情報を、CRLF複数設定

			PatientResultExamData itemCreatinine = helper.ExamDataList.LastOrDefault<PatientResultExamData>
				(pd => string.Compare(
					pd.ExamCode, MsgConst.PATIENT_RESULT_VALUE_EXAMDATE_EXAM_CODE_CRE) == 0);
			SetStringToCommand(PARAM_NAME_CREATININERESULT, GetCREATININERESULT(itemCreatinine), command); //Cr値
			SetDateTimeToCommand(PARAM_NAME_CREATININEUPDATEDATE, GetCREATININEUPDATEDATE(itemCreatinine), command); //Cr値 計測日

			PatientResultExamData itemEGFR = helper.ExamDataList.LastOrDefault<PatientResultExamData>
				(pd => string.Compare(
					pd.ExamCode, MsgConst.PATIENT_RESULT_VALUE_EXAMDATE_EXAM_CODE_EGFR) == 0);
			SetStringToCommand(PARAM_NAME_EGFRRESULT, GetEGFRRESULT(itemEGFR), command); //cGFR
			SetDateTimeToCommand(PARAM_NAME_EGFRUPDATEDATE, GetEGFRUPDATEDATE(itemEGFR), command); //cGFR 計測日
		}
		#endregion

		/// <summary>
		/// 複数項目の区切り文字取得
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		private string GetMARKERCHARACTER(IDbConnection cn, IDbTransaction tran)
		{
			const string SQL_SELECT =
			"SELECT " +
				"s.markercharacter {0} " +
			"FROM " +
				"systemdefine s ";

			const string field_name = "markercharacter";

			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tran;
				command.CommandText = string.Format(SQL_SELECT, field_name);

				_log.Debug("MARKERCHARACTER取得します");
				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						return RQRISDBConst.SYSTEMDEFINE_MARKERCHARACTER_DEFAULT;
					}
					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);
					string markerCharacter = reader.GetStringByDBString(field_name);
					_log.DebugFormat("MARKERCHARACTER={0}", markerCharacter);
					return markerCharacter;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		/// <summary>
		/// eGFR 計測日
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		private string GetEGFRUPDATEDATE(PatientResultExamData item)
		{
			if (item == null)
			{
				return "";
			}
			return DateTimeUtils.GetDateTimeStringExact(
				item.ExamDate,
				"yyyyMMdd", "yyyy/MM/dd");
		}

		/// <summary>
		/// eGFR
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		private string GetEGFRRESULT(PatientResultExamData item)
		{
			if (item == null)
			{
				return "";
			}
			return item.ExamResult;
		}

		/// <summary>
		/// Cr値 計測日
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private string GetCREATININEUPDATEDATE(PatientResultExamData item)
		{
			if (item == null)
			{
				return "";
			}

			return DateTimeUtils.GetDateTimeStringExact(
				item.ExamDate,
				"yyyyMMdd", "yyyy/MM/dd");
		}

		/// <summary>
		/// Cr値
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private string GetCREATININERESULT(PatientResultExamData item)
		{
			if (item == null)
			{
				return "";
			}
			return item.ExamResult;
		}

		/// <summary>
		/// 検査データ情報
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="markerCharacter"></param>
		/// <returns></returns>
		private string GetEXAMDATA(PatientResultHelper helper, string markerCharacter)
		{
			StringBuilder sb = new StringBuilder();
			foreach (PatientResultExamData item in helper.ExamDataList)
			{
				sb.Append(item.ToStringByExamData());
			}
			const int MAX_LENGTH_EXAMDATA = 1024;
			return MBCSHelper.Copy(
				sb.ToString(), 1, MAX_LENGTH_EXAMDATA);
		}

		/// <summary>
		/// 患者入外区分　１：外来　２：入院
		/// </summary>
		/// <param name="patientAgg"></param>
		/// <returns></returns>
		private string GetKANJA_NYUGAIKBN(PatientAttributeAggregate patientAgg)
		{
			if (patientAgg.PATIENT_INOUT.TrimData == MsgConst.PATIENT_INOUT_ADMISSION)
			{
				return RQRISDBConst.PATIENTINFO_KANJA_NYUGAIKBN_ADMISSION;
			}
			return RQRISDBConst.PATIENTINFO_KANJA_NYUGAIKBN_CLINIC;
		}

		/// <summary>
		///ｱﾚﾙｷﾞｰ情報を、ｼｽﾃﾑ区切文字で複数設定
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="markerCharacter"></param>
		/// <returns></returns>
		private string GetALLERGY(PatientResultHelper helper, string markerCharacter)
		{
			StringBuilder sb = new StringBuilder();
			foreach (PatientResultAllergy item in helper.AllergyList)
			{
				sb.AddWithSeparator(item.Name,
					markerCharacter);
			}
			return sb.ToString();
		}

		/// <summary>
		/// ｱﾚﾙｷﾞｰ情報識別子 0：ｱﾚﾙｷﾞｰなし　1：ｱﾚﾙｷﾞｰあり
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		private string GetALLERGYMARK(PatientResultHelper helper)
		{
			if (helper.AllergyList.Count > 0)
			{
				return RQRISDBConst.PATIENTINFO_SUSPECT;
			}

			return RQRISDBConst.PATIENTINFO_SUSPECT_NOT;
		}

		/// <summary>
		/// HIS最新更新日時
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private string GetHIS_UPDATEDATE(ICommunicationControl cc)
		{
			CommunicationControlAggregate control = cc.CommunicationControl;
			return DateTimeUtils.GetDateTimeStringExact(
				control.SEND_DATE.TrimData + control.SEND_TIME.TrimData,
				"yyyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");
		}

		/// <summary>
		/// 感染情報をｼｽﾃﾑ区切文字で複数設定 各感染項目毎に、名称=値　で設定する 値は、0：未検査　　1：陰性　　2:陽性　　3：不明
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="markerCharacter"></param>
		/// <returns></returns>
		private string GetINFECTION(PatientResultHelper helper, string markerCharacter)
		{
			StringBuilder sb = new StringBuilder();
			foreach (PatientResultInfection item in helper.InfectionList)
			{
				sb.AddWithSeparator(string.Format("{0}={1}", item.ResultCode, item.ExamName),
					markerCharacter);
			}
			return sb.ToString();
		}

		/// <summary>
		/// 感染情報識別子 0：感染情報なし　　1：感染疑いあり
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		private string GetINFECTIONMARK(PatientResultHelper helper)
		{
			if (helper.InfectionList.Count > 0)
			{
				return RQRISDBConst.PATIENTINFO_SUSPECT;
			}

			return RQRISDBConst.PATIENTINFO_SUSPECT_NOT;
		}

		/// <summary>
		/// 血液型（A,B,O,AB）＋RHTYPE（＋,－）
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		/// <remarks>
		/// (1)設定タイプ=2の明細の2つ目の項目を抜き出す
		/// (2)抜き出した値が、A or B or O or AB の場合は、その値を使用する。A or B or O or AB以外の場合は、Null設定
		/// (3)設定タイプ=2の明細の4つ目の項目を抜き出す
		/// (4)抜き出した値が、+ or - の場合は、その値を使用する。+ or - の場合は、Null扱い。
		/// (5) 前項(2)でNullの場合は、PatientInfo.BloodにNull設定。それ以外の場合は前項(2)の値＋前項(4)の値を設定"
		/// </remarks>
		private string GetBLOOD(PatientResultHelper helper)
		{
			if (helper.BloodList.Count < 1)
			{
				return "";
			}
			PatientResultBlood blood = helper.BloodList[0]; //かならず先頭を使用する

			StringBuilder sb = new StringBuilder();
			if (!RQRISDBConst.PATIENTINFO_BLOOD_TYPE_LIST.Contains(blood.ExamResultABO))
			{
				return "";
			}
			sb.Append(blood.ExamResultABO);

			if (!RQRISDBConst.PATIENTINFO_RH_TYPE_LIST.Contains(blood.ExamResultRH))
			{
				return sb.ToString();
			}
			sb.Append(blood.ExamResultRH);
			
			return sb.ToString();
		}

		/// <summary>
		/// 体重（ｋｇ）
		/// </summary>
		/// <param name="patient"></param>
		/// <returns></returns>
		private string GetWEIGHT(PatientAttributeAggregate patient)
		{
			return StringUtils.GetDecimalStringByFixedString(
				patient.WEIGHT.TrimData, INDEX_POINT);
		}

		/// <summary>
		/// 身長（ｃｍ）
		/// </summary>
		/// <param name="patient"></param>
		/// <returns></returns>
		private string GetTALL(PatientAttributeAggregate patient)
		{
			return StringUtils.GetDecimalStringByFixedString(
				patient.HEIGHT.TrimData, INDEX_POINT);
		}

	}
}
