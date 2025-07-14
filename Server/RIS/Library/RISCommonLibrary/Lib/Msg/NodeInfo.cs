using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// ノード型をあらわす
	/// </summary>
	public enum NodeTypeEnum
	{
		/// <summary>
		/// 電文データを持つ(リーフノード)
		/// </summary>
		ntData, 
		/// <summary>
		/// 電文データは持たない子クラスを持つノード
		/// </summary>
		ntAggregate, 
		/// <summary>
		/// 電文データは持たない配列をあらわすノード
		/// </summary>
		ntArray 
	}

	/// <summary>
	/// 項目定義クラス
	/// </summary>
	public class NodeInfo
	{

		/// <summary>
		/// 名称
		/// </summary>
		public String Name
		{
			get;
			set;
		}

		/// <summary>
		/// 表示用日本語名
		/// </summary>
		public String NameJ
		{
			get;
			set;
		}

		/// <summary>
		/// ノード型
		/// </summary>
		public NodeTypeEnum NodeType
		{
			get;
			set;
		}

		/// <summary>
		/// フィールドバイト数
		/// </summary>
		/// <remarks>Shift_JIS前提</remarks>
		public Int32 FieldLength
		{
			get;
			set;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="nameJ"></param>
		/// <param name="nodeType"></param>
		/// <param name="fieldLength"></param>
		public NodeInfo(string name, string nameJ, NodeTypeEnum nodeType,
			 int fieldLength)
		{
			Name = name;
			NameJ = nameJ;
			NodeType = nodeType;
			FieldLength = fieldLength;
		}


	}
}
