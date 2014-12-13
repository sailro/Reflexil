using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectinnerstruct.png", Header = "Inject inner struct", Category = "ReflexilTInnerInject")]
    internal class InjectInnerStructContextMenu : BaseTypeDefinitionContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.Inject(InjectType.Struct);
        }
    }
}

