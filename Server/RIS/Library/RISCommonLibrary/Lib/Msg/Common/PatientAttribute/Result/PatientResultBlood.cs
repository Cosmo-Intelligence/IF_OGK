using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute.Result
{
	/// <summary>
	/// 患者部明細行結果値検索結果
	/// </summary>
	public class PatientResultBlood : PatientResultItemBase
	{
		#region const
		/// <summary>
		/// 設定タイプ-血液型
		/// </summary>
		private const string CONFIG_TYPE_BLOOD = "2";

		/// <summary>
		/// 検索結果ABO
		/// </summary>
		private const int EXAM_RESULT_ABO_INDEX = 1;

		/// <summary>
		/// 検査日ABO(YYYYMMDD)
		/// </summary>
		private const int EXAM_DATE_ABO_INDEX = EXAM_RESULT_ABO_INDEX + 1;

		/// <summary>
		/// 検索結果RH
		/// </summary>
		private const int EXAM_RESULT_RH_INDEX = EXAM_DATE_ABO_INDEX + 1;

		/// <summary>
		/// 検査日RH(YYYYMMDD)
		/// </summary>
		private const int EXAM_DATE_RH_INDEX = EXAM_RESULT_RH_INDEX + 1;
		#endregion

		#region field

		#endregion

		#region property

		/// <summary>
		/// 設定タイプ
		/// </summary>
		public override string ConfigType
		{
			get
			{
				return CONFIG_TYPE_BLOOD;
			}
		}

		/// <summary>
		/// 検索結果ABO
		/// </summary>
		public string ExamResultABO
		{
			get
			{
				return GetValueFromList(EXAM_RESULT_ABO_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_RESULT_ABO_INDEX, SrcList);
			}
		}

		/// <summary>
		/// 検査日ABO(YYYYMMDD)
		/// </summary>
		public string ExamDateABO
		{
			get
			{
				return GetValueFromList(EXAM_DATE_ABO_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_DATE_ABO_INDEX, SrcList);
			}
		}

		/// <summary>
		/// 検索結果RH
		/// </summary>
		public string ExamResultRH
		{
			get
			{
				return GetValueFromList(EXAM_RESULT_RH_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_RESULT_RH_INDEX, SrcList);
			}
		}

		/// <summary>
		/// 検査日RH(YYYYMMDD)
		/// </summary>
		public string ExamDateRH
		{
			get
			{
				return GetValueFromList(EXAM_DATE_RH_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_DATE_RH_INDEX, SrcList);
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list"></param>
		public PatientResultBlood()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PatientResultBlood(List<string> list)
			: base(list)
		{
		}
		#endregion

		protected override void SetStringByValues(string[] values)
		{
			if (values.Length < 5)
			{
				return;
			}
			ExamResultABO = values[1];
			ExamDateABO = values[2];
			ExamResultRH = values[3];
			ExamDateRH = values[4];
		}

		protected override string[] GetStringByValues()
		{
			return new[] { ConfigType, ExamResultABO, ExamDateABO, ExamResultRH, ExamDateRH };
		}

	}
}
