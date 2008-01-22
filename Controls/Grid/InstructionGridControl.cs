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
using System;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;
using Reflexil.Utils;
#endregion

namespace Reflexil.Editors
{
    public partial class InstructionGridControl : Reflexil.Editors.GridControl<Instruction>
    {

        #region " Methods "
        public InstructionGridControl()
        {
            InitializeComponent();
        }

        protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
        {
            MenCreate.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
            MenReplaceBody.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
            MenDeleteAll.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
        }

        protected override void MenCreate_Click(object sender, EventArgs e)
        {
            using (CreateInstructionForm createForm = new CreateInstructionForm())
            {
                if (createForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenEdit_Click(object sender, EventArgs e)
        {
            using (EditInstructionForm editForm = new EditInstructionForm())
            {
                if (editForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenDelete_Click(object sender, EventArgs e)
        {
            foreach (Instruction ins in SelectedItems)
            {
                MethodDefinition.Body.CilWorker.Remove(ins);
            }
            RaiseGridUpdated();
        }

        protected override void MenDeleteAll_Click(object sender, EventArgs e)
        {
            MethodDefinition.Body.Instructions.Clear();
            RaiseGridUpdated();
        }

        protected override void DoDragDrop(object sender, System.Windows.Forms.DataGridViewRow sourceRow, System.Windows.Forms.DataGridViewRow targetRow, System.Windows.Forms.DragEventArgs e)
        {
            Instruction sourceIns = sourceRow.DataBoundItem as Instruction;
            Instruction targetIns = targetRow.DataBoundItem as Instruction;

            if (sourceIns != targetIns)
            {
                MethodDefinition.Body.CilWorker.Remove(sourceIns);
                if (sourceRow.Index > targetRow.Index)
                {
                    MethodDefinition.Body.CilWorker.InsertBefore(targetIns, sourceIns);
                }
                else
                {
                    MethodDefinition.Body.CilWorker.InsertAfter(targetIns, sourceIns);
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
            using (CodeForm codeForm = new CodeForm(MethodDefinition))
            {
                if (codeForm.ShowDialog(this) == DialogResult.OK)
                {
                    CecilHelper.ImportMethodBody(codeForm.MethodDefinition, MethodDefinition);
                    if (BodyReplaced != null) BodyReplaced(this, EventArgs.Empty);
                }
            }
        }
        #endregion
        
    }
}

