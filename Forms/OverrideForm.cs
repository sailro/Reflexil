
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
	public partial class OverrideForm: Form
    {

        #region " Fields "
        private MethodReference m_selectedmethodreference;
        private MethodDefinition m_mdef;
        #endregion

        #region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }

        public MethodReference SelectedMethodReference
        {
            get
            {
                return m_selectedmethodreference;
            }
        }

        protected bool IsFormComplete
        {
            get
            {
                foreach (Control ctl in Controls)
                {
                    ctl.Focus();
                    if (!Validate()) return false;
                }
                return true;
            }
        }
        #endregion

        #region " Methods "
        public OverrideForm()
        {
            InitializeComponent();
            this.MethodReferenceEditor.Dock = System.Windows.Forms.DockStyle.None;
        }

        public virtual DialogResult ShowDialog(MethodDefinition mdef, MethodReference selected)
        {
            m_mdef = mdef;
            m_selectedmethodreference = selected;
            return base.ShowDialog();
        }
        #endregion

        #region " Events "
        private void MethodReferenceEditor_Validating(object sender, CancelEventArgs e)
        {
            if (MethodReferenceEditor.SelectedOperand == null)
            {
                ErrorProvider.SetError(MethodReferenceEditor, "Type is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(MethodReferenceEditor, string.Empty);
            }
        }
        #endregion

	}
}