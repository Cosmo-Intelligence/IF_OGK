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
	internal class ORDER_COMMENTUpdater: BaseUpdater
	{
		#region const

		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO order_comment " +
		"( " +
			"ris_id, " +
			"comment_seq, " +
			"comment_kbn, " +
			"comment_id " +
		") " +
		"VALUES " +
		"( " +
			":ris_id, " +
			":comment_seq, " +
			":comment_kbn, " +
			":comment_id " +
		") ";
		#endregion

		#region param

		/// <summary>
		/// RIS識別ID
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// コメント連番
		/// </summary>
		private const string PARAM_NAME_COMMENT_SEQ = "comment_seq";

		/// <summary>
		/// コメント区分
		/// </summary>
		private const string PARAM_NAME_COMMENT_KBN = "comment_kbn";

		/// <summary>
		/// コメントID
		/// </summary>
		private const string PARAM_NAME_COMMENT_ID = "comment_id";

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
			int totalJVIndex = 0;
			for (int detailIndex = 0; detailIndex < helper.Count; detailIndex++)
			{
				OrderDetailArrayHelperItem detail = helper[detailIndex];
				if (detail == null)
				{
					continue;
				}
				if (detail.JVList == null)
				{
					continue;
				}

				for (int jvIndex = 0; jvIndex < detail.JVList.Count; jvIndex++)
				{
					OrderDetailAggregate jvItem = detail.JVList[jvIndex];
					SetParams(orderData, command, totalJVIndex, jvItem);
					ExecuteSQLInner(command);
					totalJVIndex++;
				}
			}
		}

		/// <summary>
		/// パラメータ設定
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		/// <param name="jvIndex"></param>
		/// <param name="jvItem"></param>
		public void SetParams(ARISReceive.Data.OrderMsgData data,
			System.Data.IDbCommand command, int jvIndex, OrderDetailAggregate jvItem)
		{
			command.Parameters.Clear();
			
			SetStringToCommand(PARAM_NAME_RIS_ID, data.RIS_ID, command); //RIS識別ID
			SetIntToCommand(PARAM_NAME_COMMENT_SEQ, (jvIndex + 1).ToString(), command); //コメント連番
			SetStringToCommand(PARAM_NAME_COMMENT_KBN, jvItem.COMMENT_KIND.TrimData, command); //コメント区分
			SetStringToCommand(PARAM_NAME_COMMENT_ID, jvItem.ITEM_CODE.TrimData, command); //コメントID
		}
	}
}
