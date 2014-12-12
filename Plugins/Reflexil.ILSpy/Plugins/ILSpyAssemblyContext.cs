extern alias ilspycecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Reflexil.Plugins;

using icMethodDefinition = ilspycecil::Mono.Cecil.MethodDefinition;
using icPropertyDefinition = ilspycecil::Mono.Cecil.PropertyDefinition;
using icFieldDefinition = ilspycecil::Mono.Cecil.FieldDefinition;
using icEventDefinition = ilspycecil::Mono.Cecil.EventDefinition;
using icResource = ilspycecil::Mono.Cecil.Resource;
using icAssemblyNameReference = ilspycecil::Mono.Cecil.AssemblyNameReference;
using icIMemberDefinition = ilspycecil::Mono.Cecil.IMemberDefinition;
using icEmbeddedResource = ilspycecil::Mono.Cecil.EmbeddedResource;
using icAssemblyLinkedResource = ilspycecil::Mono.Cecil.AssemblyLinkedResource;
using icLinkedResource = ilspycecil::Mono.Cecil.LinkedResource;
using icTypeDefinition = ilspycecil::Mono.Cecil.TypeDefinition;

namespace Reflexil.ILSpy.Plugins
{
	class ILSpyAssemblyContext : IAssemblyContext
	{
		private readonly Dictionary<icMethodDefinition, MethodDefinition> _methodcache;
		private readonly Dictionary<icPropertyDefinition, PropertyDefinition> _propertycache;
		private readonly Dictionary<icFieldDefinition, FieldDefinition> _fieldcache;
		private readonly Dictionary<icEventDefinition, EventDefinition> _eventcache;
		private readonly Dictionary<icResource, Resource> _resourcecache;
		private readonly Dictionary<icAssemblyNameReference, AssemblyNameReference> _assemblynamereferencecache;
		private readonly Dictionary<icTypeDefinition, TypeDefinition> _typecache;

		public AssemblyDefinition AssemblyDefinition { get; set; }

		public ILSpyAssemblyContext()
			: this(null)
		{
		}

		public ILSpyAssemblyContext(AssemblyDefinition adef)
		{
			AssemblyDefinition = adef;
			_methodcache = new Dictionary<icMethodDefinition, MethodDefinition>();
			_propertycache = new Dictionary<icPropertyDefinition, PropertyDefinition>();
			_fieldcache = new Dictionary<icFieldDefinition, FieldDefinition>();
			_eventcache = new Dictionary<icEventDefinition, EventDefinition>();
			_resourcecache = new Dictionary<icResource, Resource>();
			_assemblynamereferencecache = new Dictionary<icAssemblyNameReference, AssemblyNameReference>();
			_typecache = new Dictionary<icTypeDefinition, TypeDefinition>();
		}

		public void RemoveFromCache(object item)
		{
			var dictionaries = new IDictionary[] { _methodcache, _propertycache, _fieldcache, _eventcache, _resourcecache, _assemblynamereferencecache };
			foreach (var dic in dictionaries.Where(dic => dic.Contains(item)))
				dic.Remove(item);
		}

		private TCecilDef TryGetOrAdd<TCecilDef, TILSpyDef>(Dictionary<TILSpyDef, TCecilDef> cache, TILSpyDef item, Func<TILSpyDef, TCecilDef> finder) where TCecilDef : class
		{
			TCecilDef result;

			if (cache.TryGetValue(item, out result))
				return result;

			result = finder(item);
			if (result == null)
				return null;

			cache.Add(item, result);
			return result;
		}

		public MethodDefinition GetMethodDefinition(icMethodDefinition item)
		{
			return TryGetOrAdd(_methodcache, item, mdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : ILSpyHelper.FindMatchingMethod(tdef, mdef);
			});
		}

		public PropertyDefinition GetPropertyDefinition(icPropertyDefinition item)
		{
			return TryGetOrAdd(_propertycache, item, pdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : ILSpyHelper.FindMatchingProperty(tdef, pdef);
			});
		}

		public FieldDefinition GetFieldDefinition(icFieldDefinition item)
		{
			return TryGetOrAdd(_fieldcache, item, fdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : ILSpyHelper.FindMatchingField(tdef, fdef);
			});
		}

		public EventDefinition GetEventDefinition(icEventDefinition item)
		{
			return TryGetOrAdd(_eventcache, item, edef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : ILSpyHelper.FindMatchingEvent(tdef, edef);
			});
		}

		public AssemblyNameReference GetAssemblyNameReference(icAssemblyNameReference item)
		{
			return TryGetOrAdd(_assemblynamereferencecache, item, anref => ILSpyHelper.FindMatchingAssemblyReference(AssemblyDefinition, anref));
		}

		public TypeDefinition GetTypeDefinition(icTypeDefinition item)
		{
			return TryGetOrAdd(_typecache, item, tdef => ILSpyHelper.FindMatchingType(AssemblyDefinition, tdef));
		}

		public Resource GetResource(icResource item)
		{
			return TryGetOrAdd(_resourcecache, item, res => ILSpyHelper.FindMatchingResource(AssemblyDefinition, res));
		}
	}
}
