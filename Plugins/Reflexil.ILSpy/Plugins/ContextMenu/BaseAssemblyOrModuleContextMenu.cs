using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
    internal abstract class BaseAssemblyOrModuleContextMenu : BaseContextMenu
    {
	    protected override bool IsVisible(SharpTreeNode node)
        {
            return node is AssemblyTreeNode;
        }
    }
}

