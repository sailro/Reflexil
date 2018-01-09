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
using System.Collections.Generic;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;
using Reflexil.Utils;

namespace Reflexil.Editors
{
	public partial class InstructionGridControl : BaseInstructionGridControl
	{

		public InstructionGridControl()
		{
			InitializeComponent();
			_copiedItems = new List<Instruction>();
		}

		protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
		{
			MenCreate.Enabled = !ReadOnly && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
			MenEdit.Enabled = !ReadOnly && (FirstSelectedItem != null);
			MenReplaceBody.Enabled = !ReadOnly && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
			MenDelete.Enabled = !ReadOnly && (SelectedItems.Length > 0);
			MenDeleteAll.Enabled = !ReadOnly && (OwnerDefinition != null) && (OwnerDefinition.Body != null);

			MenCopy.Enabled = !ReadOnly && (SelectedItems.Length > 0);
			MenPaste.Enabled = !ReadOnly && (_copiedItems.Count > 0);
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

		protected override void DoDragDrop(object sender, DataGridViewRow sourceRow, DataGridViewRow targetRow, DragEventArgs e)
		{
			var sourceIns = sourceRow.DataBoundItem as Instruction;
			var targetIns = targetRow.DataBoundItem as Instruction;

			if (sourceIns == targetIns)
				return;

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

		public delegate void BodyReplacedEventHandler(object sender, EventArgs e);

		public event BodyReplacedEventHandler BodyReplaced;

		private void MenReplaceBody_Click(object sender, EventArgs e)
		{
			using (var codeForm = new CodeForm(OwnerDefinition))
			{
				if (codeForm.ShowDialog(this) != DialogResult.OK)
					return;

				try
				{
					CecilHelper.CloneMethodBody(codeForm.MethodDefinition, OwnerDefinition);
					if (BodyReplaced != null) BodyReplaced(this, EventArgs.Empty);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void MenReplaceNop_Click(object sender, EventArgs e)
		{
			foreach (var ins in SelectedItems)
			{
				ins.Operand = null;
				ins.OpCode = OpCodes.Nop;
			}
			RaiseGridUpdated();
		}

		private readonly List<Instruction> _copiedItems;

		private void MenCopy_Click(object sender, EventArgs e)
		{
			_copiedItems.Clear();
			foreach (var item in SelectedItems)
				_copiedItems.Add(new Instruction(item.OpCode, item.Operand));
		}

		private void MenPaste_Click(object sender, EventArgs e)
		{
			foreach (var item in _copiedItems)
			{
				var copy = new Instruction(item.OpCode, item.Operand);
				var processor = OwnerDefinition.Body.GetILProcessor();

				if (FirstSelectedItem != null)
					processor.InsertAfter(FirstSelectedItem, copy);
				else
					processor.Append(copy);
			}
			RaiseGridUpdated();
		}

	}

	public class BaseInstructionGridControl : GridControl<Instruction, MethodDefinition>
	{
	}
}