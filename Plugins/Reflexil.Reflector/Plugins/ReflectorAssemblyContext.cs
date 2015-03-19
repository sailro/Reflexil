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

using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Reflector.CodeModel;
using System.Collections;

namespace Reflexil.Plugins.Reflector
{
	internal sealed class ReflectorAssemblyContext : BaseAssemblyContext
	{

		private readonly Dictionary<IMethodDeclaration, MethodDefinition> _methodcache;
		private readonly Dictionary<IPropertyDeclaration, PropertyDefinition> _propertycache;
		private readonly Dictionary<IFieldDeclaration, FieldDefinition> _fieldcache;
		private readonly Dictionary<IEventDeclaration, EventDefinition> _eventcache;
		private readonly Dictionary<IResource, Resource> _resourcecache;
		//fix: use toString() instead of object himself (getHashcode seems to be overriden)
		private readonly Dictionary<string, AssemblyNameReference> _assemblynamereferencecache;
		private readonly Dictionary<ITypeDeclaration, TypeDefinition> _typecache;

		public ReflectorAssemblyContext()
		{
			_methodcache = new Dictionary<IMethodDeclaration, MethodDefinition>();
			_propertycache = new Dictionary<IPropertyDeclaration, PropertyDefinition>();
			_fieldcache = new Dictionary<IFieldDeclaration, FieldDefinition>();
			_eventcache = new Dictionary<IEventDeclaration, EventDefinition>();
			_resourcecache = new Dictionary<IResource, Resource>();
			_assemblynamereferencecache = new Dictionary<String, AssemblyNameReference>();
			_typecache = new Dictionary<ITypeDeclaration, TypeDefinition>();
		}

		public void RemoveFromCache(object item)
		{
			var dictionaries = new IDictionary[] { _methodcache, _propertycache, _fieldcache, _eventcache, _resourcecache, _assemblynamereferencecache, _typecache };
			foreach (var dic in dictionaries)
				dic.Remove(item);
		}

		public TypeDefinition GetTypeDefinition(ITypeDeclaration item)
		{
			return TryGetOrAdd(_typecache, item, tdef => ReflectorHelper.FindMatchingType(AssemblyDefinition, tdef));
		}

		public MethodDefinition GetMethodDefinition(IMethodDeclaration item)
		{
			return TryGetOrAdd(_methodcache, item, mdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType as ITypeDeclaration);
				return tdef == null ? null : ReflectorHelper.FindMatchingMethod(tdef, mdef);
			});
		}

		public PropertyDefinition GetPropertyDefinition(IPropertyDeclaration item)
		{
			return TryGetOrAdd(_propertycache, item, pdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType as ITypeDeclaration);
				return tdef == null ? null : ReflectorHelper.FindMatchingProperty(tdef, pdef);
			});
		}

		public FieldDefinition GetFieldDefinition(IFieldDeclaration item)
		{
			return TryGetOrAdd(_fieldcache, item, fdef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType as ITypeDeclaration);
				return tdef == null ? null : ReflectorHelper.FindMatchingField(tdef, fdef);
			});
		}

		public EventDefinition GetEventDefinition(IEventDeclaration item)
		{
			return TryGetOrAdd(_eventcache, item, edef =>
			{
				var tdef = GetTypeDefinition(item.DeclaringType as ITypeDeclaration);
				return tdef == null ? null : ReflectorHelper.FindMatchingEvent(tdef, edef);
			});
		}

		public AssemblyNameReference GetAssemblyNameReference(IAssemblyReference item)
		{
			//fix: use toString() instead of object himself (getHashcode seems to be overriden)
			AssemblyNameReference result;

			var key = item.ToString();
			if (_assemblynamereferencecache.TryGetValue(key, out result))
				return result;

			result = ReflectorHelper.FindMatchingAssemblyReference(AssemblyDefinition, item);
			if (result == null)
				return null;

			_assemblynamereferencecache.Add(key, result);
			return result;
		}

		public Resource GetResource(IResource item)
		{
			// Fix for in-memory resource in embedded resource
			var parent = item.GetType().GetProperty("Parent", typeof(IEmbeddedResource));
			if (parent != null)
				item = parent.GetValue(item, null) as IEmbeddedResource;

			return TryGetOrAdd(_resourcecache, item, res => ReflectorHelper.FindMatchingResource(AssemblyDefinition, res));
		}

	}
}