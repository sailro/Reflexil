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
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;

#endregion

namespace Reflexil.Editors
{
	/// <summary>
	/// Assembly Name reference attributes editor (all object readable/writeable non indexed properties)
	/// </summary>
	public partial class AssemblyNameReferenceAttributesControl : BaseAssemblyNameReferenceAttributesControl
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public AssemblyNameReferenceAttributesControl()
		{
			InitializeComponent();
			Algorithm.DataSource = Enum.GetValues(typeof (AssemblyHashAlgorithm));
		}

		/// <summary>
		/// Bind an assembly name reference this control
		/// </summary>
		/// <param name="anref">Assembly name reference to bind</param>
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

		#endregion

		#region Events

		/// <summary>
		/// PublicKey, PublicKeyToken, Hash validation
		/// </summary>
		/// <param name="sender">object to validate</param>
		/// <param name="e">parameters</param>
		private void StringToByte_Validating(object sender, CancelEventArgs e)
		{
			try
			{
				var textBox = sender as TextBox;
				if (textBox != null)
				{
					var input = textBox.Text;
					if (input.Length%2 != 0)
						return;

					ByteHelper.StringToByte(input);
					ErrorProvider.SetError(sender as Control, string.Empty);
				}
			}
			catch (Exception)
			{
				ErrorProvider.SetError((Control) sender, "Incorrect byte sequence");
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Culture update
		/// </summary>
		/// <param name="sender">Updater object</param>
		/// <param name="e">parameters</param>
		private void AssemblyCulture_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Culture = AssemblyCulture.Text;
		}

		/// <summary>
		/// Version update
		/// </summary>
		/// <param name="sender">Updater object</param>
		/// <param name="e">parameters</param>
		private void Version_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Version = new Version(Convert.ToInt32(Major.Value), Convert.ToInt32(Minor.Value), Convert.ToInt32(Build.Value), Convert.ToInt32(Revision.Value));
		}

		/// <summary>
		/// PublicKey update
		/// </summary>
		/// <param name="sender">Updater object</param>
		/// <param name="e">parameters</param>
		private void PublicKey_Validated(object sender, EventArgs e)
		{
			if (Item == null || ByteHelper.ByteToString(Item.PublicKey) == PublicKey.Text)
				return;

			Item.PublicKey = ByteHelper.StringToByte(PublicKey.Text);
			Bind(Item);
		}

		/// <summary>
		/// PublicKeyToken update
		/// </summary>
		/// <param name="sender">Updater object</param>
		/// <param name="e">parameters</param>
		private void PublicKeyToken_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.PublicKeyToken = ByteHelper.StringToByte(PublicKeyToken.Text);
		}

		/// <summary>
		/// Algorithm update
		/// </summary>
		/// <param name="sender">Updater object</param>
		/// <param name="e">parameters</param>
		private void Algorithm_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.HashAlgorithm = (AssemblyHashAlgorithm)Algorithm.SelectedItem;
		}

		/// <summary>
		/// Hash update
		/// </summary>
		/// <param name="sender">Updater object</param>
		/// <param name="e">parameters</param>
		private void Hash_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Hash = ByteHelper.StringToByte(Hash.Text);
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseAssemblyNameReferenceAttributesControl : SplitAttributesControl<AssemblyNameReference>
	{
	}

	#endregion
}