#! /bin/sh
cd /root/Server/DB/publish
dotnet App.dll --appId=1 --appType=DB --config=../ServerConfig_Linux.txt
