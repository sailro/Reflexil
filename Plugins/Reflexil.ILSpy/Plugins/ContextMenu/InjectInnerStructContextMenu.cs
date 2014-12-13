using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectinnerstruct.png", Header = "Inject inner struct", Category = "ReflexilTInnerInject")]
    internal class InjectInnerStructContextMenu : BaseTypeDefinitionContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
        }
    }
}

