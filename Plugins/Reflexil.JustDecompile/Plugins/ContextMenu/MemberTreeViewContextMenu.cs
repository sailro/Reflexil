﻿/* Reflexil Copyright (c) 2007-2016 Sebastien LEBRETON

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
using Microsoft.Practices.Prism.Commands;

namespace Reflexil.JustDecompile.Plugins.ContextMenu
{
	internal class MemberTreeViewContextMenu : MenuItem
	{
		public MemberTreeViewContextMenu()
		{
			InitializeMenuItems(this);
		}

		public static void InitializeMenuItems(MenuItem item)
		{
			item.MenuItems.Add(new MenuItem
			{
				Header = "Rename...",
				IconFile = "rename.png",
				Command = new DelegateCommand(() => JustDecompilePackage.RenameItem(item, EventArgs.Empty))
			});

			item.MenuItems.Add(new MenuItem
			{
				Header = "Delete",
				IconFile = "delete.png",
				Command = new DelegateCommand(() => JustDecompilePackage.DeleteItem(item, EventArgs.Empty))
			});

			item.MenuItems.Add(new MenuSeparator());

			item.MenuItems.Add(new MenuItem
			{
				Header = "Update JustDecompile object model",
				IconFile = "update.png",
				Command = new DelegateCommand(() => JustDecompilePackage.UpdateHostObjectModel(item, EventArgs.Empty))
			});
		}
	}
}