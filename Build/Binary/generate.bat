@echo off

set PATH=.\tools;%WINDIR%\Microsoft.NET\Framework\v4.0.30319;%PATH%

msbuild ..\..\reflexil.sln /t:Clean,Build /p:Configuration=Release;TargetFrameworkVersion=v3.5

rm -fr output
mkdir output

cp ..\..\bin\Release\*.dll output
cp ..\..\bin\Release\*.config output
cp ..\..\Plugins\Reflexil.Reflector\bin\Release\*.dll output
cp ..\..\Plugins\Reflexil.CecilStudio\bin\Release\*.dll output

cp ..\..\Changelog output
cp ..\..\credits.txt output
cp ..\..\README.FIRST.txt output

cd ..\..\
.\Build\Binary\tools\zip -r licenses.zip Licenses 
cd .\Build\Binary\
mv ..\..\licenses.zip output

grep "AssemblyVersion" ..\..\Properties\SolutionVersion.cs | sed "s/.*\"\(.\..\)\..\..\".*/\1/g" > version.txt
set /P REFLEXIL_VERSION=<version.txt

IF "%1"=="AIO" GOTO ILMERGE
GOTO CLEANUP

:ILMERGE
set ASSEMBLIES=.\output\Reflexil.dll .\output\Reflexil.Reflector.dll .\output\Be.Windows.Forms.HexBox.dll .\output\De4dot.dll .\output\ICSharpCode.NRefactory.dll .\output\ICSharpCode.SharpDevelop.Dom.dll .\output\ICSharpCode.TextEditor.dll .\output\Mono.Cecil.dll .\output\Mono.Cecil.Mdb.dll .\output\Mono.Cecil.Pdb.dll
ILMerge /ndebug /keyfile:..\..\Keys\reflexil.snk /out:.\output\Reflexil.Reflector.AIO.dll %ASSEMBLIES% /lib:..\..\Plugins\Reflexil.Reflector\Libs\Binaries
rm %ASSEMBLIES% .\output\Reflexil.CecilStudio.dll .\output\Reflexil.dll.config
set REFLEXIL_VERSION=%REFLEXIL_VERSION%.AIO

:CLEANUP
rm -fr reflexil.%REFLEXIL_VERSION%.bin
mv output reflexil.%REFLEXIL_VERSION%.bin

rm reflexil.%REFLEXIL_VERSION%.bin.zip
zip -r reflexil.%REFLEXIL_VERSION%.bin.zip reflexil.%REFLEXIL_VERSION%.bin

rm -fr reflexil.%REFLEXIL_VERSION%.bin
rm version.txt