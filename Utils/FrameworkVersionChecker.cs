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
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
#endregion

namespace Reflexil.Utils
{
    public enum FrameworkVersions
    {
        v1_1_4322,
        v2_0_50727,
        v3_0,
        v3_5,
        Mono_2_4
    }

    /// <summary>
    /// Check .NET Framework Version
    /// </summary>
    public static class FrameworkVersionChecker
    {

        #region " Constants "
        const string REG_LOCATION = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP";
        #endregion

        #region " Fields "
        private static string _monoVersion = null;
        #endregion

        #region " Methods "
        /// <summary>
        /// Get the version of Mono 
        /// </summary>
        public static string MonoVersion
        {
            get
            {
                if (_monoVersion == null)
                {
                    var t = Type.GetType("Mono.Runtime");
                    if (t != null)
                    {
                        var method = t.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
                        _monoVersion = (string)method.Invoke(t, null);
                    }
                }
                return _monoVersion;
            }
        }

        /// <summary>
        /// Check if a specific .NET Framework version is installed.
        /// </summary>
        /// <param name="version">version to test</param>
        /// <returns>True if installed</returns>
        public static bool IsVersionInstalled(FrameworkVersions version)
        {
            try
            {
                switch (version)
                {
                    case FrameworkVersions.Mono_2_4:

                        if (!string.IsNullOrEmpty(MonoVersion))
                        {
                            Regex regex = new Regex(@"^Mono (?<major>\d+)\.(?<minor>\d+)(\..*)?$");
                            if (regex.IsMatch(MonoVersion))
                            {
                                string[] items = regex.Split(MonoVersion);

                                int major = Convert.ToInt32(items[regex.GroupNumberFromName("major")]);
                                int minor = Convert.ToInt32(items[regex.GroupNumberFromName("minor")]);

                                return (major == 2 && minor >= 4) || (major >= 3);
                            }
                        }
                        break;
                    default:
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
                        break;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        #endregion

    }
}
