using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/reload.png", Header = "Reload", Category = "ReflexilMain")]
	internal class ReloadContextMenu : BaseAssemblyOrModuleContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

