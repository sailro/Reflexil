/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System;
using System.Drawing;
using Reflector;
#endregion

namespace Reflexil.Plugins.Reflector
{
    class SubMenuUIContext : ButtonUIContext
    {
        public MenuUIContext MenuContext { get; set; }

        public SubMenuUIContext(MenuUIContext menucontext)
            : base(menucontext.Bar, () => menucontext.Item.Items.AddSeparator(), null, null)
        {
            MenuContext = menucontext;
        }

        public SubMenuUIContext(MenuUIContext menucontext, string caption, EventHandler clickHandler, Image image)
            : base(menucontext.Bar, () => menucontext.Item.Items.AddButton(caption, clickHandler) , clickHandler, image )
        {
            MenuContext = menucontext;
        }

        public override void Unload()
        {
            if ((clickHandler != null) && (Item != null))
            {
                Item.Click -= clickHandler;
            }
            if (MenuContext != null)
            {
                MenuContext.Item.Items.Remove(Item);
            }
            Item = null;
            MenuContext = null;
            base.Unload();
        }
    }

    class MenuUIContext : UIContext
    {
        public new ICommandBarMenu Item { 
            get {
                return base.Item as ICommandBarMenu;
            }
            set {
                base.Item = value;
            }
        }

        public MenuUIContext(ICommandBar bar, string identifier, string caption)
            : base(bar, () => bar.Items.AddMenu(identifier, caption))
        {
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

    class ButtonUIContext : UIContext
    {
        public new ICommandBarButton Item { 
            get {
                return base.Item as ICommandBarButton;
            }
            set {
                base.Item = value;
            }
        }

        protected EventHandler clickHandler;

        protected ButtonUIContext(ICommandBar bar, Func<ICommandBarItem> itembuilder, EventHandler clickHandler, Image image) : base(bar, itembuilder, image)
        {
            this.clickHandler = clickHandler;
        }

        public ButtonUIContext(ICommandBar bar, string caption, EventHandler clickHandler, Image image)
            : base(bar, () => bar.Items.AddButton(caption, clickHandler), image)
        {
            this.clickHandler = clickHandler;
        }

        public ButtonUIContext(ICommandBar bar)
            : base(bar, () => bar.Items.AddSeparator())
        {
        }

        public override void Unload()
        {
            if ((clickHandler != null) && (Item != null))
            {
                Item.Click -= clickHandler;
            }
            base.Unload();
        }
    }

    class UIContext
    {
        public ICommandBarItem Item { get; set; }
        public ICommandBar Bar { get; set; }

#if DEBUG
        public static int InstanceCount { get; set; }
#endif

        public UIContext(ICommandBar bar, Func<ICommandBarItem> itembuilder) : this(bar, itembuilder, null)
        {
        }

        public UIContext(ICommandBar bar, Func<ICommandBarItem> itembuilder, Image image)
        {
            this.Item = itembuilder();
            if (image != null)
            {
                Item.Image = image;
            }
            this.Bar = bar;

#if DEBUG
            InstanceCount++;
#endif
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
#if DEBUG
            InstanceCount--;
#endif

        }
    }
}
