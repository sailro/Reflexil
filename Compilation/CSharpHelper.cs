/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
        protected const string REFERENCE_PARAMETER = "ref ";
        protected const string GENERIC_CONSTRAINT = " where ";
        protected const string GENERIC_CONSTRAINT_LIST_START = " : ";
        protected const string GENERIC_LIST_START = "<";
        protected const string GENERIC_LIST_END = ">";
        protected const string METHOD_BODY_START = "{";
        protected const string METHOD_BODY_END = "}";
        protected const string RETURN = "return ";
        protected const string DEFAULT_START = "default(";
        protected const string DEFAULT_END = ");";
        protected const string NAMESPACE_START = "{";
        protected const string NAMESPACE_END = "}";
        protected const string CLASS_START = "{";
        protected const string CLASS_END = "}";
        protected const string USING = "using ";
        protected const string NAMESPACE = "namespace ";
        protected const string CLASS = "class ";
        protected const string REGION_START = "#region ";
        protected const string REGION_END = "#endregion ";
        protected const string SEPARATOR = ";";
        protected const string COMMENT = "// ";
        protected const string STATIC = "static ";
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor. Aliases initialisation.
        /// </summary>
        public CSharpHelper()
        {
            m_aliases.Add("System.Object", "object");
            m_aliases.Add("System.Int16", "short");
            m_aliases.Add("System.Int32", "int");
            m_aliases.Add("System.Int64", "long");
            m_aliases.Add("System.UInt16", "ushort");
            m_aliases.Add("System.UInt32", "uint");
            m_aliases.Add("System.UInt64", "ulong");
            m_aliases.Add("System.Boolean", "bool");
            m_aliases.Add("System.Char", "char");
            m_aliases.Add("System.Decimal", "decimal");
            m_aliases.Add("System.Double", "double");
            m_aliases.Add("System.Single", "float");
            m_aliases.Add("System.String", "string");
            m_aliases.Add("System.Void", "void");
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
            return GenerateSourceCode(mdef, references, NAMESPACE, NAMESPACE_START, NAMESPACE_END);
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
                        Write(GENERIC_CONSTRAINT);
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
            WriteLine(METHOD_BODY_START);
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                Write(RETURN);
                Write(DEFAULT_START);
                VisitTypeReference(mdef.ReturnType.ReturnType);
                WriteLine(DEFAULT_END);
            }
            UnIdent();
            IdentLevel--;
            Ident();
            WriteLine(METHOD_BODY_END);
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
                        Write(GENERIC_CONSTRAINT);
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
        protected override void WriteFieldsStubs(FieldDefinitionCollection fields)
        {
            WriteFieldsStubs(fields, REGION_START, REGION_END);
        }

        /// <summary>
        /// Write methods stubs to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition to exclude</param>
        /// <param name="methods">Methods definitions</param>
        protected override void WriteMethodsStubs(MethodDefinition mdef, MethodDefinitionCollection methods)
        {
            WriteMethodsStubs(mdef, methods, REGION_START, REGION_END);
        }

        /// <summary>
        /// Write default namespaces to the text buffer
        /// </summary>
        protected override void WriteDefaultNamespaces()
        {
            foreach (string item in DEFAULT_NAMESPACES)
            {
                Write(USING);
                Write(item);
                WriteLine(SEPARATOR);
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
        /// Write a method's owner type to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteType(MethodDefinition mdef)
        {
            WriteType(mdef, CLASS, CLASS_START, CLASS_END);
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
                Write(STATIC);
            }
            VisitTypeReference(field.FieldType);
            Write(SPACE);
            Write(field.Name);
            Write(SEPARATOR);
        }

        /// <summary>
        /// Visit a method definition
        /// </summary>
        /// <param name="method">Method definition</param>
        public override void VisitMethodDefinition(MethodDefinition method)
        {
            if (method.IsStatic)
            {
                Write(STATIC);
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
                VisitTypeReference(method.ReturnType.ReturnType);
                Write(SPACE);
                Write(method.Name);
            }
        }

        /// <summary>
        /// Visit a type definition
        /// </summary>
        /// <param name="type">Type definition</param>
        public override void VisitTypeDefinition(TypeDefinition type)
        {
            HandleName(type, type.Name);
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
                Write(REFERENCE_PARAMETER);
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
                HandleName(type, name);
                VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, git.GenericArguments);
            }
            else
            {
                HandleName(type, name);
            }
        }

        /// <summary>
        /// Visit a generic parameter collection
        /// </summary>
        /// <param name="genparams">Generic parameter collection</param>
        public override void VisitGenericParameterCollection(GenericParameterCollection genparams)
        {
            VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, genparams);
        }

        /// <summary>
        /// Visit a parameter definition collection
        /// </summary>
        /// <param name="parameters"></param>
        public override void VisitParameterDefinitionCollection(ParameterDefinitionCollection parameters)
        {
            VisitVisitableCollection(PARAMETER_LIST_START, PARAMETER_LIST_END, BASIC_SEPARATOR, true, parameters);
        }

        /// <summary>
        /// Visit a parameter definition
        /// </summary>
        /// <param name="parameter">Parameter definition</param>
        public override void VisitParameterDefinition(ParameterDefinition parameter)
        {
            VisitTypeReference(parameter.ParameterType);
            Write(SPACE);
            Write(parameter.Name);
        }
        #endregion

        #endregion

    }
}
