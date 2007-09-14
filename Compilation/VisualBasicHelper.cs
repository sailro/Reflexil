
#region " Imports "
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
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
        #endregion

        #region " Fields "
        private Stack<bool> m_displayconstraintsstack = new Stack<bool>();
        #endregion

        #region " Methods "
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
            m_aliases.Add("System.Single", "Float");
            m_aliases.Add("System.String", "String");

            m_displayconstraintsstack.Push(false);
        }

        public override string GetMethodSignature(MethodDefinition mdef)
        {
            Reset();
            WriteMethodSignature(mdef);
            return GetResult();
        }

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

        public override string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references)
        {
            return GenerateSourceCode(mdef, references, NAMESPACE, String.Empty, NAMESPACE_END);
        }

        #region " Writers "
        protected override void WriteMethodSignature(MethodDefinition mdef)
        {
            mdef.Accept(this);
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                Write(AS_TYPE);
                VisitTypeReference(mdef.ReturnType.ReturnType);
            }
        }

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

        protected override void WriteField(FieldDefinition fdef)
        {
            fdef.Accept(this);
        }

        protected override void WriteTypeSignature(TypeDefinition tdef)
        {
            tdef.Accept(this);


            if (tdef.GenericParameters.Count > 0)
            {
                Replace(GENERIC_TYPE_TAG + tdef.GenericParameters.Count, String.Empty);
            }
        }

        protected override void WriteComment(string comment)
        {
            Write(COMMENT);
            WriteLine(comment);
        }

        protected override void WriteFieldsStubs(FieldDefinitionCollection fields)
        {
            WriteFieldsStubs(fields, REGION_START, REGION_END);
        }

        protected override void WriteMethodsStubs(MethodDefinition mdef, MethodDefinitionCollection methods)
        {
            WriteMethodsStubs(mdef, methods, REGION_START, REGION_END);
        }

        protected override void WriteDefaultNamespaces()
        {
            foreach (string item in DEFAULT_NAMESPACES)
            {
                Write(IMPORTS);
                WriteLine(item);
            }
        }

        protected override void WriteClass(MethodDefinition mdef)
        {
            WriteClass(mdef, CLASS, String.Empty, CLASS_END);
        }
        #endregion

        #region " IReflectionVisitor "
        public override void VisitFieldDefinition(FieldDefinition field)
        {
            Write(DECLARE);
            Write(field.Name);
            Write(AS_TYPE);
            VisitTypeReference(field.FieldType);
        }

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

        public override void VisitTypeDefinition(TypeDefinition type)
        {
            HandleName(type, type.Name);
        }

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

        public override void VisitGenericParameterCollection(GenericParameterCollection genparams)
        {
            m_displayconstraintsstack.Push(true);
            VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, genparams);
            m_displayconstraintsstack.Pop();
        }

        public override void VisitParameterDefinitionCollection(ParameterDefinitionCollection parameters)
        {
            VisitVisitableCollection(PARAMETER_LIST_START, PARAMETER_LIST_END, BASIC_SEPARATOR, true, parameters);
        }

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
