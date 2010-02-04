/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
                NestedTypeCollection coll = tdef.DeclaringType.NestedTypes;
                if (coll.Contains(tdef))
                {
                    coll.Remove(tdef);
                }
            }
            if (tdef.Module != null)
            {
                TypeDefinitionCollection coll = tdef.Module.Types;
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
                if (mdef.DeclaringType.Constructors.Contains(mdef))
                {
                    mdef.DeclaringType.Constructors.Remove(mdef);
                }
                else if (mdef.DeclaringType.Methods.Contains(mdef))
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
                PropertyDefinitionCollection coll = pdef.DeclaringType.Properties;
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
                FieldDefinitionCollection coll = fdef.DeclaringType.Fields;
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
                EventDefinitionCollection coll = edef.DeclaringType.Events;
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
        }
        #endregion

    }
}
