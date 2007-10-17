
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
	
	public partial class CreateInstructionForm
	{
		
		#region " Events "
		private void ButInsertBefore_Click(System.Object sender, System.EventArgs e)
		{
			Instruction newins = CreateInstruction();
			if (newins != null)
			{
                MethodDefinition.Body.CilWorker.InsertBefore(SelectedInstruction, newins);
			}
		}
		
		private void ButInsertAfter_Click(System.Object sender, System.EventArgs e)
		{
			Instruction newins = CreateInstruction();
			if (newins != null)
			{
                MethodDefinition.Body.CilWorker.InsertAfter(SelectedInstruction, newins);
			}
		}
		
		private void ButAppend_Click(System.Object sender, System.EventArgs e)
		{
			Instruction newins = CreateInstruction();
			if (newins != null)
			{
                MethodDefinition.Body.CilWorker.Append(newins);
			}
		}

        protected override void Operands_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            base.Operands_SelectedIndexChanged(sender, e);
            if (MethodDefinition != null)
            {
                ButInsertBefore.Enabled = (SelectedInstruction != null) && !((Operands.SelectedItem) is NotSupportedOperandEditor);
                ButInsertAfter.Enabled = (SelectedInstruction != null) && !((Operands.SelectedItem) is NotSupportedOperandEditor);
                ButAppend.Enabled = !((Operands.SelectedItem) is NotSupportedOperandEditor);
            }
        }
		
		private void CreateForm_Load(System.Object sender, System.EventArgs e)
		{
			Operands_SelectedIndexChanged(this, EventArgs.Empty);
			OpCodes_SelectedIndexChanged(this, EventArgs.Empty);
		}
		#endregion
		
		#region " Methods "
        public CreateInstructionForm() : base()
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

