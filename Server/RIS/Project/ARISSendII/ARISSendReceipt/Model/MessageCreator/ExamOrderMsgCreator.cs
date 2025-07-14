using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using ARISSend.Data;
using ARISSend.Model.MessageCreator.ExamDetail;
using RISBizLibrary.Data;
using RISBizLibrary.Utils;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.Exam;
using RISCommonLibrary.Lib.Msg.Exam;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISSend.Model.MessageCreator
{
	internal class ExamOrderMsgCreator : BaseMsgCreator
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
				return "実施";
			}
		}
	
		/// <summary>
		/// REQUESTTYPEごとの定義
		/// </summary>
		private readonly RequestTypeData[] DefineByRequestType = 
		{
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM,
				ProcessingType = MsgConst.PROCESSING_TYPE_NEW_EXAM
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM_RESEND,
				ProcessingType = MsgConst.PROCESSING_TYPE_UPDATE_EXAM
			},
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM_STOP,
				ProcessingType = MsgConst.PROCESSING_TYPE_STOP_EXAM
			}
		};

		#region ISocketParameter メンバ

		public override string Host
		{
			get
			{
				return ConfigurationManager.AppSettings["ExamHost"].StringToString();
			}
		}

		public override int Port
		{
			get
			{
				return ConfigurationManager.AppSettings["ExamPort"].StringToInt32();
			}
		}

		public override int ConnectionTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ExamConnectionTimeout"].StringToInt32();
			}
		}

		public override int SendTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ExamSendTimeout"].StringToInt32();
			}
		}

		public override int ReceiveTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ExamReceiveTimeout"].StringToInt32();
			}
		}

		#endregion

		#endregion

		#region constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ExamOrderMsgCreator()
			: base()
		{
		}
		#endregion

		#region IMessageCreator メンバ

		public override string[] GetRequestTypes()
		{
			return new[]
			{
				RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM_RESEND,
				RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM_STOP
			};
		}

		public override BaseMsg CreateMsg(ARISSend.Data.ToHisInfo toHisInfo, System.Data.IDbConnection cn)
		{
			_log.Info("実施情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"o.kanja_id, " +
				"e.examenddate, " +
				"TO_CHAR(e.examenddate, 'YYYYMMDD') examenddate_yyyymmdd, " +
				"TO_CHAR(e.examenddate, 'HH24MI') examenddate_hh24mi, " +
				"e.bikou, " +
				"e.kensasitu_id, " +
				"e.kensa_gisi_id, " +
				"e.kensatype_id, " +
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
			const string FIELD_EXAMENDDATE_YYYYMMDD = "examenddate_yyyymmdd";
			const string FIELD_EXAMENDDATE_HH24MI = "examenddate_hh24mi";
			const string FIELD_BIKOU = "bikou";
			const string FIELD_KENSASITU_ID = "kensasitu_id";
			const string FIELD_KENSA_GISI_ID = "kensa_gisi_id";
			const string FIELD_KENSATYPE_ID = "kensatype_id";

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
							"RISデータが見つかりませんでした。RIS_ID={0}", toHisInfo.RIS_ID));
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					ExamMsg msg = new ExamMsg();

					#region 通信制御部
					CommunicationControlAggregate cc = msg.MsgBody.CommunicationControl;
					cc.CreateCommunicationControlAgg();
					cc.TELEGRAPH_KIND.Data = MsgConst.TELEGRAPH_KIND_EXAM_INFO;
					cc.ERR_STATUS.Data = MsgConst.ERR_STATUS_NORMAL;
					cc.ORDER_NO.Data = reader.GetStringByDBString(FIELD_ORDERNO);
					#endregion

					#region 実施情報部
					ExamAggregate exam = msg.MsgBody.Exam;
					exam.PROCESSING_TYPE.Data = GetPROCESSING_TYPE(toHisInfo);
					exam.PATIENT_NO.Data = reader.GetStringByDBString(FIELD_KANJA_ID);
					exam.EXAM_DATE.Data = reader.GetStringByDBString(FIELD_EXAMENDDATE_YYYYMMDD);
					exam.EXAM_TIME.Data = reader.GetStringByDBString(FIELD_EXAMENDDATE_HH24MI);
					exam.EXAM_COMMENT.Data = MBCSHelper.Copy(reader.GetStringByDBString(FIELD_BIKOU), 1, 
						ExamNodeInfo.EXAM_EXAM_COMMENT.FieldLength);
					exam.SHOOT_ROOM.Data = reader.GetStringByDBString(FIELD_KENSASITU_ID);
					exam.EXAMINATOR_CODE.Data = reader.GetStringByDBString(FIELD_KENSA_GISI_ID);

					string kensatypeID = reader.GetStringByDBString(FIELD_KENSATYPE_ID);

					ExamDetailBuilder builder = new ExamDetailBuilder(kensatypeID, toHisInfo, cn);
					builder.CreateMsgDetail(exam);
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


    }
}