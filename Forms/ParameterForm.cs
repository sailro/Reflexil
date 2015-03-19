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
using System.ComponentModel;

#endregion

namespace Reflexil.Forms
{
	public partial class ParameterForm : TypeSpecificationForm
	{
		#region Properties

		public MethodDefinition MethodDefinition { get; private set; }
		public ParameterDefinition SelectedParameter { get; private set; }

		#endregion

		#region Methods

		public ParameterForm()
		{
			InitializeComponent();
		}

		protected ParameterDefinition CreateParameter()
		{
			var prm =
				new ParameterDefinition(MethodDefinition.DeclaringType.Module.Import(TypeSpecificationEditor.SelectedTypeReference))
				{
					Name = ItemName.Text,
				};

			var attributeProvider = Attributes.Item as ParameterDefinition;
			if (attributeProvider != null)
				prm.Attributes = attributeProvider.Attributes;

			ConstantEditor.CopyStateTo(prm);

			return prm;
		}

		public virtual DialogResult ShowDialog(MethodDefinition mdef, ParameterDefinition selected)
		{
			MethodDefinition = mdef;
			SelectedParameter = selected;
			return base.ShowDialog(mdef);
		}

		#endregion

		#region Events

		private void Constant_Validating(object sender, CancelEventArgs e)
		{
			try
			{
				ErrorProvider.SetError(ConstantEditor, string.Empty);
			}
			catch (Exception)
			{
				ErrorProvider.SetError(ConstantEditor, "Unable to convert input");
				e.Cancel = true;
			}
		}

		#endregion
	}
}