using Reflexil.Plugins;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.ILSpy.Plugins
{
	[ExportMainMenuCommand(Menu = "_Tools", Header = "_Reflexil")]
	public sealed class ILSpyPackage : BasePackage, ICommand
	{
		public ILSpyPackage()
		{
			PluginFactory.Register(new ILSpyPlugin(this));

			CheckPrerequisites();

			// MainWindow
			ReflexilWindow = new Forms.ReflexilWindow();

			// Events
			var instance = MainWindow.Instance;
			var field = typeof (MainWindow).GetField("treeView", BindingFlags.NonPublic | BindingFlags.Instance);
			if (field == null)
				return;

			var treeview = (SharpTreeView) field.GetValue(instance);
			treeview.SelectionChanged += ActiveItemChanged;

			instance.CurrentAssemblyListChanged += (sender, args) =>
			{
				if (args.NewItems!= null && args.NewItems.Count > 0)
					AssemblyLoaded(sender, args);
				else
					AssemblyUnloaded(sender, args);
			};

			PluginFactory.GetInstance().ReloadAssemblies(Assemblies);
			ReflexilWindow.HandleItem(ActiveItem);
		}

		public override System.Collections.ICollection Assemblies
		{
			get { return MainWindow.Instance.CurrentAssemblyList.GetAssemblies(); }
		}

		public override object ActiveItem
		{
			get { return MainWindow.Instance.SelectedNodes.FirstOrDefault(); }
		}

		protected override void Button_Click(object sender, EventArgs e)
		{
			ShowMessage("foo");
		}

		public override void ShowMessage(string message)
		{
			System.Windows.MessageBox.Show(message);
		}

		public void Execute(object parameter)
		{
			Button_Click(this, EventArgs.Empty);
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		[ExportContextMenuEntryAttribute(Header = "Verify Assembly")]
		public class VerifyAssembly : IContextMenuEntry
		{
			public bool IsVisible(TextViewContext context)
			{
				return context.SelectedTreeNodes != null && context.SelectedTreeNodes.All(n => n is AssemblyTreeNode);
			}

			public bool IsEnabled(TextViewContext context)
			{
				return context.SelectedTreeNodes != null && context.SelectedTreeNodes.Length == 1;
			}

			public void Execute(TextViewContext context)
			{
			}
		}
	}


}
