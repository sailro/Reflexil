// Martin Lottering, Lukasz Swiatkowski.
// From CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using VS = System.Windows.Forms.VisualStyles;

namespace Reflexil.Editors
{
	[ToolboxItem(false)]
	public sealed partial class Popup : ToolStripDropDown
	{
		public Control Content { get; private set; }
		public bool UseFadeEffect { get; set; }
		public bool FocusOnOpen { get; set; }
		public bool AcceptAlt { get; set; }

		private Popup _ownerPopup;
		private Popup _childPopup;

		private bool _allowResizable;
		private bool _resizable;

		public bool Resizable
		{
			get { return _resizable && _allowResizable; }
			set { _resizable = value; }
		}

		public new Size MinimumSize { get; set; }
		public new Size MaximumSize { get; set; }

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

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (AcceptAlt && ((keyData & Keys.Alt) == Keys.Alt)) return false;
			return base.ProcessDialogKey(keyData);
		}

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

		public void Show(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			SetOwnerItem(control);
			Show(control, control.ClientRectangle);
		}

		public void Show(Control control, Rectangle area)
		{
			if (control == null)
				throw new ArgumentNullException("control");

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

		protected override void SetVisibleCore(bool visible)
		{
			var opacity = Opacity;
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

			var popup = control as Popup;
			if (popup != null)
			{
				_ownerPopup = popup;
				_ownerPopup._childPopup = this;
				OwnerItem = popup.Items[0];
				return;
			}

			if (control.Parent != null)
				SetOwnerItem(control.Parent);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			Content.MinimumSize = Size;
			Content.MaximumSize = Size;
			Content.Size = Size;
			Content.Location = Point.Empty;
			base.OnSizeChanged(e);
		}

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

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (InternalProcessResizing(ref m, false))
				return;

			base.WndProc(ref m);
		}

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

				_sizeGripRenderer.DrawBackground(e.Graphics,
					new Rectangle(clientSize.Width - 0x10, clientSize.Height - 0x10, 0x10, 0x10));
			}
			else
			{
				ControlPaint.DrawSizeGrip(e.Graphics, Content.BackColor, clientSize.Width - 0x10, clientSize.Height - 0x10, 0x10, 0x10);
			}
		}
	}
}
