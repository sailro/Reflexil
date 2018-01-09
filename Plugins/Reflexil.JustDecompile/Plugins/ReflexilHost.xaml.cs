/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Reflexil.JustDecompile;

namespace Reflexil.Plugins.JustDecompile
{

	public partial class ReflexilHost
	{
		private readonly JustDecompilePackage _package;

		public ReflexilHost()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}

		public ReflexilHost(JustDecompilePackage package, Action onCloseExecuted) : this()
		{
			Close.Command = new DelegateCommand(onCloseExecuted);

			_package = package;
			Header.Text = BasePackage.ReflexilWindowText;
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
			var ct = GetCompositionTarget(Root);
			var dips = new Vector(Root.ActualWidth, Root.ActualHeight);
			var pixels = ct.TransformToDevice.Transform(dips);

			_package.ReflexilWindow.Width = (int) pixels.X;
			_package.ReflexilWindow.Height = (int) pixels.Y;
		}

		private static CompositionTarget GetCompositionTarget(Visual control)
		{
			{
				var source = PresentationSource.FromVisual(control);
				if (source != null)
					return source.CompositionTarget;
			}

			using (var source = new HwndSource(new HwndSourceParameters()))
				return source.CompositionTarget;
		}
	}
}