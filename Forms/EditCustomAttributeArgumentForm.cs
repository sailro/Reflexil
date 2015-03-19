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
using System.Windows.Forms;

#endregion

namespace Reflexil.Forms
{
	public partial class EditCustomAttributeArgumentForm : CustomAttributeArgumentForm
	{
		#region Methods

		public EditCustomAttributeArgumentForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		private void ButUpdate_Click(object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				if (SelectedArgument != null)
				{
					var index = SelectedAttribute.ConstructorArguments.IndexOf(SelectedArgument.Value);
					SelectedAttribute.ConstructorArguments.RemoveAt(index);
					SelectedAttribute.ConstructorArguments.Insert(index, AttributeArgumentEditor.SelectedArgument);
				}

				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void EditOverrideForm_Load(object sender, EventArgs e)
		{
			if (SelectedArgument != null)
				AttributeArgumentEditor.SelectedArgument = SelectedArgument.Value;
		}

		#endregion
	}
}