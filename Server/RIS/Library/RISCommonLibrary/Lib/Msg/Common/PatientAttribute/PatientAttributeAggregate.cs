using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute
{
	/// <summary>
	/// 患者属性部
	/// </summary>
	public class PatientAttributeAggregate : AggregateNode
	{
		#region property
		/// <summary>
		/// 処理タイプ
		/// </summary>
		public DataNode PROCESSING_TYPE
		{
			get;
			set;
		}

		/// <summary>
		/// 患者番号
		/// </summary>
		public DataNode PATIENT_NO
		{
			get;
			set;
		}

		/// <summary>
		/// 患者入外区分
		/// </summary>
		public DataNode PATIENT_INOUT
		{
			get;
			set;
		}

		/// <summary>
		/// 病棟コード
		/// </summary>
		public DataNode WARD_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 病室コード
		/// </summary>
		public DataNode ROOM_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 入院科コード
		/// </summary>
		public DataNode ADMISSION_SECTION_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 患者カナ氏名
		/// </summary>
		public DataNode KANA_NAME
		{
			get;
			set;
		}

		/// <summary>
		/// 患者漢字氏名
		/// </summary>
		public DataNode KANJI_NAME
		{
			get;
			set;
		}

		/// <summary>
		/// 性別
		/// </summary>
		public DataNode SEX
		{
			get;
			set;
		}

		/// <summary>
		/// 生年月日
		/// </summary>
		public DataNode BIRTH_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// 住所1
		/// </summary>
		public DataNode ADDRESS1
		{
			get;
			set;
		}

		/// <summary>
		/// 住所2
		/// </summary>
		public DataNode ADDRESS2
		{
			get;
			set;
		}

		/// <summary>
		/// 身長
		/// </summary>
		public DataNode HEIGHT
		{
			get;
			set;
		}

		/// <summary>
		/// 身長測定日
		/// </summary>
		public DataNode HEIGHT_MEASURE_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// 体重
		/// </summary>
		public DataNode WEIGHT
		{
			get;
			set;
		}

		/// <summary>
		/// 体重測定日
		/// </summary>
		public DataNode WEIGHT_MEASURE_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// 主治医コード
		/// </summary>
		public DataNode ATTENDING_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 主治医名
		/// </summary>
		public DataNode ATTENDING_NAME
		{
			get;
			set;
		}

		/// <summary>
		/// 再来受付
		/// </summary>
		public DataNode RE_RECEIPT
		{
			get;
			set;
		}

		/// <summary>
		/// 会計
		/// </summary>
		public DataNode ACCOUNT
		{
			get;
			set;
		}

		/// <summary>
		/// 地域医療患者ＩＤ
		/// </summary>
		public DataNode COMMUNITY_HEALTHCARE_PATIENT_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 予備
		/// </summary>
		public DataNode RESERVE1
		{
			get;
			set;
		}


		/// <summary>
		/// 予備
		/// </summary>
		public DataNode RESULT_VALUE
		{
			get;
			set;
		}
		/// <summary>
		/// 予備
		/// </summary>
		public DataNode RESERVE2
		{
			get;
			set;
		}

		#endregion

		#region constructor
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PatientAttributeAggregate()
			: base(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ROOT)
		{
			PROCESSING_TYPE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_PROCESSING_TYPE));
			PATIENT_NO = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_PATIENT_NO));
			PATIENT_INOUT = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_PATIENT_INOUT));
			WARD_CODE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_WARD_CODE));
			ROOM_CODE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ROOM_CODE));
			ADMISSION_SECTION_CODE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ADMISSION_SECTION_CODE));
			KANA_NAME = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_KANA_NAME));
			KANJI_NAME = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_KANJI_NAME));
			SEX = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_SEX));
			BIRTH_DATE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_BIRTH_DATE));
			ADDRESS1 = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ADDRESS1));
			ADDRESS2 = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ADDRESS2));
			HEIGHT = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_HEIGHT));
			HEIGHT_MEASURE_DATE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_HEIGHT_MEASURE_DATE));
			WEIGHT = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_WEIGHT));
			WEIGHT_MEASURE_DATE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_WEIGHT_MEASURE_DATE));
			ATTENDING_CODE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ATTENDING_CODE));
			ATTENDING_NAME = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ATTENDING_NAME));
			RE_RECEIPT = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_RE_RECEIPT));
			ACCOUNT = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_ACCOUNT));
			COMMUNITY_HEALTHCARE_PATIENT_ID = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_COMMUNITY_HEALTHCARE_PATIENT_ID));
			RESERVE1 = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_RESERVE1));
			RESULT_VALUE = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_RESULT_VALUE));
			RESERVE2 = AddChildNode(new DataNode(PatientAttributeNodeInfo.PATIENT_ATTRIBUTE_RESERVE2));
		}
		#endregion
	}
}
