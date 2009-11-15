/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using Reflector.CodeModel;
#endregion

namespace Reflexil.Plugins.Reflector
{
    /// <summary>
    /// Assembly context: allowing to cache methods definitions
    /// </summary>
    class ReflectorAssemblyContext : IAssemblyContext
    {

        #region " Fields "
        private AssemblyDefinition m_adef;
        private Dictionary<IMethodDeclaration, MethodDefinition> m_methodcache;
        private Dictionary<IAssemblyReference, AssemblyNameReference> m_assemblynamereferencecache;
        #endregion

        #region " Properties "
        public AssemblyDefinition AssemblyDefinition
        {
            get
            {
                return m_adef;
            }
            set
            {
                m_adef = value;
            }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public ReflectorAssemblyContext()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="adef">Assembly definition</param>
        public ReflectorAssemblyContext(AssemblyDefinition adef)
        {
            m_adef = adef;
            m_methodcache = new Dictionary<IMethodDeclaration, MethodDefinition>();
            m_assemblynamereferencecache = new Dictionary<IAssemblyReference, AssemblyNameReference>();
        }

        /// <summary>
        /// Retrieve from cache or search a method definition from host program' object.
        /// </summary>
        /// <param name="item">object (ie Method declaration/definition)</param>
        /// <returns>Method definition or null if not found</returns>
        public MethodDefinition GetMethodDefinition(object item)
        {
            if (!(item is IMethodDeclaration))
            {
                throw new ArgumentException(typeof(IMethodDeclaration).Name);
            }

            IMethodDeclaration mdec = item as IMethodDeclaration;
            MethodDefinition result = null;

            if ((mdec != null) && (!m_methodcache.ContainsKey(mdec)))
            {
                ITypeDeclaration classdec = (ITypeDeclaration)mdec.DeclaringType;
                TypeDefinition typedef = ReflectorHelper.FindMatchingType(AssemblyDefinition, classdec);

                if (typedef != null)
                {
                    result = ReflectorHelper.FindMatchingMethod(typedef, mdec);
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

        /// <summary>
        /// Retrieve from cache or search an assembly name reference from user program' object (assembly reference).
        /// </summary>
        /// <param name="item">object (Assembly reference, ...)</param>
        /// <returns>Assembly Name Reference or null if not found</returns>
        public AssemblyNameReference GetAssemblyNameReference(object item)
        {
            if (!(item is IAssemblyReference))
            {
                throw new ArgumentException(typeof(IAssemblyReference).Name);
            }

            IAssemblyReference aref = item as IAssemblyReference;
            AssemblyNameReference result = null;

            if ((aref != null) && (!m_assemblynamereferencecache.ContainsKey(aref)))
            {
                foreach (AssemblyNameReference anref in AssemblyDefinition.MainModule.AssemblyReferences)
                {
                    if (anref.ToString() == aref.ToString())
                    {
                        result = anref;
                        // add result to cache
                        m_assemblynamereferencecache.Add(aref, result);
                    }
                }
            }
            else
            {
                // Assembly Name Reference is already cached
                result = m_assemblynamereferencecache[aref];
            }

            return result;
        }
        #endregion

    }
}
