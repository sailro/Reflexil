
#region " Imports "
using System;
using System.Windows.Forms;
using Reflexil.Editors;
using Reflexil.Handlers;
using Mono.Cecil.Cil;
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
				Handler.MethodDefinition.Body.CilWorker.Replace(Handler.SelectedInstruction, newins);
			}
		}
		
		protected override void Operands_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.Operands_SelectedIndexChanged(sender, e);
			if (Handler != null)
			{
				ButUpdate.Enabled = Handler.SelectedInstruction != null && ! ((Operands.SelectedItem) is NotSupportedOperandEditor);
			}
		}
		
		private void EditForm_Load(Object sender, EventArgs e)
		{
			Operands_SelectedIndexChanged(this, EventArgs.Empty);
			OpCodes_SelectedIndexChanged(this, EventArgs.Empty);
			if (Handler.SelectedInstruction != null&& Handler.SelectedInstruction.Operand != null)
			{
				((IGlobalOperandEditor) Operands.SelectedItem).SelectOperand(Handler.SelectedInstruction.Operand);
			}
		}
		#endregion
		
		#region " Methods "
        public EditInstructionForm() : base()
        {
            InitializeComponent();
        }

		public override DialogResult ShowDialog(MethodDefinitionHandler handler)
		{
			FillControls(handler);
			if (handler.SelectedInstruction != null)
			{
				foreach (IGlobalOperandEditor editor in Operands.Items)
				{
                    if (handler.SelectedInstruction.Operand != null)
					{
                        if (editor.IsOperandHandled(handler.SelectedInstruction.Operand))
						{
							Operands.SelectedItem = editor;
							Operands_SelectedIndexChanged(this, EventArgs.Empty);
							break;
						}
					}
				}
                OpCodes.SelectedItem = handler.SelectedInstruction.OpCode;
			}
			
			return base.ShowDialog(handler);
		}
		#endregion
		
	}
	
}


