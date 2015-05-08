/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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

extern alias ilspycecil;

using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;

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

namespace Reflexil.Plugins.ILSpy
{
	internal sealed class ILSpyAssemblyContext : BaseAssemblyContext
	{
		private readonly Dictionary<icMethodDefinition, MethodDefinition> _methodcache;
		private readonly Dictionary<icPropertyDefinition, PropertyDefinition> _propertycache;
		private readonly Dictionary<icFieldDefinition, FieldDefinition> _fieldcache;
		private readonly Dictionary<icEventDefinition, EventDefinition> _eventcache;
		private readonly Dictionary<icResource, Resource> _resourcecache;
		private readonly Dictionary<icAssemblyNameReference, AssemblyNameReference> _assemblynamereferencecache;
		private readonly Dictionary<icTypeDefinition, TypeDefinition> _typecache;

		public ILSpyAssemblyContext()
		{
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
			var dictionaries = new IDictionary[] { _methodcache, _propertycache, _fieldcache, _eventcache, _resourcecache, _assemblynamereferencecache, _typecache};
			foreach (var dic in dictionaries)
				dic.Remove(item);
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
