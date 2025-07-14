using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// メッセージファクトリクラス
	/// </summary>
	/// <remarks>シングルトン</remarks>
	public sealed class MsgFactory
	{
		/// <summary>
		/// インスタンス
		/// </summary>
		private static readonly MsgFactory _instance = new MsgFactory();

		/// <summary>
		/// 登録したメッセージのリスト
		/// </summary>
		private List<Type> _msgTypeList = new List<Type>();

		/// <summary>
		/// インスタンスにアクセスするプロパティ
		/// </summary>
		public static MsgFactory Instance
		{
			get
			{
				return _instance;
			}
		}

		public List<Type> MsgTypeList
		{
			get
			{
				return _msgTypeList;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <remarks>プライベート</remarks>
		private MsgFactory()
		{

		}

		/// <summary>
		/// メッセージクラスを登録する
		/// </summary>
		public void RegistMsg(Type msgType)
		{
			_msgTypeList.Add(msgType);
		}

		/// <summary>
		/// メッセージ文字列から該当の電文インスタンスを返す
		/// </summary>
		/// <param name="textMessage"></param>
		/// <returns></returns>
		public BaseMsg CreateMsgByTextMessage(string textMessage)
		{
			foreach (Type item in _msgTypeList)
			{
				BaseMsg msg = Activator.CreateInstance(item) as BaseMsg;
				if (msg == null)
				{
					continue;
				}
				if (!msg.CanDecode(textMessage))
				{
					continue;
				}
				return msg;
			}
			return null;
		}

		/// <summary>
		/// メッセージ日本語名から該当の電文インスタンスを返す
		/// </summary>
		/// <param name="textMessage"></param>
		/// <returns></returns>
		public BaseMsg CreateMsgByMessageNameJ(string messageNameJ)
		{
			foreach (Type item in _msgTypeList)
			{
				BaseMsg msg = Activator.CreateInstance(item) as BaseMsg;
				if (msg == null)
				{
					continue;
				}
				if (string.Compare(msg.MessageNameJ, messageNameJ, true) != 0)
				{
					continue;
				}
				return msg;
			}
			return null;
		}

	}
}
