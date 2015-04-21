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
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Editors;

#endregion

namespace Reflexil.Forms
{
	public partial class GenericInstanceForm<T> : Form where T : IGenericInstance
	{
		private readonly IGenericParameterProvider _provider;

		#region Properties

		public T GenericInstance
		{
			get
			{
				var result = CreateGenericInstance();
				foreach (GroupBox box in FlowPanel.Controls)
				{
					var editor = (TypeSpecificationEditor) box.Controls[0];
					result.GenericArguments.Add(editor.SelectedTypeReference);
				}
				
				return result;
			}
		}

		#endregion

		#region Events

		private void Ok_Click(object sender, EventArgs e)
		{
			if (GenericInstance.GenericArguments.Any(a => a == null))
				MessageBox.Show("Please set properly all arguments", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
				DialogResult = DialogResult.OK;
		}
	
		#endregion

		#region Methods

		protected T CreateGenericInstance()
		{
			return default(T);
		}

		public GenericInstanceForm(IGenericParameterProvider provider)
		{
			InitializeComponent();

			Title.Text = String.Format(Title.Text, provider, provider.GenericParameters.Count);
			_provider = provider;

			foreach (var parameter in provider.GenericParameters)
			{
				var box = new GroupBox {Width = 408, Height = 119, Text = parameter.Name};
				var editor = new TypeSpecificationEditor { Left = 8, Top = 20, AllowReference = false, AllowPointer = false};
				box.Controls.Add(editor);
				FlowPanel.Controls.Add(box);
			}
		}

		#endregion

	}
}