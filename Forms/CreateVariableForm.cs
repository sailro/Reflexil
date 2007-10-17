
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

