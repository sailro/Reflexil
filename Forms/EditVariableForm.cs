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

#endregion

namespace Reflexil.Forms
{
    public partial class EditVariableForm : Reflexil.Forms.VariableForm
    {

        #region " Methods "
        public EditVariableForm()
        {
            InitializeComponent();
        }
        #endregion

        #region " Events "
        private void ButUpdate_Click(object sender, EventArgs e)
        {
            if (IsFormComplete)
            {
                SelectedVariable.Name = ItemName.Text;
                SelectedVariable.VariableType = MethodDefinition.DeclaringType.Module.Import(TypeSpecificationEditor.SelectedTypeReference);

                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void EditVariableForm_Load(object sender, EventArgs e)
        {
            ItemName.Text = SelectedVariable.Name;
            TypeSpecificationEditor.SelectedTypeReference = SelectedVariable.VariableType;
        }
        #endregion

    }
}

