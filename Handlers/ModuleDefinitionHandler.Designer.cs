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
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Definition = new Reflexil.Editors.ModuleDefinitionControl();
            this.SuspendLayout();
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "Assembly files (*.exe, *.dll)|*.exe;*.dll";
            // 
            // Definition
            // 
            this.Definition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Definition.Item = null;
            this.Definition.Location = new System.Drawing.Point(0, 0);
            this.Definition.Name = "Definition";
            this.Definition.ReadOnly = false;
            this.Definition.Size = new System.Drawing.Size(312, 126);
            this.Definition.TabIndex = 0;
            // 
            // ModuleDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Definition);
            this.Name = "ModuleDefinitionHandler";
            this.Size = new System.Drawing.Size(312, 126);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private Editors.ModuleDefinitionControl Definition;
		
	}
}
