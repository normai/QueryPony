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
echo Summary: This fetches a fresh QueryPonyLib.dll physically into the QueryPonyGui/libs/ folder.
echo This is called from the project's Pre-build event, wanted by the single-file-delivery feature.
echo Parameter = %1
echo ***********************************************************
@echo on

echo Copying library from QueryPony/QueryPonyLib/bin/x64/%1/
copy %~dp0..\QueryPonyLib\bin\x64\%1\QueryPonyLib.dll %~dp0libs\QueryPonyLib.dll

rem pause
