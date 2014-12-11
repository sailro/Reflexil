using ICSharpCode.ILSpy;
using Reflexil.Wrappers;

namespace Reflexil.ILSpy.Plugins
{
	class ILSpyAssemblyWrapper : IAssemblyWrapper
	{
		private readonly LoadedAssembly _loadedAssembly;

		public string Location
		{
			get { return _loadedAssembly != null ? _loadedAssembly.FileName : string.Empty; }
		}

		public bool IsValid
		{
			get { return _loadedAssembly != null && _loadedAssembly.AssemblyDefinition != null; }
		}

		public ILSpyAssemblyWrapper(LoadedAssembly loadedAssembly)
		{
			_loadedAssembly = loadedAssembly;
		}

		public override string ToString()
		{
			return IsValid ? _loadedAssembly.AssemblyDefinition.Name.Name : string.Empty;
		}
	}
}
