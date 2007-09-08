
#region " Imports "
using System;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Reflection;
#endregion

namespace Reflexil.Utils
{
	static class StrongNameUtility
    {

        #region " Constants "
        const string SN_FILENAME = "sn.exe";
        const string PATH_ENV_VAR = "PATH";
        const string SDK_PATH_REGKEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework";
        const string SDK_PATH_REGVALUE = "sdkInstallRootv2.0";
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

                if (executable == null)
                {
                    try
                    {
                        executable = Registry.GetValue(SDK_PATH_REGKEY, SDK_PATH_REGVALUE, string.Empty).ToString();
                        executable = Path.Combine(PreparePath(executable), SDK_BIN_PATH);
                        if (!Directory.Exists(executable))
                        {
                            executable = string.Empty;
                        }
                    }
                    catch (Exception)
                    {
                        executable = string.Empty;
                    }
                    executable = Path.Combine(PreparePath(executable), SN_FILENAME);
                }

                return executable;
            }
        }
        #endregion

        #region " Methods "
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

        public static bool Resign(string assemblyfile, string keyfile, bool show)
        {
            return CallStrongNameUtility(string.Format("-R \"{0}\" \"{1}\"", assemblyfile, keyfile), show);
        }

        public static bool RegisterForVerificationSkipping(string assemblyfile)
        {
            return CallStrongNameUtility(string.Format("-Vr \"{0}\"", assemblyfile), false);
        }
        #endregion

	}
}
