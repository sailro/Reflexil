
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
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

