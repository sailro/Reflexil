@echo off

set PATH=.\tools;%PATH%

rm .\sources\Release\net35\*.pdb
ILMerge /keyfile:.\sources\de4dot.snk /out:De4Dot.Reflexil.dll .\sources\Release\net35\de4dot.code.dll .\sources\Release\net35\de4dot.mdecrypt.dll .\sources\Release\net35\de4dot.blocks.dll .\sources\Release\net35\AssemblyData.dll .\sources\Release\net35\dnlib.dll

mv -f De4Dot.Reflexil.dll ..\..\Binaries\
rm De4Dot.Reflexil.pdb
