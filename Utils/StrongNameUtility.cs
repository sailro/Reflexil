
#region " Imports "
using System;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
#endregion

namespace Reflexil.Utils
{
	static class StrongNameUtility
    {

        #region " Properties "
        public static string ExecutablePath
        {
            get
            {
                string snpath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework", "sdkInstallRootv2.0", string.Empty).ToString();
                snpath = Path.Combine(snpath, "Bin");
                if (!Directory.Exists(snpath))
                {
                   snpath = string.Empty;
                }
                snpath = Path.Combine(snpath, "sn.exe");
                return snpath;
            }
        }
        #endregion

        #region " Methods "
        private static bool CallStrongNameUtility(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(ExecutablePath, arguments);
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            Process snProcess = Process.Start(startInfo);
            snProcess.WaitForExit();
            return snProcess.ExitCode == 0;
        }

        public static bool Resign(string assemblyfile, string keyfile)
        {
            return CallStrongNameUtility(string.Format("-R \"{0}\" \"{1}\"", assemblyfile, keyfile));
        }

        public static bool RegisterForVerificationSkipping(string assemblyfile)
        {
            return CallStrongNameUtility(string.Format("-Vr \"{0}\"", assemblyfile));
        }
        #endregion

	}
}
