using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Common.Order;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Utils;
using RISBizLibrary.Data;

namespace RISBizLibrary.Updater.Table
{
	public class PATIENTCOMMENTSUpdater : BaseUpdater
	{
		#region const

		#region MergeSQL
		/// <summary>
		/// MergeSQL
		/// </summary>
		private const string MERGE_SQL =
		//2020.06.12 Mod Start Cosmo@H.Taira
		//"MERGE INTO patientcomments p " +
		//"USING " +
		//"( " +
		//	"SELECT " +
		//		":kanja_id kanja_id, " +
		//		":kensatype_id kensatype_id, " +
		//		":patientcomment patientcomment " +
		//	"FROM " +
		//		"DUAL " +
		//") pn " +
		//"ON " +
		//"( " +
		//	"p.kanja_id = pn.kanja_id AND " +
		//	"p.kensatype_id = pn.kensatype_id " +
		//") " +
		//"WHEN MATCHED THEN " +
		//"UPDATE SET " +
		//	"patientcomment = :patientcomment " +
		//"WHEN NOT MATCHED THEN " +
		//"INSERT " +
		//"( " +
		//	"kanja_id, " +
		//	"kensatype_id, " +
		//	"patientcomment " +
		//") " +
		//"VALUES " +
		//"( " +
		//	"pn.kanja_id, " +
		//	"pn.kensatype_id, " +
		//	"pn.patientcomment " +
		//") ";
		"MERGE INTO patientcomments p " +
		"USING " +
		"( " +
			"SELECT " +
				":kanja_id kanja_id, " +
				":kensatype_id kensatype_id, " +
				":patientcomment patientcomment " +
			"FROM " +
				"DUAL " +
		") pn " +
		"ON " +
		"( " +
			"p.kanja_id = pn.kanja_id AND " +
			"p.kensatype_id = pn.kensatype_id " +
		") " +
		"WHEN NOT MATCHED THEN " +
		"INSERT " +
		"( " +
			"kanja_id, " +
			"kensatype_id, " +
			"patientcomment " +
		") " +
		"VALUES " +
		"( " +
			"pn.kanja_id, " +
			"pn.kensatype_id, " +
			"pn.patientcomment " +
		") ";
		//2020.06.12 Mod End   Cosmo@H.Taira

		#endregion

		#region param

		/// <summary>
		/// kanja_id
		/// </summary>
		private const string PARAM_NAME_KANJA_ID = "kanja_id";

		/// <summary>
		/// kensatype_id
		/// </summary>
		private const string PARAM_NAME_KENSATYPE_ID = "kensatype_id";

		/// <summary>
		/// patientcomment
		/// </summary>
		private const string PARAM_NAME_PATIENTCOMMENT = "patientcomment";

		#endregion

		#endregion

		#region field
		#endregion

		#region ITableUpdater メンバ

		public override string TargetSQL
		{
			get
			{
				return MERGE_SQL;
			}
		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="order"></param>
		/// <param name="cn"></param>
		private new void Execute(BaseMsgData data, System.Data.IDbCommand command)
		{
		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="cn"></param>
		public void Execute(BaseMsg msg, System.Data.IDbCommand command)
		{
			command.CommandText = TargetSQL;
			ExecuteAndSetParam(msg, command);
		}

		/// <summary>
		/// SQL実行手続き
		/// </summary>
		/// <param name="data"></param>
		/// <param name="command"></param>
		private new void ExecuteAndSetParam(BaseMsgData data, System.Data.IDbCommand command)
		{
		}
		public void ExecuteAndSetParam(BaseMsg msg, System.Data.IDbCommand command)
		{
			SetParamsCommon(msg, command); //COMMON設定
			ExecuteSQLInner(command);
		}

		private new void SetParams(BaseMsgData data, System.Data.IDbCommand command)
		{
		}

		/// <summary>
		/// COMMON用設定
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="command"></param>
		public void SetParamsCommon(BaseMsg msg, System.Data.IDbCommand command)
		{
			command.Parameters.Clear();
			PatientAttributeAggregate patientAgg = ((IPatientAttribute)(msg.Body)).PatientAttribute;

			SetStringToCommand(PARAM_NAME_KANJA_ID,
				patientAgg.PATIENT_NO.TrimData, command); //患者ID
			SetStringToCommand(PARAM_NAME_KENSATYPE_ID, RQRISDBConst.PATIENTCOMMENTS_KENSATYPE_ID_COMMON, command); //検査種別ID
			SetStringToCommand(PARAM_NAME_PATIENTCOMMENT, "", command); //コメント
		}

		#endregion
	}
}
