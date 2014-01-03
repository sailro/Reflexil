@echo off

set PATH=.\tools;%WINDIR%\Microsoft.NET\Framework\v4.0.30319;%PATH%

msbuild sources\de4dot.sln /p:Configuration=Release;TargetFrameworkVersion=v2.0 /p:DefineConstants="NET_2_0"