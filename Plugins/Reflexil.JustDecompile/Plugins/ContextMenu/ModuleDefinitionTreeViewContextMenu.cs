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
using Microsoft.Practices.Prism.Commands;
using Reflexil.Utils;

namespace Reflexil.JustDecompile.Plugins.ContextMenu
{
	internal class ModuleDefinitionTreeViewContextMenu : MenuItem
	{
		public ModuleDefinitionTreeViewContextMenu()
		{
			MenuItems.Add(new MenuItem
			{
				Header = "Inject assembly reference",
				IconFile = "injectassemblyreference.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.AssemblyReference))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject class",
				IconFile = "injectclass.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Class))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject enum",
				IconFile = "injectenum.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Enum))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject interface",
				IconFile = "injectinterface.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Interface))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject struct",
				IconFile = "injectstruct.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Struct))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Inject resource",
				IconFile = "injectresource.png",
				Command = new DelegateCommand(() => JustDecompilePackage.Inject(InjectType.Resource))
			});

			MenuItems.Add(new MenuSeparator());

			MenuItems.Add(new MenuItem
			{
				Header = "Reload Reflexil object model",
				IconFile = "reload.png",
				Command = new DelegateCommand(() => JustDecompilePackage.ReloadAssembly(this, EventArgs.Empty))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Update JustDecompile object model",
				IconFile = "reload.png",
				Command = new DelegateCommand(() => JustDecompilePackage.UpdateHostObjectModel(this, EventArgs.Empty))
			});

			MenuItems.Add(new MenuSeparator());

			MenuItems.Add(new MenuItem
			{
				Header = "Obfuscator search...",
				IconFile = "obfuscator.png",
				Command = new DelegateCommand(() => JustDecompilePackage.SearchObfuscator(this, EventArgs.Empty))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Rename...",
				IconFile = "rename.png",
				Command = new DelegateCommand(() => JustDecompilePackage.RenameItem(this, EventArgs.Empty))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Save as...",
				IconFile = "saveas.png",
				Command = new DelegateCommand(() => JustDecompilePackage.SaveAssembly(this, EventArgs.Empty))
			});

			MenuItems.Add(new MenuItem
			{
				Header = "Verify",
				IconFile = "verify.png",
				Command = new DelegateCommand(() => JustDecompilePackage.VerifyAssembly(this, EventArgs.Empty))
			});
		}
	}
}