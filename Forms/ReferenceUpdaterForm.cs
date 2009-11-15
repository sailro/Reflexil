/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
	public partial class ReferenceUpdaterForm : Form
    {
 
        #region " Events "
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            AssemblyDefinition[] assemblies = e.Argument as AssemblyDefinition[];
            int progress = 0;

            try
            {
                ProcessStrongNames(assemblies);
                ProcessReferences(assemblies);
                foreach (AssemblyDefinition asmdef in assemblies)
                {
                    progress++;
                    worker.ReportProgress((progress * 100) / assemblies.Length, asmdef.Name);
                    AssemblyFactory.SaveAssembly(asmdef, asmdef.MainModule.Image.FileInformation.FullName);
                }
            }
            catch (Exception ex)
            {
                worker.ReportProgress(0,ex);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is Exception)
            {
                MessageBox.Show(String.Format("Reflexil is unable to save this assembly: {0}", (e.UserState as Exception).Message));
            }
            else
            {
                ProgressBar.Value = e.ProgressPercentage;
                Assembly.Text = e.UserState.ToString();
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion

        #region " Methods "
        public DialogResult ShowDialog(AssemblyDefinition[] asmdefs)
        {
            BackgroundWorker.RunWorkerAsync(asmdefs);
            return ShowDialog();
        }

        public ReferenceUpdaterForm()
		{
			InitializeComponent();
        }

        private void ProcessReferences(AssemblyDefinition[] assemblies)
        {
            foreach (AssemblyDefinition asmdef in assemblies)
            {
                ProcessReferences(asmdef, assemblies);
            }
        }

        private void ProcessReferences(AssemblyDefinition current, AssemblyDefinition[] assemblies)
        {
            foreach (AssemblyDefinition asmdef in assemblies)
            {
                if (!asmdef.Equals(current))
                {
                    ProcessReferences(current, asmdef);
                }
            }
        }

        private void ProcessReferences(AssemblyDefinition current, AssemblyDefinition other)
        {
            foreach (ModuleDefinition moddef in current.Modules)
            {
                foreach (AssemblyNameReference anref in moddef.AssemblyReferences)
                {
                    if (CecilHelper.ReferenceMatches(anref, other.Name))
                    {
                        CecilHelper.RemoveStrongNameReference(anref);
                    }
                }
            }
        }

        private void ProcessStrongNames(AssemblyDefinition[] assemblies)
        {
            foreach (AssemblyDefinition asmdef in assemblies)
            {
                CecilHelper.RemoveStrongName(asmdef);
            }
        }
        #endregion

	}
}
