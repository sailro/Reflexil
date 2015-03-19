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
	public partial class EditCustomAttributeForm : CustomAttributeForm
	{
		#region Methods

		public EditCustomAttributeForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		private void ButUpdate_Click(object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				FixAndUpdateWorkingAttribute();
				var index = SelectedProvider.CustomAttributes.IndexOf(SelectedAttribute);
				SelectedProvider.CustomAttributes.RemoveAt(index);
				SelectedProvider.CustomAttributes.Insert(index, WorkingAttribute);
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void EditOverrideForm_Load(object sender, EventArgs e)
		{
			var clone = new CustomAttribute(SelectedAttribute.Constructor);

			foreach (var ctorarg in SelectedAttribute.ConstructorArguments)
				clone.ConstructorArguments.Add(ctorarg);

			foreach (var fnarg in SelectedAttribute.Fields)
				clone.Fields.Add(fnarg);

			foreach (var pnarg in SelectedAttribute.Properties)
				clone.Properties.Add(pnarg);

			WorkingAttribute = clone;
			ConstructorArguments.Bind(clone);
			Fields.Bind(clone);
			Properties.Bind(clone);
			Constructor.SelectedOperand = clone.Constructor;
			AttributeType.SelectedOperand = clone.AttributeType;
		}

		#endregion
	}
}