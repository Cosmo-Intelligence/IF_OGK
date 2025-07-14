using RISBizLibrary.Receive.Model;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.ConnectionClose;
using RISCommonLibrary.Lib.Msg.ConnectionOpen;
using RISCommonLibrary.Lib.Msg.Hospitalize;
using RISCommonLibrary.Lib.Msg.Response;

namespace ARISReceive.Model
{
	class MessageHandlerHospital : MessageHandler
	{

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MessageHandlerHospital(): base()
		{

		}

		#endregion

		#region method

		#endregion

		/// <summary>
		/// DB処理を任せるクラスを生成する
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		protected override BaseResponseMsg SetDBSetter(BaseMsg msg)
		{
			if (msg.RequestKind == RequestKindEnum.rkRequestOpen)
			{
				_log.Info("回線接続要求を受信しました");
			}
			if (msg.RequestKind == RequestKindEnum.rkRequestClose)
			{
				_log.Info("回線切断要求を受信しました");
			}
			if (msg.RequestKind == RequestKindEnum.rkHospitalize)
			{
				_log.Info("入退院情報を受信しました");
				HospitalMsgDBSetter dbsetter = new HospitalMsgDBSetter();
				return dbsetter.SetDataToDatabase(msg);
			}
			return new ResponseMsg(); //回線接続要求・回線切断要求はDB接続処理なし
		}
	
		/// <summary>
		/// メッセージファクトリに登録する
		/// </summary>
		protected override void RegistFactory()
		{
			MsgFactory.Instance.RegistMsg(typeof(ConnectionOpenMsg));
			MsgFactory.Instance.RegistMsg(typeof(ConnectionCloseMsg));
			MsgFactory.Instance.RegistMsg(typeof(HospitalizeMsg));
		}

	}
}
