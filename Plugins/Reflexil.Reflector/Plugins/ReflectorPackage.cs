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
using System.Linq;
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
        const string REFLECTOR_METHODDEC_ID = "Browser.MethodDeclaration"; 
        const string REFLECTOR_FIELDDEC_ID = "Browser.FieldDeclaration";
        const string REFLECTOR_PROPERTYDEC_ID = "Browser.PropertyDeclaration";
        const string REFLECTOR_EVENTDEC_ID = "Browser.EventDeclaration";
        const string REFLECTOR_ASSEMBLYREF_ID = "Browser.AssemblyReference";
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

        /// <summary>
        /// Add a menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>a menu context</returns>
        private MenuUIContext AddMenu(string id)
        {
            if (cbm.CommandBars[id].Items.Count > 0)
            {
                items.Add(new MenuUIContext(cbm.CommandBars[id]));
            }
            return new MenuUIContext(cbm.CommandBars[id], GenerateId(id), REFLEXIL_BUTTON_TEXT, BasePlugin.ReflexilImage);
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

                    // Menus
                    var typemenu = AddMenu(REFLECTOR_TYPEDEC_ID);
                    var assemblymenu = AddMenu(REFLECTOR_ASSEMBLY_ID);
                    var assemblyrefmenu = AddMenu(REFLECTOR_ASSEMBLYREF_ID);
                    var modulemenu = AddMenu(REFLECTOR_MODULE_ID);
                    var methodmenu = AddMenu(REFLECTOR_METHODDEC_ID);
                    var fieldmenu = AddMenu(REFLECTOR_FIELDDEC_ID);
                    var propertymenu = AddMenu(REFLECTOR_PROPERTYDEC_ID);
                    var eventmenu = AddMenu(REFLECTOR_EVENTDEC_ID);

                    var allmenus = new UIContext[] { typemenu, assemblymenu, assemblyrefmenu, modulemenu, methodmenu, fieldmenu, propertymenu, eventmenu };
                    var membersmenus = new UIContext[] { assemblyrefmenu, typemenu, methodmenu, fieldmenu, propertymenu, eventmenu };

                    // Type declaration menu
                    items.Add(new SubMenuUIContext(typemenu, "Inject inner class", (sender, e) => Inject(EInjectType.Class), browserimages.Images[(int)EBrowserImages.PublicClass]));
                    items.Add(new SubMenuUIContext(typemenu, "Inject inner interface", (sender, e) => Inject(EInjectType.Interface), browserimages.Images[(int)EBrowserImages.PublicInterface]));
                    items.Add(new SubMenuUIContext(typemenu, "Inject inner struct", (sender, e) => Inject(EInjectType.Struct), browserimages.Images[(int)EBrowserImages.PublicStructure]));
                    items.Add(new SubMenuUIContext(typemenu, "Inject inner enum", (sender, e) => Inject(EInjectType.Enum), browserimages.Images[(int)EBrowserImages.PublicEnum]));
                    items.Add(new SubMenuUIContext(typemenu));
                    items.Add(new SubMenuUIContext(typemenu, "Inject event", (sender, e) => Inject(EInjectType.Event), browserimages.Images[(int)EBrowserImages.PublicEvent]));
                    items.Add(new SubMenuUIContext(typemenu, "Inject field", (sender, e) => Inject(EInjectType.Field), browserimages.Images[(int)EBrowserImages.PublicField]));
                    items.Add(new SubMenuUIContext(typemenu, "Inject method", (sender, e) => Inject(EInjectType.Method), browserimages.Images[(int)EBrowserImages.PublicMethod]));
                    items.Add(new SubMenuUIContext(typemenu, "Inject constructor", (sender, e) => Inject(EInjectType.Constructor), browserimages.Images[(int)EBrowserImages.PublicConstructor]));
                    items.Add(new SubMenuUIContext(typemenu, "Inject property", (sender, e) => Inject(EInjectType.Property), browserimages.Images[(int)EBrowserImages.PublicProperty]));

                    // Shared subitems for Assembly/Module
                    foreach (MenuUIContext menu in new MenuUIContext[] { assemblymenu, modulemenu })
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
                    }

                    // Shared subitems for renaming/deleting
                    foreach (MenuUIContext menu in membersmenus)
                    {
                        if (menu == typemenu)
                        {
                            items.Add(new SubMenuUIContext(menu));
                        }
                        items.Add(new SubMenuUIContext(menu, "Rename...", RenameMember, barimages.Images[(int)EBarImages.New]));
                        items.Add(new SubMenuUIContext(menu, "Delete", DeleteMember, barimages.Images[(int)EBarImages.Delete]));
                    }

                    items.AddRange(allmenus);
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

#if DEBUG
            System.Diagnostics.Debug.Assert(UIContext.InstanceCount == items.Count);
#endif

            // Menus, buttons and events
            foreach (UIContext item in items)
            {
                item.Unload();
            }

#if DEBUG
            System.Diagnostics.Debug.Assert(UIContext.InstanceCount == 0);
#endif

            PluginFactory.Unregister();
        }
		
		#endregion

    }
}


