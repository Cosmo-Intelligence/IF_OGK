using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using ARISSend.Data;
using RISBizLibrary.Data;
using RISBizLibrary.Utils;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.Receipt;
using RISCommonLibrary.Lib.Msg.Receipt;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISSend.Model.MessageCreator
{
	internal class ReceiptMsgCreator : BaseMsgCreator
	{
		#region field

		#endregion

		#region property

		/// <summary>
		/// ログ出力用名前
		/// </summary>
		public override string NameForLog
		{
			get
			{
				return "受付";
			}
		}

		/// <summary>
		/// REQUESTTYPEごとの定義
		/// </summary>
		private readonly RequestTypeData[] DefineByRequestType = 
		{
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT,
				ProcessingType = MsgConst.PROCESSING_TYPE_NEW
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT_CANCEL,
				ProcessingType = MsgConst.PROCESSING_TYPE_DELETE
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_IMAGE,
				ProcessingType = MsgConst.PROCESSING_TYPE_IMAGE
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_FIRST,
				ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_FIRST
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND,
				ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND_CANCEL,
				ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND_CANCEL
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION,
				ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION_CANCEL,
				ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND_CANCEL
			}
		};

		#region ISocketParameter メンバ

		public override string Host
		{
			get
			{
				return ConfigurationManager.AppSettings["ReceiptHost"].StringToString();
			}
		}

		public override int Port
		{
			get
			{
				return ConfigurationManager.AppSettings["ReceiptPort"].StringToInt32();
			}
		}

		public override int ConnectionTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ReceiptConnectionTimeout"].StringToInt32();
			}
		}

		public override int SendTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ReceiptSendTimeout"].StringToInt32();
			}
		}

		public override int ReceiveTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ReceiptReceiveTimeout"].StringToInt32();
			}
		}

		#endregion

		#endregion

		#region constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReceiptMsgCreator(): base()
		{
		}
		#endregion

		#region method
		
		#region IMessageCreator メンバ

		public override string[] GetRequestTypes()
		{
			return new[]
			{
				RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT_CANCEL,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_IMAGE,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_FIRST,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND_CANCEL,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION_CANCEL
			};
		}

		public override BaseMsg CreateMsg(ARISSend.Data.ToHisInfo toHisInfo, System.Data.IDbConnection cn)
		{
			_log.Info("受付情報を取得します");

			const string SQL_SELECT =
				"SELECT " +
					"o.kanja_id, " +
					"e.receiptdate, " +
					"TO_CHAR(e.receiptdate, 'YYYYMMDD') receiptdate_yyyymmdd, " +
					"TO_CHAR(e.receiptdate, 'HH24MI') receiptdate_hh24mi, " +
					"e.uketuke_tantou_id, " +
					"eo.addendum01 orderno " +
				"FROM " +
					"ordermaintable o " +
					"INNER JOIN exmaintable e " +
					"ON " +
						"e.ris_id = o.ris_id " +
					"INNER JOIN extendorderinfo eo " +
					"ON " +
						"eo.ris_id = o.ris_id " +
				"WHERE " +
					"o.ris_id = :ris_id ";

			const string FIELD_ORDERNO = "orderno";
			const string FIELD_KANJA_ID = "kanja_id";
			const string FIELD_RECEIPTDATE_YYYYMMDD = "receiptdate_yyyymmdd";
			const string FIELD_RECEIPTDATE_HH24MI = "receiptdate_hh24mi";
			const string FIELD_UKETUKE_TANTOU_ID = "uketuke_tantou_id";

			const string PARAM_RIS_ID = "ris_id";
			
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_RIS_ID, toHisInfo.RIS_ID);
				command.Parameters.Add(param);

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new DataNotFoundException(string.Format(
							"RIS_IDデータが見つかりませんでした。RIS_ID={0}", toHisInfo.RIS_ID));
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					ReceiptMsg msg = new ReceiptMsg();
					#region 通信制御部
					CommunicationControlAggregate cc = msg.MsgBody.CommunicationControl;
					cc.CreateCommunicationControlAgg();
					cc.TELEGRAPH_KIND.Data = MsgConst.TELEGRAPH_KIND_RECEIPT_INFO;
					cc.ERR_STATUS.Data = MsgConst.ERR_STATUS_NORMAL;
					cc.ORDER_NO.Data = reader.GetStringByDBString(FIELD_ORDERNO);
					#endregion

					#region 受付(進捗)情報部
					ReceiptAggregate rcpt = msg.MsgBody.Receipt;
					rcpt.PROCESSING_TYPE.Data = GetPROCESSING_TYPE(toHisInfo);
					rcpt.PATIENT_NO.Data = reader.GetStringByDBString(FIELD_KANJA_ID);
					rcpt.RECEIPT_DATE.Data = reader.GetStringByDBString(FIELD_RECEIPTDATE_YYYYMMDD);
					rcpt.RECEIPT_TIME.Data = reader.GetStringByDBString(FIELD_RECEIPTDATE_HH24MI);
					rcpt.RECEIPTOR_CODE.Data = reader.GetStringByDBString(FIELD_UKETUKE_TANTOU_ID);
					#endregion

					return msg;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		public override BaseMsgData CreateMsgData()
		{

			return new ReceiptMsgData();
		}

		private string GetPROCESSING_TYPE(ToHisInfo toHisInfo)
		{
			IEnumerable<RequestTypeData> defines = 
				from d in DefineByRequestType
				where string.Compare(d.RequestType, toHisInfo.RequestType, true) == 0
				select d;

			RequestTypeData define = defines.FirstOrDefault();
			if (define == null)
			{
				return "";
			}
			return define.ProcessingType;
		}

		#endregion

		#endregion

	}
}
