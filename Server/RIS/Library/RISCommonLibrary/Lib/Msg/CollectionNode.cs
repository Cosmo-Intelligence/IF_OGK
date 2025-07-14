using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Exceptions;
using System.Diagnostics;

namespace RISCommonLibrary.Lib.Msg
{

	/// <summary>
	/// データ変更後イベント引数
	/// </summary>
	public class AddEventArgs : EventArgs
	{

		/// <summary>
		/// 変更後データ
		/// </summary>
		public string ChangedData
		{
			get;
			set;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="changedData"></param>
		public AddEventArgs(string changedData)
		{
			ChangedData = changedData;
		}
	}

	/// <summary>
	/// 集合を子供に持つノード
	/// </summary>
	public class CollectionNode : BaseNode
	{

		#region field
		
		/// <summary>
		/// データ変更後イベント
		/// </summary>
		public event EventHandler<AddEventArgs> OnAdd;

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CollectionNode(): base()
		{

		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CollectionNode(NodeInfo define)
			: base(define)
		{
		}

		#endregion

		#region method
		
		/// <summary>
		/// ノードのデータを文字列にする
		/// </summary>
		/// <returns>エンコードされた文字列</returns>
		public override string Encode()
		{
			string r = "";
			for (int idx = 0; idx < this.Count; idx++)
			{
				r += this[idx].Encode();
			};
			return r;
		}

		/// <summary>
		/// 文字列からノードを復元する
		/// </summary>
		/// <param name="src">復元元文字列</param>
		public override void Decode(StringIterator src)
		{
			for (int idx = 0; idx < this.Count; idx++)
			{
				this[idx].Decode(src);
			};

			if ((this.Root == this) && (!src.EOF))
			{
				//長すぎても無視するだけにする
////				throw new NodeOparationException("デコード元データが長すぎます", this);
				Trace.WriteLine("デコード元データが長すぎます");
			}
		}

		/// <summary>
		/// 子ノードを追加する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <returns>子ノードのインデックス</returns>
		public override int Add(BaseNode child)
		{
			child.Parent = this;
			this.Nodes.Add(child);
			int addedIndex = this.Nodes.IndexOf(child);
			EventHandler<AddEventArgs> tempEvent = OnAdd;
			if (tempEvent != null)
			{
				tempEvent(this, new AddEventArgs(this.Nodes.Count.ToString()));
			}
			SetDataLength();
			return addedIndex;
		}

		/// <summary>
		/// 指定されたインデックスの子ノードを削除する
		/// </summary>
		/// <param name="index">子ノードのインデックス</param>
		public override void Delete(int index)
		{
			this.Nodes.RemoveAt(index);
			SetDataLength();
		}

		/// <summary>
		/// 子ノードをクリアする
		/// </summary>
		public override void Clear()
		{
			this.Nodes.Clear();
		}

		/// <summary>
		/// ノードの表示
		/// </summary>
		public override string Snapshot
		{
			get
			{
				return string.Format("{0}-{1}: COUNT={2}", this.NameJ, this.Name, this.Count);
			}
		}

		/// <summary>
		/// 名前を指定して子ノードを取得する
		/// </summary>
		/// <param name="nodeName">ノード名</param>
		/// <returns>子ノード</returns>
		/// <remarks>名前が一致する子ノードがなければnilを返す</remarks>
		public override BaseNode GetNodeByName(string nodeName)
		{
			BaseNode node = null;
			for (int idx = 0; idx < this.Count; idx++)
			{
				node = this[idx];
				if (node.Name != nodeName)
				{
					continue;
				}
				return node;
			}
			return null;
		}

		/// <summary>
		/// データを取得する
		/// </summary>
		public override string Data
		{
			get
			{
				throw new NodeOparationException("データノード以外では、取得できません", this);
			}
			set
			{
				throw new NodeOparationException("データノード以外では、設定できません", this);
			}
		}

		/// <summary>
		/// トリムされたデータ
		/// </summary>
		public override string TrimData
		{
			get
			{
				throw new NodeOparationException("データノード以外では、取得できません", this);
			}
		}

		/// <summary>
		/// ノードのサイズを取得する
		/// </summary>
		/// <remarks>子ノードのサイズを再帰的に取得し、合計する</remarks>
		public override int Size
		{
			get
			{
				int r = 0;
				for (int idx = 0; idx < this.Count; idx++)
				{
					r += this[idx].Size;
				}
				return r;
			}
		}

		/// <summary>
		/// データ長設定
		/// </summary>
		protected void SetDataLength()
		{
			BaseRootNode root = this.Root as BaseRootNode;
			if (root == null)
			{
				return;
			}
			root.ReCalcDataLength();
		}
		#endregion
	}
}
