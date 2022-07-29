@echo off

rem To re-activate this batchfile
rem   - In VS goto QueryPonyGui project properties
rem   - Goto the 'Build Events' tab
rem   - Place '$(ProjectDir)prebuild.bat' into the 'Pre-build event command line' box
rem That command was taken out there to possibly find a solution without it [chg 20210522`1221]

rem id : 20130708`0902
echo **************** QueryPonyGui/prebuild.bat ****************
echo summary : QueryPonyGui depends on iobus.lib and QueryPonyLib.exe/dll. This
echo    batchfile fetches freshly built libraries physically into the project
echo    This is wanted for building with the single-file-delivery feature.
echo    This file is called from the project's Build Options Prebuild Event,
echo    do not call it manually (though that should not hurt).
echo ***********************************************************

@echo on

copy %~dp0..\QueryPonyLib\bin\Debug\QueryPonyLib.exe %~dp0libs\QueryPonyLib.exe

rem pause
