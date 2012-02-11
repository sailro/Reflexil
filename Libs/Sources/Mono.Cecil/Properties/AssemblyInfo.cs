//
// AssemblyInfo.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2011 Jb Evain
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle ("Mono.Cecil")]
[assembly: AssemblyProduct ("Mono.Cecil")]
[assembly: AssemblyCopyright ("Copyright © 2008 - 2011 Jb Evain")]

[assembly: ComVisible (false)]

[assembly: Guid ("fd225bb4-fa53-44b2-a6db-85f5e48dcb54")]

[assembly: AssemblyVersion ("0.9.5.0")]
#if !CF
[assembly: AssemblyFileVersion ("0.9.5.0")]
#endif

[assembly: InternalsVisibleTo ("Mono.Cecil.Pdb, PublicKey=002400000480000094000000060200000024000052534131000400000100010079159977d2d03a8e6bea7a2e74e8d1afcc93e8851974952bb480a12c9134474d04062447c37e0e68c080536fcf3c3fbe2ff9c979ce998475e506e8ce82dd5b0f350dc10e93bf2eeecf874b24770c5081dbea7447fddafa277b22de47d6ffea449674a4f9fccf84d15069089380284dbdd35f46cdff12a1bd78e4ef0065d016df")]
[assembly: InternalsVisibleTo ("Mono.Cecil.Mdb, PublicKey=002400000480000094000000060200000024000052534131000400000100010079159977d2d03a8e6bea7a2e74e8d1afcc93e8851974952bb480a12c9134474d04062447c37e0e68c080536fcf3c3fbe2ff9c979ce998475e506e8ce82dd5b0f350dc10e93bf2eeecf874b24770c5081dbea7447fddafa277b22de47d6ffea449674a4f9fccf84d15069089380284dbdd35f46cdff12a1bd78e4ef0065d016df")]
[assembly: InternalsVisibleTo ("Mono.Cecil.Rocks, PublicKey=002400000480000094000000060200000024000052534131000400000100010079159977d2d03a8e6bea7a2e74e8d1afcc93e8851974952bb480a12c9134474d04062447c37e0e68c080536fcf3c3fbe2ff9c979ce998475e506e8ce82dd5b0f350dc10e93bf2eeecf874b24770c5081dbea7447fddafa277b22de47d6ffea449674a4f9fccf84d15069089380284dbdd35f46cdff12a1bd78e4ef0065d016df")]
[assembly: InternalsVisibleTo ("Mono.Cecil.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010079159977d2d03a8e6bea7a2e74e8d1afcc93e8851974952bb480a12c9134474d04062447c37e0e68c080536fcf3c3fbe2ff9c979ce998475e506e8ce82dd5b0f350dc10e93bf2eeecf874b24770c5081dbea7447fddafa277b22de47d6ffea449674a4f9fccf84d15069089380284dbdd35f46cdff12a1bd78e4ef0065d016df")]
// HACK - Reflexil - Access to internals
[assembly: InternalsVisibleTo ("Reflexil, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")]
[assembly: InternalsVisibleTo ("Reflexil.Reflector, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")]
[assembly: InternalsVisibleTo ("Reflexil.CecilStudio, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd73d865f854370fed97a7083188e96c70fb23e24e4e6f9d4e849af21b057ef123d44fe5f221bd035de75efce3b997efc81bb1214b307121814e2cc066f4615c111464379131b92af8796b4e3ecf4663341c1429c3179e558af997955eda044d2a54a2bd0586c886f96657594f1e64f598e0c2abe933266c767c7632d8636c9c")]
