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
    public abstract class BasePackage : IPackage 
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

        public ReflexilWindow ReflexilWindow
        {
            get { return reflexilwindow; }
        }

        public IHandler ActiveHandler
        {
            get { return activehandler; }
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

        /// <summary>
        /// Reload the current assembly
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        protected virtual void ReloadAssembly(object sender, EventArgs e)
        {
            AssemblyHelper.ReloadAssembly(GetCurrentModuleOriginalLocation());
            IHandler handler = PluginFactory.GetInstance().Package.ActiveHandler;
            if (handler != null && handler.IsItemHandled(ActiveItem))
            {
                handler.HandleItem(ActiveItem);
            }
        }

        /// <summary>
        /// Rename the current member
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        protected virtual void RenameMember(object sender, EventArgs e)
        {
            IHandler handler = PluginFactory.GetInstance().Package.ActiveHandler;
            if (handler != null && handler.TargetObject != null)
            {
                using (RenameForm frm = new RenameForm()) {
                    if (frm.ShowDialog(handler.TargetObject) == DialogResult.OK)
                    {
                        OnItemRenamed();
                    }
                }
            }
        }

        /// <summary>
        /// Delete the current member
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        protected virtual void DeleteMember(object sender, EventArgs e)
        {
            IHandler handler = PluginFactory.GetInstance().Package.ActiveHandler;
            if (handler != null && handler.TargetObject != null)
            {
                DeleteHelper.Delete(handler.TargetObject);
                OnItemDeleted();
            }
        }

        /// <summary>
        /// When an item is injected
        /// </summary>
        protected virtual void OnItemInjected()
        {
            DisplayWarning();
            ActiveItemChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// When an item is deleted
        /// </summary>
        protected virtual void OnItemDeleted()
        {
            DisplayWarning();
            ActiveItemChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// When an item is renamed
        /// </summary>
        protected virtual void OnItemRenamed()
        {
            DisplayWarning();
            ActiveItemChanged(this, EventArgs.Empty);
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Display a warning about synchronization loss between host application and Reflexil,
        /// after making major changes like inject/rename/delate.
        /// </summary>
        protected virtual void DisplayWarning()
        {
            if (Settings.Default.DisplayWarning)
            {
                using (SyncWarningForm frm = new SyncWarningForm())
                {
                    frm.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Retrieve current assembly definition.
        /// The active handler must return an Assembly/Module definition
        /// </summary>
        /// <returns>Assemlbly definition</returns>
        protected virtual AssemblyDefinition GetCurrentAssemblyDefinition()
        {
            IHandler handler = PluginFactory.GetInstance().Package.ActiveHandler;
            if (handler != null)
            {
                if (handler.TargetObject is AssemblyDefinition)
                {
                    return handler.TargetObject as AssemblyDefinition;
                }
                else if (handler.TargetObject is ModuleDefinition)
                {
                    return (handler.TargetObject as ModuleDefinition).Assembly;
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieve original location of the current module
        /// </summary>
        /// <returns>path</returns>
        protected virtual string GetCurrentModuleOriginalLocation()
        {
            IHandler handler = PluginFactory.GetInstance().Package.ActiveHandler;
            if (handler != null)
            {
                if (handler.TargetObject is AssemblyDefinition)
                {
                    return (handler.TargetObject as AssemblyDefinition).MainModule.Image.FileInformation.FullName;
                }
                else if (handler.TargetObject is ModuleDefinition)
                {
                    return (handler.TargetObject as ModuleDefinition).Image.FileInformation.FullName;
                }
            }

            return null;
        }

        /// <summary>
        /// Generate an ID
        /// </summary>
        /// <param name="id">ID suffix</param>
        /// <returns>String ID</returns>
        protected virtual string GenerateId(string id)
        {
            return string.Concat("Reflexil.", id);
        }

        /// <summary>
        /// Inject a specific item
        /// </summary>
        /// <param name="type">item type to inject</param>
        protected virtual void Inject(EInjectType type)
        {
            using (InjectForm frm = new InjectForm())
            {
                if (frm.ShowDialog(type) == DialogResult.OK)
                {
                    OnItemInjected();
                }
            }
        }

        /// <summary>
        /// Display a message
        /// </summary>
        /// <param name="message">message to display</param>
        public abstract void ShowMessage(string message);

        /// <summary>
        /// Check framework prerequisites
        /// </summary>
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


