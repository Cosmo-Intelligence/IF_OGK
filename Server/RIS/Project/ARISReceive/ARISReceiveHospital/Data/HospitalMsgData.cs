using RISBizLibrary.Data;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Hospitalize;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;

namespace ARISReceive.Data
{
	public class HospitalMsgData : ReceiveMsgData
	{

		#region 外からもらってくるもの

		/// <summary>
		/// 対象RIS_ID
		/// </summary>
		public string RIS_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 検査種別ID
		/// </summary>
		public string KensaTypeID
		{
			get;
			set;
		}

		#endregion
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
		/// RQRIS用オーダNo
		/// </summary>
		/// <remarks>
		/// RQRISのオーダNoカラムが16バイトなのに対して電文上は18バイトフルでくる
		/// 仕方が無いので、先頭2バイトを削って16バイトで格納している
		/// オリジナルの方はEXTENDORDERINFO.ADDENDUM01に設定する
		/// </remarks>
		public string OrderNoForRQRIS
		{
			get
			{
				if (Request == null)
				{
					return "";
				}
				CommunicationControlAggregate agr = Request.MsgBody.CommunicationControl;
				const int RQRIS_ORDER_NO_LENGTH = 16;
				if (agr.ORDER_NO.TrimData.Length < RQRIS_ORDER_NO_LENGTH)
				{
					return agr.ORDER_NO.TrimData;
				}
				return agr.ORDER_NO.TrimData.Remove(0, agr.ORDER_NO.TrimData.Length - RQRIS_ORDER_NO_LENGTH);
			}
		}

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
