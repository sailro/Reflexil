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

        #region " Methods "
        /// <summary>
        /// Remove an assembly name reference
        /// </summary>
        /// <param name="anref">Assembly name reference</param>
        public static void Delete(AssemblyNameReference anref)
        {
            IPlugin plugin = PluginFactory.GetInstance();
            ModuleDefinition moddef = null;

            foreach (IAssemblyWrapper wrapper in plugin.GetAssemblies(true))
            {
                if (wrapper.IsValid)
                {
                    if (plugin.IsAssemblyContextLoaded(wrapper.Location))
                    {
                        IAssemblyContext context = plugin.GetAssemblyContext(wrapper.Location);
                        foreach (ModuleDefinition imoddef in context.AssemblyDefinition.Modules)
                        {
                            foreach (AssemblyNameReference ianref in imoddef.AssemblyReferences)
                            {
                                if (anref == ianref)
                                {
                                    moddef = imoddef;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (moddef != null)
            {
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
                Mono.Collections.Generic.Collection<TypeDefinition> coll = tdef.DeclaringType.NestedTypes;
                if (coll.Contains(tdef))
                {
                    coll.Remove(tdef);
                }
            }
            if (tdef.Module != null)
            {
                Mono.Collections.Generic.Collection<TypeDefinition> coll = tdef.Module.Types;
                if (coll.Contains(tdef))
                {
                    coll.Remove(tdef);
                }
            }
        }

        /// <summary>
        /// Remove a method definition
        /// </summary>
        /// <param name="mdef">Constructor or standard method definition</param>
        public static void Delete(MethodDefinition mdef)
        {
            if (mdef.DeclaringType != null)
            {
                if (mdef.DeclaringType.Methods.Contains(mdef))
                {
                    mdef.DeclaringType.Methods.Remove(mdef);
                }
            }
        }

        /// <summary>
        /// Remove a property definition and getter/setter method(s)
        /// </summary>
        /// <param name="pdef">Property definition</param>
        public static void Delete(PropertyDefinition pdef)
        {
            if (pdef.DeclaringType != null)
            {
                Mono.Collections.Generic.Collection<PropertyDefinition> coll = pdef.DeclaringType.Properties;
                if (coll.Contains(pdef))
                {
                    if (pdef.GetMethod != null)
                    {
                        Delete(pdef.GetMethod);
                    }
                    if (pdef.SetMethod != null)
                    {
                        Delete(pdef.SetMethod);
                    }

                    coll.Remove(pdef);
                }
            }
        }

        /// <summary>
        /// Remove a field definition
        /// </summary>
        /// <param name="fdef">Field definition</param>
        public static void Delete(FieldDefinition fdef)
        {
            if (fdef.DeclaringType != null)
            {
                Mono.Collections.Generic.Collection<FieldDefinition> coll = fdef.DeclaringType.Fields;
                if (coll.Contains(fdef))
                {
                    coll.Remove(fdef);
                }
            }
        }

        /// <summary>
        /// Remove an event definition and add/remove methods
        /// </summary>
        /// <param name="edef">Event definition</param>
        public static void Delete(EventDefinition edef)
        {
            if (edef.DeclaringType != null)
            {
                Mono.Collections.Generic.Collection<EventDefinition> coll = edef.DeclaringType.Events;
                if (coll.Contains(edef))
                {
                    if (edef.AddMethod != null)
                    {
                        Delete(edef.AddMethod);
                    }
                    if (edef.RemoveMethod != null)
                    {
                        Delete(edef.RemoveMethod);
                    }

                    coll.Remove(edef);
                }
            }
        }

        /// <summary>
        /// Remove a resource
        /// </summary>
        /// <param name="resource">Resource</param>
        public static void Delete(Resource resource)
        {
            IPlugin plugin = PluginFactory.GetInstance();
            ModuleDefinition moddef = null;

            foreach (IAssemblyWrapper wrapper in plugin.GetAssemblies(true))
            {
                if (wrapper.IsValid)
                {
                    if (plugin.IsAssemblyContextLoaded(wrapper.Location))
                    {
                        IAssemblyContext context = plugin.GetAssemblyContext(wrapper.Location);
                        foreach (ModuleDefinition imoddef in context.AssemblyDefinition.Modules)
                        {
                            if (imoddef.Resources.Contains(resource))
                            {
                                moddef = imoddef;
                                break;
                            }
                        }
                    }
                }

                if (moddef != null)
                {
                    moddef.Resources.Remove(resource);
                }
            }
        }


        /// <summary>
        /// Remove an object
        /// </summary>
        /// <param name="obj">Type/Method/Property/Field/Event definition/Assembly Reference</param>
        public static void Delete(Object obj)
        {
            if (obj is TypeDefinition)
            {
                Delete(obj as TypeDefinition);
            }
            else if (obj is MethodDefinition)
            {
                Delete(obj as MethodDefinition);
            }
            else if (obj is PropertyDefinition)
            {
                Delete(obj as PropertyDefinition);
            }
            else if (obj is FieldDefinition)
            {
                Delete(obj as FieldDefinition);
            }
            else if (obj is EventDefinition)
            {
                Delete(obj as EventDefinition);
            }
            else if (obj is AssemblyNameReference)
            {
                Delete(obj as AssemblyNameReference);
            }
            else if (obj is Resource)
            {
                Delete(obj as Resource);
            }
        }
        #endregion

    }
}
