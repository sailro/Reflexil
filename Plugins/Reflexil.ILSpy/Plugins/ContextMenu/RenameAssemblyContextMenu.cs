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
using System.Reflection;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;
using Mono.Cecil;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/rename.png", Header = "Rename...", Category = "ReflexilMain", Order = 1100)]
	internal class RenameAssemblyContextMenu : BaseAssemblyOrModuleContextMenu, IRenamerContextMenu
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
			// After renaming an assembly, ILSpy is still using the filename to display node text, even if assembly name changed
			var newNode = MainWindow.Instance.FindNodeByPath(path, true);
			if (newNode == null)
				return null;

			// Hack, so we have to change the shortname, without changing the filename, so that the user can reload the previous state
			var adef = targetObject as AssemblyDefinition;
			if (!(newNode is AssemblyTreeNode) || adef == null) 
				return newNode;

			var la = (newNode as AssemblyTreeNode).LoadedAssembly;
			var pInfo = la.GetType().GetField("shortName", BindingFlags.Instance | BindingFlags.NonPublic);
			if (pInfo == null) 
				return newNode;

			pInfo.SetValue(la, adef.Name.Name);
			newNode.RaisePropertyChanged("Text");

			return newNode;
		}
    }
}

