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

#endregion

namespace Reflexil.Plugins.CecilStudio
{
	/// <summary>
	/// Plugin implementation for Cecil Studio 
	/// </summary>
	internal class CecilStudioPlugin : BasePlugin
	{
		#region Properties

		public override string HostApplication
		{
			get { return "Cecil Studio"; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="package">Host package</param>
		public CecilStudioPlugin(IPackage package) : base(package)
		{
		}

		/// <summary>
		/// Determine if the plugin is able to retrieve an Assembly Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		public override bool IsAssemblyLinkedResourceHandled(object item)
		{
			return item is AssemblyLinkedResource;
		}

		/// <summary>
		/// Retrieve an Assembly Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching A.L. Resource</returns>
		public override AssemblyLinkedResource GetAssemblyLinkedResource(object item)
		{
			return item as AssemblyLinkedResource;
		}

		/// <summary>
		/// Retrieve a Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Linked Resource</returns>
		public override LinkedResource GetLinkedResource(object item)
		{
			return item as LinkedResource;
		}

		/// <summary>
		/// Determine if the plugin is able to retrieve a Linked Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		public override bool IsLinkedResourceHandled(object item)
		{
			return item is LinkedResource;
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
		/// Determine if the plugin is able to retrieve an Embedded Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>true if handled</returns>
		public override bool IsEmbeddedResourceHandled(object item)
		{
			return item is EmbeddedResource;
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
		/// Retrieve an Embedded Resource from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Embedded Resource</returns>
		public override EmbeddedResource GetEmbeddedResource(object item)
		{
			return item as EmbeddedResource;
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
		/// Retrieve a Module Definition from the object
		/// </summary>
		/// <param name="item">the object</param>
		/// <returns>The matching Module Definition</returns>
		public override ModuleDefinition GetModuleDefinition(object item)
		{
			return item as ModuleDefinition;
		}

		/// <summary>
		/// Load assembly from disk
		/// </summary>
		/// <param name="location">assembly location</param>
		/// <param name="loadsymbols">load pdb symbols</param>
		/// <returns></returns>
		public override AssemblyDefinition LoadAssembly(string location, bool loadsymbols)
		{
			// Stay in sync with Cecil Studio browser, don't load anything but reuse previously loaded assembly
			return
				Package.HostAssemblies.Cast<CecilStudioAssemblyWrapper>()
					.Select(w => w.AssemblyDefinition)
					.FirstOrDefault(adef => adef.MainModule.Image.FileName.Equals(location, StringComparison.OrdinalIgnoreCase));
		}

		public override IAssemblyContext GetAssemblyContext(object item)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}