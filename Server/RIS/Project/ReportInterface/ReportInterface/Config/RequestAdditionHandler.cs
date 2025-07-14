using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ReportInterface.Config
{
	public class RequestAdditionHandler : ConfigurationSection
	{
		public RequestAdditionHandler()
		{
		}

		// 子要素のコレクション
		[ConfigurationProperty("requestAdditionElements")]
		public RequestAdditionItemElementCollection RequestAdditionSectionCollection
		{
			get
			{
				RequestAdditionItemElementCollection childCollection = 
					(RequestAdditionItemElementCollection)this["requestAdditionElements"];
				return childCollection;
			}
		}
	}
}