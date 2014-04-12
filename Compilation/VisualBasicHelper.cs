/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
    /// <summary>
    /// VisualBasic code generator
    /// </summary>
    internal class VisualBasicHelper : BaseLanguageHelper
    {

        #region Constants
        protected const string NewLine = " _";
        protected const string Comment = "' ";
        protected const string RegionStart = "#Region ";
        protected const string RegionEnd = "#End Region ";
        #endregion

        #region Fields
        private readonly Stack<bool> _displayConstraintsStack = new Stack<bool>();
        #endregion

        #region Methods
        /// <summary>
        /// Constructor. Aliases initialisation.
        /// </summary>
        public VisualBasicHelper()
        {
            Aliases.Add("System.Object", EVisualBasicKeyword.Object.ToString());
            Aliases.Add("System.Int16", EVisualBasicKeyword.Short.ToString());
            Aliases.Add("System.Int32", EVisualBasicKeyword.Integer.ToString());
            Aliases.Add("System.Int64", EVisualBasicKeyword.Long.ToString());
            Aliases.Add("System.UInt16", EVisualBasicKeyword.UShort.ToString());
            Aliases.Add("System.UInt32", EVisualBasicKeyword.UInteger.ToString());
            Aliases.Add("System.UInt64", EVisualBasicKeyword.ULong.ToString());
            Aliases.Add("System.Boolean", EVisualBasicKeyword.Boolean.ToString());
            Aliases.Add("System.Char", EVisualBasicKeyword.Char.ToString());
            Aliases.Add("System.Decimal", EVisualBasicKeyword.Decimal.ToString());
            Aliases.Add("System.Double", EVisualBasicKeyword.Double.ToString());
            Aliases.Add("System.Single", EVisualBasicKeyword.Single.ToString());
            Aliases.Add("System.String", EVisualBasicKeyword.String.ToString());
            Aliases.Add("[]", "()");

            _displayConstraintsStack.Push(false);
        }

        /// <summary>
        /// Write the correct method prefix using a method result type
        /// </summary>
        /// <param name="tref">Method result type reference</param>
        private void HandleSubFunction(TypeReference tref)
        {
	        Write(tref.FullName == typeof (void).FullName ? EVisualBasicKeyword.Sub : EVisualBasicKeyword.Function);
	        Write(Space);
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
            var startNs = Surround(EVisualBasicKeyword.Namespace, ESpaceSurrounder.After);
            var endNs = Surround(EVisualBasicKeyword.End, ESpaceSurrounder.After) + startNs;
            return GenerateSourceCode(mdef, references, startNs, String.Empty, endNs);
        }

        /// <summary>
        /// Replace all keyword in a string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result string</returns>
        protected override string HandleKeywords(string str)
        {
            foreach (var keyword in Enum.GetNames(typeof(EVisualBasicKeyword)))
            {
                if (str.ToLower() == keyword.ToLower())
                    str = LeftBracket + str + RightBracket;
            }
            return str;
        }
        #endregion
        
        #region Writers
        /// <summary>
        /// Write a method signature to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteMethodSignature(MethodDefinition mdef)
        {
            if (IsUnsafe(mdef))
            {
                WriteComment("This method is 'unsafe' and cannot be used in VB.NET");
                Write(Comment);
            }
            mdef.Accept(this);
	        
			if (mdef.ReturnType.FullName == typeof (void).FullName)
				return;
	        
			Write(EVisualBasicKeyword.As, ESpaceSurrounder.Both);
	        VisitTypeReference(mdef.ReturnType);
        }

        /// <summary>
        /// Write a method body to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteMethodBody(MethodDefinition mdef)
        {
            var isunsafe = IsUnsafe(mdef);

            IdentLevel++;
            WriteLine();
            if (mdef.ReturnType.FullName != typeof(void).FullName)
            {
                if (isunsafe)
                    Write(Comment);

				Write(EVisualBasicKeyword.Return, ESpaceSurrounder.After);
                Write(EVisualBasicKeyword.Nothing);
                WriteLine();
            }
            
			UnIdent();
            IdentLevel--;
            Ident();
            
			if (isunsafe)
                Write(Comment);

			Write(EVisualBasicKeyword.End, ESpaceSurrounder.After);
            HandleSubFunction(mdef.ReturnType);
            WriteLine();
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
        /// <param name="tdef">Type definition</param>
        protected override void WriteTypeSignature(TypeDefinition tdef)
        {
            tdef.Accept(this);


            if (tdef.GenericParameters.Count > 0)
            {
                Replace(GenericTypeTag + tdef.GenericParameters.Count, String.Empty);
            }
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
        protected override void WriteMethodsStubs(MethodDefinition mdef, Mono.Collections.Generic.Collection<MethodDefinition> methods)
        {
            WriteMethodsStubs(mdef, methods, RegionStart, RegionEnd);
        }

        /// <summary>
        /// Write default namespaces to the text buffer
        /// </summary>
        protected override void WriteDefaultNamespaces()
        {
            Write(EVisualBasicKeyword.Option, ESpaceSurrounder.After);
            Write(EVisualBasicKeyword.Explicit, ESpaceSurrounder.After);
            WriteLine(EVisualBasicKeyword.On);
            Write(EVisualBasicKeyword.Option, ESpaceSurrounder.After);
            Write(EVisualBasicKeyword.Strict, ESpaceSurrounder.After);
            WriteLine(EVisualBasicKeyword.On);
            WriteLine();
            Write(RegionStart);
            WriteLine("\" Imports \"");
            foreach (var item in DefaultNamespaces)
            {
                Write(EVisualBasicKeyword.Imports, ESpaceSurrounder.After);
                WriteLine(item);
            }
            WriteLine(RegionEnd);
        }

        /// <summary>
        /// Write a method's owner type to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteType(MethodDefinition mdef)
        {
            var startClass = Surround(EVisualBasicKeyword.Class, ESpaceSurrounder.After);
            var endClass = Surround(EVisualBasicKeyword.End, ESpaceSurrounder.After) + startClass;
            WriteType(mdef, startClass, String.Empty, endClass);
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

            Write(EVisualBasicKeyword.Dim, ESpaceSurrounder.After);

			if (field.IsStatic)
                Write(EVisualBasicKeyword.Shared, ESpaceSurrounder.After);
            
			Write(HandleKeywords(field.Name));
            Write(EVisualBasicKeyword.As, ESpaceSurrounder.Both);
	        VisitTypeReference(typeReference);
        }

        /// <summary>
        /// Visit a method definition
        /// </summary>
        /// <param name="method">Method definition</param>
        public override void VisitMethodDefinition(MethodDefinition method)
        {
            if (method.IsStatic)
                Write(EVisualBasicKeyword.Shared, ESpaceSurrounder.After);

			HandleSubFunction(method.ReturnType);
            if (method.IsConstructor)
            {
                Write(EVisualBasicKeyword.New);
            }
            else
            {
                Write(HandleKeywords(method.Name));
            }
        }

        /// <summary>
        /// Visit a type definition
        /// </summary>
        /// <param name="type">Type definition</param>
        public override void VisitTypeDefinition(TypeDefinition type)
        {
            HandleTypeName(type, type.Name);
        }

        /// <summary>
        /// Visit a type reference
        /// </summary>
        /// <param name="type">Type reference</param>
        public override void VisitTypeReference(TypeReference type)
        {
            string name = type.Name;

            if (type.Name.EndsWith(ReferenceTypeTag))
                name = name.Replace(ReferenceTypeTag, String.Empty);

			if (type.Namespace != String.Empty)
                name = type.Namespace + NamespaceSeparator + name;

			if (type is GenericInstanceType)
            {
                var git = type as GenericInstanceType;
                name = name.Replace(GenericTypeTag + git.GenericArguments.Count, String.Empty);
                HandleTypeName(type, name);
                _displayConstraintsStack.Push(false);
                VisitVisitableCollection(LeftParenthesis + Surround(EVisualBasicKeyword.Of, ESpaceSurrounder.After), RightParenthesis, BasicSeparator, false, git.GenericArguments);
                _displayConstraintsStack.Pop();
            }
            else
            {
                HandleTypeName(type, name);
            }
            if (_displayConstraintsStack.Peek() && (type is GenericParameter))
            {
                VisitVisitableCollection(Surround(EVisualBasicKeyword.As, ESpaceSurrounder.Both) + LeftBrace, RightBrace, BasicSeparator, false, (type as GenericParameter).Constraints);
            }
        }

        /// <summary>
        /// Visit a generic parameter collection
        /// </summary>
        /// <param name="genparams">Generic parameter collection</param>
        public override void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams)
        {
            _displayConstraintsStack.Push(true);
            VisitVisitableCollection(LeftParenthesis + Surround(EVisualBasicKeyword.Of, ESpaceSurrounder.After), RightParenthesis, BasicSeparator, false, genparams);
            _displayConstraintsStack.Pop();
        }

        /// <summary>
        /// Visit a parameter definition collection
        /// </summary>
        /// <param name="parameters"></param>
        public override void VisitParameterDefinitionCollection(Mono.Collections.Generic.Collection<ParameterDefinition> parameters)
        {
            VisitVisitableCollection(LeftParenthesis, RightParenthesis, BasicSeparator, true, parameters);
        }

        /// <summary>
        /// Visit a parameter definition
        /// </summary>
        /// <param name="parameter">Parameter definition</param>
        public override void VisitParameterDefinition(ParameterDefinition parameter)
        {
	        Write(parameter.ParameterType.Name.EndsWith(ReferenceTypeTag)
		        ? EVisualBasicKeyword.ByRef
		        : EVisualBasicKeyword.ByVal);
	        Write(Space);
            Write(HandleKeywords(parameter.Name));
            Write(EVisualBasicKeyword.As, ESpaceSurrounder.Both);
            VisitTypeReference(parameter.ParameterType);
        }
        #endregion

        #endregion

    }
}
