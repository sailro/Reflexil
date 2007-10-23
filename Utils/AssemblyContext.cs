
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using Reflector.CodeModel;
using Mono.Cecil;
#endregion

namespace Reflexil.Utils
{
	class AssemblyContext
    {

        #region " Fields "
        private AssemblyDefinition m_adef;
        private Dictionary<IMethodDeclaration, MethodDefinition> m_methodcache;
        #endregion

        #region " Properties "
        public AssemblyContext(AssemblyDefinition adef)
        {
            m_adef = adef;
            m_methodcache = new Dictionary<IMethodDeclaration, MethodDefinition>();
        }
        #endregion

        #region " Methods "
        public AssemblyDefinition AssemblyDefinition
        {
            get
            {
                return m_adef;
            }
        }

        public MethodDefinition GetMethodDefinition(IMethodDeclaration mdec)
        {
            MethodDefinition result = null;

            if ((mdec != null) && (!m_methodcache.ContainsKey(mdec)))
            {
                ITypeDeclaration classdec = (ITypeDeclaration)mdec.DeclaringType;
                TypeDefinition typedef = CecilHelper.FindMatchingType(AssemblyDefinition, classdec);

                if (typedef != null)
                {
                    result = CecilHelper.FindMatchingMethod(typedef, mdec);
                    if (result != null)
                    {
                        // add result to cache
                        m_methodcache.Add(mdec, result);
                    }
                }
            }
            else
            {
                // Method definition is already cached
                result = m_methodcache[mdec];
            }

            return result;
        }
        #endregion

	}
}
