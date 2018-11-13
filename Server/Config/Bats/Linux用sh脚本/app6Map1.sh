#! /bin/sh
cd /root/Server/Map1/publish
dotnet App.dll --appId=6 --appType=Map --config=../ServerConfig_Linux.txt
