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
	public static class GenericInstanceFormFactory
	{
		public static IGenericInstanceForm GetForm(MemberReference reference)
		{
			var provider = reference as IGenericParameterProvider;
			if (provider == null)
				return null;

			if (provider is MethodReference)
				return new GenericInstanceMethodForm(provider);

			if (provider is TypeReference)
				return new GenericInstanceTypeForm(provider);

			return null;
		}
	}

	public interface IGenericInstanceForm : IDisposable
	{
		IGenericInstance GenericInstance { get; }
		DialogResult ShowDialog();
	}

	public partial class GenericInstanceForm<T> : Form, IGenericInstanceForm where T : IGenericInstance
	{
		protected readonly IGenericParameterProvider Provider;

		#region Properties

		public IGenericInstance GenericInstance
		{
			get
			{
				var result = CreateGenericInstance();

				foreach (var editor in from GroupBox box in FlowPanel.Controls select (TypeSpecificationEditor) box.Controls[0])
					result.GenericArguments.Add(editor.SelectedTypeReference);
				
				return result;
			}
		}

		#endregion

		#region Methods

		protected virtual T CreateGenericInstance()
		{
			return default(T);
		}

		protected GenericInstanceForm(IGenericParameterProvider provider)
		{
			InitializeComponent();

			Title.Text = String.Format(Title.Text, provider, provider.GenericParameters.Count);
			Provider = provider;

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