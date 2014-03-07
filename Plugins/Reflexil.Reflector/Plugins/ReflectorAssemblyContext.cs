/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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

#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
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
	internal class ReflectorAssemblyContext : IAssemblyContext
	{

		#region Fields

		private readonly Dictionary<IMethodDeclaration, MethodDefinition> _methodcache;
		private readonly Dictionary<IPropertyDeclaration, PropertyDefinition> _propertycache;
		private readonly Dictionary<IFieldDeclaration, FieldDefinition> _fieldcache;
		private readonly Dictionary<IEventDeclaration, EventDefinition> _eventcache;
		private readonly Dictionary<IResource, Resource> _resourcecache;
		//fix: use toString() instead of object himself (getHashcode seems to be overriden)
		private readonly Dictionary<String, AssemblyNameReference> _assemblynamereferencecache;

		#endregion

		#region Properties

		public AssemblyDefinition AssemblyDefinition { get; set; }

		#endregion

		#region Methods

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
			AssemblyDefinition = adef;
			_methodcache = new Dictionary<IMethodDeclaration, MethodDefinition>();
			_propertycache = new Dictionary<IPropertyDeclaration, PropertyDefinition>();
			_fieldcache = new Dictionary<IFieldDeclaration, FieldDefinition>();
			_eventcache = new Dictionary<IEventDeclaration, EventDefinition>();
			_resourcecache = new Dictionary<IResource, Resource>();
			_assemblynamereferencecache = new Dictionary<String, AssemblyNameReference>();
		}

		/// <summary>
		/// Remove an item from cache
		/// </summary>
		/// <param name="item">item to remove</param>
		public void RemoveFromCache(object item)
		{
			var dictionaries = new IDictionary[]
			{_methodcache, _propertycache, _fieldcache, _eventcache, _resourcecache, _assemblynamereferencecache};
			foreach (var dic in dictionaries.Where(dic => dic.Contains(item)))
				dic.Remove(item);
		}

		private delegate TCecil FindMatchingMember<out TCecil, in TReflector>(TypeDefinition tdef, TReflector item)
			where TCecil : class;

		private TCecil GetMemberItemFromCache<TCecil, TReflector>(object item, IDictionary<TReflector, TCecil> cache,
			FindMatchingMember<TCecil, TReflector> finder) where TReflector : class, IMemberReference where TCecil : class
		{
			if (!(item is TReflector))
				throw new ArgumentException(typeof (TReflector).Name);

			var memberdec = (TReflector) item;
			TCecil result;

			if (!cache.ContainsKey(memberdec))
			{
				var classdec = (ITypeDeclaration) memberdec.DeclaringType;
				var typedef = ReflectorHelper.FindMatchingType(AssemblyDefinition, classdec);

				if (typedef == null)
					return null;

				result = finder(typedef, memberdec);
				if (result != null)
				{
					// add result to cache
					cache.Add(memberdec, result);
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
			return GetMemberItemFromCache(item, _methodcache, ReflectorHelper.FindMatchingMethod);
		}

		/// <summary>
		/// Retrieve from cache or search a property definition from host program' object.
		/// </summary>
		/// <param name="item">object (ie Property declaration/definition)</param>
		/// <returns>Property definition or null if not found</returns>
		public PropertyDefinition GetPropertyDefinition(object item)
		{
			return GetMemberItemFromCache(item, _propertycache, ReflectorHelper.FindMatchingProperty);
		}

		/// <summary>
		/// Retrieve from cache or search a field definition from host program' object.
		/// </summary>
		/// <param name="item">object (ie Field declaration/definition)</param>
		/// <returns>Field definition or null if not found</returns>
		public FieldDefinition GetFieldDefinition(object item)
		{
			return GetMemberItemFromCache(item, _fieldcache, ReflectorHelper.FindMatchingField);
		}

		/// <summary>
		/// Retrieve from cache or search an event definition from host program' object.
		/// </summary>
		/// <param name="item">object (ie Event declaration/definition)</param>
		/// <returns>Event definition or null if not found</returns>
		public EventDefinition GetEventDefinition(object item)
		{
			return GetMemberItemFromCache(item, _eventcache, ReflectorHelper.FindMatchingEvent);
		}

		/// <summary>
		/// Retrieve from cache or search an assembly name reference from user program' object (assembly reference).
		/// </summary>
		/// <param name="item">object (Assembly reference, ...)</param>
		/// <returns>Assembly Name Reference or null if not found</returns>
		public AssemblyNameReference GetAssemblyNameReference(object item)
		{
			if (!(item is IAssemblyReference))
				throw new ArgumentException(typeof (IAssemblyReference).Name);

			var aref = item as IAssemblyReference;
			AssemblyNameReference result = null;

			//fix: use toString() instead of object himself (getHashcode seems to be overriden)
			if (!_assemblynamereferencecache.ContainsKey(aref.ToString()))
			{
				foreach (var anref in
					AssemblyDefinition.MainModule.AssemblyReferences.Where(anref => anref.ToString() == aref.ToString()))
				{
					result = anref;
					// add result to cache
					_assemblynamereferencecache.Add(aref.ToString(), result);
					break;
				}
			}
			else
			{
				// Assembly Name Reference is already cached
				result = _assemblynamereferencecache[aref.ToString()];
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
			var parent = item.GetType().GetProperty("Parent", typeof (IEmbeddedResource));
			if (parent != null)
				item = parent.GetValue(item, null);

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

		private TC GetGenericResource<TR, TC>(object item, Func<AssemblyDefinition, TR, TC> finder)
			where TR : class, IResource where TC : Resource
		{
			if (!(item is TR))
				throw new ArgumentException(typeof (TR).Name);

			var eres = (TR) item;
			TC result;

			if (!_resourcecache.ContainsKey(eres))
			{
				// add result to cache
				result = finder(AssemblyDefinition, eres);
				if (result != null)
					_resourcecache.Add(eres, result);
			}
			else
			{
				// resource is already cached
				result = (TC) _resourcecache[eres];
			}

			return result;
		}

		#endregion

	}
}
