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

using Mono.Cecil;

#endregion

namespace Reflexil.Wrappers
{
	/// <summary>
	/// Namespace wrapper
	/// </summary>
	internal class NamespaceWrapper
	{
		#region Fields

		private readonly string _namespace;
		private readonly ModuleDefinition _modef;

		#endregion

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="modef">Module definition</param>
		/// <param name="namespace">Namespace to wrap</param>
		public NamespaceWrapper(ModuleDefinition modef, string @namespace)
		{
			_modef = modef;
			_namespace = string.IsNullOrEmpty(@namespace) ? "-" : @namespace;
		}

		/// <summary>
		/// Determines whether the specified Object is equal to the current Object
		/// </summary>
		/// <param name="obj">Object to compare</param>
		/// <returns>True if the same namespace and module definition</returns>
		public override bool Equals(object obj)
		{
			if (obj is NamespaceWrapper)
			{
				var other = obj as NamespaceWrapper;
				return (_modef.Equals(other._modef)) && (_namespace == other._namespace);
			}
			// ReSharper disable once BaseObjectEqualsIsObjectEquals
			return base.Equals(obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash based on the module hash and the namespace hash</returns>
		public override int GetHashCode()
		{
			return (_modef.GetHashCode() + "|" + _namespace.GetHashCode()).GetHashCode();
		}

		/// <summary>
		/// Returns the namespace
		/// </summary>
		/// <returns>Namespace</returns>
		public override string ToString()
		{
			return _namespace;
		}

		#endregion
	}
}