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
using Reflexil.Forms;
#endregion

namespace Reflexil.Editors
{
    public partial class InterfaceGridControl : BaseInterfaceGridControl
    {

        #region " Methods "
        public InterfaceGridControl()
        {
            InitializeComponent();
        }

        protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
        {
            MenCreate.Enabled = (!ReadOnly) && (OwnerDefinition != null);
            MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem != null);
            MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
            MenDeleteAll.Enabled = (!ReadOnly) && (OwnerDefinition != null);
        }

        protected override void MenCreate_Click(object sender, EventArgs e)
        {
            using (CreateInterfaceForm createForm = new CreateInterfaceForm())
            {
                if (createForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void MenEdit_Click(object sender, EventArgs e)
        {
            using (EditInterfaceForm editForm = new EditInterfaceForm())
            {
                if (editForm.ShowDialog(OwnerDefinition, FirstSelectedItem) == DialogResult.OK)
                {
                    RaiseGridUpdated();
                }
            }
        }

        protected override void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < Grid.Rows.Count && e.RowIndex < OwnerDefinition.Interfaces.Count)
            {
                e.Value = OwnerDefinition.Interfaces[e.RowIndex];
            }
        }

        protected override void MenDelete_Click(object sender, EventArgs e)
        {
            foreach (TypeReference var in SelectedItems)
            {
                OwnerDefinition.Interfaces.Remove(var);
            }
            RaiseGridUpdated();
        }

        protected override void MenDeleteAll_Click(object sender, EventArgs e)
        {
            OwnerDefinition.Interfaces.Clear();
            RaiseGridUpdated();
        }

        protected override void DoDragDrop(object sender, System.Windows.Forms.DataGridViewRow sourceRow, System.Windows.Forms.DataGridViewRow targetRow, System.Windows.Forms.DragEventArgs e)
        {
            TypeReference sourceExc = sourceRow.DataBoundItem as TypeReference;
            TypeReference targetExc = targetRow.DataBoundItem as TypeReference;

            if (sourceExc != targetExc)
            {
                OwnerDefinition.Interfaces.Remove(sourceExc);
                OwnerDefinition.Interfaces.Insert(targetRow.Index, sourceExc);
                RaiseGridUpdated();
            }
        }

        public override void Bind(TypeDefinition tdef)
        {
            base.Bind(tdef);
            if (tdef != null)
            {
                BindingSource.DataSource = tdef.Interfaces;
            }
            else
            {
                BindingSource.DataSource = null;
            }
        }
        #endregion

    }

    #region " VS Designer generic support "
    public class BaseInterfaceGridControl : Reflexil.Editors.GridControl<TypeReference, TypeDefinition>
    {
    }
    #endregion
}

