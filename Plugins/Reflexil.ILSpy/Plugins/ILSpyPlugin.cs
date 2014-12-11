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
			var node = item as MethodTreeNode;
			if (node == null)
				return null;

			var icmdef = node.MethodDefinition;
			var context = GetAssemblyContextFromicAssemblyDefinition(icmdef.Module.Assembly);
			return context.GetMethodDefinition(icmdef);
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
			return GetAssemblyContext<ILSpyAssemblyContext>(location);
		}

		public override AssemblyNameReference GetAssemblyNameReference(object item)
		{
			return null;
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

		private ILSpyAssemblyContext GetAssemblyContextFromicAssemblyDefinition(icAssemblyDefinition icadef)
		{
			var node = MainWindow.Instance.FindTreeNode(icadef) as AssemblyTreeNode;
			return node == null ? null : (ILSpyAssemblyContext) GetAssemblyContext(node.LoadedAssembly.FileName);
		}

		public override TypeDefinition GetTypeDefinition(object item)
		{
			var node = item as TypeTreeNode;
			if (node == null)
				return null;

			var ictdef = node.TypeDefinition;
			var context = GetAssemblyContextFromicAssemblyDefinition(ictdef.Module.Assembly);
			return context.GetTypeDefinition(ictdef);
		}

		public override string GetModuleLocation(object item)
		{
			return null;
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
