using ICSharpCode.ILSpy;
using ICSharpCode.TreeView;
using Reflexil.Utils;

namespace Reflexil.Plugins.ILSpy.ContextMenu
{
	[ExportContextMenuEntry(Icon = "resources/injectassemblyreference.png", Header = "Inject assembly reference", Category = "ReflexilAMInject")]
    internal class InjectAssemblyReferenceContextMenu : BaseAssemblyOrModuleContextMenu
    {
		public override void Execute(TextViewContext context)
		{
			PreserveNodeSelection(context, () => base.Execute(context));
		}

		protected override void Execute(SharpTreeNode node)
        {
			ILSpyPackage.Inject(InjectType.AssemblyReference);
        }
    }
}

