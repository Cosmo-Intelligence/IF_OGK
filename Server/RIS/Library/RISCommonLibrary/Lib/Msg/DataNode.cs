using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Utils;
using RISCommonLibrary.Lib.Exceptions;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// データを持つノード
	/// </summary>
	public class DataNode: BaseNode
	{
		/// <summary>
		/// データ
		/// </summary>
		protected string _data = "";

		/// <summary>
		/// データを取得・設定する
		/// </summary>
		public override string Data
		{
			get
			{
				return _data;
			}
			set
			{
				string s = value;
				if (string.IsNullOrEmpty(s))
				{
					s = "";
				}
				_data = s.ShiftJisPadRight(this.NodeInfo.FieldLength, MsgConst.MSG_PADDING_CHAR);
			}
		}

		public override string TrimData
		{
			get
			{
				if (String.IsNullOrEmpty(this.Data))
				{
					return "";
				}
				return this.Data.Trim(new[] { ' ', '　' });
			}
		}

		public override int Size
		{
			get
			{
				return this.NodeInfo.FieldLength;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="define">ノード定義</param>
		public DataNode(NodeInfo define)
			: base(define)
		{
			_data = _data.PadLeft(this.Size, MsgConst.MSG_PADDING_CHAR);
		}

		/// <summary>
		/// ノードのデータを文字列にする
		/// </summary>
		/// <returns>エンコードされた文字列</returns>
		public override string Encode()
		{
			int dataLength = MBCSHelper.GetSJISLength(this.Data);
			if (dataLength > this.NodeInfo.FieldLength)
			{
				throw new NodeOparationException(
					string.Format("エンコード前のデータ長がフィールドサイズを超えています={0}", this.Data), this);
			}

			string r = this.Data.ShiftJisPadRight(this.NodeInfo.FieldLength, MsgConst.MSG_PADDING_CHAR);

			if (MBCSHelper.GetSJISLength(r) < this.NodeInfo.FieldLength)
			{
				throw new NodeOparationException(string.Format("エンコード後のデータ長がフィールドサイズを下回っています={0}", r), this);
			}
			return r;
		}

		/// <summary>
		/// 文字列からノードを復元する
		/// </summary>
		/// <param name="src">復元元文字列</param>
		/// <returns></returns>
		public override void Decode(StringIterator src)
		{
			if (src.EOF)
			{
				throw new NodeOparationException("デコードできるデータが存在しません", this);
			}
			int gotLength = src.GetNext(this.NodeInfo.FieldLength);
			this.Data = src.Current;

			if (gotLength < this.NodeInfo.FieldLength)
			{
				throw new NodeOparationException("データが足りません", this);
			}
		}

		/// <summary>
		/// 自ノードに子ノードを追加する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <returns>追加した子ノードのインデックス</returns>
		public override int Add(BaseNode child)
		{
			//データノードには子ノードを追加できない
			throw new NodeOparationException("データノードには子ノードを追加できません", this);
		}

		public override void Delete(int index)
		{
			//データノードには子ノードを追加できない
			throw new NodeOparationException("データノードからは子ノードを削除できません", this);
		}

		public override void Clear()
		{
			//データノードには子ノードを追加できない
			throw new NodeOparationException("データノードからは子ノードをクリアできません", this);
		}

		/// <summary>
		/// スナップショット用文字列を取得する
		/// </summary>
		public override string Snapshot
		{
			get
			{
				return string.Format("{0}-{1}: DATA=\"{2}\"", this.NameJ, this.Name, this.Data);
			}
		}

		/// <summary>
		/// 名前を指定して子ノードを取得する
		/// </summary>
		/// <param name="nodeName">ノード名</param>
		/// <returns>子ノード</returns>
		/// <remarks>常にnullを返す</remarks>
		public override BaseNode GetNodeByName(string nodeName)
		{
			return null;
		}

	}
}
