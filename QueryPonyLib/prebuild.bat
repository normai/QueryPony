@echo off
echo -----------------------------------------------------------------------
rem id : 20130708o0901
echo summary : QueryPonyGui depends on iobus.lib. This batchfile fetches
echo    a freshly built iobus.lib physically into the QueryPonyGui project.
echo    This is wanted for building with the single-file-delivery feature.
echo    This file is called from the project's Build Options prebuild event,
echo    do not call it manually (though that should not hurt).
echo -----------------------------------------------------------------------
echo.
@echo on

copy %~dp0..\IOBus\bin\Debug\iobus.dll %~dp0libs\iobus.dll

rem pause
