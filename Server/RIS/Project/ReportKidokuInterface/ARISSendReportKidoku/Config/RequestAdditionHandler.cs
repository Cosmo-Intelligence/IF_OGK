using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ARISSend.Config
{
	public class RequestAdditionHandler : ConfigurationSection
	{
		public RequestAdditionHandler()
		{

		}

		// 子要素のコレクション
		[ConfigurationProperty("radiologySection")]
		public RadiologySectionElementCollection RadiologySectioCollection
		{
			get
			{
				RadiologySectionElementCollection childCollection = 
		            (RadiologySectionElementCollection)this["radiologySection"];
				return childCollection;
			}
		}

		// 子要素のコレクション
		[ConfigurationProperty("radiologyInfuseSection")]
		public RadiologyInfuseSectionElementCollection RadiologyInfuseSectionCollection
		{
			get
			{
				RadiologyInfuseSectionElementCollection childCollection = 
		            (RadiologyInfuseSectionElementCollection)this["radiologyInfuseSection"];
				return childCollection;
			}
		}
	}
}