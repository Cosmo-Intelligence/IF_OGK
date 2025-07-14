using System.Configuration;
using System.Data;
using ARISReceive.Data;
using ARISReceive.Updater;
using RISBizLibrary.Data;
using RISBizLibrary.Receive.Model;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISReceive.Model
{
	/// <summary>
	/// メッセージからDBへ更新する
	/// </summary>
	internal class HospitalMsgDBSetter : BaseMsgDBSetter
	{
		#region field

		#endregion

		#region property

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HospitalMsgDBSetter()
			: base()
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
			return new HospitalMsgData();
		}

		/// <summary>
		/// 内部処理
		/// </summary>
		/// <param name="msgData"></param>
		/// <param name="cn"></param>
		/// <returns></returns>
		protected override BaseResponseMsg SetDataToDatabaseInner(BaseMsgData msgData, IDbConnection cn)
		{
			HospitalMsgData hospitalMsgData =  (HospitalMsgData)msgData;

			ValidateMaster(hospitalMsgData, cn);
			HospitalUpdater updater = new HospitalUpdater();
			updater.InsertOrUpdate(hospitalMsgData, cn);
			return hospitalMsgData.Response;
		}

		/// <summary>
		/// マスタチェック
		/// </summary>
		/// <param name="omsg"></param>
		private void ValidateMaster(HospitalMsgData msgData, IDbConnection cn)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());

				#region 患者情報部
				ValidatePatient(msgData.Request.MsgBody.PatientAttribute, command);
				#endregion
			}
		}

		#endregion
	}
}
