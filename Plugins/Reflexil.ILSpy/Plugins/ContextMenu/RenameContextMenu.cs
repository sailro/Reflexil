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
	[ExportContextMenuEntry(Icon = "resources/rename.png", Header = "Rename...", Category = "ReflexilMember", Order = 10)]
    internal class RenameContextMenu : BaseMemberContextMenu, IRenamerContextMenu
    {
		public override void Execute(TextViewContext context)
		{
			this.RenameSelectedNode(context, base.Execute);
		}

		protected override void Execute(SharpTreeNode node)
		{
			ILSpyPackage.RenameItem(node, EventArgs.Empty);
		}

		SharpTreeNode IRenamerContextMenu.FindRenamedNode(ILSpyTreeNode oldNode, string[] path, string oldName, object targetObject)
		{
			var newName = RenameHelper.GetName(targetObject);
			RenamePath(oldNode, path, oldName, newName);

			return MainWindow.Instance.FindNodeByPath(path, true);
		}

		private static void TypeParts(string fullname, out string ns, out string name)
		{
			if (fullname != null && fullname.Contains("."))
			{
				var offset = fullname.LastIndexOf(".", StringComparison.Ordinal);
				ns = fullname.Substring(0, offset);
				name = fullname.Substring(offset + 1);
			}
			else
			{
				ns = "-";
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
    }
}

