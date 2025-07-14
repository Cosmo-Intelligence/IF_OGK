using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute
{
	/// <summary>
	/// 患者属性部のノード定義
	/// </summary>
	class PatientAttributeNodeInfo
	{
		public static NodeInfo PATIENT_ATTRIBUTE_ROOT = new NodeInfo("PATIENT_ATTRIBUTE", "患者属性部", NodeTypeEnum.ntAggregate, -1);

		public static NodeInfo PATIENT_ATTRIBUTE_PROCESSING_TYPE = new NodeInfo("PROCESSING_TYPE", "処理タイプ", NodeTypeEnum.ntData, 1);
		public static NodeInfo PATIENT_ATTRIBUTE_PATIENT_NO = new NodeInfo("PATIENT_NO", "患者番号", NodeTypeEnum.ntData, 10);
		public static NodeInfo PATIENT_ATTRIBUTE_PATIENT_INOUT = new NodeInfo("PATIENT_INOUT", "患者入外区分", NodeTypeEnum.ntData, 1);
		public static NodeInfo PATIENT_ATTRIBUTE_WARD_CODE = new NodeInfo("WARD_CODE", "病棟コード", NodeTypeEnum.ntData, 3);
		public static NodeInfo PATIENT_ATTRIBUTE_ROOM_CODE = new NodeInfo("ROOM_CODE", "病室コード", NodeTypeEnum.ntData, 5);
		public static NodeInfo PATIENT_ATTRIBUTE_ADMISSION_SECTION_CODE = new NodeInfo("ADMISSION_SECTION_CODE", "入院科コード", NodeTypeEnum.ntData, 3);
		public static NodeInfo PATIENT_ATTRIBUTE_KANA_NAME = new NodeInfo("KANA_NAME", "患者カナ氏名", NodeTypeEnum.ntData, 40);
		public static NodeInfo PATIENT_ATTRIBUTE_KANJI_NAME = new NodeInfo("KANJI_NAME", "患者漢字氏名", NodeTypeEnum.ntData, 40);
		public static NodeInfo PATIENT_ATTRIBUTE_SEX = new NodeInfo("SEX", "性別", NodeTypeEnum.ntData, 1);
		public static NodeInfo PATIENT_ATTRIBUTE_BIRTH_DATE = new NodeInfo("BIRTH_DATE", "生年月日", NodeTypeEnum.ntData, 8);
		public static NodeInfo PATIENT_ATTRIBUTE_ADDRESS1 = new NodeInfo("ADDRESS1", "住所1", NodeTypeEnum.ntData, 60);
		public static NodeInfo PATIENT_ATTRIBUTE_ADDRESS2 = new NodeInfo("ADDRESS2", "住所2", NodeTypeEnum.ntData, 60);
		public static NodeInfo PATIENT_ATTRIBUTE_HEIGHT = new NodeInfo("HEIGHT", "身長", NodeTypeEnum.ntData, 5);
		public static NodeInfo PATIENT_ATTRIBUTE_HEIGHT_MEASURE_DATE = new NodeInfo("HEIGHT_MEASURE_DATE", "身長測定日", NodeTypeEnum.ntData, 8);
		public static NodeInfo PATIENT_ATTRIBUTE_WEIGHT = new NodeInfo("WEIGHT", "体重", NodeTypeEnum.ntData, 5);
		public static NodeInfo PATIENT_ATTRIBUTE_WEIGHT_MEASURE_DATE = new NodeInfo("WEIGHT_MEASURE_DATE", "体重測定日", NodeTypeEnum.ntData, 8);
		public static NodeInfo PATIENT_ATTRIBUTE_ATTENDING_CODE = new NodeInfo("ATTENDING_CODE", "主治医コード", NodeTypeEnum.ntData, 6);
		public static NodeInfo PATIENT_ATTRIBUTE_ATTENDING_NAME = new NodeInfo("ATTENDING_NAME", "主治医名", NodeTypeEnum.ntData, 40);
		public static NodeInfo PATIENT_ATTRIBUTE_RE_RECEIPT = new NodeInfo("RE_RECEIPT", "再来受付", NodeTypeEnum.ntData, 1);
		public static NodeInfo PATIENT_ATTRIBUTE_ACCOUNT = new NodeInfo("ACCOUNT", "会計", NodeTypeEnum.ntData, 1);
		public static NodeInfo PATIENT_ATTRIBUTE_COMMUNITY_HEALTHCARE_PATIENT_ID = new NodeInfo("COMMUNITY_HEALTHCARE_PATIENT_ID", "地域医療患者ＩＤ", NodeTypeEnum.ntData, 11);
		public static NodeInfo PATIENT_ATTRIBUTE_RESERVE1 = new NodeInfo("RESERVE1", "予備", NodeTypeEnum.ntData, 10);
		public static NodeInfo PATIENT_ATTRIBUTE_RESULT_VALUE = new NodeInfo("RESULT_VALUE", "結果値", NodeTypeEnum.ntData, 1503);
		public static NodeInfo PATIENT_ATTRIBUTE_RESERVE2 = new NodeInfo("RESERVE2", "予備", NodeTypeEnum.ntData, 50);
	}
}
