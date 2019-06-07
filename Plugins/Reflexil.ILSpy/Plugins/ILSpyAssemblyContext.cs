/* Reflexil Copyright (c) 2007-2019 Sebastien Lebreton

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

using System.Collections;
using System.Collections.Generic;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using Mono.Cecil;
using Resource = Mono.Cecil.Resource;
using AssemblyNameReference = Mono.Cecil.AssemblyNameReference;
using IResource = ICSharpCode.Decompiler.Metadata.Resource;

namespace Reflexil.Plugins.ILSpy
{
	internal sealed class ILSpyAssemblyContext : BaseAssemblyContext
	{
		private readonly Dictionary<IMethod, MethodDefinition> _methodcache;
		private readonly Dictionary<IProperty, PropertyDefinition> _propertycache;
		private readonly Dictionary<IField, FieldDefinition> _fieldcache;
		private readonly Dictionary<IEvent, EventDefinition> _eventcache;
		private readonly Dictionary<IResource, Resource> _resourcecache;
		private readonly Dictionary<IAssemblyReference, AssemblyNameReference> _assemblynamereferencecache;
		private readonly Dictionary<ITypeDefinition, TypeDefinition> _typecache;

		public ILSpyAssemblyContext()
		{
			_methodcache = new Dictionary<IMethod, MethodDefinition>();
			_propertycache = new Dictionary<IProperty, PropertyDefinition>();
			_fieldcache = new Dictionary<IField, FieldDefinition>();
			_eventcache = new Dictionary<IEvent, EventDefinition>();
			_resourcecache = new Dictionary<IResource, Resource>();
			_assemblynamereferencecache = new Dictionary<IAssemblyReference, AssemblyNameReference>();
			_typecache = new Dictionary<ITypeDefinition, TypeDefinition>();
		}

		public void RemoveFromCache(object item)
		{
			var dictionaries = new IDictionary[] {_methodcache, _propertycache, _fieldcache, _eventcache, _resourcecache, _assemblynamereferencecache, _typecache};
			foreach (var dic in dictionaries)
				dic.Remove(item);
		}

		public MethodDefinition GetMethodDefinition(IMethod item)
		{
			return TryGetOrAdd(_methodcache, item, mdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringTypeDefinition);
				return tdef == null ? null : ILSpyHelper.FindMatchingMethod(tdef, mdef);
			});
		}

		public PropertyDefinition GetPropertyDefinition(IProperty item)
		{
			return TryGetOrAdd(_propertycache, item, pdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringTypeDefinition);
				return tdef == null ? null : ILSpyHelper.FindMatchingProperty(tdef, pdef);
			});
		}

		public FieldDefinition GetFieldDefinition(IField item)
		{
			return TryGetOrAdd(_fieldcache, item, fdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringTypeDefinition);
				return tdef == null ? null : ILSpyHelper.FindMatchingField(tdef, fdef);
			});
		}

		public EventDefinition GetEventDefinition(IEvent item)
		{
			return TryGetOrAdd(_eventcache, item, edef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringTypeDefinition);
				return tdef == null ? null : ILSpyHelper.FindMatchingEvent(tdef, edef);
			});
		}

		public AssemblyNameReference GetAssemblyNameReference(IAssemblyReference item)
		{
			return TryGetOrAdd(_assemblynamereferencecache, item, anref => ILSpyHelper.FindMatchingAssemblyReference(AssemblyDefinition, anref));
		}

		public TypeDefinition GetTypeDefinition(ITypeDefinition item)
		{
			return TryGetOrAdd(_typecache, item, tdef => ILSpyHelper.FindMatchingType(AssemblyDefinition, tdef));
		}

		public Resource GetResource(IResource item)
		{
			return TryGetOrAdd(_resourcecache, item, res => ILSpyHelper.FindMatchingResource(AssemblyDefinition, res));
		}
	}
}
