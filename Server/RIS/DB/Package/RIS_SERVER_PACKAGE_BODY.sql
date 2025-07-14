CREATE OR REPLACE PACKAGE BODY "RIS_SERVER_PACKAGE" IS

  --RIS_IDを新規に発番する
  --YYYYMMDDnnnnnnnn	16桁から構成されるユニーク番号
  --YYYYMMDD	発番時のシステム時刻（SYSDATE）の年月日
  --nnnnnnnn	RISIDSequenceﾃｰﾌﾞﾙで管理された連番で、10000001～99999999の繰返し
  FUNCTION "GET_NEW_RIS_ID"
  RETURN VARCHAR2
  IS
    v_ris_id CHAR(16);
  BEGIN -- executable part starts here
    select TO_CHAR(SYSDATE, 'YYYYMMDD') || TO_CHAR(RISIDSEQUENCE.NEXTVAL, 'FM00000009') INTO v_ris_id FROM DUAL;
    RETURN v_ris_id;
  END "GET_NEW_RIS_ID";


  --STUDYINSTANCEUIDを新規に作成する
  --1.2.392.200045.6960.4.7.nnnnnn.RIS_ID	45桁以上で構成されるユニーク番号
  --1.2.392.200045.6960.4.7.	ﾊﾟｯｹｰｼﾞ識別コード（RadiQuest/RISの識別コード）横河電機の各パッケージに割当てられたユニークなコードである。
  --nnnnnn.	ライセンス番号　製品1本毎にエンジ部で発行する番号で、システムメンテナンス画面のシステム定義（設定１）から設定する。
  --RIS_ID	RIS識別ID
  FUNCTION "GET_NEW_STUDYINSTANCEUID" (
    "P_RIS_ID" IN VARCHAR2)
  RETURN VARCHAR2
  IS
    package_cd CONSTANT CHAR(24) := '1.2.392.200045.6960.4.7.';
    v_licenseno CHAR(6);
  BEGIN -- executable part starts here
    SELECT licenseno INTO v_licenseno FROM systemdefine;
    RETURN package_cd || TRIM(v_licenseno) || '.'  || P_RIS_ID;
  END "GET_NEW_STUDYINSTANCEUID";

  --トークン（区切り文字による部分文字列）の取り出し 
  --P_STRING 対象文字列  
  --P_DELIMIT トークンの区切り文字列  
  --P_POS 切り出し開始 要素の番号（1～） ≠ 文字位置 1 
  --P_NTH 切り出し要素数 1 
  --P_EOD 行末に到達時の戻り値 NULL 
  --戻り値 切り出された文字列トークン 
  FUNCTION STRTOKEN(
    "P_STRING" VARCHAR2,
    "P_DELIMIT" VARCHAR2,
    "P_POS" POSITIVEN := 1,
    "P_NTH" POSITIVEN := 1,
    "P_EOD" VARCHAR2 := NULL)
  RETURN VARCHAR2
  IS
    vStartPos  PLS_INTEGER;
    vEndPos    PLS_INTEGER;
  BEGIN
    IF (P_POS = 1) THEN
      vStartPos := 1;
    ELSE
      vStartPos := INSTR(P_STRING, P_DELIMIT, 1, P_POS - 1);
      IF (vStartPos = 0) THEN
        RETURN P_EOD;
      END IF;
      vStartPos := vStartPos + 1;
    END IF;
    vEndPos := INSTR(P_STRING, P_DELIMIT, vStartPos, P_NTH);
    IF (vEndPos = 0) THEN
      RETURN SUBSTR(P_STRING, vStartPos);
    END IF;
    RETURN SUBSTR(P_STRING, vStartPos, vEndPos - vStartPos);
  END "STRTOKEN";

  -- うるう日生まれに対応
  -- マイナスになってしまうときは999才
  -- 年齢計算に関する法律を考慮した場合において、BASEDATE が大晦日のときの問題に対応
  -- P_METHOD: 0:一般的な年齢／1:年齢計算に関する法律を考慮／2:数え年
  FUNCTION YEARS_OF_AGE(
    "P_BIRTHDAY" IN DATE,
    "P_BASEDATE" IN DATE := SYSDATE,
    "P_METHOD" IN NUMBER := 0 )
  RETURN NUMBER
  IS
    vBaseDate     DATE := P_BASEDATE;
    vYearsOffset  NUMBER := 0;
    vYears        NUMBER;
  BEGIN
    IF (P_BIRTHDAY IS NULL) OR (P_BASEDATE IS NULL) THEN
      RETURN 999;
    END IF;
    IF (P_METHOD = 1) THEN
      vBaseDate := vBaseDate + 1;
    END IF;
    IF vBaseDate < P_BIRTHDAY THEN
      RETURN 999;
    END IF;
    vYears := EXTRACT(YEAR FROM vBaseDate) - EXTRACT(YEAR FROM P_BIRTHDAY);
    IF (P_METHOD = 2) THEN
      vYearsOffset := 1;
    ELSIF (TO_CHAR(vBaseDate, 'MMDD') < TO_CHAR(P_BIRTHDAY, 'MMDD')) THEN
      vYearsOffset := -1;
    END IF;

    RETURN vYears + vYearsOffset;
  END "YEARS_OF_AGE";

  --数値に変換する
  --P_VALUE      IN VARCHAR2 数値変換対象文字列
  --P_PRECISION    IN NUMBER 整数部最大桁数
  --P_SCALE     IN NUMBER 小数点以下桁数
  --P_DEFAULT   IN NUMBER := NULL 最大桁数を超えたとき、または例外が生成に設定する数値 
  FUNCTION TO_NUMBER_DEF(
    P_VALUE      IN VARCHAR2,
    P_PRECISION    IN NUMBER,
    P_SCALE     IN NUMBER,
    P_DEFAULT   IN NUMBER := NULL
  )
  RETURN NUMBER
  AS
    vNumberValue NUMBER;
    vConverted VARCHAR2(256);
    vINDEX_SHARP NUMBER(1);
  BEGIN
    vNumberValue := TO_NUMBER(P_VALUE);--いったんNumberに変換
    vNumberValue := ROUND(vNumberValue, P_SCALE);
    IF LENGTH(TRUNC(vNumberValue)) > P_PRECISION THEN
      RETURN P_DEFAULT;-- 整数部最大桁数を超えた
    END IF;
    RETURN vNumberValue;
  EXCEPTION
   --dbms_output.put_line('TO_NUMBER_DEF error: ' || SQLERRM);
    WHEN OTHERS THEN
      RETURN P_DEFAULT;
  END "TO_NUMBER_DEF";

  --指定された秒数を追加して返す
  --P_DATE      IN DATE 日付
  --P_SECOND    IN NUMBER 追加する秒数(整数を指定してください)
  FUNCTION ADD_SECONDS(
    P_DATE      IN DATE,
    P_SECOND    IN NUMBER
  )
  RETURN DATE
  AS
  BEGIN
    IF (P_SECOND IS NULL) THEN
      RETURN P_DATE;
    END IF;
    RETURN P_DATE + (P_SECOND / 86400);
  END "ADD_SECONDS";

END "RIS_SERVER_PACKAGE";
/
