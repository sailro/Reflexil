@echo off

if "%1"=="" goto USAGE
set PLUGIN=%1
echo -=[Generating Reflexil for %PLUGIN% %2]=-

REM ########## Build binaries ##################################################
echo|set /p=Building Reflexil...
set PATH=.\tools;%PATH%
msbuild ..\..\reflexil.sln /t:Clean,Build /p:Configuration=Release
set BUILD_STATUS=%ERRORLEVEL%
if %BUILD_STATUS%==0 echo Success!
if not %BUILD_STATUS%==0 (
	echo Failed!
	exit /b %BUILD_STATUS%
)

rm -fr output
mkdir output

REM ########## Copying binaries ################################################
echo Copying Reflexil for %PLUGIN% binaries
cp ..\..\bin\Release\*.dll output
cp ..\..\bin\Release\*.config output
cp ..\..\Plugins\Reflexil.%PLUGIN%\bin\Release\*.dll output
cp ..\..\Plugins\Reflexil.%PLUGIN%\bin\Release\PluginConfig.xml output 2> NUL
cp ..\..\Changelog output
cp ..\..\credits.txt output

REM ########## Copying licenses ################################################
echo Zipping licenses
cd ..\..\
.\Build\Binary\tools\zip -r licenses.zip Licenses > NUL
cd .\Build\Binary\
mv ..\..\licenses.zip output

REM ########## Retrieve current version ########################################
grep "AssemblyVersion" ..\..\Properties\SolutionVersion.cs | sed "s/.*\"\(.\..\)\..\..\".*/\1/g" > version.txt
set /P REFLEXIL_VERSION=<version.txt

if "%2"=="AIO" goto ILMERGE
goto CLEANUP

REM ########## ILMerge assemblies ##############################################
:ILMERGE
if "%ILMERGEINPUT%"=="" goto USAGE
if "%ILMERGEOUTPUT%"=="" goto USAGE

echo|set /p=ILMerging into %ILMERGEOUTPUT:~9%...
set ASSEMBLIES=.\output\Reflexil.dll .\output\Be.Windows.Forms.HexBox.Reflexil.dll .\output\De4dot.Reflexil.dll .\output\ICSharpCode.NRefactory.Reflexil.dll .\output\ICSharpCode.SharpDevelop.Dom.Reflexil.dll .\output\ICSharpCode.TextEditor.Reflexil.dll .\output\Mono.Cecil.Reflexil.dll .\output\Mono.Cecil.Mdb.Reflexil.dll .\output\Mono.Cecil.Pdb.Reflexil.dll
ILMerge /ndebug /keyfile:..\..\Keys\reflexil.snk /out:%ILMERGEOUTPUT% %ASSEMBLIES% %ILMERGEINPUT% /lib:..\..\Plugins\Reflexil.%PLUGIN%\Libs\Binaries
set ILMERGE_STATUS=%ERRORLEVEL%
if %ILMERGE_STATUS%==0 echo Success!
if not %ILMERGE_STATUS%==0 (
	echo Failed!
	goto end
)

set REFLEXIL_VERSION=%REFLEXIL_VERSION%.AIO
rm -f %ASSEMBLIES% .\output\Reflexil.dll.config
if "%ILMERGEINPUT%"=="%ILMERGEOUTPUT%" goto CLEANUP
rm -f %ILMERGEINPUT%

REM ########## Package and clean-up ############################################
:CLEANUP
set SIGNATURE=reflexil.for.%PLUGIN%.%REFLEXIL_VERSION%.bin
echo Packaging %SIGNATURE%.zip
rm -fr %SIGNATURE%
mv output %SIGNATURE%

rm -fr %SIGNATURE%.zip
zip -r %SIGNATURE%.zip %SIGNATURE% > NUL

rm -fr %SIGNATURE%
rm version.txt

goto END

REM ########## Usage ###########################################################
:USAGE
echo Usage: generate Plugin [AIO]
echo Where Plugin is Reflector^|ILSpy^|JustDecompile
echo Optional AIO for an All In One (IlMerged) version, for this ILMERGEINPUT and ILMERGEOUTPUT env. vars must be set

:END
echo.