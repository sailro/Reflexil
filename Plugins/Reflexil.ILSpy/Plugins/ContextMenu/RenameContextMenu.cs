/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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
using System.Reflection;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;
using Mono.Cecil;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/rename.png", Header = "Rename...", Category = "ReflexilMember")]
    internal class RenameContextMenu : BaseMemberContextMenu
    {
		internal static void HandleSelectedNodeRenaming(TextViewContext context, Action<TextViewContext> action)
		{
			var treeView = context.TreeView;
			if (treeView == null)
				return;

			var instance = MainWindow.Instance;
			var node = treeView.SelectedItem as ILSpyTreeNode;
			var path = instance.GetPathForNode(node);
			var targetObject = ILSpyPackage.ActiveHandler.TargetObject;
			var oldName = RenameHelper.GetName(targetObject);

			action(context);

			// After renaming an assembly, ILSpy is still using the filename to display node text, even if assembly name changed
			if (!(node is AssemblyTreeNode))
			{
				var newName = RenameHelper.GetName(targetObject);
				RenamePath(node, path, oldName, newName);
			}

			// Update path to reflect new name
			var newNode = instance.FindNodeByPath(path, true);
			if (newNode == null)
				return;

			// Hack, so we have to change the shortname, without changing the filename, so that the user can reload the previous state
			var adef = targetObject as AssemblyDefinition;
			if (newNode is AssemblyTreeNode && adef != null)
			{
				var la = (newNode as AssemblyTreeNode).LoadedAssembly;
				var pInfo = la.GetType().GetField("shortName", BindingFlags.Instance | BindingFlags.NonPublic);
				if (pInfo != null)
				{
					pInfo.SetValue(la, adef.Name.Name);
					newNode.RaisePropertyChanged("Text");					
				}
			}

			instance.SelectNode(newNode);
			newNode.IsExpanded = true;
		}

		public override void Execute(TextViewContext context)
		{
			HandleSelectedNodeRenaming(context, base.Execute);
		}

		private static void TypeParts(string fullname, out string ns, out string name)
		{
			if (fullname.Contains("."))
			{
				var offset = fullname.LastIndexOf(".", StringComparison.Ordinal);
				ns = fullname.Substring(0, offset);
				name = fullname.Substring(offset + 1);
			}
			else
			{
				ns = string.Empty;
				name = fullname;
			}
		}

		private static void RenamePath(ILSpyTreeNode node, string[] path, string oldName, string newName)
		{
			if (node is TypeTreeNode)
			{
				string oldns;
				TypeParts(oldName, out oldns, out oldName);

				string newns;
				TypeParts(newName, out newns, out newName);

				if (path.Length > 1)
					path[path.Length - 2] = path[path.Length - 2].Replace(oldns, newns);
			}

			if (path.Length > 0)
				path[path.Length - 1] = path[path.Length - 1].Replace(oldName, newName);
		}

		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.RenameItem(node, EventArgs.Empty);
        }
    }
}

