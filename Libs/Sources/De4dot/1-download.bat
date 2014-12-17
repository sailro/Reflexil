@echo off

set PATH=.\tools;%PATH%

rm -fr sources
wget https://github.com/0xd4d/de4dot/archive/master.zip -O de4dot.zip
wget https://github.com/0xd4d/dnlib/archive/master.zip -O dnlib.zip
unzip de4dot.zip
unzip dnlib.zip
mv dnlib-master dnlib
mv de4dot-master sources
rm de4dot.zip dnlib.zip
mkdir sources\dnlib\src
mv dnlib\src\* sources\dnlib\src
rm -fr cecil dnlib