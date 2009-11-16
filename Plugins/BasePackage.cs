/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using Reflexil.Forms;
using Reflexil.Properties;
using Mono.Cecil;
using System.Windows.Forms;
using Reflexil.Handlers;
using Reflexil.Plugins;
using Reflexil.Utils;
using System.Collections;
#endregion

namespace Reflexil.Plugins
{
    /// <summary>
    /// Base for addin entry point
    /// </summary>
    public abstract class BasePackage
    {

        #region " Constants "
        protected readonly string REFLEXIL_WINDOW_TEXT = string.Format("Sebastien LEBRETON's Reflexil v{0}", typeof(BasePackage).Assembly.GetName().Version.ToString(2));
        protected readonly string REFLEXIL_BUTTON_TEXT = string.Format("Reflexil v{0}", typeof(BasePackage).Assembly.GetName().Version.ToString(2));
        protected const string REFLEXIL_WINDOW_ID = "Reflexil.Window";
        #endregion

        #region " Fields "
        protected ReflexilWindow reflexilwindow;
        protected IHandler activehandler;
        #endregion

        #region " Properties "
        public abstract ICollection Assemblies
        {
            get;
        }

        public abstract object ActiveItem
        {
            get;
        }
        #endregion

        #region " Events "
        /// <summary>
        /// 'Reflexil' button click 
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        protected abstract void Button_Click(object sender, EventArgs e);

        /// <summary>
        /// Browser active item changed 
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        protected virtual void ActiveItemChanged(object sender, EventArgs e)
        {
            activehandler = reflexilwindow.HandleItem(ActiveItem);
        }

        /// <summary>
        /// Assembly loaded
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        protected virtual void AssemblyLoaded(object sender, EventArgs e)
        {
            PluginFactory.GetInstance().ReloadAssemblies(Assemblies);
        }

        /// <summary>
        /// Assembly unloaded
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        protected virtual void AssemblyUnloaded(object sender, EventArgs e)
        {
            PluginFactory.GetInstance().ReloadAssemblies(Assemblies);
            PluginFactory.GetInstance().SynchronizeAssemblyContexts(Assemblies);
        }
        #endregion

        #region " Methods "
        public abstract void ShowMessage(string message);

        public void CheckFrameWorkVersion()
        {
            if (!FrameworkVersionChecker.IsVersionInstalled(FrameworkVersions.v3_5) && !FrameworkVersionChecker.IsVersionInstalled(FrameworkVersions.Mono_2_4))
            {
                ShowMessage("Warning, Reflexil is unable to locate .NET Framework 3.5 or Mono 2.4! This is required!");
            }
        }
        #endregion

    }
}


