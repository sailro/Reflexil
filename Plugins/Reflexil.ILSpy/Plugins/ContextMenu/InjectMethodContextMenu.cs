using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectmethod.png", Header = "Inject method", Category = "ReflexilTInject")]
    internal class InjectMethodContextMenu : BaseTypeDefinitionContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.Inject(InjectType.Method);
        }
    }
}

