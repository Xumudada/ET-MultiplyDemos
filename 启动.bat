@echo off
rd /q /s %cd%\Bin
rd /q /s %cd%\Output
cd %cd%/Server/App
dotnet publish
dotnet clean
cd %cd%/../../
del /a /f %cd%\Bin\Pathfinding.Ionic.Zip.Reduced.dll

xcopy /y %cd%\Server\Config\StartConfig\LocalAllServer.txt  %cd%\Bin\
xcopy /y %cd%\Server\Config\UnitConfig.txt  %cd%\Bin\Config\
xcopy /y %cd%\Server\Config\graph.bytes  %cd%\Bin\Config\

cd %cd%/Bin/publish
dotnet App.dll --appId=1 --appType=AllServer --config=../LocalAllServer.txt