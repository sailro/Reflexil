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
using System.ComponentModel;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Forms
{
	public partial class TypeSpecificationForm: Form
    {

        #region " Properties "
        protected bool IsFormComplete
        {
            get
            {
                foreach (Control ctl in Controls)
                {
                    ctl.Focus();
                    if (!Validate()) return false;
                }
                return true;
            }
        }
        #endregion

        #region " Methods "
        public TypeSpecificationForm()
        {
            InitializeComponent();
        }

        public virtual DialogResult ShowDialog(MethodDefinition mdef)
        {
            TypeSpecificationEditor.MethodDefinition = mdef;
            return base.ShowDialog();
        }
        #endregion

        #region " Events "
        private void ItemName_Validating(object sender, CancelEventArgs e)
        {
            if (ItemName.Text == string.Empty)
            {
                ErrorProvider.SetError(ItemName, "Name is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(ItemName, string.Empty);
            }
        }

        private void TypeSpecificationEditor_Validating(object sender, CancelEventArgs e)
        {
            bool validated;
            if (TypeSpecificationEditor.SelectedTypeReference is TypeSpecification)
            {
                TypeSpecification tspec = TypeSpecificationEditor.SelectedTypeReference as TypeSpecification;
                validated = tspec.ElementType != null;
            }
            else
            {
                validated = TypeSpecificationEditor.SelectedTypeReference != null;
            }

            if (!validated)
            {
                ErrorProvider.SetError(TypeSpecificationEditor, "Type is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(TypeSpecificationEditor, string.Empty);
            }
        }
        #endregion

	}
}