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
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;

namespace Reflexil.Editors
{
	public partial class AssemblyNameReferenceAttributesControl : BaseAssemblyNameReferenceAttributesControl
	{
		public AssemblyNameReferenceAttributesControl()
		{
			InitializeComponent();
			Algorithm.DataSource = Enum.GetValues(typeof(AssemblyHashAlgorithm));
		}

		public override void Bind(AssemblyNameReference anref)
		{
			base.Bind(anref);
			if (anref != null)
			{
				AssemblyCulture.Text = anref.Culture;
				Major.Value = anref.Version.Major;
				Minor.Value = anref.Version.Minor;
				Build.Value = anref.Version.Build;
				Revision.Value = anref.Version.Revision;
				PublicKey.Text = ByteHelper.ByteToString(anref.PublicKey);
				PublicKeyToken.Text = ByteHelper.ByteToString(anref.PublicKeyToken);
				Hash.Text = ByteHelper.ByteToString(anref.Hash);
				Algorithm.SelectedItem = anref.HashAlgorithm;
			}
			else
			{
				AssemblyCulture.Text = string.Empty;
				Major.Value = 0;
				Minor.Value = 0;
				Build.Value = 0;
				Revision.Value = 0;
				PublicKey.Text = string.Empty;
				PublicKeyToken.Text = string.Empty;
				Hash.Text = string.Empty;
				Algorithm.SelectedIndex = -1;
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

		private void AssemblyCulture_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Culture = AssemblyCulture.Text;
		}

		private void Version_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Version = new Version(Convert.ToInt32(Major.Value), Convert.ToInt32(Minor.Value), Convert.ToInt32(Build.Value), Convert.ToInt32(Revision.Value));
		}

		private void PublicKey_Validated(object sender, EventArgs e)
		{
			if (Item == null || ByteHelper.ByteToString(Item.PublicKey) == PublicKey.Text)
				return;

			Item.PublicKey = ByteHelper.StringToByte(PublicKey.Text);
			Bind(Item);
		}

		private void PublicKeyToken_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.PublicKeyToken = ByteHelper.StringToByte(PublicKeyToken.Text);
		}

		private void Algorithm_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.HashAlgorithm = (AssemblyHashAlgorithm) Algorithm.SelectedItem;
		}

		private void Hash_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Hash = ByteHelper.StringToByte(Hash.Text);
		}
	}

	public class BaseAssemblyNameReferenceAttributesControl : SplitAttributesControl<AssemblyNameReference>
	{
	}
}