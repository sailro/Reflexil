/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
#endregion

namespace Reflexil.Plugins.CecilStudio
{
    /// <summary>
    /// Plugin implementation for Cecil Studio 
    /// </summary>
    class CecilStudioPlugin : BasePlugin
    {

        #region " Properties "
        public override string HostApplication
        {
            get { return "Cecil Studio"; }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="package">Host package</param>
        public CecilStudioPlugin(IPackage package) : base(package)
        {
        }

        /// <summary>
        /// Return all assemblies loaded into the host program
        /// </summary>
        /// <param name="wrap">true when wrapping native objects into IAssemblyWrapper</param>
        /// <returns>Assemblies</returns>
        public override ICollection GetAssemblies(bool wrap)
        {
            if (wrap)
            {
                ArrayList result = new ArrayList();
                foreach (AssemblyDefinition adef in m_assemblies)
                {
                    result.Add(new CecilStudioAssemblyWrapper(adef));
                }
                return result;
            }
            else
            {
                return m_assemblies;
            }
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsAssemblyNameReferenceHandled(object item)
        {
            return item is AssemblyNameReference;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsAssemblyDefinitionHandled(object item)
        {
            return item is AssemblyDefinition;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsTypeDefinitionHandled(object item)
        {
            return item is TypeDefinition;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Module Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsModuleDefinitionHandled(object item)
        {
            return item is ModuleDefinition;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsMethodDefinitionHandled(object item)
        {
            return item is MethodDefinition;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Property Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsPropertyDefinitionHandled(object item)
        {
            return item is PropertyDefinition;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Field Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsFieldDefinitionHandled(object item)
        {
            return item is FieldDefinition;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Event Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsEventDefinitionHandled(object item)
        {
            return item is EventDefinition;
        }
        
        /// <summary>
        /// Retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Method Definition</returns>
        public override MethodDefinition GetMethodDefinition(object item)
        {
            return item as MethodDefinition;
        }

        /// <summary>
        /// Get an assembly context in cache or create a new one if necessary
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>Null if unable to load the assembly</returns>
        public override IAssemblyContext GetAssemblyContext(string location)
        {
            return GetAssemblyContext<CecilStudioAssemblyContext>(location);
        }

        /// <summary>
        /// Retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Name Reference</returns>
        public override AssemblyNameReference GetAssemblyNameReference(object item)
        {
            return item as AssemblyNameReference;
        }

        /// <summary>
        /// Retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Definition</returns>
        public override AssemblyDefinition GetAssemblyDefinition(object item)
        {
            return item as AssemblyDefinition;
        }

        /// <summary>
        /// Retrieve a Property Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Property Definition</returns>
        public override PropertyDefinition GetPropertyDefinition(object item)
        {
            return item as PropertyDefinition;
        }

        /// <summary>
        /// Retrieve a Field Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Field Definition</returns>
        public override FieldDefinition GetFieldDefinition(object item)
        {
            return item as FieldDefinition;
        }

        /// <summary>
        /// Retrieve an Event Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Event Definition</returns>
        public override EventDefinition GetEventDefinition(object item)
        {
            return item as EventDefinition;
        }

        /// <summary>
        /// Retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Type Definition</returns>
        public override TypeDefinition GetTypeDefinition(object item)
        {
            return item as TypeDefinition;
        }

        /// <summary>
        /// Retrieve the location of the module object
        /// </summary>
        /// <param name="item">the module object</param>
        /// <returns>the location</returns>
        public override string GetModuleLocation(object item)
        {
            return (item as ModuleDefinition).Image.FileName;
        }

        /// <summary>
        /// Synchronize assembly contexts with host' loaded assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        public override void SynchronizeAssemblyContexts(ICollection assemblies)
        {
            List<string> locations = new List<string>();

            foreach (AssemblyDefinition adef in assemblies)
            {
                locations.Add(adef.MainModule.Image.FileName);
            }

            foreach (string location in new ArrayList(m_assemblycache.Keys))
            {
                if (!locations.Contains(location))
                {
                    m_assemblycache.Remove(location);
                }
            }
        }

        /// <summary>
        /// Load assembly from disk
        /// </summary>
        /// <param name="location">assembly location</param>
        /// <returns></returns>
        public override AssemblyDefinition LoadAssembly(string location, bool loadsymbols)
        {
            // Stay in sync with Cecil Studio browser, don't load anything but reuse previously loaded assembly
            foreach(AssemblyDefinition adef in m_assemblies) {
                if (adef.MainModule.Image.FileName.Equals(location, StringComparison.OrdinalIgnoreCase)) {
                    return adef;
                }
            }

            return null;
        }
        #endregion

    }
}
