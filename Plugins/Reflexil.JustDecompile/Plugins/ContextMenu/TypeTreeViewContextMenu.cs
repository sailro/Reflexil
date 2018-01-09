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

using Microsoft.Practices.Prism.Commands;
using Reflexil.Utils;

namespace Reflexil.JustDecompile.Plugins.ContextMenu
{
	internal class TypeTreeViewContextMenu : MenuItem
	{
		public TypeTreeViewContextMenu()
		{
			MenuItems.Add(new MenuItem
			{
				Header = "Inject constructor",
				IconFile = "injectconstructor.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Constructor))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject event",
				IconFile = "injectevent.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Event))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject field",
				IconFile = "injectfield.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Field))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject method",
				IconFile = "injectmethod.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Method))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject property",
				IconFile = "injectproperty.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Property))
			});

			MenuItems.Add(new MenuSeparator());

			MenuItems.Add(new MenuItem
			{
				Header = "Inject inner class",
				IconFile = "injectinnerclass.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Class))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject inner enum",
				IconFile = "injectinnerenum.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Enum))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject inner interface",
				IconFile = "injectinnerinterface.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Interface))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject inner struct",
				IconFile = "injectinnerstruct.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Struct))
			});

			MenuItems.Add(new MenuSeparator());
			MemberTreeViewContextMenu.InitializeMenuItems(this);
		}
	}
}