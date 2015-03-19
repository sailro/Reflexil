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
using System.IO;
using System.Windows.Forms;
using Reflexil.Utils;
using Mono.Cecil;

#endregion

namespace Reflexil.Forms
{
	public partial class StrongNameForm : Form
	{
		#region Properties

		public AssemblyDefinition AssemblyDefinition { get; set; }
		public string DelaySignedFileName { get; set; }

		#endregion

		#region Events

		private void Resign_Click(object sender, EventArgs e)
		{
			if (OpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				var extension = Path.GetExtension(OpenFileDialog.FileName);

				if (
					!StrongNameUtility.Resign(DelaySignedFileName, OpenFileDialog.FileName,
						extension != null && extension.ToLower() == ".pfx"))
				{
					MessageBox.Show(@"Re-signing fails, check that the supplied key is valid and match the original assembly key",
						@"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show(@"Re-signing succeeds", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					DialogResult = DialogResult.OK;
				}
			}
		}

		private void Register_Click(object sender, EventArgs e)
		{
			if (!StrongNameUtility.RegisterForVerificationSkipping(DelaySignedFileName))
			{
				MessageBox.Show(@"Registering for verification skipping fails", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				MessageBox.Show(@"Registering for verification skipping succeeds", @"Information", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				DialogResult = DialogResult.OK;
			}
		}

		private void RemoveSN_Click(object sender, EventArgs e)
		{
			using (var frm = new StrongNameRemoverForm())
			{
				frm.AssemblyDefinition = AssemblyDefinition;
				if (frm.ShowDialog() == DialogResult.OK)
					DialogResult = DialogResult.OK;
			}
		}

		#endregion

		#region Methods

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