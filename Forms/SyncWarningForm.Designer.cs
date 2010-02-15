namespace Reflexil.Forms
{
	partial class SyncWarningForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncWarningForm));
            this.LabWarning = new System.Windows.Forms.Label();
            this.CbxWarning = new System.Windows.Forms.CheckBox();
            this.BtOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabWarning
            // 
            this.LabWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabWarning.Location = new System.Drawing.Point(0, 0);
            this.LabWarning.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.LabWarning.Name = "LabWarning";
            this.LabWarning.Padding = new System.Windows.Forms.Padding(8);
            this.LabWarning.Size = new System.Drawing.Size(557, 78);
            this.LabWarning.TabIndex = 0;
            this.LabWarning.Text = resources.GetString("LabWarning.Text");
            // 
            // CbxWarning
            // 
            this.CbxWarning.AutoSize = true;
            this.CbxWarning.Checked = global::Reflexil.Properties.Settings.Default.DisplayWarning;
            this.CbxWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbxWarning.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Reflexil.Properties.Settings.Default, "DisplayWarning", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbxWarning.Location = new System.Drawing.Point(97, 81);
            this.CbxWarning.Name = "CbxWarning";
            this.CbxWarning.Size = new System.Drawing.Size(363, 17);
            this.CbxWarning.TabIndex = 1;
            this.CbxWarning.Text = "Always warn me (you can change this later in the Reflexil configuration).";
            this.CbxWarning.UseVisualStyleBackColor = true;
            // 
            // BtOk
            // 
            this.BtOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtOk.Location = new System.Drawing.Point(241, 113);
            this.BtOk.Name = "BtOk";
            this.BtOk.Size = new System.Drawing.Size(75, 23);
            this.BtOk.TabIndex = 2;
            this.BtOk.Text = "OK";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // SyncWarningForm
            // 
            this.AcceptButton = this.BtOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 150);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this.CbxWarning);
            this.Controls.Add(this.LabWarning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncWarningForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reflexil warning";
            this.Load += new System.EventHandler(this.SyncWarningForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label LabWarning;
        private System.Windows.Forms.CheckBox CbxWarning;
        private System.Windows.Forms.Button BtOk;
	}
}