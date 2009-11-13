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
using Mono.Cecil;
#endregion

namespace Reflexil.Plugins.CecilStudio
{
    class CecilStudioAssemblyContext : IAssemblyContext
    {

        #region " Fields "
        private AssemblyDefinition m_adef;
        #endregion

        #region " Properties "
        public AssemblyDefinition AssemblyDefinition
        {
            get
            { 
                return m_adef; 
            }
            set
            {
                m_adef = value;
            }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Retrieve from cache or search a method definition from host program' object.
        /// </summary>
        /// <param name="item">object (ie Method declaration/definition)</param>
        /// <returns>Method definition or null if not found</returns>
        public MethodDefinition GetMethodDefinition(object item)
        {
            return item as MethodDefinition;
        }

        /// <summary>
        /// Retrieve from cache or search an assembly name reference from user program' object (assembly reference).
        /// </summary>
        /// <param name="item">object (Assembly reference, ...)</param>
        /// <returns>Assembly Name Reference or null if not found</returns>
        public AssemblyNameReference GetAssemblyNameReference(object item)
        {
            return item as AssemblyNameReference;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CecilStudioAssemblyContext()
            : this(null)
        {
        }

        /// <summary>
        /// Constructgor
        /// </summary>
        /// <param name="assembly">assembly definition</param>
        public CecilStudioAssemblyContext(AssemblyDefinition assembly)
        {
            this.m_adef = assembly;
        }
        #endregion

    }
}
