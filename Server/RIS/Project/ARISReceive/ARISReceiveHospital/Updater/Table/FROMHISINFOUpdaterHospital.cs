using ARISReceive.Updater.Table.FROMHIINFO;
using RISBizLibrary.Data;
using RISBizLibrary.Updater.Table;
using RISBizLibrary.Updater.Table.FROMHIINFO;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace ARISReceive.Updater.Table
{
	internal class FROMHISINFOUpdaterHospital : FROMHISINFOUpdater
	{
		protected override FROMHISINFOState GetState(BaseMsgData data)
		{
			CommunicationControlAggregate control = ((ICommunicationControl)(data.Request.Body)).CommunicationControl;
			PatientAttributeAggregate patient = ((IPatientAttribute)(data.Request.Body)).PatientAttribute;
			if (control.TELEGRAPH_KIND.TrimData == MsgConst.TELEGRAPH_KIND_HOSPITALIZE)
			{
				return new FROMHISINFOStateHospitalize(data as ReceiveMsgData);
			}
			throw new MsgAnomalyException(string.Format(
				"想定していない電文種別を受信しました。電文種別={0}",
					control.TELEGRAPH_KIND.TrimData));
		}
	}
}
