using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectproperty.png", Header = "Inject property", Category = "ReflexilTInject")]
    internal class InjectPropertyContextMenu : BaseTypeDefinitionContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
        }
    }
}

