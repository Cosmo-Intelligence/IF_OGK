using System;
using System.Configuration;
using System.Data;
using ReportInterface.Data;
using ReportInterface.Exceptions;
using ReportInterface.Properties;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ReportInterface.Model.Executor
{
	internal class OrderExecutor : BaseExecutor, IExecutor
	{
		#region field

		#endregion

		#region IExecutor メンバ
		
		public bool CanExecute(string requestType)
		{
			return string.Compare(requestType, ToReportManager.TOREPORTINFO_REQUESTTYPE_ORDER, true) == 0;
		}

		public void Execute(ToReportInfo t, IDbConnection cn)
		{
			_log.Info("新規オーダ処理をします");
			BaseData data = ReadFromRIS(t, cn);
			WriteToReport(data, t);
			_log.Info("新規オーダ処理をしました");
		}
		#endregion

		protected BaseData ReadFromRIS(ToReportInfo t, IDbConnection cn)
		{
			DataNewOrder data = new DataNewOrder();
			ReadFromRISForOrder(data.Order, t, cn);
			ReadFromRISForPatient(data.Order.PATID, data.Patient, t, cn);
			return data;
		}

		protected void ReadFromRISForOrder(DataNewOrderOrder order, ToReportInfo t,
			IDbConnection cn)
		{
			_log.Debug("オーダ情報を取得します");

			#region sql
			
			const string SQL_SELECT =
			"SELECT " +
				"o.ris_id, " +
				"SEQ_REPORTEXAM_ID.NEXTVAL id, " +
				"'1' hspid, " +
				"o.kanja_id patid, " +
				"p.kanasimei kana, " +
				"p.romasimei roma, " +
				"p.kanjisimei kanji, " +
				"p.sex sex, " +
				"TO_DATE(TO_CHAR(p.birthday,'FM00000000')) birthday, " +
				"o.kensa_date_age age, " +
				"TO_DATE(TO_CHAR(NVL(o.kensa_date,0),'FM00000000') || TO_CHAR(NVL(o.kensa_starttime,0),'FM000000'),'YYYYMMDDHH24MISS') examdate, " +
				"k.kensatype_name modality, " +
				"bb.buibunrui_name locus, " +
				"b.bui_name detaillocus, " +
				"eo.order_date requestdate, " +
				"s.section_name requestsection, " +
				"o.irai_doctor_name requestdoctor, " +
				"o.denpyo_nyugaikbn inoutpatient, " +
				"bt.byoutou_name ward, " +
				"CASE o.denpyo_nyugaikbn " +
					"WHEN '2' THEN o.denpyo_byoutou_id " +
					"ELSE NULL " +
				"END ward, " +
				"oi.kensa_siji purpose, " +
				"oi.rinsyou diagnosis, " +
				"oi.remarks remarks, " +
				"o.orderno odrid, " +
				"0 status, " +
				"er.examroom_name examroom, " +
				"eo.sikyu_flg require, " +
				"TRUNC(p.tall, 1) tall, " +
				"TRUNC(p.weight, 1) weight, " +
				"ocds.section_name diagnosissection " +
			"FROM " +
				"ordermaintable o " +
				"LEFT OUTER JOIN patientinfo p " +
				"ON " +
					"p.kanja_id = o.kanja_id " +
				"LEFT OUTER JOIN orderindicatetable oi " +
				"ON " +
					"oi.ris_id = o.ris_id " +
				"LEFT OUTER JOIN extendorderinfo eo " +
				"ON " +
					"eo.ris_id = o.ris_id " +
				"LEFT OUTER JOIN kensatypemaster k " +
				"ON " +
					"k.kensatype_id = o.kensatype_id " +
				"LEFT OUTER JOIN sectionmaster s " +
				"ON " +
					"s.section_id = o.irai_section_id " +
				"LEFT OUTER JOIN examroommaster er " +
				"ON " +
					"er.examroom_id = o.kensasitu_id " +
				"LEFT OUTER JOIN " +
				"( " +
					"SELECT " +
						"ob_inner.ris_id, " +
						"ob_inner.no, " +
						"ob_inner.bui_id " +
					"FROM " +
						"orderbuitable ob_inner " +
						"INNER JOIN " +
						"( " +
							"SELECT " +
								"ob_last.ris_id ris_id, " +
								"MAX(ob_last.no) no " +
							"FROM " +
								"orderbuitable ob_last " +
							"GROUP BY " +
								"ob_last.ris_id " +
						") ob_last_inner " +
						"ON " +
							"ob_last_inner.ris_id = ob_inner.ris_id AND " +
							"ob_last_inner.no = ob_inner.no " +
				") ob " +
				"ON " +
					"ob.ris_id = o.ris_id " +
				"LEFT OUTER JOIN buimaster b " +
				"ON " +
					"b.bui_id = ob.bui_id " +
				"LEFT OUTER JOIN buibunruimaster bb " +
				"ON " +
					"bb.buibunrui_id = b.buibunrui_id " +
				"LEFT OUTER JOIN diagnosis_section_master ds " +
				"ON " +
					"ds.comment_id = ob.bui_id " +
				"LEFT OUTER JOIN byoutoumaster bt " +
				"ON " +
					"bt.byoutou_id = o.denpyo_byoutou_id " +
				"LEFT OUTER JOIN " +
				"( " +
					"SELECT " +
						"oc.ris_id, " +
						"ds.section_name " +
					"FROM " +
						"order_comment oc " +
						"INNER JOIN " +
						"( " +
							"SELECT " +
								"oc_key.ris_id, " +
								"MIN(oc_key.comment_seq) comment_seq " +
							"FROM " +
								"order_comment oc_key " +
								"INNER JOIN diagnosis_section_master ds_key " +
								"ON " +
									"ds_key.comment_id = oc_key.comment_id " +
							"GROUP BY " +
								"oc_key.ris_id " +
						") min_oc " +
						"ON " +
							"min_oc.ris_id = oc.ris_id AND " +
							"min_oc.comment_seq = oc.comment_seq " +
						"LEFT OUTER JOIN diagnosis_section_master ds " +
						"ON " +
							"ds.comment_id = oc.comment_id " +
				") ocds " +
				"ON " +
					"ocds.ris_id = o.ris_id " +
			"WHERE " +
				"o.ris_id = :ris_id ";
			#endregion

			#region field定義
			
			const string FIELD_ID = "id";
			const string FIELD_HSPID = "hspid";
			const string FIELD_PATID = "patid";
			const string FIELD_KANA = "kana";
			const string FIELD_ROMA = "roma";
			const string FIELD_KANJI = "kanji";
			const string FIELD_SEX = "sex";
			const string FIELD_BIRTHDAY = "birthday";
			const string FIELD_AGE = "age";
			const string FIELD_EXAMDATE = "examdate";
			const string FIELD_MODALITY = "modality";
			const string FIELD_LOCUS = "locus";
			const string FIELD_DETAILLOCUS = "detaillocus";
			const string FIELD_REQUESTDATE = "requestdate";
			const string FIELD_REQUESTSECTION = "requestsection";
			const string FIELD_REQUESTDOCTOR = "requestdoctor";
			const string FIELD_INOUTPATIENT = "inoutpatient";
			const string FIELD_WARD = "ward";
			const string FIELD_PURPOSE = "purpose";
			const string FIELD_DIAGNOSIS = "diagnosis";
			const string FIELD_REMARKS = "remarks";
			const string FIELD_ODRID = "odrid";
			const string FIELD_STATUS = "status";
			const string FIELD_EXAMROOM = "examroom";
			const string FIELD_REQUIRE = "require";
			const string FIELD_TALL = "tall";
			const string FIELD_WEIGHT = "weight";
			const string FIELD_DIAGNOSISSECTION = "diagnosissection";
			#endregion

			const string PARAM_RIS_ID = "ris_id";

			using (IDbCommand command = cn.CreateCommand())
			{
                ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_RIS_ID, t.RIS_ID);
				command.Parameters.Add(param);

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new ReportDataNotFoundException(string.Format(
							"RIS_IDデータが見つかりませんでした。RIS_ID={0}", t.RIS_ID));
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					#region 設定する
					
					order.ID = reader.GetStringByDB(FIELD_ID);
					order.HSPID = reader.GetStringByDB(FIELD_HSPID);
					order.PATID = reader.GetStringByDB(FIELD_PATID);
					order.KANA = reader.GetStringByDB(FIELD_KANA);
					order.ROMA = reader.GetStringByDB(FIELD_ROMA);
					order.KANJI = reader.GetStringByDB(FIELD_KANJI);
					order.SEX = reader.GetStringByDB(FIELD_SEX);
					order.BIRTHDAY = reader.GetStringByDB(FIELD_BIRTHDAY);
					order.AGE = reader.GetStringByDB(FIELD_AGE);
					order.EXAMDATE = reader.GetStringByDB(FIELD_EXAMDATE);
					order.MODALITY = reader.GetStringByDB(FIELD_MODALITY);
					order.LOCUS = reader.GetStringByDB(FIELD_LOCUS);
					order.DETAILLOCUS = reader.GetStringByDB(FIELD_DETAILLOCUS);
					order.REQUESTDATE = reader.GetStringByDB(FIELD_REQUESTDATE);
					order.REQUESTSECTION = reader.GetStringByDB(FIELD_REQUESTSECTION);
					order.REQUESTDOCTOR = reader.GetStringByDB(FIELD_REQUESTDOCTOR);
					order.INOUTPATIENT = reader.GetStringByDB(FIELD_INOUTPATIENT);
					order.WARD = reader.GetStringByDB(FIELD_WARD);
					order.PURPOSE = reader.GetStringByDB(FIELD_PURPOSE);
					order.DIAGNOSIS = reader.GetStringByDB(FIELD_DIAGNOSIS);
					order.REMARKS = reader.GetStringByDB(FIELD_REMARKS);
					order.ODRID = reader.GetStringByDB(FIELD_ODRID);
					order.STATUS = reader.GetStringByDB(FIELD_STATUS);
					order.EXAMROOM = reader.GetStringByDB(FIELD_EXAMROOM);
					order.REQUIRE = reader.GetStringByDB(FIELD_REQUIRE);
					order.TALL = reader.GetStringByDB(FIELD_TALL);
					order.WEIGHT = reader.GetStringByDB(FIELD_WEIGHT);
					order.DIAGNOSISSECTION = reader.GetStringByDB(FIELD_DIAGNOSISSECTION);
					#endregion
				}
				finally
				{
					reader.Close();
				}
			}
		}

		protected void ReadFromRISForPatient(string kanjaID, DataNewOrderPatient patient, ToReportInfo t,
			IDbConnection cn)
		{
			_log.Debug("患者情報を取得します");

			const string SQL_SELECT =
			"SELECT " +
				"p.kanja_id id, " +
				"p.kanasimei kana, " +
				"p.romasimei roma, " +
				"p.kanjisimei kanji, " +
				"p.sex sex, " +
				"TO_DATE(TO_CHAR(p.birthday, 'FM00000000')) birthday, " +
				"'1' attribute, " +
				"TRUNC(p.tall, 1) tall, " +
				"TRUNC(p.weight, 1) weight " +
			"FROM " +
				"patientinfo p " +
			"WHERE " +
				"p.kanja_id = :kanja_id ";

			const string FIELD_ID = "id";
			const string FIELD_KANA = "kana";
			const string FIELD_ROMA = "roma";
			const string FIELD_KANJI = "kanji";
			const string FIELD_SEX = "sex";
			const string FIELD_BIRTHDAY = "birthday";
			const string FIELD_ATTRIBUTE = "attribute";
			const string FIELD_TALL = "tall";
			const string FIELD_WEIGHT = "weight";

			const string PARAM_KANJA_ID = "kanja_id";

			using (IDbCommand command = cn.CreateCommand())
			{
                ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_KANJA_ID, kanjaID);
				command.Parameters.Add(param);

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new ReportDataNotFoundException(string.Format(
							"KANJA_IDデータが見つかりませんでした。KANJA_ID={0}", kanjaID));
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					patient.ID = reader.GetStringByDB(FIELD_ID);
					patient.KANA = reader.GetStringByDB(FIELD_KANA);
					patient.ROMA = reader.GetStringByDB(FIELD_ROMA);
					patient.KANJI = reader.GetStringByDB(FIELD_KANJI);
					patient.SEX = reader.GetStringByDB(FIELD_SEX);
					patient.BIRTHDAY = reader.GetStringByDB(FIELD_BIRTHDAY);
					patient.ATTRIBUTE = reader.GetStringByDB(FIELD_ATTRIBUTE);
					patient.TALL = reader.GetStringByDB(FIELD_TALL);
					patient.WEIGHT = reader.GetStringByDB(FIELD_WEIGHT);
				}
				finally
				{
					reader.Close();
				}
			}
		}

		protected void WriteToReport(BaseData data, ToReportInfo t)
		{
			String connectionStringReport = Settings.Default.ConnectionStringReport;
            IDbConnection connectionReport = ODPConnectionUtils.Connect(connectionStringReport);
			try
			{
				WriteToReportTran(data, t, connectionReport);
			}
			finally
			{
				connectionReport.Close();
			}
		}

		protected void WriteToReportTran(BaseData data, ToReportInfo t, IDbConnection cn)
		{
			IDbTransaction tr = cn.BeginTransaction();
			_log.Debug("BeginTransactionしました");
			try
			{
				using (IDbCommand command = cn.CreateCommand())
				{
                    ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
					command.Transaction = tr;
					WriteToReportOrder(data, t, command);
                    WriteToReportPatient(data, t, command);
					tr.Commit();
					_log.Debug("Commitしました");
				}
			}
			catch (Exception)
			{
				tr.Rollback();
				_log.Debug("Rollbackしました");
				throw;
			}
		}

        /// <summary>
        /// ExamBuiInfoテーブルへのレコード登録
        /// ExamInfoテーブルへのレコード登録と同じタイミングで、レコード登録
        /// 2019.05.07 仕様変更により追加機能
        /// </summary>
        /// <param name="order"></param>
        /// <param name="command"></param>
        private void Insert_ExamBuiInfo(DataNewOrderOrder order, IDbCommand command)
        {
            const string INSERT_SQL =
            "INSERT INTO exambuiinfo ( " +
                "id, " +
                "no, " +
                "bui_name, " +
                "houkou_name, " +
                "sayuu_name, " +
                "houhou_name, " +
                "buibunrui_name " +
            ") VALUES ( " +
                ":prm_id, " +
                "1, " +
                ":prm_detaillocus, " +
                "null, " +
                "null, " +
                "null, " +
                ":prm_locus " +
            ") ";
            command.CommandText = INSERT_SQL;
            command.Parameters.Clear();
            command.SetStringToCommand("prm_id", order.ID);
            command.SetStringToCommand("prm_locus", order.LOCUS);
            command.SetStringToCommand("prm_detaillocus", order.DETAILLOCUS);

            MiscUtils.WriteDbCommandLogForLog4net(command, _log);
            _log.DebugFormat("SQL実行します");
            try
            {
                int count = Convert.ToInt32(command.ExecuteNonQuery());
                _log.DebugFormat("INSERT INTO exambuiinfo 対象{0}件 - ID = {1}", count, order.ID);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("INSERT INTO exambuiinfo エラー：{0}", ex.Message);
                throw ex;
            }
        }

        protected void WriteToReportOrder(BaseData data, ToReportInfo t, IDbCommand command)
		{
			#region sql
			
			const string INSERT_SQL =
			"INSERT INTO examinfo " +
			"( " +
				"id, " +
				"hspid, " +
				"patid, " +
				"kana, " +
				"roma, " +
				"kanji, " +
				"sex, " +
				"birthday, " +
				"age, " +
				"examdate, " +
				"modality, " +
				"locus, " +
				"detaillocus, " +
				"requestdate, " +
				"requestsection, " +
				"requestdoctor, " +
				"inoutpatient, " +
				"ward, " +
				"purpose, " +
				"diagnosis, " +
				"remarks, " +
                "odrid, " +
                // ■y_higuchi 2019.03 - 仕様変更 - ACNOへODRIDと同じ値を入れる - add - ここから
                "acno, " +
                // ■y_higuchi 2019.03 - 仕様変更 - ACNOへODRIDと同じ値を入れる - add - ここまで
                "status, " +
				"examroom, " +
				"require , " +
				"tall, " +
				"weight, " +
				"diagnosissection " +
			") " +
			"VALUES " +
			"( " +
				":id, " +
				":hspid, " +
				":patid, " +
				":kana, " +
				":roma, " +
				":kanji, " +
				":sex, " +
				":birthday, " +
				":age, " +
				":examdate, " +
				":modality, " +
				":locus, " +
				":detaillocus, " +
				":requestdate, " +
				":requestsection, " +
				":requestdoctor, " +
				":inoutpatient, " +
				":ward, " +
				":purpose, " +
				":diagnosis, " +
				":remarks, " +
				":odrid, " +
                // ■y_higuchi 2019.03 - 仕様変更 - ACNOへODRIDと同じ値を入れる - add - ここから
                ":odrid, " +
                // ■y_higuchi 2019.03 - 仕様変更 - ACNOへODRIDと同じ値を入れる - add - ここまで
                ":status, " +
				":examroom, " +
				":require , " +
				":tall, " +
				":weight, " +
				":diagnosissection " +
			") ";
			#endregion

			#region パラメータ
			
			const string PARAM_NAME_ID = "id";
			const string PARAM_NAME_HSPID = "hspid";
			const string PARAM_NAME_PATID = "patid";
			const string PARAM_NAME_KANA = "kana";
			const string PARAM_NAME_ROMA = "roma";
			const string PARAM_NAME_KANJI = "kanji";
			const string PARAM_NAME_SEX = "sex";
			const string PARAM_NAME_BIRTHDAY = "birthday";
			const string PARAM_NAME_AGE = "age";
			const string PARAM_NAME_EXAMDATE = "examdate";
			const string PARAM_NAME_MODALITY = "modality";
			const string PARAM_NAME_LOCUS = "locus";
			const string PARAM_NAME_DETAILLOCUS = "detaillocus";
			const string PARAM_NAME_REQUESTDATE = "requestdate";
			const string PARAM_NAME_REQUESTSECTION = "requestsection";
			const string PARAM_NAME_REQUESTDOCTOR = "requestdoctor";
			const string PARAM_NAME_INOUTPATIENT = "inoutpatient";
			const string PARAM_NAME_WARD = "ward";
			const string PARAM_NAME_PURPOSE = "purpose";
			const string PARAM_NAME_DIAGNOSIS = "diagnosis";
			const string PARAM_NAME_REMARKS = "remarks";
			const string PARAM_NAME_ODRID = "odrid";
			const string PARAM_NAME_STATUS = "status";
			const string PARAM_NAME_EXAMROOM = "examroom";
			const string PARAM_NAME_REQUIRE = "require";
			const string PARAM_NAME_TALL = "tall";
			const string PARAM_NAME_WEIGHT = "weight";
			const string PARAM_NAME_DIAGNOSISSECTION = "diagnosissection";
			#endregion

			command.CommandText = INSERT_SQL;

			DataNewOrderOrder order = ((DataNewOrderOrder)((DataNewOrder)data).Order);
			command.Parameters.Clear();

			#region パラメータ設定

			command.SetStringToCommand(PARAM_NAME_ID, order.ID);
			command.SetStringToCommand(PARAM_NAME_HSPID, order.HSPID);
			command.SetStringToCommand(PARAM_NAME_PATID, order.PATID);
			command.SetStringToCommand(PARAM_NAME_KANA, order.KANA);
			command.SetStringToCommand(PARAM_NAME_ROMA, order.ROMA);
			command.SetStringToCommand(PARAM_NAME_KANJI, order.KANJI);
			command.SetStringToCommand(PARAM_NAME_SEX, order.SEX);
			command.SetDateTimeToCommand(PARAM_NAME_BIRTHDAY, order.BIRTHDAY);
			command.SetIntToCommand(PARAM_NAME_AGE, order.AGE);
			command.SetDateTimeToCommand(PARAM_NAME_EXAMDATE, order.EXAMDATE);
			command.SetStringToCommand(PARAM_NAME_MODALITY, order.MODALITY);

            // ■y_higuchi 2019.05- 仕様変更 - 「,」囲み - del - ここから
            // command.SetStringToCommand(PARAM_NAME_LOCUS, order.LOCUS);
            // command.SetStringToCommand(PARAM_NAME_DETAILLOCUS, order.DETAILLOCUS);
            // ■y_higuchi 2019.05- 仕様変更 - 「,」囲み - del - ここまで - add - ここから
            if (order.LOCUS == null)
            {
                command.SetStringToCommand(PARAM_NAME_LOCUS, order.LOCUS);
            }
            else
            {
                String strComma = order.LOCUS;
                if (strComma.Length > 0)
                {
                    if (strComma.Substring(0, 1) != ",")
                    {
                        strComma = "," + strComma;
                    }
                    if (strComma.Substring(strComma.Length - 1, 1) != ",")
                    {
                        strComma = strComma + ",";
                    }
                }
                command.SetStringToCommand(PARAM_NAME_LOCUS, strComma);
            }
            if (order.DETAILLOCUS == null)
            {
                command.SetStringToCommand(PARAM_NAME_DETAILLOCUS, order.DETAILLOCUS);
            }
            else
            {
                String strComma2 = order.DETAILLOCUS;
                if (strComma2.Length > 0)
                {
                    if (strComma2.Substring(0, 1) != ",")
                    {
                        strComma2 = "," + strComma2;
                    }
                    if (strComma2.Substring(strComma2.Length - 1, 1) != ",")
                    {
                        strComma2 = strComma2 + ",";
                    }
                }
                command.SetStringToCommand(PARAM_NAME_DETAILLOCUS, strComma2);
            }
            // ■y_higuchi 2019.05- 仕様変更 - 「,」囲み - add - ここまで

            command.SetDateTimeToCommand(PARAM_NAME_REQUESTDATE, order.REQUESTDATE);
			command.SetStringToCommand(PARAM_NAME_REQUESTSECTION, order.REQUESTSECTION);
			command.SetStringToCommand(PARAM_NAME_REQUESTDOCTOR, order.REQUESTDOCTOR);
			command.SetIntToCommand(PARAM_NAME_INOUTPATIENT, order.INOUTPATIENT);
			command.SetStringToCommand(PARAM_NAME_WARD, order.WARD);
			command.SetStringToCommand(PARAM_NAME_PURPOSE, order.PURPOSE);
			command.SetStringToCommand(PARAM_NAME_DIAGNOSIS, order.DIAGNOSIS);
			command.SetStringToCommand(PARAM_NAME_REMARKS, order.REMARKS);

            // ■y_higuchi 2019.03 - 仕様変更 - t.MessageID1 からではなく RISDB.ORDERNO そのものを入れる - del - ここから
            // command.SetStringToCommand(PARAM_NAME_ODRID, t.MessageID1);
            // ■y_higuchi 2019.03 - 仕様変更 - t.MessageID1 からではなく RISDB.ORDERNO そのものを入れる - del - ここまで - add - ここから
            command.SetStringToCommand(PARAM_NAME_ODRID, order.ODRID);
            // ■y_higuchi 2019.03 - 仕様変更 - t.MessageID1 からではなく RISDB.ORDERNO そのものを入れる - add - ここまで

            command.SetIntToCommand(PARAM_NAME_STATUS, order.STATUS);
			command.SetStringToCommand(PARAM_NAME_EXAMROOM, order.EXAMROOM);
			command.SetStringToCommand(PARAM_NAME_REQUIRE, order.REQUIRE);
			command.SetDecimalToCommand(PARAM_NAME_TALL, order.TALL);
			command.SetDecimalToCommand(PARAM_NAME_WEIGHT, order.WEIGHT);
			command.SetStringToCommand(PARAM_NAME_DIAGNOSISSECTION, order.DIAGNOSISSECTION);

			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("SQL実行します");
            try
            {
                int count = Convert.ToInt32(command.ExecuteNonQuery());
                _log.DebugFormat("WriteToReportOrder 対象{0}件 - ID = {1}", count, order.ID);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("WriteToReportOrder エラー：{0}", ex.Message);
                throw ex;
            }

            // ■y_higuchi 2019.05- 仕様変更 - ExamBuiInfoテーブルへのレコード登録 - add - ここから
            Insert_ExamBuiInfo(order, command);
            // ■y_higuchi 2019.05- 仕様変更 - ExamBuiInfoテーブルへのレコード登録 - add - ここまで
        }

        protected void WriteToReportPatient(BaseData data, ToReportInfo t, IDbCommand command)
		{
			#region sql
			
			const string INSERT_SQL =
			"MERGE INTO patientinfo p " +
			"USING  " +
			"( " +
				"SELECT " +
					":id id, " +
					":kana kana, " +
					":roma roma, " +
					":kanji kanji, " +
					":sex sex, " +
					":birthday birthday, " +
					":attribute attribute, " +
					":tall tall, " +
					":weight weight " +
				"FROM " +
					"DUAL " +
			") pn " +
			"ON  " +
			"( p.id = pn.id) " +
			"WHEN MATCHED THEN " + //-- 既存レコードの更新
				"UPDATE SET " +
					"kana = pn.kana, " +
					"roma = pn.roma, " +
					"kanji = pn.kanji, " +
					"sex = pn.sex, " +
					"birthday = pn.birthday, " +
					"attribute = pn.attribute, " +
					"tall = pn.tall, " +
					"weight = pn.weight " +
			"WHEN NOT MATCHED THEN " + //-- 新規レコードの作成
				"INSERT " +
				"( " +
					"id, " +
					"kana, " +
					"roma, " +
					"kanji, " +
					"sex, " +
					"birthday, " +
					"attribute, " +
					"tall, " +
					"weight " +
				") " +
				"VALUES " +
				"( " +
					"pn.id, " +
					"pn.kana, " +
					"pn.roma, " +
					"pn.kanji, " +
					"pn.sex, " +
					"pn.birthday, " +
					"pn.attribute, " +
					"pn.tall, " +
					"pn.weight " +
				") ";
			#endregion

			#region パラメータ
			const string PARAM_NAME_ID = "id";
			const string PARAM_NAME_KANA = "kana";
			const string PARAM_NAME_ROMA = "roma";
			const string PARAM_NAME_KANJI = "kanji";
			const string PARAM_NAME_SEX = "sex";
			const string PARAM_NAME_BIRTHDAY = "birthday";
			const string PARAM_NAME_ATTRIBUTE = "attribute";
			const string PARAM_NAME_TALL = "tall";
			const string PARAM_NAME_WEIGHT = "weight";
			#endregion

			command.CommandText = INSERT_SQL;

			DataNewOrderPatient patient = ((DataNewOrderPatient)((DataNewOrder)data).Patient);
			command.Parameters.Clear();
			command.SetStringToCommand(PARAM_NAME_ID, patient.ID);
			command.SetStringToCommand(PARAM_NAME_KANA, patient.KANA);
			command.SetStringToCommand(PARAM_NAME_ROMA, patient.ROMA);
			command.SetStringToCommand(PARAM_NAME_KANJI, patient.KANJI);
			command.SetStringToCommand(PARAM_NAME_SEX, patient.SEX);
			command.SetDateTimeToCommand(PARAM_NAME_BIRTHDAY, patient.BIRTHDAY);
			command.SetStringToCommand(PARAM_NAME_ATTRIBUTE, patient.ATTRIBUTE);
			command.SetDecimalToCommand(PARAM_NAME_TALL, patient.TALL);
			command.SetDecimalToCommand(PARAM_NAME_WEIGHT, patient.WEIGHT);

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("SQL実行します");
			int count = Convert.ToInt32(command.ExecuteNonQuery());
			_log.DebugFormat("対象{0}件", count);
		}
	}
}
