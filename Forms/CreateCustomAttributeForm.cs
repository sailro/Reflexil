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
using Mono.Cecil;

#endregion

namespace Reflexil.Forms
{
	public partial class CreateCustomAttributeForm : CustomAttributeForm
	{
		#region Methods

		public CreateCustomAttributeForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		private void ButInsertBefore_Click(Object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				FixAndUpdateWorkingAttribute();
				SelectedProvider.CustomAttributes.Insert(SelectedProvider.CustomAttributes.IndexOf(SelectedAttribute),
					WorkingAttribute);
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void ButInsertAfter_Click(Object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				FixAndUpdateWorkingAttribute();
				SelectedProvider.CustomAttributes.Insert(SelectedProvider.CustomAttributes.IndexOf(SelectedAttribute) + 1,
					WorkingAttribute);
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void ButAppend_Click(Object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				FixAndUpdateWorkingAttribute();
				SelectedProvider.CustomAttributes.Add(WorkingAttribute);
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void CreateOverrideForm_Load(object sender, EventArgs e)
		{
			ButInsertBefore.Enabled = (SelectedAttribute != null);
			ButInsertAfter.Enabled = (SelectedAttribute != null);

			var newca = new CustomAttribute(null);
			WorkingAttribute = newca;
			ConstructorArguments.Bind(WorkingAttribute);
			Fields.Bind(WorkingAttribute);
			Properties.Bind(WorkingAttribute);
		}

		#endregion
	}
}