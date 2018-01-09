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
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Wrappers;

namespace Reflexil.Forms
{
	public partial class InstructionSelectForm
	{
		public List<Instruction> SelectedInstructions
		{
			get { return (from InstructionWrapper wrapper in LbxSelection.Items select wrapper.Item).ToList(); }
		}

		private void LbxInstructions_DoubleClick(object sender, EventArgs e)
		{
			if (LbxInstructions.SelectedItem != null)
			{
				LbxSelection.Items.Add(((ICloneable) LbxInstructions.SelectedItem).Clone());
			}
		}

		private void LbxSelection_DoubleClick(object sender, EventArgs e)
		{
			if (LbxSelection.SelectedItem != null)
			{
				LbxSelection.Items.Remove(LbxSelection.SelectedItem);
			}
		}

		private void ButTop_Click(object sender, EventArgs e)
		{
			MoveSelection(0);
		}

		private void ButUp_Click(object sender, EventArgs e)
		{
			MoveSelection(LbxSelection.SelectedIndex - 1);
		}

		private void ButDown_Click(object sender, EventArgs e)
		{
			MoveSelection(LbxSelection.SelectedIndex + 1);
		}

		private void ButBottom_Click(object sender, EventArgs e)
		{
			MoveSelection(LbxSelection.Items.Count - 1);
		}

		public InstructionSelectForm()
		{
			InitializeComponent();
		}

		public InstructionSelectForm(MethodDefinition mdef, IEnumerable<Instruction> instructions, IEnumerable<Instruction> selectedinstructions)
		{
			InitializeComponent();

			foreach (var ins in instructions)
				LbxInstructions.Items.Add(new InstructionWrapper(ins, mdef));

			foreach (var ins in selectedinstructions)
				LbxSelection.Items.Add(new InstructionWrapper(ins, mdef));
		}

		private void MoveSelection(int newindex)
		{
			var selection = LbxSelection.SelectedItem;
			if ((selection == null) || (newindex < 0) || (newindex >= LbxSelection.Items.Count))
				return;

			LbxSelection.Items.Remove(selection);
			LbxSelection.Items.Insert(newindex, selection);
			LbxSelection.SelectedIndex = newindex;
		}
	}
}