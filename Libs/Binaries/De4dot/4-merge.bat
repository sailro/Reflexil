@echo off

set PATH=.\tools;%PATH%

ILMerge /keyfile:.\sources\de4dot.snk /out:De4Dot.dll .\sources\Release\de4dot.code.dll .\sources\Release\blocks.dll .\sources\Release\AssemblyData.dll .\sources\Release\ICSharpCode.SharpZipLib.dll .\sources\Release\Mono.Cecil.dll

mv -f De4Dot.dll ..\
rm De4Dot.pdb
