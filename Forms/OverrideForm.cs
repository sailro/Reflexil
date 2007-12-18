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
using System.ComponentModel;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Forms
{
	public partial class OverrideForm: Form
    {

        #region " Fields "
        private MethodReference m_selectedmethodreference;
        private MethodDefinition m_mdef;
        #endregion

        #region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }

        public MethodReference SelectedMethodReference
        {
            get
            {
                return m_selectedmethodreference;
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
        public OverrideForm()
        {
            InitializeComponent();
            this.MethodReferenceEditor.Dock = System.Windows.Forms.DockStyle.None;
        }

        public virtual DialogResult ShowDialog(MethodDefinition mdef, MethodReference selected)
        {
            m_mdef = mdef;
            m_selectedmethodreference = selected;
            return base.ShowDialog();
        }
        #endregion

        #region " Events "
        private void MethodReferenceEditor_Validating(object sender, CancelEventArgs e)
        {
            if (MethodReferenceEditor.SelectedOperand == null)
            {
                ErrorProvider.SetError(MethodReferenceEditor, "Type is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(MethodReferenceEditor, string.Empty);
            }
        }
        #endregion

	}
}