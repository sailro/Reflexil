/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
#endregion

namespace Reflexil.Forms
{
    public partial class CreateInterfaceForm : Reflexil.Forms.InterfaceForm
    {

        #region " Methods "
        public CreateInterfaceForm()
        {
            InitializeComponent();
        }


        #endregion

        #region " Events "
        private void ButInsertBefore_Click(System.Object sender, System.EventArgs e)
        {
            if (IsFormComplete)
            {
                InterfaceCollection interfaces = TypeDefinition.Interfaces;
                interfaces.Insert(interfaces.IndexOf(SelectedTypeReference), TypeDefinition.Module.Import(TypeReferenceEditor.SelectedOperand));
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void ButInsertAfter_Click(System.Object sender, System.EventArgs e)
        {
            if (IsFormComplete)
            {
                InterfaceCollection interfaces = TypeDefinition.Interfaces;
                interfaces.Insert(interfaces.IndexOf(SelectedTypeReference) + 1, TypeDefinition.Module.Import(TypeReferenceEditor.SelectedOperand));
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void ButAppend_Click(System.Object sender, System.EventArgs e)
        {
            if (IsFormComplete)
            {
                InterfaceCollection interfaces = TypeDefinition.Interfaces;
                interfaces.Add(TypeDefinition.Module.Import(TypeReferenceEditor.SelectedOperand));
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void CreateInterfaceForm_Load(object sender, EventArgs e)
        {
            ButInsertBefore.Enabled = (SelectedTypeReference != null);
            ButInsertAfter.Enabled = (SelectedTypeReference != null);
        }
        #endregion

    }
}

