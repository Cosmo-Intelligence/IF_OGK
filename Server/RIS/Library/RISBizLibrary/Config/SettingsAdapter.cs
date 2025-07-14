using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RISBizLibrary.Config
{
	/// <summary>
	/// 各APの設定をdllへ引き渡す
	/// </summary>
	/// <remarks>シングルトン</remarks>
	public class SettingsAdapter
	{
		/// <summary>
		/// インスタンス
		/// </summary>
		private static readonly SettingsAdapter _instance = new SettingsAdapter();

		/// <summary>
		/// Settings
		/// </summary>
		private ApplicationSettingsBase _settings;

		/// <summary>
		/// インスタンスにアクセスするプロパティ
		/// </summary>
		public static SettingsAdapter Instance
		{
			get
			{
				return _instance;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <remarks>プライベート</remarks>
		private SettingsAdapter()
		{

		}

		/// <summary>
		/// 設定を登録する
		/// </summary>
		public void RegistSettings(ApplicationSettingsBase settings)
		{
			_settings = settings;
		}

		/// <summary>
		/// object型の値を返す
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public object GetValue(string name)
		{
			if (_settings == null)
			{
				return null;
			}
			return _settings[name];
		}

	}
}
