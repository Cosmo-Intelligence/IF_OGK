using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARISReceive.Data;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISBizLibrary.Updater.Table;
using RISBizLibrary.Data;

namespace ARISReceive.Updater.Table
{
	internal class EXMAINTABLEUpdater : BaseUpdater
	{
		#region const

		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO exmaintable " +
		"( " +
			"ris_id, " +
			"kanja_id, " +
			"kensatype_id, " +
			"status " +
		") " +
		"VALUES " +
		"( " +
			":ris_id, " +
			":kanja_id, " +
			":kensatype_id, " +
			":status " +
		") ";
		#endregion

		#region param
		/// <summary>
		/// ris_id
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// kanja_id
		/// </summary>
		private const string PARAM_NAME_KANJA_ID = "kanja_id";

		/// <summary>
		/// kensatype_id
		/// </summary>
		private const string PARAM_NAME_KENSATYPE_ID = "kensatype_id";

		/// <summary>
		/// status
		/// </summary>
		private const string PARAM_NAME_STATUS = "status";
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

			SetStringToCommand(PARAM_NAME_RIS_ID, orderData.RIS_ID, command); //RIS識別ID YYYYMMDDNNNNNNNN YYYYMMDD：KENSA_DATE ｎNNNNNNN：ｼｰｹﾝｽﾃｰﾌﾞﾙの連番（常に8桁）
			SetStringToCommand(PARAM_NAME_KENSATYPE_ID, orderData.KensaTypeID, command); //検査種別ID
			SetStringToCommand(PARAM_NAME_KANJA_ID, patientAgg.PATIENT_NO.TrimData, command); //患者ID
			SetIntToCommand(PARAM_NAME_STATUS, RQRISDBConst.EXMAINTABLE_STATUS_NOT_RECEIPT, command); //ステータス
			#endregion
		}
		#endregion
	}
}
