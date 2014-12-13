using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectassemblyreference.png", Header = "Inject assembly reference", Category = "ReflexilAMInject")]
    internal class InjectAssemblyReferenceContextMenu : BaseAssemblyOrModuleContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

