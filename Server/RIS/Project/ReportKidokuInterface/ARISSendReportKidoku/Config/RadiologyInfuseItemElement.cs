using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ARISSend.Config
{
	/// <summary>
	/// 画像診断加算の手技/加算情報
	/// </summary>
	public class RadiologyInfuseItemElement : ConfigurationElement
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RadiologyInfuseItemElement()
		{

		}

		// InfuseID 属性
		[ConfigurationProperty("infuseID", IsKey=true, IsRequired=true)]
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
