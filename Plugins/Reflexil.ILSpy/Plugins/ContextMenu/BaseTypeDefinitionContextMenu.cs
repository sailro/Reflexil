using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
    internal abstract class BaseTypeDefinitionContextMenu : BaseContextMenu
    {
        public override bool IsVisible(SharpTreeNode node)
        {
            return node is TypeTreeNode;
        }
    }
}

