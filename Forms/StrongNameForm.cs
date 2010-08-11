/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
using System.IO;
using System.Windows.Forms;
using Reflexil.Utils;
using Mono.Cecil;
#endregion

namespace Reflexil.Forms
{
	public partial class StrongNameForm: Form
    {
    
        #region " Fields "
        private AssemblyDefinition m_assemblydefinition;
        #endregion

        #region " Properties "
        public AssemblyDefinition AssemblyDefinition
        {
            get
            {
                return m_assemblydefinition;
            }
            set
            {
                m_assemblydefinition = value;
            }
        }
        #endregion

        #region " Events "
        private void Resign_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!StrongNameUtility.Resign(AssemblyDefinition.MainModule.Image.FileName, OpenFileDialog.FileName, Path.GetExtension(OpenFileDialog.FileName).ToLower() == ".pfx"))
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
            if (!StrongNameUtility.RegisterForVerificationSkipping(AssemblyDefinition.MainModule.Image.FileName))
            {
                MessageBox.Show("Registering for verification skipping fails", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Registering for verification skipping succeeds", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }

        private void RemoveSN_Click(object sender, EventArgs e)
        {
            using (StrongNameRemoverForm frm = new StrongNameRemoverForm())
            {
                frm.AssemblyDefinition = AssemblyDefinition;
                if (frm.ShowDialog() == DialogResult.OK) {
                    DialogResult = DialogResult.OK;
                }
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