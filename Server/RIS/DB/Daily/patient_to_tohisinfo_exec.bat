@echo off

REM %date% %time% YYYYMMDDHHMMSS組み立て
set tmpDate10=%date:~-10%
set YYYYMMDD=%tmpDate10:/=%

set tmpTime8=%time:~0,8%
set tmpTime6=%tmpTime8::=%
set HHMMSS=%tmpTime6: =0%


REM 8文字で構成されたフォルダのうち、名前でソートした最新の90個を除いたフォルダを消す
echo %date% %time% 過去ログフォルダ削除
for /f "skip=90" %%A in ('dir /b /ad /o-n ????????') do rd /s /q %%A

echo %date% %time% ログフォルダ作成
MKDIR .\%YYYYMMDD%

echo %date% %time% 日次患者要求開始

REM call patient_to_tohisinfo.bat rris rris1 rris > .\%YYYYMMDD%\%YYYYMMDD%%HHMMSS%patient_to_tohisinfo.log
call patient_to_tohisinfo.bat rris rris1 rris > .\%YYYYMMDD%\%YYYYMMDD%%HHMMSS%patient_to_tohisinfo.log

echo %date% %time% 日次患者要求終了


