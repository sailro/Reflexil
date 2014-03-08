/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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

#region Imports

using System.Linq;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
#endregion

namespace Reflexil.Utils
{
    public enum FrameworkVersions
    {
		// ReSharper disable InconsistentNaming
        v1_1_4322,
        v2_0_50727,
        v3_0,
        v3_5,
        Mono_2_4
		// ReSharper restore InconsistentNaming
	}

    /// <summary>
    /// Check .NET Framework Version
    /// </summary>
    public static class FrameworkVersionChecker
    {

        #region Constants
        const string RegLocation = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP";
        #endregion

        #region Fields
        private static string _monoVersion;
        #endregion

        #region Methods
        /// <summary>
        /// Get the version of Mono 
        /// </summary>
        public static string MonoVersion
        {
            get
            {
	            if (_monoVersion != null) 
					return _monoVersion;
	            
				var t = Type.GetType("Mono.Runtime");
	            if (t == null)
					return _monoVersion;
	            
				var method = t.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
	            _monoVersion = (string)method.Invoke(t, null);
	            
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
                            var regex = new Regex(@"^Mono (?<major>\d+)\.(?<minor>\d+)(\..*)?$");
                            if (regex.IsMatch(MonoVersion))
                            {
                                var items = regex.Split(MonoVersion);

                                var major = Convert.ToInt32(items[regex.GroupNumberFromName("major")]);
                                var minor = Convert.ToInt32(items[regex.GroupNumberFromName("minor")]);

                                return (major == 2 && minor >= 4) || (major >= 3);
                            }
                        }
                        break;
                    default:
                        var masterKey = Registry.LocalMachine.OpenSubKey(RegLocation);

                        if (masterKey != null)
                        {
	                        var subKeyNames = masterKey.GetSubKeyNames();
	                        if (subKeyNames.Any(ver => ver.ToLower().Replace(".", "_") == version.ToString()))
		                        return true;
                        }
		                break;
                }
            }
            catch (Exception)
            {
				return false;
			}

            return false;
        }
        #endregion

    }
}
