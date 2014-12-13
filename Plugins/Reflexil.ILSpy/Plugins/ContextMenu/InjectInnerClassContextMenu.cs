using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectinnerclass.png", Header = "Inject inner class", Category = "ReflexilTInnerInject")]
    internal class InjectInnerClassContextMenu : BaseTypeDefinitionContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

