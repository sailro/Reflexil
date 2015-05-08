/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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
using System.Drawing;
using Reflector;

namespace Reflexil.Plugins.Reflector
{
	internal class SubMenuUIContext : ButtonUIContext
	{
		private MenuUIContext MenuContext { get; set; }

		public SubMenuUIContext(MenuUIContext menucontext)
			: base(menucontext.Bar, () => menucontext.Item.Items.AddSeparator(), null, null)
		{
			MenuContext = menucontext;
		}

		public SubMenuUIContext(MenuUIContext menucontext, string caption, EventHandler clickHandler, Image image)
			: base(menucontext.Bar, () => menucontext.Item.Items.AddButton(caption, clickHandler), clickHandler, image)
		{
			MenuContext = menucontext;
		}

		public override void Unload()
		{
			if ((ClickHandler != null) && (Item != null))
				Item.Click -= ClickHandler;

			if (MenuContext != null)
				MenuContext.Item.Items.Remove(Item);

			Item = null;
			MenuContext = null;
			base.Unload();
		}
	}

	internal class MenuUIContext : UIContext
	{
		public new ICommandBarMenu Item
		{
			get { return base.Item as ICommandBarMenu; }
		}

		public MenuUIContext(ICommandBar bar, string identifier, string caption, Image image)
			: base(bar, () => bar.Items.AddMenu(identifier, caption, image))
		{
		}

		public MenuUIContext(ICommandBar bar)
			: base(bar, () => bar.Items.AddSeparator())
		{
		}
	}

	internal class ButtonUIContext : UIContext
	{
		protected new ICommandBarButton Item
		{
			get { return base.Item as ICommandBarButton; }
			set { base.Item = value; }
		}

		protected readonly EventHandler ClickHandler;

		protected ButtonUIContext(ICommandBar bar, Func<ICommandBarItem> itembuilder, EventHandler clickHandler, Image image)
			: base(bar, itembuilder, image)
		{
			ClickHandler = clickHandler;
		}

		public ButtonUIContext(ICommandBar bar, string caption, EventHandler clickHandler, Image image)
			: base(bar, () => bar.Items.AddButton(caption, clickHandler), image)
		{
			ClickHandler = clickHandler;
		}

		public ButtonUIContext(ICommandBar bar)
			: base(bar, () => bar.Items.AddSeparator())
		{
		}

		public override void Unload()
		{
			if (ClickHandler != null && Item != null)
				Item.Click -= ClickHandler;

			base.Unload();
		}
	}

	internal class UIContext
	{
		protected ICommandBarItem Item { get; set; }
		public ICommandBar Bar { get; private set; }
		public static int InstanceCount { get; private set; }

		protected UIContext(ICommandBar bar, Func<ICommandBarItem> itembuilder, Image image = null)
		{
			Item = itembuilder();
			if (image != null)
				Item.Image = image;

			// Reflector is hiding separators in SubMenuItem ! Workaround that
			if (Item is ICommandBarSeparator)
			{
				Item.PropertyChanged += (sender, args) => {
					if (!Item.Visible)
						Item.Visible = true;
				};
			}

			Bar = bar;

			InstanceCount++;
		}

		public virtual void Unload()
		{
			if (Bar != null)
			{
				if (Item != null)
				{
					Bar.Items.Remove(Item);
					Item = null;
					Bar = null;
				}
			}
			InstanceCount--;
		}
	}
}