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

#region Imports

using System;
using System.Linq;
using Mono.Cecil;
using Reflexil.Plugins;
using Reflexil.Wrappers;

#endregion

namespace Reflexil.Utils
{
	/// <summary>
	/// Helper for deleting existing items
	/// </summary>
	public static class DeleteHelper
	{
		#region Methods

		/// <summary>
		/// Remove an assembly name reference
		/// </summary>
		/// <param name="anref">Assembly name reference</param>
		public static void Delete(AssemblyNameReference anref)
		{
			var plugin = PluginFactory.GetInstance();

			foreach (IAssemblyWrapper wrapper in plugin.Package.HostAssemblies)
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

		/// <summary>
		/// Remove a type definition
		/// </summary>
		/// <param name="tdef">Nested or flat type definition</param>
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

		/// <summary>
		/// Remove a method definition
		/// </summary>
		/// <param name="mdef">Constructor or standard method definition</param>
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

		/// <summary>
		/// Remove a property definition and getter/setter method(s)
		/// </summary>
		/// <param name="pdef">Property definition</param>
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

		/// <summary>
		/// Remove a field definition
		/// </summary>
		/// <param name="fdef">Field definition</param>
		public static void Delete(FieldDefinition fdef)
		{
			if (fdef.DeclaringType == null)
				return;

			var fields = fdef.DeclaringType.Fields;
			if (fields.Contains(fdef))
				fields.Remove(fdef);
		}

		/// <summary>
		/// Remove an event definition and add/remove methods
		/// </summary>
		/// <param name="edef">Event definition</param>
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

		/// <summary>
		/// Remove a resource
		/// </summary>
		/// <param name="resource">Resource</param>
		public static void Delete(Resource resource)
		{
			var plugin = PluginFactory.GetInstance();
			ModuleDefinition moddef = null;

			foreach (IAssemblyWrapper wrapper in plugin.Package.HostAssemblies)
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


		/// <summary>
		/// Remove an object
		/// </summary>
		/// <param name="obj">Type/Method/Property/Field/Event definition/Assembly Reference</param>
		public static void Delete(Object obj)
		{
			if (obj is TypeDefinition)
				Delete(obj as TypeDefinition);
			else if (obj is MethodDefinition)
				Delete(obj as MethodDefinition);
			else if (obj is PropertyDefinition)
				Delete(obj as PropertyDefinition);
			else if (obj is FieldDefinition)
				Delete(obj as FieldDefinition);
			else if (obj is EventDefinition)
				Delete(obj as EventDefinition);
			else if (obj is AssemblyNameReference)
				Delete(obj as AssemblyNameReference);
			else if (obj is Resource)
				Delete(obj as Resource);
		}

		#endregion
	}
}