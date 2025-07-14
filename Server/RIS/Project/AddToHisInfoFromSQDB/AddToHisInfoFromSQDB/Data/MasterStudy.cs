using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Utils;

namespace AddToHisInfoFromSQDB.Data
{
	/// <summary>
	/// MasterStudyテーブルデータクラス
	/// </summary>
	internal class MasterStudy
	{
		/// <summary>
		/// スタディインスタンスUID
		/// </summary>
		public string StudyInstanceUID
		{
			get;
			set;
		}

		/// <summary>
		/// アクセッションNo
		/// </summary>
		public string AccessionNumber
		{
			get;
			set;
		}

		/// <summary>
		/// 登録日付時刻
		/// </summary>
		public string InsertDatetime
		{
			get;
			set;
		}

		/// <summary>
		/// ログ出力用文字列取得
		/// </summary>
		/// <returns></returns>
		public String ToStringForLog()
		{
			const String FORMAT_LOG = "StudyInstanceUID={0}, AccessionNumber={1}, InsertDatetime={2}";
			return String.Format(FORMAT_LOG, StudyInstanceUID, AccessionNumber, InsertDatetime);
		}


		//objと自分自身が等価のときはtrueを返す
		public override bool Equals(object obj)
		{
			//objがnullか、型が違うときは、等価でない
			if (obj == null || this.GetType() != obj.GetType())
			{
				return false;
			}
			MasterStudy m = (MasterStudy)obj;

			return (this.StudyInstanceUID == m.StudyInstanceUID) &&
				(this.AccessionNumber == m.AccessionNumber) &&
				(this.InsertDatetime == m.InsertDatetime);
		}

		//Equalsがtrueを返すときに同じ値を返す
		public override int GetHashCode()
		{
			return this.StudyInstanceUID.StringToString().GetHashCode() ^
				this.AccessionNumber.StringToString().GetHashCode() ^
				this.InsertDatetime.StringToString().GetHashCode();
		}

	}
}
