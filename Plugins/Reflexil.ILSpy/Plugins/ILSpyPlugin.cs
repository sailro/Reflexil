using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.ILSpy.TreeNodes;
using Mono.Cecil;
using Reflexil.Plugins;

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

		public override bool IsLinkedResourceHandled(object item)
		{
			return item is ResourceTreeNode;
		}

		public override ICollection GetAssemblies(bool wrap)
		{
			throw new NotImplementedException();
		}

		public override bool IsAssemblyNameReferenceHandled(object item)
		{
			return false;
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

		public override bool IsEmbeddedResourceHandled(object item)
		{
			return item is ResourceTreeNode;
		}

		public override bool IsAssemblyLinkedResourceHandled(object item)
		{
			return item is ResourceTreeNode;
		}

		public override LinkedResource GetLinkedResource(object item)
		{
			return null;
		}

		public override MethodDefinition GetMethodDefinition(object item)
		{
			return null;
		}

		public override PropertyDefinition GetPropertyDefinition(object item)
		{
			return null;
		}

		public override FieldDefinition GetFieldDefinition(object item)
		{
			return null;
		}

		public override EventDefinition GetEventDefinition(object item)
		{
			return null;
		}

		public override EmbeddedResource GetEmbeddedResource(object item)
		{
			return null;
		}

		public override AssemblyLinkedResource GetAssemblyLinkedResource(object item)
		{
			return null;
		}

		public override IAssemblyContext GetAssemblyContext(string location)
		{
			return null;
		}

		public override AssemblyDefinition LoadAssembly(string location, bool readsymbols)
		{
			return null;
		}

		public override AssemblyNameReference GetAssemblyNameReference(object item)
		{
			return null;
		}

		public override AssemblyDefinition GetAssemblyDefinition(object item)
		{
			return null;
		}

		public override TypeDefinition GetTypeDefinition(object item)
		{
			return null;
		}

		public override string GetModuleLocation(object item)
		{
			return null;
		}

		public override void SynchronizeAssemblyContexts(ICollection assemblies)
		{
		}
	}
}
