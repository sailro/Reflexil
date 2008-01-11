/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using System.Collections;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;
#endregion

namespace Reflexil.Forms
{
	public partial class StrongNameRemoverForm: Form
    {

        #region " Fields "
        private AssemblyDefinition m_snassembly = null;
        #endregion

        #region " Properties "
        public AssemblyDefinition AssemblyDefinition
        {
            get
            {
                return m_snassembly;
            }
            set
            {
                m_snassembly = value;
                Add.Enabled = AutoScan.Enabled = Process.Enabled = m_snassembly != null;
                if (m_snassembly != null)
                {
                    SNAssembly.Text = m_snassembly.ToString();
                }
                else
                {
                    SNAssembly.Text = string.Empty;
                }
            }
        }
        #endregion

        #region " Methods "
        public StrongNameRemoverForm()
        {
            InitializeComponent();
        }

        private AssemblyDefinition OpenAssemblyDialog()
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    AssemblyDefinition asmdef = AssemblyFactory.GetAssembly(OpenFileDialog.FileName);
                    if ((asmdef.Name.Flags & AssemblyFlags.PublicKey) != 0)
                    {
                        return asmdef;
                    }
                    else
                    {
                        MessageBox.Show("This assembly does not have a strong name.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Reflexil is unable to load this assembly: {0}", ex.Message));
                }
            }
            return null;
        }
        #endregion

        #region " Events "
        private void Add_Click(object sender, EventArgs e)
        {
            AssemblyDefinition asmdef = OpenAssemblyDialog();
            if (asmdef != null)
            {
                ReferencingAssemblies.Items.Add(asmdef);
            }
        }

        private void ReferencingAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            Remove.Enabled = ReferencingAssemblies.SelectedItems.Count > 0;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            IEnumerable assemblies = new ArrayList(ReferencingAssemblies.SelectedItems);
            foreach (AssemblyDefinition asmdef in assemblies)
            {
                ReferencingAssemblies.Items.Remove(asmdef);
            }
        }

        private void SelectSNAssembly_Click(object sender, EventArgs e)
        {
            AssemblyDefinition = OpenAssemblyDialog();
        }

        private void AutoScan_Click(object sender, EventArgs e)
        {
            if (AssemblyDefinition != null)
            {
                FolderBrowserDialog.SelectedPath = AssemblyDefinition.MainModule.Image.FileInformation.DirectoryName;
                if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    using (DirectoryScanForm frm = new DirectoryScanForm())
                    {
                        if (frm.ShowDialog(AssemblyDefinition) == DialogResult.OK)
                        {
                            ReferencingAssemblies.Items.AddRange(frm.ReferencingAssemblies);
                        }
                    }
                }
            }
        }

        private void Process_Click(object sender, EventArgs e)
        {
            try
            {
                CecilHelper.RemoveStrongName(AssemblyDefinition);
                AssemblyFactory.SaveAssembly(AssemblyDefinition, AssemblyDefinition.MainModule.Image.FileInformation.FullName);
                // then update references
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Reflexil is unable to save this assembly: {0}", ex.Message));
            }
        }
        #endregion

	}
}
