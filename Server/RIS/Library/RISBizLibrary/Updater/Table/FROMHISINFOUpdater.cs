using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISBizLibrary.Data;
using RISBizLibrary.Updater.Table.FROMHIINFO;

namespace RISBizLibrary.Updater.Table
{
	public abstract class FROMHISINFOUpdater : BaseUpdater
	{

		#region const

		#region InsertSQL

		/// <summary>
		/// InsertSQL
		/// </summary>
		private const string INSERT_SQL =
		"INSERT INTO fromhisinfo " +
		"( " +
			"recieveid, " +
			"recievedate, " +
			"messagetype, " +
			"ris_id, " +
			"messageid1, " +
			"messageid2, " +
			"recievetext " +
		") " +
		"VALUES " +
		"( " +
			"FROMHISSEQUENCE.NEXTVAL, " +
			"SYSDATE, " +
			":messagetype, " +
			":ris_id, " +
			":messageid1, " +
			":messageid2, " +
			":recievetext " +
		") ";
		#endregion

		#region param
		/// <summary>
		/// recieveid
		/// </summary>
		private const string PARAM_NAME_RECIEVEID = "recieveid";

		/// <summary>
		/// messagetype
		/// </summary>
		private const string PARAM_NAME_MESSAGETYPE = "messagetype";

		/// <summary>
		/// ris_id
		/// </summary>
		private const string PARAM_NAME_RIS_ID = "ris_id";

		/// <summary>
		/// messageid1
		/// </summary>
		private const string PARAM_NAME_MESSAGEID1 = "messageid1";

		/// <summary>
		/// messageid2
		/// </summary>
		private const string PARAM_NAME_MESSAGEID2 = "messageid2";

		/// <summary>
		/// recievetext
		/// </summary>
		private const string PARAM_NAME_RECIEVETEXT = "recievetext";

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
			PatientAttributeAggregate patientAgg = ((IPatientAttribute)(data.Request.Body)).PatientAttribute;

			FROMHISINFOState state = GetState(data);

			SetStringToCommand(PARAM_NAME_MESSAGETYPE, state.GetMESSAGETYPE(), command); //受信データタイプ識別子（GGNN）
			SetStringToCommand(PARAM_NAME_RIS_ID, state.GetRIS_ID(), command); //RIS識別ID
			SetStringToCommand(PARAM_NAME_MESSAGEID1, state.GetMESSAGEID1(), command); //OIXX、ACｘｘ：オーダ番号 PIｘｘ：患者ID
			SetStringToCommand(PARAM_NAME_MESSAGEID2, state.GetMESSAGEID2(), command); //OIXX、ACｘｘ：患者ID PIXX：患者ｶﾅ名
			//全角を含む4000バイトごえだとだめORA-01461
			//NULLでOKになったのでNULLで更新する
			SetStringToCommand(PARAM_NAME_RECIEVETEXT, data.Request.TextMessage, command); //受信電文
			#endregion
		}

		#endregion

		/// <summary>
		/// 状態クラスの取得
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		protected abstract FROMHISINFOState GetState(BaseMsgData data);

	}
}
