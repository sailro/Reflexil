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
using System;
using System.IO;
using Microsoft.Win32;
#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Locate SDK utilities
    /// </summary>
	public static class SdkUtility
	{
        #region " Constants "
        const string PATH_ENV_VAR = "PATH";
        readonly static string[] SDK_PATH_REGKEYS = { @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework", @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.1", @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.0A" };
        readonly static string[] SDK_PATH_REGVALUES = { "sdkInstallRootv2.0", "InstallationFolder", "InstallationFolder" };
        const string SDK_BIN_PATH = "Bin";
        #endregion

        #region " Methods "
        /// <summary>
        /// Remove all invalid chars from a pathname
        /// </summary>
        /// <param name="input">path to parse</param>
        /// <returns>corrected path</returns>
        private static string PreparePath(string input)
        {
            if (input != null)
            {
                foreach (char ch in Path.GetInvalidPathChars())
                {
                    input = input.Replace(ch.ToString(), String.Empty);
                }
            }
            return input;
        }

        /// <summary>
        /// Try to retrieve a valid path from registry
        /// </summary>
        /// <param name="regkey">registry key</param>
        /// <param name="regvalue">registry value</param>
        /// <returns></returns>
        private static string TryGetPathFromRegistry(string regkey, string regvalue, string utilityfilename)
        {
            string executable = string.Empty;
            try
            {
                executable = Registry.GetValue(regkey, regvalue, string.Empty).ToString();
                executable = Path.Combine(PreparePath(executable), SDK_BIN_PATH);
                if (!Directory.Exists(executable))
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            executable = Path.Combine(PreparePath(executable), utilityfilename);
            return executable;
        }

        /// <summary>
        /// Retrieve an utility
        /// </summary>
        /// <param name="utilityfilename">base filename</param>
        /// <returns>empty if not found</returns>
        public static string Locate(string utilityfilename)
        {
            string executable = Path.Combine(PreparePath(Path.GetDirectoryName(typeof(SdkUtility).Assembly.Location)), utilityfilename);
            if (!File.Exists(executable))
            {
                executable = null;
            }
            else
            {
                return executable;
            }


            string path = System.Environment.GetEnvironmentVariable(PATH_ENV_VAR);
            foreach (string item in path.Split(Path.PathSeparator))
            {
                if (File.Exists(Path.Combine(PreparePath(item), utilityfilename)))
                {
                    executable = Path.Combine(PreparePath(item), utilityfilename);
                    break;
                }
            }

            int regindex = 0;
            while ((executable == null) && (regindex < SDK_PATH_REGKEYS.Length))
            {
                executable = TryGetPathFromRegistry(SDK_PATH_REGKEYS[regindex], SDK_PATH_REGVALUES[regindex], utilityfilename);
                regindex++;
            }

            return (executable == null) ? string.Empty : executable;
        }
        #endregion

	}
}
