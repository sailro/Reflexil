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
using System.Reflection;
using JustDecompile.API.Core;
using Mono.Cecil;

namespace Reflexil.Plugins.JustDecompile
{
	internal static class JustDecompileHelper
	{
		public static TypeDefinition FindMatchingType(AssemblyDefinition adef, ITypeDefinition ictdef)
		{
			return adef.MainModule.GetTypes().FirstOrDefault(t => t.FullName == ictdef.FullName);
		}

		public static MethodDefinition FindMatchingMethod(TypeDefinition tdef, IMethodDefinition item)
		{
			return tdef.Methods.FirstOrDefault(m => m.ToString() == item.ToString());
		}

		public static PropertyDefinition FindMatchingProperty(TypeDefinition tdef, IPropertyDefinition item)
		{
			return tdef.Properties.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static FieldDefinition FindMatchingField(TypeDefinition tdef, IFieldDefinition item)
		{
			return tdef.Fields.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static EventDefinition FindMatchingEvent(TypeDefinition tdef, IEventDefinition item)
		{
			return tdef.Events.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static AssemblyNameReference FindMatchingAssemblyReference(AssemblyDefinition adef, object item)
		{
			return adef.MainModule.AssemblyReferences.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static Resource FindMatchingResource(AssemblyDefinition adef, IResource item)
		{
			return adef.MainModule.Resources.FirstOrDefault(p => p.Name == item.Name);
		}

		public static object ExtractCecilAssemblyNameReference(IAssemblyNameReference anr)
		{
			var field =
				anr.GetType()
					.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
					.FirstOrDefault(f => f.FieldType.FullName == typeof(AssemblyNameReference).FullName);

			if (field == null)
				return null;

			return field.GetValue(anr);
		}
	}
}