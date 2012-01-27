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
        readonly static string[] SDK_PATH_REGKEYS = { 
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework",
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.1",
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.0A",
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A" };
        readonly static string[] SDK_PATH_REGVALUES = { 
            "sdkInstallRootv2.0", 
            "InstallationFolder", 
            "InstallationFolder",
            "InstallationFolder" };
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
