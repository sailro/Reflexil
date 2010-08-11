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
	
	public partial class CreateInstructionForm
	{
		
		#region " Events "
		private void ButInsertBefore_Click(System.Object sender, System.EventArgs e)
		{
			Instruction newins = CreateInstruction();
			if (newins != null)
			{
                MethodDefinition.Body.GetILProcessor().InsertBefore(SelectedInstruction, newins);
			}
		}
		
		private void ButInsertAfter_Click(System.Object sender, System.EventArgs e)
		{
			Instruction newins = CreateInstruction();
			if (newins != null)
			{
                MethodDefinition.Body.GetILProcessor().InsertAfter(SelectedInstruction, newins);
			}
		}
		
		private void ButAppend_Click(System.Object sender, System.EventArgs e)
		{
			Instruction newins = CreateInstruction();
			if (newins != null)
			{
                MethodDefinition.Body.GetILProcessor().Append(newins);
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

