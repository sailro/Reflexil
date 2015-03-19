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

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;

#endregion

namespace Reflexil.Editors
{
	public class MultipleInstructionReferenceEditor : BasePopupControl, IOperandEditor<Instruction[]>
	{
		#region Fields

		private MethodDefinition _mdef;
		private readonly List<Instruction> _instructions;
		private List<Instruction> _selectedInstructions;

		#endregion

		#region Properties

		public string Label
		{
			get { return "-> Multiple instructions references"; }
		}

		public string ShortLabel
		{
			get { return Label; }
		}

		public List<Instruction> SelectedInstructions
		{
			get { return _selectedInstructions; }
			set
			{
				_selectedInstructions = value;
				var count = 0;

				if (_selectedInstructions != null)
					count = _selectedInstructions.Count;

				Text = string.Format("{0} instruction(s)", count);
			}
		}

		object IOperandEditor.SelectedOperand
		{
			get { return SelectedOperand; }
			set { SelectedOperand = (Instruction[]) value; }
		}

		public Instruction[] SelectedOperand
		{
			get { return SelectedInstructions.ToArray(); }
			set { SelectedInstructions = new List<Instruction>(value); }
		}

		#endregion

		#region Events

		protected override void OnClick(System.EventArgs e)
		{
			using (var selectform = new InstructionSelectForm(_mdef, _instructions, _selectedInstructions))
			{
				if (selectform.ShowDialog() == DialogResult.OK)
				{
					SelectedInstructions = selectform.SelectedInstructions;
				}
			}
		}

		#endregion

		#region Methods

		public MultipleInstructionReferenceEditor()
		{
		}

		public bool IsOperandHandled(object operand)
		{
			return (operand) is Instruction[];
		}

		public MultipleInstructionReferenceEditor(ICollection instructions)
		{
			_instructions = new List<Instruction>();
			if (instructions != null)
			{
				foreach (Instruction ins in instructions)
				{
					_instructions.Add(ins);
				}
			}

			SelectedInstructions = new List<Instruction>();

			// ReSharper disable once DoNotCallOverridableMethodsInConstructor
			Dock = DockStyle.Fill;
		}

		public Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return worker.Create(opcode, _selectedInstructions.ToArray());
		}

		public void Initialize(MethodDefinition mdef)
		{
			_mdef = mdef;
		}

		#endregion
	}
}