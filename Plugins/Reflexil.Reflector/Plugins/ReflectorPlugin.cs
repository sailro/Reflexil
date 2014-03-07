/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Reflector.CodeModel;
using Reflexil.Utils;
#endregion

namespace Reflexil.Plugins.Reflector
{
    /// <summary>
    /// Plugin implementation for Reflector 
    /// </summary>
	class ReflectorPlugin : BasePlugin 
	{

        #region Properties
        public override string HostApplication
        {
            get { return "Reflector"; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="package">Host package</param>
        public ReflectorPlugin(IPackage package) : base(package)
        {
        }

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
        /// Determine if the plugin is able to retrieve a Field Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsFieldDefinitionHandled(object item)
        {
            return item is IFieldDeclaration;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Event Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsEventDefinitionHandled(object item)
        {
            return item is IEventDeclaration;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Embedded Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsEmbeddedResourceHandled(object item)
        {
            return item is IEmbeddedResource;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsAssemblyLinkedResourceHandled(object item)
        {
            return item is IResource && !IsEmbeddedResourceHandled(item) && !IsLinkedResourceHandled(item);
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public override bool IsLinkedResourceHandled(object item)
        {
            return item is IFileResource;
        }

        /// <summary>
        /// Retrieve an Assembly Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching A.L. Resource</returns>
        public override AssemblyLinkedResource GetAssemblyLinkedResource(object item)
        {
            var res = item as IResource;
            return (AssemblyLinkedResource)ReflectorHelper.ReflectorResourceToCecilResource(res);
        }

        /// <summary>
        /// Retrieve a Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Linked Resource</returns>
        public override LinkedResource GetLinkedResource(object item)
        {
            var res = item as IResource;
            return (LinkedResource)ReflectorHelper.ReflectorResourceToCecilResource(res);
        }

        /// <summary>
        /// Retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Method Definition</returns>
        public override MethodDefinition GetMethodDefinition(object item)
        {
            var mdec = (IMethodDeclaration)item;
            return ReflectorHelper.ReflectorMethodToCecilMethod(mdec);
        }

        /// <summary>
        /// Retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Name Reference</returns>
        public override AssemblyNameReference GetAssemblyNameReference(object item)
        {
            var aref = item as IAssemblyReference;
            return ReflectorHelper.ReflectorAssemblyReferenceToCecilAssemblyNameReference(aref);
        }

        /// <summary>
        /// Retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Definition</returns>
        public override AssemblyDefinition GetAssemblyDefinition(object item)
        {
            var aloc = item as IAssemblyLocation;
            return ReflectorHelper.ReflectorAssemblyLocationToCecilAssemblyDefinition(aloc);
        }

        /// <summary>
        /// Retrieve a Property Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Property Definition</returns>
        public override PropertyDefinition GetPropertyDefinition(object item)
        {
            var pdec = (IPropertyDeclaration)item;
            return ReflectorHelper.ReflectorPropertyToCecilProperty(pdec);
        }

        /// <summary>
        /// Retrieve a Field Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Field Definition</returns>
        public override FieldDefinition GetFieldDefinition(object item)
        {
            var fdec = item as IFieldDeclaration;
            return ReflectorHelper.ReflectorFieldToCecilField(fdec);
        }

        /// <summary>
        /// Retrieve an Event Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Event Definition</returns>
        public override EventDefinition GetEventDefinition(object item)
        {
            var edec = item as IEventDeclaration;
            return ReflectorHelper.ReflectorEventToCecilEvent(edec);
        }

        /// <summary>
        /// Retrieve an Embedded Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Embedded Resource</returns>
        public override EmbeddedResource GetEmbeddedResource(object item)
        {
            var res = item as IResource;
            return (EmbeddedResource)ReflectorHelper.ReflectorResourceToCecilResource(res);
        }

        /// <summary>
        /// Retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Type Definition</returns>
        public override TypeDefinition GetTypeDefinition(object item)
        {
            var tdec = item as ITypeDeclaration;
            return ReflectorHelper.ReflectorTypeToCecilType(tdec);
        }

        /// <summary>
        /// Synchronize assembly contexts with host' loaded assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        public override void SynchronizeAssemblyContexts(ICollection assemblies)
        {
            var locations = new List<string>();

            foreach (IAssembly asm in assemblies)
                locations.Add(Environment.ExpandEnvironmentVariables(asm.Location));

            foreach (string location in new ArrayList(Assemblycache.Keys))
                if (!locations.Contains(location))
                    Assemblycache.Remove(location);
        }

        /// <summary>
        /// Return all assemblies loaded into Reflector
        /// </summary>
        /// <param name="wrap">true when wrapping native objects into IAssemblyWrapper</param>
        /// <returns>Assemblies</returns>
        public override ICollection GetAssemblies(bool wrap)
        {
	        if (!wrap) 
				return Assemblies;
	        
			var result = new ArrayList();
	        foreach (IAssembly asm in Assemblies)
		        result.Add(new ReflectorAssemblyWrapper(asm));

	        return result;
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
        /// <param name="loadsymbols">load symbols</param>
        /// <returns></returns>
        public override AssemblyDefinition LoadAssembly(string location, bool loadsymbols)
        {
            var parameters = new ReaderParameters {ReadSymbols = loadsymbols, ReadingMode = ReadingMode.Deferred};
	        var resolver = new ReflexilAssemblyResolver();
            try
            {
                return resolver.ReadAssembly(location, parameters);
            } catch(Exception)
            {
	            // perhaps pdb file is not found, just ignore this time
	            if (!loadsymbols) 
					throw;
	            
				parameters.ReadSymbols = false;
	            return resolver.ReadAssembly(location, parameters);
            }
        }

        /// <summary>
        /// Remove an item from cache
        /// </summary>
        /// <param name="item">item to remove</param>
        public void RemoveFromCache(object item)
        {
	        foreach (var ctx in Assemblycache.Values.OfType<ReflectorAssemblyContext>())
		        ctx.RemoveFromCache(item);
        }

	    #endregion

    }
}
