using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;
using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;
using Reflexil.Forms;
using Reflexil.Plugins;
using MessageBox = System.Windows.MessageBox;

namespace Reflexil.ILSpy.Plugins
{
	[ExportMainMenuCommand(Menu = "_Tools", Header = "_Reflexil")]
	public sealed class ILSpyPackage : BasePackage, ICommand
	{
		private object _host;

		public ILSpyPackage()
		{
			PluginFactory.Register(new ILSpyPlugin(this));

			CheckPrerequisites();

			// MainWindow
			ReflexilWindow = new ReflexilWindow();

			// Events
			WireEvents();

			PluginFactory.GetInstance().ReloadAssemblies(Assemblies);
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
					AssemblyLoaded(sender, args);
				else
					AssemblyUnloaded(sender, args);
			};
		}

		private void WireTreeviewEvents(MainWindow instance)
		{
			var field = typeof (MainWindow).GetField("treeView", BindingFlags.NonPublic | BindingFlags.Instance);
			if (field == null)
				return;

			var treeview = (SharpTreeView) field.GetValue(instance);
			treeview.SelectionChanged += ActiveItemChanged;
		}

		public override ICollection Assemblies
		{
			get
			{
				var current = MainWindow.Instance.CurrentAssemblyList;
				var result = new List<LoadedAssembly>();
				if (current != null)
					result.AddRange(current.GetAssemblies());

				return result;
			}
		}

		public override object ActiveItem
		{
			get { return MainWindow.Instance.SelectedNodes.FirstOrDefault(); }
		}

		protected override void MainButtonClick(object sender, EventArgs e)
		{
			var instance = MainWindow.Instance;
			instance.ShowInBottomPane(ReflexilWindowText, CreateHostControlIfNecessary());
		}

		private object CreateHostControlIfNecessary()
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

		public event EventHandler CanExecuteChanged = delegate {};
		
	}


}
