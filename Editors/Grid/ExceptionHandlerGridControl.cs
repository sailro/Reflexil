
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;
using System.Windows.Forms;
#endregion

namespace Reflexil.Editors
{
    public partial class ExceptionHandlerGridControl : Reflexil.Editors.GridControl<ExceptionHandler>
    {

        #region " Methods "
        public ExceptionHandlerGridControl()
        {
            InitializeComponent();
        }

        protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
        {
            MenCreate.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
            MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
            MenDeleteAll.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
        }

        protected override void MenCreate_Click(object sender, EventArgs e)
        {
            using (CreateExceptionHandlerForm createForm = new CreateExceptionHandlerForm())
            {
                if (createForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenEdit_Click(object sender, EventArgs e)
        {
            using (EditExceptionHandlerForm editForm = new EditExceptionHandlerForm())
            {
                if (editForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenDelete_Click(object sender, EventArgs e)
        {
            foreach (ExceptionHandler handler in SelectedItems)
            {
                MethodDefinition.Body.ExceptionHandlers.Remove(handler);
            }
            RaiseGridUpdated();
        }

        protected override void MenDeleteAll_Click(object sender, EventArgs e)
        {
            MethodDefinition.Body.ExceptionHandlers.Clear();
            RaiseGridUpdated();
        }

        protected override void DoDragDrop(object sender, System.Windows.Forms.DataGridViewRow sourceRow, System.Windows.Forms.DataGridViewRow targetRow, System.Windows.Forms.DragEventArgs e)
        {
            ExceptionHandler sourceExc = sourceRow.DataBoundItem as ExceptionHandler;
            ExceptionHandler targetExc = targetRow.DataBoundItem as ExceptionHandler;

            if (sourceExc != targetExc)
            {
                MethodDefinition.Body.ExceptionHandlers.Remove(sourceExc);
                MethodDefinition.Body.ExceptionHandlers.Insert(targetRow.Index, sourceExc);
                RaiseGridUpdated();
            }
        }

        public override void Bind(MethodDefinition mdef)
        {
            base.Bind(mdef);
            if ((mdef != null) && (mdef.Body != null))
            {
                BindingSource.DataSource = mdef.Body.ExceptionHandlers;
            }
            else
            {
                BindingSource.DataSource = null;
            }
        }
#endregion

    }
}

