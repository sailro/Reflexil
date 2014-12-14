using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectevent.png", Header = "Inject event", Category = "ReflexilTInject")]
    internal class InjectEventContextMenu : BaseTypeDefinitionContextMenu
    {
		public override void Execute(TextViewContext context)
		{
			PreserveNodeSelection(context, () => base.Execute(context));
		}

		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.Inject(InjectType.Event);
        }
    }
}

