using System;
using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/verify.png", Header = "Verify", Category = "ReflexilMain")]
    internal class VerifyContextMenu : BaseAssemblyOrModuleContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.VerifyAssembly(node, EventArgs.Empty);
        }
    }
}

