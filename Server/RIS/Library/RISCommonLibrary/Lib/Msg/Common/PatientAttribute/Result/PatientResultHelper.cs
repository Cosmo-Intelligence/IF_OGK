using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Exceptions;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute.Result
{
	/// <summary>
	/// 患者部明細行結果値ヘルパー
	/// </summary>
	public class PatientResultHelper
	{
		#region field
		
		/// <summary>
		/// 結果値
		/// </summary>
		private string _resultValue;

		/// <summary>
		/// 結果値クラスファクトリリスト
		/// </summary>
		private List<PatientResultItemBase> _resultItemFactory;
		#endregion

		#region propetry

		/// <summary>
		/// 結果値
		/// </summary>
		public string ResultValue
		{
			get
			{
				return _resultValue;
			}
			set
			{
				_resultValue = value;

				#region 振り分け処理
				
				List<string> list = GetListByResultValue(_resultValue);
				ExamDataList.Clear();
				BloodList.Clear();
				InfectionList.Clear();
				AllergyList.Clear();
				List<string> resultitemList;
				foreach (string item in list)
				{
					resultitemList = GetListByResultItemValue(item);
					if (resultitemList.Count < 1)
					{
						throw new MsgNotNullException(string.Format(
							"患者部明細行結果値の設定値が設定されていません。{0}", item));
					}
					PatientResultItemBase resultItem = _resultItemFactory.FirstOrDefault(
						ri => ri.CanDecode(resultitemList[0]));
					if (resultItem == null)
					{
						throw new MsgNotNullException(string.Format(
							"患者部明細行結果値の設定値が想定外のものが設定されています。{0}", item));
					}
					if (resultItem is PatientResultExamData)
					{
						ExamDataList.Add(new PatientResultExamData(resultitemList));
						continue;
					}
					if (resultItem is PatientResultBlood)
					{
						BloodList.Add(new PatientResultBlood(resultitemList));
						continue;
					}
					if (resultItem is PatientResultInfection)
					{
						InfectionList.Add(new PatientResultInfection(resultitemList));
						continue;
					}
					if (resultItem is PatientResultAllergy)
					{
						PatientResultAllergy pa = new PatientResultAllergy(resultitemList);
						if (string.IsNullOrEmpty(pa.Name)) //名称がnullのものは省く
						{
							continue;
						}
						if (pa.Name.Trim(new[] { ' ', '　' }) == "")
						{
							continue;
						}
						AllergyList.Add(pa);
						continue;
					}
					throw new MsgNotNullException(string.Format(
						"患者部明細行結果値の設定値が想定外のものが設定されています。{0}", item));
				}
				#endregion

			}
		}


		/// <summary>
		/// 検索結果リスト
		/// </summary>
		public List<PatientResultExamData> ExamDataList
		{
			get;
			private set;
		}

		/// <summary>
		/// 血液型リスト
		/// </summary>
		public List<PatientResultBlood> BloodList
		{
			get;
			private set;
		}

		/// <summary>
		/// 感染症リスト
		/// </summary>
		public List<PatientResultInfection> InfectionList
		{
			get;
			private set;
		}

		/// <summary>
		/// アレルギーリスト
		/// </summary>
		public List<PatientResultAllergy> AllergyList
		{
			get;
			private set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PatientResultHelper()
		{
			ExamDataList = new List<PatientResultExamData>();
			BloodList = new List<PatientResultBlood>();
			InfectionList = new List<PatientResultInfection>();
			AllergyList = new List<PatientResultAllergy>();

			#region ファクトリに登録
			_resultItemFactory = new List<PatientResultItemBase>();
			_resultItemFactory.Add(new PatientResultExamData());
			_resultItemFactory.Add(new PatientResultBlood());
			_resultItemFactory.Add(new PatientResultInfection());
			_resultItemFactory.Add(new PatientResultAllergy());
			#endregion
		}
		#endregion

		#region method

		/// <summary>
		/// 結果値文字列からリスト化する
		/// </summary>
		/// <param name="resultValue"></param>
		/// <returns></returns>
		protected List<string> GetListByResultValue(string resultValue)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(resultValue))
			{
				return list;
			}
			const char RESULT_VALUE_SEPARATOR = '|';
			return resultValue.Split(RESULT_VALUE_SEPARATOR).ToList<string>();
		}

		/// <summary>
		/// 結果値項目文字列からリスト化する
		/// </summary>
		/// <param name="resultItemValue"></param>
		/// <returns></returns>
		private List<string> GetListByResultItemValue(string resultItemValue)
		{
			if (string.IsNullOrEmpty(resultItemValue))
			{
				return new List<string>();
			}
			const char RESULT_VALUE_ITEM_SEPARATOR = ',';
			return resultItemValue.Split(new[] { RESULT_VALUE_ITEM_SEPARATOR }).ToList<string>();
		}

		#endregion
	}
}
