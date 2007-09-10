
#region " Imports "
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
    internal abstract class BaseLanguageHelper : IReflectionVisitor, ILanguageHelper
    {

        #region " Constants "
        protected const string BASIC_SEPARATOR = ", ";
        protected const string GENERIC_TYPE_TAG = "`";
        protected const string PARAMETER_LIST_START = "(";
        protected const string PARAMETER_LIST_END = ")";
        protected const string REFERENCE_TYPE_TAG = "&";
        protected const string NAMESPACE_SEPARATOR = ".";
        protected const string SPACE = " ";
        #endregion

        #region " Fields "
        protected StringBuilder m_builder = new StringBuilder();
        protected Dictionary<string, string> m_aliases = new Dictionary<string, string>();
        protected bool m_fullnamespaces = true;
        #endregion

        #region " Methods "

        #region " Abstract "
        protected abstract void WriteMethodSignature(MethodDefinition mdef);
        protected abstract void WriteMethodBody(MethodDefinition mdef);
        protected abstract void WriteTypeSignature(TypeDefinition mdef);
        protected abstract void WriteField(FieldDefinition fdef);
        public abstract void VisitFieldDefinition(FieldDefinition field);
        public abstract void VisitMethodDefinition(MethodDefinition method);
        public abstract void VisitTypeReference(TypeReference type);
        public abstract void VisitGenericParameterCollection(GenericParameterCollection genparams);
        public abstract void VisitParameterDefinition(ParameterDefinition parameter);
        public abstract void VisitParameterDefinitionCollection(ParameterDefinitionCollection parameters);
        #endregion

        #region " Not Implemented "
        public virtual void VisitCustomAttributeCollection(CustomAttributeCollection customAttrs)
        {
        }
        
        public virtual void VisitSecurityDeclaration(SecurityDeclaration secDecl)
        {
        }

        public virtual void VisitCustomAttribute(CustomAttribute customAttr)
        {
        }

        public virtual void VisitSecurityDeclarationCollection(SecurityDeclarationCollection secDecls)
        {
        }

        public virtual void VisitOverride(MethodReference ov)
        {
        }

        public virtual void VisitOverrideCollection(OverrideCollection meth)
        {
        }


        public virtual void VisitGenericParameter(GenericParameter genparam)
        {
        }

        public virtual void VisitConstructorCollection(ConstructorCollection ctors)
        {
        }

        public virtual void VisitEventDefinitionCollection(EventDefinitionCollection events)
        {
        }

        public virtual void VisitFieldDefinitionCollection(FieldDefinitionCollection fields)
        {
        }

        public virtual void VisitMethodDefinitionCollection(MethodDefinitionCollection methods)
        {
        }

        public virtual void VisitNestedTypeCollection(NestedTypeCollection nestedTypes)
        {
        }

        public virtual void VisitPropertyDefinitionCollection(PropertyDefinitionCollection properties)
        {
        }

        public virtual void VisitTypeDefinitionCollection(TypeDefinitionCollection types)
        {
        }

        public virtual void VisitEventDefinition(EventDefinition evt)
        {
        }

        public virtual void VisitModuleDefinition(ModuleDefinition @module)
        {
        }

        public virtual void VisitNestedType(TypeDefinition nestedType)
        {
        }

        public virtual void VisitPropertyDefinition(PropertyDefinition @property)
        {
        }

        public virtual void VisitConstructor(MethodDefinition ctor)
        {
        }

        public virtual void TerminateModuleDefinition(ModuleDefinition @module)
        {
        }

        public virtual void VisitExternType(TypeReference externType)
        {
        }

        public virtual void VisitExternTypeCollection(ExternTypeCollection externs)
        {
        }

        public virtual void VisitInterface(TypeReference interf)
        {
        }

        public virtual void VisitInterfaceCollection(InterfaceCollection interfaces)
        {
        }

        public virtual void VisitMemberReference(MemberReference member)
        {
        }

        public virtual void VisitMemberReferenceCollection(MemberReferenceCollection members)
        {
        }

        public virtual void VisitMarshalSpec(MarshalSpec marshalSpec)
        {
        }

        public virtual void VisitTypeReferenceCollection(TypeReferenceCollection refs)
        {
        }

        public virtual void VisitPInvokeInfo(PInvokeInfo pinvk)
        {
        }
        #endregion

        public virtual string GetMethodSignature(MethodDefinition mdef)
        {
            m_builder.Length = 0;
            WriteMethodSignature(mdef);
            return m_builder.ToString();
        }

        public virtual string GetMethod(MethodDefinition mdef)
        {
            m_builder.Length = 0;
            WriteMethodSignature(mdef);
            WriteMethodBody(mdef);
            return m_builder.ToString();
        }

        public virtual string GetField(FieldDefinition fdef)
        {
            m_builder.Length = 0;
            WriteField(fdef);
            return m_builder.ToString();
        }

        public virtual string GetTypeSignature(TypeDefinition tdef)
        {
            m_builder.Length = 0;
            WriteTypeSignature(tdef);
            return m_builder.ToString();
        }

        protected string HandleAliases(string str)
        {
            foreach (string alias in m_aliases.Keys)
            {
                str = str.Replace(alias, m_aliases[alias]);
            }
            return str;
        }

        protected void HandleName(TypeReference type, string name)
        {
            name = HandleAliases(name);
            if (!m_fullnamespaces)
            {
                name = name.Replace(type.Namespace + NAMESPACE_SEPARATOR, String.Empty);
            }
            m_builder.Append(name);
        }

        protected void AppendIf(MethodAttributes value, MethodAttributes test, string str)
        {
            if (value == test)
            {
                m_builder.Append(str);
            }
        }

        public virtual void VisitTypeDefinition(TypeDefinition type)
        {
            VisitTypeReference(type);
        }

        protected virtual void VisitVisitableCollection(string start, string end, string separator, bool always, ICollection collection)
        {
            if (always | collection.Count > 0)
            {
                m_builder.Append(start);
            }

            bool firstloop = true;
            foreach (IReflectionVisitable item in collection)
            {
                if (!firstloop)
                {
                    m_builder.Append(separator);
                }
                else
                {
                    firstloop = false;
                }
                if (item is TypeDefinition)
                {
                    VisitTypeReference(item as TypeDefinition);
                }
                else
                {
                    item.Accept(this);
                }
            }

            if (always | collection.Count > 0)
            {
                m_builder.Append(end);
            }
        }
        #endregion
    }
}
