
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Forms
{
    public partial class CreateOverrideForm : Reflexil.Forms.OverrideForm
    {

        #region " Methods "
        public CreateOverrideForm()
        {
            InitializeComponent();
        }


        #endregion

        #region " Events "
        private void ButInsertBefore_Click(System.Object sender, System.EventArgs e)
        {
            if (IsFormComplete)
            {
                OverrideCollection overrides = MethodDefinition.Overrides;
                overrides.Insert(overrides.IndexOf(SelectedMethodReference), MethodDefinition.DeclaringType.Module.Import(MethodReferenceEditor.SelectedOperand));
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void ButInsertAfter_Click(System.Object sender, System.EventArgs e)
        {
            if (IsFormComplete)
            {
                OverrideCollection overrides = MethodDefinition.Overrides;
                overrides.Insert(overrides.IndexOf(SelectedMethodReference) + 1, MethodDefinition.DeclaringType.Module.Import(MethodReferenceEditor.SelectedOperand));
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void ButAppend_Click(System.Object sender, System.EventArgs e)
        {
            if (IsFormComplete)
            {
                OverrideCollection overrides = MethodDefinition.Overrides;
                overrides.Add(MethodDefinition.DeclaringType.Module.Import(MethodReferenceEditor.SelectedOperand));
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void CreateOverrideForm_Load(object sender, EventArgs e)
        {
            ButInsertBefore.Enabled = (SelectedMethodReference != null);
            ButInsertAfter.Enabled = (SelectedMethodReference != null);
        }
        #endregion

    }
}

