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
using System.Threading;
#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Wrapper for peverify.exe SDK utility
    /// </summary>
	static class PEVerifyUtility
    {

        #region " Constants "
        const string PV_FILENAME = "peverify.exe";
        #endregion

        #region " Properties "
        public static bool PEVerifyToolPresent
        {
            get
            {
                return File.Exists(PEVerifyToolFilename);
            }
        }

        private static string PEVerifyToolFilename
        {
            get
            {
                return SdkUtility.Locate(PV_FILENAME);
            }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Call peverify.exe SDK utility
        /// </summary>
        /// <param name="arguments">Program arguments </param>
        /// <param name="show">Show utility window</param>
        /// <returns>True if successfull</returns>
        public static bool CallPEVerifyUtility(string arguments, bool show, Action<TextReader> outputhandler)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(PEVerifyToolFilename, arguments);
                startInfo.CreateNoWindow = !show;
                startInfo.RedirectStandardOutput = outputhandler != null;
                startInfo.UseShellExecute = false;
                Process pvProcess = Process.Start(startInfo);

                String lines = String.Empty;
                ThreadPool.QueueUserWorkItem((state) => lines = pvProcess.StandardOutput.ReadToEnd());

                pvProcess.WaitForExit();
                if (outputhandler != null)
                {
                    outputhandler(new StringReader(lines));
                }
                return pvProcess.ExitCode == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

	}
}
