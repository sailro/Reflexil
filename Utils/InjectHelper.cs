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

using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Reflexil.Utils
{
	public static class InjectHelper
	{
		public static AssemblyNameReference InjectAssemblyNameReference(AssemblyDefinition adef, string name)
		{
			var anref = new AssemblyNameReference(name, new Version(0, 0, 0, 0));
			adef.MainModule.AssemblyReferences.Add(anref);
			return anref;
		}

		public static TypeDefinition InjectEnumDefinition(ModuleDefinition mdef, string name)
		{
			var edef = InjectTypeDefinition(mdef, name, mdef.TypeSystem.Enum);
			edef.IsSealed = true;
			var fdef = InjectFieldDefinition(edef, "value__", edef.Module.TypeSystem.Int32);
			fdef.IsRuntimeSpecialName = true;
			fdef.IsSpecialName = true;
			return edef;
		}

		public static TypeDefinition InjectStructDefinition(ModuleDefinition mdef, string name)
		{
			var sdef = InjectTypeDefinition(mdef, name, mdef.TypeSystem.ValueType);
			sdef.IsSealed = true;
			return sdef;
		}

		public static TypeDefinition InjectClassDefinition(ModuleDefinition mdef, string name, TypeReference baseType)
		{
			return InjectTypeDefinition(mdef, name, baseType);
		}

		public static TypeDefinition InjectInterfaceDefinition(ModuleDefinition mdef, string name)
		{
			var idef = InjectTypeDefinition(mdef, name, null);
			idef.IsInterface = true;
			idef.IsAbstract = true;
			return idef;
		}

		public static TypeDefinition InjectInnerEnumDefinition(TypeDefinition tdef, string name)
		{
			var edef = InjectInnerTypeDefinition(tdef, name, tdef.Module.TypeSystem.Enum);
			edef.IsSealed = true;
			var fdef = InjectFieldDefinition(edef, "value__", edef.Module.TypeSystem.Int32);
			fdef.IsRuntimeSpecialName = true;
			fdef.IsSpecialName = true;
			return edef;
		}

		public static TypeDefinition InjectInnerStructDefinition(TypeDefinition tdef, string name)
		{
			var sdef = InjectInnerTypeDefinition(tdef, name, tdef.Module.TypeSystem.ValueType);
			sdef.IsSealed = true;
			return sdef;
		}

		public static TypeDefinition InjectInnerClassDefinition(TypeDefinition tdef, string name, TypeReference baseType)
		{
			return InjectInnerTypeDefinition(tdef, name, baseType);
		}

		public static TypeDefinition InjectInnerInterfaceDefinition(TypeDefinition tdef, string name)
		{
			var idef = InjectInnerTypeDefinition(tdef, name, null);
			idef.IsInterface = true;
			idef.IsAbstract = true;
			return idef;
		}

		public static TypeDefinition InjectTypeDefinition(ModuleDefinition mdef, string name, TypeReference baseType)
		{
			// Full namespace
			var ns = string.Empty;
			if (name.Contains("."))
			{
				var offset = name.LastIndexOf(".", StringComparison.Ordinal);
				ns = name.Substring(0, offset);
				name = name.Substring(offset + 1);
			}

			var tdef = new TypeDefinition(ns, name, TypeAttributes.Public, baseType);
			mdef.Types.Add(tdef);
			return tdef;
		}

		public static TypeDefinition InjectInnerTypeDefinition(TypeDefinition tdef, string name, TypeReference baseType)
		{
			// Classname only
			var ns = string.Empty;
			if (name.Contains("."))
			{
				var offset = name.LastIndexOf(".", StringComparison.Ordinal);
				name = name.Substring(offset + 1);
			}

			var itdef = new TypeDefinition(ns, name, TypeAttributes.NestedPublic, baseType);
			tdef.NestedTypes.Add(itdef);

			return itdef;
		}

		public static MethodDefinition InjectMethodDefinition(TypeDefinition tdef, string name)
		{
			var mdef = new MethodDefinition(name, MethodAttributes.Public, tdef.Module.TypeSystem.Void);
			tdef.Methods.Add(mdef);

			if (!tdef.IsInterface)
			{
				var worker = mdef.Body.GetILProcessor();
				worker.Emit(OpCodes.Ret);
			}
			else
			{
				mdef.IsAbstract = true;
				mdef.IsVirtual = true;
				mdef.IsNewSlot = true;
			}

			mdef.IsHideBySig = true;

			return mdef;
		}

		public static MethodDefinition InjectConstructorDefinition(TypeDefinition tdef)
		{
			var cdef = new MethodDefinition(".ctor", MethodAttributes.Public, tdef.Module.TypeSystem.Void);

			var worker = cdef.Body.GetILProcessor();
			if (tdef.BaseType != null)
			{
				var mref = GetDefaultConstructor(tdef.Module, tdef.BaseType);
				if (mref != null)
				{
					worker.Emit(OpCodes.Ldarg_0);
					worker.Emit(OpCodes.Call, mref);
				}
			}
			worker.Emit(OpCodes.Ret);

			cdef.IsHideBySig = true;
			cdef.IsRuntimeSpecialName = true;
			cdef.IsSpecialName = true;

			// Only add at the end, because resolving base ctor can fail
			tdef.Methods.Add(cdef);

			return cdef;
		}

		public static MethodDefinition InjectPropertyGetter(PropertyDefinition pdef, FieldDefinition fdef)
		{
			var get = new MethodDefinition(string.Concat("get_", pdef.Name), MethodAttributes.Public, pdef.PropertyType);
			pdef.GetMethod = get;
			pdef.DeclaringType.Methods.Add(get);

			if (!pdef.DeclaringType.IsInterface)
			{
				var worker = get.Body.GetILProcessor();
				worker.Emit(OpCodes.Ldarg_0);
				worker.Emit(OpCodes.Ldfld, fdef);
				worker.Emit(OpCodes.Ret);
			}
			else
			{
				get.IsAbstract = true;
				get.IsVirtual = true;
				get.IsNewSlot = true;
			}

			get.IsHideBySig = true;
			get.IsSpecialName = true;
			get.IsGetter = true;

			return get;
		}

		public static MethodDefinition InjectPropertySetter(PropertyDefinition pdef, FieldDefinition fdef)
		{
			var set = new MethodDefinition(string.Concat("set_", pdef.Name), MethodAttributes.Public, pdef.DeclaringType.Module.TypeSystem.Void);
			set.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, pdef.PropertyType));
			pdef.SetMethod = set;
			pdef.DeclaringType.Methods.Add(set);

			if (!pdef.DeclaringType.IsInterface)
			{
				var worker = set.Body.GetILProcessor();
				worker.Emit(OpCodes.Ldarg_0);
				worker.Emit(OpCodes.Ldarg_1);
				worker.Emit(OpCodes.Stfld, fdef);
				worker.Emit(OpCodes.Ret);
			}
			else
			{
				set.IsAbstract = true;
				set.IsVirtual = true;
				set.IsNewSlot = true;
			}

			set.IsHideBySig = true;
			set.IsSpecialName = true;
			set.IsSetter = true;

			return set;
		}

		public static PropertyDefinition InjectPropertyDefinition(TypeDefinition tdef, string name, TypeReference propertyType)
		{
			var pdef = new PropertyDefinition(name, 0, propertyType);
			tdef.Properties.Add(pdef);

			FieldDefinition fdef = null;
			if (!tdef.IsInterface)
			{
				fdef = InjectFieldDefinition(pdef.DeclaringType, string.Concat("__Reflexil_", pdef.Name), pdef.PropertyType, FieldAttributes.Private);
			}

			pdef.GetMethod = InjectPropertyGetter(pdef, fdef);
			pdef.SetMethod = InjectPropertySetter(pdef, fdef);
			return pdef;
		}

		public static FieldDefinition InjectFieldDefinition(TypeDefinition tdef, string name, TypeReference fieldType, FieldAttributes attributes)
		{
			var fdef = new FieldDefinition(name, attributes, fieldType);
			tdef.Fields.Add(fdef);
			return fdef;
		}

		public static FieldDefinition InjectFieldDefinition(TypeDefinition tdef, string name, TypeReference fieldType)
		{
			return InjectFieldDefinition(tdef, name, fieldType, FieldAttributes.Public);
		}

		private static MethodReference GetDefaultConstructor(ModuleDefinition modef, TypeReference tref)
		{
			var tdef = tref.Resolve();

			if (tdef == null)
				return null;

			var ctor = tdef.Methods.FirstOrDefault(mdef => mdef.IsConstructor && mdef.Parameters.Count == 0);
			return ctor == null ? null : CecilImporter.Import(modef, ctor);
		}

		private static MethodReference GetDelegateMethod(ModuleDefinition modef, string name)
		{
			var tref = modef.TypeSystem.Delegate;
			var tdef = tref.Resolve();
			if (tdef == null)
				return null;

			return tdef.Methods.Where(mdef => mdef.Name.Equals(name) && mdef.Parameters.Count == 2).Select(m => CecilImporter.Import(modef, m)).FirstOrDefault();
		}

		public static MethodDefinition InjectEventAdder(EventDefinition edef, FieldReference fdef)
		{
			var add = new MethodDefinition(string.Concat("add_", edef.Name), MethodAttributes.Public, edef.DeclaringType.Module.TypeSystem.Void);
			add.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, edef.EventType));
			edef.AddMethod = add;
			edef.DeclaringType.Methods.Add(add);

			if (!edef.DeclaringType.IsInterface)
			{
				var worker = add.Body.GetILProcessor();

				worker.Emit(OpCodes.Ldarg_0);
				worker.Emit(OpCodes.Ldarg_0);
				worker.Emit(OpCodes.Ldfld, fdef);
				worker.Emit(OpCodes.Ldarg_1);
				worker.Emit(OpCodes.Call, GetDelegateMethod(edef.DeclaringType.Module, "Combine"));
				worker.Emit(OpCodes.Castclass, edef.EventType);
				worker.Emit(OpCodes.Stfld, fdef);
				worker.Emit(OpCodes.Ret);
			}
			else
			{
				add.IsAbstract = true;
				add.IsVirtual = true;
				add.IsNewSlot = true;
			}

			add.IsSynchronized = true;
			add.IsHideBySig = true;
			add.IsSpecialName = true;

			return add;
		}

		public static MethodDefinition InjectEventRemover(EventDefinition edef, FieldDefinition fdef)
		{
			var remove = new MethodDefinition(string.Concat("remove_", edef.Name), MethodAttributes.Public, edef.DeclaringType.Module.TypeSystem.Void);
			remove.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, edef.EventType));
			edef.RemoveMethod = remove;
			edef.DeclaringType.Methods.Add(remove);

			if (!edef.DeclaringType.IsInterface)
			{
				var worker = remove.Body.GetILProcessor();
				worker.Emit(OpCodes.Ldarg_0);
				worker.Emit(OpCodes.Ldarg_0);
				worker.Emit(OpCodes.Ldfld, fdef);
				worker.Emit(OpCodes.Ldarg_1);
				worker.Emit(OpCodes.Call, GetDelegateMethod(edef.DeclaringType.Module, "Remove"));
				worker.Emit(OpCodes.Castclass, edef.EventType);
				worker.Emit(OpCodes.Stfld, fdef);
				worker.Emit(OpCodes.Ret);
			}
			else
			{
				remove.IsAbstract = true;
				remove.IsVirtual = true;
				remove.IsNewSlot = true;
			}

			remove.IsSynchronized = true;
			remove.IsHideBySig = true;
			remove.IsSpecialName = true;

			return remove;
		}

		public static EventDefinition InjectEventDefinition(TypeDefinition tdef, string name, TypeReference eventType)
		{
			var edef = new EventDefinition(name, 0, eventType);
			tdef.Events.Add(edef);

			FieldDefinition fdef = null;
			if (!tdef.IsInterface)
			{
				fdef = InjectFieldDefinition(edef.DeclaringType, string.Concat("__Reflexil_", edef.Name), edef.EventType,
					FieldAttributes.Private);
			}
			edef.AddMethod = InjectEventAdder(edef, fdef);
			edef.RemoveMethod = InjectEventRemover(edef, fdef);
			return edef;
		}

		public static Resource InjectResource(ModuleDefinition mdef, string name, ResourceType resourceType)
		{
			Resource result;

			switch (resourceType)
			{
				case ResourceType.AssemblyLinked:
					var anref = new AssemblyNameReference(name, new Version());
					mdef.AssemblyReferences.Add(anref);

					result = new AssemblyLinkedResource(name, ManifestResourceAttributes.Public, anref);
					break;
				case ResourceType.Embedded:
					result = new EmbeddedResource(name, ManifestResourceAttributes.Public, new byte[] {});
					break;
				case ResourceType.Linked:
					result = new LinkedResource(name, ManifestResourceAttributes.Public);
					break;
				default:
					throw new ArgumentException();
			}

			mdef.Resources.Add(result);
			return result;
		}

		public static object Inject(object owner, InjectType targettype, string name, object extratype)
		{
			if (owner == null || name == null)
				throw new ArgumentException();

			var mdef = owner as AssemblyDefinition;
			if (mdef != null)
			{
				var adef = mdef;
				switch (targettype)
				{
					case InjectType.AssemblyReference:
						return InjectAssemblyNameReference(adef, name);
					case InjectType.Type:
						return InjectTypeDefinition(adef.MainModule, name, CecilImporter.Import(adef.MainModule, extratype as TypeReference));
					case InjectType.Class:
						return InjectClassDefinition(adef.MainModule, name, CecilImporter.Import(adef.MainModule, extratype as TypeReference));
					case InjectType.Interface:
						return InjectInterfaceDefinition(adef.MainModule, name);
					case InjectType.Struct:
						return InjectStructDefinition(adef.MainModule, name);
					case InjectType.Enum:
						return InjectEnumDefinition(adef.MainModule, name);
					case InjectType.Resource:
						return InjectResource(adef.MainModule, name, (ResourceType) extratype);
				}
			}
			else if (owner is TypeDefinition)
			{
				var tdef = (TypeDefinition) owner;

				switch (targettype)
				{
					case InjectType.Type:
						return InjectInnerTypeDefinition(tdef, name, CecilImporter.Import(tdef.Module, extratype as TypeReference));
					case InjectType.Class:
						return InjectInnerClassDefinition(tdef, name, CecilImporter.Import(tdef.Module, extratype as TypeReference));
					case InjectType.Interface:
						return InjectInnerInterfaceDefinition(tdef, name);
					case InjectType.Struct:
						return InjectInnerStructDefinition(tdef, name);
					case InjectType.Enum:
						return InjectInnerEnumDefinition(tdef, name);
					case InjectType.Constructor:
						return InjectConstructorDefinition(tdef);
					case InjectType.Method:
						return InjectMethodDefinition(tdef, name);
					case InjectType.Property:
						return InjectPropertyDefinition(tdef, name, CecilImporter.Import(tdef.Module, extratype as TypeReference));
					case InjectType.Field:
						return InjectFieldDefinition(tdef, name, CecilImporter.Import(tdef.Module, extratype as TypeReference));
					case InjectType.Event:
						return InjectEventDefinition(tdef, name, CecilImporter.Import(tdef.Module, extratype as TypeReference));
				}
			}

			throw new NotImplementedException();
		}
	}
}