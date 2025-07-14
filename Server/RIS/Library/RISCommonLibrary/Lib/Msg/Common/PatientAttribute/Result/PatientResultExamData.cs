using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute.Result
{
	/// <summary>
	/// 患者部明細行結果値検索結果
	/// </summary>
	public class PatientResultExamData : PatientResultItemBase
	{
		#region const
		/// <summary>
		/// 設定タイプ-検索結果
		/// </summary>
		private const string CONFIG_TYPE_EXAM_DATA = "1";

		/// <summary>
		/// 検査コード
		/// </summary>
		private const int EXAM_CODE_INDEX = 1;

		/// <summary>
		/// 検査名称
		/// </summary>
		private const int EXAM_NAME_INDEX = EXAM_CODE_INDEX + 1;

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
				return CONFIG_TYPE_EXAM_DATA;
			}
		}

		/// <summary>
		/// 検査コード
		/// </summary>
		public string ExamCode
		{
			get
			{
				return GetValueFromList(EXAM_CODE_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, EXAM_CODE_INDEX, SrcList);
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

		#region constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list"></param>
		public PatientResultExamData()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PatientResultExamData(List<string> list)
			: base(list)
		{
		}
		#endregion

		#region method

		/// <summary>
		/// ExamData用文字列を取得する
		/// </summary>
		/// <returns></returns>
		public string ToStringByExamData()
		{
			const string FORMAT_EXAMDATA = "{0}:{1}<{2}>\r\n";
			return string.Format(FORMAT_EXAMDATA, this.ExamName, 
				this.ExamResult, this.ExamDate);
		}

		protected override void SetStringByValues(string[] values)
		{
			if (values.Length < 5)
			{
				return;
			}
			ExamCode = values[1];
			ExamName = values[2];
			ExamResult = values[3];
			ExamDate = values[4];
		}

		protected override string[] GetStringByValues()
		{
			return new[] { ConfigType, ExamCode, ExamName, ExamResult, ExamDate };
		}

		#endregion
	}
}
