@echo off

set PATH=.\tools;%PATH%

rm .\sources\Release\bin\*.pdb
rm .\sources\Release\bin\*.xml
ILMerge /keyfile:.\sources\de4dot.snk /out:De4Dot.Reflexil.dll .\sources\Release\bin\de4dot.code.dll .\sources\Release\bin\de4dot.mdecrypt.dll .\sources\Release\bin\de4dot.blocks.dll .\sources\Release\bin\AssemblyData.dll .\sources\Release\bin\dnlib.dll

mv -f De4Dot.Reflexil.dll ..\..\Binaries\
rm De4Dot.Reflexil.pdb
