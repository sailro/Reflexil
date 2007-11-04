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
using System.Collections.Specialized;
using System.Text;
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
        protected const string VALUE_PARAMETER = "ByVal ";
        protected const string REFERENCE_PARAMETER = "ByRef ";
        protected const string GENERIC_CONSTRAINT_LIST_START = " As {";
        protected const string GENERIC_CONSTRAINT_LIST_END = "}";
        protected const string GENERIC_LIST_START = "(Of ";
        protected const string GENERIC_LIST_END = ")";
        protected const string AS_TYPE = " As ";
        protected const string DECLARE = "Dim ";
        protected const string METHOD_SUB = "Sub ";
        protected const string METHOD_FUNCTION = "Function ";
        protected const string METHOD_END = "End ";
        protected const string NEW_LINE = " _";
        protected const string RETURN = "Return ";
        protected const string NOTHING = "Nothing";
        protected const string CONSTRUCTOR = "New";
        protected const string SHARED = "Shared ";
        protected const string COMMENT = "' ";
        protected const string IMPORTS = "Imports ";
        protected const string REGION_START = "#Region ";
        protected const string REGION_END = "#End Region ";
        protected const string CLASS_END = "End Class";
        protected const string CLASS = "Class ";
        protected const string NAMESPACE_END = "End Namespace";
        protected const string NAMESPACE = "Namespace ";
        protected const string OPTION_STRICT_ON = "Option Strict On";
        protected const string OPTION_EXPLICIT_ON = "Option Explicit On";
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
            m_aliases.Add("System.Object", "Object");
            m_aliases.Add("System.Int16", "Short");
            m_aliases.Add("System.Int32", "Integer");
            m_aliases.Add("System.Int64", "long");
            m_aliases.Add("System.UInt16", "UShort");
            m_aliases.Add("System.UInt32", "UInteger");
            m_aliases.Add("System.UInt64", "ULong");
            m_aliases.Add("System.Boolean", "Boolean");
            m_aliases.Add("System.Char", "Char");
            m_aliases.Add("System.Decimal", "Decimal");
            m_aliases.Add("System.Double", "Double");
            m_aliases.Add("System.Single", "Single");
            m_aliases.Add("System.String", "String");
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
                Write(METHOD_SUB);
            }
            else
            {
                Write(METHOD_FUNCTION);
            }
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
            return GenerateSourceCode(mdef, references, NAMESPACE, String.Empty, NAMESPACE_END);
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
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                Write(AS_TYPE);
                VisitTypeReference(mdef.ReturnType.ReturnType);
            }
        }

        /// <summary>
        /// Write a method body to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteMethodBody(MethodDefinition mdef)
        {
            WriteLine();
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                Write(RETURN);
                Write(NOTHING);
                WriteLine();
            }
            Write(METHOD_END);
            HandleSubFunction(mdef.ReturnType.ReturnType);
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
            WriteLine(OPTION_EXPLICIT_ON);
            WriteLine(OPTION_STRICT_ON);
            WriteLine();
            foreach (string item in DEFAULT_NAMESPACES)
            {
                Write(IMPORTS);
                WriteLine(item);
            }
        }

        /// <summary>
        /// Write a method's owner type to the text buffer
        /// </summary>
        /// <param name="mdef">Method definition</param>
        protected override void WriteType(MethodDefinition mdef)
        {
            WriteType(mdef, CLASS, String.Empty, CLASS_END);
        }
        #endregion

        #region " IReflectionVisitor "
        /// <summary>
        /// Visit a field definition
        /// </summary>
        /// <param name="field">Field definition</param>
        public override void VisitFieldDefinition(FieldDefinition field)
        {
            Write(DECLARE);
            if (field.IsStatic)
            {
                Write(SHARED);
            }
            Write(field.Name);
            Write(AS_TYPE);
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
                Write(SHARED);
            }
            HandleSubFunction(method.ReturnType.ReturnType);
            if (method.IsConstructor)
            {
                Write(CONSTRUCTOR);
            }
            else
            {
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
                m_displayconstraintsstack.Push(false);
                VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, git.GenericArguments);
                m_displayconstraintsstack.Pop();
            }
            else
            {
                HandleName(type, name);
            }
            if (m_displayconstraintsstack.Peek() && (type is GenericParameter))
            {
                VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, GENERIC_CONSTRAINT_LIST_END, BASIC_SEPARATOR, false, (type as GenericParameter).Constraints);
            }
        }

        /// <summary>
        /// Visit a generic parameter collection
        /// </summary>
        /// <param name="genparams">Generic parameter collection</param>
        public override void VisitGenericParameterCollection(GenericParameterCollection genparams)
        {
            m_displayconstraintsstack.Push(true);
            VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, genparams);
            m_displayconstraintsstack.Pop();
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
            if (parameter.ParameterType.Name.EndsWith(REFERENCE_TYPE_TAG))
            {
                Write(REFERENCE_PARAMETER);
            }
            else
            {
                Write(VALUE_PARAMETER);
            }
            Write(parameter.Name);
            Write(AS_TYPE);
            VisitTypeReference(parameter.ParameterType);
        }
        #endregion

        #endregion

    }
}
