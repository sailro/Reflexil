using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectmethod.png", Header = "Inject method", Category = "ReflexilTInject")]
    internal class InjectMethodContextMenu : BaseTypeDefinitionContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

