//
// DefaultReferenceImporter.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2010 Jb Evain
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

using System;

namespace Mono.Cecil {

	public class DefaultImporter : IImporter {

		ModuleDefinition m_module;

		public ModuleDefinition Module {
			get { return m_module; }
		}

		public DefaultImporter (ModuleDefinition module)
		{
			m_module = module;
		}

		public AssemblyNameReference ImportAssembly (AssemblyNameReference asm)
		{
			AssemblyNameReference asmRef = GetAssemblyNameReference (asm);
			if (asmRef != null)
				return asmRef;

			asmRef = new AssemblyNameReference (
				asm.Name, asm.Version);
			asmRef.PublicKeyToken = asm.PublicKeyToken;
			asmRef.HashAlgorithm = asm.HashAlgorithm;
            asmRef.Culture = asm.Culture;
            m_module.AssemblyReferences.Add(asmRef);
			return asmRef;
		}

		AssemblyNameReference GetAssemblyNameReference (AssemblyNameReference asm)
		{
			foreach (AssemblyNameReference reference in m_module.AssemblyReferences)
				if (reference.FullName == asm.FullName)
					return reference;

			return null;
		}

		TypeSpecification GetTypeSpec (TypeSpecification original, ImportContext context)
		{
			TypeSpecification typeSpec;

			TypeReference elementType = ImportTypeReference (original.ElementType, context);
			if (original is PointerType) {
				typeSpec = new PointerType (elementType);
			} else if (original is ArrayType) { // deal with complex arrays
				typeSpec = new ArrayType (elementType);
			} else if (original is ByReferenceType) {
				typeSpec = new ByReferenceType (elementType);
			} else if (original is GenericInstanceType) {
				GenericInstanceType git = original as GenericInstanceType;
				GenericInstanceType genElemType = new GenericInstanceType (elementType);

				context.GenericContext.CheckProvider (genElemType.GetElementType(), git.GenericArguments.Count);
				foreach (TypeReference arg in git.GenericArguments)
					genElemType.GenericArguments.Add (ImportTypeReference (arg, context));

				typeSpec = genElemType;
            }
            else if (original is OptionalModifierType)
            {
                TypeReference mt = (original as OptionalModifierType).ModifierType;
                typeSpec = new OptionalModifierType(elementType, ImportTypeReference(mt, context));
            }
            else if (original is RequiredModifierType)
            {
                TypeReference mt = (original as RequiredModifierType).ModifierType;
                typeSpec = new RequiredModifierType(elementType, ImportTypeReference(mt, context));
			} else if (original is SentinelType) {
				typeSpec = new SentinelType (elementType);
			} else if (original is FunctionPointerType) {
				FunctionPointerType ori = original as FunctionPointerType;

				FunctionPointerType fnptr = new FunctionPointerType ();
                fnptr.HasThis = ori.HasThis;
                fnptr.ExplicitThis = ori.ExplicitThis;
                fnptr.CallingConvention = ori.CallingConvention;
                fnptr.ReturnType = ImportTypeReference (ori.ReturnType, context);

				foreach (ParameterDefinition parameter in ori.Parameters)
					fnptr.Parameters.Add (new ParameterDefinition (ImportTypeReference (parameter.ParameterType, context)));

				typeSpec = fnptr;
			} else
				throw new ReflectionException ("Unknown element type: {0}", original.GetType ().Name);

			return typeSpec;
		}

		static GenericParameter GetGenericParameter (GenericParameter gp, ImportContext context)
		{
			GenericParameter p;
			if (gp.Owner is TypeReference)
				p = context.GenericContext.Type.GenericParameters [gp.Position];
			else if (gp.Owner is MethodReference)
				p = context.GenericContext.Method.GenericParameters [gp.Position];
			else
				throw new NotSupportedException ();

			return p;
		}

		public virtual TypeReference ImportTypeReference (TypeReference t, ImportContext context)
		{
			if (t.Module == m_module)
				return t;

			if (t is TypeSpecification)
				return GetTypeSpec (t as TypeSpecification, context);

			if (t is GenericParameter)
				return GetGenericParameter (t as GenericParameter, context);

			AssemblyNameReference asm;
			if (t.Scope is AssemblyNameReference)
				asm = ImportAssembly ((AssemblyNameReference) t.Scope);
			else if (t.Scope is ModuleDefinition)
				asm = ImportAssembly (((ModuleDefinition) t.Scope).Assembly.Name);
			else
				throw new NotImplementedException ();

            TypeReference type;
            if (t.DeclaringType != null)
            {
				type = new TypeReference (t.Name, string.Empty, asm, t.IsValueType);
				type.DeclaringType = ImportTypeReference (t.DeclaringType, context);
			} else
				type = new TypeReference (t.Name, t.Namespace, asm, t.IsValueType);

			TypeReference contextType = context.GenericContext.Type;

			context.GenericContext.Type = type;

			GenericParameter.CloneInto (t, type, context);

			context.GenericContext.Type = contextType;

			return type;
		}

		public virtual FieldReference ImportFieldReference (FieldReference fr, ImportContext context)
		{
			if (fr.DeclaringType.Module == m_module)
				return fr;

			FieldReference field = new FieldReference (
				fr.Name,
				ImportTypeReference (fr.FieldType, context));
            field.DeclaringType = ImportTypeReference(fr.DeclaringType, context);

			return field;
		}

		MethodReference GetMethodSpec (MethodReference meth, ImportContext context)
		{
			if (!(meth is GenericInstanceMethod))
				return null;

			GenericInstanceMethod gim = meth as GenericInstanceMethod;
			GenericInstanceMethod ngim = new GenericInstanceMethod (
				ImportMethodReference (gim.ElementMethod, context));

			context.GenericContext.CheckProvider (ngim.GetElementMethod(), gim.GenericArguments.Count);
			foreach (TypeReference arg in gim.GenericArguments)
				ngim.GenericArguments.Add (ImportTypeReference (arg, context));

			return ngim;
		}

		public virtual MethodReference ImportMethodReference (MethodReference mr, ImportContext context)
		{
			if (mr.DeclaringType.Module == m_module)
				return mr;

			if (mr is MethodSpecification)
				return GetMethodSpec (mr, context);

			MethodReference meth = new MethodReference ();
            meth.Name = mr.Name;
            meth.HasThis = mr.HasThis;
            meth.ExplicitThis = mr.ExplicitThis;
            meth.CallingConvention = mr.CallingConvention;
			meth.DeclaringType = ImportTypeReference (mr.DeclaringType, context);

			TypeReference contextType = context.GenericContext.Type;
			MethodReference contextMethod = context.GenericContext.Method;

			context.GenericContext.Method = meth;
			context.GenericContext.Type = meth.DeclaringType.GetElementType();

			GenericParameter.CloneInto (mr, meth, context);

			meth.ReturnType = ImportTypeReference (mr.ReturnType, context);

			foreach (ParameterDefinition param in mr.Parameters)
				meth.Parameters.Add (new ParameterDefinition (
					ImportTypeReference (param.ParameterType, context)));

			context.GenericContext.Type = contextType;
			context.GenericContext.Method = contextMethod;

			return meth;
		}

	}
}
