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
using Mono.Cecil;
using System.IO;
using Reflexil.Plugins;

#endregion

namespace Reflexil.Utils
{
	public class ReflexilAssemblyResolver : DefaultAssemblyResolver
	{
		#region Methods

		public AssemblyDefinition ReadAssembly(string file, ReaderParameters parameters)
		{
			return ReadAssembly(ReadModule(file, parameters));
		}

		public AssemblyDefinition ReadAssembly(ModuleDefinition module)
		{
			var assembly = module.Assembly;
			if (assembly == null)
				throw new ArgumentException();

			return assembly;
		}

		public ModuleDefinition ReadModule(string file, ReaderParameters parameters)
		{
			if (parameters != null)
				parameters.AssemblyResolver = this;

			var module = ModuleDefinition.ReadModule(file, parameters);
			AddSearchDirectory(Path.GetDirectoryName(file));

			return module;
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
				return base.Resolve(name, parameters); ;

			foreach (var wrapper in plugin.Package.HostAssemblies)
			{
				if (name.Name == wrapper.Name)
				{
					var context = plugin.GetAssemblyContext(wrapper.Location);
					var adef = context.AssemblyDefinition;

					if (adef.FullName == name.FullName)
						return adef;
				}
			}

			return base.Resolve(name, parameters);
		}

		#endregion
	}
}