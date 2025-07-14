rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
rem --- Service Registration --- 
rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

@echo off
setlocal
set _CURPATH=%~dp0

rem ARISReceiveHospital
SC Create ARISReceiveHospitalService binPath= "%_CURPATH%ARISReceiveHospital\ARISReceiveHospital.exe" start= delayed-auto DisplayName= "RIS-HIS患者受信_OGK"
rem echo サービス登録 RIS-HIS患者受信_OGK(ARISReceiveHospitalService)
rem SC Query ARISReceiveHospitalService

rem ARISReceiveOrder
SC Create ARISReceiveOrderService binPath= "%_CURPATH%ARISReceiveOrder\ARISReceiveOrder.exe" start= delayed-auto DisplayName= "RIS-HISオーダー受信_OGK"
rem echo サービス登録 RIS-HISオーダー受信_OGK(ARISReceiveOrderService)
rem SC Query ARISReceiveOrderService

rem ARISSendCost
SC Create ARISSendCostService binPath= "%_CURPATH%ARISSendCost\ARISSendCost.exe" start= delayed-auto DisplayName= "RIS-HIS実施送信_OGK"
rem echo サービス登録 RIS-HIS実施送信_OGK(ARISReceiveOrderService)
rem SC Query ARISSendCostService

rem ARISSendPatient
SC Create ARISSendPatientService binPath= "%_CURPATH%ARISSendPatient\ARISSendPatient.exe" start= delayed-auto DisplayName= "RIS-HIS患者要求送信_OGK"
rem echo サービス登録 RIS-HIS患者要求送信_OGK(ARISReceiveOrderService)
rem SC Query ARISSendPatientService

rem ARISSendReceipt
SC Create ARISSendReceiptService binPath= "%_CURPATH%ARISSendReceipt\ARISSendReceipt.exe" start= delayed-auto DisplayName= "RIS-HIS受付送信_OGK"
rem echo サービス登録 RIS-HIS受付送信_OGK(ARISReceiveOrderService)
rem SC Query ARISSendReceiptService

rem ReportInterface
SC Create ReportInterfaceService binPath= "%_CURPATH%ReportInterface\ReportInterface.exe" start= delayed-auto DisplayName= "RIS-REPORT連携機能_OGK"
rem echo サービス登録 RIS-REPORT連携機能_OGK(ARISReceiveOrderService)
rem SC Query ReportInterfaceService

cmd /k
