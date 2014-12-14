using System;
using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/rename.png", Header = "Rename...", Category = "ReflexilMain")]
    internal class RenameAssemblyContextMenu : BaseAssemblyOrModuleContextMenu
    {
		public override void Execute(TextViewContext context)
		{
			RenameContextMenu.HandleSelectedNodeRenaming(context, base.Execute);
		}

		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.RenameItem(node, EventArgs.Empty);
        }
    }
}

