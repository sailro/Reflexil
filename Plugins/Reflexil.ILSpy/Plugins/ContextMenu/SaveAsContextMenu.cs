using System;
using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/saveas.png", Header = "Save as...", Category = "ReflexilMain")]
    internal class SaveAsContextMenu : BaseAssemblyOrModuleContextMenu
    {
		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.SaveAssembly(node, EventArgs.Empty);
        }
    }
}

