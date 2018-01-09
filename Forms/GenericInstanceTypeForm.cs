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
using Mono.Cecil;
using Reflexil.Plugins;
using Reflexil.Utils;

namespace Reflexil.Forms
{
	internal class GenericInstanceTypeForm : BaseGenericInstanceTypeForm
	{
		public GenericInstanceTypeForm(IGenericParameterProvider provider, IGenericParameterProvider context) : base(provider, context)
		{
			if (!(provider is TypeReference))
				throw new ArgumentException();
		}

		protected override GenericInstanceType CreateGenericInstance(IEnumerable<TypeReference> arguments)
		{
			var instance = new GenericInstanceType(Provider as TypeReference);

			foreach (var argument in arguments)
				instance.GenericArguments.Add(argument);

			// Now we need to import type given the current module AND the given generic context
			var handler = PluginFactory.GetInstance().Package.ActiveHandler;
			var module = handler.TargetObjectModule;
			instance = (GenericInstanceType) CecilImporter.Import(module, instance, Context);

			return instance;
		}
	}

	internal class BaseGenericInstanceTypeForm : GenericInstanceForm<GenericInstanceType>
	{
		public BaseGenericInstanceTypeForm() : base(null, null)
		{
		}

		public BaseGenericInstanceTypeForm(IGenericParameterProvider provider, IGenericParameterProvider context) : base(provider, context)
		{
		}
	}
}
