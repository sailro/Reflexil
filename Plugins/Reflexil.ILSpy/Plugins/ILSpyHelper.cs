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

using System.Linq;
using ICSharpCode.Decompiler.TypeSystem;
using Mono.Cecil;
using ICSharpCode.Decompiler.Metadata;
using Resource = Mono.Cecil.Resource;
using AssemblyNameReference = Mono.Cecil.AssemblyNameReference;
using IResource = ICSharpCode.Decompiler.Metadata.Resource;

namespace Reflexil.Plugins.ILSpy
{
	internal static class ILSpyHelper
	{
		public static TypeDefinition FindMatchingType(AssemblyDefinition adef, ITypeDefinition item)
		{
			return adef.MainModule.GetTypes().FirstOrDefault(t => t.MetadataToken.token == item.MetadataToken.GetHashCode());
		}

		public static MethodDefinition FindMatchingMethod(TypeDefinition tdef, IMethod item)
		{
			return tdef.Methods.FirstOrDefault(m => m.MetadataToken.token == item.MetadataToken.GetHashCode());
		}

		public static PropertyDefinition FindMatchingProperty(TypeDefinition tdef, IProperty item)
		{
			return tdef.Properties.FirstOrDefault(p => p.MetadataToken.token == item.MetadataToken.GetHashCode());
        }

		public static FieldDefinition FindMatchingField(TypeDefinition tdef, IField item)
		{
			return tdef.Fields.FirstOrDefault(f => f.MetadataToken.token == item.MetadataToken.GetHashCode());
        }

		public static EventDefinition FindMatchingEvent(TypeDefinition tdef, IEvent item)
		{
			return tdef.Events.FirstOrDefault(e => e.MetadataToken.token == item.MetadataToken.GetHashCode());
        }

		public static AssemblyNameReference FindMatchingAssemblyReference(AssemblyDefinition adef, IAssemblyReference item)
		{
			return adef.MainModule.AssemblyReferences.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static Resource FindMatchingResource(AssemblyDefinition adef, IResource item)
		{
			return adef.MainModule.Resources.FirstOrDefault(p => p.Name == item.Name);
		}
	}
}