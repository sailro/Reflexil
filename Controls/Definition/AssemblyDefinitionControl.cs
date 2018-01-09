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

using System;
using System.Windows.Forms;
using Mono.Cecil;

namespace Reflexil.Editors
{
	public partial class AssemblyDefinitionControl : UserControl
	{
		private bool _readonly;

		public bool ReadOnly
		{
			get { return _readonly; }
			set
			{
				_readonly = value;
				Enabled = !value;
			}
		}

		public AssemblyDefinition Item { get; set; }

		private void ResetEntryPoint_Click(object sender, EventArgs e)
		{
			MethodDefinitionEditor.SelectedOperand = null;
			if (Item != null)
				Item.EntryPoint = null;
		}

		private void MethodDefinitionEditor_Validated(object sender, EventArgs e)
		{
			// No need to import, assembly is restricted in this case
			if (Item != null)
				Item.EntryPoint = MethodDefinitionEditor.SelectedOperand;
		}

		public AssemblyDefinitionControl()
		{
			InitializeComponent();
			MethodDefinitionEditor.Dock = DockStyle.None;
		}

		public virtual void Bind(AssemblyDefinition item)
		{
			Item = item;

			if (item != null)
			{
				MainModule.DataSource = null; // force reloading in case of module rename
				MainModule.DataSource = item.Modules;
				MainModule.SelectedItem = item.MainModule;
				MethodDefinitionEditor.SelectedOperand = item.EntryPoint;
				MethodDefinitionEditor.AssemblyRestriction = item;
			}
			else
			{
				MainModule.SelectedIndex = -1;
				MethodDefinitionEditor.SelectedOperand = null;
			}

			if (!ReadOnly)
			{
				Enabled = (item != null);
			}
		}
	}
}