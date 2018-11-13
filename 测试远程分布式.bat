@echo off
rd /q /s %cd%\Bin
rd /q /s %cd%\Output
cd %cd%/Server/App
dotnet publish
dotnet clean
cd %cd%/../../
del /a /f %cd%\Bin\Pathfinding.Ionic.Zip.Reduced.dll

copy /y %cd%\Server\Configs\ServerConfig_Linux.txt  %cd%\Bin\

xcopy %cd%\Bin\*.* %cd%\Output\DB\ /s /e /c /y /h /r
xcopy %cd%\Bin\*.* %cd%\Output\Realm\ /s /e /c /y /h /r
xcopy %cd%\Bin\*.* %cd%\Output\Location\ /s /e /c /y /h /r
xcopy %cd%\Bin\*.* %cd%\Output\Gate1\ /s /e /c /y /h /r
xcopy %cd%\Bin\*.* %cd%\Output\Gate2\ /s /e /c /y /h /r
xcopy %cd%\Bin\*.* %cd%\Output\Map1\ /s /e /c /y /h /r
xcopy %cd%\Bin\*.* %cd%\Output\Map2\ /s /e /c /y /h /r

echo Auto Rsyncing...
start %cd%/Server/Tools/cwRsync/rsync.exe -vzrtopg --password-file=./Server/Tools/cwRsync/Config/rsync.secrets --delete ./Output/ root@118.89.50.112::Upload/ --chmod=ugo=rwX

:loop
tasklist | find /i "rsync.exe"  >nul || goto :reboot
goto :loop
:reboot
ssh -p 22 root@111.222.333.444 'reboot'
rem 要使用免密码远程ssh命令需安装公钥
rem 需要修改Server/Tools/cwRsync/Config/rsync.secrets文件中的密码为Linux密码
rem 需要编辑ssh命令中的端口/Linux账号/服务器外网IP
rem 恢复电脑备份时本地ssh记录文件可能过期 删除即可
rem 需要先确认Server\App\Program.cs中每个服务器是否添加了必要的组件
rem 需要额外复制必要的文件 如xxconfig.txt/寻路数据