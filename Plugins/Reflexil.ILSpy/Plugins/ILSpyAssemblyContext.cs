extern alias ilspycecil;
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
		private readonly Dictionary<icTypeDefinition, TypeDefinition> _typedefinitioncache;

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
			_typedefinitioncache = new Dictionary<icTypeDefinition, TypeDefinition>();
		}

		public void RemoveFromCache(object item)
		{
			var dictionaries = new IDictionary[] { _methodcache, _propertycache, _fieldcache, _eventcache, _resourcecache, _assemblynamereferencecache };
			foreach (var dic in dictionaries.Where(dic => dic.Contains(item)))
				dic.Remove(item);
		}

		private delegate TCecil FindMatchingMember<out TCecil, in TReflector>(TypeDefinition tdef, TReflector item)
					where TCecil : class;

		private TCecil GetMemberItemFromCache<TCecil, TILSpyCecil>(TILSpyCecil item, IDictionary<TILSpyCecil, TCecil> cache, FindMatchingMember<TCecil, TILSpyCecil> finder)
			where TILSpyCecil : class, icIMemberDefinition
			where TCecil : class
		{
			TCecil result;

			if (cache.TryGetValue(item, out result))
				return result;

			var ictdef = item.DeclaringType;
			var tdef = ILSpyHelper.FindMatchingType(AssemblyDefinition, ictdef);

			if (tdef == null)
				return null;

			result = finder(tdef, item);
			if (result == null)
				return null;

			cache.Add(item, result);
			return result;
		}

		public MethodDefinition GetMethodDefinition(icMethodDefinition item)
		{
			return GetMemberItemFromCache(item, _methodcache, ILSpyHelper.FindMatchingMethod);
		}

		public PropertyDefinition GetPropertyDefinition(icPropertyDefinition item)
		{
			return GetMemberItemFromCache(item, _propertycache, ILSpyHelper.FindMatchingProperty);
		}

		public FieldDefinition GetFieldDefinition(icFieldDefinition item)
		{
			return GetMemberItemFromCache(item, _fieldcache, ILSpyHelper.FindMatchingField);
		}

		public EventDefinition GetEventDefinition(icEventDefinition item)
		{
			return GetMemberItemFromCache(item, _eventcache, ILSpyHelper.FindMatchingEvent);
		}

		public AssemblyNameReference GetAssemblyNameReference(icAssemblyNameReference item)
		{
			AssemblyNameReference result;

			if (_assemblynamereferencecache.TryGetValue(item, out result))
				return result;

			result = AssemblyDefinition.MainModule.AssemblyReferences.FirstOrDefault(r => r.ToString() == item.ToString());
			if (result == null)
				return null;

			_assemblynamereferencecache.Add(item, result);
			return result;
		}

		public TypeDefinition GetTypeDefinition(icTypeDefinition item)
		{
			TypeDefinition result;

			if (_typedefinitioncache.TryGetValue(item, out result))
				return result;

			result = ILSpyHelper.FindMatchingType(AssemblyDefinition, item);
			if (result == null)
				return null;

			_typedefinitioncache.Add(item, result);
			return result;
		}
	}
}
