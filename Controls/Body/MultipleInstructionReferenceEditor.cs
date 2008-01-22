/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;
#endregion

namespace Reflexil.Editors
{
	
	public partial class MultipleInstructionReferenceEditor : BasePopupControl, IOperandEditor<Instruction[]>
	{
		
		#region " Fields "
		private MethodDefinition m_mdef;
		private List<Instruction> m_instructions;
		private List<Instruction> m_selectedinstructions;
		#endregion
		
		#region " Properties "
		public bool IsOperandHandled(object operand)
		{
			return (operand) is Instruction[];
		}
		
		public string Label
		{
			get
			{
				return "-> Multiple instructions references";
			}
		}
		
		public List<Instruction> SelectedInstructions
		{
			get
			{
				return m_selectedinstructions;
			}
			set
			{
				m_selectedinstructions = value;
				int count = 0;
				if (m_selectedinstructions != null)
				{
					count = m_selectedinstructions.Count;
				}
				Text = string.Format("{0} instruction(s)", count);
			}
		}

        public Instruction[] SelectedOperand
        {
            get
            {
                return SelectedInstructions.ToArray();
            }
            set
            {
  			  SelectedInstructions = new List<Instruction>(value);
            }
        }
		#endregion
		
		#region " Events "
		protected override void OnClick(System.EventArgs e)
		{
			using (InstructionSelectForm selectform = new InstructionSelectForm(m_mdef, m_instructions, m_selectedinstructions))
			{
				if (selectform.ShowDialog() == DialogResult.OK)
				{
					SelectedInstructions = selectform.SelectedInstructions;
				}
			}
		}
		#endregion
		
		#region " Methods "
        public MultipleInstructionReferenceEditor() : base()
        {
        }

		public MultipleInstructionReferenceEditor(ICollection instructions)
		{
			
			m_instructions = new List<Instruction>();
			if (instructions != null)
			{
				foreach (Instruction ins in instructions)
				{
					m_instructions.Add(ins);
				}
			}
			
			SelectedInstructions = new List<Instruction>();
			
			this.Dock = DockStyle.Fill;
		}
		
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, m_selectedinstructions.ToArray());
		}
		
		public void Initialize(Mono.Cecil.MethodDefinition mdef)
		{
			m_mdef = mdef;
		}

        public void SelectOperand(object operand)
        {
            SelectedOperand = (Instruction[])operand;
        }

        public object CreateObject()
        {
            return SelectedOperand;
        }
		#endregion
		
	}
	
}

