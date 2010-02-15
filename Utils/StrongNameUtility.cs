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
                return SdkUtility.Locate(SN_FILENAME);
            }
        }
        #endregion

        #region " Methods "
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
