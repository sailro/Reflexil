extern alias ilspycecil;

using System;
using System.Collections;
using System.Linq;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using Mono.Cecil;
using Reflexil.Plugins;
using Reflexil.Utils;

using icAssemblyDefinition = ilspycecil::Mono.Cecil.AssemblyDefinition;
using icLinkedResource = ilspycecil::Mono.Cecil.LinkedResource;
using icEmbeddedResource = ilspycecil::Mono.Cecil.EmbeddedResource;
using icAssemblyLinkedResource = ilspycecil::Mono.Cecil.AssemblyLinkedResource;

namespace Reflexil.ILSpy.Plugins
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
			// TODO
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

		private ILSpyAssemblyContext GetAssemblyContext(ILSpyTreeNode node)
		{
			if (node == null)
				return null;

			var anode = node as AssemblyTreeNode;
			if (anode != null)
				return GetAssemblyContext(anode.LoadedAssembly.FileName) as ILSpyAssemblyContext;

			return GetAssemblyContext(node.Parent as ILSpyTreeNode);
		}

		private TDef GetDefinitionFromNode<TDef, TNode>(object item, Func<TNode, icAssemblyDefinition> assembly, Func<ILSpyAssemblyContext, TNode, TDef> finder) where TDef : class where TNode : ILSpyTreeNode
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
				context = GetAssemblyContext(node);
			
			return finder(context, node);
		}

		public override MethodDefinition GetMethodDefinition(object item)
		{
			return GetDefinitionFromNode<MethodDefinition, MethodTreeNode>(item, node => node.MethodDefinition.Module.Assembly, (context, node) => context.GetMethodDefinition(node.MethodDefinition));
		}

		public override PropertyDefinition GetPropertyDefinition(object item)
		{
			return GetDefinitionFromNode<PropertyDefinition, PropertyTreeNode>(item, node => node.PropertyDefinition.Module.Assembly, (context, node) => context.GetPropertyDefinition(node.PropertyDefinition));
		}

		public override FieldDefinition GetFieldDefinition(object item)
		{
			return GetDefinitionFromNode<FieldDefinition, FieldTreeNode>(item, node => node.FieldDefinition.Module.Assembly, (context, node) => context.GetFieldDefinition(node.FieldDefinition));
		}

		public override EventDefinition GetEventDefinition(object item)
		{
			return GetDefinitionFromNode<EventDefinition, EventTreeNode>(item, node => node.EventDefinition.Module.Assembly, (context, node) => context.GetEventDefinition(node.EventDefinition));
		}

		public override LinkedResource GetLinkedResource(object item)
		{
			return GetDefinitionFromNode<LinkedResource, ResourceTreeNode>(item, node => null, (context, node) => context.GetResource(node.Resource) as LinkedResource);
		}

		public override EmbeddedResource GetEmbeddedResource(object item)
		{
			return GetDefinitionFromNode<EmbeddedResource, ResourceTreeNode>(item, node => null, (context, node) => context.GetResource(node.Resource) as EmbeddedResource);
		}

		public override AssemblyLinkedResource GetAssemblyLinkedResource(object item)
		{
			return GetDefinitionFromNode<AssemblyLinkedResource, ResourceTreeNode>(item, node => null, (context, node) => context.GetResource(node.Resource) as AssemblyLinkedResource);
		}

		public override AssemblyNameReference GetAssemblyNameReference(object item)
		{
			return GetDefinitionFromNode<AssemblyNameReference, AssemblyReferenceTreeNode>(item, node => null, (context, node) => context.GetAssemblyNameReference(node.AssemblyNameReference));
		}

		public override TypeDefinition GetTypeDefinition(object item)
		{
			return GetDefinitionFromNode<TypeDefinition, TypeTreeNode>(item, node => node.TypeDefinition.Module.Assembly, (context, node) => context.GetTypeDefinition(node.TypeDefinition));
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
			throw new NotImplementedException();
		}

		public override AssemblyDefinition LoadAssembly(string location, bool readsymbols)
		{
			var parameters = new ReaderParameters {ReadSymbols = readsymbols, ReadingMode = ReadingMode.Deferred};
			var resolver = new ReflexilAssemblyResolver();
			try
			{
				return resolver.ReadAssembly(location, parameters);
			}
			catch (Exception)
			{
				// perhaps pdb file is not found, just ignore this time
				if (!readsymbols)
					throw;

				parameters.ReadSymbols = false;
				return resolver.ReadAssembly(location, parameters);
			}
		}

		public override ICollection GetAssemblies(bool wrap)
		{
			if (!wrap)
				return Assemblies;

			var result = new ArrayList();
			foreach (LoadedAssembly loadedAssembly in Assemblies)
				result.Add(new ILSpyAssemblyWrapper(loadedAssembly));

			return result;
		}

		public override void SynchronizeAssemblyContexts(ICollection assemblies)
		{
			var locations = Assemblies.Cast<LoadedAssembly>().Select(l => l.FileName);

			foreach (var location in Assemblycache.Keys.Where(location => !locations.Contains(location)))
				Assemblycache.Remove(location);
		}
	}
}
