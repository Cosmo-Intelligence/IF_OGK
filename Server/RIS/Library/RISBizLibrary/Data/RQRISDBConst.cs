using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISBizLibrary.Data
{
	/// <summary>
	/// RQRISDB定数
	/// </summary>
	public class RQRISDBConst
	{
		#region 共通
		/// <summary>
		/// 検査種別+0000
		/// </summary>
		public const string KENSATYPE_ID_COMMON_SUFFIX = "0000";

		/// <summary>
		/// 左右ID-なし
		/// </summary>
		public const string SAYUU_ID_NONE = "0";

		/// <summary>
		/// 複数項目区切り文字のデフォルト値
		/// </summary>
		public const string SYSTEMDEFINE_MARKERCHARACTER_DEFAULT = "|";
		#endregion

		#region SYSTEMDEFINE
		/// <summary>
		/// コメント系のセパレータ
		/// </summary>
		public const string COMMENT_SEPARATOR = "\r\n";
		#endregion

		#region ORDERMAINTABLE

		#region オーダ区分
		/// <summary>
		/// HISオーダ
		/// </summary>
		public const string ORDERMAINTABLE_SYSTEMKBN_HIS = "0";
		/// <summary>
		/// RISオーダ
		/// </summary>
		public const string ORDERMAINTABLE_SYSTEMKBN_RIS = "1";
		#endregion

		#region 読影要否フラグ
		//読影要否F　0：不要　1：必要　2：省略　3：至急
		/// <summary>
		/// 読影要否フラグ-不要
		/// </summary>
		public const string ORDERMAINTABLE_DOKUEI_FLG_UNNECESSARY = "0";

		/// <summary>
		/// 読影要否フラグ-必要
		/// </summary>
		public const string ORDERMAINTABLE_DOKUEI_FLG_NECESSARY = "1";

		/// <summary>
		/// 読影要否フラグ-省略
		/// </summary>
		public const string ORDERMAINTABLE_DOKUEI_FLG_ELISION = "2";

		/// <summary>
		/// 読影要否フラグ-至急
		/// </summary>
		public const string ORDERMAINTABLE_DOKUEI_FLG_IMMEDIATE = "3";

		#endregion

		#region 処置室使用区分
		//　0：使用しない　1：使用する
		/// <summary>
		/// 処置室使用区分-使用しない
		/// </summary>
		public const string ORDERMAINTABLE_SYOTISITU_FLG_NOT_USE = "0";
		#endregion

		#region 伝票入外区分
		//伝票入外区分　1：外来　2：入院　3：入院中外来

		/// <summary>
		/// 伝票入外区分-外来
		/// </summary>
		public const string ORDERMAINTABLE_DENPYO_NYUGAIKBN_CLINIC = "1";

		/// <summary>
		/// 伝票入外区分-入院
		/// </summary>
		public const string ORDERMAINTABLE_DENPYO_NYUGAIKBN_ADMISSION = "2";

		#endregion

		#region 所見ステータス

		/// <summary>
		/// 所見ステータス-医事請求加算
		/// </summary>
		public const string ORDERMAINTABLE_REPORT_STATUS_REQUEST_ADDITION = "40";

		#endregion

		#endregion

		#region EXTENDORDERINFO

		#region RIオーダ区分
		//RIｵｰﾀﾞ区分 1：RI注射　2：RI検査　0 その他検査
		/// <summary>
		/// RIオーダ区分-その他検査
		/// </summary>
		public const string EXTENDORDERINFO_RI_ORDER_FLG_OTHER = "0";

		/// <summary>
		/// RIオーダ区分-RI注射
		/// </summary>
		public const string EXTENDORDERINFO_RI_ORDER_FLG_NEEDLE = "1";
		#endregion

		#region 予定会計送信種別フラグ
		//予定会計送信種別ﾌﾗｸﾞ 0：会計なし　1：会計あり
		/// <summary>
		/// 予定会計送信種別フラグ-会計あり
		/// </summary>
		public const string EXTENDORDERINFO_YOTEIKAIKEI_FLG_ACCOUNT = "1";

		#endregion

		#region 放科医師立会い区分 0：立会いなし　1：立会いあり

		/// <summary>
		/// 放科医師立会い区分-立会いなし
		/// </summary>
		public const string EXTENDORDERINFO_ISITATIAI_FLG_NOT_EXIST = "0";
		
		#endregion

		#region ﾎﾟｰﾀﾌﾞﾙF 0：通常　1：ﾎﾟｰﾀﾌﾞﾙ　2：手術室

		/// <summary>
		/// ポータブルフラグ-通常
		/// </summary>
		public const string EXTENDORDERINFO_PORTABLE_FLG_NORMAL = "0";
		
		/// <summary>
		/// ポータブルフラグ-ポータブル
		/// </summary>
		public const string EXTENDORDERINFO_PORTABLE_FLG_PORTABLE = "1";
		
		/// <summary>
		/// ポータブルフラグ-手術室
		/// </summary>
		public const string EXTENDORDERINFO_PORTABLE_FLG_OPE = "2";
		
		#endregion

		#region 患者紹介F 0：通常　1：紹介患者

		/// <summary>
		/// 患者紹介フラグ-通常
		/// </summary>
		public const string EXTENDORDERINFO_KANJA_SYOKAI_FLG_NORMAL = "0";
		
		#endregion

		#region 緊急度　[1:通常、2:事後、5:至急,6:病診,7:手術室]

		/// <summary>
		/// 緊急度-通常
		/// </summary>
		public const string EXTENDORDERINFO_EXAM_TIMING_NORMAL = "1";

		/// <summary>
		/// 緊急度-事後
		/// </summary>
		public const string EXTENDORDERINFO_EXAM_TIMING_AFTER = "2";

		/// <summary>
		/// 緊急度-至急
		/// </summary>
		public const string EXTENDORDERINFO_EXAM_TIMING_IMMEDIATE = "5";

		/// <summary>
		/// 緊急度-病診
		/// </summary>
		public const string EXTENDORDERINFO_EXAM_TIMING_DIAGNOSIS = "6";

		/// <summary>
		/// 緊急度-手術室
		/// </summary>
		public const string EXTENDORDERINFO_EXAM_TIMING_OPE_ROOM = "7";

		#endregion

		#endregion

		#region ORDERBUITABLE

		#region 左右ID
		/// <summary>
		/// RIオーダ区分-その他検査
		/// </summary>
		public const string ORDERBUITABLE_SAYUU_ID_NONE = SAYUU_ID_NONE;

		#endregion

		#endregion

		#region PATIENTINFO

		#region 識別子 0：情報なし　1：あり

		/// <summary>
		/// 識別子-なし
		/// </summary>
		public const string PATIENTINFO_SUSPECT_NOT = "0";

		/// <summary>
		/// 識別子-あり
		/// </summary>
		public const string PATIENTINFO_SUSPECT = "1";

		#endregion

		#region 性別（M=男性　F=女性　O=不明）

		/// <summary>
		/// 性別-男性
		/// </summary>
		public const string PATIENTINFO_SEX_MALE = "M";

		/// <summary>
		/// 性別-女性
		/// </summary>
		public const string PATIENTINFO_SEX_FEMALE = "F";

		/// <summary>
		/// 性別-不明
		/// </summary>
		public const string PATIENTINFO_SEX_OTHER = "O";
		
		#endregion

		#region 患者入外区分　１：外来　２：入院

		/// <summary>
		/// 患者入外区分-外来
		/// </summary>
		public const string PATIENTINFO_KANJA_NYUGAIKBN_CLINIC = ORDERMAINTABLE_DENPYO_NYUGAIKBN_CLINIC;

		/// <summary>
		/// 患者入外区分-入院
		/// </summary>
		public const string PATIENTINFO_KANJA_NYUGAIKBN_ADMISSION = ORDERMAINTABLE_DENPYO_NYUGAIKBN_ADMISSION;

		#endregion


		#region 血液型（A,B,O,AB）

		/// <summary>
		/// 血液型-A
		/// </summary>
		public const string PATIENTINFO_BLOOD_TYPE_A = "A";

		/// <summary>
		/// 血液型-B
		/// </summary>
		public const string PATIENTINFO_BLOOD_TYPE_B = "B";

		/// <summary>
		/// 血液型-O
		/// </summary>
		public const string PATIENTINFO_BLOOD_TYPE_O = "O";

		/// <summary>
		/// 血液型-AB
		/// </summary>
		public const string PATIENTINFO_BLOOD_TYPE_AB = "AB";

		/// <summary>
		/// 血液型テーブル
		/// </summary>
		public static string[] PATIENTINFO_BLOOD_TYPE_LIST = {
													 PATIENTINFO_BLOOD_TYPE_A, 
													 PATIENTINFO_BLOOD_TYPE_B,
													 PATIENTINFO_BLOOD_TYPE_O,
													 PATIENTINFO_BLOOD_TYPE_AB
													};

		#endregion

		#region RHTYPE（＋,－）

		/// <summary>
		/// RH型-+
		/// </summary>
		public const string PATIENTINFO_RH_TYPE_PLUS = "+";

		/// <summary>
		/// RH型--
		/// </summary>
		public const string PATIENTINFO_RH_TYPE_MINUS = "-";


		/// <summary>
		/// RH型テーブル
		/// </summary>
		public static string[] PATIENTINFO_RH_TYPE_LIST = {
													PATIENTINFO_RH_TYPE_PLUS, 
													PATIENTINFO_RH_TYPE_MINUS
													};
		
		#endregion

		#region 感染情報をｼｽﾃﾑ区切文字で複数設定 各感染項目毎に、名称=値　で設定する 値は、0：未検査　　1：陰性　　2:陽性　　3：不明

		/// <summary>
		/// 感染情報-未検査
		/// </summary>
		public const string PATIENTINFO_INFECTION_NOT_INSPECT = "0";

		/// <summary>
		/// 感染情報-陰性
		/// </summary>
		public const string PATIENTINFO_INFECTION_NEGATIVE = "1";

		/// <summary>
		/// 感染情報-陽性
		/// </summary>
		public const string PATIENTINFO_INFECTION_POSITIVE = "2";
		
		/// <summary>
		/// 感染情報-不明
		/// </summary>
		public const string PATIENTINFO_INFECTION_UNKNOWN = "3";

		#endregion

		#endregion

		#region PATIENTCOMMENTS

		#region 検査種別ID COMMON固定
		/// <summary>
		/// 検査種別ID-COMMON
		/// </summary>
		public const string PATIENTCOMMENTS_KENSATYPE_ID_COMMON = "COMMON";

		#endregion

		#endregion

		#region EXMAINTABLE

		#region 検査ｽﾃｰﾀｽ
		/// <summary>
		/// 検査ｽﾃｰﾀｽ-未受付
		/// </summary>
		public const string EXMAINTABLE_STATUS_NOT_RECEIPT = "0";

		#endregion

		#endregion

		#region EXBUITABLE

		#region 撮影進捗
		/// <summary>
		/// 撮影進捗-0：未
		/// </summary>
		public const string EXBUITABLE_SATUEISTATUS_YET = "0";
	
		/// <summary>
		/// 撮影進捗-1：済
		/// </summary>
		public const string EXBUITABLE_SATUEISTATUS_DONE = "1";

		/// <summary>
		/// 撮影進捗-2：中止
		/// </summary>
		public const string EXBUITABLE_SATUEISTATUS_CANCEL = "2";

		#endregion

		#endregion

		#region FROMHISINFO

		#region 受信データタイプ識別子（GGNN）

		/// <summary>
		/// 受信データタイプ識別子（GGNN）-オーダ受信
		/// </summary>
		public const string FROMHISINFO_MESSAGETYPE_ORDER = "OI01";

		/// <summary>
		/// 受信データタイプ識別子（GGNN）-オーダキャンセル
		/// </summary>
		public const string FROMHISINFO_MESSAGETYPE_ORDER_CANCEL = "OI99";
	
		/// <summary>
		/// 受信データタイプ識別子（GGNN）-患者情報受信
		/// </summary>
		public const string FROMHISINFO_MESSAGETYPE_PATIENT = "PI01";

		#endregion

		#endregion

		#region TOREPORTINFO

		#region 処理タイプ識別子（GGNN）
		/// <summary>
		/// 受信データタイプ識別子（GGNN）-オーダ受信
		/// </summary>
		public const string TOREPORTINFO_MESSAGETYPE_ORDER = FROMHISINFO_MESSAGETYPE_ORDER;

		/// <summary>
		/// 受信データタイプ識別子（GGNN）-オーダキャンセル
		/// </summary>
		public const string TOREPORTINFO_MESSAGETYPE_ORDER_CANCEL = FROMHISINFO_MESSAGETYPE_ORDER_CANCEL;

		#endregion

		#region "送信ｽﾃｰﾀｽ 00：未送信 01：送信済　02：送信対象外"

		/// <summary>
		/// 送信ステータス-未送信
		/// </summary>
		public const string TOREPORTINFO_TRANSFERSTATUS_UNSENT = "00";

		/// <summary>
		/// 送信ステータス-送信済
		/// </summary>
		public const string TOREPORTINFO_TRANSFERSTATUS_SENT = "01";

		/// <summary>
		/// 送信ステータス-送信対象外
		/// </summary>
		public const string TOREPORTINFO_TRANSFERSTATUS_SENT_EXEMPT = "02";
		#endregion

		#endregion

		#region TOHISINFO

		#region 受信データタイプ識別子（GGNN）

		#region 受付系
		
		/// <summary>
		/// 処理タイプ識別子-受付通知
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_RECEIPT = "RC01";

		/// <summary>
		/// 処理タイプ識別子-受付キャンセル
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_RECEIPT_CANCEL = "RC99";

		/// <summary>
		/// 処理タイプ識別子-画像通知
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_IMAGE = "10";

		/// <summary>
		/// 処理タイプ識別子-1次所見
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_REMARK_FIRST = "20";

		/// <summary>
		/// 処理タイプ識別子-2次所見
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_REMARK_SECOND = "30";

		/// <summary>
		/// 処理タイプ識別子-2次所見解除
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_REMARK_SECOND_CANCEL = "31";

		/// <summary>
		/// 処理タイプ識別子-所見医事請求加算
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION = "40";

		/// <summary>
		/// 処理タイプ識別子-所見医事請求加算解除
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION_CANCEL = "41";

		#endregion

		#region 実施系
		
		/// <summary>
		/// 処理タイプ識別子-実施通知
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_EXAM = "OP01";

		/// <summary>
		/// 処理タイプ識別子-実施通知（再送） 
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_EXAM_RESEND = "OP02";

		/// <summary>
		/// 処理タイプ識別子-実施中止通知
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_EXAM_STOP = "OP99";

		#endregion

		#region 患者要求
		
		/// <summary>
		/// 処理タイプ識別子-患者情報取得要求
		/// </summary>
		public const string TOHISINFO_REQUESTTYPE_PATIENT = "PR01";

		#endregion
		
		#endregion

		#region "送信ｽﾃｰﾀｽ 00：未送信 01：送信済　02：送信対象外"

		/// <summary>
		/// 送信ステータス-未送信
		/// </summary>
		public const string TOHISINFO_TRANSFERSTATUS_UNSENT = "00";

		/// <summary>
		/// 送信ステータス-送信済
		/// </summary>
		public const string TOHISINFO_TRANSFERSTATUS_SENT = "01";

		/// <summary>
		/// 送信ステータス-送信対象外
		/// </summary>
		public const string TOHISINFO_TRANSFERSTATUS_SENT_EXEMPT = "02";

		#endregion

		#region 送信結果 OK NG:NG理由
		/// <summary>
		/// 送信結果 OK
		/// </summary>
		public const string TOHISINFO_TRANSFERRESULT_NORMAL = "OK";

		/// <summary>
		/// 送信結果 NG:NG理由
		/// </summary>
		public const string TOHISINFO_TRANSFERRESULT_ERROR = "NG:";

		#endregion

		#endregion

	}
}
