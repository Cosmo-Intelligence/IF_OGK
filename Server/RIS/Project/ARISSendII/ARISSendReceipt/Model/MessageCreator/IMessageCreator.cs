using System;
using System.Data;
using RISCommonLibrary.Lib.Msg;
using ARISSend.Data;
using RISBizLibrary.Data;

namespace ARISSend.Model.MessageCreator
{
    interface IMessageCreator
    {

		bool Connected
		{
			get;
		}

		void ConnectTcp();
		void DisConnectTcp();

		BaseMsgData RequestOpen();
		BaseMsgData RequestClose();

		BaseMsg CreateMsg(ToHisInfo toHisInfo,
			IDbConnection cn);

		string[] GetRequestTypes();

		BaseMsgData CreateMsgData();

		void SendSocket(BaseMsgData msgData);

    }
}
