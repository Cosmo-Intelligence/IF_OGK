using RISBizLibrary.Data;
using RISBizLibrary.Updater.Table.FROMHIINFO;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace ARISReceive.Updater.Table.FROMHIINFO
{
	public class FROMHISINFOStateHospitalize : FROMHISINFOState
	{

		public FROMHISINFOStateHospitalize(ReceiveMsgData data)
			: base(data)
		{

		}

		public override string GetRIS_ID()
		{
			return "";
		}

		public override string GetMESSAGETYPE()
		{
			return RQRISDBConst.FROMHISINFO_MESSAGETYPE_PATIENT;
		}

		public override string GetMESSAGEID1()
		{
			return Data.KanjaID;
		}

		public override string GetMESSAGEID2()
		{
			PatientAttributeAggregate agg = ((IPatientAttribute)(Data.Request.Body)).PatientAttribute;
			return agg.KANA_NAME.TrimData;
		}

	}
}
