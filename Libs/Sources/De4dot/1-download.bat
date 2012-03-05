@echo off

set PATH=.\tools;%PATH%

rm -fr sources
wget https://github.com/0xd4d/de4dot/zipball/master -O de4dot.zip
wget https://github.com/0xd4d/cecil/zipball/master -O cecil.zip
unzip de4dot.zip
unzip cecil.zip
mv 0xd4d-cecil-* cecil
mv 0xd4d-de4dot-* sources
rm de4dot.zip cecil.zip
mv cecil\* sources\cecil
rm -fr cecil