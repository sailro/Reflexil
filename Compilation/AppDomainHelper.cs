
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
#endregion

namespace Reflexil.Compilation
{
	class AppDomainHelper
    {

        #region " Methods "
        public static AppDomain CreateAppDomain()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            setup.ApplicationName = typeof(AppDomainHelper).Name + DateTime.Now.Ticks.ToString();
            return AppDomain.CreateDomain(setup.ApplicationName, AppDomain.CurrentDomain.Evidence, setup);
        }

        public static Compiler CreateCompilerInstanceAndUnwrap(AppDomain target)
        {
            return target.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().GetName().Name, typeof(Compiler).FullName) as Compiler;
        }

        public static void CheckAppDomain()
        {
            if (!AppDomain.CurrentDomain.FriendlyName.StartsWith(typeof(AppDomainHelper).Name))
            {
                throw new InvalidOperationException("Do not use this class directly, call AppDomainHelper.CreateCompilerInstanceAndUnwrap to get a proxy!");
            }
        }
        #endregion

	}
}
