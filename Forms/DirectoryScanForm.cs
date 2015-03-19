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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;

#endregion

namespace Reflexil.Forms
{
	public partial class DirectoryScanForm : Form
	{
		#region Fields

		private List<AssemblyDefinition> _referencingAssemblies = new List<AssemblyDefinition>();

		#endregion

		#region Properties

		public AssemblyDefinition[] ReferencingAssemblies
		{
			get { return _referencingAssemblies.ToArray(); }
		}

		#endregion

		#region Events

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = sender as BackgroundWorker;
			if (worker == null)
				return;

			var asmdef = e.Argument as AssemblyDefinition;
			if (asmdef == null)
				return;


			var result = new List<AssemblyDefinition>();
			e.Result = result;

			var files = new List<FileInfo>();
			var fileName = asmdef.MainModule.Image.FileName;
			var directoryName = Path.GetDirectoryName(fileName);
			if (directoryName != null)
			{
				var directory = new DirectoryInfo(directoryName);
				files.AddRange(directory.GetFiles("*.exe"));
				files.AddRange(directory.GetFiles("*.dll"));
			}

			foreach (var file in files)
			{
				if (worker.CancellationPending)
				{
					result.Clear();
					return;
				}

				string msg;
				try
				{
					var refasm = AssemblyDefinition.ReadAssembly(file.FullName);
					result.AddRange(
						refasm.MainModule.AssemblyReferences.Where(name => CecilHelper.ReferenceMatches(asmdef.Name, name))
							.Select(name => refasm));
					msg = String.Format("{0} ({1}/{2})", refasm, files.IndexOf(file), files.Count);
				}
				catch
				{
					msg = file.FullName;
				}
				worker.ReportProgress(((files.IndexOf(file) + 1)*100)/files.Count, msg);
			}
		}

		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ProgressBar.Value = e.ProgressPercentage;
			File.Text = e.UserState.ToString();
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_referencingAssemblies = e.Result as List<AssemblyDefinition>;
			DialogResult = DialogResult.OK;
		}

		#endregion

		#region Methods

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