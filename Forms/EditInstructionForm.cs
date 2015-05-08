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
using Reflexil.Editors;

#endregion

namespace Reflexil.Forms
{
	public partial class EditInstructionForm
	{
		#region Events

		private void ButUpdate_Click(Object sender, EventArgs e)
		{
			var newins = CreateInstruction();
			if (newins != null)
				MethodDefinition.Body.GetILProcessor().Replace(SelectedInstruction, newins);
		}

		protected override void Operands_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.Operands_SelectedIndexChanged(sender, e);
			if (MethodDefinition != null)
				ButUpdate.Enabled = (SelectedInstruction != null) && ! ((Operands.SelectedItem) is NotSupportedOperandEditor);
		}

		private void EditForm_Load(Object sender, EventArgs e)
		{
			Operands_SelectedIndexChanged(this, EventArgs.Empty);
			OpCodes_SelectedIndexChanged(this, EventArgs.Empty);
			if ((SelectedInstruction != null) && (SelectedInstruction.Operand != null))
				((IOperandEditor) Operands.SelectedItem).SelectedOperand = SelectedInstruction.Operand;
		}

		#endregion

		#region Methods

		public EditInstructionForm()
		{
			InitializeComponent();
		}

		public override DialogResult ShowDialog(MethodDefinition mdef, Instruction selected)
		{
			FillControls(mdef);

			if (selected != null)
			{
				foreach (IOperandEditor editor in Operands.Items)
				{
					if (selected.Operand == null)
						continue;

					if (!editor.IsOperandHandled(selected.Operand))
						continue;

					Operands.SelectedItem = editor;
					Operands_SelectedIndexChanged(this, EventArgs.Empty);
					break;
				}
				OpCodes.SelectedItem = selected.OpCode;
			}

			return base.ShowDialog(mdef, selected);
		}

		#endregion
	}
}