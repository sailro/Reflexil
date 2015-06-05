namespace Reflexil.Forms
{
	partial class GenericInstanceForm<T>
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
			this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.ContentPanel = new System.Windows.Forms.Panel();
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.Cancel = new System.Windows.Forms.Button();
			this.Ok = new System.Windows.Forms.Button();
			this.ContentPanel.SuspendLayout();
			this.ButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Dock = System.Windows.Forms.DockStyle.Top;
			this.Title.Location = new System.Drawing.Point(0, 0);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(424, 56);
			this.Title.TabIndex = 0;
			this.Title.Text = "{0} is a generic parameter provider with {1} parameter(s). Please provide type ar" +
    "gument(s):";
			this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// OpenFileDialog
			// 
			this.OpenFileDialog.Filter = "Key files (*.snk)|*.snk|Password protected key files (*.pfx)|*.pfx";
			this.OpenFileDialog.Title = "Select key file";
			// 
			// FlowPanel
			// 
			this.FlowPanel.AutoScroll = true;
			this.FlowPanel.AutoSize = true;
			this.FlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FlowPanel.Location = new System.Drawing.Point(0, 0);
			this.FlowPanel.Name = "FlowPanel";
			this.FlowPanel.Size = new System.Drawing.Size(424, 417);
			this.FlowPanel.TabIndex = 1;
			// 
			// ContentPanel
			// 
			this.ContentPanel.Controls.Add(this.ButtonPanel);
			this.ContentPanel.Controls.Add(this.FlowPanel);
			this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ContentPanel.Location = new System.Drawing.Point(0, 56);
			this.ContentPanel.Name = "ContentPanel";
			this.ContentPanel.Size = new System.Drawing.Size(424, 417);
			this.ContentPanel.TabIndex = 2;
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Controls.Add(this.Cancel);
			this.ButtonPanel.Controls.Add(this.Ok);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ButtonPanel.Location = new System.Drawing.Point(0, 381);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(424, 36);
			this.ButtonPanel.TabIndex = 9;
			// 
			// Cancel
			// 
			this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(337, 6);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 10;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// Ok
			// 
			this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Ok.Location = new System.Drawing.Point(256, 6);
			this.Ok.Name = "Ok";
			this.Ok.Size = new System.Drawing.Size(75, 23);
			this.Ok.TabIndex = 9;
			this.Ok.Text = "Ok";
			this.Ok.UseVisualStyleBackColor = true;
			// 
			// GenericInstanceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 473);
			this.Controls.Add(this.ContentPanel);
			this.Controls.Add(this.Title);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(440, 800);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(440, 512);
			this.Name = "GenericInstanceForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Generic parameter provider";
			this.ContentPanel.ResumeLayout(false);
			this.ContentPanel.PerformLayout();
			this.ButtonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.OpenFileDialog OpenFileDialog;
		private System.Windows.Forms.FlowLayoutPanel FlowPanel;
		private System.Windows.Forms.Panel ContentPanel;
		private System.Windows.Forms.Panel ButtonPanel;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.Button Ok;
	}
}