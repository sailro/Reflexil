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

using System;
using System.Collections.Generic;
using Reflector;
using Reflector.CodeModel;
using System.Windows.Forms;
using Reflexil.Utils;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Reflexil.Wrappers;

namespace Reflexil.Plugins.Reflector
{
	/// <summary>
	/// Addin entry point
	/// </summary>
	public class ReflectorPackage : BasePackage, global::Reflector.IPackage
	{

		private const string ReflectorToolsId = "Tools";
		private const string ReflectorToolBarId = "ToolBar";
		private const string ReflectorTypedecId = "Browser.TypeDeclaration";
		private const string ReflectorAssemblyId = "Browser.Assembly";
		private const string ReflectorModuleId = "Browser.Module";
		private const string ReflectorMethoddecId = "Browser.MethodDeclaration";
		private const string ReflectorFielddecId = "Browser.FieldDeclaration";
		private const string ReflectorPropertydecId = "Browser.PropertyDeclaration";
		private const string ReflectorEventdecId = "Browser.EventDeclaration";
		private const string ReflectorAssemblyrefId = "Browser.AssemblyReference";
		private const string ReflectorResourceId = "Browser.Resource";

		private IWindowManager _wm;
		private IAssemblyBrowser _ab;
		private ICommandBarManager _cbm;
		private IAssemblyManager _am;
		private IServiceProvider _sp;
		private List<UIContext> _items;
		private MethodInfo _hotReplaceAssemblyMethod;

		public override IEnumerable<IAssemblyWrapper> HostAssemblies
		{
			get { return _am.Assemblies.Cast<IAssembly>().Select(a => new ReflectorAssemblyWrapper(a)).Cast<IAssemblyWrapper>(); }
		}

		public override object ActiveItem
		{
			get { return _ab.ActiveItem; }
		}

		protected override void MainButtonClick(object sender, EventArgs e)
		{
			_wm.Windows[ReflexilWindowId].Visible = true;
		}

		public override void ShowMessage(string message)
		{
			_wm.ShowMessage(message);
		}

		private T GetService<T>()
		{
			return ((T)(_sp.GetService(typeof(T))));
		}

		private MenuUIContext AddMenu(string id)
		{
			if (_cbm.CommandBars[id].Items.Count > 0)
				_items.Add(new MenuUIContext(_cbm.CommandBars[id]));

			return new MenuUIContext(_cbm.CommandBars[id], GenerateId(id), ReflexilButtonText, BasePlugin.ReflexilImage);
		}

		protected override void ItemDeleted(object sender, EventArgs e)
		{
			var plugin = PluginFactory.GetInstance() as ReflectorPlugin;
			if (plugin != null)
				plugin.RemoveFromCache(ActiveItem);

			base.ItemDeleted(sender, e);

			if (_hotReplaceAssemblyMethod != null)
				UpdateHostObjectModel(this, EventArgs.Empty);
		}

		protected override void ItemRenamed(object sender, EventArgs e)
		{
			base.ItemRenamed(sender, e);

			if (_hotReplaceAssemblyMethod != null)
				UpdateHostObjectModel(sender, e);
		}

		protected override void ItemInjected(object sender, EventArgs e)
		{
			base.ItemInjected(sender, e);

			if (_hotReplaceAssemblyMethod != null)
				UpdateHostObjectModel(sender, e);
		}

		public void Load(IServiceProvider serviceProvider)
		{
			PluginFactory.Register(new ReflectorPlugin(this));

			_sp = serviceProvider;
			_wm = GetService<IWindowManager>();
			_ab = GetService<IAssemblyBrowser>();
			_cbm = GetService<ICommandBarManager>();
			_am = GetService<IAssemblyManager>();

			// IAssemblyManager exposes HotReplaceAssembly, but we want to support older Reflector versions
			_hotReplaceAssemblyMethod = _am.GetType().GetMethod("HotReplaceAssembly", BindingFlags.Instance | BindingFlags.Public);

			// Main Window
			_items = new List<UIContext>();
			ReflexilWindow = new Forms.ReflexilWindow();
			_wm.Windows.Add(ReflexilWindowId, ReflexilWindow, ReflexilWindowText);

			// Main button
			AddButtonSeparatorIfNeeded(ReflectorToolsId);
			_items.Add(new ButtonUIContext(_cbm.CommandBars[ReflectorToolsId], ReflexilButtonText, MainButtonClick, BasePlugin.ReflexilImage));

			AddButtonSeparatorIfNeeded(ReflectorToolBarId);
			_items.Add(new ButtonUIContext(_cbm.CommandBars[ReflectorToolBarId], ReflexilButtonText, MainButtonClick, BasePlugin.ReflexilImage));

			using (var browserimages = new ImageList())
			{
				browserimages.Images.AddStrip(PluginFactory.GetInstance().GetAllBrowserImages());
				browserimages.TransparentColor = Color.Green;

				using (var barimages = new ImageList())
				{
					barimages.Images.AddStrip(PluginFactory.GetInstance().GetAllBarImages());

					// Menus
					var typemenu = AddMenu(ReflectorTypedecId);
					var assemblymenu = AddMenu(ReflectorAssemblyId);
					var assemblyrefmenu = AddMenu(ReflectorAssemblyrefId);
					var modulemenu = AddMenu(ReflectorModuleId);
					var methodmenu = AddMenu(ReflectorMethoddecId);
					var fieldmenu = AddMenu(ReflectorFielddecId);
					var propertymenu = AddMenu(ReflectorPropertydecId);
					var eventmenu = AddMenu(ReflectorEventdecId);
					var resmenu = AddMenu(ReflectorResourceId);

					var allmenus = new []
					{typemenu, assemblymenu, assemblyrefmenu, modulemenu, methodmenu, fieldmenu, propertymenu, eventmenu, resmenu};
					var membersmenus = new []
					{assemblyrefmenu, typemenu, methodmenu, fieldmenu, propertymenu, eventmenu, resmenu};

					// Type declaration menu
					_items.Add(new SubMenuUIContext(typemenu, "Inject constructor", (sender, e) => Inject(InjectType.Constructor),
						browserimages.Images[(int)EBrowserImages.PublicConstructor]));
					_items.Add(new SubMenuUIContext(typemenu, "Inject event", (sender, e) => Inject(InjectType.Event),
						browserimages.Images[(int)EBrowserImages.PublicEvent]));
					_items.Add(new SubMenuUIContext(typemenu, "Inject field", (sender, e) => Inject(InjectType.Field),
						browserimages.Images[(int)EBrowserImages.PublicField]));
					_items.Add(new SubMenuUIContext(typemenu, "Inject method", (sender, e) => Inject(InjectType.Method),
						browserimages.Images[(int)EBrowserImages.PublicMethod]));
					_items.Add(new SubMenuUIContext(typemenu, "Inject property", (sender, e) => Inject(InjectType.Property),
						browserimages.Images[(int)EBrowserImages.PublicProperty]));
					_items.Add(new SubMenuUIContext(typemenu));
					_items.Add(new SubMenuUIContext(typemenu, "Inject inner class", (sender, e) => Inject(InjectType.Class),
						browserimages.Images[(int)EBrowserImages.PublicClass]));
					_items.Add(new SubMenuUIContext(typemenu, "Inject inner enum", (sender, e) => Inject(InjectType.Enum),
						browserimages.Images[(int)EBrowserImages.PublicEnum]));
					_items.Add(new SubMenuUIContext(typemenu, "Inject inner interface", (sender, e) => Inject(InjectType.Interface),
						browserimages.Images[(int)EBrowserImages.PublicInterface]));
					_items.Add(new SubMenuUIContext(typemenu, "Inject inner struct", (sender, e) => Inject(InjectType.Struct),
						browserimages.Images[(int)EBrowserImages.PublicStructure]));

					// Shared subitems for Assembly/Module
					foreach (var menu in new[] { assemblymenu, modulemenu })
					{
						_items.Add(new SubMenuUIContext(menu, "Inject assembly reference",
							(sender, e) => Inject(InjectType.AssemblyReference), browserimages.Images[(int)EBrowserImages.LinkedAssembly]));
						_items.Add(new SubMenuUIContext(menu, "Inject class", (sender, e) => Inject(InjectType.Class),
							browserimages.Images[(int)EBrowserImages.PublicClass]));
						_items.Add(new SubMenuUIContext(menu, "Inject enum", (sender, e) => Inject(InjectType.Enum),
							browserimages.Images[(int)EBrowserImages.PublicEnum]));
						_items.Add(new SubMenuUIContext(menu, "Inject interface", (sender, e) => Inject(InjectType.Interface),
							browserimages.Images[(int)EBrowserImages.PublicInterface]));
						_items.Add(new SubMenuUIContext(menu, "Inject struct", (sender, e) => Inject(InjectType.Struct),
							browserimages.Images[(int)EBrowserImages.PublicStructure]));
						_items.Add(new SubMenuUIContext(menu, "Inject resource", (sender, e) => Inject(InjectType.Resource),
							browserimages.Images[(int)EBrowserImages.Resources]));

						_items.Add(new SubMenuUIContext(menu));
						_items.Add(new SubMenuUIContext(menu, "Reload Reflexil object model", ReloadAssembly, barimages.Images[(int)EBarImages.Reload]));
						if (_hotReplaceAssemblyMethod != null)
							_items.Add(new SubMenuUIContext(menu, "Update Reflector object model", UpdateHostObjectModel, barimages.Images[(int)EBarImages.Reload]));

						_items.Add(new SubMenuUIContext(menu));
						_items.Add(new SubMenuUIContext(menu, "Obfuscator search...", SearchObfuscator, barimages.Images[(int)EBarImages.Search]));
						_items.Add(new SubMenuUIContext(menu, "Rename...", RenameItem, barimages.Images[(int)EBarImages.New]));
						_items.Add(new SubMenuUIContext(menu, "Save as...", SaveAssembly, barimages.Images[(int)EBarImages.Save]));
						_items.Add(new SubMenuUIContext(menu, "Verify", VerifyAssembly, barimages.Images[(int)EBarImages.Check]));
					}

					// Shared subitems for renaming/deleting
					foreach (var menu in membersmenus)
					{
						if (menu == typemenu)
							_items.Add(new SubMenuUIContext(menu));

						_items.Add(new SubMenuUIContext(menu, "Delete", DeleteItem, barimages.Images[(int)EBarImages.Delete]));
						_items.Add(new SubMenuUIContext(menu, "Rename...", RenameItem, barimages.Images[(int)EBarImages.New]));

						if (_hotReplaceAssemblyMethod != null)
						{
							_items.Add(new SubMenuUIContext(menu));
							_items.Add(new SubMenuUIContext(menu, "Update Reflector object model", UpdateHostObjectModel, barimages.Images[(int)EBarImages.Reload]));
						}
					}

					_items.AddRange(allmenus);
				}
			}

			// Main events
			_ab.ActiveItemChanged += ActiveItemChanged;
			_am.AssemblyLoaded += AssemblyLoaded;
			_am.AssemblyUnloaded += AssemblyUnloaded;

			ReflexilWindow.HandleItem(_ab.ActiveItem);
		}

		private void AddButtonSeparatorIfNeeded(string barId)
		{
			var bar = _cbm.CommandBars[barId];
			if (bar == null)
				return;

			var last = bar.Items.Cast<ICommandBarItem>().LastOrDefault();
			if (last == null)
				return;

			if (last.Text != "-")
				_items.Add(new ButtonUIContext(bar));
		}

		protected override void AssemblyUnloaded(object sender, EventArgs e)
		{
			var plugin = PluginFactory.GetInstance() as ReflectorPlugin;
			if (plugin == null)
				return;

			var locations = HostAssemblies.Where(w => w.IsValid).Select(w => w.Location);
			plugin.RemoveObsoleteAssemblyContexts(locations);
		}

		public void Unload()
		{
			// Main events
			_ab.ActiveItemChanged -= ActiveItemChanged;
			_am.AssemblyLoaded -= AssemblyLoaded;
			_am.AssemblyUnloaded -= AssemblyUnloaded;

			// Main Window
			_wm.Windows.Remove(ReflexilWindowId);

			System.Diagnostics.Debug.Assert(UIContext.InstanceCount == _items.Count);

			// Menus, buttons and events
			foreach (var item in _items)
				item.Unload();

			System.Diagnostics.Debug.Assert(UIContext.InstanceCount == 0);

			PluginFactory.Unregister();
		}

		protected override void DisplayWarning()
		{
			//Do nothing, if we use UpdateHostObjectModel
			if (_hotReplaceAssemblyMethod != null)
				return;

			base.DisplayWarning();
		}

		protected override void HotReplaceAssembly(IAssemblyWrapper wrapper, MemoryStream stream)
		{
			var reflectorWrapper = wrapper as ReflectorAssemblyWrapper;
			if (reflectorWrapper == null)
				return;

			if (_hotReplaceAssemblyMethod == null)
				return;

			_hotReplaceAssemblyMethod.Invoke(_am, new object[] { wrapper.Location, stream });
		}
	}
}