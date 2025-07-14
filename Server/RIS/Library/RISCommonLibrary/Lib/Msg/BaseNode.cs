using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Utils;

namespace RISCommonLibrary.Lib.Msg
{

	/// <summary>
	/// ノード既定
	/// </summary>
	public abstract class BaseNode: IEnumerable<BaseNode>
	{
		#region field

		/// <summary>
		/// ノード定義情報
		/// </summary>
		private NodeInfo _nodeInfo;

		/// <summary>
		/// 親ノード
		/// </summary>
		private BaseNode _parent;

		/// <summary>
		/// 子ノードリスト
		/// </summary>
		private List<BaseNode> _nodes = new List<BaseNode>();

		#endregion

		#region property
		
		public BaseNode Parent
		{
			get
			{
				return _parent;
			}
			set
			{
				_parent = value;
			}
		}

		public List<BaseNode> Nodes
		{
			get
			{
				return _nodes;
			}
			set
			{
				_nodes = value;
			}
		}

		public NodeInfo NodeInfo
		{
			get
			{
				return _nodeInfo;
			}
			set
			{
				_nodeInfo = value;
			}
		}

		public virtual String Name
		{
			get
			{
				if (this.IsArrayChild())
				{
					return Convert.ToString(Index);
				}
				return _nodeInfo.Name;
			}
		}

		public virtual String NameJ
		{
			get
			{
				if (this.IsArrayChild())
				{
					return Convert.ToString(Index);
				}
				return _nodeInfo.NameJ;
			}
		}

		public abstract Int32 Size
		{
			get;
		}

		public abstract String Data
		{
			get;
			set;
		}

		public abstract String TrimData
		{
			get;
		}

		/// <summary>
		/// 親ノード内での自分のインデックスを取得する
		/// </summary>
		/// <remarks>親ノードが存在しなければ-1を返す</remarks>
		public int Index
		{
			get
			{
				if (this.Parent == null)
				{
					return -1;
				}
				return this.Parent.Nodes.IndexOf(this);
			}
		}

		public BaseNode this[int index]
		{
			get
			{
				return _nodes[index];
			}
		}

		/// <summary>
		/// パス文字列を取得する
		/// </summary>
		public string Path
		{
			get
			{
				if (this.Parent == null)
				{
					return this.Name;
				}
				return this.Parent.Path + MsgConst.PATH_DELIMITER + this.Name;
			}
		}

		/// <summary>
		/// ノード表示文字列
		/// </summary>
		public abstract string Snapshot
		{
			get;
		}

		/// <summary>
		/// 子ノードの数を取得する
		/// </summary>
		public int Count
		{
			get
			{
				return this.Nodes.Count;
			}
		}

		/// <summary>
		/// ルートノードを取得する
		/// </summary>
		/// <remarks>親ノードが存在しなければ、自分自身がルートノードとなる</remarks>
		public BaseNode Root
		{
			get
			{
				if (this.Parent == null)
				{
					return this;
				}
				return this.Parent.Root;
			}
		}

		#endregion

		#region コンストラクタ
		
		public BaseNode()
		{

		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BaseNode(NodeInfo define)
		{
			_nodeInfo = define;
		}

		#endregion

		#region method
		
		public abstract string Encode();
		public abstract void Decode(StringIterator src);

		public abstract int Add(BaseNode child);
		public abstract void Delete(int index); 
		public abstract void Clear();
		public abstract BaseNode GetNodeByName(string nodeName);

		#region IEnumerable<BaseNode> メンバ

		/// <summary>
		/// 列挙子を取得
		/// </summary>
		public virtual IEnumerator<BaseNode> GetEnumerator()
		{
			return _nodes.GetEnumerator();
		}

		#endregion

		#region IEnumerable メンバ

		/// <summary>
		/// 列挙子を取得
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		/// <summary>
		/// エンコード後文字列での自ノードの開始位置を取得する
		/// </summary>
		/// <returns>開始位置</returns>
		public int GetStartPos()
		{
			int idx = 1;
			if (this.Parent == null)
			{
				return idx;
			}
			idx = this.Parent.GetStartPos();
			if (this.IsArrayChild())
			{
				idx += this.Parent.NodeInfo.FieldLength;
			}

			for (int forwardIndex = 0; forwardIndex < Index; forwardIndex++)
			{
				idx += this.Parent[forwardIndex].Size;
			}
			return idx;
		}

		/// <summary>
		/// 配列の子供か？
		/// </summary>
		/// <returns></returns>
		public bool IsArrayChild()
		{
			if (this.Parent == null)
			{
				return false;
			}

			if (this.Parent is ArrayNode)
			{
				return true;
			}
			if (this.Parent is DynamicArrayNode)
			{
				return true;
			}
			if (this.Parent is FixedArrayNode)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 追加可能ノードか
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// FixedArrayNodeはだめ
		/// </remarks>
		public bool IsAddAble()
		{
			if (this is FixedArrayNode)
			{
				return false;
			}

			if (this is DynamicArrayNode)
			{
				return true;
			}

			if (this is ArrayNode)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 削除可能ノードか
		/// </summary>
		/// <returns></returns>
		public bool IsDeleteAble()
		{
			if (this.Parent is ArrayNode)
			{
				return true;
			}
			if (this.Parent is DynamicArrayNode)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// 配下のノードをパス名で取得する
		/// </summary>
		/// <param name="nodePath">ノードパス</param>
		/// <returns>配下のノード</returns>
		/// <remarks>
		/// パス名には自ノードの名前は含まない
		/// "子ノード名"\"孫ノード名"\"曾孫ノード名"......
		/// </remarks>
		public BaseNode GetNodeByPath(string targetNodePath)
		{

			if (targetNodePath == "")
			{
				return this;
			}

			string nodePath = targetNodePath;

			string[] paths = nodePath.Split(MsgConst.PATH_DELIMITER);
			
			string nodeName = paths[0];

			BaseNode childNode = this.GetNodeByName(nodeName);
			
			if (childNode == null)
			{
				return null;
			}
			IEnumerable<string> remainPaths = paths.Skip(1);
			StringBuilder sb = new StringBuilder();
			foreach (var item in remainPaths)
			{
				if (sb.Length != 0)
				{
					sb.Append("\\");
				}
				sb.Append(item);
			}
			nodePath =  sb.ToString();
			return childNode.GetNodeByPath(nodePath);
		}

		/// <summary>
		/// 新しい配列にコピーする
		/// </summary>
		/// <returns></returns>
		public BaseNode[] ToArray()
		{
			return _nodes.ToArray();
		}

		/// <summary>
		/// 文字列をデリミタで分割する
		/// </summary>
		/// <param name="data">分割する文字列</param>
		/// <param name="delimiter">デリミタ</param>
		/// <returns>デリミタより前の部分</returns>
		/// <remarks処理後、元のデータからデリミタまでの部分が取り除かれる></remarks>
		private string SplitBy(ref string data, string delimiter)
		{
			int delimPos = data.IndexOf(delimiter);
			string r = "";
			if (delimPos == 0)
			{
				r =  data;
				data = "";
				return r;
			}
			
			r = MBCSHelper.Copy(data, 1, delimPos -1);

			data = MBCSHelper.Delete(data, 1, MBCSHelper.GetSJISLength(r) + 
				MBCSHelper.GetSJISLength(delimiter));
			return r;
		}

		#endregion

	}
}
