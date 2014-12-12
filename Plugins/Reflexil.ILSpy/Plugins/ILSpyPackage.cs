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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using ICSharpCode.ILSpy;
using Reflexil.Forms;
using Reflexil.Plugins;
using MessageBox = System.Windows.MessageBox;

namespace Reflexil.ILSpy.Plugins
{
	[ExportMainMenuCommand(Menu = "_Tools", Header = "_Reflexil")]
	public sealed class ILSpyPackage : BasePackage, ICommand
	{
		private object _host;

		public event EventHandler CanExecuteChanged = delegate { };

		public ILSpyPackage()
		{
			PluginFactory.Register(new ILSpyPlugin(this));

			CheckPrerequisites();

			ReflexilWindow = new ReflexilWindow();

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
			instance.SelectionChanged += ActiveItemChanged;
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
	}


}
