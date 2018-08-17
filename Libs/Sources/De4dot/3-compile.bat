@echo off

set PATH=.\tools;%PATH%

msbuild sources\de4dot.sln /p:Configuration=Release;TargetFrameworkVersion=v3.5