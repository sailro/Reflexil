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

using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Reflexil.Plugins
{
	public class BaseAssemblyContext : IAssemblyContext
	{
		protected TCecilDef TryGetOrAdd<TCecilDef, TExternalDef>(Dictionary<TExternalDef, TCecilDef> cache, TExternalDef item, Func<TExternalDef, TCecilDef> finder) where TCecilDef : class
		{
			TCecilDef result;

			if (cache.TryGetValue(item, out result))
				return result;

			result = finder(item);
			if (result == null)
				return null;

			cache.Add(item, result);
			return result;
		}

		public virtual AssemblyDefinition AssemblyDefinition
		{
			get;
			set;
		}
	}
}
