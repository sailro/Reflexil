
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
    public partial class ParameterGridControl : Reflexil.Editors.GridControl<ParameterDefinition>
    {

        #region " Methods "
        public ParameterGridControl()
        {
            InitializeComponent();
        }

        protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
        {
            MenCreate.Enabled = false;
            MenEdit.Enabled = false;
            MenDelete.Enabled = false;
            MenDeleteAll.Enabled = false;
        }

        protected override void MenCreate_Click(object sender, EventArgs e)
        {
        }

        protected override void MenEdit_Click(object sender, EventArgs e)
        {
        }

        protected override void MenDelete_Click(object sender, EventArgs e)
        {
        }

        protected override void MenDeleteAll_Click(object sender, EventArgs e)
        {
        }

        protected override void DoDragDrop(object sender, System.Windows.Forms.DataGridViewRow sourceRow, System.Windows.Forms.DataGridViewRow targetRow, System.Windows.Forms.DragEventArgs e)
        {
        }

        public override void Bind(MethodDefinition mdef)
        {
            base.Bind(mdef);
            if (mdef != null)
            {
                BindingSource.DataSource = mdef.Parameters;
            }
            else
            {
                BindingSource.DataSource = null;
            }
        }
        #endregion

    }
}

