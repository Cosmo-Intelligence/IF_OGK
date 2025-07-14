using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using System.Data;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Exceptions;
using System.Configuration;
using RISCommonLibrary.Lib.Utils;
using RISBizLibrary.Data;
using RISBizLibrary.Config;
using Oracle.DataAccess.Client;
using RISODPLibrary.Lib.Utils;

namespace RISBizLibrary.Receive.Model
{
	public abstract class BaseMsgDBSetter
	{
		#region field

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// オーダのデータを保持
		/// </summary>
		protected BaseMsgData _msgData;
		#endregion

		#region property

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BaseMsgDBSetter()
		{
			_msgData = CreateMsgData();
		}

		#endregion

		#region method

		/// <summary>
		/// 受信したメッセージでデータベースを更新する
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		public BaseResponseMsg SetDataToDatabase(BaseMsg msg)
		{
			if (msg == null)
			{
				throw new MsgAnomalyException("メッセージクラスがNullです");
			}

			_msgData.Clear();
			_msgData.Request = msg; //受信電文
			_msgData.Response = new ResponseMsg();　//返信電文

			_log.Debug("接続文字列取得します");

			string connectionString = (string)SettingsAdapter.Instance.GetValue("ConnectionString");
			_log.DebugFormat("接続文字列取得しました。{0}",
                ODPConnectionUtils.GetConnectionStringWithoutPassword(connectionString));

			_log.Debug("DB接続します");
            IDbConnection cn = ODPConnectionUtils.Connect(connectionString);
			_log.Debug("DB接続しました");
			try
			{
				return SetDataToDatabaseInner(_msgData, cn);
			}
			finally
			{
				cn.Close();
			}
		}

		/// <summary>
		/// メッセージデータ保持クラス生成
		/// </summary>
		/// <returns></returns>
		protected abstract BaseMsgData CreateMsgData();

		/// <summary>
		/// 内部処理
		/// </summary>
		/// <param name="msgData"></param>
		/// <param name="cn"></param>
		/// <returns></returns>
		protected abstract BaseResponseMsg SetDataToDatabaseInner(BaseMsgData msgData, IDbConnection cn);

		/// <summary>
		/// 患者情報部チェック
		/// </summary>
		/// <param name="p"></param>
		/// <param name="command"></param>
		protected void ValidatePatient(PatientAttributeAggregate p, IDbCommand command)
		{
			if (p == null)
			{
				return;
			}
			#region 患者情報部

			#region 入院時のみ
			if (p.PATIENT_INOUT.TrimData == MsgConst.PATIENT_INOUT_ADMISSION)
			{
				_log.Debug("病棟コードチェックを行います");
				//病棟コード
				ValidateMasterExecute(p.WARD_CODE.TrimData,
					"byoutoumaster", "byoutou_id", command);
				_log.Debug("病室コードチェックを行います");
				//病室コード
				ValidateMasterExecute(p.ROOM_CODE.TrimData,
					"byousitumaster", "byousitu_id", command);
				_log.Debug("入院科コードチェックを行います");
				//入院科コード
				ValidateMasterExecute(p.ADMISSION_SECTION_CODE.TrimData,
					"sectionmaster", "section_id", command);
			}
			#endregion

			#endregion
		}

		/// <summary>
		/// マスタチェック
		/// </summary>
		/// <param name="omsg"></param>
		protected void ValidateMasterExecute(string value, string tableName, string colName, IDbCommand command)
		{
			const string SQL_SELECT =
			"SELECT " +
				"COUNT(*) cnt " +
			"FROM " +
				"{0} t " +
			"WHERE " +
				"t.{1} = :{1} ";

			command.CommandText = string.Format(SQL_SELECT, tableName, colName);

			command.Parameters.Clear();
			IDataParameter param = command.CreateParameter();
			param.SetInputString(colName, value);
			command.Parameters.Add(param);

			_log.DebugFormat("{0}マスタチェックします", tableName);
			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			int count = Convert.ToInt32(command.ExecuteScalar());
			_log.DebugFormat("{0}件", count);
			if (count < 1)
			{
				throw new RISMasterNotExistsException(
					string.Format("{0}マスタにコードがありませんでした。code={1}",
					tableName, value));
			}
		}
		#endregion

	}
}
