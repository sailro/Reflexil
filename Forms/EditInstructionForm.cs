/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
                ((IOperandEditor)Operands.SelectedItem).SelectedOperand = SelectedInstruction.Operand;
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
                foreach (IOperandEditor editor in Operands.Items)
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


