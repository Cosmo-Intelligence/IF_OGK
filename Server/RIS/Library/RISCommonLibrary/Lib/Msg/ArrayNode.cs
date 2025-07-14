using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Exceptions;
using System.Collections;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// 配列ノード
	/// </summary>
	/// <remarks>同じ項目の繰り返しを行う</remarks>
	public class ArrayNode : CollectionNode, IArrayNode
	{
		#region field
		/// <summary>
		/// 子供の数
		/// </summary>
		private string _childCountString = "0";

		/// <summary>
		/// 表示フォーマット
		/// </summary>
		private string _formatDisplay;

		#endregion

		#region property
		
		/// <summary>
		/// ノードのサイズを取得する
		/// </summary>
		/// <remarks>ノードのサイズ</remarks>
		public override int Size
		{
			get
			{
				int r = base.Size;
				return r + this.NodeInfo.FieldLength;
			}
		}

		/// <summary>
		/// 子供の数
		/// </summary>
		public int ChildCount
		{
			get
			{
				int childCountInt;
				if (!int.TryParse(_childCountString, out childCountInt))
				{
					return 0;
				}
				return childCountInt;
			}
			protected set
			{
				_childCountString = value.ToString();
			}
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
		public ArrayNode(NodeInfo define)
			: base(define)
		{
		}

		#endregion

		#region method
		
		/// <summary>
		/// 文字列からノードを復元する
		/// </summary>
		/// <param name="src">復元元文字列</param>
		public override void Decode(StringIterator src)
		{
			if (src.EOF)
			{
				throw new NodeOparationException("デコードできるデータが存在しません", this);
			}

			int gotLength = src.GetNext(this.NodeInfo.FieldLength);
			_childCountString = src.Current;

			if (gotLength < this.NodeInfo.FieldLength)
			{
				throw new NodeOparationException("データが足りません", this);
			}

			//子供の要素数を設定
			int childCount;
			if (!int.TryParse(_childCountString, out childCount))
			{
				_childCountString = "0";
				childCount = 0;
				//配列数に空文字設定がありうるらしい
				//throw new NodeOparationException(string.Format("配列要素数が不正です:{0}", _childCountString), this);
			}

			if (childCount == 0)
			{
				return;
			}

			if (src.EOF)
			{
				throw new NodeOparationException("デコードできるデータが存在しません", this);
			}

			CreateElements();

			base.Decode(src);
		}

		/// <summary>
		/// ノードのデータを文字列にする
		/// </summary>
		/// <returns>エンコードされた文字列</returns>
		public override string Encode()
		{
			string lengthString = string.Format(this.FormatDisplay, this.Count);
			lengthString += base.Encode();
			return lengthString;
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
		/// 要素のクラスを取得する
		/// </summary>
		/// <returns>抽象ノードには要素クラスがないのでnullを返す</returns>
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
			int childCount;
			if (!int.TryParse(_childCountString, out childCount))
			{
				return;
			}
			while (this.Count < childCount)
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
