/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System;
using System.Windows.Forms;

using Be.Windows.Forms;

namespace Reflexil.Editors
{
	/// <summary>
	/// Summary description for FormFind.
	/// </summary>
	public class HexFindForm : System.Windows.Forms.Form
	{
		private Be.Windows.Forms.HexBox hexBox;
		private System.Windows.Forms.TextBox txtString;
		private System.Windows.Forms.RadioButton rbString;
        private System.Windows.Forms.RadioButton rbHex;
		private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HexFindForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			rbString.CheckedChanged += new EventHandler(rb_CheckedChanged);
			rbHex.CheckedChanged += new EventHandler(rb_CheckedChanged);

//			rbString.Enter += new EventHandler(rbString_Enter);
//			rbHex.Enter += new EventHandler(rbHex_Enter);

			hexBox.ByteProvider = new DynamicByteProvider(new ByteCollection());
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.txtString = new System.Windows.Forms.TextBox();
            this.rbString = new System.Windows.Forms.RadioButton();
            this.rbHex = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hexBox
            // 
            this.hexBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.hexBox.BuiltInContextMenu.CopyMenuItemImage = global::Reflexil.Properties.Resources.CopyHS;
            this.hexBox.BuiltInContextMenu.CutMenuItemImage = global::Reflexil.Properties.Resources.CutHS;
            this.hexBox.BuiltInContextMenu.PasteMenuItemImage = global::Reflexil.Properties.Resources.PasteHS;
            this.hexBox.Enabled = false;
            this.hexBox.Font = new System.Drawing.Font("Courier New", 9F);
            this.hexBox.HexCasing = Be.Windows.Forms.HexCasing.Lower;
            this.hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox.Location = new System.Drawing.Point(12, 76);
            this.hexBox.Name = "hexBox";
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(304, 126);
            this.hexBox.TabIndex = 3;
            // 
            // txtString
            // 
            this.txtString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtString.Location = new System.Drawing.Point(12, 28);
            this.txtString.Name = "txtString";
            this.txtString.Size = new System.Drawing.Size(304, 21);
            this.txtString.TabIndex = 1;
            // 
            // rbString
            // 
            this.rbString.Checked = true;
            this.rbString.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbString.Location = new System.Drawing.Point(12, 12);
            this.rbString.Name = "rbString";
            this.rbString.Size = new System.Drawing.Size(104, 16);
            this.rbString.TabIndex = 0;
            this.rbString.TabStop = true;
            this.rbString.Text = "Text";
            // 
            // rbHex
            // 
            this.rbHex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbHex.Location = new System.Drawing.Point(12, 60);
            this.rbHex.Name = "rbHex";
            this.rbHex.Size = new System.Drawing.Size(104, 16);
            this.rbHex.TabIndex = 2;
            this.rbHex.Text = "Hex";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(160, 211);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Find next";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(241, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // HexFindForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(336, 246);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rbHex);
            this.Controls.Add(this.rbString);
            this.Controls.Add(this.txtString);
            this.Controls.Add(this.hexBox);
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
			if(rbString.Checked)
			{
				byte[] res = System.Text.ASCIIEncoding.ASCII.GetBytes(txtString.Text);
				return res;
			}
			else
			{
				return ((DynamicByteProvider)hexBox.ByteProvider).Bytes.GetBytes();
			}
		}

		private void rb_CheckedChanged(object sender, System.EventArgs e)
		{
			txtString.Enabled = rbString.Checked;
			hexBox.Enabled = !txtString.Enabled;

			if(txtString.Enabled)
				txtString.Focus();
			else
				hexBox.Focus();
		}

		private void rbString_Enter(object sender, EventArgs e)
		{
			txtString.Focus();
		}

		private void rbHex_Enter(object sender, EventArgs e)
		{
			hexBox.Focus();
		}

		private void FormFind_Activated(object sender, System.EventArgs e)
		{
			if(rbString.Checked)
				txtString.Focus();
			else
				hexBox.Focus();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(rbString.Checked && txtString.Text.Length == 0)
				DialogResult = DialogResult.Cancel;
			else if(rbHex.Checked && hexBox.ByteProvider.Length == 0)
				DialogResult = DialogResult.Cancel;
			else
				DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
