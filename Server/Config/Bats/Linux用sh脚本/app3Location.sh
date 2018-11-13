#! /bin/sh
cd /root/Server/Location/publish
dotnet App.dll --appId=3 --appType=Location --config=../ServerConfig_Linux.txt
