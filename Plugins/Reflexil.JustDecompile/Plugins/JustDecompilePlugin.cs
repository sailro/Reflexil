/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System;
using System.Linq;
using JustDecompile.API.Core;
using Mono.Cecil;

using ResourceType = JustDecompile.API.Core.ResourceType;

namespace Reflexil.Plugins.JustDecompile
{
	public class JustDecompilePlugin : BasePlugin
	{
		public JustDecompilePlugin(IPackage package) : base(package)
		{
		}

		public override string HostApplication
		{
			get { return "JustDecompile"; }
		}

		public override bool IsAssemblyNameReferenceHandled(object item)
		{
			return item is IAssemblyReferenceTreeViewItem;
		}

		public override bool IsAssemblyDefinitionHandled(object item)
		{
			return item is IAssemblyDefinitionTreeViewItem;
		}

		public override bool IsTypeDefinitionHandled(object item)
		{
			return item is ITypeDefinitionTreeViewItem;
		}

		public override bool IsPropertyDefinitionHandled(object item)
		{
			return item is IPropertyDefinitionTreeViewItem;
		}

		public override bool IsFieldDefinitionHandled(object item)
		{
			return item is IFieldDefinitionTreeViewItem;
		}

		public override bool IsModuleDefinitionHandled(object item)
		{
			return item is IAssemblyModuleDefinitionTreeViewItem;
		}

		public override bool IsMethodDefinitionHandled(object item)
		{
			return item is IMethodDefinitionTreeViewItem;
		}

		public override bool IsEventDefinitionHandled(object item)
		{
			return item is IEventDefinitionTreeViewItem;
		}

		public override bool IsLinkedResourceHandled(object item)
		{
			var node = item as IResourceTreeViewItem;
			return node != null && node.Resource.ResourceType == ResourceType.Linked;
		}

		public override bool IsEmbeddedResourceHandled(object item)
		{
			var node = item as IResourceTreeViewItem;
			return node != null && node.Resource.ResourceType == ResourceType.Embedded;
		}

		public override bool IsAssemblyLinkedResourceHandled(object item)
		{
			var node = item as IResourceTreeViewItem;
			return node != null && node.Resource.ResourceType == ResourceType.AssemblyLinked;
		}

		public override IAssemblyContext GetAssemblyContext(string location)
		{
			return GetAssemblyContext<JustDecompileAssemblyContext>(location);
		}

		public override IAssemblyContext GetAssemblyContext(object item)
		{
			var node = item as ITreeViewItem;
			if (node == null)
				return null;

			switch (node.TreeNodeType)
			{
				case TreeNodeType.AssemblyDefinition:
					return GetAssemblyContext(((IAssemblyDefinitionTreeViewItem) node).AssemblyDefinition.MainModule.FilePath);
				case TreeNodeType.AssemblyEventDefinition:
					return GetAssemblyContext(((IEventDefinitionTreeViewItem) node).EventDefinition.DeclaringType.Module.FilePath);
				case TreeNodeType.AssemblyFieldDefinition:
					return GetAssemblyContext(((IFieldDefinitionTreeViewItem) node).FieldDefinition.DeclaringType.Module.FilePath);
				case TreeNodeType.AssemblyMethodDefinition:
					return GetAssemblyContext(((IMethodDefinitionTreeViewItem) node).MethodDefinition.DeclaringType.Module.FilePath);
				case TreeNodeType.AssemblyModuleDefinition:
					return GetAssemblyContext(((IAssemblyModuleDefinitionTreeViewItem) node).ModuleDefinition.FilePath);
				case TreeNodeType.AssemblyPropertyDefinition:
					return GetAssemblyContext(((IPropertyDefinitionTreeViewItem) node).PropertyDefinition.DeclaringType.Module.FilePath);
				case TreeNodeType.AssemblyTypeDefinition:
					return GetAssemblyContext(((ITypeDefinitionTreeViewItem) node).TypeDefinition.Module.FilePath);
				case TreeNodeType.AssemblyReference:
					return SearchAssemblyReferenceContext(((IAssemblyReferenceTreeViewItem) item).AssemblyNameReference);
				case TreeNodeType.AssemblyResource:
					return GetAssemblyContext(((IResourceTreeViewItem) node).AssemblyFile);
			}

			return null;
		}

		private IAssemblyContext SearchAssemblyReferenceContext(IAssemblyNameReference anr)
		{
			var canr = JustDecompileHelper.ExtractCecilAssemblyNameReference(anr);
			if (canr == null)
				return null;

			foreach (var wrapper in Package.HostAssemblies)
			{
				var jdwrapper = (JustDecompileAssemblyWrapper) wrapper;
				foreach (var oanr in jdwrapper.AssemblyDefinition.MainModule.AssemblyReferences)
				{
					var coanr = JustDecompileHelper.ExtractCecilAssemblyNameReference(oanr);
					if (canr == coanr)
						return GetAssemblyContext(jdwrapper.Location);
				}
			}

			return null;
		}

		private TDef MapCecilTypeFromJustDecompileNode<TDef, TNode>(object item,
			Func<JustDecompileAssemblyContext, TNode, TDef> finder) where TDef : class where TNode : class, ITreeViewItem
		{
			var node = item as TNode;
			if (node == null)
				return null;

			var context = GetAssemblyContext(node) as JustDecompileAssemblyContext;
			if (context == null)
				return null;

			return finder(context, node);
		}

		public override MethodDefinition GetMethodDefinition(object item)
		{
			return MapCecilTypeFromJustDecompileNode<MethodDefinition, IMethodDefinitionTreeViewItem>(item,
				(context, node) => context.GetMethodDefinition(node.MethodDefinition));
		}

		public override PropertyDefinition GetPropertyDefinition(object item)
		{
			return MapCecilTypeFromJustDecompileNode<PropertyDefinition, IPropertyDefinitionTreeViewItem>(item,
				(context, node) => context.GetPropertyDefinition(node.PropertyDefinition));
		}

		public override FieldDefinition GetFieldDefinition(object item)
		{
			return MapCecilTypeFromJustDecompileNode<FieldDefinition, IFieldDefinitionTreeViewItem>(item,
				(context, node) => context.GetFieldDefinition(node.FieldDefinition));
		}

		public override EventDefinition GetEventDefinition(object item)
		{
			return MapCecilTypeFromJustDecompileNode<EventDefinition, IEventDefinitionTreeViewItem>(item,
				(context, node) => context.GetEventDefinition(node.EventDefinition));
		}

		public override LinkedResource GetLinkedResource(object item)
		{
			return MapCecilTypeFromJustDecompileNode<LinkedResource, IResourceTreeViewItem>(item,
				(context, node) => context.GetResource(node.Resource) as LinkedResource);
		}

		public override EmbeddedResource GetEmbeddedResource(object item)
		{
			return MapCecilTypeFromJustDecompileNode<EmbeddedResource, IResourceTreeViewItem>(item,
				(context, node) => context.GetResource(node.Resource) as EmbeddedResource);
		}

		public override AssemblyLinkedResource GetAssemblyLinkedResource(object item)
		{
			return MapCecilTypeFromJustDecompileNode<AssemblyLinkedResource, IResourceTreeViewItem>(item,
				(context, node) => context.GetResource(node.Resource) as AssemblyLinkedResource);
		}

		public override AssemblyNameReference GetAssemblyNameReference(object item)
		{
			return MapCecilTypeFromJustDecompileNode<AssemblyNameReference, IAssemblyReferenceTreeViewItem>(item,
				(context, node) => context.GetAssemblyNameReference(node.AssemblyNameReference));
		}

		public override TypeDefinition GetTypeDefinition(object item)
		{
			return MapCecilTypeFromJustDecompileNode<TypeDefinition, ITypeDefinitionTreeViewItem>(item,
				(context, node) => context.GetTypeDefinition(node.TypeDefinition));
		}

		public override AssemblyDefinition GetAssemblyDefinition(object item)
		{
			var node = item as IAssemblyDefinitionTreeViewItem;
			if (node == null)
				return null;

			var loadedAssembly = node.AssemblyDefinition;
			var context = GetAssemblyContext(loadedAssembly.MainModule.FilePath);
			if (context == null)
				return null;

			return context.AssemblyDefinition;
		}

		public override ModuleDefinition GetModuleDefinition(object item)
		{
			var node = item as IAssemblyModuleDefinitionTreeViewItem;
			if (node == null)
				return null;

			var context = GetAssemblyContext(node.ModuleDefinition.Assembly.MainModule.FilePath);
			if (context == null)
				return null;

			return context.AssemblyDefinition.MainModule;
		}

		public void RemoveFromCache(object item)
		{
			foreach (var ctx in Assemblycache.Values.Cast<JustDecompileAssemblyContext>())
				ctx.RemoveFromCache(item);
		}
	}
}