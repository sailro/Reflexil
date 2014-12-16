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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using Reflexil.Forms;
using Reflexil.Wrappers;
using MessageBox = System.Windows.MessageBox;

namespace Reflexil.Plugins.ILSpy
{
	[ExportMainMenuCommand(Menu = "_View", MenuIcon = "resources/reflexil.png", Header = "Reflexil v2.0", MenuCategory = "Reflexil"), ExportToolbarCommand(ToolTip = "Reflexil v2.0", ToolbarCategory = "Reflexil", ToolbarIcon = "resources/reflexil.png")]
	public sealed class ILSpyPackage : BasePackage, ICommand
	{
		private ReflexilHost _host;
		private bool _hotReplacingAssembly;
		public event EventHandler CanExecuteChanged = delegate { };

		public ILSpyPackage()
		{
			PluginFactory.Register(new ILSpyPlugin(this));

			ReflexilWindow = new ReflexilWindow(useMergedAssemblyModuleHandler: true);

			WireEvents();

			ReflexilWindow.HandleItem(ActiveItem);
		}

		private void WireEvents()
		{
			var instance = MainWindow.Instance;

			WireTreeviewEvents(instance);
			WireAssemblyEvents(instance);
		}

		private void WireAssemblyEvents(MainWindow instance)
		{
			instance.CurrentAssemblyListChanged += (sender, args) =>
			{
				if (args.NewItems != null && args.NewItems.Count > 0)
					AssemblyLoaded(this, EventArgs.Empty);
				else
				{
					// Ignore if we are hot replacing an assembly
					if (_hotReplacingAssembly)
						return;

					AssemblyUnloaded(this, EventArgs.Empty);
					
					// Remove loaded contexts
					var plugin = PluginFactory.GetInstance() as ILSpyPlugin;
					if (plugin == null)
						return;

					foreach (LoadedAssembly loadedAssembly in args.OldItems)
						plugin.RemoveAssemblyContext(loadedAssembly.FileName);
				}
			};
		}

		private void WireTreeviewEvents(MainWindow instance)
		{
			instance.SelectionChanged += ActiveItemChanged;
		}

		public override IEnumerable<IAssemblyWrapper> HostAssemblies
		{
			get
			{
				var current = MainWindow.Instance.CurrentAssemblyList;
				if (current == null)
					return new List<IAssemblyWrapper>();

				return current.GetAssemblies().Select(a => new ILSpyAssemblyWrapper(a));
			}
		}

		public override object ActiveItem
		{
			get { return MainWindow.Instance.SelectedNodes.FirstOrDefault(); }
		}

		internal object GetNodeObject(ILSpyTreeNode node)
		{
			if (node == null)
				return null;

			var mnode = node as MethodTreeNode;
			if (mnode != null)
				return mnode.MethodDefinition;

			var pnode = node as PropertyTreeNode;
			if (pnode != null)
				return pnode.PropertyDefinition;

			var fnode = node as FieldTreeNode;
			if (fnode != null)
				return fnode.FieldDefinition;

			var enode = node as EventTreeNode;
			if (enode != null)
				return enode.EventDefinition;

			var rnode = node as ResourceTreeNode;
			if (rnode != null)
				return rnode.Resource;

			var anode = node as AssemblyReferenceTreeNode;
			if (anode != null)
				return anode.AssemblyNameReference;

			var tnode = node as TypeTreeNode;
			if (tnode != null)
				return tnode.TypeDefinition;

			return null;
		}

		protected override void ItemDeleted(object sender, EventArgs e)
		{
			var nodeObject = GetNodeObject(ActiveItem as ILSpyTreeNode);
			var plugin = PluginFactory.GetInstance() as ILSpyPlugin;

			if (plugin != null && nodeObject != null)
				plugin.RemoveFromCache(nodeObject);

			base.ItemDeleted(sender, e);
			UpdateILSpyObjectModel(sender, e);
		}

		protected override void ItemRenamed(object sender, EventArgs e)
		{
			base.ItemRenamed(sender, e);
			UpdateILSpyObjectModel(sender, e);
		}

		protected override void ItemInjected(object sender, EventArgs e)
		{
			base.ItemInjected(sender, e);
			UpdateILSpyObjectModel(sender, e);
		}

		protected override void MainButtonClick(object sender, EventArgs e)
		{
			var instance = MainWindow.Instance;
			var content = CreateHostControlIfNecessary();
			instance.ShowInBottomPane(ReflexilWindowText, content);
		}

		private ReflexilHost CreateHostControlIfNecessary()
		{
			if (_host != null)
				return _host;

			Application.EnableVisualStyles();
			_host = new ReflexilHost(this);
			return _host;
		}

		public override void ShowMessage(string message)
		{
			MessageBox.Show(message);
		}

		public void Execute(object parameter)
		{
			MainButtonClick(this, EventArgs.Empty);
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		protected override void DisplayWarning()
		{
			//Do nothing
		}

		public void UpdateILSpyObjectModel(object sender, EventArgs empty)
		{
			var plugin = PluginFactory.GetInstance() as ILSpyPlugin;
			if (plugin == null)
				return;

			var context = plugin.GetAssemblyContext(ActiveItem as ILSpyTreeNode);
			if (context == null)
				return;

			var adef = context.AssemblyDefinition;
			var wrapper = HostAssemblies.FirstOrDefault(a => a.Location == adef.MainModule.Image.FileName) as ILSpyAssemblyWrapper;
			if (wrapper == null)
				return;

			var loadedAssembly = wrapper.LoadedAssembly;

			// Ok we have everything, write the assembly to stream
			var stream = new MemoryStream();
			adef.MainModule.Write(stream);
			stream.Position = 0;

			// Then hot-replace the assembly
			try
			{
				_hotReplacingAssembly = true;
				loadedAssembly.AssemblyList.HotReplaceAssembly(loadedAssembly.FileName, stream);
			}
			finally
			{
				_hotReplacingAssembly = false;
			}

		}
	}


}
