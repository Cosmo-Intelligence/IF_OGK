using RISBizLibrary.Data;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Order;
using RISCommonLibrary.Lib.Msg.RequestPatient;

namespace ARISSend.Data
{
	/// <summary>
	/// 患者要求メッセージのデータを保持する
	/// </summary>
	public class PatientMsgData : BaseSendMsgData
	{
		#region field

		#endregion

		#region property

		#region 外からもらってくるもの

		#endregion

		#region メッセージ
		
		/// <summary>
		/// メッセージ
		/// </summary>
		public new RequestPatientMsg Request
		{
			get
			{
				return base.Request as RequestPatientMsg;
			}
			set
			{
				base.Request = value;
			}
		}

		#endregion

		#region メッセージから取得
		
		#endregion

		#endregion

		#region method

		#endregion
	}
}
