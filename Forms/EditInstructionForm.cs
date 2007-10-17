
#region " Imports "
using System;
using System.Windows.Forms;
using Reflexil.Editors;
using Reflexil.Handlers;
using Mono.Cecil.Cil;
using Mono.Cecil;
#endregion

namespace Reflexil.Forms
{
	
	public partial class EditInstructionForm
	{
		
		#region " Events "
		private void ButUpdate_Click(Object sender, EventArgs e)
		{
			Instruction newins = CreateInstruction();
			if (newins != null)
			{
                MethodDefinition.Body.CilWorker.Replace(SelectedInstruction, newins);
			}
		}
		
		protected override void Operands_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.Operands_SelectedIndexChanged(sender, e);
			if (MethodDefinition != null)
			{
                ButUpdate.Enabled = (SelectedInstruction != null) && ! ((Operands.SelectedItem) is NotSupportedOperandEditor);
			}
		}

        private void EditForm_Load(Object sender, EventArgs e)
        {
            Operands_SelectedIndexChanged(this, EventArgs.Empty);
            OpCodes_SelectedIndexChanged(this, EventArgs.Empty);
            if ((SelectedInstruction != null) && (SelectedInstruction.Operand != null))
            {
                ((IGlobalOperandEditor)Operands.SelectedItem).SelectOperand(SelectedInstruction.Operand);
            }
        }
		#endregion
		
		#region " Methods "
        public EditInstructionForm() : base()
        {
            InitializeComponent();
        }

        public override DialogResult ShowDialog(MethodDefinition mdef, Instruction selected)
		{
            FillControls(mdef);
            if (selected != null)
            {
                foreach (IGlobalOperandEditor editor in Operands.Items)
                {
                    if (selected.Operand != null)
                    {
                        if (editor.IsOperandHandled(selected.Operand))
                        {
                            Operands.SelectedItem = editor;
                            Operands_SelectedIndexChanged(this, EventArgs.Empty);
                            break;
                        }
                    }
                }
                OpCodes.SelectedItem = selected.OpCode;
            }

            return base.ShowDialog(mdef, selected);
		}
		#endregion
		
	}
	
}


