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
