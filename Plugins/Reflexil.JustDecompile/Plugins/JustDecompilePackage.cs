/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using JustDecompile.API.Core;
using JustDecompile.API.CompositeEvents;
using JustDecompile.API.Core.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

using Reflexil.Forms;
using Reflexil.Plugins;
using Reflexil.Plugins.JustDecompile;
using Reflexil.Wrappers;
using System;
using System.IO;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Reflexil.JustDecompile.Plugins.ContextMenu;
using Application = System.Windows.Forms.Application;

namespace Reflexil.JustDecompile
{
	[ModuleExport(typeof(JustDecompilePackage))]
	public class JustDecompilePackage : BasePackage, IModule, IPartImportsSatisfiedNotification
	{
		private const string PluginRegion = "PluginRegion";
		private const string ToolMenuRegion = "ToolMenuRegion";
		private const string AssemblyTreeViewContextMenuRegion = "AssemblyTreeViewContextMenuRegion";
		private const string TypeTreeViewContextMenuRegion = "TypeTreeViewContextMenuRegion";
		private const string ResourceTreeViewContextMenuRegion = "ResourceTreeViewContextMenuRegion";
		private const string EmbeddedResourceTreeViewContextMenuRegion = "EmbeddedResourceTreeViewContextMenuRegion";
		private const string AssemblyReferenceTreeViewContextMenuRegion = "AssemblyReferenceTreeViewContextMenuRegion";
		private const string MemberTreeViewContextMenuRegion = "MemberTreeViewContextMenuRegion";
		private const string ModuleDefinitionTreeViewContextMenuRegion = "ModuleDefinitionTreeViewContextMenuRegion";

		private ReflexilHost _host;

		[Import] private IEventAggregator _eventAggregator = null;
		[Import] private IAssemblyManagerService _assemblyManager = null;
		//[Import] private ITreeViewNavigatorService _treeViewNavigator = null;
		[Import] private IRegionManager _regionManager = null;

		public override IEnumerable<IAssemblyWrapper> HostAssemblies
		{
			get { return _assemblyManager.LoadedAssemblies.Select(a => new JustDecompileAssemblyWrapper(a)); }
		}

		private ITreeViewItem _activeItem;
		private JustDecompilePlugin _justDecompilePlugin;

		public override object ActiveItem
		{
			get { return _activeItem; }
		}

		public void Initialize()
		{
			_justDecompilePlugin = new JustDecompilePlugin(this);
			PluginFactory.Register(_justDecompilePlugin);
			ReflexilWindow = new ReflexilWindow();

			var moduleDefinitionTreeViewContextMenu = new ModuleDefinitionTreeViewContextMenu();
			var memberTreeViewContextMenu = new MemberTreeViewContextMenu();

			_regionManager.AddToRegion(ToolMenuRegion, new JustDecompileToolMenuItem(() => MainButtonClick(this, EventArgs.Empty)));
			_regionManager.AddToRegion(AssemblyTreeViewContextMenuRegion, moduleDefinitionTreeViewContextMenu);
			_regionManager.AddToRegion(TypeTreeViewContextMenuRegion, new TypeTreeViewContextMenu());
			_regionManager.AddToRegion(ResourceTreeViewContextMenuRegion, memberTreeViewContextMenu);
			_regionManager.AddToRegion(EmbeddedResourceTreeViewContextMenuRegion, memberTreeViewContextMenu);
			_regionManager.AddToRegion(AssemblyReferenceTreeViewContextMenuRegion, memberTreeViewContextMenu);
			_regionManager.AddToRegion(MemberTreeViewContextMenuRegion, memberTreeViewContextMenu);
			_regionManager.AddToRegion(ModuleDefinitionTreeViewContextMenuRegion, moduleDefinitionTreeViewContextMenu);

			WireTreeViewEvents();
			WireAssemblyEvents();

			ReflexilWindow.HandleItem(ActiveItem);
		}

		public void OnImportsSatisfied()
		{
		}

		private void WireTreeViewEvents()
		{
			_eventAggregator.GetEvent<SelectedTreeViewItemChangedEvent>().Subscribe(item =>
			{
				_activeItem = item;
				ActiveItemChanged(item, EventArgs.Empty);
			});
		}

		private void WireAssemblyEvents()
		{
			_eventAggregator.GetEvent<TreeViewItemCollectionChangedEvent>().Subscribe(items =>
			{
				var validLocations = items.Where(i => i is IAssemblyDefinitionTreeViewItem)
					.Cast<IAssemblyDefinitionTreeViewItem>()
					.Select(a => a.AssemblyDefinition.MainModule.FilePath);

				_justDecompilePlugin.RemoveObsoleteAssemblyContexts(validLocations);
			});
		}

		protected override void ItemDeleted(object sender, EventArgs e)
		{
			//var nodeObject = GetNodeObject(ActiveItem as ILSpyTreeNode);
			//var plugin = PluginFactory.GetInstance() as ILSpyPlugin;

			//if (plugin != null && nodeObject != null)
			//	plugin.RemoveFromCache(nodeObject);

			base.ItemDeleted(sender, e);
			UpdateHostObjectModel(sender, e);
		}

		protected override void ItemRenamed(object sender, EventArgs e)
		{
			base.ItemRenamed(sender, e);
			UpdateHostObjectModel(sender, e);
		}

		protected override void ItemInjected(object sender, EventArgs e)
		{
			base.ItemInjected(sender, e);
			UpdateHostObjectModel(sender, e);
		}

		protected override void DisplayWarning()
		{
			//Do nothing, as we use UpdateHostObjectModel
		}

		protected override void HotReplaceAssembly(IAssemblyWrapper wrapper, MemoryStream stream)
		{
			var ilspyWrapper = wrapper as JustDecompileAssemblyWrapper;
			if (ilspyWrapper == null)
				return;

			_assemblyManager.HotReplaceAssembly(wrapper.Location, stream);
		}

		protected override void MainButtonClick(object sender, EventArgs e)
		{
			var content = CreateHostControlIfNecessary();
			_regionManager.AddToRegion(PluginRegion, content);
		}

		private void OnCloseReflexilHost()
		{
			var pluginRegion = _regionManager.Regions[PluginRegion];

			if (pluginRegion.Views.Contains(_host))
				pluginRegion.Remove(_host);
		}

		private ReflexilHost CreateHostControlIfNecessary()
		{
			if (_host != null)
				return _host;

			Application.EnableVisualStyles();
			_host = new ReflexilHost(this, OnCloseReflexilHost);
			return _host;
		}

		public override void ShowMessage(string message)
		{
			MessageBox.Show(message);
		}
	}
}