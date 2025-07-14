using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MsgEditor.CommonUI
{
	/// <summary>
	/// メニューアイテムがメッセージの型を持つ
	/// </summary>
	public class ToolStripMenuItemWithMsg : ToolStripMenuItem
	{
		#region property
		
		/// <summary>
		/// 保持しているメッセージ型
		/// </summary>
		public Type MsgType
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		#region public
		/// <summary>
		/// 
		/// </summary>
		/// <param name="image"></param>
		public ToolStripMenuItemWithMsg(Image image)
			: base(image)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		public ToolStripMenuItemWithMsg(string text)
			: base(text)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="image"></param>
		public ToolStripMenuItemWithMsg(string text, Image image)
			: base(text, image)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="image"></param>
		/// <param name="dropDownItems"></param>
		public ToolStripMenuItemWithMsg(string text, Image image, params ToolStripItem[] dropDownItems)
			: base(text, image, dropDownItems)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="image"></param>
		/// <param name="onClick"></param>
		/// <param name="shortcutKeys"></param>
		public ToolStripMenuItemWithMsg(string text, Image image, EventHandler onClick, Keys shortcutKeys)
			: base(text, image, onClick, shortcutKeys)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="image"></param>
		/// <param name="onClick"></param>
		/// <param name="msgType"></param>
		public ToolStripMenuItemWithMsg(string text, Image image, EventHandler onClick,
			Type msgType)
			: base(text, image, onClick)
		{
			this.MsgType = msgType;
		}

		#endregion

		#region protected

		/// <summary>
		/// 
		/// </summary>
		protected ToolStripMenuItemWithMsg()
			: base()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="image"></param>
		/// <param name="onClick"></param>
		protected ToolStripMenuItemWithMsg(string text, Image image, EventHandler onClick)
			: base(text, image, onClick)
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="image"></param>
		/// <param name="onClick"></param>
		/// <param name="name"></param>
		protected ToolStripMenuItemWithMsg(string text, Image image, EventHandler onClick, string name) :
			base(text, image, onClick, name)
		{

		}
		#endregion

		#endregion

	}
}
