
#region " Imports "
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Reflexil.Utils;
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
                new StrongNameUtility().Resign(m_assemblyfile, OpenFileDialog.FileName);
            }
        }

        private void Register_Click(object sender, EventArgs e)
        {
            new StrongNameUtility().RegisterForVerificationSkipping(m_assemblyfile);
        }
        #endregion

        #region " Methods "
        public StrongNameForm()
        {
            InitializeComponent();
        }
        #endregion

	}
}