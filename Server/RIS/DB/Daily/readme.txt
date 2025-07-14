= 日次患者要求について =

== ファイル ==
 * insert_tohisinfo_patient.sql
  InsertSQL。本日検査日のORDERMAINTABLEをTOHISINFOテーブルへINSERTする
 * patient_to_tohisinfo.bat
  insert_tohisinfo_patient.sqlを呼ぶ
 * patient_to_tohisinfo_exec.bat
  日次患者要求起動バッチ。patient_to_tohisinfo.batをDB接続先をパラメータとして起動する。
