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
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Plugins;
using Reflexil.Utils;

namespace Reflexil.Forms
{
	internal class GenericInstanceMethodForm : BaseGenericInstanceMethodForm
	{
		public GenericInstanceMethodForm(IGenericParameterProvider provider, IGenericParameterProvider context)
			: base(provider, context)
		{
			if (!(provider is MethodReference))
				throw new ArgumentException();
		}

		protected override GenericInstanceMethod CreateGenericInstance(IEnumerable<TypeReference> arguments)
		{
			var mref = (MethodReference) Provider;

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

			var instance = new GenericInstanceMethod(reference);
			foreach (var argument in arguments)
				instance.GenericArguments.Add(argument);

			// Now we need to import method given the current module AND the given generic context
			var handler = PluginFactory.GetInstance().Package.ActiveHandler;
			var module = handler.TargetObjectModule;
			instance = (GenericInstanceMethod) CecilImporter.Import(module, instance, Context);

			return instance;
		}

		private TypeReference HandleGenericType(TypeReference tref)
		{
			var form = GenericInstanceFormFactory.GetForm(tref, Context);
			if (form != null && form.ShowDialog() == DialogResult.OK)
			{
				var instance = (TypeReference) form.GenericInstance;
				if (instance != null)
					return instance;
			}

			return tref;
		}
	}

	internal class BaseGenericInstanceMethodForm : GenericInstanceForm<GenericInstanceMethod>
	{
		public BaseGenericInstanceMethodForm() : base(null, null)
		{
		}

		public BaseGenericInstanceMethodForm(IGenericParameterProvider provider, IGenericParameterProvider context) : base(provider, context)
		{
		}
	}
}
