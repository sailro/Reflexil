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

#region Imports

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Reflexil.Handlers;
using System.Text;

#endregion

namespace Reflexil.Forms
{
	/// <summary>
	/// Main reflexil window
	/// </summary>
	public sealed partial class ReflexilWindow
	{
		#region Fields

		private readonly List<IHandler> _handlers = new List<IHandler>();

		#endregion

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public ReflexilWindow(bool useMergedAssemblyModuleHandler = false)
		{
			InitializeComponent();
			DoubleBuffered = true;

			var nsh = new NotSupportedHandler();

			if (useMergedAssemblyModuleHandler)
				_handlers.Add(new MergedAssemblyModuleDefinitionHandler());
			else
			{
				_handlers.Add(new AssemblyDefinitionHandler());
				_handlers.Add(new ModuleDefinitionHandler());
			}

			_handlers.Add(new AssemblyNameReferenceHandler());
			_handlers.Add(new TypeDefinitionHandler());
			_handlers.Add(new MethodDefinitionHandler());
			_handlers.Add(new PropertyDefinitionHandler());
			_handlers.Add(new FieldDefinitionHandler());
			_handlers.Add(new EventDefinitionHandler());
			_handlers.Add(new EmbeddedResourceHandler());
			_handlers.Add(new LinkedResourceHandler());
			_handlers.Add(new AssemblyLinkedResourceHandler());
			_handlers.Add(nsh);

			foreach (var handler in _handlers)
			{
				var control = handler as Control;
				if (control != null)
					control.Dock = DockStyle.Fill;

				if (handler != nsh)
				{
					nsh.LabInfo.Text += @" - " + handler.Label + Environment.NewLine;
				}
			}

#if DEBUG
			PGrid.Visible = true;
#endif
		}

		/// <summary>
		/// Handle browser tree item
		/// </summary>
		/// <param name="item">Item to handle</param>
		public IHandler HandleItem(object item)
		{
			foreach (var handler in _handlers)
			{
				if (!handler.IsItemHandled(item))
					continue;

				handler.HandleItem(item);

				var control = handler as Control;
				if (control != null)
				{
					if (!(GroupBox.Controls.Count > 0 && GroupBox.Controls[0].Equals(control)))
					{
						GroupBox.Controls.Clear();
						GroupBox.Controls.Add(control);
					}
				}

				GroupBox.Text = handler.Label;

				return handler;
			}
			return null;
		}

		/// <summary>
		/// Handle configure button click 
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">attributes</param>
		private void Configure_Click(object sender, EventArgs e)
		{
			using (var frm = new ConfigureForm())
			{
				if (frm.ShowDialog() != DialogResult.OK)
					return;

				foreach (var handler in _handlers)
					handler.OnConfigurationChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Handle Strong Name button click 
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">attributes</param>
		private void SNRemover_Click(object sender, EventArgs e)
		{
			using (var frm = new StrongNameRemoverForm())
				frm.ShowDialog();
		}

		/// <summary>
		/// Debug PGrid button click 
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">attributes</param>
		private void PGrid_Click(object sender, EventArgs e)
		{
			using (var frm = new PropertyGridForm())
				frm.ShowDialog();
		}

		#endregion
	}
}