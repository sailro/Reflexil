namespace Reflexil.Editors
{
	partial class MethodAttributesControl
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
            this.RVA = new System.Windows.Forms.TextBox();
            this.LabRVA = new System.Windows.Forms.Label();
            this.LabCallingConvention = new System.Windows.Forms.Label();
            this.CallingConvention = new System.Windows.Forms.ComboBox();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.Attributes);
            this.SplitContainer.Panel1MinSize = 216;
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.RVA);
            this.SplitContainer.Panel2.Controls.Add(this.LabRVA);
            this.SplitContainer.Panel2.Controls.Add(this.LabCallingConvention);
            this.SplitContainer.Panel2.Controls.Add(this.CallingConvention);
            this.SplitContainer.Size = new System.Drawing.Size(647, 416);
            this.SplitContainer.SplitterDistance = 216;
            this.SplitContainer.TabIndex = 3;
            // 
            // Attributes
            // 
            this.Attributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Attributes.Location = new System.Drawing.Point(0, 0);
            this.Attributes.Name = "Attributes";
            this.Attributes.Size = new System.Drawing.Size(216, 416);
            this.Attributes.TabIndex = 0;
            // 
            // RVA
            // 
            this.RVA.Location = new System.Drawing.Point(107, 32);
            this.RVA.Name = "RVA";
            this.RVA.ReadOnly = true;
            this.RVA.Size = new System.Drawing.Size(100, 20);
            this.RVA.TabIndex = 4;
            // 
            // LabRVA
            // 
            this.LabRVA.AutoSize = true;
            this.LabRVA.Location = new System.Drawing.Point(4, 35);
            this.LabRVA.Name = "LabRVA";
            this.LabRVA.Size = new System.Drawing.Size(29, 13);
            this.LabRVA.TabIndex = 3;
            this.LabRVA.Text = "RVA";
            // 
            // LabCallingConvention
            // 
            this.LabCallingConvention.AutoSize = true;
            this.LabCallingConvention.Location = new System.Drawing.Point(4, 7);
            this.LabCallingConvention.Name = "LabCallingConvention";
            this.LabCallingConvention.Size = new System.Drawing.Size(97, 13);
            this.LabCallingConvention.TabIndex = 2;
            this.LabCallingConvention.Text = "Calling convention:";
            // 
            // CallingConvention
            // 
            this.CallingConvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CallingConvention.FormattingEnabled = true;
            this.CallingConvention.Location = new System.Drawing.Point(107, 4);
            this.CallingConvention.Name = "CallingConvention";
            this.CallingConvention.Size = new System.Drawing.Size(100, 21);
            this.CallingConvention.TabIndex = 1;
            this.CallingConvention.SelectionChangeCommitted += new System.EventHandler(this.CallingConvention_SelectionChangeCommitted);
            // 
            // MethodAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer);
            this.Name = "MethodAttributesControl";
            this.Size = new System.Drawing.Size(647, 416);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.TextBox RVA;
        private System.Windows.Forms.Label LabRVA;
        private System.Windows.Forms.Label LabCallingConvention;
        private System.Windows.Forms.ComboBox CallingConvention;
        private Reflexil.Editors.AttributesControl Attributes;
	}
}
