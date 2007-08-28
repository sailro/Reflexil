
#region " Imports "
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Reflexil.Utils;
using System.IO;
#endregion

namespace Reflexil.Forms
{
	public partial class StrongNameForm: Form
    {
        #region " Fields "
        private string m_assemblyfile;
        #endregion

        #region " Properties "
        public string AssemblyFile
        {
            get
            {
                return m_assemblyfile;
            }
            set
            {
                m_assemblyfile = value;
            }
        }
        #endregion

        #region " Events "
        private void Resign_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!StrongNameUtility.Resign(m_assemblyfile, OpenFileDialog.FileName, Path.GetExtension(OpenFileDialog.FileName).ToLower() == ".pfx"))
                {
                    MessageBox.Show("Re-signing fails, check that the supplied key is valid and match the original assembly key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Re-signing succeeds", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void Register_Click(object sender, EventArgs e)
        {
            if (!StrongNameUtility.RegisterForVerificationSkipping(m_assemblyfile))
            {
                MessageBox.Show("Registering for verification skipping fails", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Registering for verification skipping succeeds", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region " Methods "
        public StrongNameForm()
        {
            InitializeComponent();
            if (!StrongNameUtility.StrongNameToolPresent)
            {
                SnToolNotFound.Visible = true;
                Note.Visible = false;
                Resign.Enabled = false;
                Register.Enabled = false;
            }
            else
            {
                SnToolNotFound.Visible = false;
            }
        }
        #endregion

	}
}