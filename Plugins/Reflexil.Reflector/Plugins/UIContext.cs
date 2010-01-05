using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reflector;
using System.Drawing;

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
        }

        public virtual void Unload()
        {
            if (Bar != null)
            {
                if (Item != null)
                {
                    Bar.Items.Remove(Item);
                    Item = null;
                }
            }
        }
    }
}
