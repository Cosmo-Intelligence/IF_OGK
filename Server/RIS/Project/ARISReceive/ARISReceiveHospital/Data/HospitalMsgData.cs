using RISBizLibrary.Data;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Hospitalize;

namespace ARISReceive.Data
{
	public class HospitalMsgData : ReceiveMsgData
	{

		#region メッセージ

		/// <summary>
		/// オーダメッセージ
		/// </summary>
		public new HospitalizeMsg Request
		{
			get
			{
				return base.Request as HospitalizeMsg;
			}
			set
			{
				base.Request = value;
			}
		}

		#endregion

		#region メッセージから取得

		/// <summary>
		/// 患者ID
		/// </summary>
		public override string KanjaID
		{
			get
			{
				if (Request == null)
				{
					return "";
				}
				PatientAttributeAggregate agg = Request.MsgBody.PatientAttribute;
				return agg.PATIENT_NO.TrimData;
			}
		}
		#endregion
	}
}
