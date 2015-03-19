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
using Reflexil.Utils;

#endregion

namespace Reflexil.Editors
{
	public partial class InstructionGridControl : BaseInstructionGridControl
	{
		#region Methods

		public InstructionGridControl()
		{
			InitializeComponent();
		}

		protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
		{
			MenCreate.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
			MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
			MenReplaceBody.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
			MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
			MenDeleteAll.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
		}

		protected override void MenCreate_Click(object sender, EventArgs e)
		{
			using (var createForm = new CreateInstructionForm())
			{
				if (createForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
				{
					RaiseGridUpdated();
				}
			}
		}

		protected override void MenEdit_Click(object sender, EventArgs e)
		{
			using (var editForm = new EditInstructionForm())
			{
				if (editForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
				{
					RaiseGridUpdated();
				}
			}
		}

		protected override void MenDelete_Click(object sender, EventArgs e)
		{
			foreach (var ins in SelectedItems)
			{
				OwnerDefinition.Body.GetILProcessor().Remove(ins);
			}
			RaiseGridUpdated();
		}

		protected override void MenDeleteAll_Click(object sender, EventArgs e)
		{
			OwnerDefinition.Body.Instructions.Clear();
			RaiseGridUpdated();
		}

		protected override void DoDragDrop(object sender, DataGridViewRow sourceRow, DataGridViewRow targetRow,
			DragEventArgs e)
		{
			var sourceIns = sourceRow.DataBoundItem as Instruction;
			var targetIns = targetRow.DataBoundItem as Instruction;

			if (sourceIns != targetIns)
			{
				OwnerDefinition.Body.GetILProcessor().Remove(sourceIns);
				if (sourceRow.Index > targetRow.Index)
				{
					OwnerDefinition.Body.GetILProcessor().InsertBefore(targetIns, sourceIns);
				}
				else
				{
					OwnerDefinition.Body.GetILProcessor().InsertAfter(targetIns, sourceIns);
				}
				RaiseGridUpdated();
			}
		}

		public override void Bind(MethodDefinition mdef)
		{
			base.Bind(mdef);
			if ((mdef != null) && (mdef.Body != null))
			{
				BindingSource.DataSource = mdef.Body.Instructions;
			}
			else
			{
				BindingSource.DataSource = null;
			}
		}

		#endregion

		#region Events

		public delegate void BodyReplacedEventHandler(object sender, EventArgs e);

		public event BodyReplacedEventHandler BodyReplaced;

		private void MenReplaceBody_Click(object sender, EventArgs e)
		{
			using (var codeForm = new CodeForm(OwnerDefinition))
			{
				if (codeForm.ShowDialog(this) == DialogResult.OK)
				{
					CecilHelper.CloneMethodBody(codeForm.MethodDefinition, OwnerDefinition);
					if (BodyReplaced != null) BodyReplaced(this, EventArgs.Empty);
				}
			}
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseInstructionGridControl : GridControl<Instruction, MethodDefinition>
	{
	}

	#endregion
}