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
using Reflexil.Wrappers;

#endregion

namespace Reflexil.Plugins.CecilStudio
{
	internal class CecilStudioAssemblyWrapper : IAssemblyWrapper
	{
		#region Fields

		private readonly AssemblyDefinition _adef;

		#endregion

		#region Properties

		public AssemblyDefinition AssemblyDefinition
		{
			get { return _adef; }
		}

		public string Name
		{
			get { return _adef != null ? _adef.Name.Name : string.Empty; }
		}

		public string Location
		{
			get { return (_adef != null) ? _adef.MainModule.Image.FileName : string.Empty; }
		}

		public bool IsValid
		{
			get { return _adef != null; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="assembly">assembly to wrap</param>
		public CecilStudioAssemblyWrapper(AssemblyDefinition assembly)
		{
			_adef = assembly;
		}

		/// <summary>
		/// ToString Override
		/// </summary>
		/// <returns>Provide a name (commonly used by browser nodes)</returns>
		public override string ToString()
		{
			return Name;
		}

		#endregion
	}
}