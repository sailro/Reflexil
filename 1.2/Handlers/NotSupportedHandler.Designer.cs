using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Handlers
{
	public partial class NotSupportedHandler : System.Windows.Forms.UserControl
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
            this.LabInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabInfo
            // 
            this.LabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabInfo.Location = new System.Drawing.Point(0, 0);
            this.LabInfo.Name = "LabInfo";
            this.LabInfo.Size = new System.Drawing.Size(499, 262);
            this.LabInfo.TabIndex = 0;
            this.LabInfo.Text = "This item is not supported by Reflexil. \r\n\r\nSupported items:\r\n";
            // 
            // NotSupportedHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabInfo);
            this.Name = "NotSupportedHandler";
            this.Size = new System.Drawing.Size(499, 262);
            this.ResumeLayout(false);

        }

        public System.Windows.Forms.Label LabInfo;
		
	}
}

