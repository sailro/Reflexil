/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
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

        #region " Constants "
        protected const string NEW_LINE = " _";
        protected const string COMMENT = "' ";
        protected const string REGION_START = "#Region ";
        protected const string REGION_END = "#End Region ";
        #endregion

        #region " Fields "
        private Stack<bool> m_displayconstraintsstack = new Stack<bool>();
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor. Aliases initialisation.
        /// </summary>
        public VisualBasicHelper()
        {
            m_aliases.Add("System.Object", EVisualBasicKeyword.Object.ToString());
            m_aliases.Add("System.Int16", EVisualBasicKeyword.Short.ToString());
            m_aliases.Add("System.Int32", EVisualBasicKeyword.Integer.ToString());
            m_aliases.Add("System.Int64", EVisualBasicKeyword.Long.ToString());
            m_aliases.Add("System.UInt16", EVisualBasicKeyword.UShort.ToString());
            m_aliases.Add("System.UInt32", EVisualBasicKeyword.UInteger.ToString());
            m_aliases.Add("System.UInt64", EVisualBasicKeyword.ULong.ToString());
            m_aliases.Add("System.Boolean", EVisualBasicKeyword.Boolean.ToString());
            m_aliases.Add("System.Char", EVisualBasicKeyword.Char.ToString());
            m_aliases.Add("System.Decimal", EVisualBasicKeyword.Decimal.ToString());
            m_aliases.Add("System.Double", EVisualBasicKeyword.Double.ToString());
            m_aliases.Add("System.Single", EVisualBasicKeyword.Single.ToString());
            m_aliases.Add("System.String", EVisualBasicKeyword.String.ToString());
            m_aliases.Add("[]", "()");

            m_displayconstraintsstack.Push(false);
        }

        /// <summary>
        /// Write the correct method prefix using a method result type
        /// </summary>
        /// <param name="tref">Method result type reference</param>
        private void HandleSubFunction(TypeReference tref)
        {
            if (tref.FullName == typeof(void).FullName)
            {
                Write(EVisualBasicKeyword.Sub);
            }
            else
            {
                Write(EVisualBasicKeyword.Function);
            }
            Write(SPACE);
        }

        #region " BaseLanguageHelper "
        /// <summary>
        /// Generate source code from method declaring type. All others
        /// methods are generated as stubs.
        /// </summary>
        /// <param name="mdef">Method definition</param>
        /// <param name="references">Assembly references</param>
        /// <returns>generated source code</returns>
        public override string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references)
        {
            string startNs = Surround(EVisualBasicKeyword.Namespace, ESpaceSurrounder.After);
            string endNs = Surround(EVisualBasicKeyword.End, ESpaceSurrounder.After) + startNs;
            return GenerateSourceCode(mdef, references, startNs, String.Empty, endNs);
        }

        /// <summary>
        /// Replace all keyword in a string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result string</returns>
        protected override string HandleKeywords(string str)
        {
            foreach (string keyword in System.Enum.GetNames(typeof(EVisualBasicKeyword)))
            {
                if (str.ToLower() == keyword.ToLower())
                {
                    str = LEFT_BRACKET + str + RIGHT_BRACKET;
                }
            }
            return str;
        }
        #endregion
        
        #region " Writers "
        /// <summary>
        /// Write a method signature to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteMethodSignature(MethodDefinition mdef)
        {
            if (IsUnsafe(mdef))
            {
                WriteComment("This method is 'unsafe' and cannot be used in VB.NET");
                Write(COMMENT);
            }
            mdef.Accept(this);
            if (mdef.ReturnType.FullName != typeof(void).FullName)
            {
                Write(EVisualBasicKeyword.As, ESpaceSurrounder.Both);
                VisitTypeReference(mdef.ReturnType);
            }
        }

        /// <summary>
        /// Write a method body to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteMethodBody(MethodDefinition mdef)
        {
            bool isunsafe = IsUnsafe(mdef);

            IdentLevel++;
            WriteLine();
            if (mdef.ReturnType.FullName != typeof(void).FullName)
            {
                if (isunsafe)
                {
                    Write(COMMENT);
                }
                Write(EVisualBasicKeyword.Return, ESpaceSurrounder.After);
                Write(EVisualBasicKeyword.Nothing);
                WriteLine();
            }
            UnIdent();
            IdentLevel--;
            Ident();
            if (isunsafe)
            {
                Write(COMMENT);
            }
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
                Replace(GENERIC_TYPE_TAG + tdef.GenericParameters.Count, String.Empty);
            }
        }

        /// <summary>
        /// Write a comment to the text buffer
        /// </summary>
        /// <param name="comment">Comment</param>
        protected override void WriteComment(string comment)
        {
            Write(COMMENT);
            WriteLine(comment);
        }

        /// <summary>
        /// Write fields stubs to the text buffer
        /// </summary>
        /// <param name="fields">Fields stubs</param>
        protected override void WriteFieldsStubs(Mono.Collections.Generic.Collection<FieldDefinition> fields)
        {
            WriteFieldsStubs(fields, REGION_START, REGION_END);
        }

        /// <summary>
        /// Write methods stubs to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition to exclude</param>
        /// <param name="methods">Methods definitions</param>
        protected override void WriteMethodsStubs(MethodDefinition mdef, Mono.Collections.Generic.Collection<MethodDefinition> methods)
        {
            WriteMethodsStubs(mdef, methods, REGION_START, REGION_END);
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
            Write(REGION_START);
            WriteLine("\" Imports \"");
            foreach (string item in DEFAULT_NAMESPACES)
            {
                Write(EVisualBasicKeyword.Imports, ESpaceSurrounder.After);
                WriteLine(item);
            }
            WriteLine(REGION_END);
        }

        /// <summary>
        /// Write a method's owner type to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteType(MethodDefinition mdef)
        {
            string startClass = Surround(EVisualBasicKeyword.Class, ESpaceSurrounder.After);
            string endClass = Surround(EVisualBasicKeyword.End, ESpaceSurrounder.After) + startClass;
            WriteType(mdef, startClass, String.Empty, endClass);
        }

        /// <summary>
        /// Write referenced assemblies to the text buffer (as a comment)
        /// </summary>
        /// <param name="references">Assembly references</param>
        protected override void WriteReferencedAssemblies(List<AssemblyNameReference> references)
        {
            WriteReferencedAssemblies(references, REGION_START, REGION_END);
        }
        #endregion

        #region " IReflectionVisitor "
        /// <summary>
        /// Visit a field definition
        /// </summary>
        /// <param name="field">Field definition</param>
        public override void VisitFieldDefinition(FieldDefinition field)
        {
            Write(EVisualBasicKeyword.Dim, ESpaceSurrounder.After);
            if (field.IsStatic)
            {
                Write(EVisualBasicKeyword.Shared, ESpaceSurrounder.After);
            }
            Write(HandleKeywords(field.Name));
            Write(EVisualBasicKeyword.As, ESpaceSurrounder.Both);
            VisitTypeReference(field.FieldType);
        }

        /// <summary>
        /// Visit a method definition
        /// </summary>
        /// <param name="method">Method definition</param>
        public override void VisitMethodDefinition(MethodDefinition method)
        {
            if (method.IsStatic)
            {
                Write(EVisualBasicKeyword.Shared, ESpaceSurrounder.After);
            }
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

            if (type.Name.EndsWith(REFERENCE_TYPE_TAG))
            {
                name = name.Replace(REFERENCE_TYPE_TAG, String.Empty);
            }
            if (type.Namespace != String.Empty)
            {
                name = type.Namespace + NAMESPACE_SEPARATOR + name;
            }
            if (type is GenericInstanceType)
            {
                GenericInstanceType git = type as GenericInstanceType;
                name = name.Replace(GENERIC_TYPE_TAG + git.GenericArguments.Count, String.Empty);
                HandleTypeName(type, name);
                m_displayconstraintsstack.Push(false);
                VisitVisitableCollection(LEFT_PARENTHESIS + Surround(EVisualBasicKeyword.Of, ESpaceSurrounder.After), RIGHT_PARENTHESIS, BASIC_SEPARATOR, false, git.GenericArguments);
                m_displayconstraintsstack.Pop();
            }
            else
            {
                HandleTypeName(type, name);
            }
            if (m_displayconstraintsstack.Peek() && (type is GenericParameter))
            {
                VisitVisitableCollection(Surround(EVisualBasicKeyword.As, ESpaceSurrounder.Both) + LEFT_BRACE, RIGHT_BRACE, BASIC_SEPARATOR, false, (type as GenericParameter).Constraints);
            }
        }

        /// <summary>
        /// Visit a generic parameter collection
        /// </summary>
        /// <param name="genparams">Generic parameter collection</param>
        public override void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams)
        {
            m_displayconstraintsstack.Push(true);
            VisitVisitableCollection(LEFT_PARENTHESIS + Surround(EVisualBasicKeyword.Of, ESpaceSurrounder.After), RIGHT_PARENTHESIS, BASIC_SEPARATOR, false, genparams);
            m_displayconstraintsstack.Pop();
        }

        /// <summary>
        /// Visit a parameter definition collection
        /// </summary>
        /// <param name="parameters"></param>
        public override void VisitParameterDefinitionCollection(Mono.Collections.Generic.Collection<ParameterDefinition> parameters)
        {
            VisitVisitableCollection(LEFT_PARENTHESIS, RIGHT_PARENTHESIS, BASIC_SEPARATOR, true, parameters);
        }

        /// <summary>
        /// Visit a parameter definition
        /// </summary>
        /// <param name="parameter">Parameter definition</param>
        public override void VisitParameterDefinition(ParameterDefinition parameter)
        {
            if (parameter.ParameterType.Name.EndsWith(REFERENCE_TYPE_TAG))
            {
                Write(EVisualBasicKeyword.ByRef);
            }
            else
            {
                Write(EVisualBasicKeyword.ByVal);
            }
            Write(SPACE);
            Write(HandleKeywords(parameter.Name));
            Write(EVisualBasicKeyword.As, ESpaceSurrounder.Both);
            VisitTypeReference(parameter.ParameterType);
        }
        #endregion

        #endregion

    }
}
