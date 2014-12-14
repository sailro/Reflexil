using System;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/rename.png", Header = "Rename...", Category = "ReflexilMember")]
    internal class RenameContextMenu : BaseMemberContextMenu
    {
		internal static void HandleSelectedNodeRenaming(TextViewContext context, Action<TextViewContext> action)
		{
			var instance = MainWindow.Instance;
			var node = context.TreeView.SelectedItem as ILSpyTreeNode;
			var path = instance.GetPathForNode(node);
			var targetObject = ILSpyPackage.ActiveHandler.TargetObject;
			var oldName = RenameHelper.GetName(targetObject);

			action(context);

			var newName = RenameHelper.GetName(targetObject);
			RenamePath(node, path, oldName, newName);

			// Update path to reflect new name
			var newNode = instance.FindNodeByPath(path, true);
			if (newNode == null)
				return;

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

