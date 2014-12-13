using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectresource.png", Header = "Inject resource", Category = "ReflexilAMInject")]
    internal class InjectResourceContextMenu : BaseAssemblyOrModuleContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
        }
    }
}

