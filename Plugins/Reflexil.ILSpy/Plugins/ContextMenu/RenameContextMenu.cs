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

			// After renaming an assembly, ILSpy is still using the filename to display node text, even if asse
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

