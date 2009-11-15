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
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Wrapper for sn.exe SDK utility
    /// </summary>
	static class StrongNameUtility
    {

        #region " Constants "
        const string SN_FILENAME = "sn.exe";
        const string PATH_ENV_VAR = "PATH";
        readonly static string[] SDK_PATH_REGKEYS = {@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework", @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.0A"};
        readonly static string[] SDK_PATH_REGVALUES = { "sdkInstallRootv2.0", "InstallationFolder" };
        const string SDK_BIN_PATH = "Bin";
        #endregion

        #region " Properties "
        public static bool StrongNameToolPresent
        {
            get
            {
                return File.Exists(StrongNameToolFilename);
            }
        }

        private static string StrongNameToolFilename
        {
            get
            {
                string executable = Path.Combine(PreparePath(Path.GetDirectoryName(typeof(StrongNameUtility).Assembly.Location)), SN_FILENAME);
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
                    if (File.Exists(Path.Combine(PreparePath(item), SN_FILENAME)))
                    {
                        executable = Path.Combine(PreparePath(item), SN_FILENAME);
                        break;
                    }
                }

                int regindex = 0;
                while ((executable == null) && (regindex < SDK_PATH_REGKEYS.Length))
                {
                    executable = TryGetPathFromRegistry(SDK_PATH_REGKEYS[regindex], SDK_PATH_REGVALUES[regindex]);
                    regindex++;
                }

                return (executable == null) ? string.Empty : executable;
            }
        }
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
                    input = input.Replace(ch.ToString(),String.Empty);
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
        private static string TryGetPathFromRegistry(string regkey, string regvalue)
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
            executable = Path.Combine(PreparePath(executable), SN_FILENAME);
            return executable;
        }

        /// <summary>
        /// Call sn.exe SDK utility
        /// </summary>
        /// <param name="arguments">Program arguments </param>
        /// <param name="show">Show utility window</param>
        /// <returns>True if successfull</returns>
        private static bool CallStrongNameUtility(string arguments, bool show)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(StrongNameToolFilename, arguments);
                startInfo.CreateNoWindow = !show;
                startInfo.UseShellExecute = false;
                Process snProcess = Process.Start(startInfo);
                snProcess.WaitForExit();
                return snProcess.ExitCode == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Resign an assembly with a valid key
        /// </summary>
        /// <param name="assemblyfile">Assembly location</param>
        /// <param name="keyfile">Keyfile</param>
        /// <param name="show">Show utility window</param>
        /// <returns>True if successfull</returns>
        public static bool Resign(string assemblyfile, string keyfile, bool show)
        {
            return CallStrongNameUtility(string.Format("-R \"{0}\" \"{1}\"", assemblyfile, keyfile), show);
        }

        /// <summary>
        ///Register an assembly for verification skipping 
        /// </summary>
        /// <param name="assemblyfile">Assembly location</param>
        /// <returns>True if successfull</returns>
        public static bool RegisterForVerificationSkipping(string assemblyfile)
        {
            return CallStrongNameUtility(string.Format("-Vr \"{0}\"", assemblyfile), false);
        }
        #endregion

	}
}
