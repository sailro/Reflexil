
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Windows.Forms;
using Reflexil.Handlers;
using Reflexil.Utils;
using Reflexil.Editors;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Forms
{
	
	public partial class InstructionForm 
	{
		
		#region " Fields "
		private MethodDefinitionHandler m_handler;
		#endregion
		
		#region " Properties "
        public MethodDefinitionHandler Handler
        {
            get
            {
                return m_handler;
            }
        }
		#endregion
		
		#region " Events "
		protected virtual void Operands_SelectedIndexChanged(object sender, EventArgs e)
		{
			OperandPanel.Controls.Clear();
			OperandPanel.Controls.Add((Control) Operands.SelectedItem);
			if (Handler != null)
			{
				((IGlobalOperandEditor) Operands.SelectedItem).Initialize(Handler.MethodDefinition);
			}
		}
		
		protected virtual void OpCodes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (OpCodes.SelectedItem != null)
            {
                RtbOpCodeDesc.Text = DataManager.GetInstance().GetOpcodeDesc((OpCode)OpCodes.SelectedItem);
            }
		}

        private void OpCodes_TextChanged(object sender, EventArgs e)
        {
            if (OpCodes.SelectedItem == null)
            {
                RtbOpCodeDesc.Text = "Unknown opcode";
            }
        }
		#endregion
		
		#region " Methods "
        public InstructionForm() : base()
        {
            InitializeComponent();
        }

		public void FillControls(MethodDefinitionHandler handler)
		{
			OpCodeBindingSource.DataSource = DataManager.GetInstance().GetAllOpCodes();
			OpCodes.SelectedIndex = 0;
			
			Operands.Items.Add(new NullOperandEditor());
			Operands.Items.Add(new ByteEditor());
			Operands.Items.Add(new SByteEditor());
			Operands.Items.Add(new IntegerEditor());
			Operands.Items.Add(new LongEditor());
			Operands.Items.Add(new SingleEditor());
			Operands.Items.Add(new DoubleEditor());
			Operands.Items.Add(new StringEditor());

            if (handler.MethodDefinition.HasBody)
			{
                Operands.Items.Add(new InstructionReferenceEditor(handler.MethodDefinition.Body.Instructions));
                Operands.Items.Add(new MultipleInstructionReferenceEditor(handler.MethodDefinition.Body.Instructions));
                Operands.Items.Add(new VariableReferenceEditor(handler.MethodDefinition.Body.Variables));
			}
			else
			{
				Operands.Items.Add(new GenericOperandReferenceEditor<Instruction, InstructionWrapper>(null));
				Operands.Items.Add(new MultipleInstructionReferenceEditor(null));
				Operands.Items.Add(new GenericOperandReferenceEditor<VariableDefinition, VariableWrapper>(null));
			}

            Operands.Items.Add(new ParameterReferenceEditor(handler.MethodDefinition.Parameters));
			Operands.Items.Add(new FieldReferenceEditor());
			Operands.Items.Add(new MethodReferenceEditor());
			Operands.Items.Add(new GenericTypeReferenceEditor());
			Operands.Items.Add(new TypeReferenceEditor());
			Operands.Items.Add(new NotSupportedOperandEditor());
			
			Operands.SelectedIndex = 0;
		}
		
		public virtual DialogResult ShowDialog(MethodDefinitionHandler handler)
		{
			m_handler = handler;
			return base.ShowDialog();
		}
		
		protected Instruction CreateInstruction()
		{
			try
			{
                if (OpCodes.SelectedItem != null)
                {
                    IGlobalOperandEditor editor = (IGlobalOperandEditor)Operands.SelectedItem;
                    Instruction ins = editor.CreateInstruction(Handler.MethodDefinition.Body.CilWorker, ((OpCode)OpCodes.SelectedItem));
                    return ins;
                }
                else
                {
                    MessageBox.Show("Unknown opcode");
                    return null;
                }
			}
			catch (Exception)
			{
				MessageBox.Show("Reflexil is unable to create this instruction, check coherence between the opcode and the operand");
				return null;
			}
		}
		#endregion

    }
	
}


