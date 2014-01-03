@echo off

set PATH=.\tools;%PATH%

rm -fr sources
wget https://bitbucket.org/0xd4d/de4dot/get/master.zip -O de4dot.zip
wget https://bitbucket.org/0xd4d/dnlib/get/master.zip -O dnlib.zip
unzip de4dot.zip
unzip dnlib.zip
mv 0xd4d-dnlib-* dnlib
mv 0xd4d-de4dot-* sources
rm de4dot.zip dnlib.zip
mkdir sources\dnlib\src
mv dnlib\src\* sources\dnlib\src
rm -fr cecil dnlib