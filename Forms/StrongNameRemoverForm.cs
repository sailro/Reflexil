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
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Mono.Cecil;

#endregion

namespace Reflexil.Forms
{
	public partial class StrongNameRemoverForm : Form
	{
		#region Fields

		private AssemblyDefinition _adef;

		#endregion

		#region Properties

		public AssemblyDefinition AssemblyDefinition
		{
			get { return _adef; }
			set
			{
				_adef = value;
				Add.Enabled = AutoScan.Enabled = Process.Enabled = _adef != null;
				if (_adef != null)
				{
					SNAssembly.Text = _adef.ToString();
					Tooltip.SetToolTip(SNAssembly, value.MainModule.Image.FileName);
				}
				else
				{
					SNAssembly.Text = string.Empty;
					Tooltip.SetToolTip(SNAssembly, null);
				}
			}
		}

		#endregion

		#region Methods

		public StrongNameRemoverForm()
		{
			InitializeComponent();
		}

		private static AssemblyDefinition LoadAssembly(string filename)
		{
			try
			{
				return AssemblyDefinition.ReadAssembly(filename);
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("Reflexil is unable to load this assembly: {0}", ex.Message));
			}
			return null;
		}

		#endregion

		#region Events

		private void Add_Click(object sender, EventArgs e)
		{
			OpenFileDialog.Multiselect = true;

			if (OpenFileDialog.ShowDialog() != DialogResult.OK)
				return;

			foreach (var asmdef in OpenFileDialog.FileNames.Select(LoadAssembly).Where(asmdef => asmdef != null))
				ReferencingAssemblies.Items.Add(asmdef);
		}

		private void ReferencingAssemblies_SelectedIndexChanged(object sender, EventArgs e)
		{
			Remove.Enabled = ReferencingAssemblies.SelectedItems.Count > 0;
		}

		private void Remove_Click(object sender, EventArgs e)
		{
			IEnumerable assemblies = new ArrayList(ReferencingAssemblies.SelectedItems);
			foreach (AssemblyDefinition asmdef in assemblies)
			{
				ReferencingAssemblies.Items.Remove(asmdef);
			}
		}

		private void SelectSNAssembly_Click(object sender, EventArgs e)
		{
			OpenFileDialog.Multiselect = false;

			if (OpenFileDialog.ShowDialog() != DialogResult.OK)
				return;

			var loader = LoadAssembly(OpenFileDialog.FileName);
			if (loader != null)
				AssemblyDefinition = loader;
		}

		private void AutoScan_Click(object sender, EventArgs e)
		{
			if (AssemblyDefinition != null)
			{
				ReferencingAssemblies.Items.Clear();
				using (var frm = new DirectoryScanForm())
				{
					if (frm.ShowDialog(AssemblyDefinition) == DialogResult.OK)
						// ReSharper disable once CoVariantArrayConversion
						ReferencingAssemblies.Items.AddRange(frm.ReferencingAssemblies);
				}
			}
		}

		private void Process_Click(object sender, EventArgs e)
		{
			try
			{
				using (var frm = new ReferenceUpdaterForm())
				{
					var assemblies = new ArrayList(ReferencingAssemblies.Items) {AssemblyDefinition};
					frm.ShowDialog(assemblies.ToArray(typeof (AssemblyDefinition)) as AssemblyDefinition[]);
				}

				AssemblyDefinition = AssemblyDefinition;
				// Refresh hack
				ReferencingAssemblies.DisplayMember = GetType().Name;
				ReferencingAssemblies.DisplayMember = string.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("Reflexil is unable to save this assembly: {0}", ex.Message));
			}
		}

		private void ReferencingAssemblies_MouseMove(object sender, MouseEventArgs e)
		{
			var coords = new Point(e.X, e.Y);
			var index = ReferencingAssemblies.IndexFromPoint(coords);

			if (index > -1)
			{
				var adef = ReferencingAssemblies.Items[index] as AssemblyDefinition;
				Tooltip.SetToolTip(ReferencingAssemblies, adef != null ? adef.MainModule.Image.FileName : string.Empty);
			}
			else
				Tooltip.SetToolTip(ReferencingAssemblies, string.Empty);
		}

		#endregion
	}
}