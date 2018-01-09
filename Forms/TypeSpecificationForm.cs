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

using System.ComponentModel;
using System.Windows.Forms;
using Mono.Cecil;

namespace Reflexil.Forms
{
	internal partial class TypeSpecificationForm : Form
	{
		protected bool IsFormComplete
		{
			get
			{
				foreach (Control ctl in Controls)
				{
					ctl.Focus();
					if (!Validate()) return false;
				}
				return true;
			}
		}

		public TypeSpecificationForm()
		{
			InitializeComponent();
		}

		public virtual DialogResult ShowDialog(IGenericParameterProvider context)
		{
			TypeSpecificationEditor.Context = context;
			return ShowDialog();
		}

		private void ItemName_Validating(object sender, CancelEventArgs e)
		{
			if (ItemName.Visible && ItemName.Text == string.Empty)
			{
				ErrorProvider.SetError(ItemName, "Name is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(ItemName, string.Empty);
			}
		}

		private void TypeSpecificationEditor_Validating(object sender, CancelEventArgs e)
		{
			bool validated;
			var typeSpecification = TypeSpecificationEditor.SelectedTypeReference as TypeSpecification;
			if (typeSpecification != null)
			{
				validated = typeSpecification.ElementType != null;
			}
			else
			{
				validated = TypeSpecificationEditor.SelectedTypeReference != null;
			}

			if (!validated)
			{
				ErrorProvider.SetError(TypeSpecificationEditor, "Type is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(TypeSpecificationEditor, string.Empty);
			}
		}
	}
}