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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using de4dot.code;
using de4dot.code.renamer;

#endregion

namespace Reflexil.Forms
{
	public partial class AssemblyCleanerForm : Form
	{
		#region Events

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = sender as BackgroundWorker;
			var ofile = e.Argument as IObfuscatedFile;

			if (worker == null || ofile == null)
				return;

			try
			{
				worker.ReportProgress(0, "Preparing deobfuscation");
				ofile.DeobfuscateBegin();

				worker.ReportProgress(20, "Deobfuscating");
				ofile.Deobfuscate();

				worker.ReportProgress(40, "Finishing deobfuscation");
				ofile.DeobfuscateEnd();

				worker.ReportProgress(60, "Renaming items");
				const RenamerFlags flags = RenamerFlags.RenameNamespaces |
				                           RenamerFlags.RenameTypes |
				                           RenamerFlags.RenameProperties |
				                           RenamerFlags.RenameEvents |
				                           RenamerFlags.RenameFields |
				                           RenamerFlags.RenameMethods |
				                           RenamerFlags.RenameMethodArgs |
				                           RenamerFlags.RenameGenericParams |
				                           RenamerFlags.RestorePropertiesFromNames |
				                           RenamerFlags.RestoreEventsFromNames |
				                           RenamerFlags.RestoreProperties |
				                           RenamerFlags.RestoreEvents;
				var renamer = new Renamer(ofile.DeobfuscatorContext, new[] {ofile}, flags);
				renamer.Rename();

				worker.ReportProgress(80, "Saving");
				ofile.Save();
				worker.ReportProgress(100, "Done");
			}
			catch (Exception ex)
			{
				worker.ReportProgress(0, ex);
				e.Result = ex;
			}
			finally
			{
				ofile.DeobfuscateCleanUp();
			}
		}

		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState is Exception)
			{
				//MessageBox.Show(String.Format("Reflexil is unable to clean this assembly: {0}", (e.UserState as Exception).Message));
				MessageBox.Show(
					String.Format(
						"Reflexil is unable to clean this assembly, please use the full de4dot release (the assembly is probably using opcode virtualization)."));
			}
			else
			{
				ProgressBar.Value = e.ProgressPercentage;
				Step.Text = e.UserState.ToString();
			}
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Result is Exception)
				DialogResult = DialogResult.Cancel;
			else
				DialogResult = DialogResult.OK;
		}

		#endregion

		#region Methods

		public DialogResult ShowDialog(IObfuscatedFile ofile, string newFilename)
		{
			// HACK
			var ofiletype = ofile.GetType();
			var ofield = ofiletype.GetField("options", BindingFlags.NonPublic | BindingFlags.Instance);

			Debug.Assert(ofield != null, "Check De4Dot impl.");

			var options = (ObfuscatedFile.Options) ofield.GetValue(ofile);
			options.NewFilename = newFilename;
			BackgroundWorker.RunWorkerAsync(ofile);

			return ShowDialog();
		}

		public AssemblyCleanerForm()
		{
			InitializeComponent();
		}

		#endregion
	}
}