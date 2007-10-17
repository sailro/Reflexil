
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;
using Reflexil.Utils;
using Reflexil.Wrappers;
using System.Windows.Forms;
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

