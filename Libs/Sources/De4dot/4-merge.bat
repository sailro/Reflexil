@echo off

set PATH=.\tools;%PATH%

ILMerge /keyfile:.\sources\de4dot.snk /out:De4Dot.dll .\sources\Release\bin\de4dot.code.dll .\sources\Release\bin\de4dot.mdecrypt.dll .\sources\Release\bin\blocks.dll .\sources\Release\bin\AssemblyData.dll .\sources\Release\bin\ICSharpCode.SharpZipLib.dll .\sources\Release\bin\Mono.Cecil.dll

mv -f De4Dot.dll ..\..\Binaries\
rm De4Dot.pdb
