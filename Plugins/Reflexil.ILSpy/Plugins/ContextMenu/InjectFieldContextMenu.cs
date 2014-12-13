using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectfield.png", Header = "Inject field", Category = "ReflexilTInject")]
    internal class InjectFieldContextMenu : BaseTypeDefinitionContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
        }
    }
}

