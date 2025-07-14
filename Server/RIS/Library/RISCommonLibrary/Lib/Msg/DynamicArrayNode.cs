using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// 配列数を動的に指定するノード
	/// </summary>
	/// <remarks>
	/// 同じ項目の繰り返しを行う
	/// 電文中に出てこない
	/// </remarks>
	public class DynamicArrayNode : CollectionNode, IArrayNode
	{
		#region field

		/// <summary>
		/// 表示フォーマット
		/// </summary>
		private string _formatDisplay;

		#endregion

		#region property

		/// <summary>
		/// 子供の数
		/// </summary>
		public int ChildCount
		{
			get;
			set;
		}

		/// <summary>
		/// 表示フォーマット
		/// </summary>
		public string FormatDisplay
		{
			get
			{
				if (string.IsNullOrEmpty(_formatDisplay))
				{
					_formatDisplay = MsgUtils.GetFormatZeroPading(this.NodeInfo.FieldLength);
				}
				return _formatDisplay;
			}
		}
		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DynamicArrayNode(NodeInfo define)
			: base(define)
		{

		}

		#endregion

		#region method

		/// <summary>
		/// 文字列からノードを復元する
		/// </summary>
		/// <param name="src">復元元文字列</param>
		/// <remarks></remarks>
		public override void Decode(StringIterator src)
		{
			CreateElements();

			base.Decode(src);
		}

		/// <summary>
		/// ノードのデータを文字列にする
		/// </summary>
		/// <returns>エンコードされた文字列</returns>
		/// <remarks>電文中に無いのでそのまま返す</remarks>
		public override string Encode()
		{
			return base.Encode();
		}

		/// <summary>
		/// ノード表示文字列
		/// </summary>
		public override string Snapshot
		{
			get
			{
				return string.Format("{0}-{1}: DATA={2}", this.NameJ, this.Name,
					string.Format(this.FormatDisplay, this.Count));
			}
		}

		/// <summary>
		/// 繰り返すAggクラス
		/// </summary>
		/// <returns></returns>
		public virtual Type GetElementClass()
		{
			return null;
		}

		/// <summary>
		/// ノードクラスを指定して要素ノードを生成する
		/// </summary>
		/// <param name="nodeClass">ノードクラス</param>
		public void CreateElements(Type nodeClass)
		{
			while (this.Count < ChildCount)
			{
				object o = Activator.CreateInstance(nodeClass);
				base.Add((BaseNode)o);
			}
		}

		/// <summary>
		/// ノードクラスを指定して要素ノードを生成する
		/// </summary>
		public void CreateElements()
		{
			CreateElements(GetElementClass());
		}

		/// <summary>
		/// 子ノードを追加する
		/// </summary>
		/// <returns>子ノードのインデックス</returns>
		public int Add()
		{
			object o = Activator.CreateInstance(GetElementClass());
			SetDataLength();
			return Add((BaseNode)o);
		}

		#endregion

	}
}
