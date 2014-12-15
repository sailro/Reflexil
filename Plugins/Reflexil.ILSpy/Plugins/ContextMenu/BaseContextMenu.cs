using System;
using System.Linq;
using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
    internal abstract class BaseContextMenu : IContextMenuEntry
    {
	    protected abstract void Execute(SharpTreeNode node);
	    protected abstract bool IsVisible(SharpTreeNode node);

	    public bool IsEnabled(TextViewContext context)
	    {
		    var nodes = context.SelectedTreeNodes;
		    return nodes != null && nodes.Length == 1;
	    }

	    public bool IsVisible(TextViewContext context)
		{
			return context.TreeView != null && IsVisible(context.TreeView.SelectedItem as SharpTreeNode);
		}

	    public virtual void Execute(TextViewContext context)
	    {
		    var treeView = context.TreeView;
		    if (treeView == null)
			    return;

		    var node = treeView.SelectedItem as SharpTreeNode;
			if (node != null)
				Execute(node);
	    }

	    protected static ILSpyPackage ILSpyPackage
	    {
		    get { return PluginFactory.GetInstance().Package as ILSpyPackage; }
	    }

		protected static void PreserveNodeSelection(TextViewContext context, Action action)
		{
			var treeView = context.TreeView;
			if (treeView == null)
				return;
	
			var instance = MainWindow.Instance;
			var oldNode = treeView.SelectedItem as SharpTreeNode;
			var path = instance.GetPathForNode(oldNode);

			action();

			var newNode = instance.FindNodeByPath(path, true);
			if (newNode == null)
				return;

			instance.SelectNode(newNode);
			newNode.IsExpanded = true;
		}

    }
}

