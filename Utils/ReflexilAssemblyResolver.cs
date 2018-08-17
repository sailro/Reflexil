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
using Mono.Cecil;
using System.IO;
using Reflexil.Plugins;

namespace Reflexil.Utils
{
	public class ReflexilAssemblyResolver : DefaultAssemblyResolver
	{
		private readonly ReaderParameters _parameters;

		public ReflexilAssemblyResolver(ReaderParameters parameters)
		{
			_parameters = parameters;
			_parameters.AssemblyResolver = this;
		}

		public new void RegisterAssembly(AssemblyDefinition assembly)
		{
			base.RegisterAssembly(assembly);
		}

		public override AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
		{
			// Try to find the assembly in the Host list first, then use the default resolver
			var plugin = PluginFactory.GetInstance();
			if (plugin == null || plugin.Package == null)
				return base.Resolve(name, _parameters);

			foreach (var wrapper in plugin.Package.HostAssemblies)
			{
				if (name.Name != wrapper.Name)
					continue;

				var context = plugin.GetAssemblyContext(wrapper.Location);
				var adef = context.AssemblyDefinition;

				if (adef.FullName == name.FullName)
					return adef;
			}

			return base.Resolve(name, _parameters);
		}

		internal AssemblyDefinition ReadAssembly(string location)
		{
			AddSearchDirectory(Path.GetDirectoryName(location));
			var module = ModuleDefinition.ReadModule(location, _parameters);

			return module.Assembly;
		}
	}
}