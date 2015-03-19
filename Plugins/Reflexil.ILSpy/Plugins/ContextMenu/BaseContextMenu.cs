/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using System;
using System.Linq;
using System.Windows.Forms;
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
		    return nodes != null && nodes.Length == 1 && ILSpyPackage.ActiveHandler != null && ILSpyPackage.ActiveHandler.TargetObject != null;
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
		    {
				// Be sure to validate any modifications
			    ILSpyPackage.ReflexilWindow.ValidateChildren(ValidationConstraints.Enabled);

				Execute(node);
			}
	    }

	    internal static ILSpyPackage ILSpyPackage
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

			var newNode = instance.FindNodeByPath(path, false);

			// If not found let's try parent node
			if (newNode == null && path.Length > 1)
			{
				newNode = instance.FindNodeByPath(path.Take(path.Length - 1).ToArray(), false);
				if (oldNode != null)
					oldNode = oldNode.Parent;
			}

			if (newNode == null)
				return;

			instance.SelectNode(newNode);
			newNode.IsExpanded = oldNode != null && oldNode.IsExpanded;
		}

    }
}

