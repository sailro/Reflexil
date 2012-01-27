/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using Reflector.CodeModel;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Plugins.Reflector
{
    /// <summary>
    /// Assembly wrapper: used by the browser to provide load on demand
    /// </summary>
	class ReflectorAssemblyWrapper : IAssemblyWrapper
    {

        #region " Fields "
        private IAssembly assembly;
        #endregion

        #region " Properties "
        public string Location
        {
            get
            {
                return (assembly != null) ? System.Environment.ExpandEnvironmentVariables(assembly.Location) : string.Empty;
            }
        }

        public bool IsValid
        {
            get
            {
                return assembly != null && assembly.Type != AssemblyType.None;
            }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assembly">Assembly to be wrapped</param>
        public ReflectorAssemblyWrapper(IAssembly assembly)
        {
            this.assembly = assembly;
        }

        /// <summary>
        /// ToString Override
        /// </summary>
        /// <returns>Provide a name (commonly used by browser nodes)</returns>
        public override string ToString()
        {
            return (assembly != null) ? assembly.Name : string.Empty;
        }
        #endregion

    }
}
