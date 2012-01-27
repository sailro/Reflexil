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
    /// C# code generator
    /// </summary>
    internal class CSharpHelper : BaseLanguageHelper
    {

        #region " Constants "
        protected const string GENERIC_CONSTRAINT_LIST_START = " : ";
        protected const string REGION_START = "#region ";
        protected const string REGION_END = "#endregion ";
        protected const string SEPARATOR = ";";
        protected const string COMMENT = "// ";
        protected const string AT = "@";
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor. Aliases initialisation.
        /// </summary>
        public CSharpHelper()
        {
            m_aliases.Add("System.Object", ECSharpKeyword.@object.ToString());
            m_aliases.Add("System.Int16", ECSharpKeyword.@short.ToString());
            m_aliases.Add("System.Int32", ECSharpKeyword.@int.ToString());
            m_aliases.Add("System.Int64", ECSharpKeyword.@long.ToString());
            m_aliases.Add("System.UInt16", ECSharpKeyword.@ushort.ToString());
            m_aliases.Add("System.UInt32", ECSharpKeyword.@uint.ToString());
            m_aliases.Add("System.UInt64", ECSharpKeyword.@ulong.ToString());
            m_aliases.Add("System.Boolean", ECSharpKeyword.@bool.ToString());
            m_aliases.Add("System.Char", ECSharpKeyword.@char.ToString());
            m_aliases.Add("System.Decimal", ECSharpKeyword.@decimal.ToString());
            m_aliases.Add("System.Double", ECSharpKeyword.@double.ToString());
            m_aliases.Add("System.Single", ECSharpKeyword.@float.ToString());
            m_aliases.Add("System.String", ECSharpKeyword.@string.ToString());
            m_aliases.Add("System.Void", ECSharpKeyword.@void.ToString());
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
            return GenerateSourceCode(mdef, references, Surround(ECSharpKeyword.@namespace, ESpaceSurrounder.After), LEFT_BRACE, RIGHT_BRACE);
        }

        /// <summary>
        /// Replace all keyword in a string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result string</returns>
        protected override string HandleKeywords(string str)
        {
            foreach (string keyword in System.Enum.GetNames(typeof(ECSharpKeyword)))
            {
                if (str == keyword)
                {
                    str = AT + str; 
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
            mdef.Accept(this);

            if (mdef.GenericParameters.Count > 0)
            {
                foreach (GenericParameter genparam in mdef.GenericParameters)
                {
                    if (genparam.Constraints.Count > 0)
                    {
                        Write(ECSharpKeyword.@where, ESpaceSurrounder.Both);
                        genparam.Accept(this);
                        VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, String.Empty, BASIC_SEPARATOR, false, genparam.Constraints);
                    }
                }
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
            WriteLine(LEFT_BRACE);
            if (mdef.ReturnType.FullName != typeof(void).FullName)
            {
                Write(ECSharpKeyword.@return, ESpaceSurrounder.After);
                Write(ECSharpKeyword.@default);
                Write(LEFT_PARENTHESIS);
                VisitTypeReference(mdef.ReturnType);
                Write(RIGHT_PARENTHESIS);
                WriteLine(SEPARATOR);
            }
            UnIdent();
            IdentLevel--;
            Ident();
            WriteLine(RIGHT_BRACE);
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
                foreach (GenericParameter genparam in tdef.GenericParameters)
                {
                    if (genparam.Constraints.Count > 0)
                    {
                        Write(ECSharpKeyword.@where, ESpaceSurrounder.Both);
                        genparam.Accept(this);
                        VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, String.Empty, BASIC_SEPARATOR, false, genparam.Constraints);
                    }
                }
                Replace(GENERIC_TYPE_TAG + tdef.GenericParameters.Count, String.Empty);
            }
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
            Write(REGION_START);
            WriteLine("\" Imports \"");
            foreach (string item in DEFAULT_NAMESPACES)
            {
                Write(ECSharpKeyword.@using, ESpaceSurrounder.After);
                Write(item);
                WriteLine(SEPARATOR);
            }
            WriteLine(REGION_END);
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
        /// Write a method's owner type to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteType(MethodDefinition mdef)
        {
            WriteType(mdef, Surround(ECSharpKeyword.@class, ESpaceSurrounder.After), LEFT_BRACE, RIGHT_BRACE);
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
            if (field.IsStatic)
            {
                Write(ECSharpKeyword.@static, ESpaceSurrounder.After);
            }
            VisitTypeReference(field.FieldType);
            Write(SPACE);
            Write(HandleKeywords(field.Name));
            Write(SEPARATOR);
        }

        /// <summary>
        /// Visit a method definition
        /// </summary>
        /// <param name="method">Method definition</param>
        public override void VisitMethodDefinition(MethodDefinition method)
        {
            if (IsUnsafe(method))
            {
                Write(ECSharpKeyword.@unsafe, ESpaceSurrounder.After);
            }
            if (method.IsStatic)
            {
                Write(ECSharpKeyword.@static, ESpaceSurrounder.After);
            }
            if (method.IsConstructor)
            {
                string name = method.DeclaringType.Name;
                if (method.DeclaringType.GenericParameters.Count > 0)
                {
                    name = name.Replace(GENERIC_TYPE_TAG + method.DeclaringType.GenericParameters.Count, String.Empty);
                }
                Write(name);
            }
            else
            {
                VisitTypeReference(method.ReturnType);
                Write(SPACE);
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
                Write(ECSharpKeyword.@ref, ESpaceSurrounder.After);
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
                VisitVisitableCollection(LEFT_CHEVRON, RIGHT_CHEVRON, BASIC_SEPARATOR, false, git.GenericArguments);
            }
            else
            {
                HandleTypeName(type, name);
            }
        }

        /// <summary>
        /// Visit a generic parameter collection
        /// </summary>
        /// <param name="genparams">Generic parameter collection</param>
        public override void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams)
        {
            VisitVisitableCollection(LEFT_CHEVRON, RIGHT_CHEVRON, BASIC_SEPARATOR, false, genparams);
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
            VisitTypeReference(parameter.ParameterType);
            Write(SPACE);
            Write(HandleKeywords(parameter.Name));
        }
        #endregion

        #endregion

    }
}
