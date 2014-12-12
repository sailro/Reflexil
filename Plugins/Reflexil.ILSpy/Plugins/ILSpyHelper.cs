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

namespace Reflexil.ILSpy.Plugins
{
	class ILSpyHelper
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
