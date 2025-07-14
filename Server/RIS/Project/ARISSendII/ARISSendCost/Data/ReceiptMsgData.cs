using RISBizLibrary.Data;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Order;
using RISCommonLibrary.Lib.Msg.Receipt;

namespace ARISSend.Data
{
	/// <summary>
	/// 受付メッセージのデータを保持する
	/// </summary>
	public class ReceiptMsgData : BaseSendMsgData
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
		public new ReceiptMsg Request
		{
			get
			{
				return base.Request as ReceiptMsg;
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
