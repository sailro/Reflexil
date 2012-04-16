/*
    Copyright (C) 2011-2012 de4dot@gmail.com

    This file is part of de4dot.

    de4dot is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    de4dot is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with de4dot.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("de4dot.code")]
[assembly: AssemblyDescription("Deobfuscates obfuscated .NET applications")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("de4dot.code")]
[assembly: AssemblyCopyright("Copyright (C) 2011-2012 de4dot@gmail.com")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.8.1.3405")]
[assembly: AssemblyFileVersion("1.8.1.3405")]
 
// HACK - Reflexil - Access to internals 
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo ("Reflexil, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")] 
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo ("Reflexil.Reflector, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")] 
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo ("Reflexil.CecilStudio, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")] 
