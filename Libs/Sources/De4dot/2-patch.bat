@echo off

set PATH=.\tools;%PATH%

cp ..\..\..\Keys\reflexil.snk .\sources\de4dot.snk
cp ..\..\..\Keys\reflexil.snk .\sources\dnlib\dnlib.snk
cp -f .\patch\de4dot.sln .\sources\de4dot.sln

echo. >> .\sources\de4dot.code\Properties\AssemblyInfo.cs
echo // HACK - Reflexil - Access to internals >> .\sources\de4dot.code\Properties\AssemblyInfo.cs

echo [assembly: System.Runtime.CompilerServices.InternalsVisibleTo ("Reflexil, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")] >> .\sources\de4dot.code\Properties\AssemblyInfo.cs
echo [assembly: System.Runtime.CompilerServices.InternalsVisibleTo ("Reflexil.Reflector, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")] >> .\sources\de4dot.code\Properties\AssemblyInfo.cs
echo [assembly: System.Runtime.CompilerServices.InternalsVisibleTo ("Reflexil.CecilStudio, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")] >> .\sources\de4dot.code\Properties\AssemblyInfo.cs
echo [assembly: System.Runtime.CompilerServices.InternalsVisibleTo ("Reflexil.ILSpy.Plugin, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")] >> .\sources\de4dot.code\Properties\AssemblyInfo.cs

rpl -R -x".csproj" "<TargetFrameworkVersion>v2.0</TargetFrameworkVersion>" "<TargetFrameworkVersion>v3.5</TargetFrameworkVersion>" .\sources
rpl -R -x".csproj" "<AssemblyName>de4dot</AssemblyName>" "<AssemblyName>De4dot.Reflexil</AssemblyName>" .\sources

echo // Foo > .\sources\dnlib\src\ExtensionAttribute.cs