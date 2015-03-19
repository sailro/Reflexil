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
	public partial class CreateInstructionForm
	{
		#region Events

		private void ButInsertBefore_Click(Object sender, EventArgs e)
		{
			var newins = CreateInstruction();
			if (newins != null)
				MethodDefinition.Body.GetILProcessor().InsertBefore(SelectedInstruction, newins);
		}

		private void ButInsertAfter_Click(Object sender, EventArgs e)
		{
			var newins = CreateInstruction();
			if (newins != null)
				MethodDefinition.Body.GetILProcessor().InsertAfter(SelectedInstruction, newins);
		}

		private void ButAppend_Click(Object sender, EventArgs e)
		{
			var newins = CreateInstruction();
			if (newins != null)
				MethodDefinition.Body.GetILProcessor().Append(newins);
		}

		protected override void Operands_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.Operands_SelectedIndexChanged(sender, e);
			if (MethodDefinition == null)
				return;

			ButInsertBefore.Enabled = (SelectedInstruction != null) && !((Operands.SelectedItem) is NotSupportedOperandEditor);
			ButInsertAfter.Enabled = (SelectedInstruction != null) && !((Operands.SelectedItem) is NotSupportedOperandEditor);
			ButAppend.Enabled = !((Operands.SelectedItem) is NotSupportedOperandEditor);
		}

		private void CreateForm_Load(Object sender, EventArgs e)
		{
			Operands_SelectedIndexChanged(this, EventArgs.Empty);
			OpCodes_SelectedIndexChanged(this, EventArgs.Empty);
		}

		#endregion

		#region Methods

		public CreateInstructionForm()
		{
			InitializeComponent();
		}

		public override DialogResult ShowDialog(MethodDefinition mdef, Instruction selected)
		{
			FillControls(mdef);
			return base.ShowDialog(mdef, selected);
		}

		#endregion
	}
}