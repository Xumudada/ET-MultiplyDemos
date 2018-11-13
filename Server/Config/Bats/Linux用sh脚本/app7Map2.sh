#! /bin/sh
cd /root/Server/Map2/publish
dotnet App.dll --appId=7 --appType=Map --config=../ServerConfig_Linux.txt
