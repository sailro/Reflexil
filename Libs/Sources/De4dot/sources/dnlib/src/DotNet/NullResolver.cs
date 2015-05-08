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

﻿namespace dnlib.DotNet {
	/// <summary>
	/// A resolver that always fails
	/// </summary>
	public sealed class NullResolver : IAssemblyResolver, IResolver {
		/// <summary>
		/// The one and only instance of this type
		/// </summary>
		public static readonly NullResolver Instance = new NullResolver();

		NullResolver() {
		}

		/// <inheritdoc/>
		public AssemblyDef Resolve(IAssembly assembly, ModuleDef sourceModule) {
			return null;
		}

		/// <inheritdoc/>
		public bool AddToCache(AssemblyDef asm) {
			return true;
		}

		/// <inheritdoc/>
		public bool Remove(AssemblyDef asm) {
			return false;
		}

		/// <inheritdoc/>
		public void Clear() {
		}

		/// <inheritdoc/>
		public TypeDef Resolve(TypeRef typeRef, ModuleDef sourceModule) {
			return null;
		}

		/// <inheritdoc/>
		public IMemberForwarded Resolve(MemberRef memberRef) {
			return null;
		}
	}
}
