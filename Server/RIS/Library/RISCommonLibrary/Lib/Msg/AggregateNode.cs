using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Exceptions;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// 集合ノード
	/// </summary>
	/// <remarks>さまざまな項目の集合</remarks>
	public class AggregateNode : CollectionNode
	{
		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AggregateNode(): base()
		{

		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AggregateNode(NodeInfo define)
			: base(define)
		{
		}

		#endregion

		#region method
		
		/// <summary>
		/// 子ノードを追加する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <returns>子ノードのインデックス</returns>
		public override int Add(BaseNode child)
		{
			BaseNode node = GetNodeByName(child.Name);
			if (node != null)
			{
				throw new NodeOparationException(string.Format(
					"同名の子ノードが既に存在します[{0}]", child.Name), this);
			}
			return base.Add(child);
		}

		/// <summary>
		/// 子ノードを追加し、変数に代入する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <param name="pRefVar">参照変数</param>
		/// <returns>子ノードのインデックス</returns>
		public int AddNode(BaseNode child, ref BaseNode refVar)
		{
			refVar = child;
			return Add(child);
		}

		/// <summary>
		/// 子ノードを追加し、変数に代入する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <param name="pRefVar">参照変数</param>
		/// <returns>子ノードのインデックス</returns>
		public int AddNode(DataNode child, ref DataNode refVar)
		{
			refVar = child;
			return Add(child);
		}

		/// <summary>
		/// 子ノードを追加し、変数に代入する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <param name="pRefVar">参照変数</param>
		/// <returns>子ノードのインデックス</returns>
		public DataNode AddChildNode(DataNode child)
		{
			Add(child);
			return child;
		}

		/// <summary>
		/// 子ノードを追加し、変数に代入する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <param name="pRefVar">参照変数</param>
		/// <returns>子ノードのインデックス</returns>
		public ArrayNode AddChildNode(ArrayNode child)
		{
			Add(child);
			return child;
		}

		/// <summary>
		/// 子ノードを追加し、変数に代入する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <param name="pRefVar">参照変数</param>
		/// <returns>子ノードのインデックス</returns>
		public AggregateNode AddChildNode(AggregateNode child)
		{
			Add(child);
			return child;
		}

		#endregion
	}
}
