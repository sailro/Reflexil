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
	class GenericInstanceMethodForm : GenericInstanceForm<GenericInstanceMethod>
	{
		public GenericInstanceMethodForm(IGenericParameterProvider provider)
			: base(provider)
		{
			if (!(provider is MethodReference))
				throw new ArgumentException();
		}


		protected override GenericInstanceMethod CreateGenericInstance()
		{
			var mref = (MethodReference)Provider;

			var reference = new MethodReference
			{
				Name = mref.Name,
				DeclaringType = HandleGenericType(mref.DeclaringType),
				HasThis = mref.HasThis,
				ExplicitThis = mref.ExplicitThis,
				ReturnType = mref.ReturnType,
				CallingConvention = mref.CallingConvention,
			};

			foreach (var param in mref.Parameters)
				reference.Parameters.Add(new ParameterDefinition(param.ParameterType));

			foreach (var genParam in mref.GenericParameters)
				reference.GenericParameters.Add(new GenericParameter(genParam.Name, reference));

			return new GenericInstanceMethod(reference);
		}

		private static TypeReference HandleGenericType(TypeReference tref)
		{
			var form = GenericInstanceFormFactory.GetForm(tref);
			if (form != null && form.ShowDialog() == DialogResult.OK)
				return (TypeReference) form.GenericInstance;

			return tref;
		}
	}
}
