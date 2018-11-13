#! /bin/sh
cd /root/Server/Realm/publish
dotnet App.dll --appId=2 --appType=Realm --config=../ServerConfig_Linux.txt
