@echo off

set PATH=.\tools;%PATH%

msbuild /t:restore sources\de4dot.netframework.sln
msbuild sources\de4dot.netframework.sln /p:Configuration=Release,GenerateAssemblyInfo=false