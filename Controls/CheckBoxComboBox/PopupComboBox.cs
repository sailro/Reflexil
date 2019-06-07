// Martin Lottering, Lukasz Swiatkowski.
// From CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Reflexil.Editors
{
	[ToolboxBitmap(typeof(ComboBox)), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"),
	 Description("Displays an editable text box with a drop-down list of permitted values.")]
	public partial class PopupComboBox : ComboBox
	{
		public PopupComboBox()
		{
			InitializeComponent();
			base.DropDownHeight = base.DropDownWidth = 1;
			base.IntegralHeight = false;
		}

		protected Popup PopupDropDown;
		private Control _dropDownControl;

		public Control DropDownControl
		{
			get { return _dropDownControl; }
			set
			{
				if (_dropDownControl == value)
					return;
				_dropDownControl = value;
				PopupDropDown = new Popup(value);
			}
		}

		public void ShowDropDown()
		{
			if (PopupDropDown != null)
			{
				PopupDropDown.Show(this);
			}
		}

		public void HideDropDown()
		{
			if (PopupDropDown != null)
			{
				PopupDropDown.Hide();
			}
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == (NativeMethods.WM_REFLECT + NativeMethods.WM_COMMAND))
			{
				if (NativeMethods.HiWord(m.WParam) == NativeMethods.CBN_DROPDOWN)
				{
					// Blocks a redisplay when the user closes the control by clicking 
					// on the combobox.
					var timeSpan = DateTime.Now.Subtract(PopupDropDown.LastClosedTimeStamp);
					if (timeSpan.TotalMilliseconds > 500)
						ShowDropDown();
					return;
				}
			}

			base.WndProc(ref m);
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new int DropDownWidth
		{
			get { return base.DropDownWidth; }
			set { base.DropDownWidth = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new int DropDownHeight
		{
			get { return base.DropDownHeight; }
			set
			{
				PopupDropDown.Height = value;
				base.DropDownHeight = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool IntegralHeight
		{
			get { return base.IntegralHeight; }
			set { base.IntegralHeight = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new ObjectCollection Items
		{
			get { return base.Items; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new int ItemHeight
		{
			get { return base.ItemHeight; }
			set { base.ItemHeight = value; }
		}
	}
}
