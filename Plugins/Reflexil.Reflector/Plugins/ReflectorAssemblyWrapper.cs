/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
        public object AssemblyDefinition
        {
            get { return assembly; }
        }

        public string Location
        {
            get
            {
                return (assembly != null) ? assembly.Location : string.Empty;
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
