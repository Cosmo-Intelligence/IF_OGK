CREATE OR REPLACE TRIGGER trg_tohisinfo_insert_report
	AFTER UPDATE OF report_status
		ON ordermaintable
	REFERENCING OLD AS old NEW AS new
	FOR EACH ROW WHEN (
			new.systemkbn = '0' AND
			new.report_status IN ('20', '30', '31', '40', '41') AND
			(
				old.report_status IS NULL OR
				old.report_status <> new.report_status 
			)
		)
BEGIN
	INSERT INTO tohisinfo 
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
	VALUES
	(
		FROMRISSEQUENCE.NEXTVAL,
		SYSDATE,
		:new.ris_id,
		'Report',
		'RISÉTÅ[Éo',
		:new.report_status,
		:new.orderno,
		:new.kanja_id,
		'00',
		NULL,
		NULL,
		NULL
	);
END;
/
