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
using Reflexil.Forms;
#endregion

namespace Reflexil.Editors
{
    public partial class OverrideGridControl : BaseOverrideGridControl
    {

        #region " Methods "
        public OverrideGridControl()
        {
            InitializeComponent();
        }

        protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
        {
            MenCreate.Enabled = (!ReadOnly) && (MethodDefinition != null);
            MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
            MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
            MenDeleteAll.Enabled = (!ReadOnly) && (MethodDefinition != null);
        }

        protected override void MenCreate_Click(object sender, EventArgs e)
        {
            using (CreateOverrideForm createForm = new CreateOverrideForm())
            {
                if (createForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenEdit_Click(object sender, EventArgs e)
        {
            using (EditOverrideForm editForm = new EditOverrideForm())
            {
                if (editForm.ShowDialog(MethodDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < Grid.Rows.Count)
            {
                e.Value = Grid.Rows[e.RowIndex].DataBoundItem;
            }
        }

        protected override void MenDelete_Click(object sender, EventArgs e)
        {
            foreach (MethodReference var in SelectedItems)
            {
                MethodDefinition.Overrides.Remove(var);
            }
            RaiseGridUpdated();
        }

        protected override void MenDeleteAll_Click(object sender, EventArgs e)
        {
            MethodDefinition.Overrides.Clear();
            RaiseGridUpdated();
        }

        protected override void DoDragDrop(object sender, System.Windows.Forms.DataGridViewRow sourceRow, System.Windows.Forms.DataGridViewRow targetRow, System.Windows.Forms.DragEventArgs e)
        {
            MethodReference sourceExc = sourceRow.DataBoundItem as MethodReference;
            MethodReference targetExc = targetRow.DataBoundItem as MethodReference;

            if (sourceExc != targetExc)
            {
                MethodDefinition.Overrides.Remove(sourceExc);
                MethodDefinition.Overrides.Insert(targetRow.Index, sourceExc);
                RaiseGridUpdated();
            }
        }

        public override void Bind(MethodDefinition mdef)
        {
            base.Bind(mdef);
            if (mdef != null)
            {
                BindingSource.DataSource = mdef.Overrides;
            }
            else
            {
                BindingSource.DataSource = null;
            }
        }
        #endregion

    }

    #region " VS Designer generic support "
    public class BaseOverrideGridControl : Reflexil.Editors.GridControl<MethodReference>
    {
    }
    #endregion
}

