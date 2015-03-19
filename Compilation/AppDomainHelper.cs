/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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

using System;
using System.IO;
using System.Reflection;

#endregion

namespace Reflexil.Compilation
{
	/// <summary>
	/// Helper class for AppDomain isolation
	/// </summary>
	public class AppDomainHelper
	{
		#region Methods

		/// <summary>
		/// Create a new AppDomain with the same ApplicationBase as the current assemby
		/// </summary>
		/// <returns>new AppDomain</returns>
		public static AppDomain CreateAppDomain()
		{
			var setup = new AppDomainSetup
			{
				ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				ApplicationName = typeof (AppDomainHelper).Name + DateTime.Now.Ticks
			};
			return AppDomain.CreateDomain(setup.ApplicationName, AppDomain.CurrentDomain.Evidence, setup);
		}

		/// <summary>
		/// Create a new Compiler class proxy with a specific AppDomain 
		/// </summary>
		/// <param name="target">AppDomain for proxy creation</param>
		/// <returns>new Compiler proxy</returns>
		public static Compiler CreateCompilerInstanceAndUnwrap(AppDomain target)
		{
			return
				target.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().GetName().Name, typeof (Compiler).FullName) as
					Compiler;
		}

		/// <summary>
		/// Check that AppDomain isolation is correctly used
		/// </summary>
		public static void CheckAppDomain()
		{
			if (!AppDomain.CurrentDomain.FriendlyName.StartsWith(typeof (AppDomainHelper).Name))
			{
				throw new InvalidOperationException(
					"Do not use this class directly, call AppDomainHelper.CreateCompilerInstanceAndUnwrap to get a proxy!");
			}
		}

		#endregion
	}
}