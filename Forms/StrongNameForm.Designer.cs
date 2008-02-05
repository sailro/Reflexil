namespace Reflexil.Forms
{
	partial class StrongNameForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
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
            this.Title = new System.Windows.Forms.Label();
            this.Register = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Resign = new System.Windows.Forms.Button();
            this.Note = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SnToolNotFound = new System.Windows.Forms.Label();
            this.RemoveSN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Location = new System.Drawing.Point(12, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(315, 28);
            this.Title.TabIndex = 0;
            this.Title.Text = "Original assembly is signed. This patched version is now \"delay signed\", and will" +
                " not load correctly until you fix it:";
            // 
            // Register
            // 
            this.Register.Location = new System.Drawing.Point(15, 50);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(302, 23);
            this.Register.TabIndex = 0;
            this.Register.Text = "Register it for verification skipping (on this computer)";
            this.Register.UseVisualStyleBackColor = true;
            this.Register.Click += new System.EventHandler(this.Register_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(15, 137);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(302, 23);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Cancel and keep it delay signed";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Resign
            // 
            this.Resign.Location = new System.Drawing.Point(15, 79);
            this.Resign.Name = "Resign";
            this.Resign.Size = new System.Drawing.Size(302, 23);
            this.Resign.TabIndex = 1;
            this.Resign.Text = "Re-sign with key ...";
            this.Resign.UseVisualStyleBackColor = true;
            this.Resign.Click += new System.EventHandler(this.Resign_Click);
            // 
            // Note
            // 
            this.Note.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Note.Location = new System.Drawing.Point(12, 172);
            this.Note.Name = "Note";
            this.Note.Size = new System.Drawing.Size(315, 37);
            this.Note.TabIndex = 4;
            this.Note.Text = "You can also fix this later with the Strong Name Utility (sn) of the .NET SDK.";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "Key files (*.snk)|*.snk|Password protected key files (*.pfx)|*.pfx";
            this.OpenFileDialog.Title = "Select key file";
            // 
            // SnToolNotFound
            // 
            this.SnToolNotFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SnToolNotFound.ForeColor = System.Drawing.Color.Red;
            this.SnToolNotFound.Location = new System.Drawing.Point(12, 172);
            this.SnToolNotFound.Name = "SnToolNotFound";
            this.SnToolNotFound.Size = new System.Drawing.Size(315, 37);
            this.SnToolNotFound.TabIndex = 5;
            this.SnToolNotFound.Text = "Warning, Strong Name Utility (sn.exe) not found. Update your PATH environment var" +
                "iable or install .NET SDK";
            this.SnToolNotFound.Visible = false;
            // 
            // RemoveSN
            // 
            this.RemoveSN.Location = new System.Drawing.Point(15, 108);
            this.RemoveSN.Name = "RemoveSN";
            this.RemoveSN.Size = new System.Drawing.Size(302, 23);
            this.RemoveSN.TabIndex = 2;
            this.RemoveSN.Text = "Remove Strong Name ...";
            this.RemoveSN.UseVisualStyleBackColor = true;
            this.RemoveSN.Click += new System.EventHandler(this.RemoveSN_Click);
            // 
            // StrongNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(331, 210);
            this.Controls.Add(this.RemoveSN);
            this.Controls.Add(this.SnToolNotFound);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.Resign);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Register);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StrongNameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Signed Assembly";
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Resign;
        private System.Windows.Forms.Label Note;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Label SnToolNotFound;
        private System.Windows.Forms.Button RemoveSN;
	}
}