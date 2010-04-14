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
using Reflexil.Editors;
using System.ComponentModel;
#endregion

namespace Reflexil.Forms
{
    public partial class ParameterForm : Reflexil.Forms.TypeSpecificationForm
    {

        #region " Fields "
        private MethodDefinition m_mdef;
        private ParameterDefinition m_selectedparameter;
        #endregion

        #region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }

        public ParameterDefinition SelectedParameter
        {
            get
            {
                return m_selectedparameter;
            }
        }
        #endregion

        #region " Methods "
        public ParameterForm()
        {
            InitializeComponent();
        }

        protected ParameterDefinition CreateParameter()
        {
            ParameterDefinition prm = new ParameterDefinition(MethodDefinition.DeclaringType.Module.Import(TypeSpecificationEditor.SelectedTypeReference));
            prm.Name = ItemName.Text;
            prm.Attributes = (Attributes.Item as ParameterDefinition).Attributes;
            ConstantEditor.CopyStateTo(prm);

            return prm;
        }

        public virtual DialogResult ShowDialog(MethodDefinition mdef, ParameterDefinition selected)
        {
            m_mdef = mdef;
            m_selectedparameter = selected;
            return base.ShowDialog(mdef);
        }
        #endregion

        #region " Events "
        private void Constant_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ErrorProvider.SetError(ConstantEditor, string.Empty);
            }
            catch (Exception)
            {
                ErrorProvider.SetError(ConstantEditor, "Unable to convert input");
                e.Cancel = true;
            }
        }
        #endregion

    }
}

