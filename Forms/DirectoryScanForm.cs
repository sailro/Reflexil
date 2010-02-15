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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;
#endregion

namespace Reflexil.Forms
{
	public partial class DirectoryScanForm: Form
    {

        #region " Fields "
        private List<AssemblyDefinition> m_referencingassemblies = new List<AssemblyDefinition>();
        #endregion

        #region " Properties "
        public AssemblyDefinition[] ReferencingAssemblies
        {
            get
            {
                return m_referencingassemblies.ToArray();
            }
        }
        #endregion

        #region " Events "
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            AssemblyDefinition asmdef = e.Argument as AssemblyDefinition;

            List<AssemblyDefinition> result = new List<AssemblyDefinition>();
            e.Result = result;

            List<FileInfo> files = new List<FileInfo>();
            DirectoryInfo directory = asmdef.MainModule.Image.FileInformation.Directory;
            files.AddRange(directory.GetFiles("*.exe"));
            files.AddRange(directory.GetFiles("*.dll"));

            string msg = string.Empty;
            foreach (FileInfo file in files)
            {
                if (worker.CancellationPending)
                {
                    result.Clear();
                    return;
                }
                try
                {
                    AssemblyDefinition refasm = AssemblyFactory.GetAssembly(file.FullName);
                    foreach (AssemblyNameReference name in refasm.MainModule.AssemblyReferences)
                    {
                        if (CecilHelper.ReferenceMatches(asmdef.Name, name))
                        {
                            result.Add(refasm);
                        }
                    }
                    msg = String.Format("{0} ({1}/{2})", refasm, files.IndexOf(file), files.Count);
                }
                catch
                {
                    msg = file.FullName;
                }
                worker.ReportProgress(((files.IndexOf(file)+1) * 100) / files.Count, msg);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
            File.Text = e.UserState.ToString();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_referencingassemblies = e.Result as List<AssemblyDefinition>;
            DialogResult = DialogResult.OK;
        }
        #endregion

        #region " Methods "
        public DialogResult ShowDialog(AssemblyDefinition asmdef)
        {
            Directory.Text = asmdef.MainModule.Image.FileInformation.Directory.FullName;
            BackgroundWorker.RunWorkerAsync(asmdef);
            return ShowDialog();
        }

		public DirectoryScanForm()
		{
			InitializeComponent();
        }
        #endregion

	}
}
