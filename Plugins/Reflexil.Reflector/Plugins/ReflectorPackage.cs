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
using System.Collections.Generic;
using System.Collections;
using Reflector;
using Reflector.CodeModel;
using Mono.Cecil;
using Reflexil.Forms;
using System.Windows.Forms;
using Reflexil.Utils;
using System.Drawing;
using Reflexil.Handlers;
#endregion

namespace Reflexil.Plugins.Reflector
{
	/// <summary>
	/// Addin entry point
	/// </summary>
    public partial class ReflectorPackage : BasePackage, global::Reflector.IPackage
	{
		
		#region " Constants "
        const string REFLECTOR_TOOLS_ID = "Tools";
        const string REFLECTOR_TYPEDEC_ID = "Browser.TypeDeclaration";
        const string REFLECTOR_ASSEMBLY_ID = "Browser.Assembly";
        const string REFLECTOR_MODULE_ID = "Browser.Module";
        const string REFLEXIL_TYPEDEC_MENU_ID = "Reflexil.TypeDeclaration.Menu";
        const string REFLEXIL_ASSEMBLY_MENU_ID = "Reflexil.Assembly.Menu";
        const string REFLEXIL_MODULE_MENU_ID = "Reflexil.Module.Menu";
        #endregion
		
		#region " Fields "
		private IWindowManager wm;
		private IAssemblyBrowser ab;
		private ICommandBarManager cbm;
		private IAssemblyManager am;
		private IServiceProvider sp;
        private List<UIContext> items;
		#endregion

        #region " Properties "
        public override ICollection Assemblies
        {
            get { return am.Assemblies; }
        }

        public override object ActiveItem
        {
            get { return ab.ActiveItem; }
        }
        #endregion

        #region " Events "
        /// <summary>
        /// 'Reflexil' button click 
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
		protected override void Button_Click(object sender, EventArgs e)
		{
			wm.Windows[REFLEXIL_WINDOW_ID].Visible = true;
		}
        #endregion

		#region " Methods "
        /// <summary>
        /// Display a message
        /// </summary>
        /// <param name="message">message to display</param>
        public override void ShowMessage(string message)
        {
            wm.ShowMessage(message);
        }

        /// <summary>
        /// Helper method
        /// </summary>
        /// <typeparam name="T">Reflector service interface</typeparam>
        /// <returns>Reflector service implementation</returns>
		public T GetService<T>()
		{
			return ((T) (sp.GetService(typeof(T))));
		}

        private void Inject(EInjectType type)
        {
            using (InjectForm frm = new InjectForm())
            {
                frm.ShowDialog(type);
            }
        }

        private void ReloadAssembly(object sender, EventArgs e)
        {
            AssemblyHelper.ReloadAssembly(GetCurrentModuleOriginalLocation());
            IHandler handler = PluginFactory.GetInstance().Package.ActiveHandler;
            if (handler != null && handler.IsItemHandled(ab.ActiveItem))
            {
                handler.HandleItem(ab.ActiveItem);
            }
        }

        private AssemblyDefinition GetCurrentAssemblyDefinition()
        {
            IAssembly assembly = null;
            if (ab.ActiveItem is IAssembly)
            {
                assembly = ab.ActiveItem as IAssembly;
            }
            else if (ab.ActiveItem is IModule)
            {
                assembly = (ab.ActiveItem as IModule).Assembly;
            }
            return PluginFactory.GetInstance().GetAssemblyDefinition(assembly);   
        }

        private string GetCurrentModuleOriginalLocation()
        {
            IModule module = null;
            if (ab.ActiveItem is IAssembly)
            {
                return Environment.ExpandEnvironmentVariables((ab.ActiveItem as IAssembly).Location);
            }
            else if (ab.ActiveItem is IModule)
            {
                module = ab.ActiveItem as IModule;
            }
            return PluginFactory.GetInstance().GetModuleLocation(module);
        }


        /// <summary>
        /// Addin load method
        /// </summary>
        /// <param name="serviceProvider">Reflector service provider</param>
		public void Load(System.IServiceProvider serviceProvider)
		{
            PluginFactory.Register(new ReflectorPlugin(this));

			sp = serviceProvider;
			wm = GetService<IWindowManager>();
			ab = GetService<IAssemblyBrowser>();
			cbm = GetService<ICommandBarManager>();
			am = GetService<IAssemblyManager>();

            CheckFrameWorkVersion();

            // Main Window
            items = new List<UIContext>();
            reflexilwindow = new Reflexil.Forms.ReflexilWindow();
            wm.Windows.Add(REFLEXIL_WINDOW_ID, reflexilwindow, REFLEXIL_WINDOW_TEXT);
			
            // Main button
            items.Add(new ButtonUIContext(cbm.CommandBars[REFLECTOR_TOOLS_ID]));
            items.Add(new ButtonUIContext(cbm.CommandBars[REFLECTOR_TOOLS_ID], REFLEXIL_BUTTON_TEXT, Button_Click, BasePlugin.ReflexilImage));

            using (ImageList browserimages = new ImageList())
            {
                browserimages.Images.AddStrip(PluginFactory.GetInstance().GetAllBrowserImages());
                browserimages.TransparentColor = Color.Green;

                using (ImageList barimages = new ImageList())
                {
                    barimages.Images.AddStrip(PluginFactory.GetInstance().GetAllBarImages());

                    // Type declaration menu
                    items.Add(new MenuUIContext(cbm.CommandBars[REFLECTOR_TYPEDEC_ID]));
                    var tmenu = new MenuUIContext(cbm.CommandBars[REFLECTOR_TYPEDEC_ID], REFLEXIL_TYPEDEC_MENU_ID, REFLEXIL_BUTTON_TEXT, BasePlugin.ReflexilImage);
                    items.Add(new SubMenuUIContext(tmenu, "Inject inner class", (sender, e) => Inject(EInjectType.Class), browserimages.Images[(int)EBrowserImages.PublicClass]));
                    items.Add(new SubMenuUIContext(tmenu, "Inject inner interface", (sender, e) => Inject(EInjectType.Interface), browserimages.Images[(int)EBrowserImages.PublicInterface]));
                    items.Add(new SubMenuUIContext(tmenu, "Inject inner struct", (sender, e) => Inject(EInjectType.Struct), browserimages.Images[(int)EBrowserImages.PublicStructure]));
                    items.Add(new SubMenuUIContext(tmenu, "Inject inner enum", (sender, e) => Inject(EInjectType.Enum), browserimages.Images[(int)EBrowserImages.PublicEnum]));
                    items.Add(new SubMenuUIContext(tmenu));
                    items.Add(new SubMenuUIContext(tmenu, "Inject event", (sender, e) => Inject(EInjectType.Event), browserimages.Images[(int)EBrowserImages.PublicEvent]));
                    items.Add(new SubMenuUIContext(tmenu, "Inject field", (sender, e) => Inject(EInjectType.Field), browserimages.Images[(int)EBrowserImages.PublicField]));
                    items.Add(new SubMenuUIContext(tmenu, "Inject method", (sender, e) => Inject(EInjectType.Method), browserimages.Images[(int)EBrowserImages.PublicMethod]));
                    items.Add(new SubMenuUIContext(tmenu, "Inject constructor", (sender, e) => Inject(EInjectType.Constructor), browserimages.Images[(int)EBrowserImages.PublicConstructor]));
                    items.Add(new SubMenuUIContext(tmenu, "Inject property", (sender, e) => Inject(EInjectType.Property), browserimages.Images[(int)EBrowserImages.PublicProperty]));
                    items.Add(tmenu);

                    // Assembly menu
                    items.Add(new MenuUIContext(cbm.CommandBars[REFLECTOR_ASSEMBLY_ID]));
                    var amenu = new MenuUIContext(cbm.CommandBars[REFLECTOR_ASSEMBLY_ID], REFLEXIL_ASSEMBLY_MENU_ID, REFLEXIL_BUTTON_TEXT, BasePlugin.ReflexilImage);

                    // Module menu
                    items.Add(new MenuUIContext(cbm.CommandBars[REFLECTOR_MODULE_ID]));
                    var mmenu = new MenuUIContext(cbm.CommandBars[REFLECTOR_MODULE_ID], REFLEXIL_MODULE_MENU_ID, REFLEXIL_BUTTON_TEXT, BasePlugin.ReflexilImage);

                    // Shared subitems dor Assembly/Module
                    foreach (MenuUIContext menu in new MenuUIContext[] { amenu, mmenu })
                    {
                        items.Add(new SubMenuUIContext(menu, "Inject class", (sender, e) => Inject(EInjectType.Class), browserimages.Images[(int)EBrowserImages.PublicClass]));
                        items.Add(new SubMenuUIContext(menu, "Inject interface", (sender, e) => Inject(EInjectType.Interface), browserimages.Images[(int)EBrowserImages.PublicInterface]));
                        items.Add(new SubMenuUIContext(menu, "Inject struct", (sender, e) => Inject(EInjectType.Struct), browserimages.Images[(int)EBrowserImages.PublicStructure]));
                        items.Add(new SubMenuUIContext(menu, "Inject enum", (sender, e) => Inject(EInjectType.Enum), browserimages.Images[(int)EBrowserImages.PublicEnum]));
                        items.Add(new SubMenuUIContext(menu, "Inject assembly reference", (sender, e) => Inject(EInjectType.AssemblyReference), browserimages.Images[(int)EBrowserImages.LinkedAssembly]));
                        items.Add(new SubMenuUIContext(menu));
                        items.Add(new SubMenuUIContext(menu, "Save as...", (sender, e) => AssemblyHelper.SaveAssembly(GetCurrentAssemblyDefinition(), GetCurrentModuleOriginalLocation()), barimages.Images[(int)EBarImages.Save]));
                        items.Add(new SubMenuUIContext(menu, "Reload", ReloadAssembly, barimages.Images[(int)EBarImages.Reload]));
                        items.Add(new SubMenuUIContext(menu, "Verify", (sender, e) => AssemblyHelper.VerifyAssembly(GetCurrentAssemblyDefinition(), GetCurrentModuleOriginalLocation()), barimages.Images[(int)EBarImages.Check]));
                        items.Add(menu);
                    }
                }
            }

            // Main events
            ab.ActiveItemChanged += this.ActiveItemChanged;
            am.AssemblyLoaded += this.AssemblyLoaded;
            am.AssemblyUnloaded += this.AssemblyUnloaded;
            
			PluginFactory.GetInstance().ReloadAssemblies(am.Assemblies);
			reflexilwindow.HandleItem(ab.ActiveItem);
		}
		
        /// <summary>
        /// Addin unload method
        /// </summary>
		public void Unload()
		{
            // Main events
            ab.ActiveItemChanged -= this.ActiveItemChanged;
            am.AssemblyLoaded -= this.AssemblyLoaded;
            am.AssemblyUnloaded -= this.AssemblyUnloaded;

            // Main Window
            wm.Windows.Remove(REFLEXIL_WINDOW_ID);

            // Menus, buttons and events
            foreach (UIContext item in items)
            {
                item.Unload();
            }
        }
		
		#endregion

    }
}


