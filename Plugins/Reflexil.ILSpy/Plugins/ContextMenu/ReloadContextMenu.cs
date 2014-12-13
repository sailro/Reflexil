using System;
using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/reload.png", Header = "Reload Reflexil codemodel", Category = "ReflexilMain")]
	internal class ReloadContextMenu : BaseAssemblyOrModuleContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.ReloadAssembly(node, EventArgs.Empty);
        }
    }
}

