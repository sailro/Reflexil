using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Forms
{
	
	public partial class ReflexilWindow : System.Windows.Forms.UserControl
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
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.SNRemover = new System.Windows.Forms.Label();
            this.Configure = new System.Windows.Forms.Label();
            this.PGrid = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn1.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            // 
            // DataGridViewTextBoxColumn2
            // 
            this.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn2.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn2.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            this.DataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // DataGridViewTextBoxColumn3
            // 
            this.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn3.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn3.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            this.DataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // DataGridViewTextBoxColumn4
            // 
            this.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn4.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn4.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
            this.DataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // DataGridViewTextBoxColumn5
            // 
            this.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn5.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn5.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5";
            // 
            // DataGridViewTextBoxColumn6
            // 
            this.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn6.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn6.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6";
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.TopPanel);
            this.MainPanel.Controls.Add(this.BottomPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(529, 375);
            this.MainPanel.TabIndex = 5;
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.GroupBox);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(529, 355);
            this.TopPanel.TabIndex = 7;
            // 
            // GroupBox
            // 
            this.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox.Location = new System.Drawing.Point(0, 0);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(529, 355);
            this.GroupBox.TabIndex = 0;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Handler name";
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.PGrid);
            this.BottomPanel.Controls.Add(this.SNRemover);
            this.BottomPanel.Controls.Add(this.Configure);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 355);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(529, 20);
            this.BottomPanel.TabIndex = 6;
            // 
            // SNRemover
            // 
            this.SNRemover.AutoSize = true;
            this.SNRemover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SNRemover.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SNRemover.ForeColor = System.Drawing.Color.Blue;
            this.SNRemover.Location = new System.Drawing.Point(116, 3);
            this.SNRemover.Name = "SNRemover";
            this.SNRemover.Size = new System.Drawing.Size(133, 13);
            this.SNRemover.TabIndex = 6;
            this.SNRemover.Text = "[Strong Name Remover ...]";
            this.SNRemover.Click += new System.EventHandler(this.SNRemover_Click);
            // 
            // Configure
            // 
            this.Configure.AutoSize = true;
            this.Configure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Configure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Configure.ForeColor = System.Drawing.Color.Blue;
            this.Configure.Location = new System.Drawing.Point(3, 3);
            this.Configure.Name = "Configure";
            this.Configure.Size = new System.Drawing.Size(107, 13);
            this.Configure.TabIndex = 5;
            this.Configure.Text = "[Configure Reflexil ...]";
            this.Configure.Click += new System.EventHandler(this.Configure_Click);
            // 
            // PGrid
            // 
            this.PGrid.AutoSize = true;
            this.PGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PGrid.ForeColor = System.Drawing.Color.Blue;
            this.PGrid.Location = new System.Drawing.Point(255, 3);
            this.PGrid.Name = "PGrid";
            this.PGrid.Size = new System.Drawing.Size(83, 13);
            this.PGrid.TabIndex = 7;
            this.PGrid.Text = "[PropertyGrid ...]";
            this.PGrid.Visible = false;
            this.PGrid.Click += new System.EventHandler(this.PGrid_Click);
            // 
            // ReflexilWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "ReflexilWindow";
            this.Size = new System.Drawing.Size(529, 375);
            this.MainPanel.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn4;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn5;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn6;
		internal System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label Configure;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.Label SNRemover;
        private System.Windows.Forms.Label PGrid;
		
	}
}
