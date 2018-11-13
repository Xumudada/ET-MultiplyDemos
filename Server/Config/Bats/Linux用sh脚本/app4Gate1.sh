#! /bin/sh
cd /root/Server/Gate1/publish
dotnet App.dll --appId=4 --appType=Gate --config=../ServerConfig_Linux.txt
