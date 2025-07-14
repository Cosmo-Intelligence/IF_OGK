CREATE OR REPLACE PACKAGE STATISTICS_PRINT_PACKAGE AS

	--’è”(ƒNƒ‰ƒCƒAƒ“ƒg‚Æ“¯Šú‚·‚é‚à‚Ì‚ð’è”‚É‚Ü‚Æ‚ß‚é)
	--sÚ×
	GYO_DETAIL CONSTANT VARCHAR2(4) := '–¾×';
	GYO_TOTAL CONSTANT VARCHAR2(4) := '‘Œv';
	GYO_TOTAL_DAY CONSTANT VARCHAR2(10) := '¬Œv(“ú•t)';
	GYO_TOTAL_KENSA_TYPE CONSTANT VARCHAR2(23) := '¬Œv(ŒŸ¸Ží•Ê)';
	GYO_TOTAL_KENSA_TYPE_DAY CONSTANT VARCHAR2(19) := '¬Œv(ŒŸ¸Ží•Ê+“ú•t)';
	GYO_TOTAL_KENSA_TYPE_BUIBUNRUI CONSTANT VARCHAR2(23) := '¬Œv(ŒŸ¸Ží•Ê+•”ˆÊ•ª—Þ)';
	GYO_TOTAL_SOCHI_TYPE CONSTANT VARCHAR2(23) := '¬Œv(ŒŸ¸‘•’u+ŒŸ¸Ží•Ê)';
	GYO_TOTAL_SOCHI_TYPE_DAY CONSTANT VARCHAR2(28) := '¬Œv(ŒŸ¸‘•’u+ŒŸ¸Ží•Ê+“ú•t)';
	GYO_TOTAL_SOCHI_TYPE_BUIBUNRUI CONSTANT VARCHAR2(32) := '¬Œv(ŒŸ¸‘•’u+ŒŸ¸Ží•Ê+•”ˆÊ•ª—Þ)';
	GYO_TOTAL_PLACE_TYPE CONSTANT VARCHAR2(23) := '¬Œv(ŽÀŽ{êŠ+ŒŸ¸Ží•Ê)';
	GYO_TOTAL_PLACE_TYPE_DAY CONSTANT VARCHAR2(28) := '¬Œv(ŽÀŽ{êŠ+ŒŸ¸Ží•Ê+“ú•t)';
	GYO_TOTAL_PLACE_TYPE_BUIBUNRUI  CONSTANT VARCHAR2(32) := '¬Œv(ŽÀŽ{êŠ+ŒŸ¸Ží•Ê+•”ˆÊ•ª—Þ)';

	GYO_TOTAL_FILM CONSTANT VARCHAR2(14) := '¬Œv(ƒtƒBƒ‹ƒ€)';
	GYO_TOTAL_ZOUEIZAI CONSTANT VARCHAR2(12) := '¬Œv(‘¢‰eÜ)';
	GYO_TOTAL_KUBUN CONSTANT VARCHAR2(10) := '¬Œv(‹æ•ª)';
	GYO_TOTAL_BUNRUI CONSTANT VARCHAR2(10) := '¬Œv(•ª—Þ)';
	GYO_TOTAL_BUNRUI_DAY CONSTANT VARCHAR2(15) := '¬Œv(•ª—Þ+“ú•t)';

	--‹æ•ª–¼(’P•[‚Ì“Ë‡‚¹‚ÉŽg—p)
	KBN_NAME_IPPAN CONSTANT VARCHAR2(9) := 'ˆê”ÊŽB‰e';
	KBN_NAME_PORTABLE CONSTANT VARCHAR2(10) := 'ƒ|[ƒ^ƒuƒ‹';
	KBN_NAME_X_DR CONSTANT VARCHAR2(8) := '‚wü“§Ž‹';
	KBN_NAME_CT CONSTANT VARCHAR2(4) := '‚b‚s';
	KBN_NAME_US CONSTANT VARCHAR2(4) := '‚t‚r';
	KBN_NAME_UCT CONSTANT VARCHAR2(6) := '‚t‚b‚s';
	KBN_NAME_KYOFUKUBU CONSTANT VARCHAR2(10) := '‹¹• •”ŽB‰e';
	KBN_NAME_KOTSU CONSTANT VARCHAR2(8) := 'œŽB‰e‘¼';
	KBN_NAME_DANSO CONSTANT VARCHAR2(8) := '’f‘wŽB‰e';
	KBN_NAME_NYUBO CONSTANT VARCHAR2(8) := '“û–[ŽB‰e';
	KBN_NAME_KENSHIN CONSTANT VARCHAR2(8) := 'Œ’f‹Æ–±';
	KBN_NAME_LINAC CONSTANT VARCHAR2(11) := '×²Å¯¸¸Þ×Ì¨°';
	KBN_NAME_SIMU CONSTANT VARCHAR2(8) := '¼­ÐÚ°¼®Ý';
	KBN_NAME_KOTSUEN CONSTANT VARCHAR2(8) := 'œ‰–’è—Ê';
	KBN_NAME_DR CONSTANT VARCHAR2(8) := '“§Ž‹‘¢‰e';
	KBN_NAME_KANJASU CONSTANT VARCHAR2(14) := 'Š³ŽÒ”(‹~‹}ŠÜ)';
	KBN_NAME_BUNRUI_SONOTA CONSTANT VARCHAR2(6) := '‚»‚Ì‘¼';
	KBN_NAME_BUNRUI_TOKUSHU CONSTANT VARCHAR2(4) := '“ÁŽê';

	--CT
	KBN_NAME_BUNRUI_IPPAN CONSTANT VARCHAR2(4) := 'ˆê”Ê';
	KBN_NAME_TOKEIBU_TANJUN CONSTANT VARCHAR2(10) := '“ªèò•”’Pƒ';
	KBN_NAME_TOKEIBU_ZOEI CONSTANT VARCHAR2(10) := '“ªèò•”‘¢‰e';
	KBN_NAME_KUKANBU_TANJUN CONSTANT VARCHAR2(10) := '‹ëŠ²•”’Pƒ';
	KBN_NAME_KUKANBU_ZOEI CONSTANT VARCHAR2(10) := '‹ëŠ²•”‘¢‰e';
	KBN_NAME_SHISHI_TANJUN CONSTANT VARCHAR2(8) := 'ŽlŽˆ’Pƒ';
	KBN_NAME_SHISHI_ZOEI CONSTANT VARCHAR2(8) := 'ŽlŽˆ‘¢‰e';
	KBN_NAME_RTP CONSTANT VARCHAR2(3) := 'RTP';
	KBN_NAME_SEIKEN CONSTANT VARCHAR2(4) := '¶ŒŸ';
	KBN_NAME_IVR_CT_FUKUBU CONSTANT VARCHAR2(12) := 'IVR-CT(• •”)';
	KBN_NAME_IVR_CT_KOTSUBAN CONSTANT VARCHAR2(12) := 'IVR-CT(œ”Õ)';
	--RI
	KBN_NAME_SPECT CONSTANT VARCHAR2(5) := 'SPECT';
	KBN_NAME_BUBUN_DOU CONSTANT VARCHAR2(10) := '•”•ª(“®‘Ô)';
	KBN_NAME_BUBUN_SEI CONSTANT VARCHAR2(10) := '•”•ª(Ã‘Ô)';
	KBN_NAME_ZENSHIN CONSTANT VARCHAR2(4) := '‘Sg';
	KBN_NAME_SONOTA CONSTANT VARCHAR2(6) := '‚»‚Ì‘¼';
	--DR
	KBN_NAME_I_TOSHI CONSTANT VARCHAR2(6) := 'ˆÝ“§Ž‹';
	KBN_NAME_CHOKUCHO CONSTANT VARCHAR2(4) := '’’°';
	KBN_NAME_SHOKUDO_TOSHI CONSTANT VARCHAR2(8) := 'H“¹“§Ž‹';
	KBN_NAME_SHOCHO_TOSHI CONSTANT VARCHAR2(8) := '¬’°“§Ž‹';
	KBN_NAME_DIC CONSTANT VARCHAR2(3) := 'DIC';
	KBN_NAME_ERCP CONSTANT VARCHAR2(4) := 'ERCP';
	KBN_NAME_PTCD CONSTANT VARCHAR2(20) := 'PTCDE”^á‡ƒhƒŒƒi[ƒW';
	KBN_NAME_TUBE_ZOEI CONSTANT VARCHAR2(18) := 'ƒ`ƒ…[ƒu‘¢‰eEŒðŠ·';
	KBN_NAME_IVH CONSTANT VARCHAR2(13) := 'IVH‘}“üEŒðŠ·';
	KBN_NAME_FUJINKA CONSTANT VARCHAR2(6) := '•wl‰È';
	KBN_NAME_ESWL CONSTANT VARCHAR2(4) := 'ESWL';
	KBN_NAME_SONOTA_ZOEI CONSTANT VARCHAR2(12) := '‚»‚Ì‘¼‚Ì‘¢‰e';
	KBN_NAME_NAISHIKYO CONSTANT VARCHAR2(12) := '“àŽ‹‹¾“IŽ¡—Ã';
	--US
	--•ª—Þ–¼
	BUNRUI_NAME_SHINZO CONSTANT VARCHAR2(16) := '‡@S‘Ÿ—Ìˆæ‘Œ”';
	BUNRUI_NAME_FUKUBU CONSTANT VARCHAR2(18) := '‡A• •”“™—Ìˆæ‘Œ”';
	BUNRUI_NAME_TAIHYO CONSTANT VARCHAR2(18) := '‡B‘Ì•\“™—Ìˆæ‘Œ”';
	BUNRUI_NAME_KENKO_KANRI CONSTANT VARCHAR2(16) := 'Œ’NŠÇ—ƒZƒ“ƒ^[';
	BUNRUI_NAME_BYOSHINRENKE CONSTANT VARCHAR2(8) := '•af˜AŒg';
	--‹æ•ª–¼
	KBN_NAME_SEIJIN CONSTANT VARCHAR2(4) := '¬l';
	KBN_NAME_SHONI CONSTANT VARCHAR2(4) := '¬Ž™';
	KBN_NAME_ZOEI CONSTANT VARCHAR2(4) := '‘¢‰e';
	KBN_NAME_KEISHOKUDO CONSTANT VARCHAR2(6) := 'ŒoH“¹';
	KBN_NAME_FUKUBU CONSTANT VARCHAR2(4) := '• •”';
	KBN_NAME_HINYOKIKA CONSTANT VARCHAR2(8) := '”å”AŠíŒn';
	KBN_NAME_SANKA CONSTANT VARCHAR2(4) := 'ŽY‰È';
	KBN_NAME_KEIDOMYAKU_ZOEI CONSTANT VARCHAR2(10) := 'Œo“®–¬‘¢‰e';
	KBN_NAME_KEIJOMYAKU_ZOEI CONSTANT VARCHAR2(10) := 'ŒoÃ–¬‘¢‰e';
	KBN_NAME_PFA_PEIT CONSTANT VARCHAR2(16) := '‚o‚e‚`^‚o‚d‚h‚s';
	KBN_NAME_SEIKEN_SENSHI CONSTANT VARCHAR2(10) := '¶ŒŸEúŽh';
	KBN_NAME_NYUSEN CONSTANT VARCHAR2(4) := '“û‘B';
	KBN_NAME_KOJOSEN CONSTANT VARCHAR2(6) := 'bó‘B';
	KBN_NAME_KEKKANKEI CONSTANT VARCHAR2(6) := 'ŒŒŠÇŒn';
	KBN_NAME_BUNRUI_DOPPLER CONSTANT VARCHAR2(6) := 'ƒhƒvƒ‰';
	KBN_NAME_LEVOVIST CONSTANT VARCHAR2(10) := 'ƒŒƒ{ƒrƒXƒg';
	KBN_NAME_SONAZOID CONSTANT VARCHAR2(10) := 'ƒ\ƒiƒ]ƒCƒh';
	KBN_NAME_SEISHOKU_1K CONSTANT VARCHAR2(20) := '¶H‚P‚O‚O‚‚Œ@‚P‚j';
	KBN_NAME_SEISHOKU_100 CONSTANT VARCHAR2(22) := '¶—H‰–…@‚P‚O‚O‚‚Œ';
	KBN_NAME_SEISHOKU_20 CONSTANT VARCHAR2(22) := '¶—H‰–…@@‚Q‚O‚‚Œ';
	--ŒŽ•ñ
	--•ª—ÞID(statistics_monthly_dat.class_key‚Æ“¯Šú)
	GEPPO_BUNRUI_ID_IPPAN CONSTANT VARCHAR2(3) := '100';
	GEPPO_BUNRUI_ID_KOTSUEN CONSTANT VARCHAR2(3) := '120';
	GEPPO_BUNRUI_ID_DR CONSTANT VARCHAR2(3) := '200';
	GEPPO_BUNRUI_ID_KESSEKI CONSTANT VARCHAR2(3) := '201';
	GEPPO_BUNRUI_ID_US CONSTANT VARCHAR2(3) := '400';
	GEPPO_BUNRUI_ID_CT CONSTANT VARCHAR2(3) := '500';
	GEPPO_BUNRUI_ID_MRI CONSTANT VARCHAR2(3) := '600';
	GEPPO_BUNRUI_ID_RI_TAIGAI CONSTANT VARCHAR2(3) := '700';
	GEPPO_BUNRUI_ID_RI_NAIYO CONSTANT VARCHAR2(3) := '702';
	GEPPO_BUNRUI_ID_KEKKAN CONSTANT VARCHAR2(3) := '800';
	GEPPO_BUNRUI_ID_PETCT CONSTANT VARCHAR2(3) := '901';
	--•ª—Þ–¼
	--‹æ•ª–¼
	GEPPO_KBN_NAME_IPPAN CONSTANT VARCHAR2(4) := 'ˆê”Ê';
	GEPPO_KBN_NAME_PORTABLE CONSTANT VARCHAR2(10) := 'ƒ|[ƒ^ƒuƒ‹';
	GEPPO_KBN_NAME_TOBU CONSTANT VARCHAR2(4) := '“ª•”';
	GEPPO_KBN_NAME_FUKUBU CONSTANT VARCHAR2(4) := '• •”';
	GEPPO_KBN_NAME_KYOFUKUBU CONSTANT VARCHAR2(6) := '‹¹• •”';
	GEPPO_KBN_NAME_JOFUKUBU CONSTANT VARCHAR2(6) := 'ã• •”';
	GEPPO_KBN_NAME_SHINZO CONSTANT VARCHAR2(4) := 'S‘Ÿ';
	GEPPO_KBN_NAME_TANDO CONSTANT VARCHAR2(4) := '’_“¹';
	GEPPO_KBN_NAME_CHOKUCHO CONSTANT VARCHAR2(4) := '’’°';
	GEPPO_KBN_NAME_NYORO CONSTANT VARCHAR2(4) := '”A˜H';
	GEPPO_KBN_NAME_SONOTA CONSTANT VARCHAR2(6) := '‚»‚Ì‘¼';
	GEPPO_KBN_NAME_RI_TAIGAI CONSTANT VARCHAR2(10) := 'RI‘ÌŠO‘ª’è';
	GEPPO_KBN_NAME_RI_NAIYO CONSTANT VARCHAR2(10) := 'RI“à—p—Ã–@';
	GEPPO_KBN_NAME_PETCT CONSTANT VARCHAR2(6) := 'PET-CT';
	GEPPO_KBN_NAME_KOTSUEN CONSTANT VARCHAR2(8) := 'œ‰–’è—Ê';
	--Film“ÇžE‘o/CD“ÇžE‘o
	CD_FILM_KBN_NAME_FILM_READ CONSTANT VARCHAR2(8) := 'FilmŽæž';
	CD_FILM_KBN_NAME_FILM_WRITE CONSTANT VARCHAR2(8) := 'Filmo—Í';
	CD_FILM_KBN_NAME_CD_READ CONSTANT VARCHAR2(6) := 'CDŽæž';
	CD_FILM_KBN_NAME_CD_WRITE CONSTANT VARCHAR2(6) := 'CDo—Í';


	--ƒJ[ƒ\ƒ‹’è‹`
	--ŽB‰eŒŸ¸”ˆê——i•”ˆÊ•ª—Þj
	CURSOR cBuiBunrui(P_EXAMENDDATE DATE, P_PLACE_ID VARCHAR2) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_KENSA_TYPE_DAY
			WHEN 2 THEN GYO_TOTAL_KENSA_TYPE_BUIBUNRUI
			WHEN 3 THEN GYO_TOTAL_KENSA_TYPE
			WHEN 5 THEN GYO_TOTAL_DAY
			WHEN 7 THEN GYO_TOTAL
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.kensatype_id,
		t.kensatype_name,
		t.kensatype_showorder,
		t.examenddate_yyyymmdd10,
		t.buibunrui_id,
		t.buibunrui_name,
		t.bui_showorder,
		t.cnt
	FROM
		(
			SELECT
				g.gid,
				g.kensatype_id,
				k.kensatype_name,
				k.showorder kensatype_showorder,
				g.examenddate_yyyymmdd10,
				g.buibunrui_id,
				bb_name.buibunrui_name,
				bb_name.showorder bui_showorder,
				g.cnt
			FROM
				(

					SELECT
						GROUPING_ID
						(
							e.kensatype_id, 
							TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
							bb.buibunrui_id
						) gid,
						e.kensatype_id,
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						bb.buibunrui_id,
						COUNT(*) cnt
					FROM
						exmaintable e
						INNER JOIN place_exam_room_master pe
						ON
							pe.exam_room_id = e.kensasitu_id AND
							pe.place_id = P_PLACE_ID
						INNER JOIN exbuitable eb
						ON
							eb.ris_id = e.ris_id AND
							eb.satueistatus = '1'
						INNER JOIN buimaster b
						ON
							b.bui_id = eb.bui_id
						INNER JOIN buibunruimaster bb
						ON
							bb.buibunrui_id = b.buibunrui_id
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90
					GROUP BY
						GROUPING SETS --¬Œvì¬s
						(
							(
								e.kensatype_id,
								TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
								bb.buibunrui_id
							),
							(
								e.kensatype_id, 
								bb.buibunrui_id
							),
							(
								e.kensatype_id, 
								TO_CHAR(e.examenddate, 'YYYY/MM/DD')
							),
							(
								TO_CHAR(e.examenddate, 'YYYY/MM/DD')
							),
							(
								e.kensatype_id
							),
							()
						)

				) g
				LEFT OUTER JOIN buibunruimaster bb_name
				ON
					bb_name.buibunrui_id = g.buibunrui_id
				LEFT OUTER JOIN kensatypemaster k
				ON
					k.kensatype_id = g.kensatype_id
		) t
	ORDER BY
		t.kensatype_showorder,
		t.kensatype_id,
		t.bui_showorder,
		t.buibunrui_id,
		t.gid,
		t.examenddate_yyyymmdd10;

	--ŽB‰eŒŸ¸”ˆê——iŽåsˆ×j
	--ŽÀŽ{êŠ‘I‘ðŽž
	--C³Žž‚ÍcBuiAll‚àŒ©’¼‚·‚±‚Æ
	CURSOR cBui(P_EXAMENDDATE DATE, P_PLACE_ID VARCHAR2) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_SOCHI_TYPE_DAY
			WHEN 2 THEN GYO_TOTAL_SOCHI_TYPE_BUIBUNRUI
			WHEN 3 THEN GYO_TOTAL_SOCHI_TYPE
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.kensasitu_id,
		t.kensasitu_name,
		t.kensasitu_showorder,
		t.kensatype_id,
		t.kensatype_name,
		t.kensatype_showorder,
		t.bui_id,
		t.bui_name,
		t.bui_showorder,
		t.examenddate_yyyymmdd10,
		t.cnt
	FROM
		(
			SELECT
				g.gid,
				g.kensasitu_id,
				er.examroom_name kensasitu_name,
				er.showorder kensasitu_showorder,
				g.kensatype_id,
				k.kensatype_name,
				k.showorder kensatype_showorder,
				g.examenddate_yyyymmdd10,
				g.bui_id,
				b_name.bui_name,
				b_name.showorder bui_showorder,
				g.cnt
			FROM
				(
					SELECT
						GROUPING_ID
						(
							e.kensasitu_id,
							e.kensatype_id, 
							TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
							b.bui_id
						) gid,
						e.kensasitu_id,
						e.kensatype_id,
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						b.bui_id,
						COUNT(*) cnt
					FROM
						exmaintable e
						INNER JOIN place_exam_room_master pe
						ON
							pe.exam_room_id = e.kensasitu_id AND
							pe.place_id = P_PLACE_ID
						INNER JOIN exbuitable eb
						ON
							eb.ris_id = e.ris_id AND
							eb.satueistatus = '1'
						INNER JOIN buimaster b
						ON
							b.bui_id = eb.bui_id
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90
					GROUP BY
						GROUPING SETS --¬Œvì¬s
						(
							(
								e.kensasitu_id,
								e.kensatype_id,
								TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
								b.bui_id
							),
							(
								e.kensasitu_id,
								e.kensatype_id, 
								b.bui_id
							),
							(
								e.kensasitu_id,
								e.kensatype_id, 
								TO_CHAR(e.examenddate, 'YYYY/MM/DD')
							),
							(
								e.kensasitu_id,
								e.kensatype_id
							)
						)
				) g
				LEFT OUTER JOIN buimaster b_name
				ON
					b_name.bui_id = g.bui_id
				LEFT OUTER JOIN kensatypemaster k
				ON
					k.kensatype_id = g.kensatype_id
				LEFT OUTER JOIN examroommaster er
				ON
					er.examroom_id = g.kensasitu_id
		) t
	ORDER BY
		t.kensasitu_showorder,
		t.kensasitu_id,
		t.kensatype_id,
		t.kensatype_showorder,
		t.kensatype_id,
		t.bui_showorder,
		t.bui_id,
		t.gid,
		t.examenddate_yyyymmdd10;

	--ŽB‰eŒŸ¸”ˆê——iŽåsˆ×j
	--ALL‘I‘ðŽž
	--cBui‚Æ‚ÍŽ—‚Ä‚é‚ªGROUP BY‚ªˆá‚¤‚Ì‚Å’ˆÓ
	CURSOR cBuiAll(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_PLACE_TYPE_DAY
			WHEN 2 THEN GYO_TOTAL_PLACE_TYPE_BUIBUNRUI
			WHEN 3 THEN GYO_TOTAL_PLACE_TYPE
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.place_id,
		t.place_name,
		t.place_showorder,
		t.kensatype_id,
		t.kensatype_name,
		t.kensatype_showorder,
		t.bui_id,
		t.bui_name,
		t.bui_showorder,
		t.examenddate_yyyymmdd10,
		t.cnt
	FROM
		(
			SELECT
				g.gid,
				g.place_id,
				p.place_name place_name,
				p.showorder place_showorder,
				g.kensatype_id,
				k.kensatype_name,
				k.showorder kensatype_showorder,
				g.examenddate_yyyymmdd10,
				g.bui_id,
				b_name.bui_name,
				b_name.showorder bui_showorder,
				g.cnt
			FROM
				(
					SELECT
						GROUPING_ID
						(
							pe.place_id,
							e.kensatype_id, 
							TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
							b.bui_id
						) gid,
						pe.place_id,
						e.kensatype_id,
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						b.bui_id,
						COUNT(*) cnt
					FROM
						exmaintable e
						INNER JOIN place_exam_room_master pe
						ON
							pe.exam_room_id = e.kensasitu_id
						INNER JOIN exbuitable eb
						ON
							eb.ris_id = e.ris_id AND
							eb.satueistatus = '1'
						INNER JOIN buimaster b
						ON
							b.bui_id = eb.bui_id
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90
					GROUP BY
						GROUPING SETS --¬Œvì¬s
						(
							(
								pe.place_id,
								e.kensatype_id,
								TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
								b.bui_id
							),
							(
								pe.place_id,
								e.kensatype_id, 
								b.bui_id
							),
							(
								pe.place_id,
								e.kensatype_id, 
								TO_CHAR(e.examenddate, 'YYYY/MM/DD')
							),
							(
								pe.place_id,
								e.kensatype_id
							)
						)
				) g
				LEFT OUTER JOIN buimaster b_name
				ON
					b_name.bui_id = g.bui_id
				LEFT OUTER JOIN kensatypemaster k
				ON
					k.kensatype_id = g.kensatype_id
				LEFT OUTER JOIN place_master p
				ON
					p.place_id = g.place_id
		) t
	ORDER BY
		t.place_showorder,
		t.place_id,
		t.kensatype_id,
		t.kensatype_showorder,
		t.kensatype_id,
		t.bui_showorder,
		t.bui_id,
		t.gid,
		t.examenddate_yyyymmdd10;

	--ƒtƒBƒ‹ƒ€ˆê——
	CURSOR cFilm(P_EXAMENDDATE DATE, P_PLACE_ID VARCHAR2) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_DAY
			WHEN 2 THEN GYO_TOTAL_FILM
			WHEN 3 THEN GYO_TOTAL
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.film_id,
		t.film_name,
		t.film_showorder,
		t.cnt
	FROM
		(
			SELECT
				g.gid,
				g.examenddate_yyyymmdd10,
				g.film_id,
				f_name.film_name,
				f_name.showorder film_showorder,
				g.cnt
			FROM
				(
					SELECT
						GROUPING_ID
						(
							TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
							f.film_id
						) gid,
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						f.film_id,
						SUM(ef.used) cnt
					FROM
						exmaintable e
						INNER JOIN place_exam_room_master pe
						ON
							pe.exam_room_id = e.kensasitu_id AND
							pe.place_id = P_PLACE_ID
						INNER JOIN exfilmtable ef
						ON
							ef.ris_id = e.ris_id
						INNER JOIN filmmaster f
						ON
							f.film_id = ef.film_id
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90
					GROUP BY
						GROUPING SETS --¬Œvì¬s
						(
							(
								TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
								f.film_id
							),
							(
								f.film_id
							),
							(
								TO_CHAR(e.examenddate, 'YYYY/MM/DD')
							),
							()
						)
				) g
				LEFT OUTER JOIN filmmaster f_name
				ON
					f_name.film_id = g.film_id
		) t
	ORDER BY
		t.film_showorder,
		t.film_id,
		t.gid,
		t.examenddate_yyyymmdd10;

	--‘¢‰eÜE–òÜˆê——
	CURSOR cZoueizai(P_EXAMENDDATE DATE, P_PLACE_ID VARCHAR2) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 2 THEN GYO_TOTAL_ZOUEIZAI
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.zoueizai_id,
		t.zoueizai_name,
		t.zoueizai_showorder,
		t.zoueizaitanni_name,
		t.cnt
	FROM
		(
			SELECT
				g.gid,
				g.examenddate_yyyymmdd10,
				g.zoueizai_id,
				p_name.zoueizai_name,
				p_name.showorder zoueizai_showorder,
				pu.zoueizaitanni_name,
				g.cnt
			FROM
				(
					SELECT
						GROUPING_ID
						(
							TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
							p.zoueizai_id
						) gid,
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						p.zoueizai_id,
						SUM(ez.suuryou_iji) cnt
					FROM
						exmaintable e
						INNER JOIN place_exam_room_master pe
						ON
							pe.exam_room_id = e.kensasitu_id AND
							pe.place_id = P_PLACE_ID
						INNER JOIN exzoueizaitable ez
						ON
							ez.ris_id = e.ris_id
						INNER JOIN partsmaster p
						ON
							p.zoueizai_id = ez.parts_id AND
							p.ZOUEIZAIKBN in ('JC','JZ')
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90
					GROUP BY
						GROUPING SETS --¬Œvì¬s
						(
							(
								TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
								p.zoueizai_id
							),
							(
								p.zoueizai_id
							)
						)
				) g
				LEFT OUTER JOIN partsmaster p_name
				ON
					p_name.zoueizai_id = g.zoueizai_id AND
					p_name.ZOUEIZAIKBN in ('JC','JZ')
				LEFT OUTER JOIN partsunitmaster pu
				ON
					pu.zoueizaitanni_id = p_name.zoueizaiijitanni_id
		) t
	ORDER BY
		t.zoueizai_showorder,
		t.zoueizai_id,
		t.gid,
		t.examenddate_yyyymmdd10;

	--‹~–½‹~‹}ƒZƒ“ƒ^[@WŒv•\
	CURSOR cEmergency(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_DAY
			WHEN 2 THEN GYO_TOTAL_KUBUN
			WHEN 3 THEN GYO_TOTAL
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.examenddate_yyyymmdd10, 
					g.kbn_name
				) gid,
				g.examenddate_yyyymmdd10,
				g.kbn_name,
				CASE g.kbn_name
					WHEN KBN_NAME_IPPAN THEN 0
					WHEN KBN_NAME_PORTABLE THEN 1
					WHEN KBN_NAME_X_DR THEN 2
					WHEN KBN_NAME_CT THEN 3
					WHEN KBN_NAME_US THEN 4
					WHEN KBN_NAME_UCT THEN 5
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
			(
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE
						WHEN b.statistic_satsuei_id1 IN ('1001', '1002', '1003', '1004', '1005', '1051', '1052') AND
							e.kensatype_id = '100' THEN KBN_NAME_IPPAN
						WHEN b.statistic_satsuei_id1 = '1006' AND
							e.kensatype_id = '100' THEN KBN_NAME_PORTABLE
						WHEN b.statistic_satsuei_id1 IN ('2001', '2002', '2003', '2004', '2005', '2006', '2007', '2008', '2009', '2010', '2011', '2012', '2013') AND
							e.kensatype_id = '200' THEN KBN_NAME_X_DR
						WHEN b.statistic_satsuei_id2 IN ('5101', '5102', '5103', '5104', '5105', '5106', '5107') AND
							e.kensatype_id = '500' THEN KBN_NAME_CT
						WHEN b.statistic_satsuei_id1 IN ('4201', '4202', '4203', '4204', '4205', '4206', '4207', '4208', '4301', '4302', '4303', '4304') AND
							e.kensatype_id = '400' THEN KBN_NAME_US
						WHEN b.statistic_satsuei_id1 IN ('4101', '4102', '4103', '4104', '4105') AND
							e.kensatype_id = '400' THEN KBN_NAME_UCT
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
					INNER JOIN exbuitable eb
					ON
						eb.ris_id = e.ris_id AND
						eb.satueistatus = '1'
					INNER JOIN buimaster b
					ON
						b.bui_id = eb.bui_id
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND
					e.kensasitu_id IN ('CTM', 'CTN', 'ER0','ER1','ER2','ER3','ER4') --‹~‹}‚É‚ ‚½‚é‚à‚Ì
			) g
			WHERE
				g.kbn_name IS NOT NULL
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.examenddate_yyyymmdd10, 
						g.kbn_name
					) ,
					(
						g.examenddate_yyyymmdd10
					),
					(
						g.kbn_name
					),
					()
				)
		) t
	ORDER BY
		t.kbn_showorder,
		t.kbn_name,
		t.gid,
		t.examenddate_yyyymmdd10;

	--ˆê”ÊŽB‰e@•ª—Þ•ÊWŒv•\
	CURSOR cIppan(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 2 THEN GYO_TOTAL_KUBUN
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.examenddate_yyyymmdd10, 
					g.kbn_name
				) gid,
				g.examenddate_yyyymmdd10,
				g.kbn_name,
				CASE g.kbn_name
					WHEN KBN_NAME_KYOFUKUBU THEN 0
					WHEN KBN_NAME_KOTSU THEN 1
					WHEN KBN_NAME_DANSO THEN 2
					WHEN KBN_NAME_NYUBO THEN 3
					WHEN KBN_NAME_KENSHIN THEN 4
					WHEN KBN_NAME_PORTABLE THEN 5
					WHEN KBN_NAME_LINAC THEN 6
					WHEN KBN_NAME_SIMU THEN 7
					WHEN KBN_NAME_KOTSUEN THEN 8
					WHEN KBN_NAME_DR THEN 9
					WHEN KBN_NAME_KANJASU THEN 10
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
			(
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE b.ss_id
						WHEN '1001' THEN KBN_NAME_KYOFUKUBU
						WHEN '1002' THEN KBN_NAME_KOTSU
						WHEN '1003' THEN KBN_NAME_DANSO
						WHEN '1004' THEN KBN_NAME_NYUBO
						WHEN '1005' THEN KBN_NAME_KENSHIN
						WHEN '1006' THEN KBN_NAME_PORTABLE
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
					INNER JOIN exbuitable eb
					ON
						eb.ris_id = e.ris_id AND
						eb.satueistatus = '1'
					INNER JOIN 
					(
						SELECT bui_id, statistic_satsuei_id1 ss_id FROM buimaster UNION ALL
						SELECT bui_id, statistic_satsuei_id2       FROM buimaster 
					) b
					ON
						b.bui_id = eb.bui_id AND
						b.ss_id IN ('1001', '1002', '1003', '1004', '1005', '1006') --‹¹• •”ŽB‰e-ƒ|[ƒ^ƒuƒ‹‚É‚Â‚¢‚Ä
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND 
					e.kensatype_id = '100' AND 
					e.kensasitu_id IN ('PO', 'IP', 'IP5', 'IP8', 'IP9', 'I39') --‹~‹}ˆÈŠO
				UNION ALL
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE b.ss_id
						WHEN '1051' THEN KBN_NAME_LINAC
						WHEN '1052' THEN KBN_NAME_SIMU
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
					INNER JOIN exbuitable eb
					ON
						eb.ris_id = e.ris_id AND
						eb.satueistatus = '1'
					INNER JOIN 
					(
						SELECT bui_id, statistic_satsuei_id1 ss_id FROM buimaster UNION ALL
						SELECT bui_id, statistic_satsuei_id2       FROM buimaster 
					) b
					ON
						b.bui_id = eb.bui_id AND
						b.ss_id IN ('1051', '1052') --×²Å¯¸¸Þ×Ì¨°A¼­ÐÚ°¼®Ý
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND 
					e.kensasitu_id IN ('IPL', 'IPS') --•úŽËüŽ¡—Ã‚É‚ ‚½‚é‚à‚Ì
				UNION ALL
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE
						WHEN e.kensatype_id = '120' AND e.kensasitu_id IN ('060','BM') THEN KBN_NAME_KOTSUEN
						WHEN e.kensatype_id = '200' AND e.kensasitu_id = '044' THEN KBN_NAME_DR
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND 
					e.kensatype_id IN ('120', '200') AND
					e.kensasitu_id IN ('060','BM','044')

				UNION ALL
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE
						WHEN e.kensatype_id IN('100','120','200') AND e.kensasitu_id IN ('PO', 'IP', 'IP5', 'IP8', 'IP9', 'I39','060','BM','044') THEN KBN_NAME_KANJASU --•úŽËüŽ¡—Ã‚É‚ ‚½‚ç‚È‚¢‚à‚Ì
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND 
					e.kensatype_id IN ('100', '120', '200') AND
					e.kensasitu_id IN ('PO', 'IP', 'IP5', 'IP8', 'IP9', 'I39','060','BM','044')
			) g
			WHERE
				g.kbn_name IS NOT NULL
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.examenddate_yyyymmdd10, 
						g.kbn_name
					) ,
					(
						g.kbn_name
					)
				)
		) t
	ORDER BY
		t.kbn_showorder,
		t.kbn_name,
		t.gid,
		t.examenddate_yyyymmdd10;

	--‚b‚s@•ª—Þ•ÊWŒv•\
	CURSOR cCT(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 4 THEN GYO_TOTAL_KUBUN
			ELSE ''
		END gyo_syosai,
		t.gid,
		p.showorder place_showorder,
		t.place_id,
		p.place_name place_name,
		t.examenddate_yyyymmdd10,
		t.bunrui_name,
		t.bunrui_showorder,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.place_id,
					g.examenddate_yyyymmdd10,
					g.bunrui_name,
					g.kbn_name
				) gid,
				g.place_id,
				g.examenddate_yyyymmdd10,
				g.bunrui_name,
				CASE g.bunrui_name
					WHEN KBN_NAME_BUNRUI_IPPAN THEN 0
					WHEN KBN_NAME_BUNRUI_TOKUSHU THEN 1
					WHEN KBN_NAME_BUNRUI_SONOTA THEN 2
					ELSE 999
				END bunrui_showorder,
				g.kbn_name,
				CASE g.kbn_name
					WHEN KBN_NAME_TOKEIBU_TANJUN THEN 0
					WHEN KBN_NAME_TOKEIBU_ZOEI THEN 1
					WHEN KBN_NAME_KUKANBU_TANJUN THEN 2
					WHEN KBN_NAME_KUKANBU_ZOEI THEN 3
					WHEN KBN_NAME_SHISHI_TANJUN THEN 4
					WHEN KBN_NAME_SHISHI_ZOEI THEN 5
					WHEN KBN_NAME_RTP THEN 6
					WHEN KBN_NAME_SEIKEN THEN 7
					WHEN KBN_NAME_IVR_CT_FUKUBU THEN 8
					WHEN KBN_NAME_IVR_CT_KOTSUBAN THEN 9
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
				(
					SELECT
						pe.place_id,
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						CASE b.statistic_satsuei_id1
							WHEN '5000' THEN KBN_NAME_BUNRUI_IPPAN
							WHEN '5001' THEN KBN_NAME_BUNRUI_TOKUSHU
							WHEN '5002' THEN KBN_NAME_BUNRUI_SONOTA
							ELSE ''
						END bunrui_name,
						CASE b.statistic_satsuei_id2
							WHEN '5101' THEN KBN_NAME_TOKEIBU_TANJUN
							WHEN '5102' THEN KBN_NAME_TOKEIBU_ZOEI
							WHEN '5103' THEN KBN_NAME_KUKANBU_TANJUN
							WHEN '5104' THEN KBN_NAME_KUKANBU_ZOEI
							WHEN '5105' THEN KBN_NAME_SHISHI_TANJUN
							WHEN '5106' THEN KBN_NAME_SHISHI_ZOEI
							WHEN '5107' THEN KBN_NAME_RTP
							WHEN '5108' THEN KBN_NAME_SEIKEN
							WHEN '5109' THEN KBN_NAME_IVR_CT_FUKUBU
							WHEN '5110' THEN KBN_NAME_IVR_CT_KOTSUBAN
							ELSE ''
						END kbn_name
					FROM
						exmaintable e
						INNER JOIN place_exam_room_master pe
						ON
							pe.exam_room_id = e.kensasitu_id AND
							pe.place_id IN ('22000', '50000', '73000') --ŠO—ˆ‚b‚sA’†‰›•úŽËüA‹~–½‹~‹}
						INNER JOIN exbuitable eb
						ON
							eb.ris_id = e.ris_id AND
							eb.satueistatus = '1'
						INNER JOIN buimaster b
						ON
							b.bui_id = eb.bui_id AND
							(
								( --ˆê”Ê
									b.statistic_satsuei_id1 = '5000' AND
									b.statistic_satsuei_id2 IN ('5101', '5102', '5103', '5104', '5105', '5106')
								) OR
								( --“ÁŽê
									b.statistic_satsuei_id1 = '5001' AND
									b.statistic_satsuei_id2 IN ('5101', '5102', '5103', '5104', '5105', '5106')
								) OR
								( --‚»‚Ì‘¼
									b.statistic_satsuei_id1 = '5002' AND
									b.statistic_satsuei_id2 IN ('5107', '5108', '5109', '5110')
								)
							)
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90 AND 
						e.kensasitu_id IN ('001', '002', '003', '004', '023', 'CT', 'CTM', 'CTN', 'ER2') AND 
						e.kensatype_id = '500'
				) g
			WHERE
				g.bunrui_name IS NOT NULL AND
				g.kbn_name IS NOT NULL
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.place_id,
						g.examenddate_yyyymmdd10, 
						g.bunrui_name,
						g.kbn_name
					) ,
					(
						g.place_id,
						g.bunrui_name,
						g.kbn_name
					)
				)
		) t
		LEFT OUTER JOIN place_master p
		ON
			p.place_id = t.place_id
	ORDER BY
		p.showorder,
		p.place_id,
		t.bunrui_showorder,
		t.bunrui_name,
		t.kbn_showorder,
		t.kbn_name,
		t.gid,
		t.examenddate_yyyymmdd10;

	--‚q‚h@•ª—Þ•ÊWŒv•\
	CURSOR cRI(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 2 THEN GYO_TOTAL_KUBUN
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.examenddate_yyyymmdd10, 
					g.kbn_name
				) gid,
				g.examenddate_yyyymmdd10,
				g.kbn_name,
				CASE g.kbn_name
					WHEN KBN_NAME_SPECT THEN 0
					WHEN KBN_NAME_BUBUN_DOU THEN 1
					WHEN KBN_NAME_BUBUN_SEI THEN 2
					WHEN KBN_NAME_ZENSHIN THEN 3
					WHEN KBN_NAME_SONOTA THEN 4
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
			(
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE b.statistic_satsuei_id1
						WHEN '7001' THEN KBN_NAME_SPECT
						WHEN '7002' THEN KBN_NAME_BUBUN_DOU
						WHEN '7003' THEN KBN_NAME_BUBUN_SEI
						WHEN '7004' THEN KBN_NAME_ZENSHIN
						WHEN '7009' THEN KBN_NAME_SONOTA
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
					INNER JOIN exbuitable eb
					ON
						eb.ris_id = e.ris_id AND
						eb.satueistatus = '1'
					INNER JOIN buimaster b
					ON
						b.bui_id = eb.bui_id AND
						b.statistic_satsuei_id1 IN ('7001', '7002', '7003', '7004', '7009')
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND 
					e.kensatype_id = '700'
			) g
			WHERE
				g.kbn_name IS NOT NULL
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.examenddate_yyyymmdd10, 
						g.kbn_name
					) ,
					(
						g.kbn_name
					)
				)
		) t
	ORDER BY
		t.kbn_showorder,
		t.kbn_name,
		t.gid,
		t.examenddate_yyyymmdd10;

	--“§Ž‹‘¢‰e@•ª—Þ•ÊWŒv•\
	CURSOR cDR(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_DAY
			WHEN 2 THEN GYO_TOTAL_KUBUN
			WHEN 3 THEN GYO_TOTAL
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.examenddate_yyyymmdd10, 
					g.kbn_name
				) gid,
				g.examenddate_yyyymmdd10,
				g.kbn_name,
				CASE g.kbn_name
					WHEN KBN_NAME_I_TOSHI THEN 0
					WHEN KBN_NAME_CHOKUCHO THEN 1
					WHEN KBN_NAME_SHOKUDO_TOSHI THEN 2
					WHEN KBN_NAME_SHOCHO_TOSHI THEN 3
					WHEN KBN_NAME_DIC THEN 4
					WHEN KBN_NAME_ERCP THEN 5
					WHEN KBN_NAME_PTCD THEN 6
					WHEN KBN_NAME_TUBE_ZOEI THEN 7
					WHEN KBN_NAME_IVH THEN 8
					WHEN KBN_NAME_FUJINKA THEN 9
					WHEN KBN_NAME_ESWL THEN 10
					WHEN KBN_NAME_SONOTA_ZOEI THEN 11
					WHEN KBN_NAME_NAISHIKYO THEN 12
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
			(
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE b.statistic_satsuei_id1
						WHEN '2001' THEN KBN_NAME_I_TOSHI
						WHEN '2002' THEN KBN_NAME_CHOKUCHO
						WHEN '2003' THEN KBN_NAME_SHOKUDO_TOSHI
						WHEN '2004' THEN KBN_NAME_SHOCHO_TOSHI
						WHEN '2005' THEN KBN_NAME_DIC
						WHEN '2006' THEN KBN_NAME_ERCP
						WHEN '2007' THEN KBN_NAME_PTCD
						WHEN '2008' THEN KBN_NAME_TUBE_ZOEI
						WHEN '2009' THEN KBN_NAME_IVH
						WHEN '2010' THEN KBN_NAME_FUJINKA
						WHEN '2011' THEN KBN_NAME_ESWL
						WHEN '2012' THEN KBN_NAME_SONOTA_ZOEI
						WHEN '2013' THEN KBN_NAME_NAISHIKYO
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
					INNER JOIN exbuitable eb
					ON
						eb.ris_id = e.ris_id AND
						eb.satueistatus = '1'
					INNER JOIN buimaster b
					ON
						b.bui_id = eb.bui_id AND
						b.statistic_satsuei_id1 IN ('2001', '2002', '2003', '2004', '2005', '2006', '2007', '2008', '2009', '2010', '2011', '2012', '2013')
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND
					e.kensasitu_id IN ('040', '041', '042', '043', 'TO') AND --ŠO—ˆ“§Ž‹‚É‚ ‚½‚é‚à‚Ì
					e.kensatype_id = '200'
			) g
			WHERE
				g.kbn_name IS NOT NULL
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.examenddate_yyyymmdd10, 
						g.kbn_name
					) ,
					(
						g.examenddate_yyyymmdd10
					),
					(
						g.kbn_name
					),
					()
				)
		) t
	ORDER BY
		t.kbn_showorder,
		t.kbn_name,
		t.gid,
		t.examenddate_yyyymmdd10;


	--‚t‚r@•ª—Þ•ÊWŒv•\
	--’´‰¹”g
	CURSOR cUS(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_BUNRUI_DAY
			WHEN 3 THEN GYO_TOTAL_DAY
			WHEN 4 THEN GYO_TOTAL_KUBUN
			WHEN 5 THEN GYO_TOTAL_BUNRUI
			WHEN 7 THEN GYO_TOTAL
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.bunrui_name,
		t.bunrui_showorder,
		t.ss_id,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.examenddate_yyyymmdd10, 
					g.bunrui_name,
					g.ss_id
				) gid,
				g.examenddate_yyyymmdd10,
				g.bunrui_name,
				CASE g.bunrui_name
					WHEN BUNRUI_NAME_SHINZO THEN 0
					WHEN BUNRUI_NAME_FUKUBU THEN 1
					WHEN BUNRUI_NAME_TAIHYO THEN 2
					ELSE 999
				END bunrui_showorder, --•\Ž¦‡Ý’è
				g.ss_id,
				CASE g.ss_id
					WHEN '4101' THEN KBN_NAME_SEIJIN
					WHEN '4102' THEN KBN_NAME_SHONI
					WHEN '4103' THEN KBN_NAME_ZOEI
					WHEN '4104' THEN KBN_NAME_BUNRUI_TOKUSHU
					WHEN '4105' THEN KBN_NAME_KEISHOKUDO
					WHEN '4201' THEN KBN_NAME_FUKUBU
					WHEN '4202' THEN KBN_NAME_HINYOKIKA
					WHEN '4203' THEN KBN_NAME_SANKA
					WHEN '4204' THEN KBN_NAME_BUNRUI_SONOTA
					WHEN '4205' THEN KBN_NAME_KEIDOMYAKU_ZOEI
					WHEN '4206' THEN KBN_NAME_KEIJOMYAKU_ZOEI
					WHEN '4207' THEN KBN_NAME_PFA_PEIT
					WHEN '4208' THEN KBN_NAME_SEIKEN_SENSHI
					WHEN '4301' THEN KBN_NAME_NYUSEN
					WHEN '4302' THEN KBN_NAME_KOJOSEN
					WHEN '4303' THEN KBN_NAME_KEKKANKEI
					WHEN '4304' THEN KBN_NAME_BUNRUI_SONOTA
					ELSE ''
				END kbn_name,
				CASE g.ss_id
					WHEN '4101' THEN 0 --¬l
					WHEN '4102' THEN 1 --¬Ž™
					WHEN '4103' THEN 2 --‘¢‰e
					WHEN '4104' THEN 3 --“ÁŽê
					WHEN '4105' THEN 4 --ŒoH“¹
					WHEN '4201' THEN 5 --• •”
					WHEN '4202' THEN 6 --”å”AŠíŒn
					WHEN '4203' THEN 7 --ŽY‰È
					WHEN '4204' THEN 8 --‚»‚Ì‘¼
					WHEN '4205' THEN 9 --Œo“®–¬‘¢‰e
					WHEN '4206' THEN 10 --ŒoÃ–¬‘¢‰e
					WHEN '4207' THEN 11 --‚o‚e‚`^‚o‚d‚h‚s
					WHEN '4208' THEN 12 --¶ŒŸEúŽh
					WHEN '4301' THEN 13 --“û‘B
					WHEN '4302' THEN 14 --bó‘B
					WHEN '4303' THEN 15 --ŒŒŠÇŒn
					WHEN '4304' THEN 16 --‚»‚Ì‘¼
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
				(
					SELECT
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						CASE 
							WHEN b.ss_id IN ('4101', '4102', '4103', '4104', '4105') THEN BUNRUI_NAME_SHINZO
							WHEN b.ss_id IN ('4201', '4202', '4203', '4204', '4205', '4206', '4207', '4208') THEN BUNRUI_NAME_FUKUBU
							WHEN b.ss_id IN ('4301', '4302', '4303', '4304') THEN BUNRUI_NAME_TAIHYO
							ELSE ''
						END bunrui_name,
						b.ss_id
					FROM
						exmaintable e
						INNER JOIN exbuitable eb
						ON
							eb.ris_id = e.ris_id AND
							eb.satueistatus = '1'
						INNER JOIN 
						(
							SELECT bui_id, statistic_satsuei_id1 ss_id FROM buimaster UNION ALL
							SELECT bui_id, statistic_satsuei_id2       FROM buimaster UNION ALL
							SELECT bui_id, statistic_us_id             FROM buimaster 
						) b
						ON
							b.bui_id = eb.bui_id AND
							b.ss_id IN ('4101', '4102', '4103', '4104', '4105', '4201', '4202', '4203', '4204', '4205', '4206', '4207', '4208', '4301', '4302', '4303', '4304')
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90 AND 
						e.kensatype_id = '400'
				) g
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.examenddate_yyyymmdd10, 
						g.bunrui_name,
						g.ss_id
					) ,
					(
						g.examenddate_yyyymmdd10, 
						g.bunrui_name
					),
					(
						g.bunrui_name,
						g.ss_id
					),
					(
						g.bunrui_name
					) ,
					(
						g.examenddate_yyyymmdd10
					) ,
					()
				)
		) t
	ORDER BY
		t.bunrui_showorder,
		t.kbn_showorder,
		t.gid,
		t.examenddate_yyyymmdd10;

	--US
	--ƒhƒvƒ‰
	CURSOR cUSDoppler(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 4 THEN GYO_TOTAL_KUBUN
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.bunrui_name,
		t.bunrui_showorder,
		t.ss_id,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g2.examenddate_yyyymmdd10, 
					g2.bunrui_name,
					g2.ss_id
				) gid,
				g2.examenddate_yyyymmdd10,
				g2.bunrui_name,
				CASE g2.bunrui_name
					WHEN BUNRUI_NAME_SHINZO THEN 0
					WHEN BUNRUI_NAME_FUKUBU THEN 1
					WHEN BUNRUI_NAME_TAIHYO THEN 2
					WHEN BUNRUI_NAME_KENKO_KANRI THEN 3
					WHEN BUNRUI_NAME_BYOSHINRENKE THEN 4
					ELSE 999
				END bunrui_showorder, --•\Ž¦‡Ý’è
				g2.ss_id,
				CASE g2.ss_id
					WHEN '4011' THEN KBN_NAME_PORTABLE
					WHEN '4021' THEN KBN_NAME_PORTABLE
					WHEN '4022' THEN KBN_NAME_BUNRUI_DOPPLER
					WHEN '4031' THEN KBN_NAME_BUNRUI_DOPPLER
					WHEN '4091' THEN BUNRUI_NAME_KENKO_KANRI
					WHEN '4092' THEN BUNRUI_NAME_BYOSHINRENKE
					ELSE ''
				END kbn_name,
				CASE g2.ss_id
					WHEN '4011' THEN 0 --ƒ|[ƒ^ƒuƒ‹
					WHEN '4022' THEN 1 --ƒhƒvƒ‰
					WHEN '4021' THEN 2 --ƒ|[ƒ^ƒuƒ‹
					WHEN '4031' THEN 3 --ƒhƒvƒ‰
					WHEN '4091' THEN 4 --Œ’NŠÇ—ƒZƒ“ƒ^[
					WHEN '4092' THEN 5 --•af˜AŒg
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
				(
					SELECT
						TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
						CASE 
							WHEN b.statistic_us_bunrui_id IN ('4011') THEN BUNRUI_NAME_SHINZO
							WHEN b.statistic_us_bunrui_id IN ('4021', '4022') THEN BUNRUI_NAME_FUKUBU
							WHEN b.statistic_us_bunrui_id IN ('4031') THEN BUNRUI_NAME_TAIHYO
							WHEN b.statistic_us_bunrui_id IN ('4091') THEN BUNRUI_NAME_KENKO_KANRI
							WHEN b.statistic_us_bunrui_id IN ('4092') THEN BUNRUI_NAME_BYOSHINRENKE
							ELSE ''
						END bunrui_name,
						b.statistic_us_bunrui_id ss_id
					FROM
						exmaintable e
						INNER JOIN exbuitable eb
						ON
							eb.ris_id = e.ris_id AND
							eb.satueistatus = '1'
						INNER JOIN buimaster b
						ON
							b.bui_id = eb.bui_id AND
							b.statistic_us_bunrui_id IN ('4011', '4021', '4022', '4031', '4091', '4092')
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90 AND 
						e.kensatype_id = '400'
				) g2
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g2.examenddate_yyyymmdd10, 
						g2.bunrui_name,
						g2.ss_id
					) ,
					(
						g2.bunrui_name,
						g2.ss_id
					)
				)
		) t
	ORDER BY
		t.bunrui_showorder,
		t.kbn_showorder,
		t.gid,
		t.examenddate_yyyymmdd10;

	--US
	--‘¢‰eÜ
	CURSOR cUSZoueizai(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 2 THEN GYO_TOTAL_KUBUN
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.parts_id,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					TO_CHAR(e.examenddate, 'YYYY/MM/DD'), 
					ez.parts_id
				) gid,
				TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
				ez.parts_id,
				CASE ez.parts_id
					WHEN 'JD5370' THEN KBN_NAME_LEVOVIST
					WHEN 'JD5460' THEN KBN_NAME_SONAZOID
					WHEN 'JD0520' THEN KBN_NAME_SEISHOKU_1K
					WHEN 'JD0525' THEN KBN_NAME_SEISHOKU_100
					WHEN 'JD0530' THEN KBN_NAME_SEISHOKU_20
					ELSE ''
				END kbn_name,
				CASE ez.parts_id
					WHEN 'JD5370' THEN 0 --ƒŒƒ{ƒrƒXƒg
					WHEN 'JD5460' THEN 1 --ƒ\ƒiƒ]ƒCƒh
					WHEN 'JD0520' THEN 2 --¶H‚P‚O‚O‚‚Œ@‚P‚j
					WHEN 'JD0525' THEN 3 --¶—H‰–…@‚P‚O‚O‚‚Œ
					WHEN 'JD0530' THEN 4 --¶—H‰–…@@‚Q‚O‚‚Œ
					ELSE 999
				END kbn_showorder,
				SUM(ez.suuryou_iji) cnt
			FROM
				exmaintable e
				INNER JOIN exbuitable eb
				ON
					eb.ris_id = e.ris_id AND
					eb.satueistatus = '1'
				INNER JOIN exzoueizaitable ez
				ON
					ez.ris_id = eb.ris_id AND
					ez.bui_no = eb.no AND
					ez.parts_id IN ('JD5370', 'JD5460', 'JD0520', 'JD0525', 'JD0530')
			WHERE
				e.examenddate >= P_EXAMENDDATE AND
				e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
				e.status = 90 AND 
				e.kensatype_id = '400'
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						TO_CHAR(e.examenddate, 'YYYY/MM/DD'),
						ez.parts_id
					) ,
					(
						ez.parts_id
					)
				)
		) t
	ORDER BY
		t.kbn_showorder,
		t.gid,
		t.examenddate_yyyymmdd10;

	--ˆã—Ã‹Zp•”f—ÃŒŸ¸‰È@ŒŽ•ñ
	--¡”N“xWŒv
	CURSOR cGeppo(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_BUNRUI
			WHEN 3 THEN GYO_TOTAL
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.bunrui_id,
		t.bunrui_name,
		t.bunrui_showorder,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.bunrui_id,
					g.kbn_name
				) gid,
				g.bunrui_id,
				CASE g.bunrui_id
					WHEN GEPPO_BUNRUI_ID_IPPAN THEN 'ˆê”ÊŽB‰e'
					WHEN GEPPO_BUNRUI_ID_DR THEN 'Xü“§Ž‹'
					WHEN GEPPO_BUNRUI_ID_CT THEN 'CT'
					WHEN GEPPO_BUNRUI_ID_KEKKAN THEN 'ŒŒŠÇ‘¢‰e'
					WHEN GEPPO_BUNRUI_ID_US THEN '’´‰¹”g'
					WHEN GEPPO_BUNRUI_ID_MRI THEN 'MRI'
					WHEN GEPPO_BUNRUI_ID_KESSEKI THEN 'Œ‹Î”jÓŽ¡—Ã'
					WHEN GEPPO_BUNRUI_ID_RI_TAIGAI THEN 'RI‘ÌŠO‘ª’è'
					WHEN GEPPO_BUNRUI_ID_RI_NAIYO THEN 'RI“à—p—Ã–@'
					WHEN GEPPO_BUNRUI_ID_PETCT THEN 'PET-CT'
					WHEN GEPPO_BUNRUI_ID_KOTSUEN THEN 'œ‰–’è—Ê'
					ELSE ''
				END bunrui_name, 
				CASE g.bunrui_id
					WHEN GEPPO_BUNRUI_ID_IPPAN THEN 0
					WHEN GEPPO_BUNRUI_ID_DR THEN 1
					WHEN GEPPO_BUNRUI_ID_CT THEN 2
					WHEN GEPPO_BUNRUI_ID_KEKKAN THEN 3
					WHEN GEPPO_BUNRUI_ID_US THEN 4
					WHEN GEPPO_BUNRUI_ID_MRI THEN 5
					WHEN GEPPO_BUNRUI_ID_KESSEKI THEN 6
					WHEN GEPPO_BUNRUI_ID_RI_TAIGAI THEN 7
					WHEN GEPPO_BUNRUI_ID_RI_NAIYO THEN 8
					WHEN GEPPO_BUNRUI_ID_PETCT THEN 9
					WHEN GEPPO_BUNRUI_ID_KOTSUEN THEN 10
					ELSE 999
				END bunrui_showorder, --•\Ž¦‡Ý’è
				g.kbn_name,
				CASE g.bunrui_id
					WHEN GEPPO_BUNRUI_ID_IPPAN THEN 
						CASE g.kbn_name
							WHEN GEPPO_KBN_NAME_IPPAN THEN 0
							WHEN GEPPO_KBN_NAME_PORTABLE THEN 1
							ELSE 999
						END
					WHEN GEPPO_BUNRUI_ID_DR THEN 
						CASE g.kbn_name
							WHEN GEPPO_KBN_NAME_JOFUKUBU THEN 0
							WHEN GEPPO_KBN_NAME_CHOKUCHO THEN 1
							WHEN GEPPO_KBN_NAME_SONOTA THEN 2
							ELSE 999
						END
					WHEN GEPPO_BUNRUI_ID_CT THEN 
						CASE g.kbn_name
							WHEN GEPPO_KBN_NAME_TOBU THEN 0
							WHEN GEPPO_KBN_NAME_KYOFUKUBU THEN 1
							WHEN GEPPO_KBN_NAME_SONOTA THEN 2
							ELSE 999
						END
					WHEN GEPPO_BUNRUI_ID_KEKKAN THEN
						CASE g.kbn_name
							WHEN GEPPO_KBN_NAME_TOBU THEN 0
							WHEN GEPPO_KBN_NAME_KYOFUKUBU THEN 1
							WHEN GEPPO_KBN_NAME_SHINZO THEN 2
							WHEN GEPPO_KBN_NAME_SONOTA THEN 3
							ELSE 999
						END
					WHEN GEPPO_BUNRUI_ID_US THEN 
						CASE g.kbn_name
							WHEN GEPPO_KBN_NAME_FUKUBU THEN 0
							WHEN GEPPO_KBN_NAME_SHINZO THEN 1
							WHEN GEPPO_KBN_NAME_SONOTA THEN 2
							ELSE 999
						END
					WHEN GEPPO_BUNRUI_ID_MRI THEN
						CASE g.kbn_name
							WHEN GEPPO_KBN_NAME_TOBU THEN 0
							WHEN GEPPO_KBN_NAME_KYOFUKUBU THEN 1
							WHEN GEPPO_KBN_NAME_SONOTA THEN 2
							ELSE 999
						END
					WHEN GEPPO_BUNRUI_ID_KESSEKI THEN 
						CASE g.kbn_name
							WHEN GEPPO_KBN_NAME_NYORO THEN 0
							WHEN GEPPO_KBN_NAME_TANDO THEN 1
							ELSE 999
						END
					WHEN GEPPO_BUNRUI_ID_RI_TAIGAI THEN 0 --RI‘ÌŠO‘ª’è
					WHEN GEPPO_BUNRUI_ID_RI_NAIYO THEN 0
					WHEN GEPPO_BUNRUI_ID_PETCT THEN 0
					WHEN GEPPO_BUNRUI_ID_KOTSUEN THEN 0
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
				(
					SELECT
						b.ss_id ss_id,
						CASE 
							--•”ˆÊ•ª—Þƒ}ƒXƒ^
							WHEN b.ss_id IN ('8001', '8002', '8003', '8009') THEN GEPPO_BUNRUI_ID_KEKKAN
							WHEN b.ss_id IN ('6001', '6002', '6009') THEN GEPPO_BUNRUI_ID_MRI
							WHEN b.ss_id = '7001' THEN GEPPO_BUNRUI_ID_RI_TAIGAI
							WHEN b.ss_id = '7002' THEN GEPPO_BUNRUI_ID_RI_NAIYO
							WHEN b.ss_id IN ('9001','9002','9003','9004','9005','9006','9007','9008','9009','9010','9011','9012','9013','9014','9015','9016') THEN GEPPO_BUNRUI_ID_PETCT
							WHEN b.ss_id = '1201' THEN GEPPO_BUNRUI_ID_KOTSUEN
							--•”ˆÊƒ}ƒXƒ^
							WHEN b.ss_id IN ('1101', '1102', '1103', '1104', '1105', '1106') THEN GEPPO_BUNRUI_ID_IPPAN
							WHEN b.ss_id IN ('2101', '2102', '2199') THEN GEPPO_BUNRUI_ID_DR
							WHEN b.ss_id IN ('5101', '5102', '5103', '5104', '5105', '5106', '5107', '5108', '5109') THEN GEPPO_BUNRUI_ID_CT
							WHEN b.ss_id IN ('4200', '4201', '4202', '4203', '4204', '4205', '4206', '4207', '4208', '4101', '4102', '4103', '4104', '4105', '4301', '4302', '4303', '4304') THEN GEPPO_BUNRUI_ID_US
							WHEN b.ss_id IN ('2103', '2104') THEN GEPPO_BUNRUI_ID_KESSEKI
							ELSE ''
						END bunrui_id,
						CASE 
							--•”ˆÊ•ª—Þƒ}ƒXƒ^
							--ŒŒŠÇ‘¢‰e
							WHEN b.ss_id = '8001' THEN GEPPO_KBN_NAME_TOBU
							WHEN b.ss_id = '8002' THEN GEPPO_KBN_NAME_KYOFUKUBU
							WHEN b.ss_id = '8003' THEN GEPPO_KBN_NAME_SHINZO
							WHEN b.ss_id = '8009' THEN GEPPO_KBN_NAME_SONOTA
							--MRI
							WHEN b.ss_id = '6001' THEN GEPPO_KBN_NAME_TOBU
							WHEN b.ss_id = '6002' THEN GEPPO_KBN_NAME_KYOFUKUBU
							WHEN b.ss_id = '6009' THEN GEPPO_KBN_NAME_SONOTA
							--RI‘ÌŠO‘ª’è
							WHEN b.ss_id = '7001' THEN GEPPO_KBN_NAME_RI_TAIGAI
							--RI“à—p—Ã–@
							WHEN b.ss_id = '7002' THEN GEPPO_KBN_NAME_RI_NAIYO
							--PET-CT
							WHEN b.ss_id IN ( '9001','9002','9003','9004','9005','9006','9007','9008','9009','9010','9011','9012','9013','9014','9015','9016') THEN GEPPO_KBN_NAME_PETCT
							--œ‰–’è—Ê
							WHEN b.ss_id = '1201' THEN GEPPO_KBN_NAME_KOTSUEN
							--•”ˆÊƒ}ƒXƒ^
							--ˆê”ÊŽB‰e
							WHEN b.ss_id IN ('1101', '1102', '1103', '1104', '1105') THEN GEPPO_KBN_NAME_IPPAN
							WHEN b.ss_id = '1106' THEN GEPPO_KBN_NAME_PORTABLE
							--Xü“§Ž‹
							WHEN b.ss_id = '2101' THEN GEPPO_KBN_NAME_JOFUKUBU
							WHEN b.ss_id = '2102' THEN GEPPO_KBN_NAME_CHOKUCHO
							WHEN b.ss_id = '2199' THEN GEPPO_KBN_NAME_SONOTA
							--CT
							WHEN b.ss_id IN ('5101', '5102') THEN GEPPO_KBN_NAME_TOBU
							WHEN b.ss_id IN ('5103', '5104') THEN GEPPO_KBN_NAME_KYOFUKUBU
							WHEN b.ss_id IN ('5105', '5106', '5107', '5108', '5109') THEN GEPPO_KBN_NAME_SONOTA
							--’´‰¹”g
							WHEN b.ss_id IN ('4200', '4201', '4202', '4203', '4204', '4205', '4206', '4207', '4208') THEN GEPPO_KBN_NAME_FUKUBU
							WHEN b.ss_id IN ('4101', '4102', '4103', '4104', '4105') THEN GEPPO_KBN_NAME_SHINZO
							WHEN b.ss_id IN ('4301', '4302', '4303', '4304') THEN GEPPO_KBN_NAME_SONOTA
							--Œ‹Î”jÓŽ¡—Ã
							WHEN b.ss_id = '2103' THEN GEPPO_KBN_NAME_NYORO
							WHEN b.ss_id = '2104' THEN GEPPO_KBN_NAME_TANDO
							ELSE ''
						END kbn_name
					FROM
						exmaintable e
						INNER JOIN 
						(
							SELECT
								eb_inner.ris_id,
								MIN(eb_inner.no) no
							FROM
								exbuitable eb_inner
							WHERE
								eb_inner.satueistatus = '1'
							GROUP BY
								eb_inner.ris_id
						) eb_key
						ON
							eb_key.ris_id = e.ris_id
						INNER JOIN exbuitable eb
						ON
							eb.ris_id = eb_key.ris_id AND
							eb.no = eb_key.no
						INNER JOIN 
						(
							SELECT
								b_inner.bui_id bui_id,
								bb_inner.statistic_geppo_bunrui_id ss_id
							FROM
								buimaster b_inner
								INNER JOIN buibunruimaster bb_inner
								ON
									bb_inner.buibunrui_id = b_inner.buibunrui_id AND
									bb_inner.statistic_geppo_bunrui_id IN 
									(
										'8001', '8002', '8003', '8009', --ŒŒŠÇ‘¢‰e
										'6001', '6002', '6009', --MRI
										'7001', --RI‘ÌŠO‘ª’è
										'7002', --RI“à—p—Ã–@
										'9001','9002','9003','9004','9005','9006','9007','9008','9009','9010','9011','9012','9013','9014','9015','9016', --PET-CT
										'1201' --œ‰–’è—Ê
									)
							UNION ALL
							SELECT
								b_inner.bui_id,
								b_inner.statistic_geppo_bunrui_id
							FROM
								buimaster b_inner
							WHERE
								b_inner.statistic_geppo_bunrui_id IN 
								(
									'1101', '1102', '1103', '1104', '1105', '1106', --ˆê”ÊŽB‰e
									'2101', '2102', '2199', --Xü“§Ž‹
									'5101', '5102', '5103', '5104', '5105', '5106', '5107', '5108', '5109', --CT
									'4101', '4102', '4103', '4104', '4105', '4200', '4201', '4202', '4203', '4204', '4205', '4206', '4207', '4208', '4301', '4302', '4303', '4304', --’´‰¹”g
									'2103', '2104' --Œ‹Î”jÓŽ¡—Ã
								)
						) b
						ON
							b.bui_id = eb.bui_id 
					WHERE
						e.examenddate >= P_EXAMENDDATE AND
						e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
						e.status = 90
				) g
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.bunrui_id,
						g.kbn_name
					) ,
					(
						g.bunrui_id
					),
					()
				)
		) t
	ORDER BY
		t.bunrui_showorder,
		t.kbn_showorder,
		t.gid;

	--ˆã—Ã‹Zp•”f—ÃŒŸ¸‰È@ŒŽ•ñ
	--‘O”N“xWŒv
	CURSOR cGeppoPrev(P_TARGET_MONTH VARCHAR2) IS 
	SELECT
		s.class_key bunrui_id,
		s.exam_count cnt
	FROM
		statistics_monthly_data s
	WHERE
		s.target_month = P_TARGET_MONTH; --yyyyMM‚Ì•¶Žš—ñ

	--Film“ÇžE‘o/CD“ÇžE‘oˆóü
	CURSOR cCDFilm(P_EXAMENDDATE DATE) IS 
	SELECT
		CASE t.gid
			WHEN 0 THEN GYO_DETAIL
			WHEN 1 THEN GYO_TOTAL_DAY
			WHEN 2 THEN GYO_TOTAL_KUBUN
			WHEN 3 THEN GYO_TOTAL
			ELSE ''
		END gyo_syosai,
		t.gid,
		t.examenddate_yyyymmdd10,
		t.kbn_name,
		t.kbn_showorder,
		t.cnt
	FROM
		(
			SELECT
				GROUPING_ID
				(
					g.examenddate_yyyymmdd10, 
					g.kbn_name
				) gid,
				g.examenddate_yyyymmdd10,
				g.kbn_name,
				CASE g.kbn_name
					WHEN CD_FILM_KBN_NAME_FILM_READ THEN 0
					WHEN CD_FILM_KBN_NAME_FILM_WRITE THEN 1
					WHEN CD_FILM_KBN_NAME_CD_READ THEN 2
					WHEN CD_FILM_KBN_NAME_CD_WRITE THEN 3
					ELSE 999
				END kbn_showorder, --•\Ž¦‡Ý’è
				COUNT(*) cnt
			FROM
			(
				SELECT
					TO_CHAR(e.examenddate, 'YYYY/MM/DD') examenddate_yyyymmdd10,
					CASE o.kensasitu_id
						WHEN 'BU3' THEN CD_FILM_KBN_NAME_FILM_READ
						WHEN 'BU4' THEN CD_FILM_KBN_NAME_FILM_WRITE
						WHEN 'BU1' THEN CD_FILM_KBN_NAME_CD_READ
						WHEN 'BU2' THEN CD_FILM_KBN_NAME_CD_WRITE
						ELSE ''
					END kbn_name
				FROM
					exmaintable e
					INNER JOIN exbuitable eb
					ON
						eb.ris_id = e.ris_id AND
						eb.satueistatus = '1'
					INNER JOIN ordermaintable o
					ON
						o.ris_id = e.ris_id
				WHERE
					e.examenddate >= P_EXAMENDDATE AND
					e.examenddate < LAST_DAY(P_EXAMENDDATE) + 1 AND
					e.status = 90 AND
					o.kensasitu_id IN ('BU1', 'BU2', 'BU3', 'BU4') --Film“Çž¥‘o/CD“Çž¥‘o
			) g
			WHERE
				g.kbn_name IS NOT NULL
			GROUP BY
				GROUPING SETS --¬Œvì¬s
				(
					(
						g.examenddate_yyyymmdd10, 
						g.kbn_name
					) ,
					(
						g.examenddate_yyyymmdd10
					),
					(
						g.kbn_name
					),
					()
				)
		) t
	ORDER BY
		t.kbn_showorder,
		t.kbn_name,
		t.gid,
		t.examenddate_yyyymmdd10;




	--Œ^’è‹`
	TYPE buiBunruiCur IS REF CURSOR RETURN cBuiBunrui%ROWTYPE;
	TYPE buiCur IS REF CURSOR RETURN cBui%ROWTYPE;
	TYPE buiAllCur IS REF CURSOR RETURN cBuiAll%ROWTYPE;
	TYPE filmCur IS REF CURSOR RETURN cFilm%ROWTYPE;
	TYPE zoueizaiCur IS REF CURSOR RETURN cZoueizai%ROWTYPE;
	TYPE emergencyCur IS REF CURSOR RETURN cEmergency%ROWTYPE;
	TYPE ippanCur IS REF CURSOR RETURN cIppan%ROWTYPE;
	TYPE ctCur IS REF CURSOR RETURN cCT%ROWTYPE;
	TYPE riCur IS REF CURSOR RETURN cRI%ROWTYPE;
	TYPE drCur IS REF CURSOR RETURN cDR%ROWTYPE;
	TYPE usCur IS REF CURSOR RETURN cUS%ROWTYPE;
	TYPE usDopplerCur IS REF CURSOR RETURN cUSDoppler%ROWTYPE;
	TYPE usZoueizaiCur IS REF CURSOR RETURN cUSZoueizai%ROWTYPE;
	TYPE geppoCur IS REF CURSOR RETURN cGeppo%ROWTYPE;
	TYPE geppoPrevCur IS REF CURSOR RETURN cGeppoPrev%ROWTYPE;
	TYPE cdFilmCur IS REF CURSOR RETURN cCDFilm%ROWTYPE;


	--ŠÖ”’è‹`
	--ŽB‰eŒŸ¸”ˆê——i•”ˆÊ•ª—Þj
	PROCEDURE GetBuiBunruiData(
		P_EXAMENDDATE in DATE,
		P_PLACE_ID in  VARCHAR2,
		P_BUIBUNRUI_CURSOR in out buiBunruiCur
	);

	--ŽB‰eŒŸ¸”ˆê——iŽåsˆ×j
	--ŽÀŽ{êŠ‘I‘ðŽž
	PROCEDURE GetBuiData(
		P_EXAMENDDATE in DATE,
		P_PLACE_ID in  VARCHAR2,
		P_BUI_CURSOR in out buiCur
	);

	--ŽB‰eŒŸ¸”ˆê——iŽåsˆ×j
	--ALL‘I‘ðŽž
	PROCEDURE GetBuiDataAll(
		P_EXAMENDDATE in DATE,
		P_BUI_ALL_CURSOR in out buiAllCur
	);

	--ƒtƒBƒ‹ƒ€ˆê——
	PROCEDURE GetFilmData(
		P_EXAMENDDATE in DATE,
		P_PLACE_ID in  VARCHAR2,
		P_FILM_CURSOR in out filmCur
	);

	--‘¢‰eÜE–òÜˆê——
	PROCEDURE GetZoueizaiData(
		P_EXAMENDDATE in DATE,
		P_PLACE_ID in  VARCHAR2,
		P_ZOUEIZAI_CURSOR in out zoueizaiCur
	);

	--‹~–½‹~‹}ƒZƒ“ƒ^[@WŒv•\
	PROCEDURE GetEmergencyData(
		P_EXAMENDDATE in DATE,
		P_EMERGENCY_CURSOR in out emergencyCur
	);

	--ˆê”ÊŽB‰e@•ª—Þ•ÊWŒv•\ˆóü
	PROCEDURE GetIppanData(
		P_EXAMENDDATE in DATE,
		P_IPPAN_CURSOR in out ippanCur
	);

	--‚b‚s@•ª—Þ•ÊWŒv•\ˆóü
	PROCEDURE GetCTData(
		P_EXAMENDDATE in DATE,
		P_CT_CURSOR in out ctCur
	);

	--‚q‚h@•ª—Þ•ÊWŒv•\ˆóü
	PROCEDURE GetRIData(
		P_EXAMENDDATE in DATE,
		P_RI_CURSOR in out riCur
	);

	--“§Ž‹‘¢‰e@•ª—Þ•ÊWŒv•\ˆóü
	PROCEDURE GetDRData(
		P_EXAMENDDATE in DATE,
		P_DR_CURSOR in out drCur
	);

	--‚t‚r@•ª—Þ•ÊWŒv•\ˆóü
	PROCEDURE GetUSData(
		P_EXAMENDDATE in DATE,
		P_US_CURSOR in out usCur,
		P_US_DOPPLER_CURSOR in out usDopplerCur,
		P_US_ZOUEIZAI_CURSOR in out usZoueizaiCur
	);

	--ˆã—Ã‹Zp•”f—ÃŒŸ¸‰È@ŒŽ•ñˆóü
	PROCEDURE GetGeppoData(
		P_EXAMENDDATE in DATE,
		P_GEPPO_CURSOR in out geppoCur,
		P_GEPPO_PREV_CURSOR in out geppoPrevCur
	);

	--Film“ÇžE‘o/CD“ÇžE‘oˆóü
	PROCEDURE GetCDFilmData(
		P_EXAMENDDATE in DATE,
		P_CD_FILM_CURSOR in out cdFilmCur
	);

END STATISTICS_PRINT_PACKAGE;