using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectstruct.png", Header = "Inject struct", Category = "ReflexilAMInject")]
    internal class InjectStructContextMenu : BaseAssemblyOrModuleContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
        }
    }
}

