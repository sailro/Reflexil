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

using System.ComponentModel;
using System.Windows.Forms;
using Mono.Cecil;

#endregion

namespace Reflexil.Forms
{
	public partial class InterfaceForm : Form
	{
		#region Properties

		public TypeDefinition TypeDefinition { get; private set; }
		public TypeReference SelectedTypeReference { get; private set; }

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

		#endregion

		#region Methods

		public InterfaceForm()
		{
			InitializeComponent();
		}

		public virtual DialogResult ShowDialog(TypeDefinition tdef, TypeReference selected)
		{
			TypeDefinition = tdef;
			SelectedTypeReference = selected;
			return ShowDialog();
		}

		#endregion

		#region Events

		private void TypeReferenceEditor_Validating(object sender, CancelEventArgs e)
		{
			if (TypeReferenceEditor.SelectedOperand == null)
			{
				ErrorProvider.SetError(TypeReferenceEditor, "Type is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(TypeReferenceEditor, string.Empty);
			}
		}

		#endregion
	}
}