/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using System;
using System.Collections;
using System.Linq;
using Cecil.Decompiler.Gui.Services;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Reflexil.Utils;
#endregion

namespace Reflexil.Plugins.CecilStudio
{
	/// <summary>
	/// Addin entry point
	/// </summary>
    public class CecilStudioPackage : BasePackage, Cecil.Decompiler.Gui.Services.IPlugin
    {

        #region " Constants "
        const string CECILSTUDIO_RESOURCE_IMAGES = "Cecil.Decompiler.Gui.icons.png";
        #endregion

        #region " Fields "
        private IWindowManager wm;
		private IAssemblyBrowser ab;
		private IBarManager cbm;
		private IAssemblyManager am;
		private IServiceProvider sp;
        private List<UIContext> items;
		#endregion

        #region " Properties "
        public override ICollection Assemblies
        {
            get { return Enumerable.ToList(am.Assemblies); }
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
        /// <typeparam name="T">Cecil Studio service interface</typeparam>
        /// <returns>Cecil studio service implementation</returns>
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
            if (cbm.Bars[id].Items.Count > 0)
            {
                items.Add(new MenuUIContext(cbm.Bars[id]));
            }
            return new MenuUIContext(cbm.Bars[id], GenerateId(id), REFLEXIL_BUTTON_TEXT, BasePlugin.ReflexilImage);
        }

        /// <summary>
        /// Addin load method
        /// </summary>
        /// <param name="serviceProvider">Cecil Studio service provider</param>
		public void Load(System.IServiceProvider serviceProvider)
		{
            PluginFactory.Register(new CecilStudioPlugin(this));
            
            sp = serviceProvider;
			wm = GetService<IWindowManager>();
			ab = GetService<IAssemblyBrowser>();
			cbm = GetService<IBarManager>();
			am = GetService<IAssemblyManager>();

            CheckFrameWorkVersion();

            // Main Window
            items = new List<UIContext>();
            reflexilwindow = new Reflexil.Forms.ReflexilWindow();
            IWindow window = wm.Windows.Add(REFLEXIL_WINDOW_ID, reflexilwindow, REFLEXIL_WINDOW_TEXT);
            window.Image = BasePlugin.ReflexilImage;

            // Main button
            items.Add(new ButtonUIContext(cbm.Bars[BarNames.Toolbar]));
            items.Add(new ButtonUIContext(cbm.Bars[BarNames.Toolbar], REFLEXIL_BUTTON_TEXT, Button_Click, BasePlugin.ReflexilImage));

            using (ImageList browserimages = new ImageList())
            {
                browserimages.Images.AddStrip(PluginFactory.GetInstance().GetAllBrowserImages());
                browserimages.TransparentColor = Color.Green;

                using (ImageList barimages = new ImageList())
                {
                    barimages.Images.AddStrip(PluginFactory.GetInstance().GetAllBarImages());

                    // Menus
                    var typemenu = AddMenu(BarNames.TypeDefinitionBrowser.ToString());
                    var assemblymenu = AddMenu(BarNames.AssemblyDefinitionBrowser.ToString());
                    var assemblyrefmenu = AddMenu(BarNames.AssemblyNameReferenceBrowser.ToString());
                    var modulemenu = AddMenu(BarNames.ModuleDefinitionBrowser.ToString());
                    var methodmenu = AddMenu(BarNames.MethodDefinitionBrowser.ToString());
                    var fieldmenu = AddMenu(BarNames.FieldDefinitionBrowser.ToString());
                    var propertymenu = AddMenu(BarNames.PropertyDefinitionBrowser.ToString());
                    var eventmenu = AddMenu(BarNames.EventDefinitionBrowser.ToString());

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
                        items.Add(new SubMenuUIContext(menu, "Obfuscator search...", (sender, e) => AssemblyHelper.SearchObfuscator(GetCurrentModuleOriginalLocation()), barimages.Images[(int)EBarImages.Search]));
                        items.Add(new SubMenuUIContext(menu, "Reload", ReloadAssembly, barimages.Images[(int)EBarImages.Reload]));
                        items.Add(new SubMenuUIContext(menu, "Rename...", RenameItem, barimages.Images[(int)EBarImages.New]));
                        items.Add(new SubMenuUIContext(menu, "Verify", (sender, e) => AssemblyHelper.VerifyAssembly(GetCurrentAssemblyDefinition(), GetCurrentModuleOriginalLocation()), barimages.Images[(int)EBarImages.Check]));
                    }

                    // Shared subitems for renaming/deleting
                    foreach (MenuUIContext menu in membersmenus)
                    {
                        if (menu == typemenu)
                        {
                            items.Add(new SubMenuUIContext(menu));
                        }
                        items.Add(new SubMenuUIContext(menu, "Rename...", RenameItem, barimages.Images[(int)EBarImages.New]));
                        items.Add(new SubMenuUIContext(menu, "Delete", DeleteMember, barimages.Images[(int)EBarImages.Delete]));
                    }

                    items.AddRange(allmenus);
                }
            }

            // Main events
            ab.ActiveItemChanged += this.ActiveItemChanged;
            am.AssemblyLoaded += this.AssemblyLoaded;
            am.AssemblyUnloaded += this.AssemblyUnloaded;
            
			PluginFactory.GetInstance().ReloadAssemblies(Enumerable.ToList(am.Assemblies));
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


