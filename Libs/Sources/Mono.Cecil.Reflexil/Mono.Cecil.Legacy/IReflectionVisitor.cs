//
// IReflectionVisitor.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2011 Jb Evain
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

namespace Mono.Cecil {

	public interface IReflectionVisitor {

        void VisitModuleDefinition(ModuleDefinition module);
        void VisitTypeDefinitionCollection(Mono.Collections.Generic.Collection<TypeDefinition> types);
        void VisitTypeDefinition(TypeDefinition type);
        void VisitTypeReferenceCollection(Mono.Collections.Generic.Collection<TypeReference> refs);
        void VisitTypeReference(TypeReference type);
        void VisitMemberReferenceCollection(Mono.Collections.Generic.Collection<MemberReference> members);
        void VisitMemberReference(MemberReference member);
        void VisitInterfaceCollection(Mono.Collections.Generic.Collection<TypeReference> interfaces);
        void VisitInterface(TypeReference interf);
        void VisitExternTypeCollection(Mono.Collections.Generic.Collection<TypeReference> externs);
        void VisitExternType(TypeReference externType);
        void VisitOverrideCollection(Mono.Collections.Generic.Collection<MethodReference> meth);
        void VisitOverride(MethodReference ov);
        void VisitNestedTypeCollection(Mono.Collections.Generic.Collection<TypeDefinition> nestedTypes);
        void VisitNestedType(TypeDefinition nestedType);
        void VisitParameterDefinitionCollection(Mono.Collections.Generic.Collection<ParameterDefinition> parameters);
        void VisitParameterDefinition(ParameterDefinition parameter);
        void VisitMethodDefinitionCollection(Mono.Collections.Generic.Collection<MethodDefinition> methods);
        void VisitMethodDefinition(MethodDefinition method);
		void VisitMethodReference(MethodReference method);
		void VisitConstructorCollection(Mono.Collections.Generic.Collection<MethodDefinition> ctors);
        void VisitConstructor(MethodDefinition ctor);
        void VisitPInvokeInfo(PInvokeInfo pinvk);
        void VisitEventDefinitionCollection(Mono.Collections.Generic.Collection<EventDefinition> events);
        void VisitEventDefinition(EventDefinition evt);
        void VisitFieldDefinitionCollection(Mono.Collections.Generic.Collection<FieldDefinition> fields);
        void VisitFieldDefinition(FieldDefinition field);
        void VisitPropertyDefinitionCollection(Mono.Collections.Generic.Collection<PropertyDefinition> properties);
        void VisitPropertyDefinition(PropertyDefinition property);
        void VisitSecurityDeclarationCollection(Mono.Collections.Generic.Collection<SecurityDeclaration> secDecls);
        void VisitSecurityDeclaration(SecurityDeclaration secDecl);
        void VisitCustomAttributeCollection(Mono.Collections.Generic.Collection<CustomAttribute> customAttrs);
        void VisitCustomAttribute(CustomAttribute customAttr);
        void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams);
        void VisitGenericParameter(GenericParameter genparam);
        void VisitMarshalSpec(MarshalInfo marshalSpec);

        void TerminateModuleDefinition(ModuleDefinition module);
	}
}
