using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectinterface.png", Header = "Inject interface", Category = "ReflexilAMInject")]
    internal class InjectInterfaceContextMenu : BaseAssemblyOrModuleContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
        }
    }
}

