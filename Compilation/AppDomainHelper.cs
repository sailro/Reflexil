/* Reflexil Copyright (c) 2007-2016 Sebastien LEBRETON

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
using System.IO;
using System.Reflection;

namespace Reflexil.Compilation
{
	public class AppDomainHelper
	{
		public static AppDomain CreateAppDomain()
		{
			var setup = new AppDomainSetup
			{
				ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				ApplicationName = typeof(AppDomainHelper).Name + DateTime.Now.Ticks
			};
			return AppDomain.CreateDomain(setup.ApplicationName, AppDomain.CurrentDomain.Evidence, setup);
		}

		public static Compiler CreateCompilerInstanceAndUnwrap(AppDomain target)
		{
			return target.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().GetName().Name, typeof(Compiler).FullName) as Compiler;
		}

		public static void CheckAppDomain()
		{
			if (!AppDomain.CurrentDomain.FriendlyName.StartsWith(typeof(AppDomainHelper).Name))
				throw new InvalidOperationException("Do not use this class directly, call AppDomainHelper.CreateCompilerInstanceAndUnwrap to get a proxy!");
		}
	}
}