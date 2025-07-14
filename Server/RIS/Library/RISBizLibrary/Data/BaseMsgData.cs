using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace RISBizLibrary.Data
{
	/// <summary>
	/// メッセージのデータを保持する
	/// </summary>
	public abstract class BaseMsgData
	{
		#region field

		#endregion

		#region property

		#region 外からもらってくるもの

		#endregion

		#region メッセージ

		/// <summary>
		/// 要求メッセージ
		/// </summary>
		public BaseMsg Request
		{
			get;
			set;
		}

		/// <summary>
		/// 応答メッセージ
		/// </summary>
		public BaseResponseMsg Response
		{
			get;
			set;
		}

		#endregion

		#endregion

		#region method

		/// <summary>
		/// クリアする
		/// </summary>
		public virtual void Clear()
		{
		}
		#endregion

	}
}
