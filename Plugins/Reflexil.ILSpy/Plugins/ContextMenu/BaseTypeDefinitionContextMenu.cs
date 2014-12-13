using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
    internal abstract class BaseTypeDefinitionContextMenu : BaseContextMenu
    {
	    protected override bool IsVisible(SharpTreeNode node)
        {
            return node is TypeTreeNode;
        }
    }
}

