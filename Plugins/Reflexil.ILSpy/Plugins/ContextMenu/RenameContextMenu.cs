using System;
using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/rename.png", Header = "Rename...", Category = "ReflexilMember")]
    internal class RenameContextMenu : BaseMemberContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.RenameItem(node, EventArgs.Empty);
        }
    }
}

