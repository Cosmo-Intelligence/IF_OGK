using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using ReportInterface.Data;
using ReportInterface.Exceptions;
using ReportInterface.Properties;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;
using ReportInterface.Config;

namespace ReportInterface.Model.Executor
{
	internal class ExamFinishExecutor : BaseUpdateExecutor, IExecutor
	{
		#region field


		#endregion

		#region IExecutor メンバ

		public void Execute(ReportInterface.Data.ToReportInfo t, System.Data.IDbConnection cn)
		{
			_log.Info("検査終了処理します");

			BaseData data = ReadFromRIS(t, cn);
			WriteToReport(t, data);

			_log.Info("検査終了処理しました");
		}

		public bool CanExecute(string requestType)
		{
			if (string.Compare(ToReportManager.TOREPORTINFO_REQUESTTYPE_EXAM_FINISH, requestType) == 0)
			{
				return true;
			}
			if (string.Compare(ToReportManager.TOREPORTINFO_REQUESTTYPE_EXAM_FINISH_RE, requestType) == 0)
			{
				return true;
			}
			return false;
		}

		#endregion

		private BaseData ReadFromRIS(ToReportInfo t, IDbConnection cn)
		{
			_log.Debug("検査完了情報を取得します");

			#region sql

			const string SQL_SELECT =
			"SELECT " + 
				"em.ris_id, " + 
				"ob.no, " + 
				"em.examenddate examdate, " + 
				"bb.buibunrui_name locus, " + 
				"b.bui_name detaillocus, " + 
				"'核種：' || eo.examdata01 || '、' || " + 
					"'使用量：' || eo.examdata02 || 'MBq、' || " + 
					"'血糖値：' || eo.examdata04 || 'mg/dL' purpose, " + 
            // ■y_higuchi 2019.03 - 仕様変更 - ステータス値の変更 - add - ここから
                "20 status, " +
            // ■y_higuchi 2019.03 - 仕様変更 - ステータス値の変更 - add - ここまで - del - ここから
            //    "4 status, " +
            // ■y_higuchi 2019.03 - 仕様変更 - ステータス値の変更 - del - ここまで
                "ez.zoueizai_names medicine, " + 
				"em.jisisya_name examinedoctor, " + 
				"em.kensatype_id, " + 
				"em.bikou, " + 
				"em.renraku_memo, " + 
				"CASE " + 
					"WHEN " + 
						"EXISTS " + 
							"( " + 
								"SELECT " + 
									"* " + 
								"FROM " + 
									"exinfusetable ei " + 
								"WHERE " + 
									"ei.ris_id = :ris_id AND " + 
									"ei.infuse_id IN ({0}) AND " + 
									"ei.suuryou_iji = 1 AND " + 
									"EXISTS " + 
										"( " + 
											"SELECT " + 
												"* " + 
											"FROM " + 
												"exbuitable eb " + 
											"WHERE " + 
												"eb.ris_id = ei.ris_id AND " + 
												"eb.no = ei.bui_no AND " + 
												"eb.satueistatus = '1' " + 
										") " + 
							") THEN 1 " + 
					"ELSE 0 " + 
				"END gazousindan1, " + 
				"p.kanasimei kana, " +
				"p.romasimei roma, " +
				"p.kanjisimei kanji, " +
				"p.sex sex, " +
				"TO_DATE(TO_CHAR(p.birthday,'FM00000000')) birthday " +
			"FROM " + 
				"exmaintable em " + 
				"LEFT OUTER JOIN " + 
					"( " + 
						"SELECT " + 
							"eb_inner.ris_id, " + 
							"eb_inner.no, " + 
							"eb_inner.bui_id " + 
						"FROM " + 
							"exbuitable eb_inner " + 
							"INNER JOIN " + 
								"( " + 
									"SELECT " + 
										"eb_last.ris_id ris_id, " + 
										"MAX(eb_last.no) no " + 
									"FROM " + 
										"exbuitable eb_last " + 
									"WHERE " + 
										"eb_last.satueistatus = '1' " + 
									"GROUP BY " + 
										"eb_last.ris_id " + 
								") eb_last_inner " + 
							"ON " + 
								"eb_last_inner.ris_id = eb_inner.ris_id AND " + 
								"eb_last_inner.no = eb_inner.no " + 
					") ob " + 
				"ON " + 
					"ob.ris_id = em.ris_id " + 
				"LEFT OUTER JOIN buimaster b " + 
				"ON " + 
					"b.bui_id = ob.bui_id " + 
				"LEFT OUTER JOIN buibunruimaster bb " + 
				"ON " + 
					"bb.buibunrui_id = b.buibunrui_id " + 
				"LEFT OUTER JOIN extendexaminfo eo " + 
				"ON " + 
					"eo.ris_id = em.ris_id " + 
				"LEFT OUTER JOIN " + 
					"( " + 
						"SELECT " + 
							"ez.ris_id, " + 
							"LISTAGG " + 
								"( " + 
									"p.zoueizai_name, ',' " + 
								") " + 
							"WITHIN GROUP " + 
								"( " + 
									"ORDER BY " + 
										"ez.ris_id, " + 
										"ez.bui_no, " + 
										"ez.no " + 
								") zoueizai_names " + 
						"FROM " + 
							"exzoueizaitable ez " + 
							"LEFT OUTER JOIN partsmaster p " + 
							"ON " + 
								"p.zoueizai_id = ez.parts_id " + 
						"WHERE " + 
							"EXISTS " + 
							"( " + 
								"SELECT " + 
									"* " + 
								"FROM " + 
									"exbuitable eb " + 
								"WHERE " + 
									"eb.ris_id = ez.ris_id AND " + 
									"eb.no = ez.bui_no AND " + 
									"eb.SATUEISTATUS = '1' " + 
							") " + 
						"GROUP BY " + 
							"ez.ris_id " + 
					") ez " + 
				"ON " + 
					"ez.ris_id = em.ris_id " + 
				"LEFT OUTER JOIN patientinfo p " +
				"ON " +
					"p.kanja_id = em.kanja_id " +
			"WHERE " + 
				"em.ris_id = :ris_id ";
			#endregion

			#region field定義
			const string FIELD_EXAMDATE = "examdate";
			const string FIELD_LOCUS = "locus";
			const string FIELD_DETAILLOCUS = "detaillocus";
			const string FIELD_PURPOSE = "purpose";
			const string FIELD_STATUS = "status";
			const string FIELD_MEDICINE = "medicine";
			const string FIELD_EXAMINEDOCTOR = "examinedoctor";
			const string FIELD_KENSATYPE_ID = "kensatype_id";
			const string FIELD_BIKOU = "bikou";
			const string FIELD_RENRAKU_MEMO = "renraku_memo";
			const string FIELD_GAZOUSINDAN1 = "gazousindan1";
			const string FIELD_KANA = "kana";
			const string FIELD_ROMA = "roma";
			const string FIELD_KANJI = "kanji";
			const string FIELD_SEX = "sex";
			const string FIELD_BIRTHDAY = "birthday";
			#endregion

			const string PARAM_RIS_ID = "ris_id";

			using (IDbCommand command = cn.CreateCommand())
			{
				RequestAdditionHandler handler = (RequestAdditionHandler)ConfigurationManager.GetSection("requestAdditionSection");
				int elementsCount = handler.RequestAdditionSectionCollection.Count;
				if (handler.RequestAdditionSectionCollection.Count == 0)
				{
					throw new ConfigException("画像診断加算のInfuseIDが設定されていません。");
				}

				List<string> infuseIDList = new List<string>();
				for (int idx =0; idx < elementsCount; idx++)
				{
					string infuseID = handler.RequestAdditionSectionCollection.Get(idx).InfuseID;
					if (string.IsNullOrEmpty(infuseID))
					{
						throw new ConfigException("画像診断加算のInfuseIDが空です。");
					}
					infuseIDList.Add("'" + infuseID + "'");
				}

				ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = string.Format(SQL_SELECT, string.Join(",", infuseIDList.ToArray()));

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
					DataExamFinish data = new DataExamFinish();

					data.EXAMDATE = reader.GetStringByDB(FIELD_EXAMDATE);
					data.LOCUS = reader.GetStringByDB(FIELD_LOCUS);
					data.DETAILLOCUS = reader.GetStringByDB(FIELD_DETAILLOCUS);
					data.PURPOSE = reader.GetStringByDB(FIELD_PURPOSE);
					data.STATUS = reader.GetStringByDB(FIELD_STATUS);
					data.MEDICINE = reader.GetStringByDB(FIELD_MEDICINE);
					data.EXAMINEDOCTOR = reader.GetStringByDB(FIELD_EXAMINEDOCTOR);
					data.KENSATYPE_ID = reader.GetStringByDB(FIELD_KENSATYPE_ID);
					data.BIKOU = reader.GetStringByDB(FIELD_BIKOU);
					data.RENRAKU_MEMO = reader.GetStringByDB(FIELD_RENRAKU_MEMO);
					data.GAZOUSINDAN1 = reader.GetStringByDB(FIELD_GAZOUSINDAN1);
					data.KANA = reader.GetStringByDB(FIELD_KANA);
					data.ROMA = reader.GetStringByDB(FIELD_ROMA);
					data.KANJI = reader.GetStringByDB(FIELD_KANJI);
					data.SEX = reader.GetStringByDB(FIELD_SEX);
					data.BIRTHDAY = reader.GetStringByDB(FIELD_BIRTHDAY);
					#endregion

					return data;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		private void WriteToReport(ToReportInfo t, BaseData data)
		{
			String connectionStringReport = Settings.Default.ConnectionStringReport;
			IDbConnection connectionReport = ODPConnectionUtils.Connect(connectionStringReport);
			try
			{
				ValidateStatusForReport(t.MessageID1, connectionReport);
				using (IDbCommand command = connectionReport.CreateCommand())
				{
                    ODPCommandUtils.InitCommandODP(command, ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
					WriteToReportOrder(t, data, command);       
				}
			}
			finally
			{
				connectionReport.Close();
			}
		}

        /// <summary>
        /// ExamBuiInfoテーブルのレコード削除
        /// ExamInfoテーブルのレコード削除と同じタイミングで、関連するレコードを削除
        /// 2019.05.08 仕様変更により追加機能
        /// </summary>
        /// <param name="toRepInfo"></param>
        /// <param name="command"></param>
        private void Delete_ExamBuiInfo(ReportInterface.Data.ToReportInfo toRepInfo, IDbCommand command)
        {
            const string DELETE_SQL =
            " DELETE exambuiinfo exabi" +
            " WHERE ( exabi.id IN (" +
                    " SELECT exai.id" +
                    " FROM examinfo exai" +
                    " WHERE (exai.odrid = :prm_odrid)" +
                    " AND (exai.patid = :prm_patid)" +
                  " ))";
            command.CommandText = DELETE_SQL;
            command.Parameters.Clear();
            command.SetStringToCommand("prm_odrid", toRepInfo.MessageID1);
            command.SetStringToCommand("prm_patid", toRepInfo.MessageID2);
            MiscUtils.WriteDbCommandLogForLog4net(command, _log);
            _log.DebugFormat("SQL実行します");
            try
            {
                int count = Convert.ToInt32(command.ExecuteNonQuery());
                _log.DebugFormat("DELETE exambuiinfo 対象{0}件 - patid = {1}, ordid = {2}", count, toRepInfo.MessageID2, toRepInfo.MessageID1);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("DELETE exambuiinfo エラー：{0}", ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// ExamBuiInfoテーブルへのレコード登録
        /// ExamInfoテーブルへのレコード登録と同じタイミングで、レコード登録
        /// 2019.05.07 仕様変更により追加機能
        /// </summary>
        /// <param name="order"></param>
        /// <param name="command"></param>
        private void Insert_ExamBuiInfo(ReportInterface.Data.ToReportInfo toRepInfo, DataExamFinish order, IDbCommand command)
        {
            const string INSERT_SQL =
            " INSERT INTO exambuiinfo" +
            " SELECT exai.id id" +
                  ", 1 no" +
                  ", :prm_detaillocus bui_name" +
                  ", null houkou_name" +
                  ", null sayuu_name" +
                  ", null houhou_name" +
                  ", :prm_locus buibunrui_name" +
            " FROM examinfo exai" +
            " WHERE (exai.odrid = :prm_odrid)" +
            // " AND (exai.patid = :prm_patid)" +
            "";
            command.CommandText = INSERT_SQL;
            command.Parameters.Clear();
            command.SetStringToCommand("prm_detaillocus", order.DETAILLOCUS);
            command.SetStringToCommand("prm_locus", order.LOCUS);
            command.SetStringToCommand("prm_odrid", toRepInfo.MessageID1);
            // command.SetStringToCommand("prm_patid", toRepInfo.MessageID2);
            MiscUtils.WriteDbCommandLogForLog4net(command, _log);
            _log.DebugFormat("SQL実行します");
            try
            {
                int count = Convert.ToInt32(command.ExecuteNonQuery());
                _log.DebugFormat("INSERT INTO exambuiinfo 対象{0}件 - odrid = {1}", count, toRepInfo.MessageID1);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("INSERT INTO exambuiinfo エラー：{0}", ex.Message);
                throw ex;
            }
        }

        private void DelIns_ExamBuiInfo(ReportInterface.Data.ToReportInfo toRepInfo, DataExamFinish order, IDbCommand command)
        {
            if (order.KENSATYPE_ID == null)
            {
                // KENSATYPE_ID の指定が NULL なので、 Del&InsしてＯＫ
            }
            else
            {
                string pet_ct = ConfigurationManager.AppSettings["KensaTypeIDPETCT"].StringToString();
                string kensatype_id = order.KENSATYPE_ID;
                if ((pet_ct.Length > 0) && (kensatype_id.Length > 0))
                {
                    // KENSATYPE_ID が config.PETCT に一致するので、 Del&Insしない
                    if (pet_ct == kensatype_id) return;
                }
            }
            Delete_ExamBuiInfo(toRepInfo, command);
            Insert_ExamBuiInfo(toRepInfo, order, command);
        }

        private void WriteToReportOrder(ToReportInfo t, BaseData data, IDbCommand command)      // ここと呼び出し元にDataExamFinishの引数を追加する　//TODO
		{
			#region sql

			// 設定ファイルから値をとってくる処理を記載		//TODO
			// 上でとった値で検査目的を書き込まない処理の判定文を記載
			// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - add - ここから
			DataExamFinish dataExamFinish = (DataExamFinish)data;
			string pet_ct = ConfigurationManager.AppSettings["KensaTypeIDPETCT"].StringToString();
			Boolean petct_purpose = Boolean.Parse(ConfigurationManager.AppSettings["PetCtPurpose"]);
			String purpose = null;
			if (dataExamFinish.KENSATYPE_ID == pet_ct)
            {
				// 検査種別が「PET-CT」の場合、既存(false)か改造後(true)かを選択
				if (!petct_purpose)
                {
					// 既存の場合
					purpose = "e.purpose = CASE CAST(:kensatype_id AS VARCHAR2(10)) " +
									"WHEN '{0}' THEN NVL2(e.purpose, e.purpose || CHR(13) || CHR(10), '') || " +
										"CAST(:purpose AS VARCHAR2(117)) || CHR(13) || CHR(10) " +
									"ELSE e.purpose " +
								"END, ";

				} else
                {
					// 改造後の場合
					purpose = "e.purpose = CASE CAST(:kensatype_id AS VARCHAR2(10)) " +
									"WHEN '{0}' THEN NVL2(e.purpose, e.purpose || CHR(13) || CHR(10), '')  " +
									"ELSE e.purpose " +
								"END, ";
				}
            } else
            {
				// PET-CT以外の場合
				purpose = "e.purpose = CASE CAST(:kensatype_id AS VARCHAR2(10)) " +
								"WHEN '{0}' THEN NVL2(e.purpose, e.purpose || CHR(13) || CHR(10), '') || " +
									"CAST(:purpose AS VARCHAR2(117)) || CHR(13) || CHR(10) " +
								"ELSE e.purpose " +
							"END, ";
			}
			// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - add - ここまで

			// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - del - ここから
			// 定数ではなく変数を使用できるようにするためconstを外す
			//const string UPDATE_SQL =			
			string UPDATE_SQL =
			// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - del - ここまで
				"UPDATE examinfo e " +
		        "SET " +
		            "e.examdate = :examdate, " +
					"e.locus = CASE CAST(:kensatype_id AS VARCHAR2(10)) " +
									"WHEN '{0}' THEN " +
										"e.locus " +
									"ELSE CAST(:locus AS VARCHAR2(128)) " +
								"END, " +
					"e.detaillocus = CASE CAST(:kensatype_id AS VARCHAR2(10)) " +
									"WHEN '{0}' THEN " +
										"e.detaillocus " +
									"ELSE CAST(:detaillocus AS VARCHAR2(64)) " +
								"END, " +

					// 下の処理を修正する　//TODO
					// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - del - ここから
					purpose +
					//"e.purpose = CASE CAST(:kensatype_id AS VARCHAR2(10)) " +
					//				"WHEN '{0}' THEN NVL2(e.purpose, e.purpose || CHR(13) || CHR(10), '') || " +
					//					"CAST(:purpose AS VARCHAR2(117)) || CHR(13) || CHR(10) " +
					//				"ELSE e.purpose " +
					//			"END, " +
					// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - del - ここまで
					"e.remarks = SUBSTRB(NVL2(e.remarks, e.remarks || CHR(13) || CHR(10), '') || " +
								" :bikou, 1, 2000), " +
					"e.status = :status, " +
					"e.medicine = CASE CAST(:kensatype_id AS VARCHAR2(10)) " +
									"WHEN '{0}' THEN " +
										"CAST(:purpose AS VARCHAR2(117)) " +
									"ELSE CAST(:medicine AS VARCHAR2(1000)) " +
								"END, " +
					"e.examinedoctor = :examinedoctor, " +
					"e.gazousindan1 = :gazousindan1, " +
					"e.kana = :kana, " +
					"e.roma = :roma, " +
					"e.kanji = :kanji, " +
					"e.sex = :sex, " +
					"e.birthday = :birthday " +
		        "WHERE " +
		            "e.odrid = :odrid ";
		    #endregion

			#region パラメータ
			const string PARAM_NAME_ODRID = "odrid";
			const string PARAM_NAME_EXAMDATE = "examdate";
			const string PARAM_NAME_LOCUS = "locus";
			const string PARAM_NAME_DETAILLOCUS = "detaillocus";
			const string PARAM_NAME_PURPOSE = "purpose";
			const string PARAM_NAME_STATUS = "status";
			const string PARAM_NAME_MEDICINE = "medicine";
			const string PARAM_NAME_EXAMINEDOCTOR = "examinedoctor";
			const string PARAM_NAME_KENSATYPE_ID = "kensatype_id";
			const string PARAM_NAME_BIKOU = "bikou";
			const string PARAM_NAME_GAZOUSINDAN1 = "gazousindan1";
			const string PARAM_NAME_KANA = "kana";
			const string PARAM_NAME_ROMA = "roma";
			const string PARAM_NAME_KANJI = "kanji";
			const string PARAM_NAME_SEX = "sex";
			const string PARAM_NAME_BIRTHDAY = "birthday";
			#endregion

			command.CommandText = string.Format(UPDATE_SQL, 
				ConfigurationManager.AppSettings["KensaTypeIDPETCT"].StringToString());

			// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - del - ここから
//			DataExamFinish dataExamFinish = (DataExamFinish)data;           //UPDATE文の上に移動
			// ■m_miyazaki 2022.02 - 仕様変更 - 検査種別=PET-CTの場合検査情報を表示しない、検査情報の付加を選択可にする - del - ここまで
			command.Parameters.Clear();
			#region パラメータ設定
			command.SetStringToCommand(PARAM_NAME_ODRID, t.MessageID1);

			command.SetDateTimeToCommand(PARAM_NAME_EXAMDATE, dataExamFinish.EXAMDATE);

            // ■y_higuchi 2019.05 - 仕様変更 - 「,」囲み - del - ここから
            // command.SetStringToCommand(PARAM_NAME_LOCUS, dataExamFinish.LOCUS);
            // command.SetStringToCommand(PARAM_NAME_DETAILLOCUS, dataExamFinish.DETAILLOCUS);
            // ■y_higuchi 2019.05- 仕様変更 - 「,」囲み - del - ここまで - add - ここから
            if (dataExamFinish.LOCUS == null)
            {
                command.SetStringToCommand(PARAM_NAME_LOCUS, dataExamFinish.LOCUS);
            }
            else
            {
                String strComma = dataExamFinish.LOCUS;
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
            if (dataExamFinish.DETAILLOCUS == null)
            {
                command.SetStringToCommand(PARAM_NAME_DETAILLOCUS, dataExamFinish.DETAILLOCUS);
            }
            else
            {
                String strComma2 = dataExamFinish.DETAILLOCUS;
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
            // ■y_higuchi 2019.05 - 仕様変更 - 「,」囲み - add - ここまで

            command.SetStringToCommand(PARAM_NAME_PURPOSE, dataExamFinish.PURPOSE);
			command.SetIntToCommand(PARAM_NAME_STATUS, dataExamFinish.STATUS);
			command.SetStringToCommand(PARAM_NAME_MEDICINE, dataExamFinish.MEDICINE);
			command.SetStringToCommand(PARAM_NAME_EXAMINEDOCTOR, dataExamFinish.EXAMINEDOCTOR);
			command.SetStringToCommand(PARAM_NAME_KENSATYPE_ID, dataExamFinish.KENSATYPE_ID);
			command.SetStringToCommand(PARAM_NAME_BIKOU, GetBikou(dataExamFinish.BIKOU, dataExamFinish.RENRAKU_MEMO));
			command.SetIntToCommand(PARAM_NAME_GAZOUSINDAN1, dataExamFinish.GAZOUSINDAN1);
			command.SetStringToCommand(PARAM_NAME_KANA, dataExamFinish.KANA);
			command.SetStringToCommand(PARAM_NAME_ROMA, dataExamFinish.ROMA);
			command.SetStringToCommand(PARAM_NAME_KANJI, dataExamFinish.KANJI);
			command.SetStringToCommand(PARAM_NAME_SEX, dataExamFinish.SEX);
			command.SetDateTimeToCommand(PARAM_NAME_BIRTHDAY, dataExamFinish.BIRTHDAY);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.DebugFormat("SQL実行します");
			int count = Convert.ToInt32(command.ExecuteNonQuery());
			_log.DebugFormat("対象{0}件", count);

            // ■y_higuchi 2019.05- 仕様変更 - ExamBuiInfoテーブルへ Delete & Insert - add - ここから
            // 同じタイミングで ExamBuiInfo テーブルへ Delete & Insert を実行する
            DelIns_ExamBuiInfo(t, dataExamFinish, command);
            // ■y_higuchi 2019.05- 仕様変更 - ExamBuiInfoテーブルへ Delete & Insert - add - ここから
        }

        private string GetBikou(string bikou, string renraku_memo)
		{
			if (!string.IsNullOrEmpty(bikou) && !string.IsNullOrEmpty(renraku_memo))
			{
				return string.Format("{0}\r\n{1}", bikou, renraku_memo);
			}

			if (!string.IsNullOrEmpty(bikou))
			{
				return bikou;
			}

			return renraku_memo;
		}

	}
}
