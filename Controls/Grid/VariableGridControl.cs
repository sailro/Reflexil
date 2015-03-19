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
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;

#endregion

namespace Reflexil.Editors
{
	public partial class VariableGridControl : BaseVariableGridControl
	{
		#region Methods

		public VariableGridControl()
		{
			InitializeComponent();
		}

		protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
		{
			MenCreate.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
			MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
			MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
			MenDeleteAll.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
		}

		protected override void MenCreate_Click(object sender, EventArgs e)
		{
			using (var createForm = new CreateVariableForm())
			{
				if (createForm.ShowDialog(OwnerDefinition, FirstSelectedItem) != DialogResult.OK)
					return;

				OwnerDefinition.Body.InitLocals = OwnerDefinition.Body.Variables.Count > 0;
				RaiseGridUpdated();
			}
		}

		protected override void MenEdit_Click(object sender, EventArgs e)
		{
			using (var editForm = new EditVariableForm())
			{
				if (editForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
				{
					RaiseGridUpdated();
				}
			}
		}

		protected override void MenDelete_Click(object sender, EventArgs e)
		{
			foreach (var var in SelectedItems)
			{
				OwnerDefinition.Body.Variables.Remove(var);
			}
			OwnerDefinition.Body.InitLocals = OwnerDefinition.Body.Variables.Count > 0;
			RaiseGridUpdated();
		}

		protected override void MenDeleteAll_Click(object sender, EventArgs e)
		{
			OwnerDefinition.Body.Variables.Clear();
			OwnerDefinition.Body.InitLocals = OwnerDefinition.Body.Variables.Count > 0;
			RaiseGridUpdated();
		}

		protected override void DoDragDrop(object sender, DataGridViewRow sourceRow, DataGridViewRow targetRow,
			DragEventArgs e)
		{
			var sourceExc = sourceRow.DataBoundItem as VariableDefinition;
			var targetExc = targetRow.DataBoundItem as VariableDefinition;

			if (sourceExc == targetExc)
				return;

			OwnerDefinition.Body.Variables.Remove(sourceExc);
			OwnerDefinition.Body.Variables.Insert(targetRow.Index, sourceExc);
			RaiseGridUpdated();
		}

		public override void Bind(MethodDefinition mdef)
		{
			base.Bind(mdef);
			if ((mdef != null) && (mdef.Body != null))
			{
				BindingSource.DataSource = mdef.Body.Variables;
			}
			else
			{
				BindingSource.DataSource = null;
			}
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseVariableGridControl : GridControl<VariableDefinition, MethodDefinition>
	{
	}

	#endregion
}