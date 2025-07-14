using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ReportInterface.Config
{
	/// <summary>
	/// 画像診断加算ノード定義
	/// </summary>
	public class RequestAdditionItemElement : ConfigurationElement
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RequestAdditionItemElement()
		{

		}
		// InfuseID 属性
		[ConfigurationProperty("infuseID", IsRequired=true)]
		public string InfuseID
		{
			get
			{
				return (string)this["infuseID"];
			}
			set
			{
				this["infuseID"] = value;
			}
		}
	}
}
