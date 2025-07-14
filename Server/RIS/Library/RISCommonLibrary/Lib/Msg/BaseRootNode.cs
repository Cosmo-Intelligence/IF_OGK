using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// ルート基底
	/// </summary>
	public abstract class BaseRootNode: AggregateNode
	{
		#region field
		
		#endregion

		#region property
		
		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BaseRootNode(NodeInfo define)
			: base(define)
		{
		}
		#endregion

		#region method

		/// <summary>
		/// データ長を再計算する
		/// </summary>
		public abstract void ReCalcDataLength();

		/// <summary>
		/// 電文長を取得する
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public abstract string GetDataLength(string src);

		/// <summary>
		/// 電文種別を取得する
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public abstract string GetTelegraphKind(string src);

		#endregion
	}
}
