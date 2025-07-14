using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Utils;

namespace RISCommonLibrary.Lib.Msg.Common
{
	/// <summary>
	/// ルート基底
	/// </summary>
	public class CommonRootNode : BaseRootNode, ICommunicationControl
	{
		#region field
		
		#endregion

		#region property
		
		/// <summary>
		/// 通信制御部
		/// </summary>
		public CommunicationControlAggregate CommunicationControl
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CommonRootNode(NodeInfo define)
			: base(define)
		{
			CommunicationControl = new CommunicationControlAggregate();
			Add(CommunicationControl);
		}
		#endregion

		#region method

		/// <summary>
		/// データ長を再計算する
		/// </summary>
		public override void ReCalcDataLength()
		{
			CommunicationControl.DATA_LENGTH.Data = this.Size.ToString();
		}

		/// <summary>
		/// 電文長
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public override string GetDataLength(string src)
		{
			return MsgUtils.GetDataLength(src);
		}

		/// <summary>
		/// 電文種別取得
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public override string GetTelegraphKind(string src)
		{
			return MsgUtils.GetTelegraphKind(src);
		}
		#endregion
	}
}
