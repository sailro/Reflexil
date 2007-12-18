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

namespace Reflexil.Wrappers
{
    /// <summary>
    /// Namespace wrapper
    /// </summary>
	class NamespaceWrapper
    {

        #region " Fields "
        private string m_namespace;
        private ModuleDefinition m_modef;
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="modef">Module definition</param>
        /// <param name="namespace">Namespace to wrap</param>
        public NamespaceWrapper(ModuleDefinition modef, string @namespace)
        {
            m_modef = modef;
            if (string.IsNullOrEmpty(@namespace))
            {
                m_namespace = "-";
            }
            else
            {
                m_namespace = @namespace;
            }
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
                NamespaceWrapper other = obj as NamespaceWrapper;
                return (m_modef.Equals(other.m_modef)) && (m_namespace == other.m_namespace);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash based on the module hash and the namespace hash</returns>
        public override int GetHashCode()
        {
            return (m_modef.GetHashCode().ToString() + "|" + m_namespace.GetHashCode().ToString()).GetHashCode();
        }
        
        /// <summary>
        /// Returns the namespace
        /// </summary>
        /// <returns>Namespace</returns>
        public override string ToString()
        {
            return m_namespace;
        }
        #endregion

	}
}
