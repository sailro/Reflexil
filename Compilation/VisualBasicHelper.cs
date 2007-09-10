
#region " Imports "
using System;
using System.Collections.Generic;
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
        }

        public override string GetMethodSignature(MethodDefinition mdef)
        {
            m_builder.Length = 0;
            WriteMethodSignature(mdef);
            return m_builder.ToString();
        }

        protected override void WriteMethodSignature(MethodDefinition mdef)
        {
            mdef.Accept(this);
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                m_builder.Append(AS_TYPE);
                VisitTypeReference(mdef.ReturnType.ReturnType);
            }
        }

        protected override void WriteMethodBody(MethodDefinition mdef)
        {
            m_builder.AppendLine();
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                m_builder.Append(RETURN);
                m_builder.Append(NOTHING);
                m_builder.AppendLine();
            }
            m_builder.Append(METHOD_END);
            HandleSubFunction(mdef.ReturnType.ReturnType);
            m_builder.AppendLine();
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
                m_builder.Replace(GENERIC_TYPE_TAG + tdef.GenericParameters.Count, String.Empty);
            }
        }

        public override void VisitFieldDefinition(FieldDefinition field)
        {
            m_builder.Append(DECLARE);
            m_builder.Append(field.Name);
            m_builder.Append(AS_TYPE);
            VisitTypeReference(field.FieldType);
        }

        private void HandleSubFunction(TypeReference tref)
        {
            if (tref.FullName == typeof(void).FullName)
            {
                m_builder.Append(METHOD_SUB);
            }
            else
            {
                m_builder.Append(METHOD_FUNCTION);
            }
        }

        public override void VisitMethodDefinition(MethodDefinition method)
        {
            HandleSubFunction(method.ReturnType.ReturnType);
            if (method.IsConstructor)
            {
                m_builder.Append(CONSTRUCTOR);
            }
            else
            {
                m_builder.Append(method.Name);
            }
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
                VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, git.GenericArguments);
            }
            else
            {
                HandleName(type, name);
            }
            // TODO: check when generic return type, we add this. we're wrong!
            if (type is GenericParameter)
            {
                VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, GENERIC_CONSTRAINT_LIST_END, BASIC_SEPARATOR, false, (type as GenericParameter).Constraints);
            }
        }

        public override void VisitGenericParameterCollection(GenericParameterCollection genparams)
        {
            VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, genparams);
        }

        public override void VisitParameterDefinitionCollection(ParameterDefinitionCollection parameters)
        {
            VisitVisitableCollection(PARAMETER_LIST_START, PARAMETER_LIST_END, BASIC_SEPARATOR, true, parameters);
        }

        public override void VisitParameterDefinition(ParameterDefinition parameter)
        {
            if (parameter.ParameterType.Name.EndsWith(REFERENCE_TYPE_TAG))
            {
                m_builder.Append(REFERENCE_PARAMETER);
            }
            else
            {
                m_builder.Append(VALUE_PARAMETER);
            }
            m_builder.Append(parameter.Name);
            m_builder.Append(AS_TYPE);
            VisitTypeReference(parameter.ParameterType);
        }
        #endregion

    }
}
