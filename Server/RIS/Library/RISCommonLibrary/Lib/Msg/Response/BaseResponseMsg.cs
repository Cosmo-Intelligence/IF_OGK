using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg.Common;

namespace RISCommonLibrary.Lib.Msg.Response
{

	/// <summary>
	/// 応答電文既定ハンドリングクラス
	/// </summary>
	public abstract class BaseResponseMsg : BaseMsg
	{
		#region field
		#endregion

		#region property

		/// <summary>
		/// 対象電文名
		/// </summary>
		public override abstract string MessageNameJ
		{
			get;
		}

		/// <summary>
		/// メッセージ長
		/// </summary>
		public override abstract int MsgLength
		{
			get;
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>応答処理作成の都合で基底に持たせた</remarks>
		public CommonRootNode MsgBody
		{
			get
			{
				return (CommonRootNode)Body;
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public override string[] TelegraphKinds
		{
			get
			{
				return new[] { MsgConst.TELEGRAPH_KIND_RESPONSE_OK,
								MsgConst.TELEGRAPH_KIND_RESPONSE_NG};
			}
		}
		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BaseResponseMsg()
			: base()
		{
		}
		#endregion

		#region method

		#endregion


	}
}
