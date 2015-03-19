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
using Reflexil.Forms;

#endregion

namespace Reflexil.Editors
{
	public partial class OverrideGridControl : BaseOverrideGridControl
	{
		#region Methods

		public OverrideGridControl()
		{
			InitializeComponent();
		}

		protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
		{
			MenCreate.Enabled = (!ReadOnly) && (OwnerDefinition != null);
			MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
			MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
			MenDeleteAll.Enabled = (!ReadOnly) && (OwnerDefinition != null);
		}

		protected override void MenCreate_Click(object sender, EventArgs e)
		{
			using (var createForm = new CreateOverrideForm())
			{
				if (createForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
				{
					RaiseGridUpdated();
				}
			}
		}

		protected override void MenEdit_Click(object sender, EventArgs e)
		{
			using (var editForm = new EditOverrideForm())
			{
				if (editForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
				{
					RaiseGridUpdated();
				}
			}
		}

		protected override void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < Grid.Rows.Count && e.RowIndex < OwnerDefinition.Overrides.Count)
			{
				e.Value = OwnerDefinition.Overrides[e.RowIndex];
			}
		}

		protected override void MenDelete_Click(object sender, EventArgs e)
		{
			foreach (var mref in SelectedItems)
			{
				OwnerDefinition.Overrides.Remove(mref);
			}
			RaiseGridUpdated();
		}

		protected override void MenDeleteAll_Click(object sender, EventArgs e)
		{
			OwnerDefinition.Overrides.Clear();
			RaiseGridUpdated();
		}

		protected override void DoDragDrop(object sender, DataGridViewRow sourceRow, DataGridViewRow targetRow,
			DragEventArgs e)
		{
			var sourceExc = sourceRow.DataBoundItem as MethodReference;
			var targetExc = targetRow.DataBoundItem as MethodReference;

			if (sourceExc == targetExc)
				return;

			OwnerDefinition.Overrides.Remove(sourceExc);
			OwnerDefinition.Overrides.Insert(targetRow.Index, sourceExc);
			RaiseGridUpdated();
		}

		public override void Bind(MethodDefinition mdef)
		{
			base.Bind(mdef);
			BindingSource.DataSource = mdef != null ? mdef.Overrides : null;
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseOverrideGridControl : GridControl<MethodReference, MethodDefinition>
	{
	}

	#endregion
}