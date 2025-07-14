using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ARISSend.Config
{
	/// <summary>
	/// 放射線科情報
	/// </summary>
	public class RadiologyItemElement : ConfigurationElement
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RadiologyItemElement()
		{

		}

		// CommentID 属性
		[ConfigurationProperty("commentID", IsKey=true, IsRequired=true)]
		public string CommentID
		{
			get
			{
				return (string)this["commentID"];
			}
			set
			{
				this["commentID"] = value;
			}
		}
	}
}
