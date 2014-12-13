using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/rename.png", Header = "Rename...", Category = "ReflexilMember")]
    internal class RenameContextMenu : BaseMemberContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

