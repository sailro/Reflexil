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
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using JustDecompile.API.Core;
using Reflexil.Plugins;

namespace Reflexil.JustDecompile.Plugins.ContextMenu
{
	internal class MenuItem : IMenuItem
	{
		public object Header { get; set; }
		public string IconFile { get; set; }

		public object Icon
		{
			get
			{
				if (string.IsNullOrEmpty(IconFile))
					return null;

				return new Image
				{
					Source =
						new BitmapImage(new Uri("pack://application:,,,/Reflexil.JustDecompile.Module;component/Resources/" + IconFile))
				};
			}
		}

		public ICommand Command { get; set; }
		public IList<IMenuItem> MenuItems { get; set; }

		public static JustDecompilePackage JustDecompilePackage
		{
			get { return (JustDecompilePackage) PluginFactory.GetInstance().Package; }
		}

		public MenuItem()
		{
			Header = BasePackage.ReflexilButtonText;
			IconFile = "reflexil.png";
			MenuItems = new List<IMenuItem>();
		}
	}
}