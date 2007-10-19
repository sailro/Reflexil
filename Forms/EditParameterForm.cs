
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            SelectedParameter.Constant = null;
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
            SelectedParameter.ParameterType = TypeSpecificationEditor.SelectedTypeReference;
        }
        #endregion

    }
}

