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
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;

#endregion

namespace Reflexil.Editors
{
	/// <summary>
	/// Linked ressource attributes editor
	/// </summary>
	public partial class LinkedResourceAttributesControl : BaseLinkedResourceAttributesControl
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public LinkedResourceAttributesControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Bind a resource to this control
		/// </summary>
		/// <param name="res">Resource to bind</param>
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

		#endregion

		#region Events

		/// <summary>
		/// Filename validation
		/// </summary>
		/// <param name="sender">object to validate</param>
		/// <param name="e">parameters</param>
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

		/// <summary>
		/// Hash validation
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
					string input = textBox.Text;
					if (input.Length%2 == 0)
					{
						ByteHelper.StringToByte(input);
						ErrorProvider.SetError(sender as Control, string.Empty);
					}
				}
			}
			catch (Exception)
			{
				ErrorProvider.SetError((Control) sender, "Incorrect byte sequence");
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Filename update
		/// </summary>
		/// <param name="sender">Updater object</param>
		/// <param name="e">parameters</param>
		private void Filename_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.File = Filename.Text;
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

	#region VS Designer generic support

	public class BaseLinkedResourceAttributesControl : SplitAttributesControl<LinkedResource>
	{
	}

	#endregion
}