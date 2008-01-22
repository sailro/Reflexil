namespace Reflexil.Handlers
{
    partial class AssemblyNameReferenceHandler
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabReference = new System.Windows.Forms.TabPage();
            this.NameReference = new Reflexil.Editors.AssemblyNameReferenceAttributesControl();
            this.TabControl.SuspendLayout();
            this.TabReference.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabReference);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(526, 400);
            this.TabControl.TabIndex = 0;
            // 
            // TabReference
            // 
            this.TabReference.Controls.Add(this.NameReference);
            this.TabReference.Location = new System.Drawing.Point(4, 22);
            this.TabReference.Name = "TabReference";
            this.TabReference.Padding = new System.Windows.Forms.Padding(3);
            this.TabReference.Size = new System.Drawing.Size(518, 374);
            this.TabReference.TabIndex = 0;
            this.TabReference.Text = "Name reference";
            this.TabReference.UseVisualStyleBackColor = true;
            // 
            // NameReference
            // 
            this.NameReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameReference.Item = null;
            this.NameReference.Location = new System.Drawing.Point(3, 3);
            this.NameReference.Name = "NameReference";
            this.NameReference.ReadOnly = false;
            this.NameReference.Size = new System.Drawing.Size(512, 368);
            this.NameReference.TabIndex = 0;
            // 
            // AssemblyNameReferenceHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "AssemblyNameReferenceHandler";
            this.Size = new System.Drawing.Size(526, 400);
            this.TabControl.ResumeLayout(false);
            this.TabReference.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabReference;
        private Reflexil.Editors.AssemblyNameReferenceAttributesControl NameReference;

    }
}
