using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute.Result
{
	/// <summary>
	/// 患者部明細行結果値感染症
	/// </summary>
	public class PatientResultInfection : PatientResultItemBase
	{
		#region const
		/// <summary>
		/// 設定タイプ-感染症
		/// </summary>
		private const string CONFIG_TYPE_INFECTION = "3";

		/// <summary>
		/// 結果コード
		/// </summary>
		private const int RESULT_CODE_INDEX = 1;

		/// <summary>
		/// 検査名称
		/// </summary>
		private const int EXAM_NAME_INDEX = RESULT_CODE_INDEX + 1;

		/// <summary>
		/// 検索結果
		/// </summary>
		private const int EXAM_RESULT_INDEX = EXAM_NAME_INDEX + 1;

		/// <summary>
		/// 検査日(YYYYMMDD)
		/// </summary>
		private const int EXAM_DATE_INDEX = EXAM_RESULT_INDEX + 1;
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
				return CONFIG_TYPE_INFECTION;
			}
		}

		/// <summary>
		/// 結果コード
		/// </summary>
		public string ResultCode
		{
			get
			{
				return GetValueFromList(RESULT_CODE_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, RESULT_CODE_INDEX, SrcList);
			}
		}

		/// <summary>
		/// 検査名称
		/// </summary>
		public string ExamName
		{
			get
			{
				return GetValueFromList(EXAM_NAME_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_NAME_INDEX, SrcList);
			}
		}

		/// <summary>
		/// 検索結果
		/// </summary>
		public string ExamResult
		{
			get
			{
				return GetValueFromList(EXAM_RESULT_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_RESULT_INDEX, SrcList);
			}
		}

		/// <summary>
		/// 検査日(YYYYMMDD)
		/// </summary>
		public string ExamDate
		{
			get
			{
				return GetValueFromList(EXAM_DATE_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_DATE_INDEX, SrcList);
			}
		}
		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list"></param>
		public PatientResultInfection()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PatientResultInfection(List<string> list)
			: base(list)
		{
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="values"></param>
		protected override void SetStringByValues(string[] values)
		{
			if (values.Length < 5)
			{
				return;
			}
			ResultCode = values[1];
			ExamName = values[2];
			ExamResult = values[3];
			ExamDate = values[4];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string[] GetStringByValues()
		{
			return new[] { ConfigType, ResultCode, ExamName, ExamResult, ExamDate };
		}
	}
}
