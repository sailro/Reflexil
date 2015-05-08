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
using dnlib.Threading;

namespace dnlib.DotNet {
	/// <summary>
	/// Returns types without getting stuck in an infinite loop
	/// </summary>
	public struct AllTypesHelper {
		/// <summary>
		/// Gets a list of all types and nested types
		/// </summary>
		/// <param name="types">A list of types</param>
		public static IEnumerable<TypeDef> Types(IEnumerable<TypeDef> types) {
			var visited = new Dictionary<TypeDef, bool>();
			var stack = new Stack<IEnumerator<TypeDef>>();
			if (types != null)
				stack.Push(types.GetSafeEnumerable().GetEnumerator());
			while (stack.Count > 0) {
				var enumerator = stack.Pop();
				while (enumerator.MoveNext()) {
					var type = enumerator.Current;
					if (visited.ContainsKey(type))
						continue;
					visited[type] = true;
					yield return type;
					if (type.NestedTypes.Count > 0) {
						stack.Push(enumerator);
						enumerator = type.NestedTypes.GetSafeEnumerable().GetEnumerator();
					}
				}
			}
		}
	}
}
