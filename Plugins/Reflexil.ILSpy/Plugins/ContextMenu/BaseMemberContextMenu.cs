using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	internal abstract class BaseMemberContextMenu : BaseContextMenu
	{
		public override bool IsVisible(SharpTreeNode node)
		{
			return node is EventTreeNode
			       || node is FieldTreeNode
			       || node is MethodTreeNode
			       || node is PropertyTreeNode
			       || node is TypeTreeNode
				   || node is AssemblyReferenceTreeNode
			       || node is ResourceTreeNode;
		}
	}
}

