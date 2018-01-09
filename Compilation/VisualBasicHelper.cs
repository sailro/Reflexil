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
using System.Collections.Generic;
using Mono.Cecil;

namespace Reflexil.Compilation
{
	internal class VisualBasicHelper : BaseLanguageHelper
	{
		protected const string NewLine = " _";
		protected const string Comment = "' ";
		protected const string RegionStart = "#Region ";
		protected const string RegionEnd = "#End Region ";

		private readonly Stack<bool> _displayConstraintsStack = new Stack<bool>();

		public VisualBasicHelper()
		{
			Aliases.Add("System.Object", VisualBasicKeywords.Object.ToString());
			Aliases.Add("System.Int16", VisualBasicKeywords.Short.ToString());
			Aliases.Add("System.Int32", VisualBasicKeywords.Integer.ToString());
			Aliases.Add("System.Int64", VisualBasicKeywords.Long.ToString());
			Aliases.Add("System.UInt16", VisualBasicKeywords.UShort.ToString());
			Aliases.Add("System.UInt32", VisualBasicKeywords.UInteger.ToString());
			Aliases.Add("System.UInt64", VisualBasicKeywords.ULong.ToString());
			Aliases.Add("System.Boolean", VisualBasicKeywords.Boolean.ToString());
			Aliases.Add("System.Char", VisualBasicKeywords.Char.ToString());
			Aliases.Add("System.Decimal", VisualBasicKeywords.Decimal.ToString());
			Aliases.Add("System.Double", VisualBasicKeywords.Double.ToString());
			Aliases.Add("System.Single", VisualBasicKeywords.Single.ToString());
			Aliases.Add("System.String", VisualBasicKeywords.String.ToString());
			Aliases.Add("[]", "()");

			_displayConstraintsStack.Push(false);
		}

		private void HandleSubFunction(TypeReference tref)
		{
			Write(tref.FullName == typeof(void).FullName ? VisualBasicKeywords.Sub : VisualBasicKeywords.Function);
			Write(Space);
		}

		public override string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references)
		{
			var startNs = Surround(VisualBasicKeywords.Namespace, SpaceSurrounder.After);
			var endNs = Surround(VisualBasicKeywords.End, SpaceSurrounder.After) + startNs;
			return GenerateSourceCode(mdef, references, startNs, string.Empty, endNs);
		}

		protected override string HandleKeywords(string str)
		{
			foreach (var keyword in Enum.GetNames(typeof(VisualBasicKeywords)))
			{
				if (str.ToLower() == keyword.ToLower())
					str = LeftBracket + str + RightBracket;
			}
			return str;
		}

		protected override void WriteMethodSignature(MethodReference mref)
		{
			if (IsUnsafe(mref))
			{
				WriteComment("This method is 'unsafe' and cannot be used in VB.NET");
				Write(Comment);
			}
			mref.Accept(this);

			if (mref.ReturnType.FullName == typeof(void).FullName)
				return;

			Write(VisualBasicKeywords.As, SpaceSurrounder.Both);
			VisitTypeReference(mref.ReturnType);
		}

		protected override void WriteMethodBody(MethodDefinition mdef)
		{
			var isunsafe = IsUnsafe(mdef);

			IdentLevel++;
			WriteLine();
			if (mdef.ReturnType.FullName != typeof(void).FullName)
			{
				if (isunsafe)
					Write(Comment);

				Write(VisualBasicKeywords.Return, SpaceSurrounder.After);
				Write(VisualBasicKeywords.Nothing);
				WriteLine();
			}

			UnIdent();
			IdentLevel--;
			Ident();

			if (isunsafe)
				Write(Comment);

			Write(VisualBasicKeywords.End, SpaceSurrounder.After);
			HandleSubFunction(mdef.ReturnType);
			WriteLine();
		}

		protected override void WriteField(FieldDefinition fdef)
		{
			fdef.Accept(this);
		}

		protected override void WriteTypeSignature(TypeReference tref)
		{
			tref.Accept(this);
		}

		protected override void WriteComment(string comment)
		{
			Write(Comment);
			WriteLine(comment);
		}

		protected override void WriteFieldsStubs(Mono.Collections.Generic.Collection<FieldDefinition> fields)
		{
			WriteFieldsStubs(fields, RegionStart, RegionEnd);
		}

		protected override void WriteMethodsStubs(MethodDefinition mdef, Mono.Collections.Generic.Collection<MethodDefinition> methods)
		{
			WriteMethodsStubs(mdef, methods, RegionStart, RegionEnd);
		}

		protected override void WriteNamespaces()
		{
			Write(VisualBasicKeywords.Option, SpaceSurrounder.After);
			Write(VisualBasicKeywords.Explicit, SpaceSurrounder.After);
			WriteLine(VisualBasicKeywords.On);
			Write(VisualBasicKeywords.Option, SpaceSurrounder.After);
			Write(VisualBasicKeywords.Strict, SpaceSurrounder.After);
			WriteLine(VisualBasicKeywords.On);
			WriteLine();
			Write(RegionStart);
			WriteLine("\" Imports \"");
			foreach (var item in Namespaces)
			{
				Write(VisualBasicKeywords.Imports, SpaceSurrounder.After);
				WriteLine(item);
			}
			WriteLine(RegionEnd);
		}

		protected override void WriteType(MethodDefinition mdef)
		{
			var startClass = Surround(VisualBasicKeywords.Class, SpaceSurrounder.After);
			var endClass = Surround(VisualBasicKeywords.End, SpaceSurrounder.After) + startClass;
			WriteType(mdef, startClass, string.Empty, endClass);
		}

		protected override void WriteReferencedAssemblies(List<AssemblyNameReference> references)
		{
			WriteReferencedAssemblies(references, RegionStart, RegionEnd);
		}

		public override void VisitFieldDefinition(FieldDefinition field)
		{
			var mtype = field.FieldType as IModifierType;
			var typeReference = mtype == null ? field.FieldType : mtype.ElementType;

			if (IsUnsafe(typeReference))
			{
				WriteComment(@"Warning unsafe modifier is not supported with VB.NET");
				Write(Comment);
			}
			else if (mtype != null)
			{
				if (mtype.ModifierType.FullName == VolatileModifierTypeFullname)
				{
					WriteComment(@"Warning volatile modifier is not supported with VB.NET");
					Write(Comment);
				}
			}

			Write(VisualBasicKeywords.Dim, SpaceSurrounder.After);

			if (field.IsStatic)
				Write(VisualBasicKeywords.Shared, SpaceSurrounder.After);

			Write(HandleKeywords(field.Name));
			Write(VisualBasicKeywords.As, SpaceSurrounder.Both);
			VisitTypeReference(typeReference);
		}

		public override void VisitMethodDefinition(MethodDefinition method)
		{
			if (method.IsStatic)
				Write(VisualBasicKeywords.Shared, SpaceSurrounder.After);

			HandleSubFunction(method.ReturnType);

			if (method.IsConstructor)
				Write(VisualBasicKeywords.New);
			else
				Write(HandleKeywords(method.Name));
		}

		public override void VisitMethodReference(MethodReference method)
		{
			// TODO
		}

		public override void VisitTypeDefinition(TypeDefinition type)
		{
			WriteTypeName(type, type.Name);
		}

		public override void VisitTypeReference(TypeReference type)
		{
			if (type.IsNested)
			{
				VisitTypeReference(type.DeclaringType);
				Write(NamespaceSeparator);
			}

			string name = type.Name;

			if (type.Name.EndsWith(ReferenceTypeTag))
				name = name.Replace(ReferenceTypeTag, string.Empty);

			if (type.Namespace != string.Empty)
				name = type.Namespace + NamespaceSeparator + name;

			WriteTypeName(type, name);
			var git = type as GenericInstanceType;
			if (git != null)
			{
				WriteTypeName(git, name);
				_displayConstraintsStack.Push(false);
				VisitVisitableCollection(LeftParenthesis + Surround(VisualBasicKeywords.Of, SpaceSurrounder.After), RightParenthesis, BasicSeparator, false, git.GenericArguments);
				_displayConstraintsStack.Pop();
			}

			if (_displayConstraintsStack.Peek() && type is GenericParameter)
			{
				VisitVisitableCollection(Surround(VisualBasicKeywords.As, SpaceSurrounder.Both) + LeftBrace, RightBrace, BasicSeparator, false, ((GenericParameter) type).Constraints);
			}
		}

		public override void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams)
		{
			_displayConstraintsStack.Push(true);
			VisitVisitableCollection(LeftParenthesis + Surround(VisualBasicKeywords.Of, SpaceSurrounder.After), RightParenthesis,
				BasicSeparator, false, genparams);
			_displayConstraintsStack.Pop();
		}

		public override void VisitParameterDefinitionCollection(Mono.Collections.Generic.Collection<ParameterDefinition> parameters)
		{
			VisitVisitableCollection(LeftParenthesis, RightParenthesis, BasicSeparator, true, parameters);
		}

		public override void VisitParameterDefinition(ParameterDefinition parameter)
		{
			Write(parameter.ParameterType.Name.EndsWith(ReferenceTypeTag)
				? VisualBasicKeywords.ByRef
				: VisualBasicKeywords.ByVal);
			Write(Space);
			Write(HandleKeywords(parameter.Name));
			Write(VisualBasicKeywords.As, SpaceSurrounder.Both);
			VisitTypeReference(parameter.ParameterType);
		}
	}
}