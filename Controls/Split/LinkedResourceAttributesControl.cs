/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;

namespace Reflexil.Editors
{
	public partial class LinkedResourceAttributesControl : BaseLinkedResourceAttributesControl
	{
		public LinkedResourceAttributesControl()
		{
			InitializeComponent();
		}

		public override void Bind(LinkedResource res)
		{
			base.Bind(res);
			if (res != null)
			{
				Filename.Text = res.File;
				Hash.Text = ByteHelper.ByteToString(res.Hash);
			}
			else
			{
				Filename.Text = string.Empty;
				Hash.Text = string.Empty;
			}
		}

		private void Filename_Validating(object sender, CancelEventArgs e)
		{
			if (Filename.Text.Length == 0)
			{
				ErrorProvider.SetError(Filename, "Name is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(Filename, string.Empty);
			}
		}

		private void StringToByte_Validating(object sender, CancelEventArgs e)
		{
			try
			{
				var textBox = sender as TextBox;
				if (textBox == null)
					return;

				var input = textBox.Text;
				if (input.Length%2 != 0)
					return;

				ByteHelper.StringToByte(input);
				ErrorProvider.SetError(textBox, string.Empty);
			}
			catch (Exception)
			{
				ErrorProvider.SetError((Control) sender, "Incorrect byte sequence");
				e.Cancel = true;
			}
		}

		private void Filename_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.File = Filename.Text;
		}

		private void Hash_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Hash = ByteHelper.StringToByte(Hash.Text);
		}

		private void ButFromFile_Click(object sender, EventArgs e)
		{
			if (OpenFileDialog.ShowDialog() != DialogResult.OK)
				return;

			try
			{
				Filename.Text = Path.GetFileName(OpenFileDialog.FileName);
				Filename_Validated(this, EventArgs.Empty);

				Hash.Text = ByteHelper.ByteToString(CryptoService.ComputeHash(OpenFileDialog.FileName));
				Hash_Validated(this, EventArgs.Empty);
			}
			// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
		}
	}

	public class BaseLinkedResourceAttributesControl : SplitAttributesControl<LinkedResource>
	{
	}
}