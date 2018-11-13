@echo off
rd /q /s %cd%\Bin
rd /q /s %cd%\Output
cd %cd%/Server/App
dotnet publish
dotnet clean
cd %cd%/../../
del /a /f %cd%\Bin\Pathfinding.Ionic.Zip.Reduced.dll

copy /y %cd%\Server\Config\ServerConfig_Windows.txt  %cd%\Bin\

xcopy %cd%\Bin\*.* %cd%\Output\DB\ /s /e /c /y /h /r
copy /y %cd%\Server\Config\Bats\app1DB.bat  %cd%\Output\DB\
start /D "%cd%\Output\DB"  app1DB.bat

xcopy %cd%\Bin\*.* %cd%\Output\Realm\ /s /e /c /y /h /r
copy /y %cd%\Server\Config\Bats\app2Realm.bat  %cd%\Output\Realm\
start /D "%cd%\Output\Realm"  app2Realm.bat

xcopy %cd%\Bin\*.* %cd%\Output\Location\ /s /e /c /y /h /r
copy /y %cd%\Server\Config\Bats\app3Location.bat  %cd%\Output\Location\
start /D "%cd%\Output\Location"  app3Location.bat

xcopy %cd%\Bin\*.* %cd%\Output\Gate1\ /s /e /c /y /h /r
copy /y %cd%\Server\Config\Bats\app4Gate1.bat  %cd%\Output\Gate1\
start /D "%cd%\Output\Gate1"  app4Gate1.bat

xcopy %cd%\Bin\*.* %cd%\Output\Gate2\ /s /e /c /y /h /r
copy /y %cd%\Server\Config\Bats\app5Gate2.bat  %cd%\Output\Gate2\
start /D "%cd%\Output\Gate2"  app5Gate2.bat

xcopy %cd%\Bin\*.* %cd%\Output\Map1\ /s /e /c /y /h /r
copy /y %cd%\Server\Config\Bats\app6Map1.bat  %cd%\Output\Map1\
xcopy /y %cd%\Server\Configs\Config\graph.bytes  %cd%\Output\Map1\Config\
start /D "%cd%\Output\Map1"  app6Map1.bat

xcopy %cd%\Bin\*.* %cd%\Output\Map2\ /s /e /c /y /h /r
copy /y %cd%\Server\Config\Bats\app7Map2.bat  %cd%\Output\Map2\
xcopy /y %cd%\Server\Configs\Config\graph.bytes  %cd%\Output\Map2\Config\
start /D "%cd%\Output\Map2"  app7Map2.bat

rem 需要先确认Server\App\Program.cs中每个服务器是否添加了必要的组件
rem 需要额外复制必要的文件 如xxconfig.txt/寻路数据












