/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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

#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;

#endregion

namespace Reflexil.Forms
{
	public partial class ReferenceUpdaterForm : Form
	{
		#region Events

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = sender as BackgroundWorker;
			var assemblies = e.Argument as AssemblyDefinition[];
			var progress = 0;

			if (assemblies == null || worker == null)
				return;

			try
			{
				ProcessStrongNames(assemblies);
				ProcessReferences(assemblies);
				foreach (var asmdef in assemblies)
				{
					progress++;
					worker.ReportProgress((progress*100)/assemblies.Length, asmdef.Name);
					asmdef.Write(asmdef.MainModule.Image.FileName);
				}
			}
			catch (Exception ex)
			{
				worker.ReportProgress(0, ex);
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

		#region Methods

		public DialogResult ShowDialog(AssemblyDefinition[] asmdefs)
		{
			BackgroundWorker.RunWorkerAsync(asmdefs);
			return ShowDialog();
		}

		public ReferenceUpdaterForm()
		{
			InitializeComponent();
		}

		private static void ProcessReferences(AssemblyDefinition[] assemblies)
		{
			foreach (var asmdef in assemblies)
				ProcessReferences(asmdef, assemblies);
		}

		private static void ProcessReferences(AssemblyDefinition current, IEnumerable<AssemblyDefinition> assemblies)
		{
			foreach (var asmdef in assemblies.Where(asmdef => !asmdef.Equals(current)))
				ProcessReferences(current, asmdef);
		}

		private static void ProcessReferences(AssemblyDefinition current, AssemblyDefinition other)
		{
			foreach (var anref in from moddef in current.Modules
				from anref in moddef.AssemblyReferences
				where CecilHelper.ReferenceMatches(anref, other.Name)
				select anref)
				CecilHelper.RemoveStrongNameReference(anref);
		}

		private static void ProcessStrongNames(IEnumerable<AssemblyDefinition> assemblies)
		{
			foreach (var asmdef in assemblies)
				CecilHelper.RemoveStrongName(asmdef);
		}

		#endregion
	}
}