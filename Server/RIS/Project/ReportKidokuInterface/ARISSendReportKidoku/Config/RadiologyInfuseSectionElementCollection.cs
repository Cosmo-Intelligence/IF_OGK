using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ARISSend.Config
{
	public class RadiologyInfuseSectionElementCollection : ConfigurationElementCollection
	{
		// ConfigurationElementCollection 内に設定する ConfigurationElement の名前を設定
		public RadiologyInfuseSectionElementCollection()
		{
			this.AddElementName = "radiologyInfuseItem";
		}

		// ConfigurationElementCollection 内の ConfigurationElement をインデックスから取得
		public RadiologyInfuseItemElement Get(int index)
		{
			return (RadiologyInfuseItemElement)base.BaseGet(index);
		}

		// ConfigurationElementCollection 内の ConfigurationElement をキーから取得
		public RadiologyInfuseItemElement Get(string name)
		{
			return BaseGet(name) as RadiologyInfuseItemElement;
		}

		// ConfigurationElementCollection 内に指定したキーの ConfigurationElement があるか
		public bool Contains(string name)
		{
			return BaseGet(name) != null;
		}

		// 新しい ConfigurationElement を作成
		protected override ConfigurationElement CreateNewElement()
		{
			return new RadiologyInfuseItemElement();
		}

		// 指定した構成要素の要素キーを取得
		protected override object GetElementKey(ConfigurationElement element)
		{
			RadiologyInfuseItemElement childElement = element as RadiologyInfuseItemElement;
			return childElement.InfuseID;
		}
	}
}