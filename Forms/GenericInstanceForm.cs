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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Editors;

namespace Reflexil.Forms
{
	public static class GenericInstanceFormFactory
	{
		internal static IGenericInstanceForm GetForm(IGenericParameterProvider provider, IGenericParameterProvider context)
		{
			if (provider == null)
				return null;

			if (!provider.HasGenericParameters)
				return null;

			if (provider is MethodReference)
				return new GenericInstanceMethodForm(provider, context);

			if (provider is TypeReference)
				return new GenericInstanceTypeForm(provider, context);

			return null;
		}
	}

	public interface IGenericInstanceForm : IDisposable
	{
		IGenericInstance GenericInstance { get; }
		DialogResult ShowDialog();
	}

	internal partial class GenericInstanceForm<T> : Form, IGenericInstanceForm where T : IGenericInstance
	{
		protected readonly IGenericParameterProvider Provider;
		protected readonly IGenericParameterProvider Context;

		public IGenericInstance GenericInstance
		{
			get
			{

				var arguments = (FlowPanel.Controls.Cast<GroupBox>()
					.Select(box => (TypeSpecificationEditor) box.Controls[0]))
					.Select(editor => editor.SelectedTypeReference)
					.ToList();

				// We have undifined arguments, return null to give back the provider definition (like when using ldtoken)
				if (arguments.Any(a => a == null))
					return null;

				return CreateGenericInstance(arguments);
			}
		}

		protected virtual T CreateGenericInstance(IEnumerable<TypeReference> arguments)
		{
			return default(T);
		}

		protected GenericInstanceForm(IGenericParameterProvider provider, IGenericParameterProvider context)
		{
			InitializeComponent();

			if (provider == null)
				return;

			Title.Text = string.Format(Title.Text, provider, provider.GenericParameters.Count);
			Provider = provider;
			Context = context;

			foreach (var parameter in provider.GenericParameters)
			{
				var box = new GroupBox {Width = 408, Height = 119, Text = parameter.Name};
				var editor = new TypeSpecificationEditor { Left = 8, Top = 20, AllowReference = false, AllowPointer = false, Context = context};
				box.Controls.Add(editor);
				FlowPanel.Controls.Add(box);
			}
		}
	}
}