using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Handlers
{
	public partial class ModuleDefinitionHandler : System.Windows.Forms.UserControl
	{
		
		
		//UserControl remplace la mÃ©thode Dispose pour nettoyer la liste des composants.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && (components != null))
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		
		//Requise par le Concepteur Windows Form
		private System.ComponentModel.Container components = null;
		
		//REMARQUEÂ : la procÃ©dure suivante est requise par le Concepteur Windows Form
		//Elle peut Ãªtre modifiÃ©e Ã  l'aide du Concepteur Windows Form.
		//Ne la modifiez pas Ã  l'aide de l'Ã©diteur de code.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.Definition = new Reflexil.Editors.ModuleDefinitionControl();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.TabAttributes = new System.Windows.Forms.TabPage();
			this.TabCustomAttributes = new System.Windows.Forms.TabPage();
			this.CustomAttributes = new Reflexil.Editors.CustomAttributeGridControl();
			this.TabControl.SuspendLayout();
			this.TabAttributes.SuspendLayout();
			this.TabCustomAttributes.SuspendLayout();
			this.SuspendLayout();
			// 
			// Definition
			// 
			this.Definition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Definition.Item = null;
			this.Definition.Location = new System.Drawing.Point(3, 3);
			this.Definition.Margin = new System.Windows.Forms.Padding(4);
			this.Definition.MaximumSize = new System.Drawing.Size(500, 140);
			this.Definition.MinimumSize = new System.Drawing.Size(192, 140);
			this.Definition.Name = "Definition";
			this.Definition.ReadOnly = false;
			this.Definition.Size = new System.Drawing.Size(500, 140);
			this.Definition.TabIndex = 0;
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.TabAttributes);
			this.TabControl.Controls.Add(this.TabCustomAttributes);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 0);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(514, 277);
			this.TabControl.TabIndex = 1;
			// 
			// TabAttributes
			// 
			this.TabAttributes.Controls.Add(this.Definition);
			this.TabAttributes.Location = new System.Drawing.Point(4, 22);
			this.TabAttributes.Name = "TabAttributes";
			this.TabAttributes.Padding = new System.Windows.Forms.Padding(3);
			this.TabAttributes.Size = new System.Drawing.Size(506, 251);
			this.TabAttributes.TabIndex = 0;
			this.TabAttributes.Text = "Attributes";
			this.TabAttributes.UseVisualStyleBackColor = true;
			// 
			// TabCustomAttributes
			// 
			this.TabCustomAttributes.Controls.Add(this.CustomAttributes);
			this.TabCustomAttributes.Location = new System.Drawing.Point(4, 22);
			this.TabCustomAttributes.Name = "TabCustomAttributes";
			this.TabCustomAttributes.Padding = new System.Windows.Forms.Padding(3);
			this.TabCustomAttributes.Size = new System.Drawing.Size(506, 251);
			this.TabCustomAttributes.TabIndex = 1;
			this.TabCustomAttributes.Text = "Custom attributes";
			this.TabCustomAttributes.UseVisualStyleBackColor = true;
			// 
			// CustomAttributes
			// 
			this.CustomAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CustomAttributes.Location = new System.Drawing.Point(3, 3);
			this.CustomAttributes.Margin = new System.Windows.Forms.Padding(4);
			this.CustomAttributes.Name = "CustomAttributes";
			this.CustomAttributes.ReadOnly = false;
			this.CustomAttributes.Size = new System.Drawing.Size(500, 245);
			this.CustomAttributes.TabIndex = 0;
			this.CustomAttributes.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.CustomAttribute, Mono.Cecil.ICustomAttributeProvider>.GridUpdatedEventHandler(this.CustomAttributes_GridUpdated);
			// 
			// ModuleDefinitionHandler
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TabControl);
			this.Name = "ModuleDefinitionHandler";
			this.Size = new System.Drawing.Size(514, 277);
			this.TabControl.ResumeLayout(false);
			this.TabAttributes.ResumeLayout(false);
			this.TabCustomAttributes.ResumeLayout(false);
			this.ResumeLayout(false);

        }
        private Editors.ModuleDefinitionControl Definition;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabAttributes;
        private System.Windows.Forms.TabPage TabCustomAttributes;
        private Editors.CustomAttributeGridControl CustomAttributes;
		
	}
}
