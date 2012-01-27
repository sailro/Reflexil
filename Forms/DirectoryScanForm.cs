/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

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
            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(asmdef.MainModule.Image.FileName));
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
                    AssemblyDefinition refasm = AssemblyDefinition.ReadAssembly(file.FullName);
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
            Directory.Text = Path.GetDirectoryName(asmdef.MainModule.Image.FileName);
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
