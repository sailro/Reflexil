namespace Reflexil.Handlers
{
    partial class AssemblyDefinitionHandler
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
            this.TabDefinition = new System.Windows.Forms.TabPage();
            this.Definition = new Reflexil.Editors.AssemblyDefinitionControl();
            this.TabNameDefinition = new System.Windows.Forms.TabPage();
            this.NameDefinition = new Reflexil.Editors.AssemblyNameDefinitionAttributesControl();
            this.TabControl.SuspendLayout();
            this.TabDefinition.SuspendLayout();
            this.TabNameDefinition.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabDefinition);
            this.TabControl.Controls.Add(this.TabNameDefinition);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(526, 400);
            this.TabControl.TabIndex = 0;
            // 
            // TabDefinition
            // 
            this.TabDefinition.Controls.Add(this.Definition);
            this.TabDefinition.Location = new System.Drawing.Point(4, 22);
            this.TabDefinition.Name = "TabDefinition";
            this.TabDefinition.Padding = new System.Windows.Forms.Padding(3);
            this.TabDefinition.Size = new System.Drawing.Size(518, 374);
            this.TabDefinition.TabIndex = 1;
            this.TabDefinition.Text = "Definition";
            this.TabDefinition.UseVisualStyleBackColor = true;
            // 
            // Definition
            // 
            this.Definition.Item = null;
            this.Definition.Location = new System.Drawing.Point(7, 7);
            this.Definition.Name = "Definition";
            this.Definition.ReadOnly = false;
            this.Definition.Size = new System.Drawing.Size(414, 115);
            this.Definition.TabIndex = 0;
            // 
            // TabNameDefinition
            // 
            this.TabNameDefinition.Controls.Add(this.NameDefinition);
            this.TabNameDefinition.Location = new System.Drawing.Point(4, 22);
            this.TabNameDefinition.Name = "TabNameDefinition";
            this.TabNameDefinition.Padding = new System.Windows.Forms.Padding(3);
            this.TabNameDefinition.Size = new System.Drawing.Size(518, 374);
            this.TabNameDefinition.TabIndex = 0;
            this.TabNameDefinition.Text = "Name definition";
            this.TabNameDefinition.UseVisualStyleBackColor = true;
            // 
            // NameDefinition
            // 
            this.NameDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameDefinition.Item = null;
            this.NameDefinition.Location = new System.Drawing.Point(3, 3);
            this.NameDefinition.Name = "NameDefinition";
            this.NameDefinition.ReadOnly = false;
            this.NameDefinition.Size = new System.Drawing.Size(512, 368);
            this.NameDefinition.TabIndex = 0;
            // 
            // AssemblyDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "AssemblyDefinitionHandler";
            this.Size = new System.Drawing.Size(526, 400);
            this.TabControl.ResumeLayout(false);
            this.TabDefinition.ResumeLayout(false);
            this.TabNameDefinition.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabNameDefinition;
        private Reflexil.Editors.AssemblyNameDefinitionAttributesControl NameDefinition;
        private System.Windows.Forms.TabPage TabDefinition;
        private Reflexil.Editors.AssemblyDefinitionControl Definition;

    }
}
