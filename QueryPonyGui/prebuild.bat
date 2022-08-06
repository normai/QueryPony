@echo off
rem file : 20130708`0902
rem usage : To (re-)activate this batchfile
rem   1. In VS goto QueryPonyGui project properties
rem   2. Goto the 'Build Events' tab
rem   3. Place '$(ProjectDir)prebuild.bat' into the 'Pre-build event command line' box

rem chg 20210522`1221 : That command was taken out there to possibly find a solution without it
rem chg 20220731`1151 : Reaktivate this file. It looks like the singel-file-delivery
rem                      will not work without such batchfile

echo **************** QueryPonyGui/prebuild.bat ****************
echo Summary: QueryPonyGui depends on QueryPonyLib.dll. This batchfile
echo  fetches a freshly QueryPonyLib.dll physically into the QueryPonyGui
echo  project. This is wanted for the single-file-delivery feature.
echo  This batchfile is called from the project's Build Options Prebuild
echo  Event. Do not call it manually -- though that should not hurt.
echo ***********************************************************
@echo on

copy %~dp0..\QueryPonyLib\bin\x64\Debug\QueryPonyLib.dll %~dp0libs\QueryPonyLib.dll

rem pause
