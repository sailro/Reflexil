/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region " Imports "
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Reflector.CodeModel;
using RC=Reflector.CodeModel;
using System.Collections;
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
        private Dictionary<IPropertyDeclaration, PropertyDefinition> m_propertycache;
        private Dictionary<IFieldDeclaration, FieldDefinition> m_fieldcache;
        private Dictionary<IEventDeclaration, EventDefinition> m_eventcache;
        private Dictionary<IResource, Resource> m_resourcecache;
        //fix: use toString() instead of object himself (getHashcode seems to be overriden)
        private Dictionary<String, AssemblyNameReference> m_assemblynamereferencecache;
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
            m_propertycache = new Dictionary<IPropertyDeclaration, PropertyDefinition>();
            m_fieldcache = new Dictionary<IFieldDeclaration, FieldDefinition>();
            m_eventcache = new Dictionary<IEventDeclaration, EventDefinition>();
            m_resourcecache = new Dictionary<IResource, Resource>();
            m_assemblynamereferencecache = new Dictionary<String, AssemblyNameReference>();
        }

        /// <summary>
        /// Remove an item from cache
        /// </summary>
        /// <param name="item">item to remove</param>
        public void RemoveFromCache(object item) {
            var dictionaries = new IDictionary[] {m_methodcache, m_propertycache, m_fieldcache, m_eventcache, m_resourcecache, m_assemblynamereferencecache};
            foreach (IDictionary dic in dictionaries) {
                if (dic.Contains(item)) {
                    dic.Remove(item);
                }
            }
        }

        private delegate TCecil FindMatchingMember<TCecil, TReflector>(TypeDefinition tdef, TReflector item);
        private TCecil GetMemberItemFromCache<TCecil, TReflector>(object item, Dictionary<TReflector, TCecil> cache, FindMatchingMember<TCecil, TReflector> finder) where TReflector : RC.IMemberReference
        {
            if (!(item is TReflector))
            {
                throw new ArgumentException(typeof(TReflector).Name);
            }

            TReflector memberdec = (TReflector)item;
            TCecil result = default(TCecil);

            if ((memberdec != null) && (!cache.ContainsKey(memberdec)))
            {
                ITypeDeclaration classdec = (ITypeDeclaration)memberdec.DeclaringType;
                TypeDefinition typedef = ReflectorHelper.FindMatchingType(AssemblyDefinition, classdec);

                if (typedef != null)
                {
                    result = finder(typedef, memberdec);
                    if (result != null)
                    {
                        // add result to cache
                        cache.Add(memberdec, result);
                    }
                }
            }
            else
            {
                // Definition is already cached
                result = cache[memberdec];
            }

            return result;
        }

        /// <summary>
        /// Retrieve from cache or search a method definition from host program' object.
        /// </summary>
        /// <param name="item">object (ie Method declaration/definition)</param>
        /// <returns>Method definition or null if not found</returns>
        public MethodDefinition GetMethodDefinition(object item)
        {
            return GetMemberItemFromCache<MethodDefinition, IMethodDeclaration>(item, m_methodcache, ReflectorHelper.FindMatchingMethod);
        }

        /// <summary>
        /// Retrieve from cache or search a property definition from host program' object.
        /// </summary>
        /// <param name="item">object (ie Property declaration/definition)</param>
        /// <returns>Property definition or null if not found</returns>
        public PropertyDefinition GetPropertyDefinition(object item)
        {
            return GetMemberItemFromCache<PropertyDefinition, IPropertyDeclaration>(item, m_propertycache, ReflectorHelper.FindMatchingProperty);
        }

        /// <summary>
        /// Retrieve from cache or search a field definition from host program' object.
        /// </summary>
        /// <param name="item">object (ie Field declaration/definition)</param>
        /// <returns>Field definition or null if not found</returns>
        public FieldDefinition GetFieldDefinition(object item)
        {
            return GetMemberItemFromCache<FieldDefinition, IFieldDeclaration>(item, m_fieldcache, ReflectorHelper.FindMatchingField);
        }

        /// <summary>
        /// Retrieve from cache or search an event definition from host program' object.
        /// </summary>
        /// <param name="item">object (ie Event declaration/definition)</param>
        /// <returns>Event definition or null if not found</returns>
        public EventDefinition GetEventDefinition(object item)
        {
            return GetMemberItemFromCache<EventDefinition, IEventDeclaration>(item, m_eventcache, ReflectorHelper.FindMatchingEvent);
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

            //fix: use toString() instead of object himself (getHashcode seems to be overriden)
            if ((aref != null) && (!m_assemblynamereferencecache.ContainsKey(aref.ToString())))
            {
                foreach (var anref in
                    AssemblyDefinition.MainModule.AssemblyReferences.Where(anref => anref.ToString() == aref.ToString()))
                {
                    result = anref;
                    // add result to cache
                    m_assemblynamereferencecache.Add(aref.ToString(), result);
                    break;
                }
            }
            else
            {
                // Assembly Name Reference is already cached
                result = m_assemblynamereferencecache[aref.ToString()];
            }

            return result;
        }

        /// <summary>
        /// Retrieve from cache or search an embedded resource from an object.
        /// </summary>
        /// <param name="item">object</param>
        /// <returns>Embedded resource or null if not found</returns>
        public EmbeddedResource GetEmbeddedResource(object item)
        {
            // Fix for in-memory resource in embedded resource
            PropertyInfo parent = item.GetType().GetProperty("Parent", typeof (IEmbeddedResource));
            if (parent != null)
                item = (IEmbeddedResource)parent.GetValue(item, null);

            return GetGenericResource<IEmbeddedResource, EmbeddedResource>(item, ReflectorHelper.FindMatchingResource);
        }

        /// <summary>
        /// Retrieve from cache or search an assembly linked resource from an object.
        /// </summary>
        /// <param name="item">object</param>
        /// <returns>Assembly linked resource or null if not found</returns>
        public AssemblyLinkedResource GetAssemblyLinkedResource(object item)
        {
            return GetGenericResource<IResource, AssemblyLinkedResource>(item, ReflectorHelper.FindMatchingResource);
        }

        /// <summary>
        /// Retrieve from cache or search a linked resource from an object.
        /// </summary>
        /// <param name="item">object</param>
        /// <returns>Linked resource or null if not found</returns>
        public LinkedResource GetLinkedResource(object item)
        {
            return GetGenericResource<IFileResource, LinkedResource>(item, ReflectorHelper.FindMatchingResource);
        }

        private CT GetGenericResource<RT, CT>(object item, Func<AssemblyDefinition, RT, CT> finder) where RT:IResource where CT:Resource
        {
            if (!(item is RT))
                throw new ArgumentException(typeof(RT).Name);

            RT eres = (RT)item;
            CT result = default(CT);

            if ((eres != null) && (!m_resourcecache.ContainsKey(eres)))
            {
                // add result to cache
                result = finder(AssemblyDefinition, eres);
                if (result != null)
                    m_resourcecache.Add(eres, result);
            }
            else
            {
                // resource is already cached
                result = (CT)m_resourcecache[eres];
            }

            return result;
        }
        #endregion

    }
}
