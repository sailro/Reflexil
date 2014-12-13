using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
    internal abstract class BaseAssemblyOrModuleContextMenu : BaseContextMenu
    {
        public override bool IsVisible(SharpTreeNode node)
        {
            return node is AssemblyTreeNode;
        }
    }
}

