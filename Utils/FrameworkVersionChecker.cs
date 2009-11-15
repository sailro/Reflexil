/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using Microsoft.Win32;
using System;
#endregion

namespace Reflexil.Utils
{
    public enum FrameworkVersions
    {
        v1_1_4322,
        v2_0_50727,
        v3_0,
        v3_5,
    }

    /// <summary>
    /// Check .NET Framework Version
    /// </summary>
    public static class FrameworkVersionChecker
    {
        const string REG_LOCATION = @"SOFTWARE\Microsoft\NET Framework Setup\NDP";

        /// <summary>
        /// Check if a specific .NET Framework version is installed.
        /// </summary>
        /// <param name="version">version to test</param>
        /// <returns>True if installed</returns>
        public static bool IsVersionInstalled(FrameworkVersions version)
        {
            try
            {
                RegistryKey masterKey = Registry.LocalMachine.OpenSubKey(REG_LOCATION);

                if (masterKey != null)
                {
                    string[] SubKeyNames = masterKey.GetSubKeyNames();
                    foreach (string ver in SubKeyNames)
                    {
                        if (ver.ToLower().Replace(".", "_") == version.ToString())
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception) { };
            return false;
        }
    }
}