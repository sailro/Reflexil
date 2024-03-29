﻿/* Reflexil Copyright (c) 2007-2021 Sebastien Lebreton

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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace Reflexil.Utils
{
	public static class SdkUtility
	{
		private const string PathEnvVar = "PATH";

		private static readonly Dictionary<string, string> SdkRegistry = new Dictionary<string, string>
		{
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework", "sdkInstallRootv2.0"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.1", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.0A", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.1A", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v8.0A", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v8.1A", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\Windows\v8.1A\WinSDK-NetFx40Tools", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\NETFXSDK\4.6\WinSDK-NetFx40Tools", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\NETFXSDK\4.6.1\WinSDK-NetFx40Tools", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\NETFXSDK\4.6.2\WinSDK-NetFx40Tools", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\NETFXSDK\4.7\WinSDK-NetFx40Tools", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\NETFXSDK\4.7.1\WinSDK-NetFx40Tools", "InstallationFolder"},
			{@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\NETFXSDK\4.7.2\WinSDK-NetFx40Tools", "InstallationFolder"},
		};

		private const string SdkBinPath = "Bin";

		private static string PreparePath(string input)
		{
			return input == null
				? null
				: Path.GetInvalidPathChars()
					.Aggregate(input, (current, ch) => current.Replace(ch.ToString(CultureInfo.InvariantCulture), string.Empty));
		}

		private static string TryGetPathFromRegistry(string regkey, string regvalue, string utilityfilename)
		{
			string executable;
			try
			{
				executable = Registry.GetValue(regkey, regvalue, string.Empty) as string;
				if (executable == null)
					return null;

				var executableBinDir = Path.Combine(PreparePath(executable), SdkBinPath);
				if (Directory.Exists(executableBinDir))
					executable = executableBinDir;
			}
			catch (Exception)
			{
				return null;
			}

			executable = Path.Combine(PreparePath(executable), utilityfilename);
			return !File.Exists(executable) ? null : executable;
		}

		public static string Locate(string utilityfilename)
		{
			var executable = Path.Combine(PreparePath(Path.GetDirectoryName(typeof(SdkUtility).Assembly.Location)), utilityfilename);
			if (File.Exists(executable))
				return executable;

			var path = Environment.GetEnvironmentVariable(PathEnvVar);
			if (path != null)
			{
				foreach (var item in path.Split(Path.PathSeparator).Where(item => File.Exists(Path.Combine(PreparePath(item), utilityfilename))))
				{
					executable = Path.Combine(PreparePath(item), utilityfilename);
					return executable;
				}
			}

			foreach (var keyPair in SdkRegistry)
			{
				executable = TryGetPathFromRegistry(keyPair.Key, keyPair.Value, utilityfilename);
				if (!string.IsNullOrEmpty(executable))
					return executable;
			}

			return string.Empty;
		}
	}
}
