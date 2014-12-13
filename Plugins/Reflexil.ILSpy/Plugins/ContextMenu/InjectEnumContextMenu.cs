using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectenum.png", Header = "Inject enum", Category = "ReflexilAMInject")]
    internal class InjectEnumContextMenu : BaseAssemblyOrModuleContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

