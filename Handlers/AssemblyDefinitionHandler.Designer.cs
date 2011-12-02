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
            this.TabAttributes = new System.Windows.Forms.TabPage();
            this.Definition = new Reflexil.Editors.AssemblyDefinitionControl();
            this.TabNameDefinition = new System.Windows.Forms.TabPage();
            this.NameDefinition = new Reflexil.Editors.AssemblyNameDefinitionAttributesControl();
            this.TabCustomAttributes = new System.Windows.Forms.TabPage();
            this.CustomAttributes = new Reflexil.Editors.CustomAttributeGridControl();
            this.TabControl.SuspendLayout();
            this.TabAttributes.SuspendLayout();
            this.TabNameDefinition.SuspendLayout();
            this.TabCustomAttributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabAttributes);
            this.TabControl.Controls.Add(this.TabNameDefinition);
            this.TabControl.Controls.Add(this.TabCustomAttributes);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(526, 400);
            this.TabControl.TabIndex = 0;
            // 
            // TabAttributes
            // 
            this.TabAttributes.Controls.Add(this.Definition);
            this.TabAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabAttributes.Name = "TabAttributes";
            this.TabAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabAttributes.Size = new System.Drawing.Size(518, 374);
            this.TabAttributes.TabIndex = 1;
            this.TabAttributes.Text = "Attributes";
            this.TabAttributes.UseVisualStyleBackColor = true;
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
            // TabCustomAttributes
            // 
            this.TabCustomAttributes.Controls.Add(this.CustomAttributes);
            this.TabCustomAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabCustomAttributes.Name = "TabCustomAttributes";
            this.TabCustomAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabCustomAttributes.Size = new System.Drawing.Size(518, 374);
            this.TabCustomAttributes.TabIndex = 2;
            this.TabCustomAttributes.Text = "Custom attributes";
            this.TabCustomAttributes.UseVisualStyleBackColor = true;
            // 
            // CustomAttributes
            // 
            this.CustomAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomAttributes.Location = new System.Drawing.Point(3, 3);
            this.CustomAttributes.Name = "CustomAttributes";
            this.CustomAttributes.ReadOnly = false;
            this.CustomAttributes.Size = new System.Drawing.Size(512, 368);
            this.CustomAttributes.TabIndex = 0;
            this.CustomAttributes.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.CustomAttribute, Mono.Cecil.ICustomAttributeProvider>.GridUpdatedEventHandler(this.CustomAttributes_GridUpdated);
            // 
            // AssemblyDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "AssemblyDefinitionHandler";
            this.Size = new System.Drawing.Size(526, 400);
            this.TabControl.ResumeLayout(false);
            this.TabAttributes.ResumeLayout(false);
            this.TabNameDefinition.ResumeLayout(false);
            this.TabCustomAttributes.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabNameDefinition;
        private Reflexil.Editors.AssemblyNameDefinitionAttributesControl NameDefinition;
        private System.Windows.Forms.TabPage TabAttributes;
        private Reflexil.Editors.AssemblyDefinitionControl Definition;
        private System.Windows.Forms.TabPage TabCustomAttributes;
        private Editors.CustomAttributeGridControl CustomAttributes;

    }
}
