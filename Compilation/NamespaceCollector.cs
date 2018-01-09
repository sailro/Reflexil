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
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Collections.Generic;
using Mono.Cecil.Cil;

namespace Reflexil.Compilation
{
	internal class NamespaceCollector : IReflectionVisitor
	{
		private TypeDefinition _type;
		private HashSet<string> _namespaces = new HashSet<string>();

		public NamespaceCollector(TypeDefinition type)
		{
			_type = type;
		}

		private void VisitString(string str)
		{
			if (string.IsNullOrEmpty(str))
				return;

			if (_namespaces.Contains(str))
				return;

			_namespaces.Add(str);
		}

		internal IList<string> Collect()
		{
			_namespaces.Clear();
			VisitString("System");
			VisitString("System.Collections.Generic");
			VisitString("System.Text");
			VisitTypeDefinition(_type);
			return _namespaces.ToList();
		}

		public void VisitTypeDefinition(TypeDefinition type)
		{
			VisitNestedTypeCollection(type.NestedTypes);
			VisitFieldDefinitionCollection(type.Fields);
			VisitPropertyDefinitionCollection(type.Properties);
			VisitEventDefinitionCollection(type.Events);
			VisitInterfaceCollection(type.Interfaces);
			VisitMethodDefinitionCollection(type.Methods);
		}

		public void VisitNestedTypeCollection(Collection<TypeDefinition> nestedTypes)
		{
			foreach (var tdef in nestedTypes)
				VisitNestedType(tdef);
		}

		public void VisitNestedType(TypeDefinition nestedType)
		{
			VisitTypeDefinition(nestedType);
		}

		public void VisitFieldDefinitionCollection(Collection<FieldDefinition> fields)
		{
			foreach (var fdef in fields)
				VisitFieldDefinition(fdef);
		}

		public void VisitFieldDefinition(FieldDefinition field)
		{
			VisitTypeReference(field.FieldType);
		}

		public void VisitTypeReference(TypeReference type)
		{
			VisitString(type.Namespace);
		}

		public void VisitPropertyDefinitionCollection(Collection<PropertyDefinition> properties)
		{
			foreach (var pdef in properties)
				VisitPropertyDefinition(pdef);
		}

		public void VisitPropertyDefinition(PropertyDefinition property)
		{
			VisitTypeReference(property.PropertyType);
		}

		public void VisitEventDefinitionCollection(Collection<EventDefinition> events)
		{
			foreach (var evt in events)
				VisitEventDefinition(evt);
		}

		public void VisitEventDefinition(EventDefinition evt)
		{
			VisitTypeReference(evt.EventType);
		}

		public void VisitInterfaceCollection(Collection<InterfaceImplementation> interfaces)
		{
			foreach (var idef in interfaces)
				VisitInterface(idef.InterfaceType);
		}

		public void VisitInterface(TypeReference interf)
		{
			VisitString(interf.Namespace);
		}

		public void VisitMethodDefinitionCollection(Collection<MethodDefinition> methods)
		{
			foreach (var mdef in methods)
				VisitMethodDefinition(mdef);
		}

		public void VisitMethodDefinition(MethodDefinition method)
		{
			VisitTypeReference(method.ReturnType);
			VisitParameterDefinitionCollection(method.Parameters);

			if (method.Body == null)
				return;

			foreach (var ins in method.Body.Instructions)
			{
				var mref = ins.Operand as MethodReference;
				if (mref != null)
					VisitMethodReference(mref);
			}
		}

		public void VisitMethodReference(MethodReference method)
		{
			VisitTypeReference(method.DeclaringType);
		}

		public void VisitParameterDefinitionCollection(Collection<ParameterDefinition> parameters)
		{
			foreach (var pdef in parameters)
				VisitParameterDefinition(pdef);
		}

		public void VisitParameterDefinition(ParameterDefinition parameter)
		{
			VisitTypeReference(parameter.ParameterType);
		}

		public void VisitModuleDefinition(ModuleDefinition module) { }
		public void VisitTypeDefinitionCollection(Collection<TypeDefinition> types) { }
		public void VisitTypeReferenceCollection(Collection<TypeReference> refs) { }
		public void VisitMemberReferenceCollection(Collection<MemberReference> members) { }
		public void VisitMemberReference(MemberReference member) { }
		public void VisitExternTypeCollection(Collection<TypeReference> externs) { }
		public void VisitExternType(TypeReference externType) { }
		public void VisitOverrideCollection(Collection<MethodReference> meth) { }
		public void VisitOverride(MethodReference ov) { }
		public void VisitConstructorCollection(Collection<MethodDefinition> ctors) { }
		public void VisitConstructor(MethodDefinition ctor) { }
		public void VisitPInvokeInfo(PInvokeInfo pinvk) { }
		public void VisitSecurityDeclarationCollection(Collection<SecurityDeclaration> secDecls) { }
		public void VisitSecurityDeclaration(SecurityDeclaration secDecl) { }
		public void VisitCustomAttributeCollection(Collection<CustomAttribute> customAttrs) { }
		public void VisitCustomAttribute(CustomAttribute customAttr) { }
		public void VisitGenericParameterCollection(Collection<GenericParameter> genparams) { }
		public void VisitGenericParameter(GenericParameter genparam) { }
		public void VisitMarshalSpec(MarshalInfo marshalSpec) { }
		public void TerminateModuleDefinition(ModuleDefinition module) { }
	}
}