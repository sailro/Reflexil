
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
    public partial class CreateParameterForm : Reflexil.Forms.ParameterForm
    {

        #region " Methods "
        public CreateParameterForm()
        {
            InitializeComponent();
        }

        public override DialogResult ShowDialog(MethodDefinition mdef, ParameterDefinition selected)
        {
            FillControls(mdef);
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
                    ParameterDefinitionCollection prms = MethodDefinition.Parameters;
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
                    ParameterDefinitionCollection prms = MethodDefinition.Parameters;
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

