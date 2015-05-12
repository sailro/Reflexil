/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region Imports

using System.Collections.Generic;
using Mono.Cecil;

#endregion

namespace Reflexil.Compilation
{
	/// <summary>
	/// Helper interface for code generation
	/// </summary>
	public interface ILanguageHelper
	{
		#region Methods

		/// <summary>
		/// Generate method signature 
		/// </summary>
		/// <param name="mref">Method reference</param>
		/// <returns>generated source code</returns>
		string GetMethodSignature(MethodReference mref);

		/// <summary>
		/// Generate method
		/// </summary>
		/// <param name="mdef">Method definition</param>
		/// <returns>generated source code</returns>
		string GetMethod(MethodDefinition mdef);

		/// <summary>
		/// Generate field
		/// </summary>
		/// <param name="fdef">Field definition</param>
		/// <returns>generated source code</returns>
		string GetField(FieldDefinition fdef);

		/// <summary>
		/// Generate type signature
		/// </summary>
		/// <param name="tref">Type reference</param>
		/// <returns>generated source code</returns>
		string GetTypeSignature(TypeReference tref);

		/// <summary>
		/// Generate source code from method declaring type. All others
		/// methods are generated as stubs.
		/// </summary>
		/// <param name="mdef">Method definition</param>
		/// <param name="references">Assembly references</param>
		/// <returns>generated source code</returns>
		string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references);

		#endregion
	}
}