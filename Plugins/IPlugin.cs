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

using System.Collections.Generic;
using System.Drawing;
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Plugins
{
	public interface IPlugin
	{
		#region Properties

		IPackage Package { get; }

		string HostApplication { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Return all images as a single bitmap
		/// </summary>
		/// <returns>Bitmap</returns>
		Bitmap GetAllBrowserImages();

		/// <summary>
		/// Return all images as a single bitmap
		/// </summary>
		/// <returns>Bitmap</returns>
		Bitmap GetAllBarImages();

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
		/// Get an assembly context in cache or create a new one if necessary
		/// </summary>
		/// <param name="object">Any host object model item</param>
		/// <returns>Null if unable to load the assembly</returns>
		IAssemblyContext GetAssemblyContext(object item);

		/// <summary>
		/// Reload an assembly context
		/// </summary>
		/// <param name="location">location (key to retrieve the cached assembly context)</param>
		/// <returns>Returns the reloaded assembly context</returns>
		IAssemblyContext ReloadAssemblyContext(string location);

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
		/// Determine if the plugin is able to retrieve a Property Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		bool IsPropertyDefinitionHandled(object item);

		/// <summary>
		/// Determine if the plugin is able to retrieve a Field Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		bool IsFieldDefinitionHandled(object item);

		/// <summary>
		/// Determine if the plugin is able to retrieve an Event Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		bool IsEventDefinitionHandled(object item);

		/// <summary>
		/// Determine if the plugin is able to retrieve an Embedded Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		bool IsEmbeddedResourceHandled(object item);

		/// <summary>
		/// Determine if the plugin is able to retrieve an Assembly Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		bool IsAssemblyLinkedResourceHandled(object item);

		/// <summary>
		/// Determine if the plugin is able to retrieve a Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		bool IsLinkedResourceHandled(object item);

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
		/// Retrieve a Property Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Property Definition</returns>
		PropertyDefinition GetPropertyDefinition(object item);

		/// <summary>
		/// Retrieve a Field Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Field Definition</returns>
		FieldDefinition GetFieldDefinition(object item);

		/// <summary>
		/// Retrieve an Event Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Event Definition</returns>
		EventDefinition GetEventDefinition(object item);

		/// <summary>
		/// Retrieve an Embedded Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Embedded Resource</returns>
		EmbeddedResource GetEmbeddedResource(object item);

		/// <summary>
		/// Retrieve an Assembly Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching A.L. Resource</returns>
		AssemblyLinkedResource GetAssemblyLinkedResource(object item);

		/// <summary>
		/// Retrieve a Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Linked Resource</returns>
		LinkedResource GetLinkedResource(object item);

		/// <summary>
		/// Retrieve a Method Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Method Definition</returns>
		MethodDefinition GetMethodDefinition(object item);

		/// <summary>
		/// Retrieve the Module Definition from the object
		/// </summary>
		/// <param name="item">the module object</param>
		/// <returns>The matching Module Definition</returns>
		ModuleDefinition GetModuleDefinition(object item);

		#endregion
	}
}