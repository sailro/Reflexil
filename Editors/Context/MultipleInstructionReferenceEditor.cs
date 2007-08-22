
#region " Imports "
using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Windows.Forms;
using Reflexil.Forms;
#endregion

namespace Reflexil.Editors
{
	
	public partial class MultipleInstructionReferenceEditor : BasePopupEditor, IOperandEditor
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
				LabCaption.Text = string.Format("{0} instruction(s)", count);
			}
		}
		#endregion
		
		#region " Events "
		protected override void OnSelectClick(System.Object sender, System.EventArgs e)
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
			SelectedInstructions = new List<Instruction>((Instruction[]) operand);
		}
		#endregion
		
	}
	
}

