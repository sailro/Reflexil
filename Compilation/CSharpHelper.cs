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

#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

#endregion

namespace Reflexil.Compilation
{
	/// <summary>
	/// C# code generator
	/// </summary>
	internal class CSharpHelper : BaseLanguageHelper
	{
		#region Constants

		protected const string GenericConstraintListStart = " : ";
		protected const string RegionStart = "#region ";
		protected const string RegionEnd = "#endregion ";
		protected const string Separator = ";";
		protected const string Comment = "// ";
		protected const string At = "@";

		#endregion

		#region Methods

		/// <summary>
		/// Constructor. Aliases initialisation.
		/// </summary>
		public CSharpHelper()
		{
			Aliases.Add("System.Object", CSharpKeyword.@object.ToString());
			Aliases.Add("System.Int16", CSharpKeyword.@short.ToString());
			Aliases.Add("System.Int32", CSharpKeyword.@int.ToString());
			Aliases.Add("System.Int64", CSharpKeyword.@long.ToString());
			Aliases.Add("System.UInt16", CSharpKeyword.@ushort.ToString());
			Aliases.Add("System.UInt32", CSharpKeyword.@uint.ToString());
			Aliases.Add("System.UInt64", CSharpKeyword.@ulong.ToString());
			Aliases.Add("System.Boolean", CSharpKeyword.@bool.ToString());
			Aliases.Add("System.Char", CSharpKeyword.@char.ToString());
			Aliases.Add("System.Decimal", CSharpKeyword.@decimal.ToString());
			Aliases.Add("System.Double", CSharpKeyword.@double.ToString());
			Aliases.Add("System.Single", CSharpKeyword.@float.ToString());
			Aliases.Add("System.String", CSharpKeyword.@string.ToString());
			Aliases.Add("System.Void", CSharpKeyword.@void.ToString());
		}

		#region BaseLanguageHelper

		/// <summary>
		/// Generate source code from method declaring type. All others
		/// methods are generated as stubs.
		/// </summary>
		/// <param name="mdef">Method definition</param>
		/// <param name="references">Assembly references</param>
		/// <returns>generated source code</returns>
		public override string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references)
		{
			return GenerateSourceCode(mdef, references, Surround(CSharpKeyword.@namespace, SpaceSurrounder.After), LeftBrace,
				RightBrace);
		}

		/// <summary>
		/// Replace all keyword in a string
		/// </summary>
		/// <param name="str">Input string</param>
		/// <returns>Result string</returns>
		protected override string HandleKeywords(string str)
		{
			foreach (var keyword in Enum.GetNames(typeof (CSharpKeyword)))
			{
				if (str == keyword)
					str = At + str;
			}
			return str;
		}

		#endregion

		#region Writers

		/// <summary>
		/// Write a method signature to the text buffer
		/// </summary>
		/// <param name="mref">Method reference</param>
		protected override void WriteMethodSignature(MethodReference mref)
		{
			mref.Accept(this);

			if (mref.GenericParameters.Count <= 0)
				return;

			foreach (var genparam in mref.GenericParameters.Where(genparam => genparam.Constraints.Count > 0))
			{
				Write(CSharpKeyword.@where, SpaceSurrounder.Both);
				genparam.Accept(this);
				VisitVisitableCollection(GenericConstraintListStart, String.Empty, BasicSeparator, false, genparam.Constraints);
			}
		}

		/// <summary>
		/// Write a method body to the text buffer
		/// </summary>
		/// <param name="mdef">Method definition</param>
		protected override void WriteMethodBody(MethodDefinition mdef)
		{
			WriteLine();
			IdentLevel++;
			WriteLine(LeftBrace);

			if (mdef.ReturnType.FullName != typeof (void).FullName)
			{
				Write(CSharpKeyword.@return, SpaceSurrounder.After);
				Write(CSharpKeyword.@default);
				Write(LeftParenthesis);
				VisitTypeReference(mdef.ReturnType);
				Write(RightParenthesis);
				WriteLine(Separator);
			}

			UnIdent();
			IdentLevel--;
			Ident();
			WriteLine(RightBrace);
		}

		/// <summary>
		/// Write a field to the text buffer
		/// </summary>
		/// <param name="fdef">Field definition</param>
		protected override void WriteField(FieldDefinition fdef)
		{
			fdef.Accept(this);
		}

		/// <summary>
		/// Write a type signature to the text buffer
		/// </summary>
		/// <param name="tref">Type definition</param>
		protected override void WriteTypeSignature(TypeReference tref)
		{
			tref.Accept(this);

			if (tref.GenericParameters.Count <= 0)
				return;

			foreach (var genparam in tref.GenericParameters.Where(genparam => genparam.Constraints.Count > 0))
			{
				Write(CSharpKeyword.@where, SpaceSurrounder.Both);
				genparam.Accept(this);
				VisitVisitableCollection(GenericConstraintListStart, String.Empty, BasicSeparator, false, genparam.Constraints);
			}
		}

		/// <summary>
		/// Write fields stubs to the text buffer
		/// </summary>
		/// <param name="fields">Fields stubs</param>
		protected override void WriteFieldsStubs(Mono.Collections.Generic.Collection<FieldDefinition> fields)
		{
			WriteFieldsStubs(fields, RegionStart, RegionEnd);
		}

		/// <summary>
		/// Write methods stubs to the text buffer
		/// </summary>
		/// <param name="mdef">Method definition to exclude</param>
		/// <param name="methods">Methods definitions</param>
		protected override void WriteMethodsStubs(MethodDefinition mdef,
			Mono.Collections.Generic.Collection<MethodDefinition> methods)
		{
			WriteMethodsStubs(mdef, methods, RegionStart, RegionEnd);
		}

		/// <summary>
		/// Write default namespaces to the text buffer
		/// </summary>
		protected override void WriteDefaultNamespaces()
		{
			Write(RegionStart);
			WriteLine("\" Imports \"");
			foreach (var item in DefaultNamespaces)
			{
				Write(CSharpKeyword.@using, SpaceSurrounder.After);
				Write(item);
				WriteLine(Separator);
			}
			WriteLine(RegionEnd);
		}

		/// <summary>
		/// Write a comment to the text buffer
		/// </summary>
		/// <param name="comment">Comment</param>
		protected override void WriteComment(string comment)
		{
			Write(Comment);
			WriteLine(comment);
		}

		/// <summary>
		/// Write a method's owner type to the text buffer
		/// </summary>
		/// <param name="mdef">Method definition</param>
		protected override void WriteType(MethodDefinition mdef)
		{
			WriteType(mdef, Surround(CSharpKeyword.@class, SpaceSurrounder.After), LeftBrace, RightBrace);
		}

		/// <summary>
		/// Write referenced assemblies to the text buffer (as a comment)
		/// </summary>
		/// <param name="references">Assembly references</param>
		protected override void WriteReferencedAssemblies(List<AssemblyNameReference> references)
		{
			WriteReferencedAssemblies(references, RegionStart, RegionEnd);
		}

		#endregion

		#region IReflectionVisitor

		/// <summary>
		/// Visit a field definition
		/// </summary>
		/// <param name="field">Field definition</param>
		public override void VisitFieldDefinition(FieldDefinition field)
		{
			if (field.IsStatic)
				Write(CSharpKeyword.@static, SpaceSurrounder.After);

			var mtype = field.FieldType as IModifierType;
			var typeReference = mtype == null ? field.FieldType : mtype.ElementType;

			if (IsUnsafe(typeReference))
				Write(CSharpKeyword.@unsafe, SpaceSurrounder.After);

			if (mtype != null)
			{
				if (mtype.ModifierType.FullName == VolatileModifierTypeFullname)
					Write(CSharpKeyword.@volatile, SpaceSurrounder.After);
			}

			VisitTypeReference(typeReference);
			Write(Space);
			Write(HandleKeywords(field.Name));
			Write(Separator);
		}

		/// <summary>
		/// Visit a method definition
		/// </summary>
		/// <param name="method">Method definition</param>
		public override void VisitMethodDefinition(MethodDefinition method)
		{
			if (IsUnsafe(method))
				Write(CSharpKeyword.@unsafe, SpaceSurrounder.After);

			if (method.IsStatic)
				Write(CSharpKeyword.@static, SpaceSurrounder.After);

			VisitMethodReference(method);
		}

		/// <summary>
		/// Visit a method reference
		/// </summary>
		/// <param name="method">Method reference</param>
		public override void VisitMethodReference(MethodReference method)
		{
			if ((method.Name == ".cctor" || method.Name == ".ctor"))
			{
				WriteTypeName(method.DeclaringType, method.DeclaringType.Name);
			}
			else
			{
				VisitTypeReference(method.ReturnType);
				Write(Space);
				Write(HandleKeywords(method.Name));
			}

			var gim = method as GenericInstanceMethod;
			if (gim == null)
				return;

			VisitVisitableCollection(LeftChevron, RightChevron, BasicSeparator, false, gim.GenericArguments);
		}

		/// <summary>
		/// Visit a type definition
		/// </summary>
		/// <param name="type">Type definition</param>
		public override void VisitTypeDefinition(TypeDefinition type)
		{
			WriteTypeName(type, type.Name);
		}

		/// <summary>
		/// Visit a type reference
		/// </summary>
		/// <param name="type">Type reference</param>
		public override void VisitTypeReference(TypeReference type)
		{
			var name = type.Name;

			if (type.Name.EndsWith(ReferenceTypeTag))
			{
				Write(CSharpKeyword.@ref, SpaceSurrounder.After);
				name = name.Replace(ReferenceTypeTag, String.Empty);
			}

			if (type.Namespace != String.Empty)
				name = type.Namespace + NamespaceSeparator + name;

			WriteTypeName(type, name);

			var git = type as GenericInstanceType;
			if (git != null)
				VisitVisitableCollection(LeftChevron, RightChevron, BasicSeparator, false, git.GenericArguments);
	
		}

		/// <summary>
		/// Visit a generic parameter collection
		/// </summary>
		/// <param name="genparams">Generic parameter collection</param>
		public override void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams)
		{
			VisitVisitableCollection(LeftChevron, RightChevron, BasicSeparator, false, genparams);
		}

		/// <summary>
		/// Visit a parameter definition collection
		/// </summary>
		/// <param name="parameters"></param>
		public override void VisitParameterDefinitionCollection(
			Mono.Collections.Generic.Collection<ParameterDefinition> parameters)
		{
			VisitVisitableCollection(LeftParenthesis, RightParenthesis, BasicSeparator, true, parameters);
		}

		/// <summary>
		/// Visit a parameter definition
		/// </summary>
		/// <param name="parameter">Parameter definition</param>
		public override void VisitParameterDefinition(ParameterDefinition parameter)
		{
			VisitTypeReference(parameter.ParameterType);
			Write(Space);
			Write(HandleKeywords(parameter.Name));
		}

		#endregion

		#endregion
	}
}