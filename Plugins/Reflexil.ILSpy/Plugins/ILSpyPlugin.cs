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

extern alias ilspycecil;

using System;
using System.Linq;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using Mono.Cecil;

using icAssemblyDefinition = ilspycecil::Mono.Cecil.AssemblyDefinition;
using icLinkedResource = ilspycecil::Mono.Cecil.LinkedResource;
using icEmbeddedResource = ilspycecil::Mono.Cecil.EmbeddedResource;
using icAssemblyLinkedResource = ilspycecil::Mono.Cecil.AssemblyLinkedResource;

namespace Reflexil.Plugins.ILSpy
{
	public class ILSpyPlugin : BasePlugin
	{
		public ILSpyPlugin(IPackage package) : base(package)
		{
		}

		public override string HostApplication
		{
			get { return "ILSpy"; }
		}

		public override bool IsAssemblyNameReferenceHandled(object item)
		{
			return item is AssemblyReferenceTreeNode;
		}

		public override bool IsAssemblyDefinitionHandled(object item)
		{
			return item is AssemblyTreeNode;
		}

		public override bool IsTypeDefinitionHandled(object item)
		{
			return item is TypeTreeNode;
		}

		public override bool IsPropertyDefinitionHandled(object item)
		{
			return item is PropertyTreeNode;
		}

		public override bool IsFieldDefinitionHandled(object item)
		{
			return item is FieldTreeNode;
		}

		public override bool IsModuleDefinitionHandled(object item)
		{
			// We use a merged Assembly/MainModule handler, because ILSpy doesn't handle submodules
			return false;
		}

		public override bool IsMethodDefinitionHandled(object item)
		{
			return item is MethodTreeNode;
		}

		public override bool IsEventDefinitionHandled(object item)
		{
			return item is EventTreeNode;
		}

		public override bool IsLinkedResourceHandled(object item)
		{
			var node = item as ResourceTreeNode;
			return node != null && node.Resource is icLinkedResource;
		}

		public override bool IsEmbeddedResourceHandled(object item)
		{
			var node = item as ResourceTreeNode;
			return node != null && node.Resource is icEmbeddedResource;
		}

		public override bool IsAssemblyLinkedResourceHandled(object item)
		{
			var node = item as ResourceTreeNode;
			return node != null && node.Resource is icAssemblyLinkedResource;
		}

		public override IAssemblyContext GetAssemblyContext(string location)
		{
			return GetAssemblyContext<ILSpyAssemblyContext>(location);
		}

		public override IAssemblyContext GetAssemblyContext(object item)
		{
			var node = item as ILSpyTreeNode;
			if (node == null)
				return null;

			var anode = node as AssemblyTreeNode;
			if (anode != null)
				return GetAssemblyContext(anode.LoadedAssembly.FileName) as ILSpyAssemblyContext;

			return GetAssemblyContext(node.Parent as ILSpyTreeNode);
		}

		private TDef MapCecilTypeFromILSpyNode<TDef, TNode>(object item, Func<TNode, icAssemblyDefinition> assembly, Func<ILSpyAssemblyContext, TNode, TDef> finder) where TDef : class where TNode : ILSpyTreeNode
		{
			ILSpyAssemblyContext context;

			var node = item as TNode;
			if (node == null)
				return null;

			var adef = assembly(node);
			var anode = adef != null ? MainWindow.Instance.FindTreeNode(adef) as AssemblyTreeNode : null;
			if (anode != null) // if we can have an assembly definition, quick lookup the context
				context = GetAssemblyContext(anode.LoadedAssembly.FileName) as ILSpyAssemblyContext;
			else // else recurse the tree
				context = GetAssemblyContext(node) as ILSpyAssemblyContext;
			
			return finder(context, node);
		}

		public override MethodDefinition GetMethodDefinition(object item)
		{
			return MapCecilTypeFromILSpyNode<MethodDefinition, MethodTreeNode>(item, node => node.MethodDefinition.Module.Assembly, (context, node) => context.GetMethodDefinition(node.MethodDefinition));
		}

		public override PropertyDefinition GetPropertyDefinition(object item)
		{
			return MapCecilTypeFromILSpyNode<PropertyDefinition, PropertyTreeNode>(item, node => node.PropertyDefinition.Module.Assembly, (context, node) => context.GetPropertyDefinition(node.PropertyDefinition));
		}

		public override FieldDefinition GetFieldDefinition(object item)
		{
			return MapCecilTypeFromILSpyNode<FieldDefinition, FieldTreeNode>(item, node => node.FieldDefinition.Module.Assembly, (context, node) => context.GetFieldDefinition(node.FieldDefinition));
		}

		public override EventDefinition GetEventDefinition(object item)
		{
			return MapCecilTypeFromILSpyNode<EventDefinition, EventTreeNode>(item, node => node.EventDefinition.Module.Assembly, (context, node) => context.GetEventDefinition(node.EventDefinition));
		}

		public override LinkedResource GetLinkedResource(object item)
		{
			return MapCecilTypeFromILSpyNode<LinkedResource, ResourceTreeNode>(item, node => null, (context, node) => context.GetResource(node.Resource) as LinkedResource);
		}

		public override EmbeddedResource GetEmbeddedResource(object item)
		{
			return MapCecilTypeFromILSpyNode<EmbeddedResource, ResourceTreeNode>(item, node => null, (context, node) => context.GetResource(node.Resource) as EmbeddedResource);
		}

		public override AssemblyLinkedResource GetAssemblyLinkedResource(object item)
		{
			return MapCecilTypeFromILSpyNode<AssemblyLinkedResource, ResourceTreeNode>(item, node => null, (context, node) => context.GetResource(node.Resource) as AssemblyLinkedResource);
		}

		public override AssemblyNameReference GetAssemblyNameReference(object item)
		{
			return MapCecilTypeFromILSpyNode<AssemblyNameReference, AssemblyReferenceTreeNode>(item, node => null, (context, node) => context.GetAssemblyNameReference(node.AssemblyNameReference));
		}

		public override TypeDefinition GetTypeDefinition(object item)
		{
			return MapCecilTypeFromILSpyNode<TypeDefinition, TypeTreeNode>(item, node => node.TypeDefinition.Module.Assembly, (context, node) => context.GetTypeDefinition(node.TypeDefinition));
		}

		public override AssemblyDefinition GetAssemblyDefinition(object item)
		{
			var node = item as AssemblyTreeNode;
			if (node == null)
				return null;

			var loadedAssembly = node.LoadedAssembly;
			var context = GetAssemblyContext(loadedAssembly.FileName);
			if (context == null)
				return null;

			return context.AssemblyDefinition;
		}

		public override ModuleDefinition GetModuleDefinition(object item)
		{
			// We use a merged Assembly/MainModule handler, because ILSpy doesn't handle submodules
			return null;
		}

		public void RemoveFromCache(object item)
		{
			foreach (var ctx in Assemblycache.Values.Cast<ILSpyAssemblyContext>())
				ctx.RemoveFromCache(item);
		}

	}
}
