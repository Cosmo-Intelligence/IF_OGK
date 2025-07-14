@echo off

echo %date% %time% 日次患者要求処理開始 ユーザ:"%1" パスワード:"%2" サービス名:"%3"

title 日次患者要求処理

@if "%1"=="" goto param_not_found
@if "%2"=="" goto param_not_found
@if "%3"=="" goto param_not_found


echo %date% %time% 日次患者要求開始
sqlplus rris/rris1@rris @insert_tohisinfo_patient.sql
echo %date% %time% 日次患者要求終了

goto end

:param_not_found
echo 引数を設定してください
echo patient_to_tohisinfo.bat ユーザ パスワード サービス名
echo 例: patient_to_tohisinfo.bat rris rris1 rris ^> patient_to_tohisinfo.log

:end
