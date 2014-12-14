using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectresource.png", Header = "Inject resource", Category = "ReflexilAMInject")]
    internal class InjectResourceContextMenu : BaseAssemblyOrModuleContextMenu
    {
		public override void Execute(TextViewContext context)
		{
			PreserveNodeSelection(context, () => base.Execute(context));
		}

		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.Inject(InjectType.Resource);
        }
    }
}

