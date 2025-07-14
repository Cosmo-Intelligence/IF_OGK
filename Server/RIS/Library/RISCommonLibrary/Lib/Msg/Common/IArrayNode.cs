using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	public interface IArrayNode
	{
		/// <summary>
		/// 子ノードを追加する
		/// </summary>
		/// <returns>子ノードのインデックス</returns>
		int Add();
	}
}
