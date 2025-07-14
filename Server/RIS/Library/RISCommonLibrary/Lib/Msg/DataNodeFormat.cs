using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;

namespace RISCommonLibrary.Lib.Msg
{
	class DataNodeFormat: DataNode
	{
		#region field

		/// <summary>
		/// 表示フォーマット
		/// </summary>
		private string _formatDisplay;

		#endregion

		#region property

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
				int valueInt;
				if (!int.TryParse(value, out valueInt))
				{
					valueInt = 0;
				}
				_data = string.Format(FormatDisplay, valueInt);
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

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="define">ノード定義</param>
		public DataNodeFormat(NodeInfo define)
			: base(define)
		{
			_data = string.Format(FormatDisplay, 0);
		}


	}
}
