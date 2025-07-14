using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.Order;
using ARISReceive.Data;
using RISCommonLibrary.Lib.Utils;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;
using RISBizLibrary.Updater.Table;
using RISBizLibrary.Data;

namespace ARISReceive.Updater.Table
{
	internal class ORDERBUITABLEUpdater: BaseUpdater
	{
		#region const

		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO ORDERBUITABLE " +
		"( " +
			"ris_id, " +
			"no, " +
			"bui_id, " +
			"houkou_id, " +
			"sayuu_id, " +
			"kensahouhou_id, " +
			"kensasitu_id, " +
			"kensakiki_id " +
		") " +
		"SELECT " +
			":ris_id, " +
			":no, " +
			":bui_id, " +
			":houkou_id, " +
			":sayuu_id, " +
			":kensahouhou_id, " +
			":kensasitu_id, " +
			"RIS_SERVER_PACKAGE.STRTOKEN(e.kensakiki_id, ',', 1) " +
		"FROM " +
			"DUAL " +
			"LEFT OUTER JOIN examroommaster e " +
			"ON " +
				"e.examroom_id = :kensasitu_id ";
		#endregion

		#region param

		/// <summary>
		/// ris_id
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// no
		/// </summary>
		private const string PARAM_NAME_NO = "no";

		/// <summary>
		/// bui_id
		/// </summary>
		private const string PARAM_NAME_BUI_ID = "bui_id";

		/// <summary>
		/// houkou_id
		/// </summary>
		private const string PARAM_NAME_HOUKOU_ID = "houkou_id";

		/// <summary>
		/// sayuu_id
		/// </summary>
		private const string PARAM_NAME_SAYUU_ID = "sayuu_id";

		/// <summary>
		/// kensahouhou_id
		/// </summary>
		private const string PARAM_NAME_KENSAHOUHOU_ID = "kensahouhou_id";

		/// <summary>
		/// kensasitu_id
		/// </summary>
		private const string PARAM_NAME_KENSASITU_ID = "kensasitu_id";

		#endregion

		#endregion

		#region field
		#endregion

		public override string TargetSQL
		{
			get
			{
				return INSERT_SQL;
			}
		}

		/// <summary>
		/// SQL実行手続き
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		public override void ExecuteAndSetParam(BaseMsgData data, System.Data.IDbCommand command)
		{
			OrderMsgData orderData = data as OrderMsgData;
			OrderAggregate order = orderData.Request.MsgBody.Order;
			OrderDetailArrayHelper helper = order.ORDER_DETAIL_SUMM.GetHelper();
			for (int detailIndex = 0; detailIndex < helper.Count; detailIndex++)
			{
				OrderDetailArrayHelperItem detail = helper[detailIndex];
				SetParams(orderData, command, detailIndex, detail);
				ExecuteSQLInner(command);
			}
		}

		/// <summary>
		/// パラメータ設定
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		/// <param name="detailIndex"></param>
		/// <param name="detail"></param>
		public void SetParams(ARISReceive.Data.OrderMsgData data,
			System.Data.IDbCommand command, int detailIndex, OrderDetailArrayHelperItem detail)
		{
			command.Parameters.Clear();

			OrderAggregate order = data.Request.MsgBody.Order;
			SetStringToCommand(PARAM_NAME_RIS_ID, data.RIS_ID, command); //RIS識別ID
			SetIntToCommand(PARAM_NAME_NO, (detailIndex + 1).ToString(), command); //部位情報連番
			SetStringToCommand(PARAM_NAME_BUI_ID, detail.JA.ITEM_CODE.TrimData, command); //部位ID
			SetStringToCommand(PARAM_NAME_HOUKOU_ID, GetHOUKOU_ID(data, detail.JH), command); //方向ID
			SetStringToCommand(PARAM_NAME_SAYUU_ID, GetSAYUU_ID(data), command); //左右ID
			SetStringToCommand(PARAM_NAME_KENSAHOUHOU_ID, GetKENSAHOUHOU_ID(data, detail.JT), command); //検査方法ID
			SetStringToCommand(PARAM_NAME_KENSASITU_ID, order.EXAM_ROOM.TrimData, command); //予定検査室ID
		}

		/// <summary>
		/// 左右ID
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private string GetSAYUU_ID(ARISReceive.Data.OrderMsgData data)
		{
			return RQRISDBConst.ORDERBUITABLE_SAYUU_ID_NONE;
		}

		/// <summary>
		/// 方向ID
		/// </summary>
		/// <param name="data"></param>
		/// <param name="orderDetailAgg"></param>
		/// <returns></returns>
		private string GetHOUKOU_ID(ARISReceive.Data.OrderMsgData data, OrderDetailAggregate orderDetailAgg)
		{
			if (orderDetailAgg == null)
			{
				return data.KensaTypeID + RQRISDBConst.KENSATYPE_ID_COMMON_SUFFIX;
			}
			return orderDetailAgg.ITEM_CODE.TrimData;
		}

		/// <summary>
		/// 検査方法ID
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private string GetKENSAHOUHOU_ID(ARISReceive.Data.OrderMsgData data, OrderDetailAggregate orderDetailAgg)
		{
			if (orderDetailAgg == null)
			{
				return data.KensaTypeID + RQRISDBConst.KENSATYPE_ID_COMMON_SUFFIX;
			}
			return orderDetailAgg.ITEM_CODE.TrimData;
		}
	}
}
