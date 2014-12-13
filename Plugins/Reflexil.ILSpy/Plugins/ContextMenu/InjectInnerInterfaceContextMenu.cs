using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectinnerinterface.png", Header = "Inject inner interface", Category = "ReflexilTInnerInject")]
    internal class InjectInnerInterfaceContextMenu : BaseTypeDefinitionContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

