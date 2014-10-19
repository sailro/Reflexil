/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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

        #region Fields
        private IWindowManager _wm;
		private IAssemblyBrowser _ab;
		private IBarManager _cbm;
		private IAssemblyManager _am;
		private IServiceProvider _sp;
        private List<UIContext> _items;
		#endregion

        #region Properties
        public override ICollection Assemblies
        {
            get { return _am.Assemblies.ToList(); }
        }

        public override object ActiveItem
        {
            get { return _ab.ActiveItem; }
        }
        #endregion

        #region Events
        /// <summary>
        /// 'Reflexil' button click 
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
		protected override void Button_Click(object sender, EventArgs e)
		{
			_wm.Windows[ReflexilWindowId].Visible = true;
		}
		#endregion
		
		#region " Methods "
        /// <summary>
        /// Display a message
        /// </summary>
        /// <param name="message">message to display</param>
        public override void ShowMessage(string message)
        {
            _wm.ShowMessage(message);
        }

        /// <summary>
        /// Helper method
        /// </summary>
        /// <typeparam name="T">Cecil Studio service interface</typeparam>
        /// <returns>Cecil studio service implementation</returns>
		public T GetService<T>()
		{
			return ((T) (_sp.GetService(typeof(T))));
		}

        /// <summary>
        /// Add a menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>a menu context</returns>
        private MenuUIContext AddMenu(string id)
        {
            if (_cbm.Bars[id].Items.Count > 0)
                _items.Add(new MenuUIContext(_cbm.Bars[id]));

			return new MenuUIContext(_cbm.Bars[id], GenerateId(id), ReflexilButtonText, BasePlugin.ReflexilImage);
        }

        /// <summary>
        /// Addin load method
        /// </summary>
        /// <param name="serviceProvider">Cecil Studio service provider</param>
		public void Load(IServiceProvider serviceProvider)
		{
            PluginFactory.Register(new CecilStudioPlugin(this));
            
            _sp = serviceProvider;
			_wm = GetService<IWindowManager>();
			_ab = GetService<IAssemblyBrowser>();
			_cbm = GetService<IBarManager>();
			_am = GetService<IAssemblyManager>();

            CheckPrerequisites();

            // Main Window
            _items = new List<UIContext>();
            ReflexilWindow = new Forms.ReflexilWindow();
			var window = _wm.Windows.Add(ReflexilWindowId, ReflexilWindow, ReflexilWindowText);
            window.Image = BasePlugin.ReflexilImage;

            // Main button
            _items.Add(new ButtonUIContext(_cbm.Bars[BarNames.Toolbar]));
            _items.Add(new ButtonUIContext(_cbm.Bars[BarNames.Toolbar], ReflexilButtonText, Button_Click, BasePlugin.ReflexilImage));

            using (var browserimages = new ImageList())
            {
                browserimages.Images.AddStrip(PluginFactory.GetInstance().GetAllBrowserImages());
                browserimages.TransparentColor = Color.Green;

                using (var barimages = new ImageList())
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
                    _items.Add(new SubMenuUIContext(typemenu, "Inject inner class", (sender, e) => Inject(InjectType.Class), browserimages.Images[(int)EBrowserImages.PublicClass]));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject inner interface", (sender, e) => Inject(InjectType.Interface), browserimages.Images[(int)EBrowserImages.PublicInterface]));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject inner struct", (sender, e) => Inject(InjectType.Struct), browserimages.Images[(int)EBrowserImages.PublicStructure]));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject inner enum", (sender, e) => Inject(InjectType.Enum), browserimages.Images[(int)EBrowserImages.PublicEnum]));
                    _items.Add(new SubMenuUIContext(typemenu));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject event", (sender, e) => Inject(InjectType.Event), browserimages.Images[(int)EBrowserImages.PublicEvent]));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject field", (sender, e) => Inject(InjectType.Field), browserimages.Images[(int)EBrowserImages.PublicField]));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject method", (sender, e) => Inject(InjectType.Method), browserimages.Images[(int)EBrowserImages.PublicMethod]));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject constructor", (sender, e) => Inject(InjectType.Constructor), browserimages.Images[(int)EBrowserImages.PublicConstructor]));
                    _items.Add(new SubMenuUIContext(typemenu, "Inject property", (sender, e) => Inject(InjectType.Property), browserimages.Images[(int)EBrowserImages.PublicProperty]));

                    // Shared subitems for Assembly/Module
                    foreach (var menu in new[] { assemblymenu, modulemenu })
                    {
                        _items.Add(new SubMenuUIContext(menu, "Inject class", (sender, e) => Inject(InjectType.Class), browserimages.Images[(int)EBrowserImages.PublicClass]));
                        _items.Add(new SubMenuUIContext(menu, "Inject interface", (sender, e) => Inject(InjectType.Interface), browserimages.Images[(int)EBrowserImages.PublicInterface]));
                        _items.Add(new SubMenuUIContext(menu, "Inject struct", (sender, e) => Inject(InjectType.Struct), browserimages.Images[(int)EBrowserImages.PublicStructure]));
                        _items.Add(new SubMenuUIContext(menu, "Inject enum", (sender, e) => Inject(InjectType.Enum), browserimages.Images[(int)EBrowserImages.PublicEnum]));
                        _items.Add(new SubMenuUIContext(menu, "Inject assembly reference", (sender, e) => Inject(InjectType.AssemblyReference), browserimages.Images[(int)EBrowserImages.LinkedAssembly]));
                        _items.Add(new SubMenuUIContext(menu));
                        _items.Add(new SubMenuUIContext(menu, "Save as...", (sender, e) => AssemblyHelper.SaveAssembly(GetCurrentAssemblyDefinition(), GetCurrentModuleOriginalLocation()), barimages.Images[(int)EBarImages.Save]));
                        _items.Add(new SubMenuUIContext(menu, "Obfuscator search...", (sender, e) => AssemblyHelper.SearchObfuscator(GetCurrentModuleOriginalLocation()), barimages.Images[(int)EBarImages.Search]));
                        _items.Add(new SubMenuUIContext(menu, "Reload", ReloadAssembly, barimages.Images[(int)EBarImages.Reload]));
                        _items.Add(new SubMenuUIContext(menu, "Rename...", RenameItem, barimages.Images[(int)EBarImages.New]));
                        _items.Add(new SubMenuUIContext(menu, "Verify", (sender, e) => AssemblyHelper.VerifyAssembly(GetCurrentAssemblyDefinition(), GetCurrentModuleOriginalLocation()), barimages.Images[(int)EBarImages.Check]));
                    }

                    // Shared subitems for renaming/deleting
                    foreach (var uiContext in membersmenus)
                    {
	                    var menu = (MenuUIContext) uiContext;
	                    if (menu == typemenu)
                            _items.Add(new SubMenuUIContext(menu));

						_items.Add(new SubMenuUIContext(menu, "Rename...", RenameItem, barimages.Images[(int)EBarImages.New]));
                        _items.Add(new SubMenuUIContext(menu, "Delete", DeleteMember, barimages.Images[(int)EBarImages.Delete]));
                    }

                    _items.AddRange(allmenus);
                }
            }

            // Main events
            _ab.ActiveItemChanged += ActiveItemChanged;
            _am.AssemblyLoaded += AssemblyLoaded;
            _am.AssemblyUnloaded += AssemblyUnloaded;
            
			PluginFactory.GetInstance().ReloadAssemblies(_am.Assemblies.ToList());
			ReflexilWindow.HandleItem(_ab.ActiveItem);
		}

        /// <summary>
        /// Addin unload method
        /// </summary>
        public void Unload()
        {
            // Main events
            _ab.ActiveItemChanged -= ActiveItemChanged;
            _am.AssemblyLoaded -= AssemblyLoaded;
            _am.AssemblyUnloaded -= AssemblyUnloaded;

            // Main Window
            _wm.Windows.Remove(ReflexilWindowId);

#if DEBUG
            System.Diagnostics.Debug.Assert(UIContext.InstanceCount == _items.Count);
#endif

            // Menus, buttons and events
            foreach (var item in _items)
                item.Unload();

#if DEBUG
            System.Diagnostics.Debug.Assert(UIContext.InstanceCount == 0);
#endif

            PluginFactory.Unregister();
        }
        #endregion

    }
}


