@echo off

set PATH=.\tools;%PATH%

curl -L -k -o de4dot.zip https://github.com/0xd4d/de4dot/archive/master.zip
curl -L -k -o dnlib.zip https://github.com/0xd4d/dnlib/archive/master.zip
rm -fr sources
unzip de4dot.zip
unzip dnlib.zip
mv dnlib-master dnlib
mv de4dot-master sources
rm de4dot.zip dnlib.zip
mkdir sources\dnlib\src
mv dnlib\src\* sources\dnlib\src
rm -fr cecil dnlib