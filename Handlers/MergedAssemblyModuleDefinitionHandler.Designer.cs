namespace Reflexil.Handlers
{
    partial class MergedAssemblyModuleDefinitionHandler
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
            this.TabAssemblyAttributes = new System.Windows.Forms.TabPage();
            this.AssemblyDefinition = new Reflexil.Editors.AssemblyDefinitionControl();
            this.TabAssemblyNameDefinition = new System.Windows.Forms.TabPage();
            this.AssemblyNameDefinition = new Reflexil.Editors.AssemblyNameDefinitionAttributesControl();
            this.TabAssemblyCustomAttributes = new System.Windows.Forms.TabPage();
            this.AssemblyCustomAttributes = new Reflexil.Editors.CustomAttributeGridControl();
			this.TabModuleAttributes = new System.Windows.Forms.TabPage();
			this.ModuleDefinition = new Reflexil.Editors.ModuleDefinitionControl();
			this.TabModuleCustomAttributes = new System.Windows.Forms.TabPage();
			this.ModuleCustomAttributes = new Reflexil.Editors.CustomAttributeGridControl();
			this.TabControl.SuspendLayout();
            this.TabAssemblyAttributes.SuspendLayout();
            this.TabAssemblyNameDefinition.SuspendLayout();
            this.TabAssemblyCustomAttributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabAssemblyAttributes);
            this.TabControl.Controls.Add(this.TabAssemblyNameDefinition);
            this.TabControl.Controls.Add(this.TabAssemblyCustomAttributes);
			this.TabControl.Controls.Add(this.TabModuleAttributes);
			this.TabControl.Controls.Add(this.TabModuleCustomAttributes);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(526, 400);
            this.TabControl.TabIndex = 0;
            // 
            // TabAssemblyAttributes
            // 
            this.TabAssemblyAttributes.Controls.Add(this.AssemblyDefinition);
            this.TabAssemblyAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabAssemblyAttributes.Name = "TabAssemblyAttributes";
            this.TabAssemblyAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabAssemblyAttributes.Size = new System.Drawing.Size(518, 374);
            this.TabAssemblyAttributes.TabIndex = 1;
            this.TabAssemblyAttributes.Text = "Assembly attributes";
            this.TabAssemblyAttributes.UseVisualStyleBackColor = true;
            // 
            // AssemblyDefinition
            // 
            this.AssemblyDefinition.Item = null;
            this.AssemblyDefinition.Location = new System.Drawing.Point(7, 7);
            this.AssemblyDefinition.Name = "AssemblyDefinition";
            this.AssemblyDefinition.ReadOnly = false;
            this.AssemblyDefinition.Size = new System.Drawing.Size(414, 115);
            this.AssemblyDefinition.TabIndex = 0;
            // 
            // TabAssemblyNameDefinition
            // 
            this.TabAssemblyNameDefinition.Controls.Add(this.AssemblyNameDefinition);
            this.TabAssemblyNameDefinition.Location = new System.Drawing.Point(4, 22);
            this.TabAssemblyNameDefinition.Name = "TabAssemblyNameDefinition";
            this.TabAssemblyNameDefinition.Padding = new System.Windows.Forms.Padding(3);
            this.TabAssemblyNameDefinition.Size = new System.Drawing.Size(518, 374);
            this.TabAssemblyNameDefinition.TabIndex = 0;
            this.TabAssemblyNameDefinition.Text = "Assembly name definition";
            this.TabAssemblyNameDefinition.UseVisualStyleBackColor = true;
            // 
            // AssemblyNameDefinition
            // 
            this.AssemblyNameDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssemblyNameDefinition.Item = null;
            this.AssemblyNameDefinition.Location = new System.Drawing.Point(3, 3);
            this.AssemblyNameDefinition.Name = "AssemblyNameDefinition";
            this.AssemblyNameDefinition.ReadOnly = false;
            this.AssemblyNameDefinition.Size = new System.Drawing.Size(512, 368);
            this.AssemblyNameDefinition.TabIndex = 0;
            // 
            // TabAssemblyCustomAttributes
            // 
            this.TabAssemblyCustomAttributes.Controls.Add(this.AssemblyCustomAttributes);
            this.TabAssemblyCustomAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabAssemblyCustomAttributes.Name = "TabAssemblyCustomAttributes";
            this.TabAssemblyCustomAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabAssemblyCustomAttributes.Size = new System.Drawing.Size(518, 374);
            this.TabAssemblyCustomAttributes.TabIndex = 2;
            this.TabAssemblyCustomAttributes.Text = "Assembly custom attributes";
            this.TabAssemblyCustomAttributes.UseVisualStyleBackColor = true;
            // 
            // AssemblyCustomAttributes
            // 
            this.AssemblyCustomAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssemblyCustomAttributes.Location = new System.Drawing.Point(3, 3);
            this.AssemblyCustomAttributes.Name = "AssemblyCustomAttributes";
            this.AssemblyCustomAttributes.ReadOnly = false;
            this.AssemblyCustomAttributes.Size = new System.Drawing.Size(512, 368);
            this.AssemblyCustomAttributes.TabIndex = 0;
            this.AssemblyCustomAttributes.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.CustomAttribute, Mono.Cecil.ICustomAttributeProvider>.GridUpdatedEventHandler(this.AssemblyCustomAttributes_GridUpdated);
			// 
			// TabModuleAttributes
			// 
			this.TabModuleAttributes.Controls.Add(this.ModuleDefinition);
			this.TabModuleAttributes.Location = new System.Drawing.Point(4, 22);
			this.TabModuleAttributes.Name = "TabModuleAttributes";
			this.TabModuleAttributes.Padding = new System.Windows.Forms.Padding(3);
			this.TabModuleAttributes.Size = new System.Drawing.Size(518, 374);
			this.TabModuleAttributes.TabIndex = 3;
			this.TabModuleAttributes.Text = "Module attributes";
			this.TabModuleAttributes.UseVisualStyleBackColor = true;
			// 
			// ModuleDefinition
			// 
			this.ModuleDefinition.Item = null;
			this.ModuleDefinition.Location = new System.Drawing.Point(7, 7);
			this.ModuleDefinition.Name = "ModuleDefinition";
			this.ModuleDefinition.ReadOnly = false;
			this.ModuleDefinition.Size = new System.Drawing.Size(414, 115);
			this.ModuleDefinition.TabIndex = 0;
			// 
			// TabModuleCustomAttributes
			// 
			this.TabModuleCustomAttributes.Controls.Add(this.ModuleCustomAttributes);
			this.TabModuleCustomAttributes.Location = new System.Drawing.Point(4, 22);
			this.TabModuleCustomAttributes.Name = "TabModuleCustomAttributes";
			this.TabModuleCustomAttributes.Padding = new System.Windows.Forms.Padding(3);
			this.TabModuleCustomAttributes.Size = new System.Drawing.Size(518, 374);
			this.TabModuleCustomAttributes.TabIndex = 4;
			this.TabModuleCustomAttributes.Text = "Module custom attributes";
			this.TabModuleCustomAttributes.UseVisualStyleBackColor = true;
			// 
			// ModuleCustomAttributes
			// 
			this.ModuleCustomAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ModuleCustomAttributes.Location = new System.Drawing.Point(3, 3);
			this.ModuleCustomAttributes.Name = "ModuleCustomAttributes";
			this.ModuleCustomAttributes.ReadOnly = false;
			this.ModuleCustomAttributes.Size = new System.Drawing.Size(512, 368);
			this.ModuleCustomAttributes.TabIndex = 0;
			this.ModuleCustomAttributes.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.CustomAttribute, Mono.Cecil.ICustomAttributeProvider>.GridUpdatedEventHandler(this.ModuleCustomAttributes_GridUpdated);
			// 
            // AssemblyDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "AssemblyDefinitionHandler";
            this.Size = new System.Drawing.Size(526, 400);
            this.TabControl.ResumeLayout(false);
            this.TabAssemblyAttributes.ResumeLayout(false);
            this.TabAssemblyNameDefinition.ResumeLayout(false);
            this.TabAssemblyCustomAttributes.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabAssemblyNameDefinition;
        private Reflexil.Editors.AssemblyNameDefinitionAttributesControl AssemblyNameDefinition;
        private System.Windows.Forms.TabPage TabAssemblyAttributes;
        private Reflexil.Editors.AssemblyDefinitionControl AssemblyDefinition;
        private System.Windows.Forms.TabPage TabAssemblyCustomAttributes;
        private Editors.CustomAttributeGridControl AssemblyCustomAttributes;
		private System.Windows.Forms.TabPage TabModuleAttributes;
		private Reflexil.Editors.ModuleDefinitionControl ModuleDefinition;
		private System.Windows.Forms.TabPage TabModuleCustomAttributes;
		private Reflexil.Editors.CustomAttributeGridControl ModuleCustomAttributes;



	}
}
