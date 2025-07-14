using RISBizLibrary.Data;

namespace RISBizLibrary.Updater.Table.FROMHIINFO
{
	public abstract class  FROMHISINFOState
	{
		public virtual ReceiveMsgData Data
		{
			get;
			set;
		}

		public FROMHISINFOState(ReceiveMsgData data)
		{
			Data = data;
		}

		public abstract string GetRIS_ID();

		public abstract string GetMESSAGETYPE();

		public abstract string GetMESSAGEID1();

		public abstract string GetMESSAGEID2();

	}
}
