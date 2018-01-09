/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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
using System.Diagnostics;
using System.IO;

namespace Reflexil.Utils
{
	internal static class StrongNameUtility
	{
		private const string SnFilename = "sn.exe";

		public static bool StrongNameToolPresent
		{
			get { return File.Exists(StrongNameToolFilename); }
		}

		private static string StrongNameToolFilename
		{
			get { return SdkUtility.Locate(SnFilename); }
		}

		private static bool CallStrongNameUtility(string arguments, bool show)
		{
			try
			{
				var startInfo = new ProcessStartInfo(StrongNameToolFilename, arguments)
				{
					CreateNoWindow = !show,
					UseShellExecute = false
				};
				var process = Process.Start(startInfo);
				if (process == null)
					return false;

				process.WaitForExit();
				return process.ExitCode == 0;
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
	}
}