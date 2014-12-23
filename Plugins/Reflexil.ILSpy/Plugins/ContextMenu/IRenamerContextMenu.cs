using System;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	interface IRenamerContextMenu
	{
		SharpTreeNode FindRenamedNode(ILSpyTreeNode oldNode, string[] path, string oldName, object targetObject);
	}

	public static class RenamerContextMenuExtensions
	{
		internal static void RenameSelectedNode(this IRenamerContextMenu item, TextViewContext context, Action<TextViewContext> action)
		{
			var treeView = context.TreeView;
			var activeHandler = BaseContextMenu.ILSpyPackage.ActiveHandler;
			if (treeView == null || activeHandler == null)
				return;

			var targetObject = activeHandler.TargetObject;
			if (targetObject == null)
				return;

			var instance = MainWindow.Instance;
			var oldNode = treeView.SelectedItem as ILSpyTreeNode;
			var path = instance.GetPathForNode(oldNode);
			var oldName = RenameHelper.GetName(targetObject);

			action(context);

			var newNode = item.FindRenamedNode(oldNode, path, oldName, targetObject);
			if (newNode == null) 
				return;

			instance.SelectNode(newNode);
			newNode.IsExpanded = oldNode != null && oldNode.IsExpanded;
		}
	
	}

}
