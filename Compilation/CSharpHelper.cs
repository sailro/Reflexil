
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
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
        #endregion

        #region " Methods "
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

        protected override void WriteMethodSignature(MethodDefinition mdef)
        {
            mdef.Accept(this);

            if (mdef.GenericParameters.Count > 0)
            {
                foreach (GenericParameter genparam in mdef.GenericParameters)
                {
                    if (genparam.Constraints.Count > 0)
                    {
                        m_builder.Append(GENERIC_CONSTRAINT);
                        genparam.Accept(this);
                        VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, String.Empty, BASIC_SEPARATOR, false, genparam.Constraints);
                    }
                }
            }
        }

        protected override void WriteMethodBody(MethodDefinition mdef)
        {
            m_builder.AppendLine();
            m_builder.Append(METHOD_BODY_START);
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                m_builder.AppendLine();
                m_builder.Append(RETURN);
                m_builder.Append(DEFAULT_START);
                VisitTypeReference(mdef.ReturnType.ReturnType);
                m_builder.AppendLine(DEFAULT_END);
            }
            m_builder.Append(METHOD_BODY_END);
            m_builder.AppendLine();
        }

        protected override void WriteField(FieldDefinition fdef)
        {
            fdef.Accept(this);
        }

        public override void VisitFieldDefinition(FieldDefinition field)
        {
            VisitTypeReference(field.FieldType);
            m_builder.Append(SPACE);
            m_builder.Append(field.Name);
        }

        protected override void WriteTypeSignature(TypeDefinition tdef)
        {
            tdef.Accept(this);

            if (tdef.GenericParameters.Count > 0)
            {
                foreach (GenericParameter genparam in tdef.GenericParameters)
                {
                    if (genparam.Constraints.Count > 0)
                    {
                        m_builder.Append(GENERIC_CONSTRAINT);
                        genparam.Accept(this);
                        VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, String.Empty, BASIC_SEPARATOR, false, genparam.Constraints);
                    }
                }
                m_builder.Replace(GENERIC_TYPE_TAG + tdef.GenericParameters.Count, String.Empty);
            }
        }

        public override void VisitMethodDefinition(MethodDefinition method)
        {
            if (method.IsConstructor)
            {
                m_builder.Append(method.DeclaringType.Name);
            }
            else
            {
                VisitTypeReference(method.ReturnType.ReturnType);
                m_builder.Append(SPACE);
                m_builder.Append(method.Name);
            }
        }

        public override void VisitTypeReference(TypeReference type)
        {
            string name = type.Name;

            if (type.Name.EndsWith(REFERENCE_TYPE_TAG))
            {
                m_builder.Append(REFERENCE_PARAMETER);
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
            VisitTypeReference(parameter.ParameterType);
            m_builder.Append(SPACE);
            m_builder.Append(parameter.Name);
        }
        #endregion
    }
}
