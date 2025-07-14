using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ReportInterface.Config
{
	public class RequestAdditionItemElementCollection : ConfigurationElementCollection
	{
		// ConfigurationElementCollection 内に設定する ConfigurationElement の名前を設定
		public RequestAdditionItemElementCollection()
		{
			this.AddElementName = "requestAdditionElement";
		}

		// ConfigurationElementCollection 内の ConfigurationElement をインデックスから取得
		public RequestAdditionItemElement Get(int index)
		{
			return (RequestAdditionItemElement)base.BaseGet(index);
		}

		// ConfigurationElementCollection 内の ConfigurationElement をキーから取得
		public RequestAdditionItemElement Get(string name)
		{
			return BaseGet(name) as RequestAdditionItemElement;
		}

		// ConfigurationElementCollection 内に指定したキーの ConfigurationElement があるか
		public bool Contains(string name)
		{
			return BaseGet(name) != null;
		}

		// 新しい ConfigurationElement を作成
		protected override ConfigurationElement CreateNewElement()
		{
			return new RequestAdditionItemElement();
		}

		// 指定した構成要素の要素キーを取得
		protected override object GetElementKey(ConfigurationElement element)
		{
			RequestAdditionItemElement childElement = (RequestAdditionItemElement)element;
			return childElement.InfuseID;
		}
	}
}