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
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Reflector.CodeModel;
#endregion

namespace Reflexil.Plugins.Reflector
{
    /// <summary>
    /// Plugin implementation for Reflector 
    /// </summary>
	class ReflectorPlugin : BasePlugin 
	{
        #region " Methods "
        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsAssemblyNameReferenceHandled(object item)
        {
            return (item) is IAssemblyReference && (item as IAssemblyReference).Context != null;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsAssemblyDefinitionHandled(object item)
        {
            return ((item) is IAssembly) && (item as IAssembly).Type != AssemblyType.None;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsTypeDefinitionHandled(object item)
        {
            return (item) is ITypeDeclaration;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Module Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsModuleDefinitionHandled(object item)
        {
            return (item) is IModule;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsMethodDefinitionHandled(object item)
        {
            return (item) is IMethodDeclaration;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Property Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsPropertyDefinitionHandled(object item)
        {
            return item is IPropertyDeclaration;
        }

        /// <summary>
        /// Retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Method Definition</returns>
        public override MethodDefinition GetMethodDefinition(object item)
        {
            IMethodDeclaration mdec = (IMethodDeclaration)item;
            return ReflectorHelper.ReflectorMethodToCecilMethod(mdec);
        }

        /// <summary>
        /// Retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Name Reference</returns>
        public override AssemblyNameReference GetAssemblyNameReference(object item)
        {
            IAssemblyReference aref = item as IAssemblyReference;
            return ReflectorHelper.ReflectorAssemblyReferenceToCecilAssemblyNameReference(aref);
        }

        /// <summary>
        /// Retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Definition</returns>
        public override AssemblyDefinition GetAssemblyDefinition(object item)
        {
            IAssemblyLocation aloc = item as IAssemblyLocation;
            return ReflectorHelper.ReflectorAssemblyLocationToCecilAssemblyDefinition(aloc);
        }

        /// <summary>
        /// Retrieve a Property Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Property Definition</returns>
        public override PropertyDefinition GetPropertyDefinition(object item)
        {
            IPropertyDeclaration pdec = (IPropertyDeclaration)item;
            return ReflectorHelper.ReflectorPropertyToCecilProperty(pdec);
        }

        /// <summary>
        /// Retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Type Definition</returns>
        public override TypeDefinition GetTypeDefinition(object item)
        {
            ITypeDeclaration tdec = item as ITypeDeclaration;
            return ReflectorHelper.ReflectorTypeToCecilType(tdec);
        }

        /// <summary>
        /// Synchronize assembly contexts with host' loaded assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        public override void SynchronizeAssemblyContexts(ICollection assemblies)
        {
            List<string> locations = new List<string>();

            foreach (IAssembly asm in assemblies)
            {
                locations.Add(System.Environment.ExpandEnvironmentVariables(asm.Location));
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
        /// Return all assemblies loaded into Reflector
        /// </summary>
        /// <param name="wrap">true when wrapping native objects into IAssemblyWrapper</param>
        /// <returns>Assemblies</returns>
        public override ICollection GetAssemblies(bool wrap)
        {
            if (wrap)
            {
                ArrayList result = new ArrayList();
                foreach (IAssembly asm in m_assemblies)
                {
                    result.Add(new ReflectorAssemblyWrapper(asm));
                }
                return result;
            }
            else
            {
                return m_assemblies;
            }
        }

        /// <summary>
        /// Retrieve the location of the module object
        /// </summary>
        /// <param name="item">the module object</param>
        /// <returns>the location</returns>
        public override string GetModuleLocation(object item)
        {
            return Environment.ExpandEnvironmentVariables(((IModule)item).Location);
        }

        /// <summary>
        /// Get an assembly context in cache or create a new one if necessary
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>Null if unable to load the assembly</returns>
        public override IAssemblyContext GetAssemblyContext(string location)
        {
            return GetAssemblyContext<ReflectorAssemblyContext>(location);
        }

        /// <summary>
        /// Load assembly from disk
        /// </summary>
        /// <param name="location">assembly location</param>
        /// <returns></returns>
        public override AssemblyDefinition LoadAssembly(string location)
        {
            return AssemblyFactory.GetAssembly(location);
        }
        #endregion

    }
}
