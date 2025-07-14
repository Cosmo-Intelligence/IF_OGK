using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute.Result
{
	/// <summary>
	/// 患者部明細行結果値アレルギー
	/// </summary>
	public class PatientResultAllergy : PatientResultItemBase
	{

		#region const
		/// <summary>
		/// 設定タイプ-アレルギー
		/// </summary>
		private const string CONFIG_TYPE_ALLERGY = "4";

		/// <summary>
		/// コードインデックス
		/// </summary>
		private const int CODE_INDEX = 1;

		/// <summary>
		/// 名称インデックス
		/// </summary>
		private const int NAME_INDEX = CODE_INDEX + 1;

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
				return CONFIG_TYPE_ALLERGY;
			}
		}

		/// <summary>
		/// コード
		/// </summary>
		public string Code
		{
			get
			{
				return GetValueFromList(CODE_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, CODE_INDEX, SrcList);
			}
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			get
			{
				return GetValueFromList(NAME_INDEX, SrcList);
			}
			set
			{
				SetValueToList(value, NAME_INDEX, SrcList);
			}
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list"></param>
		public PatientResultAllergy(): base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PatientResultAllergy(List<string> list)
			: base(list)
		{
		}

		#endregion

		protected override void SetStringByValues(string[] values)
		{
			if (values.Length < 3)
			{
				return;
			}
			Code = values[1];
			Name = values[2];
		}

		protected override string[] GetStringByValues()
		{
			return new[] { ConfigType, Code, Name };
		}
	}
}
