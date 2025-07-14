using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ARISSend.Config
{
	public class RadiologySectionElementCollection : ConfigurationElementCollection
	{
		// ConfigurationElementCollection 内に設定する ConfigurationElement の名前を設定
		public RadiologySectionElementCollection()
		{
			this.AddElementName = "radiologyItem";
		}

		// ConfigurationElementCollection 内の ConfigurationElement をインデックスから取得
		public RadiologyItemElement Get(int index)
		{
			return (RadiologyItemElement)base.BaseGet(index);
		}

		// ConfigurationElementCollection 内の ConfigurationElement をキーから取得
		public RadiologyItemElement Get(string name)
		{
			return BaseGet(name) as RadiologyItemElement;
		}

		// ConfigurationElementCollection 内に指定したキーの ConfigurationElement があるか
		public bool Contains(string name)
		{
			return BaseGet(name) != null;
		}

		// 新しい ConfigurationElement を作成
		protected override ConfigurationElement CreateNewElement()
		{
			return new RadiologyItemElement();
		}

		// 指定した構成要素の要素キーを取得
		protected override object GetElementKey(ConfigurationElement element)
		{
			RadiologyItemElement childElement = element as RadiologyItemElement;
			return childElement.CommentID;
		}
	}
}