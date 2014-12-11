extern alias ilspycecil;

using System;
using System.Linq;
using Mono.Cecil;
using icEventDefinition = ilspycecil::Mono.Cecil.EventDefinition;
using icFieldDefinition = ilspycecil::Mono.Cecil.FieldDefinition;
using icMethodDefinition = ilspycecil::Mono.Cecil.MethodDefinition;
using icPropertyDefinition = ilspycecil::Mono.Cecil.PropertyDefinition;
using icTypeDefinition = ilspycecil::Mono.Cecil.TypeDefinition;

namespace Reflexil.ILSpy.Plugins
{
	class ILSpyHelper
	{
		public static TypeDefinition FindMatchingType(AssemblyDefinition adef, icTypeDefinition ictdef)
		{
			return adef.MainModule.Types.FirstOrDefault(t => t.FullName == ictdef.FullName);
		}

		public static MethodDefinition FindMatchingMethod(TypeDefinition tdef, icMethodDefinition item)
		{
			return tdef.Methods.FirstOrDefault(m => m.ToString() == item.ToString());
		}

		public static PropertyDefinition FindMatchingProperty(TypeDefinition tdef, icPropertyDefinition item)
		{
			throw new NotImplementedException();
		}

		public static FieldDefinition FindMatchingField(TypeDefinition tdef, icFieldDefinition item)
		{
			throw new NotImplementedException();
		}

		public static EventDefinition FindMatchingEvent(TypeDefinition tdef, icEventDefinition item)
		{
			throw new NotImplementedException();
		}
	}
}
