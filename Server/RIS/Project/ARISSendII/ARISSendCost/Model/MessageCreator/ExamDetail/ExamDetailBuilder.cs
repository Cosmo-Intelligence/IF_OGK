using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using RISBizLibrary.Data;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.Exam;
using RISCommonLibrary.Lib.Msg.Common.Exam.Detail;
using RISCommonLibrary.Lib.Utils;
using ARISSend.Config;
using ARISSend.Data;
using RISODPLibrary.Lib.Utils;

namespace ARISSend.Model.MessageCreator.ExamDetail
{
	internal class ExamDetailBuilder
	{
		#region field
		/// <summary>
		/// ログ
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// 検査タイプID
		/// </summary>
		private string _kensaTypeID;
		
		/// <summary>
		/// ToHisInfo
		/// </summary>
		private ARISSend.Data.ToHisInfo _toHisInfo;

		/// <summary>
		/// コネクション
		/// </summary>
		private System.Data.IDbConnection _cn;
		#endregion

		#region property

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="kensaTypeID"></param>
		/// <param name="toHisInfo"></param>
		/// <param name="cn"></param>
		public ExamDetailBuilder(string kensaTypeID, ARISSend.Data.ToHisInfo toHisInfo, System.Data.IDbConnection cn)
		{
			_kensaTypeID = kensaTypeID;
			_toHisInfo = toHisInfo;
			_cn = cn;
		}

		#endregion

		public void CreateMsgDetail(ExamAggregate exam)
		{
			if (_toHisInfo.RequestType == RQRISDBConst.TOHISINFO_REQUESTTYPE_EXAM_STOP)
			{//中止時は作成しない
				return;
			}

			List<BuiData> buiList = CreateBuiDataList(_toHisInfo);
			if (buiList.Count < 1)
			{
				throw new DataNotFoundException("部位情報が見つかりませんでした");
			}
			List<ZoueizaiData> zoueizaiList = CreateZoueizaiDataList(_toHisInfo);
			List<FilmData> filmList = CreateFilmDataList(_toHisInfo);
			List<InfuseData> infuseList = CreateInfuseDataList(_toHisInfo);
			List<OrderCommentData> orderCommentList = CreateOrderCommentDataList(_toHisInfo);

			//オーダコメントIDが存在するか
			bool commentFlg = IsOrderCommentID(orderCommentList);

			for (int idxBui = 0; idxBui < buiList.Count; idxBui++)
			{
				BuiData bui = buiList[idxBui];
				CreateMsgDetailByBuiData(bui, zoueizaiList,
					filmList, infuseList, commentFlg, exam);
			}
		}

		private bool IsOrderCommentID(List<OrderCommentData> orderCommentList)
		{
			//オーダコメントIDが存在するか
			RequestAdditionHandler handler = (RequestAdditionHandler)ConfigurationManager.GetSection("requestAdditionSection");
			foreach (OrderCommentData orderComment in orderCommentList)
			{
				if (handler.RadiologySectioCollection.Contains(orderComment.COMMENT_ID))
				{
					return true;
				}
			}
			return false;
		}

		private void CreateMsgDetailByBuiData(BuiData bui, List<ZoueizaiData> zoueizaiList,
			List<FilmData> filmList, List<InfuseData> infuseList, bool commentFlg, ExamAggregate exam)
		{
			#region 部位単位
			SetJA(bui, exam.DETAIL_DYNAMIC_SUMM);
			SetJB(bui, exam.DETAIL_DYNAMIC_SUMM);
			SetJH(bui, _kensaTypeID, exam.DETAIL_DYNAMIC_SUMM);
			SetJT(bui, _kensaTypeID, exam.DETAIL_DYNAMIC_SUMM);
			#endregion

			IEnumerable<ZoueizaiData> targetZoueizais = from z in zoueizaiList
														where z.RIS_ID == bui.RIS_ID &&
														   z.BUI_NO == bui.NO
														orderby z.RIS_ID, z.BUI_NO, z.NO
														select z;
			#region 造影剤単位
			SetJDs(targetZoueizais, exam.DETAIL_DYNAMIC_SUMM);
			#endregion

			IEnumerable<FilmData> targetFilms = from f in filmList
												where f.RIS_ID == bui.RIS_ID &&
														   f.BUI_NO == bui.NO
												orderby f.RIS_ID, f.BUI_NO, f.NO
												select f;
			#region フィルム単位
			SetJFs(targetFilms, exam.DETAIL_DYNAMIC_SUMM);
			#endregion

			IEnumerable<InfuseData> targetInfuses = from inf in infuseList
													where inf.RIS_ID == bui.RIS_ID &&
														   inf.BUI_NO == bui.NO
													orderby inf.RIS_ID, inf.BUI_NO, inf.NO
													select inf;
			#region 手技単位
			SetJJs(commentFlg, targetInfuses, exam.DETAIL_DYNAMIC_SUMM);
			SetJKs(targetInfuses, exam.DETAIL_DYNAMIC_SUMM);
			#endregion

			#region 造影剤単位
			SetJLs(targetZoueizais, exam.DETAIL_DYNAMIC_SUMM);
			#endregion
		}

		#region DBから取得

		private List<BuiData> CreateBuiDataList(ToHisInfo toHisInfo)
		{
			_log.Debug("実施部位情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"eb.ris_id, " +
				"eb.no, " +
				"eb.bui_id, " +
				"eb.houkou_id, " +
				"eb.kensahouhou_id, " +
				"eb.satueistatus, " +
				"b.buibunrui_id " +
			"FROM " +
				"exbuitable eb " +
				"INNER JOIN buimaster b " +
				"ON " +
					"b.bui_id = eb.bui_id " +
			"WHERE " +
				"eb.ris_id = :ris_id " +
			"ORDER BY " +
				"eb.ris_id, " +
				"eb.no ";

			const string FIELD_NO = "no";
			const string FIELD_BUI_ID = "bui_id";
			const string FIELD_HOUKOU_ID = "houkou_id";
			const string FIELD_KENSAHOUHOU_ID = "kensahouhou_id";
			const string FIELD_SATUEISTATUS = "satueistatus";
			const string FIELD_BUIBUNRUI_ID = "buibunrui_id";

			const string PARAM_RIS_ID = "ris_id";

			using (IDbCommand command = _cn.CreateCommand())
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
					List<BuiData> list = new List<BuiData>();
					while (reader.Read())
					{
						MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

						BuiData data = new BuiData();
						data.RIS_ID = toHisInfo.RIS_ID;
						data.NO = reader.GetStringByDBInt32(FIELD_NO);
						data.BUI_ID = reader.GetStringByDBString(FIELD_BUI_ID);
						data.HOUKOU_ID = reader.GetStringByDBString(FIELD_HOUKOU_ID);
						data.KENSAHOUHOU_ID = reader.GetStringByDBString(FIELD_KENSAHOUHOU_ID);
						data.SATUEISTATUS = reader.GetStringByDBString(FIELD_SATUEISTATUS);
						data.BUIBUNRUI_ID = reader.GetStringByDBString(FIELD_BUIBUNRUI_ID);
						list.Add(data);
					}

					return list;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		private List<ZoueizaiData> CreateZoueizaiDataList(ToHisInfo toHisInfo)
		{
			_log.Debug("実績造影剤情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"ez.ris_id, " +
				"ez.bui_no, " +
				"ez.no, " +
				"ez.parts_id, " +
				"ez.suuryou_iji, " +
				"ez.partsbunrui_id, " +
				"p.zoueizaiijitanni_id " +
			"FROM " +
				"exzoueizaitable ez " +
				"LEFT OUTER JOIN partsmaster p " +
				"ON " +
					"p.zoueizai_id = ez.parts_id " +
			"WHERE " +
				"ez.ris_id = :ris_id " +
			"ORDER BY " +
				"ez.ris_id, " +
				"ez.bui_no, " +
				"ez.no ";

			const string FIELD_BUI_NO = "bui_no";
			const string FIELD_NO = "no";
			const string FIELD_PARTS_ID = "parts_id";
			const string FIELD_SUURYOU_IJI = "suuryou_iji";
			const string FIELD_PARTSBUNRUI_ID = "partsbunrui_id";
			const string FIELD_ZOUEIZAIIJITANNI_ID = "zoueizaiijitanni_id";

			const string PARAM_RIS_ID = "ris_id";

			using (IDbCommand command = _cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_RIS_ID, _toHisInfo.RIS_ID);
				command.Parameters.Add(param);

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					List<ZoueizaiData> list = new List<ZoueizaiData>();
					while (reader.Read())
					{
						MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

						ZoueizaiData data = new ZoueizaiData();
						data.RIS_ID = toHisInfo.RIS_ID;
						data.BUI_NO = reader.GetStringByDBInt32(FIELD_BUI_NO);
						data.NO = reader.GetStringByDBInt32(FIELD_NO);
						data.PARTS_ID = reader.GetStringByDBString(FIELD_PARTS_ID);
						data.SUURYOU_IJI = reader.GetStringByDBDecimal(FIELD_SUURYOU_IJI);
						data.PARTSBUNRUI_ID = reader.GetStringByDBInt32(FIELD_PARTSBUNRUI_ID);
						data.ZOUEIZAIIJITANNI_ID = reader.GetStringByDBString(FIELD_ZOUEIZAIIJITANNI_ID);
						list.Add(data);
					}

					return list;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		private List<FilmData> CreateFilmDataList(ToHisInfo toHisInfo)
		{
			_log.Debug("実績フィルム情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"ef.ris_id, " +
				"ef.bui_no, " +
				"ef.no, " +
				"ef.film_id, " +
				"ef.used, " +
				"ef.loss, " +
				"ef.partition " +
			"FROM " +
				"exfilmtable ef " +
			"WHERE " +
				"ef.ris_id = :ris_id " +
			"ORDER BY " +
				"ef.ris_id, " +
				"ef.bui_no, " +
				"ef.no ";

			const string FIELD_BUI_NO = "bui_no";
			const string FIELD_NO = "no";
			const string FIELD_FILM_ID = "film_id";
			const string FIELD_USED = "used";
			const string FIELD_LOSS = "loss";
			const string FIELD_PARTITION = "partition";

			const string PARAM_RIS_ID = "ris_id";

			using (IDbCommand command = _cn.CreateCommand())
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
					List<FilmData> list = new List<FilmData>();
					while (reader.Read())
					{
						MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

						FilmData data = new FilmData();
						data.RIS_ID = toHisInfo.RIS_ID;
						data.BUI_NO = reader.GetStringByDBInt32(FIELD_BUI_NO);
						data.NO = reader.GetStringByDBInt32(FIELD_NO);
						data.FILM_ID = reader.GetStringByDBString(FIELD_FILM_ID);
						data.USED = reader.GetStringByDBInt32(FIELD_USED);
						data.LOSS = reader.GetStringByDBInt32(FIELD_LOSS);
						data.PARTITION = reader.GetStringByDBInt32(FIELD_PARTITION);

						list.Add(data);
					}

					return list;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		private List<InfuseData> CreateInfuseDataList(ToHisInfo toHisInfo)
		{
			_log.Debug("実績注射手技情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"ei.ris_id, " +
				"ei.bui_no, " +
				"ei.no, " +
				"ei.infuse_id, " +
				"ei.suuryou_iji, " +
				"ei.partsbunrui_id " +
			"FROM " +
				"exinfusetable ei " +
			"WHERE " +
				"ei.ris_id = :ris_id " +
			"ORDER BY " +
				"ei.ris_id, " +
				"ei.bui_no, " +
				"ei.no ";

			const string FIELD_BUI_NO = "bui_no";
			const string FIELD_NO = "no";
			const string FIELD_INFUSE_ID = "infuse_id";
			const string FIELD_SUURYOU_IJI = "suuryou_iji";
			const string FIELD_PARTSBUNRUI_ID = "partsbunrui_id";

			const string PARAM_RIS_ID = "ris_id";

			using (IDbCommand command = _cn.CreateCommand())
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
					List<InfuseData> list = new List<InfuseData>();
					while (reader.Read())
					{
						MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

						InfuseData data = new InfuseData();
						data.RIS_ID = toHisInfo.RIS_ID;
						data.BUI_NO = reader.GetStringByDBInt32(FIELD_BUI_NO);
						data.NO = reader.GetStringByDBInt32(FIELD_NO);

						data.INFUSE_ID = reader.GetStringByDBString(FIELD_INFUSE_ID);
						data.SUURYOU_IJI = reader.GetStringByDBInt32(FIELD_SUURYOU_IJI);
						data.PARTSBUNRUI_ID = reader.GetStringByDBInt32(FIELD_PARTSBUNRUI_ID);

						list.Add(data);
					}

					return list;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		private List<OrderCommentData> CreateOrderCommentDataList(ToHisInfo toHisInfo)
		{
			_log.Debug("オーダコメント情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"oc.ris_id, " +
				"oc.comment_seq, " +
				"oc.comment_kbn, " +
				"oc.comment_id " +
			"FROM " +
				"order_comment oc " +
			"WHERE " +
				"oc.ris_id = :ris_id " +
			"ORDER BY " +
				"oc.ris_id, " +
				"oc.comment_seq ";

			const string FIELD_COMMENT_SEQ = "comment_seq";
			const string FIELD_COMMENT_KBN = "comment_kbn";
			const string FIELD_COMMENT_ID = "comment_id";

			const string PARAM_RIS_ID = "ris_id";

			using (IDbCommand command = _cn.CreateCommand())
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
					List<OrderCommentData> list = new List<OrderCommentData>();
					while (reader.Read())
					{
						MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

						OrderCommentData data = new OrderCommentData();
						data.RIS_ID = toHisInfo.RIS_ID;
						data.COMMENT_SEQ = reader.GetStringByDBInt32(FIELD_COMMENT_SEQ);
						data.COMMENT_KBN = reader.GetStringByDBString(FIELD_COMMENT_KBN);
						data.COMMENT_ID = reader.GetStringByDBString(FIELD_COMMENT_ID);

						list.Add(data);
					}
					return list;
				}
				finally
				{
					reader.Close();
				}
			}
		}
		#endregion

		#region 実績部位単位

		private void SetJA(BuiData bui, ExamDetailDynamicArray detailArray)
		{
			ExamDetailAggregate detail = new ExamDetailAggregate();
			detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_ACT;
			detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
			detail.ITEM_CODE.Data = bui.BUI_ID;
			if (bui.SATUEISTATUS == RQRISDBConst.EXBUITABLE_SATUEISTATUS_CANCEL)
			{
				detail.SOME_CANCEL.Data = MsgConst.SOME_CANCEL_CANCEL;
			}
			detailArray.Add(detail);
		}

		private void SetJB(BuiData bui, ExamDetailDynamicArray detailArray)
		{
			ExamDetailAggregate detail = new ExamDetailAggregate();
			detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_BUI;
			detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
			detail.ITEM_CODE.Data = bui.BUIBUNRUI_ID;
			detailArray.Add(detail);
		}

		private void SetJH(BuiData bui, string kensaTypeID, ExamDetailDynamicArray detailArray)
		{
			if (bui.HOUKOU_ID == kensaTypeID + RQRISDBConst.KENSATYPE_ID_COMMON_SUFFIX)
			{
				return;
			}
			ExamDetailAggregate detail = new ExamDetailAggregate();
			detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_HOUKOU;
			detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
			detail.ITEM_CODE.Data = bui.HOUKOU_ID;
			detailArray.Add(detail);
		}

		private void SetJT(BuiData bui, string kensaTypeID, ExamDetailDynamicArray detailArray)
		{
			if (bui.KENSAHOUHOU_ID == kensaTypeID + RQRISDBConst.KENSATYPE_ID_COMMON_SUFFIX)
			{
				return;
			}
			ExamDetailAggregate detail = new ExamDetailAggregate();
			detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_TAII;
			detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
			detail.ITEM_CODE.Data = bui.KENSAHOUHOU_ID;
			detailArray.Add(detail);
		}

		#endregion

		#region 実績造影剤単位

		private void SetJDs(IEnumerable<ZoueizaiData> targetZoueizais, ExamDetailDynamicArray examDetailDynamicArray)
		{
			string [] bunruiZoeizai = ConfigurationManager.AppSettings["ExZoueizaiTableBunruiZoeizai"].StringToString().Split(',');
			string [] bunruiYakuzai = ConfigurationManager.AppSettings["ExZoueizaiTableBunruiYakuzai"].StringToString().Split(',');
			IEnumerable<string> bunrui = bunruiZoeizai.Concat<string>(bunruiYakuzai);
			
			IEnumerable<ZoueizaiData> JDZoueizai = from z in targetZoueizais
												   where bunrui.Contains(z.PARTSBUNRUI_ID)
												   select z;

			foreach (ZoueizaiData item in JDZoueizai)
			{
				ExamDetailAggregate detail = new ExamDetailAggregate();
				detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_DRUG;
				detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
				detail.ITEM_CODE.Data = item.PARTS_ID;
				detail.FIGURE.Data = GetSuryo(item.SUURYOU_IJI);
				detail.UNIT.Data = item.ZOUEIZAIIJITANNI_ID;
				examDetailDynamicArray.Add(detail);
			}
		}

		private void SetJLs(IEnumerable<ZoueizaiData> targetZoueizais, ExamDetailDynamicArray examDetailDynamicArray)
		{
			IEnumerable<string> bunrui = ConfigurationManager.AppSettings["ExZoueizaiTableBunruiZairyo"].StringToString().Split(',');
			IEnumerable<ZoueizaiData> JDZoueizai = from z in targetZoueizais
												   where bunrui.Contains(z.PARTSBUNRUI_ID)
												   select z;

			foreach (ZoueizaiData item in JDZoueizai)
			{
				ExamDetailAggregate detail = new ExamDetailAggregate();
				detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_MATERIAL;
				detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
				detail.ITEM_CODE.Data = item.PARTS_ID;
				detail.FIGURE.Data = GetSuryo(item.SUURYOU_IJI);
				detail.UNIT.Data = item.ZOUEIZAIIJITANNI_ID;
				examDetailDynamicArray.Add(detail);
			}
		}

		#endregion

		#region 実績手技単位
		
		private void SetJKs(IEnumerable<InfuseData> targetInfuses, ExamDetailDynamicArray examDetailDynamicArray)
		{
			IEnumerable<string> bunrui = ConfigurationManager.AppSettings["ExInfuseTableBunruiKasan"].StringToString().Split(',');
			IEnumerable<InfuseData> JDInfuses = from inf in targetInfuses
												where bunrui.Contains(inf.PARTSBUNRUI_ID)
												select inf;

			foreach (InfuseData item in JDInfuses)
			{
				ExamDetailAggregate detail = new ExamDetailAggregate();
				detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_KASAN;
				detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
				detail.ITEM_CODE.Data = item.INFUSE_ID;
				detail.FIGURE.Data = GetSuryo(item.SUURYOU_IJI);
				examDetailDynamicArray.Add(detail);
			}
		}

		private void SetJJs(bool commentFlg, IEnumerable<InfuseData> targetInfuses, ExamDetailDynamicArray examDetailDynamicArray)
		{
			IEnumerable<string> bunrui = ConfigurationManager.AppSettings["ExInfuseTableBunruiShugi"].StringToString().Split(',');
			IEnumerable<InfuseData> JDInfuses = from inf in targetInfuses
												where bunrui.Contains(inf.PARTSBUNRUI_ID)
												select inf;

			foreach (InfuseData item in JDInfuses)
			{
				RequestAdditionHandler handler = (RequestAdditionHandler)ConfigurationManager.GetSection("requestAdditionSection");
				RadiologyInfuseItemElement element = handler.RadiologyInfuseSectionCollection.Get(item.INFUSE_ID);
				if ((commentFlg) || (!commentFlg && element == null))
				{
					ExamDetailAggregate detail = new ExamDetailAggregate();
					detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_MANIPULATE;
					detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
					detail.ITEM_CODE.Data = item.INFUSE_ID;
					detail.FIGURE.Data = GetSuryo(item.SUURYOU_IJI);
					examDetailDynamicArray.Add(detail);
				}
			}
		}

		#endregion

		#region 実績フィルム単位
		
		private void SetJFs(IEnumerable<FilmData> targetFilms, ExamDetailDynamicArray examDetailDynamicArray)
		{
			foreach (FilmData f in targetFilms)
			{
				ExamDetailAggregate detail = new ExamDetailAggregate();
				detail.ITEM_KIND.Data = MsgConst.ORDER_ITEM_KIND_FILM;
				detail.COMMENT_KIND.Data = MsgConst.ORDER_COMMENT_KIND_DEFAULT;
				detail.ITEM_CODE.Data = f.FILM_ID;
				detail.FIGURE.Data = GetSuryo(f.USED);
				detail.LOSS.Data = GetSuryo(f.LOSS);
				detail.PARTITION_NUMBER.Data = f.PARTITION;
				examDetailDynamicArray.Add(detail);
			}
		}
		#endregion

		/// <summary>
		/// 数量取得
		/// </summary>
		/// <param name="suryo"></param>
		/// <returns></returns>
		/// <remarks>
		/// 整数部5桁＋小数部2桁でゼロ埋め
		/// </remarks>
		private string GetSuryo(string suryo)
		{
			if (string.IsNullOrEmpty(suryo))
			{
				return "";
			}
			decimal d;
			if (!decimal.TryParse(suryo, out d))
			{
				return "";
			}
			if (d > 99999.99m)
			{
				return "9999999";
			}
			return d.ToString("00000.00").Replace(".", "");
		}
	}
}
