
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Reflexil.Forms
{
    public partial class EditOverrideForm : Reflexil.Forms.OverrideForm
    {

        #region " Methods "
        public EditOverrideForm()
        {
            InitializeComponent();
        }
        #endregion

        #region " Events "
        private void ButUpdate_Click(object sender, EventArgs e)
        {
            if (IsFormComplete)
            {
                int index = MethodDefinition.Overrides.IndexOf(SelectedMethodReference);
                MethodDefinition.Overrides.RemoveAt(index);
                MethodDefinition.Overrides.Insert(index, MethodDefinition.DeclaringType.Module.Import(MethodReferenceEditor.SelectedOperand));
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void EditOverrideForm_Load(object sender, EventArgs e)
        {
            MethodReferenceEditor.SelectedOperand = SelectedMethodReference;
        }
        #endregion

    }
}

