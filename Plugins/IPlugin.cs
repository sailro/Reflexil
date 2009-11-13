/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Plugins
{
	public interface IPlugin
    {

        #region " Methods "
        /// <summary>
        /// Return all images as a single bitmap
        /// </summary>
        /// <returns>Bitmap</returns>
        Bitmap GetAllImages();

        /// <summary>
        /// Return all opcodes
        /// </summary>
        /// <returns>Opcodes</returns>
        List<OpCode> GetAllOpCodes();
        
        /// <summary>
        /// Get an opcode description
        /// </summary>
        /// <param name="opcode">Opcode</param>
        /// <returns>The opcode description or an empty string if not found</returns>
        string GetOpcodeDesc(OpCode opcode);

        /// <summary>
        /// Check if an assembly context is loaded
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>True is already loaded</returns>
        bool IsAssemblyContextLoaded(string location);

        /// <summary>
        /// Get an assembly context in cache or create a new one if necessary
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>Null if unable to load the assembly</returns>
        IAssemblyContext GetAssemblyContext(string location);

        /// <summary>
        /// Reload an assembly context
        /// </summary>
        /// <param name="location">location (key to retrieve the cached assembly context)</param>
        /// <returns>Returns the reloaded assembly context</returns>
        IAssemblyContext ReloadAssemblyContext(string location);

        /// <summary>
        /// Synchronize assembly contexts with host' loaded assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        void SynchronizeAssemblyContexts(ICollection assemblies);

        /// <summary>
        /// Reload assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        void ReloadAssemblies(ICollection assemblies);

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        bool IsAssemblyNameReferenceHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        bool IsAssemblyDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        bool IsTypeDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve a Module Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        bool IsModuleDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        bool IsMethodDefinitionHandled(object item);

        /// <summary>
        /// Return all assemblies loaded into the host program
        /// </summary>
        /// <param name="wrap">true when wrapping native objects into IAssemblyWrapper</param>
        /// <returns>Assemblies</returns>
        ICollection GetAssemblies(bool wrap);

        /// <summary>
        /// Reload all opcode descriptions from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        void ReloadOpcodesDesc(Stream stream);

        /// <summary>
        /// Reload all images from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        void ReloadImages(Stream stream);

        /// <summary>
        /// Retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Name Reference</returns>
        AssemblyNameReference GetAssemblyNameReference(object item);

        /// <summary>
        /// Retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Definition</returns>
        AssemblyDefinition GetAssemblyDefinition(object item);

        /// <summary>
        /// Retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Type Definition</returns>
        TypeDefinition GetTypeDefinition(object item);

        /// <summary>
        /// Retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Method Definition</returns>
        MethodDefinition GetMethodDefinition(object item);

        /// <summary>
        /// Retrieve the location of the module object
        /// </summary>
        /// <param name="item">the module object</param>
        /// <returns>the location</returns>
        string GetModuleLocation(object item);
        #endregion

    }
}
