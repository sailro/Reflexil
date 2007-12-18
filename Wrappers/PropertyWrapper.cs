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
using System;
using System.Collections.Generic;
using System.Reflection;
#endregion

namespace Reflexil.Wrappers
{
    /// <summary>
    /// Property wrapper
    /// </summary>
	class PropertyWrapper
    {

        #region " Fields "
        private PropertyInfo m_pinfo;
        private Dictionary<String, String> m_prefixes;
        #endregion

        #region " Properties "
        public PropertyInfo PropertyInfo
        {
            get
            {
                return m_pinfo;
            }
        }

        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinfo">Property info</param>
        /// <param name="prefixes">Prefixes used tio categorize properties</param>
        public PropertyWrapper(PropertyInfo pinfo, Dictionary<String, String> prefixes)
        {
            m_pinfo = pinfo;
            m_prefixes = prefixes; 
        }

        /// <summary>
        /// Returns a String that represents the property
        /// </summary>
        /// <returns>A String like category: name</returns>
        public override string ToString()
        {
            string result = m_pinfo.Name;
            if (m_prefixes.ContainsKey(result))
            {
                result = String.Format("{0}: {1}", m_prefixes[result], result);
            }
            return result;
        }
        #endregion

	}
}
