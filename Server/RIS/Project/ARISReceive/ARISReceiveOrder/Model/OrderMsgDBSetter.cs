using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using ARISReceive.Data;
using ARISReceive.Updater;
using RISBizLibrary.Data;
using RISBizLibrary.Receive.Model;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.Order;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISReceive.Model
{
	/// <summary>
	/// メッセージからDBへ更新する
	/// </summary>
	public class OrderMsgDBSetter : BaseMsgDBSetter
	{
		#region field

		#endregion

		#region property

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderMsgDBSetter():base()
		{

		}

		#endregion

		#region method

		/// <summary>
		/// メッセージデータ保持クラス生成
		/// </summary>
		/// <returns></returns>
		protected override BaseMsgData CreateMsgData()
		{
			return new OrderMsgData();
		}

		/// <summary>
		/// 内部処理
		/// </summary>
		/// <param name="msgData"></param>
		/// <param name="cn"></param>
		/// <returns></returns>
		protected override BaseResponseMsg SetDataToDatabaseInner(BaseMsgData msgData, IDbConnection cn)
		{
			OrderMsgData orderMsgData =  (OrderMsgData)msgData;
			ValidateStatus(orderMsgData, cn);
			orderMsgData.RIS_ID = GetRIS_ID(orderMsgData, cn);

			string processingType = orderMsgData.Request.MsgBody.PatientAttribute.PROCESSING_TYPE.TrimData;
			if (processingType == MsgConst.PROCESSING_TYPE_NEW)
			{
				return SetDataToDatabaseNew(orderMsgData, cn);
			}

			return SetDataToDatabaseDelete(orderMsgData, cn);
		}

		/// <summary>
		/// 新規
		/// </summary>
		/// <param name="msgData"></param>
		/// <param name="cn"></param>
		/// <returns></returns>
		private BaseResponseMsg SetDataToDatabaseNew(OrderMsgData msgData, IDbConnection cn)
		{
			msgData.KensaTypeID = GetKENSATYPE_ID(msgData, cn);
			ValidateMaster(msgData, cn);
			OrderUpdater updater = new OrderUpdater();
			updater.DeleteAndInsert(msgData, cn);
			return msgData.Response;
		}

		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="msgData"></param>
		/// <param name="cn"></param>
		/// <returns></returns>
		private BaseResponseMsg SetDataToDatabaseDelete(OrderMsgData msgData, IDbConnection cn)
		{
			OrderUpdater updater = new OrderUpdater();
			updater.Delete(msgData, cn);
			return msgData.Response;
		}

		/// <summary>
		/// RIS_ID取得
		/// </summary>
		/// <param name="omsg"></param>
		/// <param name="cn"></param>
		/// <returns></returns>
		/// <remarks>RIS_IDがすでに存在する場合はそれを返し、無かった場合は新規に発番する</remarks>
		private string GetRIS_ID(OrderMsgData msgData, IDbConnection cn)
		{
			const string SQL_SELECT =
			"SELECT " +
				"CASE " +
					"WHEN MAX(o.ris_id) IS NULL THEN RIS_SERVER_PACKAGE.GET_NEW_RIS_ID " +
					"ELSE MAX(o.ris_id) " +
				"END {0} " +
			"FROM " +
				"ordermaintable o " +
			"WHERE " +
				"o.orderno = :{1} ";

			const string FIELD_NAME = "ris_id";
			const string PARAM_NAME = "orderno";

			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = string.Format(SQL_SELECT, FIELD_NAME, PARAM_NAME);

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_NAME, msgData.OrderNoForRQRIS);
				command.Parameters.Add(param);

				_log.Debug("RIS_ID取得します");
				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new MsgAnomalyException(string.Format(
							"RIS_ID取得処理でSQLのエラーが発生しました。OrderNo={0}", msgData.OrderNoForRQRIS));
					}
					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);
					string r = reader.GetStringByDBString(FIELD_NAME);
					_log.DebugFormat("RIS_ID={0}", r);
					return r;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		/// <summary>
		/// 検査種別ID取得
		/// </summary>
		/// <param name="omsg"></param>
		/// <param name="cn"></param>
		/// <returns></returns>
		/// <remarks>削除のときは""</remarks>
		private string GetKENSATYPE_ID(OrderMsgData msgData, IDbConnection cn)
		{
			const string SQL_SELECT =
			"SELECT " +
				"b.kensatype_id {0} " +
			"FROM " +
				"buimaster b " +
			"WHERE " +
				"b.bui_id = :{1} ";

			const string field_name = "kensatype_id";
			const string param_name = "bui_id";

			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = string.Format(SQL_SELECT, field_name, param_name);

				IDataParameter param = command.CreateParameter();
				string buiid = msgData.BuiIDFirst;
				param.SetInputString(param_name, buiid);
				command.Parameters.Add(param);

				_log.Debug("KENSATYPE_ID取得します");
				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new RISMasterNotExistsException(string.Format(
							"buimasterに無い部位IDが指定されました。{0}", buiid));
					}
					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);
					string kensatypeID = reader.GetStringByDBString(field_name);
					_log.DebugFormat("KENSATYPE_ID={0}", kensatypeID);
					return kensatypeID;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		/// <summary>
		/// ステータスチェック
		/// </summary>
		/// <param name="omsg"></param>
		private void ValidateStatus(OrderMsgData msgData, IDbConnection cn)
		{
			//受付済み以上のオーダがいるか？
			const string SQL_SELECT =
			"SELECT " +
				"COUNT(*) cnt " +
			"FROM " +
				"exmaintable e " +
				"INNER JOIN ordermaintable o " +
				"ON " +
					"o.orderno = :{0} AND " +
					"o.ris_id = e.ris_id " +
			"WHERE " +
				"e.status > 9 ";

			const string PARAM_NAME = "orderno";

			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = string.Format(SQL_SELECT, PARAM_NAME);

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_NAME, msgData.OrderNoForRQRIS);
				command.Parameters.Add(param);

				_log.Debug("受付済み以上のオーダがいるかチェックします");
				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				int count = Convert.ToInt32(command.ExecuteScalar());
				_log.DebugFormat("{0}件", count);
				if (count > 0)
				{
					throw new MsgDoneReceiptException("すでに受付済み以降のデータがあります");
				}
			}
		}

		/// <summary>
		/// マスタチェック
		/// </summary>
		/// <param name="omsg"></param>
		private void ValidateMaster(OrderMsgData msgData, IDbConnection cn)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());

				#region 患者情報部
				ValidatePatient(msgData.Request.MsgBody.PatientAttribute, command);
				#endregion

				#region オーダ情報部
				ValidateOrder(msgData.Request.MsgBody.PatientAttribute,
					msgData.Request.MsgBody.Order, command);
				#endregion
			}
		}

		/// <summary>
		/// オーダ部チェック
		/// </summary>
		/// <param name="p"></param>
		/// <param name="o"></param>
		/// <param name="command"></param>
		private void ValidateOrder(PatientAttributeAggregate p, OrderAggregate o, IDbCommand command)
		{
			if (p == null)
			{
				return;
			}

			if (o == null)
			{
				return;
			}

			#region オーダ情報部

			_log.Debug("オーダチェックを行います");
			//依頼科コード
			_log.Debug("依頼科コードチェックを行います");
			ValidateMasterExecute(o.REQUEST_SECTION_CODE.TrimData,
				"sectionmaster", "section_id", command);
			
			_log.Debug("検査室チェックを行います");
			//検査室
			ValidateMasterExecute(o.EXAM_ROOM.TrimData,
				"examroommaster", "examroom_id", command);

			_log.Debug("JA(データ内容は部位)チェックを行います");
			//名細部のJAをリストにする
			IEnumerable<OrderDetailAggregate> jaList = from n in o.ORDER_DETAIL_SUMM
												  where ((OrderDetailAggregate)n).ITEM_KIND.TrimData == MsgConst.ORDER_ITEM_KIND_ACT
												  select ((OrderDetailAggregate)n);

			foreach (OrderDetailAggregate item in jaList)
			{
				ValidateMasterExecute(item.ITEM_CODE.TrimData,
					"buimaster", "bui_id", command);
			}
			#endregion
		}

		#endregion
	}
}
