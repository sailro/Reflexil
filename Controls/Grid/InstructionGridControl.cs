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
using Reflexil.Forms;
using Reflexil.Utils;
#endregion

namespace Reflexil.Editors
{
    public partial class InstructionGridControl : BaseInstructionGridControl
    {

        #region " Methods "
        public InstructionGridControl()
        {
            InitializeComponent();
        }

        protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
        {
            MenCreate.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
            MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
            MenReplaceBody.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
            MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
            MenDeleteAll.Enabled = (!ReadOnly) && (OwnerDefinition != null) && (OwnerDefinition.Body != null);
        }

        protected override void MenCreate_Click(object sender, EventArgs e)
        {
            using (CreateInstructionForm createForm = new CreateInstructionForm())
            {
                if (createForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenEdit_Click(object sender, EventArgs e)
        {
            using (EditInstructionForm editForm = new EditInstructionForm())
            {
                if (editForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenDelete_Click(object sender, EventArgs e)
        {
            foreach (Instruction ins in SelectedItems)
            {
                OwnerDefinition.Body.GetILProcessor().Remove(ins);
            }
            RaiseGridUpdated();
        }

        protected override void MenDeleteAll_Click(object sender, EventArgs e)
        {
            OwnerDefinition.Body.Instructions.Clear();
            RaiseGridUpdated();
        }

        protected override void DoDragDrop(object sender, System.Windows.Forms.DataGridViewRow sourceRow, System.Windows.Forms.DataGridViewRow targetRow, System.Windows.Forms.DragEventArgs e)
        {
            Instruction sourceIns = sourceRow.DataBoundItem as Instruction;
            Instruction targetIns = targetRow.DataBoundItem as Instruction;

            if (sourceIns != targetIns)
            {
                OwnerDefinition.Body.GetILProcessor().Remove(sourceIns);
                if (sourceRow.Index > targetRow.Index)
                {
                    OwnerDefinition.Body.GetILProcessor().InsertBefore(targetIns, sourceIns);
                }
                else
                {
                    OwnerDefinition.Body.GetILProcessor().InsertAfter(targetIns, sourceIns);
                }
                RaiseGridUpdated();
            }
        }

        public override void Bind(MethodDefinition mdef)
        {
            base.Bind(mdef);
            if ((mdef != null) && (mdef.Body != null))
            {
                BindingSource.DataSource = mdef.Body.Instructions;
            }
            else
            {
                BindingSource.DataSource = null;
            }
        }
        #endregion

        #region " Events "
        public delegate void BodyReplacedEventHandler(object sender, EventArgs e);
        public event BodyReplacedEventHandler BodyReplaced;

        private void MenReplaceBody_Click(object sender, EventArgs e)
        {
            using (CodeForm codeForm = new CodeForm(OwnerDefinition))
            {
                if (codeForm.ShowDialog(this) == DialogResult.OK)
                {
                    CecilHelper.CloneMethodBody(codeForm.MethodDefinition, OwnerDefinition);
                    if (BodyReplaced != null) BodyReplaced(this, EventArgs.Empty);
                }
            }
        }
        #endregion
        
    }

    #region " VS Designer generic support "
    public class BaseInstructionGridControl : Reflexil.Editors.GridControl<Instruction, MethodDefinition>
    {
    }
    #endregion
}

