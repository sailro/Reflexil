/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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
using JustDecompile.API.Core;
using Mono.Cecil;

namespace Reflexil.Plugins.JustDecompile
{
	internal sealed class JustDecompileAssemblyContext : BaseAssemblyContext
	{
		private readonly Dictionary<IMethodDefinition, MethodDefinition> _methodcache;
		private readonly Dictionary<IPropertyDefinition, PropertyDefinition> _propertycache;
		private readonly Dictionary<IFieldDefinition, FieldDefinition> _fieldcache;
		private readonly Dictionary<IEventDefinition, EventDefinition> _eventcache;
		private readonly Dictionary<IResource, Resource> _resourcecache;
		// the key will by the private cecil type, not the JD interface
		private readonly Dictionary<object, AssemblyNameReference> _assemblynamereferencecache;
		private readonly Dictionary<ITypeDefinition, TypeDefinition> _typecache;

		public JustDecompileAssemblyContext()
		{
			_methodcache = new Dictionary<IMethodDefinition, MethodDefinition>();
			_propertycache = new Dictionary<IPropertyDefinition, PropertyDefinition>();
			_fieldcache = new Dictionary<IFieldDefinition, FieldDefinition>();
			_eventcache = new Dictionary<IEventDefinition, EventDefinition>();
			_resourcecache = new Dictionary<IResource, Resource>();
			_assemblynamereferencecache = new Dictionary<object, AssemblyNameReference>();
			_typecache = new Dictionary<ITypeDefinition, TypeDefinition>();
		}

		public void RemoveFromCache(object item)
		{
			var dictionaries = new IDictionary[]
				{_methodcache, _propertycache, _fieldcache, _eventcache, _resourcecache, _assemblynamereferencecache, _typecache};
			foreach (var dic in dictionaries)
				dic.Remove(item);
		}

		public MethodDefinition GetMethodDefinition(IMethodDefinition item)
		{
			return TryGetOrAdd(_methodcache, item, mdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : JustDecompileHelper.FindMatchingMethod(tdef, mdef);
			});
		}

		public PropertyDefinition GetPropertyDefinition(IPropertyDefinition item)
		{
			return TryGetOrAdd(_propertycache, item, pdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : JustDecompileHelper.FindMatchingProperty(tdef, pdef);
			});
		}

		public FieldDefinition GetFieldDefinition(IFieldDefinition item)
		{
			return TryGetOrAdd(_fieldcache, item, fdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : JustDecompileHelper.FindMatchingField(tdef, fdef);
			});
		}

		public EventDefinition GetEventDefinition(IEventDefinition item)
		{
			return TryGetOrAdd(_eventcache, item, edef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType);
				return tdef == null ? null : JustDecompileHelper.FindMatchingEvent(tdef, edef);
			});
		}

		public AssemblyNameReference GetAssemblyNameReference(IAssemblyNameReference item)
		{
			var canr = JustDecompileHelper.ExtractCecilAssemblyNameReference(item);
			if (canr == null)
				return null;

			return TryGetOrAdd(_assemblynamereferencecache, canr,
				anref => JustDecompileHelper.FindMatchingAssemblyReference(AssemblyDefinition, anref));
		}

		public TypeDefinition GetTypeDefinition(ITypeDefinition item)
		{
			return TryGetOrAdd(_typecache, item, tdef => JustDecompileHelper.FindMatchingType(AssemblyDefinition, tdef));
		}

		public Resource GetResource(IResource item)
		{
			return TryGetOrAdd(_resourcecache, item, res => JustDecompileHelper.FindMatchingResource(AssemblyDefinition, res));
		}
	}
}