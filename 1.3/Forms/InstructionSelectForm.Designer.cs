using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Forms
{
	public partial class InstructionSelectForm : System.Windows.Forms.Form
	{
		
		//Form overrides dispose to clean up the component list.
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
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.ButCancel = new System.Windows.Forms.Button();
            this.ButOk = new System.Windows.Forms.Button();
            this.SelectionBox = new System.Windows.Forms.GroupBox();
            this.PanelSelection = new System.Windows.Forms.Panel();
            this.LbxSelection = new System.Windows.Forms.ListBox();
            this.MovePanel = new System.Windows.Forms.Panel();
            this.ButBottom = new System.Windows.Forms.Button();
            this.ButTop = new System.Windows.Forms.Button();
            this.ButDown = new System.Windows.Forms.Button();
            this.ButUp = new System.Windows.Forms.Button();
            this.InstructionBox = new System.Windows.Forms.GroupBox();
            this.LbxInstructions = new System.Windows.Forms.ListBox();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.BottomPanel.SuspendLayout();
            this.SelectionBox.SuspendLayout();
            this.PanelSelection.SuspendLayout();
            this.MovePanel.SuspendLayout();
            this.InstructionBox.SuspendLayout();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.ButCancel);
            this.BottomPanel.Controls.Add(this.ButOk);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 300);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(546, 38);
            this.BottomPanel.TabIndex = 1;
            // 
            // ButCancel
            // 
            this.ButCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButCancel.Location = new System.Drawing.Point(464, 7);
            this.ButCancel.Name = "ButCancel";
            this.ButCancel.Size = new System.Drawing.Size(75, 23);
            this.ButCancel.TabIndex = 1;
            this.ButCancel.Text = "Cancel";
            this.ButCancel.UseVisualStyleBackColor = true;
            // 
            // ButOk
            // 
            this.ButOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButOk.Location = new System.Drawing.Point(383, 7);
            this.ButOk.Name = "ButOk";
            this.ButOk.Size = new System.Drawing.Size(75, 23);
            this.ButOk.TabIndex = 0;
            this.ButOk.Text = "OK";
            this.ButOk.UseVisualStyleBackColor = true;
            // 
            // SelectionBox
            // 
            this.SelectionBox.Controls.Add(this.PanelSelection);
            this.SelectionBox.Controls.Add(this.MovePanel);
            this.SelectionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectionBox.Location = new System.Drawing.Point(0, 0);
            this.SelectionBox.Name = "SelectionBox";
            this.SelectionBox.Size = new System.Drawing.Size(288, 300);
            this.SelectionBox.TabIndex = 2;
            this.SelectionBox.TabStop = false;
            this.SelectionBox.Text = "Selection (dbl-clic to remove an item)";
            // 
            // PanelSelection
            // 
            this.PanelSelection.Controls.Add(this.LbxSelection);
            this.PanelSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSelection.Location = new System.Drawing.Point(3, 16);
            this.PanelSelection.Name = "PanelSelection";
            this.PanelSelection.Size = new System.Drawing.Size(282, 249);
            this.PanelSelection.TabIndex = 2;
            // 
            // LbxSelection
            // 
            this.LbxSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbxSelection.FormattingEnabled = true;
            this.LbxSelection.Location = new System.Drawing.Point(0, 0);
            this.LbxSelection.Name = "LbxSelection";
            this.LbxSelection.Size = new System.Drawing.Size(282, 238);
            this.LbxSelection.TabIndex = 0;
            this.LbxSelection.DoubleClick += new System.EventHandler(this.LbxSelection_DoubleClick);
            // 
            // MovePanel
            // 
            this.MovePanel.Controls.Add(this.ButBottom);
            this.MovePanel.Controls.Add(this.ButTop);
            this.MovePanel.Controls.Add(this.ButDown);
            this.MovePanel.Controls.Add(this.ButUp);
            this.MovePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MovePanel.Location = new System.Drawing.Point(3, 265);
            this.MovePanel.Name = "MovePanel";
            this.MovePanel.Size = new System.Drawing.Size(282, 32);
            this.MovePanel.TabIndex = 1;
            // 
            // ButBottom
            // 
            this.ButBottom.Location = new System.Drawing.Point(213, 5);
            this.ButBottom.Name = "ButBottom";
            this.ButBottom.Size = new System.Drawing.Size(64, 23);
            this.ButBottom.TabIndex = 4;
            this.ButBottom.Text = "&Bottom";
            this.ButBottom.UseVisualStyleBackColor = true;
            this.ButBottom.Click += new System.EventHandler(this.ButBottom_Click);
            // 
            // ButTop
            // 
            this.ButTop.Location = new System.Drawing.Point(3, 5);
            this.ButTop.Name = "ButTop";
            this.ButTop.Size = new System.Drawing.Size(64, 23);
            this.ButTop.TabIndex = 3;
            this.ButTop.Text = "&Top";
            this.ButTop.UseVisualStyleBackColor = true;
            this.ButTop.Click += new System.EventHandler(this.ButTop_Click);
            // 
            // ButDown
            // 
            this.ButDown.Location = new System.Drawing.Point(143, 5);
            this.ButDown.Name = "ButDown";
            this.ButDown.Size = new System.Drawing.Size(64, 23);
            this.ButDown.TabIndex = 2;
            this.ButDown.Text = "&Down";
            this.ButDown.UseVisualStyleBackColor = true;
            this.ButDown.Click += new System.EventHandler(this.ButDown_Click);
            // 
            // ButUp
            // 
            this.ButUp.Location = new System.Drawing.Point(73, 5);
            this.ButUp.Name = "ButUp";
            this.ButUp.Size = new System.Drawing.Size(64, 23);
            this.ButUp.TabIndex = 1;
            this.ButUp.Text = "&Up";
            this.ButUp.UseVisualStyleBackColor = true;
            this.ButUp.Click += new System.EventHandler(this.ButUp_Click);
            // 
            // InstructionBox
            // 
            this.InstructionBox.Controls.Add(this.LbxInstructions);
            this.InstructionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstructionBox.Location = new System.Drawing.Point(0, 0);
            this.InstructionBox.MinimumSize = new System.Drawing.Size(210, 0);
            this.InstructionBox.Name = "InstructionBox";
            this.InstructionBox.Size = new System.Drawing.Size(254, 300);
            this.InstructionBox.TabIndex = 3;
            this.InstructionBox.TabStop = false;
            this.InstructionBox.Text = "Instructions (dbl-clic to add to selection)";
            // 
            // LbxInstructions
            // 
            this.LbxInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbxInstructions.FormattingEnabled = true;
            this.LbxInstructions.Location = new System.Drawing.Point(3, 16);
            this.LbxInstructions.Name = "LbxInstructions";
            this.LbxInstructions.Size = new System.Drawing.Size(248, 277);
            this.LbxInstructions.TabIndex = 0;
            this.LbxInstructions.DoubleClick += new System.EventHandler(this.LbxInstructions_DoubleClick);
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.AutoScroll = true;
            this.SplitContainer.Panel1.AutoScrollMinSize = new System.Drawing.Size(288, 0);
            this.SplitContainer.Panel1.Controls.Add(this.SelectionBox);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.InstructionBox);
            this.SplitContainer.Size = new System.Drawing.Size(546, 300);
            this.SplitContainer.SplitterDistance = 288;
            this.SplitContainer.TabIndex = 4;
            // 
            // InstructionSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 338);
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstructionSelectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select instructions";
            this.BottomPanel.ResumeLayout(false);
            this.SelectionBox.ResumeLayout(false);
            this.PanelSelection.ResumeLayout(false);
            this.MovePanel.ResumeLayout(false);
            this.InstructionBox.ResumeLayout(false);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Panel BottomPanel;
		internal System.Windows.Forms.Button ButCancel;
		internal System.Windows.Forms.Button ButOk;
		internal System.Windows.Forms.GroupBox SelectionBox;
		internal System.Windows.Forms.GroupBox InstructionBox;
		internal System.Windows.Forms.ListBox LbxSelection;
		internal System.Windows.Forms.ListBox LbxInstructions;
		internal System.Windows.Forms.SplitContainer SplitContainer;
		internal System.Windows.Forms.Panel MovePanel;
		internal System.Windows.Forms.Button ButBottom;
		internal System.Windows.Forms.Button ButTop;
		internal System.Windows.Forms.Button ButDown;
		internal System.Windows.Forms.Button ButUp;
		internal System.Windows.Forms.Panel PanelSelection;
	}
}
