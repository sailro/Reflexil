@echo off

set PATH=.\tools;%WINDIR%\Microsoft.NET\Framework\v4.0.30319;%PATH%

msbuild sources\de4dot.sln /p:Configuration=net_3_5_Release;TargetFrameworkVersion=v3.5 /p:DefineConstants="NET_3_5"