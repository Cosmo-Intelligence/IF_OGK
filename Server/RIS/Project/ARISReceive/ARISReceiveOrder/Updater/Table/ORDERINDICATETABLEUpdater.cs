using System.Collections.Generic;
using System.Text;
using ARISReceive.Data;
using RISCommonLibrary.Lib.Utils;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.Order;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;
using RISCommonLibrary.Lib.Msg.Order;
using RISBizLibrary.Updater.Table;
using RISBizLibrary.Data;

namespace ARISReceive.Updater.Table
{
	internal class ORDERINDICATETABLEUpdater: BaseUpdater
	{
		#region const

		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO orderindicatetable " +
		"( " +
			"ris_id, " +
			"ordercomment_id, " +
			"kensa_siji, " +
			"remarks " +
		") " +
		"VALUES " +
		"( " +
			":ris_id, " +
			":ordercomment_id, " +
			":kensa_siji, " +
			":remarks " +
		") ";
		#endregion

		#region param

		/// <summary>
		/// ris_id
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// ordercomment_id
		/// </summary>
		private const string PARAM_NAME_ORDERCOMMENT_ID = "ordercomment_id";

		/// <summary>
		/// kensa_siji
		/// </summary>
		private const string PARAM_NAME_KENSA_SIJI = "kensa_siji";

		/// <summary>
		/// remarks
		/// </summary>
		private const string PARAM_NAME_REMARKS = "remarks";

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

		public override void SetParams(BaseMsgData data, System.Data.IDbCommand command)
		{

			command.Parameters.Clear();
			OrderMsgData orderData = (OrderMsgData)data;
			OrderAggregate order = orderData.Request.MsgBody.Order;
			OrderDetailArrayHelper helper = order.ORDER_DETAIL_SUMM.GetHelper();

			SetStringToCommand(PARAM_NAME_RIS_ID, orderData.RIS_ID, command); //RIS識別ID
			//SetStringToCommand(PARAM_NAME_KENSA_SIJI, order.EXAM_PURPOSE.TrimData, command); //検査目的
			const int MAX_LENGTH_KENSA_SIJI = 4000;
			SetStringToCommand(PARAM_NAME_KENSA_SIJI, MBCSHelper.Copy(order.EXAM_PURPOSE.TrimData, 1, MAX_LENGTH_KENSA_SIJI), command); //検査目的

			StringBuilder sbORDERCOMMENT_ID = new StringBuilder();
			StringBuilder sbREMARKS = new StringBuilder();
			foreach (OrderDetailArrayHelperItem item in helper)
			{
				List<OrderDetailAggregate> jvList = item.JVList;
				foreach (OrderDetailAggregate o in jvList)
				{
					if (o.COMMENT_KIND.TrimData == MsgConst.ORDER_COMMENT_KIND_ORDERCOMMENT)
					{
						sbORDERCOMMENT_ID.AddWithSeparator(o.ACTING_COMMENT.TrimData,
							RQRISDBConst.COMMENT_SEPARATOR);
						continue;
					}

					if (o.COMMENT_KIND.TrimData == MsgConst.ORDER_COMMENT_KIND_ITEMIZED)
					{
						sbREMARKS.AddWithSeparator(o.ACTING_COMMENT.TrimData,
							RQRISDBConst.COMMENT_SEPARATOR);
						continue;
					}

					if (o.COMMENT_KIND.TrimData == MsgConst.ORDER_COMMENT_KIND_KENSA_TYPE)
					{
						sbREMARKS.AddWithSeparator(o.ACTING_COMMENT.TrimData,
							RQRISDBConst.COMMENT_SEPARATOR);
					}
				};
			}
			
            // 2023.03.31 大垣通信対応のため項目の最大長を1024から4000に変更
			const int MAX_LENGTH_ORDERCOMMENT_ID = 4000;
			const int MAX_LENGTH_REMARKS = 4000;
			SetStringToCommand(PARAM_NAME_ORDERCOMMENT_ID, MBCSHelper.Copy(
				sbORDERCOMMENT_ID.ToString(), 1, MAX_LENGTH_ORDERCOMMENT_ID), command); //ｵｰﾀﾞｺﾒﾝﾄID, ｼｽﾃﾑ区切文字で複数設定可
			SetStringToCommand(PARAM_NAME_REMARKS, MBCSHelper.Copy(
				sbREMARKS.ToString(), 1, MAX_LENGTH_REMARKS), command); //臨床診断
		}
	}
}
