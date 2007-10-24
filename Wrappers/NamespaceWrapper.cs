
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
#endregion

namespace Reflexil.Wrappers
{
	class NamespaceWrapper
    {

        #region " Fields "
        private string m_namespace;
        private ModuleDefinition m_modef;
        #endregion

        #region " Methods "
        public NamespaceWrapper(ModuleDefinition modef, string @namespace)
        {
            m_modef = modef;
            if (string.IsNullOrEmpty(@namespace))
            {
                m_namespace = "-";
            }
            else
            {
                m_namespace = @namespace;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is NamespaceWrapper)
            {
                NamespaceWrapper other = obj as NamespaceWrapper;
                return (m_modef.Equals(other.m_modef)) && (m_namespace == other.m_namespace);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (m_modef.GetHashCode().ToString() + "|" + m_namespace.GetHashCode().ToString()).GetHashCode();
        }

        public override string ToString()
        {
            return m_namespace;
        }
        #endregion
	}
}
