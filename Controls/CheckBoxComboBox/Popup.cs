using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using VS = System.Windows.Forms.VisualStyles;

/*
<li>Base class for custom tooltips.</li>
<li>Office-2007-like tooltip class.</li>
*/
namespace Reflexil.Editors
{
    /// <summary>
    /// CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".
    /// Represents a pop-up window.
    /// </summary>
    [ToolboxItem(false)]
    public sealed partial class Popup : ToolStripDropDown
    {
        #region Fields & Properties

	    /// <summary>
	    /// Gets the content of the pop-up.
	    /// </summary>
	    public Control Content { get; private set; }

	    /// <summary>
	    /// Gets a value indicating whether the Popup uses the fade effect.
	    /// </summary>
	    /// <value><c>true</c> if pop-up uses the fade effect; otherwise, <c>false</c>.</value>
	    /// <remarks>To use the fade effect, the FocusOnOpen property also has to be set to <c>true</c>.</remarks>
	    public bool UseFadeEffect { get; set; }

	    /// <summary>
	    /// Gets or sets a value indicating whether to focus the content after the pop-up has been opened.
	    /// </summary>
	    /// <value><c>true</c> if the content should be focused after the pop-up has been opened; otherwise, <c>false</c>.</value>
	    /// <remarks>If the FocusOnOpen property is set to <c>false</c>, then pop-up cannot use the fade effect.</remarks>
	    public bool FocusOnOpen { get; set; }

	    /// <summary>
	    /// Gets or sets a value indicating whether presing the alt key should close the pop-up.
	    /// </summary>
	    /// <value><c>true</c> if presing the alt key does not close the pop-up; otherwise, <c>false</c>.</value>
	    public bool AcceptAlt { get; set; }

	    private Popup _ownerPopup;
        private Popup _childPopup;

        private bool _allowResizable;
        private bool _resizable;
        /// <summary>
        /// Gets or sets a value indicating whether this Popup is _resizable.
        /// </summary>
        /// <value><c>true</c> if resizable; otherwise, <c>false</c>.</value>
        public bool Resizable
        {
            get { return _resizable && _allowResizable; }
            set { _resizable = value; }
        }

	    /// <summary>
	    /// Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.
	    /// </summary>
	    /// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	    public new Size MinimumSize { get; set; }

	    /// <summary>
	    /// Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.
	    /// </summary>
	    /// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	    public new Size MaximumSize { get; set; }

	    /// <summary>
        /// Gets parameters of a new window.
        /// </summary>
        /// <returns>An object of type <see cref="T:System.Windows.Forms.CreateParams" /> used when creating a new window.</returns>
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= NativeMethods.WS_EX_NOACTIVATE;
                return cp;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Popup class.
        /// </summary>
        /// <param name="content">The content of the pop-up.</param>
        /// <remarks>
        /// Pop-up will be disposed immediately after disposion of the content control.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="content" /> is <code>null</code>.</exception>
        public Popup(Control content)
        {
	        AcceptAlt = true;
	        FocusOnOpen = true;
	        if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            Content = content;
            UseFadeEffect = SystemInformation.IsMenuAnimationEnabled && SystemInformation.IsMenuFadeEnabled;
            _allowResizable = true;
            InitializeComponent();
            AutoSize = false;
            DoubleBuffered = true;
            ResizeRedraw = true;
            var host = new ToolStripControlHost(content);
            Padding = Margin = host.Padding = host.Margin = Padding.Empty;
            MinimumSize = content.MinimumSize;
            content.MinimumSize = content.Size;
            MaximumSize = content.MaximumSize;
            content.MaximumSize = content.Size;
            Size = content.Size;
            content.Location = Point.Empty;
            Items.Add(host);
            content.Disposed += delegate
            {
                content = null;
                Dispose(true);
            };
            content.RegionChanged += (sender, e) => UpdateRegion();
            content.Paint += (sender, e) => PaintSizeGrip(e);
            UpdateRegion();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes a dialog box key.
        /// </summary>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <returns>
        /// true if the key was processed by the control; otherwise, false.
        /// </returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (AcceptAlt && ((keyData & Keys.Alt) == Keys.Alt)) return false;
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Updates the pop-up region.
        /// </summary>
        private void UpdateRegion()
        {
            if (Region != null)
            {
                Region.Dispose();
                Region = null;
            }
            if (Content.Region != null)
            {
                Region = Content.Region.Clone();
            }
        }

        /// <summary>
        /// Shows pop-up window below the specified control.
        /// </summary>
        /// <param name="control">The control below which the pop-up will be shown.</param>
        /// <remarks>
        /// When there is no space below the specified control, the pop-up control is shown above it.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="control"/> is <code>null</code>.</exception>
        public void Show(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            SetOwnerItem(control);
            Show(control, control.ClientRectangle);
        }

        /// <summary>
        /// Shows pop-up window below the specified area of specified control.
        /// </summary>
        /// <param name="control">The control used to compute screen location of specified area.</param>
        /// <param name="area">The area of control below which the pop-up will be shown.</param>
        /// <remarks>
        /// When there is no space below specified area, the pop-up control is shown above it.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="control"/> is <code>null</code>.</exception>
        public void Show(Control control, Rectangle area)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            SetOwnerItem(control);
            _resizableTop = _resizableRight = false;
            var location = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
            var screen = Screen.FromControl(control).WorkingArea;
            if (location.X + Size.Width > (screen.Left + screen.Width))
            {
                _resizableRight = true;
                location.X = (screen.Left + screen.Width) - Size.Width;
            }
            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                _resizableTop = true;
                location.Y -= Size.Height + area.Height;
            }
            location = control.PointToClient(location);
            Show(control, location, ToolStripDropDownDirection.BelowRight);
        }

        private const int Frames = 1;
        private const int Totalduration = 0; // ML : 2007-11-05 : was 100 but caused a flicker.
        private const int Frameduration = Totalduration / Frames;
        /// <summary>
        /// Adjusts the size of the owner <see cref="T:System.Windows.Forms.ToolStrip" /> to accommodate the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed, or clears and resets active <see cref="T:System.Windows.Forms.ToolStripDropDown" /> child controls of the <see cref="T:System.Windows.Forms.ToolStrip" /> if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not currently displayed.
        /// </summary>
        /// <param name="visible">true if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed; otherwise, false.</param>
        protected override void SetVisibleCore(bool visible)
        {
            double opacity = Opacity;
            if (visible && UseFadeEffect && FocusOnOpen) Opacity = 0;
            base.SetVisibleCore(visible);
            if (!visible || !UseFadeEffect || !FocusOnOpen) return;
            for (var i = 1; i <= Frames; i++)
            {
                if (i > 1)
                {
                    System.Threading.Thread.Sleep(Frameduration);
                }
                Opacity = opacity * i / Frames;
            }
            Opacity = opacity;
        }

        private bool _resizableTop;
        private bool _resizableRight;

        private void SetOwnerItem(Control control)
        {
            if (control == null)
                return;

			if (control is Popup)
            {
                var popupControl = control as Popup;
                _ownerPopup = popupControl;
                _ownerPopup._childPopup = this;
                OwnerItem = popupControl.Items[0];
                return;
            }

			if (control.Parent != null)
                SetOwnerItem(control.Parent);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            Content.MinimumSize = Size;
            Content.MaximumSize = Size;
            Content.Size = Size;
            Content.Location = Point.Empty;
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opening" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnOpening(CancelEventArgs e)
        {
            if (Content.IsDisposed || Content.Disposing)
            {
                e.Cancel = true;
                return;
            }
            UpdateRegion();
            base.OnOpening(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opened" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnOpened(EventArgs e)
        {
            if (_ownerPopup != null)
            {
                _ownerPopup._allowResizable = false;
            }
            if (FocusOnOpen)
            {
                Content.Focus();
            }
            base.OnOpened(e);
        }

        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            if (_ownerPopup != null)
            {
                _ownerPopup._allowResizable = true;
            }
            base.OnClosed(e);
        }

        public DateTime LastClosedTimeStamp = DateTime.Now;

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible == false)
                LastClosedTimeStamp = DateTime.Now;
            base.OnVisibleChanged(e);
        }

        #endregion

        #region Resizing Support

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (InternalProcessResizing(ref m, false))
            {
                return;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Processes the resizing messages.
        /// </summary>
        /// <param name="m">The message.</param>
        /// <returns>true, if the WndProc method from the base class shouldn't be invoked.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool ProcessResizing(ref Message m)
        {
            return InternalProcessResizing(ref m, true);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool InternalProcessResizing(ref Message m, bool contentControl)
        {
            if (m.Msg == NativeMethods.WM_NCACTIVATE && m.WParam != IntPtr.Zero && _childPopup != null && _childPopup.Visible)
            {
                _childPopup.Hide();
            }
            if (!Resizable)
            {
                return false;
            }
            switch (m.Msg)
            {
	            case NativeMethods.WM_NCHITTEST:
		            return OnNcHitTest(ref m, contentControl);
	            case NativeMethods.WM_GETMINMAXINFO:
		            return OnGetMinMaxInfo(ref m);
            }
            return false;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool OnGetMinMaxInfo(ref Message m)
        {
            var minmax = (NativeMethods.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.MINMAXINFO));
            minmax.maxTrackSize = MaximumSize;
            minmax.minTrackSize = MinimumSize;
            Marshal.StructureToPtr(minmax, m.LParam, false);
            return true;
        }

        private bool OnNcHitTest(ref Message m, bool contentControl)
        {
            var x = NativeMethods.LoWord(m.LParam);
            var y = NativeMethods.HiWord(m.LParam);
            var clientLocation = PointToClient(new Point(x, y));

            var gripBouns = new GripBounds(contentControl ? Content.ClientRectangle : ClientRectangle);
            var transparent = new IntPtr(NativeMethods.HTTRANSPARENT);

            if (_resizableTop)
            {
                if (_resizableRight && gripBouns.TopLeft.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTTOPLEFT;
                    return true;
                }
                if (!_resizableRight && gripBouns.TopRight.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTTOPRIGHT;
                    return true;
                }
                if (gripBouns.Top.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTTOP;
                    return true;
                }
            }
            else
            {
                if (_resizableRight && gripBouns.BottomLeft.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTBOTTOMLEFT;
                    return true;
                }
                if (!_resizableRight && gripBouns.BottomRight.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTBOTTOMRIGHT;
                    return true;
                }
                if (gripBouns.Bottom.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTBOTTOM;
                    return true;
                }
            }
            if (_resizableRight && gripBouns.Left.Contains(clientLocation))
            {
                m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTLEFT;
                return true;
            }
            if (!_resizableRight && gripBouns.Right.Contains(clientLocation))
            {
                m.Result = contentControl ? transparent : (IntPtr)NativeMethods.HTRIGHT;
                return true;
            }
            return false;
        }

        private VS.VisualStyleRenderer _sizeGripRenderer;
        /// <summary>
        /// Paints the size grip.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        public void PaintSizeGrip(PaintEventArgs e)
        {
            if (e == null || !_resizable)
            {
                return;
            }
            var clientSize = Content.ClientSize;
            if (Application.RenderWithVisualStyles)
            {
                if (_sizeGripRenderer == null)
                {
                    _sizeGripRenderer = new VS.VisualStyleRenderer(VS.VisualStyleElement.Status.Gripper.Normal);
                }
                _sizeGripRenderer.DrawBackground(e.Graphics, new Rectangle(clientSize.Width - 0x10, clientSize.Height - 0x10, 0x10, 0x10));
            }
            else
            {
                ControlPaint.DrawSizeGrip(e.Graphics, Content.BackColor, clientSize.Width - 0x10, clientSize.Height - 0x10, 0x10, 0x10);
            }
        }

        #endregion
    }
}
