Reflexil [![Build Status](https://sailro.visualstudio.com/Reflexil/_apis/build/status/sailro.Reflexil?branchName=master)](https://sailro.visualstudio.com/Reflexil/_build/latest?definitionId=2&branchName=master)
========

# This project has been discontinued
> After 16 years of proud service, this project has been discontinued. ILSpy 8 moved to .NET Core, and many changes would be required to keep it running, including:
> 
> - Migrate assemblies to .NET Standard and/or .NET Core
> - Migrate CodeDomProvider (that we use to compile stuff) to Roslyn 
> - Use Roslyn instead of NRefactory for completion
> 
> All these tasks have already been done by dnSpy, now taken over by [dnSpyEx](https://github.com/dnSpyEx). I recommend using this project going forward.

The .NET Assembly Editor

Reflexil is an assembly editor and runs as a plug-in for Red Gate's **Reflector**, **ILSpy** and **Telerik's JustDecompile**. Reflexil is using Mono.Cecil, written by Jb Evain and is able to manipulate IL code and save the modified assemblies to disk. Reflexil also supports C#/VB.NET code injection.

Homepage: http://reflexil.net

Howto: http://www.codeproject.com/KB/msil/reflexil.aspx

Compatible with:
- ILSpy
- Reflector
- Telerik JustDecompile

Videos:
- Converting a .NET GUI application to console using Reflexil and ILSpy (http://bit.ly/1H5RDdh)
- Unity3D assembly patching (AngryBots game) with Reflexil  (http://bit.ly/un1ty)
- Playing with Reflexil and Reflector (http://bit.ly/kill3rv1d) 

Download stable releases here: https://github.com/sailro/Reflexil/releases

or nightly releases here: https://sailro.visualstudio.com/Reflexil/_build?definitionId=2&_a=summary&view=runs
