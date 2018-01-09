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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace Reflexil.Compilation
{
	internal abstract class BaseLanguageHelper : IReflectionVisitor, ILanguageHelper
	{
		protected const string BasicSeparator = ", ";
		internal const string GenericTypeTag = "`";
		protected const string LeftParenthesis = "(";
		protected const string RightParenthesis = ")";
		protected const string LeftBrace = "{";
		protected const string RightBrace = "}";
		protected const string LeftChevron = "<";
		protected const string RightChevron = ">";
		protected const string LeftBracket = "[";
		protected const string RightBracket = "]";
		protected const string ReferenceTypeTag = "&";
		protected const string NamespaceSeparator = ".";
		protected const string Space = " ";
		protected const string Quote = "\"";
		protected IList<string> Namespaces = new List<string>();
		protected const string VolatileModifierTypeFullname = "System.Runtime.CompilerServices.IsVolatile";

		private readonly StringBuilder _identedbuilder = new StringBuilder();
		protected Dictionary<string, string> Aliases = new Dictionary<string, string>();

		protected BaseLanguageHelper()
		{
			IdentLevel = 0;
		}

		protected int IdentLevel { get; set; }

		protected abstract void WriteMethodSignature(MethodReference mref);
		protected abstract void WriteMethodBody(MethodDefinition mdef);
		protected abstract void WriteTypeSignature(TypeReference tref);
		protected abstract void WriteField(FieldDefinition fdef);
		protected abstract void WriteComment(string comment);
		protected abstract void WriteFieldsStubs(Mono.Collections.Generic.Collection<FieldDefinition> fields);

		protected abstract void WriteMethodsStubs(MethodDefinition mdef,
			Mono.Collections.Generic.Collection<MethodDefinition> methods);

		protected abstract void WriteNamespaces();
		protected abstract void WriteType(MethodDefinition mdef);
		protected abstract void WriteReferencedAssemblies(List<AssemblyNameReference> references);

		public abstract void VisitTypeDefinition(TypeDefinition type);
		public abstract void VisitFieldDefinition(FieldDefinition field);
		public abstract void VisitMethodDefinition(MethodDefinition method);
		public abstract void VisitMethodReference(MethodReference method);
		public abstract void VisitTypeReference(TypeReference type);
		public abstract void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams);
		public abstract void VisitParameterDefinition(ParameterDefinition parameter);

		public abstract void VisitParameterDefinitionCollection(Mono.Collections.Generic.Collection<ParameterDefinition> parameters);

		public virtual void VisitCustomAttributeCollection(Mono.Collections.Generic.Collection<CustomAttribute> customAttrs)
		{
		}

		public virtual void VisitSecurityDeclaration(SecurityDeclaration secDecl)
		{
		}

		public virtual void VisitCustomAttribute(CustomAttribute customAttr)
		{
		}

		public virtual void VisitSecurityDeclarationCollection(Mono.Collections.Generic.Collection<SecurityDeclaration> secDecls)
		{
		}

		public virtual void VisitOverride(MethodReference ov)
		{
		}

		public virtual void VisitOverrideCollection(Mono.Collections.Generic.Collection<MethodReference> meth)
		{
		}

		public virtual void VisitGenericParameter(GenericParameter genparam)
		{
		}

		public virtual void VisitConstructorCollection(Mono.Collections.Generic.Collection<MethodDefinition> ctors)
		{
		}

		public virtual void VisitEventDefinitionCollection(Mono.Collections.Generic.Collection<EventDefinition> events)
		{
		}

		public virtual void VisitFieldDefinitionCollection(Mono.Collections.Generic.Collection<FieldDefinition> fields)
		{
		}

		public virtual void VisitMethodDefinitionCollection(Mono.Collections.Generic.Collection<MethodDefinition> methods)
		{
		}

		public virtual void VisitNestedTypeCollection(Mono.Collections.Generic.Collection<TypeDefinition> nestedTypes)
		{
		}

		public virtual void VisitPropertyDefinitionCollection(Mono.Collections.Generic.Collection<PropertyDefinition> properties)
		{
		}

		public virtual void VisitTypeDefinitionCollection(Mono.Collections.Generic.Collection<TypeDefinition> types)
		{
		}

		public virtual void VisitEventDefinition(EventDefinition evt)
		{
		}

		public virtual void VisitModuleDefinition(ModuleDefinition module)
		{
		}

		public virtual void VisitNestedType(TypeDefinition nestedType)
		{
		}

		public virtual void VisitPropertyDefinition(PropertyDefinition property)
		{
		}

		public virtual void VisitConstructor(MethodDefinition ctor)
		{
		}

		public virtual void TerminateModuleDefinition(ModuleDefinition module)
		{
		}

		public virtual void VisitExternType(TypeReference externType)
		{
		}

		public virtual void VisitExternTypeCollection(Mono.Collections.Generic.Collection<TypeReference> externs)
		{
		}

		public virtual void VisitInterface(TypeReference interf)
		{
		}

		public virtual void VisitInterfaceCollection(Mono.Collections.Generic.Collection<InterfaceImplementation> interfaces)
		{
		}

		public virtual void VisitMemberReference(MemberReference member)
		{
		}

		public virtual void VisitMemberReferenceCollection(Mono.Collections.Generic.Collection<MemberReference> members)
		{
		}

		public virtual void VisitMarshalSpec(MarshalInfo marshalInfo)
		{
		}

		public virtual void VisitTypeReferenceCollection(Mono.Collections.Generic.Collection<TypeReference> refs)
		{
		}

		public virtual void VisitPInvokeInfo(PInvokeInfo pinvk)
		{
		}

		protected void ReIdent(int newidentlevel)
		{
			UnIdent();
			IdentLevel = newidentlevel;
			Ident();
		}

		protected void Ident()
		{
			for (var i = 0; i < IdentLevel; i++)
				_identedbuilder.Append("\t");
		}

		protected void UnIdent()
		{
			for (var i = 0; i < IdentLevel; i++)
				if ((_identedbuilder.Length > 0) && (_identedbuilder[_identedbuilder.Length - 1] == '\t'))
					_identedbuilder.Remove(_identedbuilder.Length - 1, 1);
		}

		protected void Replace(string oldvalue, string newvalue)
		{
			_identedbuilder.Replace(oldvalue, newvalue);
		}

		protected void WriteLine()
		{
			_identedbuilder.AppendLine();
			Ident();
		}

		protected void WriteLine(string str)
		{
			Write(str);
			WriteLine();
		}

		protected void Write(string str)
		{
			_identedbuilder.Append(str);
		}

		protected void Write(Enum item, SpaceSurrounder mode)
		{
			_identedbuilder.Append(Surround(item, mode));
		}

		protected void Write(Enum item)
		{
			Write(item.ToString());
		}

		protected void WriteLine(Enum item)
		{
			WriteLine(item.ToString());
		}

		protected void Reset()
		{
			IdentLevel = 0;
			_identedbuilder.Length = 0;
		}

		protected string GetResult()
		{
			return _identedbuilder.ToString();
		}

		public virtual string GetMethodSignature(MethodReference mref)
		{
			Reset();
			WriteMethodSignature(mref);
			return GetResult();
		}

		public virtual string GetMethod(MethodDefinition mdef)
		{
			Reset();
			WriteMethodSignature(mdef);
			WriteMethodBody(mdef);
			return GetResult();
		}

		public virtual string GetField(FieldDefinition fdef)
		{
			Reset();
			WriteField(fdef);
			return GetResult();
		}

		public virtual string GetTypeSignature(TypeReference tref)
		{
			Reset();
			WriteTypeSignature(tref);
			return GetResult();
		}

		public abstract string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references);

		protected IEnumerable<T> FilterAutogeneratedMembers<T>(IEnumerable<T> members) where T : MemberReference
		{
			return members.Where(m => !m.Name.Contains("$") && !m.Name.Contains("<") && !m.Name.Contains(">"));
		}

		protected void WriteMethodsStubs(MethodDefinition mdef, Mono.Collections.Generic.Collection<MethodDefinition> methods,
			string rskw, string rekw)
		{
			Write(rskw);
			WriteLine("\" Methods stubs \"");
			WriteComment("Do not add or update any method. If compilation fails because of a method declaration, comment it");
			foreach (var smdef in FilterAutogeneratedMembers(methods.Where(smdef => mdef != smdef)))
			{
				WriteMethodSignature(smdef);
				WriteMethodBody(smdef);
				WriteLine();
			}
			WriteLine(rekw);
		}

		protected void WriteFieldsStubs(Mono.Collections.Generic.Collection<FieldDefinition> fields, string rskw, string rekw)
		{
			Write(rskw);
			WriteLine("\" Fields stubs \"");
			WriteComment("Do not add or update any field. If compilation fails because of a field declaration, comment it");
			foreach (var fdef in FilterAutogeneratedMembers(fields))
			{
				WriteField(fdef);
				WriteLine();
			}
			WriteLine(rekw);
		}

		protected void WriteReferencedAssemblies(List<AssemblyNameReference> references, string rskw, string rekw)
		{
			Write(rskw);
			WriteLine("\" Referenced assemblies \"");

			foreach (var asmref in references)
				WriteComment(String.Format("- {0} v{1}", asmref.Name, asmref.Version));

			WriteLine(rekw);
		}

		protected void WriteType(MethodDefinition mdef, string tkw, string tskw, string tekw)
		{
			Write(tkw);
			WriteTypeSignature(mdef.DeclaringType);
			WriteLine();
			IdentLevel++;
			WriteLine(tskw);

			WriteComment("Limited support!");
			WriteComment("You can only reference methods or fields defined in the class (not in ancestors classes)");
			WriteComment("Fields and methods stubs are needed for compilation purposes only.");
			WriteComment("Reflexil will automaticaly map current type, fields or methods to original references.");
			WriteMethodSignature(mdef);
			WriteMethodBody(mdef);

			WriteLine();
			WriteMethodsStubs(mdef, mdef.DeclaringType.Methods);

			WriteLine();
			WriteFieldsStubs(mdef.DeclaringType.Fields);

			ReIdent(IdentLevel - 1);
			WriteLine();
			WriteLine(tekw);
		}

		protected string HandleAliases(string str)
		{
			return Aliases.Keys.Aggregate(str, (current, alias) => current.Replace(alias, Aliases[alias]));
		}

		protected abstract string HandleKeywords(string str);

		protected void WriteTypeName(TypeReference type, string name)
		{
			var tag = name.LastIndexOf(GenericTypeTag, StringComparison.Ordinal);
			if (tag >= 0)
				name = name.Substring(0, name.IndexOf(GenericTypeTag, StringComparison.Ordinal));

			name = HandleAliases(name);

			Write(name);
		}

		protected virtual void VisitVisitableCollection(string start, string end, string separator, bool always,
			ICollection collection)
		{
			if (always | collection.Count > 0)
				Write(start);

			var firstloop = true;
			foreach (IReflectionVisitable item in collection)
			{
				if (!firstloop)
					Write(separator);
				else
					firstloop = false;

				if (item is TypeDefinition)
					VisitTypeReference(item as TypeDefinition);
				else
					item.Accept(this);
			}

			if (always | collection.Count > 0)
				Write(end);
		}

		protected string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references, string nkw, string nskw, string nekw)
		{
			Reset();

			CollectNamespaces(mdef.DeclaringType);
			WriteNamespaces();
			WriteLine();
			WriteReferencedAssemblies(references);
			WriteLine();

			if (mdef.DeclaringType.Namespace != string.Empty)
			{
				Write(nkw);
				WriteLine(mdef.DeclaringType.Namespace);
				IdentLevel++;
				WriteLine(nskw);
			}

			WriteType(mdef);

			if (string.IsNullOrEmpty(mdef.DeclaringType.Namespace))
				return GetResult();

			ReIdent(IdentLevel - 1);
			WriteLine(nekw);

			return GetResult();
		}

		protected void CollectNamespaces(TypeDefinition type)
		{
			var nc = new NamespaceCollector(type);
			Namespaces = nc.Collect();
		}

		protected string Surround(Enum item, SpaceSurrounder mode)
		{
			var result = item.ToString();

			if (mode != SpaceSurrounder.After)
				result = Space + result;

			if (mode != SpaceSurrounder.Before)
				result = result + Space;

			return result;
		}

		protected bool IsUnsafe(TypeReference source)
		{
			if (source is PointerType)
				return true;

			var typeSpecification = source as TypeSpecification;
			if (typeSpecification != null)
				return IsUnsafe(typeSpecification.ElementType);

			return false;
		}

		protected bool IsUnsafe(MethodReference source)
		{
			return IsUnsafe(source.ReturnType) || source.Parameters.Any(param => IsUnsafe(param.ParameterType));
		}
	}
}