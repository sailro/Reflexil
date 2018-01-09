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

using System.Linq;
using Mono.Cecil;
using Reflexil.Plugins;

namespace Reflexil.Utils
{
	public static class DeleteHelper
	{
		public static void Delete(AssemblyNameReference anref)
		{
			var plugin = PluginFactory.GetInstance();

			foreach (var wrapper in plugin.Package.HostAssemblies)
			{
				if (!wrapper.IsValid)
					continue;

				if (!plugin.IsAssemblyContextLoaded(wrapper.Location))
					continue;

				var context = plugin.GetAssemblyContext(wrapper.Location);
				var moddef = context.AssemblyDefinition.Modules.FirstOrDefault(
					imoddef => imoddef.AssemblyReferences.Any(ianref => anref == ianref));

				if (moddef != null)
					moddef.AssemblyReferences.Remove(anref);
			}
		}

		public static void Delete(TypeDefinition tdef)
		{
			if (tdef.DeclaringType != null)
			{
				var ntypes = tdef.DeclaringType.NestedTypes;
				if (ntypes.Contains(tdef))
					ntypes.Remove(tdef);
			}

			if (tdef.Module == null)
				return;

			var types = tdef.Module.Types;
			if (types.Contains(tdef))
				types.Remove(tdef);
		}

		public static void Delete(MethodDefinition mdef)
		{
			if (mdef.DeclaringType == null)
				return;

			// check all properties for getter/setter
			if (mdef.IsGetter || mdef.IsSetter)
			{
				foreach (var property in mdef.DeclaringType.Properties)
				{
					if (mdef == property.GetMethod)
						property.GetMethod = null;

					if (mdef == property.SetMethod)
						property.SetMethod = null;
				}
			}

			if (mdef.DeclaringType.Methods.Contains(mdef))
				mdef.DeclaringType.Methods.Remove(mdef);
		}

		public static void Delete(PropertyDefinition pdef)
		{
			if (pdef.DeclaringType == null)
				return;

			var properties = pdef.DeclaringType.Properties;
			if (!properties.Contains(pdef))
				return;

			if (pdef.GetMethod != null)
				Delete(pdef.GetMethod);

			if (pdef.SetMethod != null)
				Delete(pdef.SetMethod);

			properties.Remove(pdef);
		}

		public static void Delete(FieldDefinition fdef)
		{
			if (fdef.DeclaringType == null)
				return;

			var fields = fdef.DeclaringType.Fields;
			if (fields.Contains(fdef))
				fields.Remove(fdef);
		}

		public static void Delete(EventDefinition edef)
		{
			if (edef.DeclaringType == null)
				return;

			var events = edef.DeclaringType.Events;
			if (!events.Contains(edef))
				return;

			if (edef.AddMethod != null)
				Delete(edef.AddMethod);

			if (edef.RemoveMethod != null)
				Delete(edef.RemoveMethod);

			events.Remove(edef);
		}

		public static void Delete(Resource resource)
		{
			var plugin = PluginFactory.GetInstance();
			ModuleDefinition moddef = null;

			foreach (var wrapper in plugin.Package.HostAssemblies)
			{
				if (wrapper.IsValid)
				{
					if (plugin.IsAssemblyContextLoaded(wrapper.Location))
					{
						var context = plugin.GetAssemblyContext(wrapper.Location);
						moddef = context.AssemblyDefinition.Modules.FirstOrDefault(
							imoddef => imoddef.Resources.Contains(resource));
					}
				}

				if (moddef != null)
					moddef.Resources.Remove(resource);
			}
		}

		public static void Delete(object obj)
		{
			// ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
			if (obj is TypeDefinition)
				Delete((TypeDefinition) obj);
			else if (obj is MethodDefinition)
				Delete((MethodDefinition) obj);
			else if (obj is PropertyDefinition)
				Delete((PropertyDefinition) obj);
			else if (obj is FieldDefinition)
				Delete((FieldDefinition) obj);
			else if (obj is EventDefinition)
				Delete((EventDefinition) obj);
			else if (obj is AssemblyNameReference)
				Delete((AssemblyNameReference) obj);
			else if (obj is Resource)
				Delete((Resource) obj);
		}
	}
}