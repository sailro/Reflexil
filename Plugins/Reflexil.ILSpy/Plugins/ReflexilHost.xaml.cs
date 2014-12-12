/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Reflexil.Plugins;
using Panel = System.Windows.Forms.Panel;

namespace Reflexil.ILSpy.Plugins
{
	public partial class ReflexilHost
	{
		private readonly IPackage _package;
		private readonly WindowsFormsHost _host;

		public ReflexilHost(IPackage package)
		{
			InitializeComponent();

			_package = package;

			var hostPanel = new Panel();
			hostPanel.Controls.Add(_package.ReflexilWindow);

			_host = new WindowsFormsHost { Child = hostPanel };
			Root.Children.Add(_host);
		}

		private void OnRootSizeChanged(object sender, SizeChangedEventArgs e)
		{
			_package.ReflexilWindow.Width = (int) Root.ActualWidth;
			_package.ReflexilWindow.Height = (int) Root.ActualHeight;
		}
	}
}
