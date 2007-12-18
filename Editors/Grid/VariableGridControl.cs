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
#endregion

namespace Reflexil.Editors
{
    public partial class VariableGridControl : Reflexil.Editors.GridControl<VariableDefinition>
    {

        #region " Methods "
        public VariableGridControl()
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
            using (CreateVariableForm createForm = new CreateVariableForm())
            {
                if (createForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenEdit_Click(object sender, EventArgs e)
        {
            using (EditVariableForm editForm = new EditVariableForm())
            {
                if (editForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenDelete_Click(object sender, EventArgs e)
        {
            foreach (VariableDefinition var in SelectedItems)
            {
                MethodDefinition.Body.Variables.Remove(var);
            }
            RaiseGridUpdated();
        }

        protected override void MenDeleteAll_Click(object sender, EventArgs e)
        {
            MethodDefinition.Body.Variables.Clear();
            RaiseGridUpdated();
        }

        protected override void DoDragDrop(object sender, System.Windows.Forms.DataGridViewRow sourceRow, System.Windows.Forms.DataGridViewRow targetRow, System.Windows.Forms.DragEventArgs e)
        {
            VariableDefinition sourceExc = sourceRow.DataBoundItem as VariableDefinition;
            VariableDefinition targetExc = targetRow.DataBoundItem as VariableDefinition;

            if (sourceExc != targetExc)
            {
                MethodDefinition.Body.Variables.Remove(sourceExc);
                MethodDefinition.Body.Variables.Insert(targetRow.Index, sourceExc);
                RaiseGridUpdated();
            }
        }

        public override void Bind(MethodDefinition mdef)
        {
            base.Bind(mdef);
            if ((mdef != null) && (mdef.Body != null))
            {
                BindingSource.DataSource = mdef.Body.Variables;
            }
            else
            {
                BindingSource.DataSource = null;
            }
        }
        #endregion

    }
}

