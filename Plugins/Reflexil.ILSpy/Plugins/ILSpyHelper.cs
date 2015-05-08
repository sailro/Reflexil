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

extern alias ilspycecil;

using System.Linq;
using Mono.Cecil;

using icAssemblyNameReference = ilspycecil::Mono.Cecil.AssemblyNameReference;
using icEventDefinition = ilspycecil::Mono.Cecil.EventDefinition;
using icFieldDefinition = ilspycecil::Mono.Cecil.FieldDefinition;
using icMethodDefinition = ilspycecil::Mono.Cecil.MethodDefinition;
using icPropertyDefinition = ilspycecil::Mono.Cecil.PropertyDefinition;
using icTypeDefinition = ilspycecil::Mono.Cecil.TypeDefinition;
using icResource = ilspycecil::Mono.Cecil.Resource;

namespace Reflexil.Plugins.ILSpy
{
	internal static class ILSpyHelper
	{
		public static TypeDefinition FindMatchingType(AssemblyDefinition adef, icTypeDefinition ictdef)
		{
			return adef.MainModule.GetTypes().FirstOrDefault(t => t.FullName == ictdef.FullName);
		}

		public static MethodDefinition FindMatchingMethod(TypeDefinition tdef, icMethodDefinition item)
		{
			return tdef.Methods.FirstOrDefault(m => m.ToString() == item.ToString());
		}

		public static PropertyDefinition FindMatchingProperty(TypeDefinition tdef, icPropertyDefinition item)
		{
			return tdef.Properties.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static FieldDefinition FindMatchingField(TypeDefinition tdef, icFieldDefinition item)
		{
			return tdef.Fields.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static EventDefinition FindMatchingEvent(TypeDefinition tdef, icEventDefinition item)
		{
			return tdef.Events.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static AssemblyNameReference FindMatchingAssemblyReference(AssemblyDefinition adef, icAssemblyNameReference item)
		{
			return adef.MainModule.AssemblyReferences.FirstOrDefault(p => p.ToString() == item.ToString());
		}

		public static Resource FindMatchingResource(AssemblyDefinition adef, icResource item)
		{
			return adef.MainModule.Resources.FirstOrDefault(p => p.ToString() == item.ToString());
		}
	}
}
