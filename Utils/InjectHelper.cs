/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Helper for injecting new items
    /// </summary>
	public static class InjectHelper
    {

        #region " Methods "
        /// <summary>
        /// Inject an assembly name reference to an assembly main module
        /// </summary>
        /// <param name="adef">Assembly definition</param>
        /// <param name="name">The name of the referenced assembly</param>
        /// <returns>the new ssembly reference</returns>
        public static AssemblyNameReference InjectAssemblyNameReference(AssemblyDefinition adef, string name)
        {
            AssemblyNameReference anref = new AssemblyNameReference(name, new Version());
            adef.MainModule.AssemblyReferences.Add(anref);
            return anref;
        }

        /// <summary>
        /// Inject an enum definition to a module definition
        /// </summary>
        /// <param name="mdef">Module definition</param>
        /// <param name="name">Enum name</param>
        /// <returns>the new enum with related value__ field</returns>
        public static TypeDefinition InjectEnumDefinition(ModuleDefinition mdef, string name)
        {
            var edef = InjectTypeDefinition(mdef, name, mdef.Import(typeof(System.Enum)));
            edef.IsSealed = true;
            var fdef = InjectFieldDefinition(edef, "value__", edef.Module.Import(typeof(int)));
            fdef.IsRuntimeSpecialName = true;
            fdef.IsSpecialName = true;
            return edef;
        }

        /// <summary>
        /// Inject a struct definition to a module definition
        /// </summary>
        /// <param name="mdef">Module definition</param>
        /// <param name="name">Struct name</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectStructDefinition(ModuleDefinition mdef, string name)
        {
            var sdef = InjectTypeDefinition(mdef, name, mdef.Import(typeof(System.ValueType)));
            sdef.IsSealed = true;
            return sdef;
        }

        /// <summary>
        /// Inject a class definition to a module definition
        /// </summary>
        /// <param name="mdef">Module definition</param>
        /// <param name="name">Class name</param>
        /// <param name="baseType">Class base type</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectClassDefinition(ModuleDefinition mdef, string name, TypeReference baseType)
        {
            return InjectTypeDefinition(mdef, name, baseType);
        }

        /// <summary>
        /// Inject an interface definition to a module definition
        /// </summary>
        /// <param name="mdef">Module definition</param>
        /// <param name="name">Interface name</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectInterfaceDefinition(ModuleDefinition mdef, string name)
        {
            var idef = InjectTypeDefinition(mdef, name, null);
            idef.IsInterface = true;
            idef.IsAbstract = true;
            return idef;
        }

        /// <summary>
        /// Inject an enum definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Class name</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectInnerEnumDefinition(TypeDefinition tdef, string name)
        {
            var edef = InjectInnerTypeDefinition(tdef, name, tdef.Module.Import(typeof(System.Enum)));
            edef.IsSealed = true;
            var fdef = InjectFieldDefinition(edef, "value__", edef.Module.Import(typeof(int)));
            fdef.IsRuntimeSpecialName = true;
            fdef.IsSpecialName = true;
            return edef;
        }

        /// <summary>
        /// Inject a struct definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Struct name</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectInnerStructDefinition(TypeDefinition tdef, string name)
        {
            var sdef = InjectInnerTypeDefinition(tdef, name, tdef.Module.Import(typeof(System.ValueType)));
            sdef.IsSealed = true;
            return sdef;
        }

        /// <summary>
        /// Inject a type definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Type name</param>
        /// <param name="baseType">Class base type</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectInnerClassDefinition(TypeDefinition tdef, string name, TypeReference baseType)
        {
            return InjectInnerTypeDefinition(tdef, name, baseType);
        }

        /// <summary>
        /// Inject an Interface definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Interface name</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectInnerInterfaceDefinition(TypeDefinition tdef, string name)
        {
            var idef = InjectInnerTypeDefinition(tdef, name, null);
            idef.IsInterface = true;
            idef.IsAbstract = true;
            return idef;
        }

        /// <summary>
        /// Inject a type definition to a module definition
        /// </summary>
        /// <param name="mdef">Module definition</param>
        /// <param name="name">Type name</param>
        /// <param name="baseType">Type base type</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectTypeDefinition(ModuleDefinition mdef, string name, TypeReference baseType)
        {
            // Full namespace
            string ns = string.Empty;
            if (name.Contains("."))
            {
                int offset = name.LastIndexOf(".");
                ns = name.Substring(0, offset);
                name = name.Substring(offset + 1);
            }

            TypeDefinition tdef = new TypeDefinition(ns, name, TypeAttributes.Public, baseType);
            mdef.Types.Add(tdef);
            return tdef;
        }

        /// <summary>
        /// Inject a type definition to a type definition
        /// </summary>
        /// <param name="mdef">Type definition</param>
        /// <param name="name">Type name</param>
        /// <param name="baseType">Type base type</param>
        /// <returns>the new TypeDefinition</returns>
        public static TypeDefinition InjectInnerTypeDefinition(TypeDefinition tdef, string name, TypeReference baseType)
        {
            // Classname only
            string ns = string.Empty;
            if (name.Contains("."))
            {
                int offset = name.LastIndexOf(".");
                name = name.Substring(offset + 1);
            }

            TypeDefinition itdef = new TypeDefinition(ns, name, TypeAttributes.NestedPublic, baseType);
            tdef.NestedTypes.Add(itdef);
            tdef.Module.Types.Add(itdef);

            return itdef;
        }

        /// <summary>
        /// Inject a method definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Method name</param>
        /// <returns>the new method definition</returns>
        public static MethodDefinition InjectMethodDefinition(TypeDefinition tdef, string name)
        {
            MethodDefinition mdef = new MethodDefinition(name, MethodAttributes.Public, tdef.Module.Import(typeof(void)));
            tdef.Methods.Add(mdef);

            if (!tdef.IsInterface)
            {
                ILProcessor worker = mdef.Body.GetILProcessor();
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

        /// <summary>
        /// Inject a constructor definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <returns>the new method definition</returns>
        public static MethodDefinition InjectConstructorDefinition(TypeDefinition tdef)
        {
            MethodDefinition cdef = new MethodDefinition(".ctor", MethodAttributes.Public, tdef.Module.Import(typeof(void)));
            tdef.Methods.Add(cdef);

            ILProcessor worker = cdef.Body.GetILProcessor();
            if (tdef.BaseType != null)
            {
                MethodReference mref = GetDefaultConstructor(tdef.Module, tdef.BaseType);
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
            return cdef;
        }

        /// <summary>
        /// Inject a property getter to a property
        /// </summary>
        /// <param name="pdef">Property definition</param>
        /// <param name="fdef">Field definition (for reading property value)</param>
        /// <returns>the new method definition</returns>
        public static MethodDefinition InjectPropertyGetter(PropertyDefinition pdef, FieldDefinition fdef)
        {
            MethodDefinition get = new MethodDefinition(string.Concat("get_", pdef.Name), MethodAttributes.Public, pdef.PropertyType);
            pdef.GetMethod = get;
            pdef.DeclaringType.Methods.Add(get);

            if (!pdef.DeclaringType.IsInterface)
            {
                ILProcessor worker = get.Body.GetILProcessor();
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

        /// <summary>
        /// Inject a property setter to a property
        /// </summary>
        /// <param name="pdef">Property definition</param>
        /// <param name="fdef">Field definition (for setting property value)</param>
        /// <returns>the new method definition</returns>
        public static MethodDefinition InjectPropertySetter(PropertyDefinition pdef, FieldDefinition fdef)
        {
            MethodDefinition set = new MethodDefinition(string.Concat("set_", pdef.Name), MethodAttributes.Public, pdef.DeclaringType.Module.Import(typeof(void)));
            set.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, pdef.PropertyType));
            pdef.SetMethod = set;
            pdef.DeclaringType.Methods.Add(set);

            if (!pdef.DeclaringType.IsInterface)
            {
                ILProcessor worker = set.Body.GetILProcessor();
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

        /// <summary>
        /// Inject a property definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Property name</param>
        /// <param name="propertyType">Property type</param>
        /// <returns>the new property definition</returns>
        public static PropertyDefinition InjectPropertyDefinition(TypeDefinition tdef, string name, TypeReference propertyType)
        {
            PropertyDefinition pdef = new PropertyDefinition(name, (PropertyAttributes)0, propertyType);
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

        /// <summary>
        /// Inject a new field definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Field name</param>
        /// <param name="fieldType">Field type</param>
        /// <param name="attributes">Field attributes</param>
        /// <returns>the new field definition</returns>
        public static FieldDefinition InjectFieldDefinition(TypeDefinition tdef, string name, TypeReference fieldType, FieldAttributes attributes)
        {
            FieldDefinition fdef = new FieldDefinition(name, attributes, fieldType);
            tdef.Fields.Add(fdef);
            return fdef;
        }

        /// <summary>
        /// Inject a new field definition to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Field name</param>
        /// <param name="fieldType">Field type</param>
        /// <returns>the new field definition</returns>
        public static FieldDefinition InjectFieldDefinition(TypeDefinition tdef, string name, TypeReference fieldType)
        {
            return InjectFieldDefinition(tdef, name, fieldType, FieldAttributes.Public);
        }

        /// <summary>
        /// Retrieve type reference default constructor 
        /// </summary>
        /// <param name="modef">Module definition</param>
        /// <param name="tref">Type reference</param>
        /// <returns>the default constructor</returns>
        private static MethodReference GetDefaultConstructor(ModuleDefinition modef, TypeReference tref)
        {
            TypeDefinition tdef = tref.Resolve();
            if (tdef != null)
            {
                foreach (MethodDefinition mdef in tdef.Methods)
                {
                    if (mdef.IsConstructor && mdef.Parameters.Count == 0)
                    {
                        return modef.Import(mdef);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieve a 'delegate' method from a module definition
        /// </summary>
        /// <param name="modef">Module definition</param>
        /// <param name="name">'delegate' method name</param>
        /// <returns>method reference</returns>
        private static MethodReference GetDelegateMethod(ModuleDefinition modef, string name)
        {
            TypeReference tref = modef.Import(typeof(Delegate));
            TypeDefinition tdef = tref.Resolve();
            if (tdef != null)
            {
                foreach (MethodDefinition mdef in tdef.Methods)
                {
                    if (mdef.Name.Equals(name) && mdef.Parameters.Count == 2)
                    {
                        return modef.Import(mdef);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Inject 'add' method to an event definition
        /// </summary>
        /// <param name="edef">Event definition</param>
        /// <param name="fdef">Field definition</param>
        /// <returns>method definition</returns>
        public static MethodDefinition InjectEventAdder(EventDefinition edef, FieldReference fdef)
        {
            MethodDefinition add = new MethodDefinition(string.Concat("add_", edef.Name), MethodAttributes.Public, edef.DeclaringType.Module.Import(typeof(void)));
            add.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, edef.EventType));
            edef.AddMethod = add;
            edef.DeclaringType.Methods.Add(add);

            if (!edef.DeclaringType.IsInterface)
            {
                ILProcessor worker = add.Body.GetILProcessor();

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

        /// <summary>
        /// Inject 'remove' method to an event definition
        /// </summary>
        /// <param name="edef">Event definition</param>
        /// <param name="fdef">Field definition</param>
        /// <returns>method definition</returns>
        public static MethodDefinition InjectEventRemover(EventDefinition edef, FieldDefinition fdef)
        {
            MethodDefinition remove = new MethodDefinition(string.Concat("remove_", edef.Name), MethodAttributes.Public, edef.DeclaringType.Module.Import(typeof(void)));
            remove.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, edef.EventType));
            edef.RemoveMethod = remove;
            edef.DeclaringType.Methods.Add(remove);

            if (!edef.DeclaringType.IsInterface)
            {
                ILProcessor worker = remove.Body.GetILProcessor();
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

        /// <summary>
        /// Inject an event to a type definition
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">Event name</param>
        /// <param name="eventType">Event type</param>
        /// <returns>event definition</returns>
        public static EventDefinition InjectEventDefinition(TypeDefinition tdef, string name, TypeReference eventType)
        {
            EventDefinition edef = new EventDefinition(name, (EventAttributes)0, eventType);
            tdef.Events.Add(edef);

            FieldDefinition fdef = null;
            if (!tdef.IsInterface)
            {
                fdef = InjectFieldDefinition(edef.DeclaringType, string.Concat("__Reflexil_", edef.Name), edef.EventType, FieldAttributes.Private);
            }
            edef.AddMethod = InjectEventAdder(edef, fdef);
            edef.RemoveMethod = InjectEventRemover(edef, fdef);
            return edef;
        }

        /// <summary>
        /// Inject a resource to a module definition
        /// </summary>
        /// <param name="mdef">Module definition</param>
        /// <param name="name">Resource name</param>
        /// <param name="resourceType">Resource type</param>
        /// <returns>resource</returns>
        public static Resource InjectResource(ModuleDefinition mdef, string name, ResourceType resourceType)
        {
            Resource result=null;

            switch(resourceType)
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

        /// <summary>
        /// Inject an item definition into an owner
        /// </summary>
        /// <param name="owner">Owner item</param>
        /// <param name="targettype">Target type definition</param>
        /// <param name="name">name for the newly created item</param>
        /// <returns>Object definition</returns>
        public static object Inject(object owner, EInjectType targettype, string name, object extratype)
        {
            if (owner != null && name != null)
            {
                if (owner is AssemblyDefinition)
                {
                    AssemblyDefinition adef = owner as AssemblyDefinition;
                    switch (targettype)
                    {
                        case EInjectType.AssemblyReference:
                            return InjectHelper.InjectAssemblyNameReference(adef, name);
                        case EInjectType.Type:
                            return InjectHelper.InjectTypeDefinition(adef.MainModule, name, adef.MainModule.Import(extratype as TypeReference));
                        case EInjectType.Class:
                            return InjectHelper.InjectClassDefinition(adef.MainModule, name, adef.MainModule.Import(extratype as TypeReference));
                        case EInjectType.Interface:
                            return InjectHelper.InjectInterfaceDefinition(adef.MainModule, name);
                        case EInjectType.Struct:
                            return InjectHelper.InjectStructDefinition(adef.MainModule, name);
                        case EInjectType.Enum:
                            return InjectHelper.InjectEnumDefinition(adef.MainModule, name);
                        case EInjectType.Resource:
                            return InjectHelper.InjectResource(adef.MainModule, name, (ResourceType) extratype);
                    }
                }
                else if (owner is TypeDefinition)
                {
                    TypeDefinition tdef = owner as TypeDefinition;

                    switch (targettype)
                    {
                        case EInjectType.Type:
                            return InjectHelper.InjectInnerTypeDefinition(tdef, name, tdef.Module.Import(extratype as TypeReference));
                        case EInjectType.Class:
                            return InjectHelper.InjectInnerClassDefinition(tdef, name, tdef.Module.Import(extratype as TypeReference));
                        case EInjectType.Interface:
                            return InjectHelper.InjectInnerInterfaceDefinition(tdef, name);
                        case EInjectType.Struct:
                            return InjectHelper.InjectInnerStructDefinition(tdef, name);
                        case EInjectType.Enum:
                            return InjectHelper.InjectInnerEnumDefinition(tdef, name);
                        case EInjectType.Constructor:
                            return InjectHelper.InjectConstructorDefinition(tdef);
                        case EInjectType.Method:
                            return InjectHelper.InjectMethodDefinition(tdef, name);
                        case EInjectType.Property:
                            return InjectHelper.InjectPropertyDefinition(tdef, name, tdef.Module.Import(extratype as TypeReference));
                        case EInjectType.Field:
                            return InjectHelper.InjectFieldDefinition(tdef, name, tdef.Module.Import(extratype as TypeReference));
                        case EInjectType.Event:
                            return InjectHelper.InjectEventDefinition(tdef, name, tdef.Module.Import(extratype as TypeReference));
                    }
                }

                throw new NotImplementedException();
            }
            else
            {
                throw new ArgumentException();
            }
        }
        #endregion

    }
}
