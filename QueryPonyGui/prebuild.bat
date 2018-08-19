@echo off
echo -----------------------------------------------------------------------
rem id : 20130708o0902
echo summary : QueryPonyGui depends on iobus.lib and QueryPonyLib.dll. This
echo    batchfile fetches freshly built libraries physically into the project
echo    This is wanted for building with the single-file-delivery feature.
echo    This file is called from the project's Build Options Prebuild Event,
echo    do not call it manually (though that should not hurt).
echo -----------------------------------------------------------------------
echo.
@echo on

copy %~dp0..\IOBus\bin\Debug\iobus.dll %~dp0libs\iobus.dll
copy %~dp0..\IOBus\bin\Debug\iobus.pdb %~dp0libs\iobus.pdb
copy %~dp0..\QueryPonyLib\bin\Debug\QueryPonyLib.dll %~dp0libs\QueryPonyLib.dll
copy %~dp0..\QueryPonyLib\bin\Debug\QueryPonyLib.pdb %~dp0libs\QueryPonyLib.pdb

rem pause
