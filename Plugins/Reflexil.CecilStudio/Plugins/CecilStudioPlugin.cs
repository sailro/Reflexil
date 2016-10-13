/* Reflexil Copyright (c) 2007-2016 Sebastien LEBRETON

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
using System.Linq;
using Mono.Cecil;

namespace Reflexil.Plugins.CecilStudio
{
	internal class CecilStudioPlugin : BasePlugin
	{
		public override string HostApplication
		{
			get { return "Cecil Studio"; }
		}

		public CecilStudioPlugin(IPackage package) : base(package)
		{
		}

		public override bool IsAssemblyLinkedResourceHandled(object item)
		{
			return item is AssemblyLinkedResource;
		}

		public override AssemblyLinkedResource GetAssemblyLinkedResource(object item)
		{
			return item as AssemblyLinkedResource;
		}

		public override LinkedResource GetLinkedResource(object item)
		{
			return item as LinkedResource;
		}

		public override bool IsLinkedResourceHandled(object item)
		{
			return item is LinkedResource;
		}

		public override bool IsAssemblyNameReferenceHandled(object item)
		{
			return item is AssemblyNameReference;
		}

		public override bool IsAssemblyDefinitionHandled(object item)
		{
			return item is AssemblyDefinition;
		}

		public override bool IsTypeDefinitionHandled(object item)
		{
			return item is TypeDefinition;
		}

		public override bool IsModuleDefinitionHandled(object item)
		{
			return item is ModuleDefinition;
		}

		public override bool IsMethodDefinitionHandled(object item)
		{
			return item is MethodDefinition;
		}

		public override bool IsPropertyDefinitionHandled(object item)
		{
			return item is PropertyDefinition;
		}

		public override bool IsFieldDefinitionHandled(object item)
		{
			return item is FieldDefinition;
		}

		public override bool IsEventDefinitionHandled(object item)
		{
			return item is EventDefinition;
		}

		public override bool IsEmbeddedResourceHandled(object item)
		{
			return item is EmbeddedResource;
		}

		public override MethodDefinition GetMethodDefinition(object item)
		{
			return item as MethodDefinition;
		}

		public override EmbeddedResource GetEmbeddedResource(object item)
		{
			return item as EmbeddedResource;
		}

		public override IAssemblyContext GetAssemblyContext(string location)
		{
			return GetAssemblyContext<CecilStudioAssemblyContext>(location);
		}

		public override AssemblyNameReference GetAssemblyNameReference(object item)
		{
			return item as AssemblyNameReference;
		}

		public override AssemblyDefinition GetAssemblyDefinition(object item)
		{
			return item as AssemblyDefinition;
		}

		public override PropertyDefinition GetPropertyDefinition(object item)
		{
			return item as PropertyDefinition;
		}

		public override FieldDefinition GetFieldDefinition(object item)
		{
			return item as FieldDefinition;
		}

		public override EventDefinition GetEventDefinition(object item)
		{
			return item as EventDefinition;
		}

		public override TypeDefinition GetTypeDefinition(object item)
		{
			return item as TypeDefinition;
		}

		public override ModuleDefinition GetModuleDefinition(object item)
		{
			return item as ModuleDefinition;
		}

		public override AssemblyDefinition LoadAssembly(string location, bool loadsymbols)
		{
			// Stay in sync with Cecil Studio browser, don't load anything but reuse previously loaded assembly
			return
				Package.HostAssemblies.Cast<CecilStudioAssemblyWrapper>()
					.Select(w => w.AssemblyDefinition)
					.FirstOrDefault(adef => adef.MainModule.Image.FileName.Equals(location, StringComparison.OrdinalIgnoreCase));
		}

		public override IAssemblyContext GetAssemblyContext(object item)
		{
			throw new NotImplementedException();
		}
	}
}