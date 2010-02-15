/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
using Mono.Cecil;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Plugins.CecilStudio
{
    class CecilStudioAssemblyWrapper : IAssemblyWrapper
    {
        #region " Fields "
        private AssemblyDefinition m_adef;
        #endregion

        #region " Properties "
        public string Location
        {
            get { return (m_adef != null) ? m_adef.MainModule.Image.FileInformation.FullName : string.Empty; }
        }

        public bool IsValid
        {
            get { return m_adef != null; }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assembly">assembly to wrap</param>
        public CecilStudioAssemblyWrapper(AssemblyDefinition assembly)
        {
            this.m_adef = assembly;
        }

        /// <summary>
        /// ToString Override
        /// </summary>
        /// <returns>Provide a name (commonly used by browser nodes)</returns>
        public override string ToString()
        {
            return (m_adef != null) ? m_adef.Name.Name : string.Empty;
        }
        #endregion

    }
}
