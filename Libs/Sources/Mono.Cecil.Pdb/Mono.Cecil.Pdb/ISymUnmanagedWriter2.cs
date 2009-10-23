//
// ISymUnmanagedWriter2.cs
//
// Author:
//   Juerg Billeter (j@bitron.ch)
//
// (C) 2008 Juerg Billeter
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

using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Mono.Cecil.Pdb {

	[Guid ("0B97726E-9E6D-4f05-9A26-424022093CAA")]
	[InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	interface ISymUnmanagedWriter2 : ISymUnmanagedWriter {
		// ISymUnmanagedWriter
		new void DefineDocument (
			[In, MarshalAs (UnmanagedType.LPWStr)] string url,
			[In] ref Guid langauge,
			[In] ref Guid languageVendor,
			[In] ref Guid documentType,
			[Out, MarshalAs (UnmanagedType.Interface)] out ISymUnmanagedDocumentWriter pRetVal);
		new void SetUserEntryPoint_Placeholder ();
		new void OpenMethod ([In] SymbolToken method);
		new void CloseMethod ();
		new void OpenScope ([In] int startOffset, [Out] out int pRetVal);
		new void CloseScope ([In] int endOffset);
		new void SetScopeRange_Placeholder ();
		new void DefineLocalVariable_Placeholder ();
		new void DefineParameter_Placeholder ();
		new void DefineField_Placeholder ();
		new void DefineGlobalVariable_Placeholder ();
		new void Close ();
		new void SetSymAttribute_Placeholder ();
		new void OpenNamespace ([In, MarshalAs (UnmanagedType.LPWStr)] string name);
		new void CloseNamespace ();
		new void UsingNamespace ([In, MarshalAs (UnmanagedType.LPWStr)] string fullName);
		new void SetMethodSourceRange_Placeholder ();
		new void Initialize (
			[In] IntPtr emitter,
			[In, MarshalAs (UnmanagedType.LPWStr)] string filename,
			[In] IStream pIStream,
			[In] bool fFullBuild);
		new void GetDebugInfo (
			[Out] out ImageDebugDirectory pIDD,
			[In] int cData,
			[Out] out int pcData,
			[In, Out, MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 1)] byte [] data);
		new void DefineSequencePoints (
			[In, MarshalAs (UnmanagedType.Interface)] ISymUnmanagedDocumentWriter document,
			[In] int spCount,
			[In, MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 1)] int [] offsets,
			[In, MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 1)] int [] lines,
			[In, MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 1)] int [] columns,
			[In, MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 1)] int [] endLines,
			[In, MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 1)] int [] endColumns);
		new void RemapToken_Placeholder ();
		new void Initialize2_Placeholder ();
		new void DefineConstant_Placeholder ();
		new void Abort_Placeholder ();

		// ISymUnmanagedWriter2
		void DefineLocalVariable2 (
			[In, MarshalAs (UnmanagedType.LPWStr)] string name,
			[In] int attributes,
			[In] SymbolToken sigToken,
			[In] int addrKind,
			[In] int addr1,
			[In] int addr2,
			[In] int addr3,
			[In] int startOffset,
			[In] int endOffset);
	}
}
