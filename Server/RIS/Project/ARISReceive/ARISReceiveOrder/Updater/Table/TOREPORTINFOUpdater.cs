using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using ARISReceive.Data;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Exceptions;
using RISBizLibrary.Updater.Table;
using RISBizLibrary.Data;

namespace ARISReceive.Updater.Table
{
	internal class TOREPORTINFOUpdater : BaseUpdater
	{
		#region const

		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO toreportinfo " +
		"( " +
			"requestid, " +
			"requestdate, " +
			"ris_id, " +
			"requesttype, " +
			"messageid1, " +
			"messageid2, " +
			"transferstatus " +
		") " +
		"VALUES " +
		"( " +
			"FROMRISSEQUENCE.NEXTVAL, " +
			"SYSDATE, " +
			":ris_id, " +
			":requesttype, " +
			":messageid1, " +
			":messageid2, " +
			":transferstatus " +
		") ";
		#endregion

		#region param
		/// <summary>
		/// ris_id
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// requesttype
		/// </summary>
		private const string PARAM_NAME_REQUESTTYPE = "requesttype";

		/// <summary>
		/// messageid1
		/// </summary>
		private const string PARAM_NAME_MESSAGEID1 = "messageid1";

		/// <summary>
		/// messageid2
		/// </summary>
		private const string PARAM_NAME_MESSAGEID2 = "messageid2";

		/// <summary>
		/// transferstatus
		/// </summary>
		private const string PARAM_NAME_TRANSFERSTATUS = "transferstatus";

		#endregion

		#endregion

		#region ITableUpdater メンバ

		/// <summary>
		/// InsertSQL
		/// </summary>
		public override string TargetSQL
		{
			get
			{
				return INSERT_SQL;
			}
		}

		/// <summary>
		/// パラメータ設定
		/// </summary>
		/// <param name="order"></param>
		/// <param name="command"></param>
		public override void SetParams(BaseMsgData data, System.Data.IDbCommand command)
		{
			command.Parameters.Clear();
			#region パラメータ

			OrderMsgData orderData = data as OrderMsgData;
			PatientAttributeAggregate patientAgg = orderData.Request.MsgBody.PatientAttribute;

			SetStringToCommand(PARAM_NAME_RIS_ID, orderData.RIS_ID, command); //RIS識別ID
			SetStringToCommand(PARAM_NAME_REQUESTTYPE, GetREQUESTTYPE(orderData), command); //処理タイプ識別子（GGNN）
			SetStringToCommand(PARAM_NAME_MESSAGEID1, orderData.OrderNoForRQRIS, command); //OIXX、ACｘｘ：オーダ番号 PIｘｘ：患者ID
			SetStringToCommand(PARAM_NAME_MESSAGEID2, orderData.KanjaID, command); //OIXX、ACｘｘ：患者ID PIXX：患者ｶﾅ名
			SetStringToCommand(PARAM_NAME_TRANSFERSTATUS, RQRISDBConst.TOREPORTINFO_TRANSFERSTATUS_UNSENT, command); //"送信ｽﾃｰﾀｽ 00：未送信 01：送信済　02：送信対象外"

			#endregion
		}

		/// <summary>
		/// 処理タイプ識別子（GGNN）
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private string GetREQUESTTYPE(OrderMsgData data)
		{
			PatientAttributeAggregate patient = data.Request.MsgBody.PatientAttribute;
			if (patient.PROCESSING_TYPE.TrimData == MsgConst.PROCESSING_TYPE_NEW)
			{
				return RQRISDBConst.FROMHISINFO_MESSAGETYPE_ORDER;
			}
			if (patient.PROCESSING_TYPE.TrimData == MsgConst.PROCESSING_TYPE_DELETE)
			{
				return RQRISDBConst.FROMHISINFO_MESSAGETYPE_ORDER_CANCEL;
			}
			throw new MsgAnomalyException(string.Format(
				"想定していない処理タイプを受信しました。処理タイプ={1}",
					patient.PROCESSING_TYPE.TrimData));
		}

		#endregion
	}
}
