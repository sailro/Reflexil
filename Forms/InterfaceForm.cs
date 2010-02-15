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
	public partial class InterfaceForm: Form
    {

        #region " Fields "
        private TypeReference m_selectedtypereference;
        private TypeDefinition m_tdef;
        #endregion

        #region " Properties "
        public TypeDefinition TypeDefinition
        {
            get
            {
                return m_tdef;
            }
        }

        public TypeReference SelectedTypeReference
        {
            get
            {
                return m_selectedtypereference;
            }
        }

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
        public InterfaceForm()
        {
            InitializeComponent();
            this.TypeReferenceEditor.Dock = System.Windows.Forms.DockStyle.None;
        }

        public virtual DialogResult ShowDialog(TypeDefinition tdef, TypeReference selected)
        {
            m_tdef = tdef;
            m_selectedtypereference = selected;
            return base.ShowDialog();
        }
        #endregion

        #region " Events "
        private void TypeReferenceEditor_Validating(object sender, CancelEventArgs e)
        {
            if (TypeReferenceEditor.SelectedOperand == null)
            {
                ErrorProvider.SetError(TypeReferenceEditor, "Type is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(TypeReferenceEditor, string.Empty);
            }
        }
        #endregion

	}
}