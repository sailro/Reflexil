/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
#endregion

namespace Reflexil.Compilation
{
    /// <summary>
    /// Helper class for AppDomain isolation
    /// </summary>
	public class AppDomainHelper
    {

        #region " Methods "
        /// <summary>
        /// Create a new AppDomain with the same ApplicationBase as the current assemby
        /// </summary>
        /// <returns>new AppDomain</returns>
        public static AppDomain CreateAppDomain()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            setup.ApplicationName = typeof(AppDomainHelper).Name + DateTime.Now.Ticks.ToString();
            return AppDomain.CreateDomain(setup.ApplicationName, AppDomain.CurrentDomain.Evidence, setup);
        }

        /// <summary>
        /// Create a new Compiler class proxy with a specific AppDomain 
        /// </summary>
        /// <param name="target">AppDomain for proxy creation</param>
        /// <returns>new Compiler proxy</returns>
        public static Compiler CreateCompilerInstanceAndUnwrap(AppDomain target)
        {
            return target.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().GetName().Name, typeof(Compiler).FullName) as Compiler;
        }

        /// <summary>
        /// Check that AppDomain isolation is correctly used
        /// </summary>
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
