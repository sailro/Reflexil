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
using Reflexil.Editors;
#endregion

namespace Reflexil.Forms
{
    public partial class EditParameterForm : Reflexil.Forms.ParameterForm
    {

        #region " Methods "
        public EditParameterForm()
        {
            InitializeComponent();
        }

        public override DialogResult ShowDialog(MethodDefinition mdef, ParameterDefinition selected)
        {
            FillControls(mdef);
            if (selected != null)
            {
                foreach (IGlobalOperandEditor editor in ConstantTypes.Items)
                {
                    if (selected.Constant != null)
                    {
                        if (editor.IsOperandHandled(selected.Constant))
                        {
                            ConstantTypes.SelectedItem = editor;
                            ConstantTypes_SelectedIndexChanged(this, EventArgs.Empty);
                            break;
                        }
                    }
                }
            }
            return base.ShowDialog(mdef, selected);
        }
        #endregion

        #region " Events "
        private void EditParameterForm_Load(object sender, EventArgs e)
        {
            ItemName.Text = SelectedParameter.Name;
            TypeSpecificationEditor.SelectedTypeReference = SelectedParameter.ParameterType;
            Attributes.Bind(SelectedParameter.Clone());
            if ((SelectedParameter != null) && (SelectedParameter.Constant != null))
            {
                ((IGlobalOperandEditor)ConstantTypes.SelectedItem).SelectOperand(SelectedParameter.Constant);
            }
        }

        private void ButUpdate_Click(object sender, EventArgs e)
        {
            if (IsFormComplete)
            {
                SelectedParameter.Attributes = ParameterAttributes.None;
                SelectedParameter.Attributes = (Attributes.Item as ParameterDefinition).Attributes;
                if (SelectedParameter.HasDefault)
                {
                    if (ConstantTypes.SelectedItem != null)
                    {
                        IGlobalOperandEditor editor = (IGlobalOperandEditor)ConstantTypes.SelectedItem;
                        SelectedParameter.Constant = editor.CreateObject();
                    }
                }
                SelectedParameter.Name = ItemName.Text;
                SelectedParameter.ParameterType = MethodDefinition.DeclaringType.Module.Import(TypeSpecificationEditor.SelectedTypeReference);

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

