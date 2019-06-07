/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace Reflexil.Editors
{
	public class HexFindForm : Form
	{
		private HexBox _hexBox;
		private TextBox _txtString;
		private RadioButton _rbString;
		private RadioButton _rbHex;
		private Button _btnOk;
		private Button _btnCancel;

		private readonly System.ComponentModel.Container _components = null;

		public HexFindForm()
		{
			InitializeComponent();

			_rbString.CheckedChanged += rb_CheckedChanged;
			_rbHex.CheckedChanged += rb_CheckedChanged;
			_hexBox.ByteProvider = new DynamicByteProvider(new ByteCollection());
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_components != null)
				{
					_components.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._hexBox = new Be.Windows.Forms.HexBox();
			this._txtString = new System.Windows.Forms.TextBox();
			this._rbString = new System.Windows.Forms.RadioButton();
			this._rbHex = new System.Windows.Forms.RadioButton();
			this._btnOk = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// hexBox
			// 
			this._hexBox.Anchor =
				((System.Windows.Forms.AnchorStyles)
					((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					   | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this._hexBox.BuiltInContextMenu.CopyMenuItemImage = global::Reflexil.Properties.Resources.CopyHS;
			this._hexBox.BuiltInContextMenu.CutMenuItemImage = global::Reflexil.Properties.Resources.CutHS;
			this._hexBox.BuiltInContextMenu.PasteMenuItemImage = global::Reflexil.Properties.Resources.PasteHS;
			this._hexBox.Enabled = false;
			this._hexBox.Font = new System.Drawing.Font("Courier New", 9F);
			this._hexBox.HexCasing = Be.Windows.Forms.HexCasing.Lower;
			this._hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
			this._hexBox.Location = new System.Drawing.Point(12, 76);
			this._hexBox.Name = "_hexBox";
			this._hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))),
				((int)(((byte)(188)))), ((int)(((byte)(255)))));
			this._hexBox.Size = new System.Drawing.Size(304, 126);
			this._hexBox.TabIndex = 3;
			// 
			// txtString
			// 
			this._txtString.Anchor =
				((System.Windows.Forms.AnchorStyles)
					(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
			this._txtString.Location = new System.Drawing.Point(12, 28);
			this._txtString.Name = "_txtString";
			this._txtString.Size = new System.Drawing.Size(304, 21);
			this._txtString.TabIndex = 1;
			// 
			// rbString
			// 
			this._rbString.Checked = true;
			this._rbString.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._rbString.Location = new System.Drawing.Point(12, 12);
			this._rbString.Name = "_rbString";
			this._rbString.Size = new System.Drawing.Size(104, 16);
			this._rbString.TabIndex = 0;
			this._rbString.TabStop = true;
			this._rbString.Text = "Text";
			// 
			// rbHex
			// 
			this._rbHex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._rbHex.Location = new System.Drawing.Point(12, 60);
			this._rbHex.Name = "_rbHex";
			this._rbHex.Size = new System.Drawing.Size(104, 16);
			this._rbHex.TabIndex = 2;
			this._rbHex.Text = "Hex";
			// 
			// btnOK
			// 
			this._btnOk.Anchor =
				((System.Windows.Forms.AnchorStyles)
					((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnOk.Location = new System.Drawing.Point(160, 211);
			this._btnOk.Name = "_btnOk";
			this._btnOk.Size = new System.Drawing.Size(75, 23);
			this._btnOk.TabIndex = 4;
			this._btnOk.Text = "Find next";
			this._btnOk.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this._btnCancel.Anchor =
				((System.Windows.Forms.AnchorStyles)
					((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(241, 211);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(75, 23);
			this._btnCancel.TabIndex = 5;
			this._btnCancel.Text = "Cancel";
			this._btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// HexFindForm
			// 
			this.AcceptButton = this._btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this._btnCancel;
			this.ClientSize = new System.Drawing.Size(336, 246);
			this.Controls.Add(this._btnCancel);
			this.Controls.Add(this._btnOk);
			this.Controls.Add(this._rbHex);
			this.Controls.Add(this._rbString);
			this.Controls.Add(this._txtString);
			this.Controls.Add(this._hexBox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HexFindForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find";
			this.Activated += new System.EventHandler(this.FormFind_Activated);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		public byte[] GetFindBytes()
		{
			return !_rbString.Checked ? ((DynamicByteProvider)_hexBox.ByteProvider).Bytes.GetBytes() : System.Text.Encoding.ASCII.GetBytes(_txtString.Text);
		}

		private void rb_CheckedChanged(object sender, EventArgs e)
		{
			_txtString.Enabled = _rbString.Checked;
			_hexBox.Enabled = !_txtString.Enabled;

			if (_txtString.Enabled)
				_txtString.Focus();
			else
				_hexBox.Focus();
		}

		private void FormFind_Activated(object sender, EventArgs e)
		{
			if (_rbString.Checked)
				_txtString.Focus();
			else
				_hexBox.Focus();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (_rbString.Checked && _txtString.Text.Length == 0)
				DialogResult = DialogResult.Cancel;
			else if (_rbHex.Checked && _hexBox.ByteProvider.Length == 0)
				DialogResult = DialogResult.Cancel;
			else
				DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
