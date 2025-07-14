using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Hospitalize
{
	/// <summary>
	/// 入退院情報ハンドリングクラス
	/// </summary>
	public class HospitalizeMsg : BaseMsg
	{
		#region field
		#endregion

		#region property

		/// <summary>
		/// 対象電文名
		/// </summary>
		public override string MessageNameJ
		{
			get
			{
				return "入退院情報";
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public override string[] TelegraphKinds
		{
			get
			{
				return new[] { MsgConst.TELEGRAPH_KIND_HOSPITALIZE };
			}
		}

		/// <summary>
		/// メッセージ長
		/// </summary>
		public override int MsgLength
		{
			get
			{
				//固定長
				return MsgConst.MSG_LENGTH_HOSPITALIZE;
			}
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>キャストの手間を避けるために作成</remarks>
		public HospitalizeRoot MsgBody
		{
			get
			{
				return (HospitalizeRoot)Body;
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HospitalizeMsg()
			: base()
		{
			Body = new HospitalizeRoot();
			RequestKind = RequestKindEnum.rkHospitalize;
		}
		#endregion
	}
}
