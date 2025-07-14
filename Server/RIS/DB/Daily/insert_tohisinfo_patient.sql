SET TIMING ON

SET SERVEROUTPUT ON


--日次患者要求作成
--requestdateは10秒ごとにずらして作成する
INSERT INTO TOHISINFO
(
	requestid,
	requestdate,
	ris_id,
	requestuser,
	requestterminalid,
	requesttype,
	messageid1,
	messageid2,
	transferstatus,
	transferdate,
	transferresult,
	transfertext
)
SELECT
	FROMRISSEQUENCE.NEXTVAL requestid,
	RIS_SERVER_PACKAGE.ADD_SECONDS(SYSDATE, rownum * 10) requestdate,
	o.ris_id ris_id,
	'batch' requestuser,
	'RISサーバ' requestterminalid,
	'PR01' requesttype,
	p.kanja_id messageid1,
	p.kanasimei messageid2,
	'00' transferstatus,
	NULL transferdate,
	NULL transferresult,
	NULL transfertext
FROM
	ordermaintable o
	INNER JOIN exmaintable e
	ON
		e.ris_id = o.ris_id AND
		e.status = 0
	INNER JOIN patientinfo p
	ON
		p.kanja_id = o.kanja_id
WHERE
	o.kensa_date = TO_NUMBER(TO_CHAR(SYSDATE, 'YYYYMMDD'))
/

COMMIT
/


SET SERVEROUTPUT OFF

SET TIMING OFF

EXIT;

