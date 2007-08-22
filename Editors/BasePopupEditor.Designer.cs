using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Editors
{
	
	public partial class BasePopupEditor : System.Windows.Forms.UserControl
	{
		
		
		//UserControl overrides dispose to clean up the component list.
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
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.ButSelect = new System.Windows.Forms.Button();
			this.ButSelect.Click += new System.EventHandler(OnSelectClick);
			this.LabCaption = new System.Windows.Forms.Label();
			this.RightPanel = new System.Windows.Forms.Panel();
			this.LeftPanel = new System.Windows.Forms.Panel();
			this.RightPanel.SuspendLayout();
			this.LeftPanel.SuspendLayout();
			this.SuspendLayout();
			//
			//ButSelect
			//
			this.ButSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ButSelect.Location = new System.Drawing.Point(0, 0);
			this.ButSelect.Name = "ButSelect";
			this.ButSelect.Size = new System.Drawing.Size(19, 21);
			this.ButSelect.TabIndex = 3;
			this.ButSelect.Text = ".";
			this.ButSelect.UseVisualStyleBackColor = true;
			//
			//LabCaption
			//
			this.LabCaption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LabCaption.Location = new System.Drawing.Point(0, 0);
			this.LabCaption.Name = "LabCaption";
			this.LabCaption.Size = new System.Drawing.Size(146, 21);
			this.LabCaption.TabIndex = 4;
			this.LabCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//RightPanel
			//
			this.RightPanel.Controls.Add(this.ButSelect);
			this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.RightPanel.Location = new System.Drawing.Point(146, 0);
			this.RightPanel.Name = "RightPanel";
			this.RightPanel.Size = new System.Drawing.Size(19, 21);
			this.RightPanel.TabIndex = 5;
			//
			//LeftPanel
			//
			this.LeftPanel.Controls.Add(this.LabCaption);
			this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LeftPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftPanel.Name = "LeftPanel";
			this.LeftPanel.Size = new System.Drawing.Size(146, 21);
			this.LeftPanel.TabIndex = 6;
			//
			//BasePopupEditor
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0f, 13.0f);
            //MONO this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.LeftPanel);
			this.Controls.Add(this.RightPanel);
			this.Name = "BasePopupEditor";
			this.Size = new System.Drawing.Size(165, 21);
			this.RightPanel.ResumeLayout(false);
			this.LeftPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		internal System.Windows.Forms.Button ButSelect;
		internal System.Windows.Forms.Label LabCaption;
		internal System.Windows.Forms.Panel RightPanel;
		internal System.Windows.Forms.Panel LeftPanel;
		
	}
}

