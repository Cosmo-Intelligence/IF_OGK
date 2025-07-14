rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
rem --- Service Registration Delete --- 
rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

@echo off
setlocal
set _CURPATH=%~dp0

SC Delete ARISReceiveHospitalService
SC Delete ARISReceiveOrderService
SC Delete ARISSendCostService
SC Delete ARISSendPatientService
SC Delete ARISSendReceiptService
SC Delete ReportInterfaceService

cmd /k
