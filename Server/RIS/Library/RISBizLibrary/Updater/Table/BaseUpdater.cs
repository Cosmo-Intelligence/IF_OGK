using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RISCommonLibrary.Lib.Utils;
using RISBizLibrary.Data;

namespace RISBizLibrary.Updater.Table
{
	/// <summary>
	/// 更新クラス基底
	/// </summary>
	public abstract class BaseUpdater : ITableUpdater
	{
		#region field
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#endregion

		#region ITableUpdater メンバ

		public abstract string TargetSQL
		{
			get;
		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="order"></param>
		/// <param name="cn"></param>
		public virtual void Execute(BaseMsgData data, System.Data.IDbCommand command)
		{
			command.CommandText = TargetSQL;
			ExecuteAndSetParam(data, command);
		}

		/// <summary>
		/// SQL実行手続き
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		public virtual void ExecuteAndSetParam(BaseMsgData data, System.Data.IDbCommand command)
		{
			SetParams(data, command);
			ExecuteSQLInner(command);
		}

		#endregion

		/// <summary>
		/// パラメータ設定
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		public virtual void SetParams(BaseMsgData data, System.Data.IDbCommand command)
		{

		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="command"></param>
		protected void ExecuteSQLInner(System.Data.IDbCommand command)
		{
			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("Insert実行します");
			int count = Convert.ToInt32(command.ExecuteNonQuery());
			_log.DebugFormat("対象{0}件", count);
		}

		/// <summary>
		/// 文字列値をコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		protected void SetStringToCommand(string paramName, string value, System.Data.IDbCommand command)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputString(paramName, value);
			command.Parameters.Add(param);
		}

		/// <summary>
		/// 数値をコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		protected void SetIntToCommand(string paramName, string value, System.Data.IDbCommand command)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputInt32FromString(paramName, value);
			command.Parameters.Add(param);
		}

		/// <summary>
		/// Decimalをコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		protected void SetDecimalToCommand(string paramName, string value, System.Data.IDbCommand command)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputDecimalFromString(paramName, value);
			command.Parameters.Add(param);
		}

		/// <summary>
		/// 日時をコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		protected void SetDateTimeToCommand(string paramName, string value, System.Data.IDbCommand command)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputDateTimeFromString(paramName, value);
			command.Parameters.Add(param);
		}

	}
}
