using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectclass.png", Header = "Inject class", Category = "ReflexilAMInject")]
    internal class InjectClassContextMenu : BaseAssemblyOrModuleContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
        }
    }
}

