﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Reflexil.Editors
{
	/// <summary>
	/// CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".
	/// Represents a Windows combo box control with a custom popup control attached.
	/// </summary>
	[ToolboxBitmap(typeof (ComboBox)), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"),
	 Description("Displays an editable text box with a drop-down list of permitted values.")]
	public partial class PopupComboBox : ComboBox
	{
		/// <summary>
		/// Initializes a new instance of the PopupComboBox class.
		/// </summary>
		public PopupComboBox()
		{
			InitializeComponent();
			base.DropDownHeight = base.DropDownWidth = 1;
			base.IntegralHeight = false;
		}

		/// <summary>
		/// The pop-up wrapper for the dropDownControl. 
		/// Made PROTECTED instead of PRIVATE so descendent classes can set its Resizable property.
		/// Note however the pop-up properties must be set after the dropDownControl is assigned, since this 
		/// popup wrapper is recreated when the dropDownControl is assigned.
		/// </summary>
		protected Popup PopupDropDown;

		private Control _dropDownControl;

		/// <summary>
		/// Gets or sets the drop down control.
		/// </summary>
		/// <value>The drop down control.</value>
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

		/// <summary>
		/// Shows the drop down.
		/// </summary>
		public void ShowDropDown()
		{
			if (PopupDropDown != null)
			{
				PopupDropDown.Show(this);
			}
		}

		/// <summary>
		/// Hides the drop down.
		/// </summary>
		public void HideDropDown()
		{
			if (PopupDropDown != null)
			{
				PopupDropDown.Hide();
			}
		}

		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
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

		#region Unused Properties

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>This property is not relevant for this class.</returns>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		 EditorBrowsable(EditorBrowsableState.Never)]
		public new int DropDownWidth
		{
			get { return base.DropDownWidth; }
			set { base.DropDownWidth = value; }
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>This property is not relevant for this class.</returns>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		 EditorBrowsable(EditorBrowsableState.Never)]
		public new int DropDownHeight
		{
			get { return base.DropDownHeight; }
			set
			{
				PopupDropDown.Height = value;
				base.DropDownHeight = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>This property is not relevant for this class.</returns>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		 EditorBrowsable(EditorBrowsableState.Never)]
		public new bool IntegralHeight
		{
			get { return base.IntegralHeight; }
			set { base.IntegralHeight = value; }
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>This property is not relevant for this class.</returns>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		 EditorBrowsable(EditorBrowsableState.Never)]
		public new ObjectCollection Items
		{
			get { return base.Items; }
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>This property is not relevant for this class.</returns>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		 EditorBrowsable(EditorBrowsableState.Never)]
		public new int ItemHeight
		{
			get { return base.ItemHeight; }
			set { base.ItemHeight = value; }
		}

		#endregion
	}
}