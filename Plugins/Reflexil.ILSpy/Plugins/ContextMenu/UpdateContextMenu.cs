using System;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/update.png", Header = "Update ILSpy object model", Category = "ReflexilMain")]
	internal class UpdateContextMenu : BaseContextMenu
    {
		public override void Execute(TextViewContext context)
		{
			var instance = MainWindow.Instance;
			var path = instance.GetPathForNode(context.TreeView.SelectedItem as SharpTreeNode);
			base.Execute(context);
			instance.SelectNode(instance.FindNodeByPath(path, true));
		}

		protected override void Execute(SharpTreeNode node)
		{
			ILSpyPackage.UpdateILSpyObjectModel(this, EventArgs.Empty);
		}

		protected override bool IsVisible(SharpTreeNode node)
		{
			return ILSpyPackage.ActiveHandler != null && ILSpyPackage.ActiveHandler.TargetObject != null;
		}
    }
}

