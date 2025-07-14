using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute.Result
{
	public abstract class PatientResultItemBase: ICloneable
	{
		#region const
		/// <summary>
		/// 結果値のデータ項目分割文字
		/// </summary>
		protected const char SEPARATOR = ',';
		#endregion

		#region field

		/// <summary>
		/// 元になるList
		/// </summary>
		private List<string> _srcList;
		#endregion

		#region property
		/// <summary>
		/// 設定値
		/// </summary>
		public abstract string ConfigType
		{
			get;
		}

		/// <summary>
		/// データリスト
		/// </summary>
		public  List<string> SrcList
		{
			get
			{
				return _srcList;
			}
		}

		#endregion

		#region constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list"></param>
		public PatientResultItemBase()
		{
			_srcList = new List<string>();
			_srcList.Add(ConfigType);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list"></param>
		public PatientResultItemBase(List<string> list)
		{
			_srcList = list;
		}

		#endregion

		#region method

		/// <summary>
		/// デコードできるか？
		/// </summary>
		/// <param name="configType"></param>
		/// <returns></returns>
		public bool CanDecode(string configType)
		{
			return ConfigType == configType;
		}

		/// <summary>
		/// 設定文字列
		/// </summary>
		/// <returns></returns>
		public virtual void SetString(string srcString)
		{
			if (string.IsNullOrEmpty(srcString))
			{
				return;
			}
			string[] values = srcString.Split(new[] { SEPARATOR });

			SetStringByValues(values);
		}

		/// <summary>
		/// 配列によって設定
		/// </summary>
		/// <param name="values"></param>
		protected abstract void SetStringByValues(string[] values);

		/// <summary>
		/// 文字列取得
		/// </summary>
		/// <returns></returns>
		public virtual string GetString()
		{
			return string.Join(SEPARATOR.ToString(), GetStringByValues());
		}

		/// <summary>
		/// クローン
		/// </summary>
		/// <returns></returns>
		public PatientResultItemBase Clone()
		{
			return (PatientResultItemBase)this.MemberwiseClone();
		}
		#region ICloneable メンバ

		/// <summary>
		/// クローン
		/// </summary>
		/// <returns></returns>
		object ICloneable.Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion

		/// <summary>
		/// 配列によって設定
		/// </summary>
		/// <param name="values"></param>
		protected abstract string[] GetStringByValues();


		protected string GetValueFromList(int index, List<string> list)
		{
			if (list == null)
			{
				return "";
			}
			if (list.Count < index)
			{
				return "";
			}
			return list[index];
		}

		protected void SetValueToList(string value, int index, List<string> list)
		{
			if (list.Count < index)
			{
				for (int idxList = 0; idxList <= index; idxList++)
				{
					list.Add("");
				}
			}
			list[index] = value;
		}


			//    if (SrcList.Count < 1)
			//    {
			//        return "";
			//    }
			//    return SrcList[1];
			//}
			//set
			//{
			//    if (SrcList.Count < 1)
			//    {
			//        SrcList.Add("");
			//    }
			//    SrcList[1] = value;

		//private string GetValueFromSeparatedString(string srcString, int index)
		//{
		//    if (string.IsNullOrEmpty(srcString))
		//    {
		//        return "";
		//    }
		//    string[] values = srcString.Split(new[] { SEPARATOR });
		//    if (values.Length -1 < index)
		//    {
		//        return "";
		//    }
		//    return values[index];
		//}

		//private string SetValueFromSeparatedString(string setValue, int index)
		//{
		//    if (string.IsNullOrEmpty(srcString))
		//    {
		//        return "";
		//    }
		//    const char SEPARATOR = ",";
		//    string[] values = srcString.Split(new[] { SEPARATOR });
		//    if (values.Length -1 < index)
		//    {
		//        return "";
		//    }
		//    return values[index];
		//}
	
		#endregion

	}
}
