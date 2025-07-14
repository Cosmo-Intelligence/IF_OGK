using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RISBizLibrary.Data;

namespace RISBizLibrary.Updater.Table
{
	/// <summary>
	/// テーブル更新インターフェース
	/// </summary>
	public interface ITableUpdater
	{
		/// <summary>
		/// 実行するSQL
		/// </summary>
		string TargetSQL
		{
			get;
		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="order"></param>
		/// <param name="command"></param>
		void Execute(BaseMsgData data, IDbCommand command);

		/// <summary>
		/// SQL実行手続き
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		void ExecuteAndSetParam(BaseMsgData data, IDbCommand command);

		/// <summary>
		/// パラメータ設定
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		void SetParams(BaseMsgData data, System.Data.IDbCommand command);

	}
}
