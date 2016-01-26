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

extern alias jdcecil;
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
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Reflexil.JustDecompile.Plugins;
using Application = System.Windows.Forms.Application;

namespace Reflexil.JustDecompile
{
    [ModuleExport(typeof(JustDecompilePackage))]
    public class JustDecompilePackage: BasePackage, IModule, IPartImportsSatisfiedNotification
	{
	    private const string PluginRegion = "PluginRegion";
	    private const string ToolMenuRegion = "ToolMenuRegion";

	    private ReflexilHost _host;

		[Import]
		private IEventAggregator _eventAggregator;

		[Import]
		private IAssemblyManagerService _assemblyManager;

		[Import]
		private ITreeViewNavigatorService _treeViewNavigator;

		[Import]
		private IRegionManager _regionManager;

		public override IEnumerable<IAssemblyWrapper> HostAssemblies
		{
			get { return _assemblyManager.LoadedAssemblies.Select(a => new JustDecompileAssemblyWrapper(a) ); }
		}

	    private ITreeViewItem _activeItem;
		public override object ActiveItem
		{
			get
			{
				return _activeItem;
			}
		}

		public void Initialize()
		{
			PluginFactory.Register(new JustDecompilePlugin(this));
			ReflexilWindow = new ReflexilWindow();

			_regionManager.AddToRegion(ToolMenuRegion, new JustDecompileToolMenuItem(() => MainButtonClick(this, EventArgs.Empty)));

			WireEvents();
			ReflexilWindow.HandleItem(ActiveItem);
		}

        public void OnImportsSatisfied()
        {
		}

		private void WireEvents()
		{
			_eventAggregator.GetEvent<SelectedTreeViewItemChangedEvent>().Subscribe(item =>
			{
				_activeItem = item;
				ActiveItemChanged(item, EventArgs.Empty);
			});
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
