/*
    Copyright (C) 2012-2014 de4dot@gmail.com

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
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
    CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

﻿using System.Collections.Generic;
using dnlib.PE;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet {
	/// <summary>
	/// Interface to decrypt methods
	/// </summary>
	public interface IMethodDecrypter {
		/// <summary>
		/// Gets the method's body
		/// </summary>
		/// <param name="rid"><c>Method</c> rid</param>
		/// <param name="rva">The <see cref="RVA"/> found in the method's <c>Method</c> row</param>
		/// <param name="parameters">The method's parameters</param>
		/// <param name="gpContext">Generic parameter context</param>
		/// <param name="methodBody">Updated with the method's <see cref="MethodBody"/> if this
		/// method returns <c>true</c></param>
		/// <returns><c>true</c> if the method body was decrypted, <c>false</c> if the method isn't
		/// encrypted and the default <see cref="MethodDef"/> body reader code should be used.</returns>
		bool GetMethodBody(uint rid, RVA rva, IList<Parameter> parameters, GenericParamContext gpContext, out MethodBody methodBody);
	}

	/// <summary>
	/// Interface to decrypt strings
	/// </summary>
	public interface IStringDecrypter {
		/// <summary>
		/// Reads a string
		/// </summary>
		/// <param name="token">String token</param>
		/// <returns>A string or <c>null</c> if we should read it from the #US heap</returns>
		string ReadUserString(uint token);
	}
}
