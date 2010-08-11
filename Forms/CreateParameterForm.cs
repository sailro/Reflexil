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
    public partial class CreateParameterForm : Reflexil.Forms.ParameterForm
    {

        #region " Methods "
        public CreateParameterForm()
        {
            InitializeComponent();
        }

        public override DialogResult ShowDialog(MethodDefinition mdef, ParameterDefinition selected)
        {
            Attributes.Bind(new ParameterDefinition(null));
            return base.ShowDialog(mdef, selected);
        }
        #endregion

        #region " Events "
        private void CreateParameterForm_Load(object sender, EventArgs e)
        {
            ButInsertBefore.Enabled = (SelectedParameter != null);
            ButInsertAfter.Enabled = (SelectedParameter != null);
        }

        private void ButAppend_Click(object sender, EventArgs e)
        {
            if (IsFormComplete)
            {
                ParameterDefinition newprm = CreateParameter();
                if (newprm != null)
                {
                    MethodDefinition.Parameters.Add(newprm);
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void ButInsertBefore_Click(object sender, EventArgs e)
        {
            if (IsFormComplete)
            {
                ParameterDefinition newprm = CreateParameter();
                if (newprm != null)
                {
                    Mono.Collections.Generic.Collection<ParameterDefinition> prms = MethodDefinition.Parameters;
                    prms.Insert(prms.IndexOf(SelectedParameter), newprm);
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void ButInsertAfter_Click(object sender, EventArgs e)
        {
            if (IsFormComplete)
            {
                ParameterDefinition newprm = CreateParameter();
                if (newprm != null)
                {
                    Mono.Collections.Generic.Collection<ParameterDefinition> prms = MethodDefinition.Parameters;
                    prms.Insert(prms.IndexOf(SelectedParameter) + 1, newprm);
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }
        #endregion

    }
}

