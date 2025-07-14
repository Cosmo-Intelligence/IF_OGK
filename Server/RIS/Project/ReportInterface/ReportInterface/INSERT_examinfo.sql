INSERT INTO examinfo
(
	id,
	hspid,
	patid,
	kana,
	roma,
	kanji,
	sex,
	birthday,
	age,
	examdate,
	modality,
	locus,
	detaillocus,
	requestdate,
	requestsection,
	requestdoctor,
	inoutpatient,
	ward,
	purpose,
	diagnosis,
	remarks,
	odrid,
	status,
	examroom,
	require ,
	medicine,
	reqcomment,
	infection,
	tall,
	weight,
	operationdoctor1,
	operationdoctor2,
	operationdoctor3,
	examinedoctor,
	diagnosissection
)
SELECT
SEQ_REPORTEXAM_ID.NEXTVAL id,
'1' hspid,
o.kanja_id patid,
p.kanasimei kana,
p.romasimei roma,
p.kanjisimei kanji,
p.sex sex,
p.birthday birthday,
o.kensa_date_age age,
o.kensa_Date examdate,
k.kensatype_name modality,
bb.buibunrui_name locus,
b.bui_name detaillocus,
eo.order_date requestdate,
s.section_name requestsection,
o.irai_doctor_name requestdoctor,
o.denpyo_nyugaikbn inoutpatient,
CASE o.denpyo_nyugaikbn 
WHEN '2' THEN o.denpyo_byoutou_id
ELSE NULL
END ward,
oi.kensa_siji purpose,
oi.rinshou diagnosis,
oi.remarks remarks,
o.orderno odrid,
0 status,
er.examroom_name examroom,
eo.sikyu_flg require ,
NULL medicine,
NULL reqcomment,
NULL infection,
p.tall tall,
p.weight weight,
NULL operationdoctor1,
NULL operationdoctor2,
NULL operationdoctor3,
NULL examinedoctor,
OrderBuiTable.Bui_IDÅ@=Å@DIAGNOSI diagnosissection
FROM
ordermaintable o
LEFT OUTER JOIN patientinfo p
ON
	p.kanja_id = o.kanja_id
LEFT OUTER JOIN orderindicatetable oi
ON
	oi.ris_id = o.ris_id
LEFT OUTER JOIN extendorderinfo eo
ON
	eo.ris_id = o.ris_id
LEFT OUTER JOIN kensatypemaster k
ON
	k.kensatypeid = o.kensatypeid
LEFT OUTER JOIN sectionmaster s
ON
	s.section_id = o.irai_section_id
LEFT OUTER JOIN examroommaster er
ON
	er.examroom_id = o.kansasituid
LEFT OUTER JOIN 
(
	SELECT
		ob_inner.ris_id,
		ob_inner.no,
		ob_inner.bui_id
	FROM
		orderbuitable ob_inner
		INNER JOIN 
			(
				SELECT
					ob_last.ris_id ris_id,
					MAX(ob_last.no) no
				FROM
					orderbuitable ob_last
				GROUP BY
					ob_last.ris_id
			) ob_last_inner
		ON
			ob_last_inner.ris_id = ob_inner.ris_id AND
			ob_last_inner.no = oob_inner.no

) ob
ON
	ob.bui_id = o.kensatypeid
LEFT OUTER JOIN buimaster b
ON
	b.bui_id = ob.bui_id
LEFT OUTER JOIN buibunruimaster bb
ON
	bb.buibunrui_id = b.buibunrui_id
LEFT OUTER JOIN diagnosis_section_master ds
ON
	ds.comment_id = ob.bui_id


