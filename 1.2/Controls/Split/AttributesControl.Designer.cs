namespace Reflexil.Editors
{
	partial class AttributesControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Flags = new System.Windows.Forms.CheckedListBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Flags
            // 
            this.Flags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flags.FormattingEnabled = true;
            this.Flags.Location = new System.Drawing.Point(0, 0);
            this.Flags.Name = "Flags";
            this.Flags.Size = new System.Drawing.Size(351, 349);
            this.Flags.Sorted = true;
            this.Flags.TabIndex = 1;
            this.Flags.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Flags_ItemCheck);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.Flags);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(351, 360);
            this.MainPanel.TabIndex = 2;
            // 
            // AttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "AttributesControl";
            this.Size = new System.Drawing.Size(351, 360);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.CheckedListBox Flags;
        private System.Windows.Forms.Panel MainPanel;
	}
}
