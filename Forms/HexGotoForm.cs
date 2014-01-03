/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System;
using System.Windows.Forms;

namespace Reflexil.Editors
{
	/// <summary>
	/// Summary description for FormGoTo.
	/// </summary>
	public class HexGotoForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Panel panel1;
        private Reflexil.Editors.LongEditor LongEditor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HexGotoForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LongEditor = new Reflexil.Editors.LongEditor();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Byte offset:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(153, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(72, 42);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LongEditor);
            this.panel1.Location = new System.Drawing.Point(72, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 22);
            this.panel1.TabIndex = 3;
            // 
            // LongEditor
            // 
            this.LongEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LongEditor.Location = new System.Drawing.Point(0, 0);
            this.LongEditor.Name = "LongEditor";
            this.LongEditor.SelectedOperand = ((long)(0));
            this.LongEditor.Size = new System.Drawing.Size(155, 22);
            this.LongEditor.TabIndex = 0;
            this.LongEditor.UseBaseSelector = true;
            this.LongEditor.Value = "";
            // 
            // HexGotoForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(240, 72);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HexGotoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goto byte";
            this.Activated += new System.EventHandler(this.FormGoTo_Activated);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void SetDefaultValue(long byteIndex)
		{
		    LongEditor.SelectedOperand = byteIndex;
		}

		public long GetByteIndex()
		{
			return LongEditor.SelectedOperand;
		}

		private void FormGoTo_Activated(object sender, System.EventArgs e)
		{
			LongEditor.Focus();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
    }
}
