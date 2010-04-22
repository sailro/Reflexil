@echo off
echo Ensure SolutionVersion.cs exists (for VS Compilations)

if exist "%1Properties\SolutionVersion.cs" goto pass
echo Creating %1Properties\SolutionVersion.cs!
cd %1AutoBuild\
build.bat
goto end

:PASS
echo %1Properties\SolutionVersion.cs exists!
goto end

:usage
echo Usage: ensuresv [ProjectPath]

:end
