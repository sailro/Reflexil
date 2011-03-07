namespace Reflexil.Editors
{
	partial class SplitAttributesControl<T>
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
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.Attributes = new Reflexil.Editors.AttributesControl();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            this.SplitContainer.Size = new System.Drawing.Size(715, 452);
            this.SplitContainer.SplitterDistance = 220;
            this.SplitContainer.Panel1MinSize = 220;
            this.SplitContainer.TabIndex = 0;
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.Attributes);
            // 
            // Attributes
            // 
            this.Attributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Attributes.Item = null;
            this.Attributes.Location = new System.Drawing.Point(0, 0);
            this.Attributes.Name = "Attributes";
            this.Attributes.Size = new System.Drawing.Size(264, 452);
            this.Attributes.TabIndex = 0;
            // 
            // SplitAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer);
            this.Name = "SplitAttributesControl";
            this.Size = new System.Drawing.Size(715, 452);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        protected System.Windows.Forms.SplitContainer SplitContainer;
        private AttributesControl Attributes;
	}
}
