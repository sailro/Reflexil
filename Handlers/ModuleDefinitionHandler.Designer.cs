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
            this.ButSaveAs = new System.Windows.Forms.Button();
            this.LabInfo = new System.Windows.Forms.Label();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ButReload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButSaveAs
            // 
            this.ButSaveAs.Location = new System.Drawing.Point(13, 34);
            this.ButSaveAs.Name = "ButSaveAs";
            this.ButSaveAs.Size = new System.Drawing.Size(198, 23);
            this.ButSaveAs.TabIndex = 1;
            this.ButSaveAs.Text = "Save as ...";
            this.ButSaveAs.UseVisualStyleBackColor = true;
            this.ButSaveAs.Click += new System.EventHandler(this.ButSaveAs_Click);
            // 
            // LabInfo
            // 
            this.LabInfo.Location = new System.Drawing.Point(10, 13);
            this.LabInfo.Name = "LabInfo";
            this.LabInfo.Size = new System.Drawing.Size(164, 13);
            this.LabInfo.TabIndex = 4;
            this.LabInfo.Text = "You can save a patched version:";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "Assembly files (*.exe, *.dll)|*.exe;*.dll";
            // 
            // ButReload
            // 
            this.ButReload.Location = new System.Drawing.Point(13, 63);
            this.ButReload.Name = "ButReload";
            this.ButReload.Size = new System.Drawing.Size(198, 23);
            this.ButReload.TabIndex = 5;
            this.ButReload.Text = "Reload";
            this.ButReload.UseVisualStyleBackColor = true;
            this.ButReload.Click += new System.EventHandler(this.ButReload_Click);
            // 
            // ModuleDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButReload);
            this.Controls.Add(this.LabInfo);
            this.Controls.Add(this.ButSaveAs);
            this.Name = "ModuleDefinitionHandler";
            this.Size = new System.Drawing.Size(220, 96);
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Button ButSaveAs;
		internal System.Windows.Forms.Label LabInfo;
		internal System.Windows.Forms.SaveFileDialog SaveFileDialog;
		internal System.Windows.Forms.Button ButReload;
		
	}
}
