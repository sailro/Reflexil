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
#endregion

namespace Reflexil.Plugins
{
	public class PluginFactory
    {

        #region " Fields "
        private static IPlugin m_instance;
        #endregion

        #region " Methods "
        public static IPlugin GetInstance()
        {
            return m_instance;
        }

        public static void Register(IPlugin plugin) {
            if (m_instance != null) {
                throw new InvalidOperationException("A plugin is already registered");
            }
            m_instance = plugin;
        }
        #endregion

    }
}
