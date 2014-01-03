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
			this.Attributes = new System.Windows.Forms.TabPage();
			this.TabCustomAttributes = new System.Windows.Forms.TabPage();
			this.CustomAttributes = new Reflexil.Editors.CustomAttributeGridControl();
			this.TabControl.SuspendLayout();
			this.Attributes.SuspendLayout();
			this.TabCustomAttributes.SuspendLayout();
			this.SuspendLayout();
			// 
			// Definition
			// 
			this.Definition.Item = null;
			this.Definition.Location = new System.Drawing.Point(4, 4);
			this.Definition.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.Definition.Name = "Definition";
			this.Definition.ReadOnly = false;
			this.Definition.Size = new System.Drawing.Size(667, 302);
			this.Definition.TabIndex = 0;
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.Attributes);
			this.TabControl.Controls.Add(this.TabCustomAttributes);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 0);
			this.TabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(685, 341);
			this.TabControl.TabIndex = 1;
			// 
			// Attributes
			// 
			this.Attributes.Controls.Add(this.Definition);
			this.Attributes.Location = new System.Drawing.Point(4, 25);
			this.Attributes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Attributes.Name = "Attributes";
			this.Attributes.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Attributes.Size = new System.Drawing.Size(677, 312);
			this.Attributes.TabIndex = 0;
			this.Attributes.Text = "Attributes";
			this.Attributes.UseVisualStyleBackColor = true;
			// 
			// TabCustomAttributes
			// 
			this.TabCustomAttributes.Controls.Add(this.CustomAttributes);
			this.TabCustomAttributes.Location = new System.Drawing.Point(4, 25);
			this.TabCustomAttributes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.TabCustomAttributes.Name = "TabCustomAttributes";
			this.TabCustomAttributes.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.TabCustomAttributes.Size = new System.Drawing.Size(677, 312);
			this.TabCustomAttributes.TabIndex = 1;
			this.TabCustomAttributes.Text = "Custom attributes";
			this.TabCustomAttributes.UseVisualStyleBackColor = true;
			// 
			// CustomAttributes
			// 
			this.CustomAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CustomAttributes.Location = new System.Drawing.Point(4, 4);
			this.CustomAttributes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.CustomAttributes.Name = "CustomAttributes";
			this.CustomAttributes.ReadOnly = false;
			this.CustomAttributes.Size = new System.Drawing.Size(669, 304);
			this.CustomAttributes.TabIndex = 0;
			this.CustomAttributes.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.CustomAttribute, Mono.Cecil.ICustomAttributeProvider>.GridUpdatedEventHandler(this.CustomAttributes_GridUpdated);
			// 
			// ModuleDefinitionHandler
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TabControl);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "ModuleDefinitionHandler";
			this.Size = new System.Drawing.Size(685, 341);
			this.TabControl.ResumeLayout(false);
			this.Attributes.ResumeLayout(false);
			this.TabCustomAttributes.ResumeLayout(false);
			this.ResumeLayout(false);

        }
        private Editors.ModuleDefinitionControl Definition;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage Attributes;
        private System.Windows.Forms.TabPage TabCustomAttributes;
        private Editors.CustomAttributeGridControl CustomAttributes;
		
	}
}
