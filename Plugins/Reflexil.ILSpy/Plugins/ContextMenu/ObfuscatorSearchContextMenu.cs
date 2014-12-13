using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/obfuscator.png", Header = "Obfuscator search...", Category = "ReflexilMain")]
	internal class ObfuscatorSearchContextMenu : BaseAssemblyOrModuleContextMenu
    {
        public override void Execute(SharpTreeNode node)
        {
        }
    }
}

