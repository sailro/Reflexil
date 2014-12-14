using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectinterface.png", Header = "Inject interface", Category = "ReflexilAMInject")]
    internal class InjectInterfaceContextMenu : BaseAssemblyOrModuleContextMenu
    {
		public override void Execute(TextViewContext context)
		{
			PreserveNodeSelection(context, () => base.Execute(context));
		}

		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.Inject(InjectType.Interface);
        }
    }
}

