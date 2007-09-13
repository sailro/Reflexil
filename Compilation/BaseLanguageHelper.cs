
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
        protected const string QUOTE = "\"";
        protected string[] DEFAULT_NAMESPACES = { "System", "System.Collections.Generic", "System.Text" };
        #endregion

        #region " Fields "
        private StringBuilder m_identedbuilder = new StringBuilder();
        private int m_identlevel = 0;
        protected Dictionary<string, string> m_aliases = new Dictionary<string, string>();
        protected bool m_fullnamespaces = true;
        #endregion

        #region " Properties "
        protected int IdentLevel
        {
            get
            {
                return m_identlevel;
            }
            set
            {
                m_identlevel = value;
            }
        }
        #endregion

        #region " Methods "

        #region " Abstract "
        protected abstract void WriteMethodSignature(MethodDefinition mdef);
        protected abstract void WriteMethodBody(MethodDefinition mdef);
        protected abstract void WriteTypeSignature(TypeDefinition mdef);
        protected abstract void WriteField(FieldDefinition fdef);
        public abstract string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references);
        public abstract void VisitTypeDefinition(TypeDefinition type);
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

        #region " Text generation "
        protected void ReIdent(int newidentlevel)
        {
            UnIdent();
            IdentLevel = newidentlevel;
            Ident();
        }

        protected void Ident()
        {
            for (int i = 0; i < IdentLevel; i++)
            {
                m_identedbuilder.Append("\t");
            }
        }

        protected void UnIdent()
        {
            for (int i = 0; i < IdentLevel; i++)
            {
                if ((m_identedbuilder.Length > 0) && (m_identedbuilder[m_identedbuilder.Length - 1] == '\t'))
                {
                    m_identedbuilder.Remove(m_identedbuilder.Length - 1, 1);
                }
            }
        }

        protected void Replace(string oldvalue, string newvalue)
        {
            m_identedbuilder.Replace(oldvalue, newvalue);
        }

        protected void WriteLine()
        {
            m_identedbuilder.AppendLine();
            Ident();
        }

        protected void WriteLine(string str)
        {
            Write(str);
            WriteLine();
        }

        protected void Write(string str)
        {
            m_identedbuilder.Append(str);
        }

        protected void Reset()
        {
            m_identlevel = 0;
            m_identedbuilder.Length = 0;
        }

        protected string GetResult()
        {
            return m_identedbuilder.ToString();
        }
#endregion

        public virtual string GetMethodSignature(MethodDefinition mdef)
        {
            Reset();
            WriteMethodSignature(mdef);
            return GetResult();
        }

        public virtual string GetMethod(MethodDefinition mdef)
        {
            Reset();
            WriteMethodSignature(mdef);
            WriteMethodBody(mdef);
            return GetResult();
        }

        public virtual string GetField(FieldDefinition fdef)
        {
            Reset();
            WriteField(fdef);
            return GetResult();
        }

        public virtual string GetTypeSignature(TypeDefinition tdef)
        {
            Reset();
            WriteTypeSignature(tdef);
            return GetResult();
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
            Write(name);
        }

        protected void AppendIf(MethodAttributes value, MethodAttributes test, string str)
        {
            if (value == test)
            {
                Write(str);
            }
        }

        protected virtual void VisitVisitableCollection(string start, string end, string separator, bool always, ICollection collection)
        {
            if (always | collection.Count > 0)
            {
                Write(start);
            }

            bool firstloop = true;
            foreach (IReflectionVisitable item in collection)
            {
                if (!firstloop)
                {
                    Write(separator);
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
                Write(end);
            }
        }
        #endregion

    }
}
