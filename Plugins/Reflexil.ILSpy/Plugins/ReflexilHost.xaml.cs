using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Reflexil.Plugins;
using Panel = System.Windows.Forms.Panel;

namespace Reflexil.ILSpy.Plugins
{
	public partial class ReflexilHost : UserControl
	{
		private readonly IPackage _package;

		public ReflexilHost(IPackage package)
		{
			InitializeComponent();

			_package = package;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			var hostPanel = new Panel();
			hostPanel.Controls.Add(_package.ReflexilWindow);

			var host = new WindowsFormsHost {Child = hostPanel};
			Root.Children.Add(host);
		}

		private void OnRootSizeChanged(object sender, SizeChangedEventArgs e)
		{
			_package.ReflexilWindow.Width = (int) Root.ActualWidth;
			_package.ReflexilWindow.Height = (int) Root.ActualHeight;
		}
	}
}
