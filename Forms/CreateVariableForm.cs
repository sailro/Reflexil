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
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Forms
{
    public partial class CreateVariableForm : Reflexil.Forms.VariableForm
    {

        #region " Methods "
        public CreateVariableForm()
        {
            InitializeComponent();
        }
        #endregion

        #region " Events "
        private void ButAppend_Click(object sender, EventArgs e)
        {
            if (IsFormComplete)
            {
                VariableDefinition newvar = CreateVariable();
                if (newvar != null)
                {
                    newvar.Index = MethodDefinition.Body.Variables.Count;
                    MethodDefinition.Body.Variables.Add(newvar);
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
                VariableDefinition newvar = CreateVariable();
                if (newvar != null)
                {
                    VariableDefinitionCollection vars = MethodDefinition.Body.Variables;
                    newvar.Index = vars.IndexOf(SelectedVariable);
                    vars.Insert(newvar.Index, newvar);
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
                VariableDefinition newvar = CreateVariable();
                if (newvar != null)
                {
                    VariableDefinitionCollection vars = MethodDefinition.Body.Variables;
                    newvar.Index = vars.IndexOf(SelectedVariable) + 1;
                    vars.Insert(newvar.Index, newvar);
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void CreateVariableForm_Load(object sender, EventArgs e)
        {
            ButInsertBefore.Enabled = (SelectedVariable != null);
            ButInsertAfter.Enabled = (SelectedVariable != null);
        }
        #endregion

    }
}

