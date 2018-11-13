#! /bin/sh
cd /root/Server/Gate2/publish
dotnet App.dll --appId=5 --appType=Gate --config=../ServerConfig_Linux.txt
