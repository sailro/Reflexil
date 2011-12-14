using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;
using Mono.Cecil;


namespace Reflexil.Forms
{
    public partial class GenericMemberReferenceForm<T> : System.Windows.Forms.Form where T : MemberReference 
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
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.Search = new System.Windows.Forms.TextBox();
            this.ButCancel = new System.Windows.Forms.Button();
            this.ButOk = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.LabSearching = new System.Windows.Forms.Label();
            this.BottomPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView
            // 
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.HideSelection = false;
            this.TreeView.ImageIndex = 0;
            this.TreeView.ImageList = this.ImageList;
            this.TreeView.Location = new System.Drawing.Point(0, 0);
            this.TreeView.Name = "TreeView";
            this.TreeView.SelectedImageIndex = 0;
            this.TreeView.Size = new System.Drawing.Size(464, 570);
            this.TreeView.TabIndex = 0;
            this.TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
            this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // ImageList
            // 
            this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList.TransparentColor = System.Drawing.Color.Green;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.Search);
            this.BottomPanel.Controls.Add(this.ButCancel);
            this.BottomPanel.Controls.Add(this.ButOk);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 570);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(464, 36);
            this.BottomPanel.TabIndex = 2;
            // 
            // Search
            // 
            this.Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Search.Location = new System.Drawing.Point(4, 8);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(292, 20);
            this.Search.TabIndex = 2;
            this.Search.TextChanged += new System.EventHandler(this.Search_TextChanged);
            // 
            // ButCancel
            // 
            this.ButCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButCancel.Location = new System.Drawing.Point(383, 6);
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
            this.ButOk.Enabled = false;
            this.ButOk.Location = new System.Drawing.Point(302, 6);
            this.ButOk.Name = "ButOk";
            this.ButOk.Size = new System.Drawing.Size(75, 23);
            this.ButOk.TabIndex = 0;
            this.ButOk.Text = "Ok";
            this.ButOk.UseVisualStyleBackColor = true;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.TreeView);
            this.MainPanel.Controls.Add(this.LabSearching);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(464, 570);
            this.MainPanel.TabIndex = 3;
            // 
            // LabSearching
            // 
            this.LabSearching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabSearching.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabSearching.Location = new System.Drawing.Point(0, 0);
            this.LabSearching.Name = "LabSearching";
            this.LabSearching.Size = new System.Drawing.Size(464, 570);
            this.LabSearching.TabIndex = 1;
            this.LabSearching.Text = "Searching...";
            this.LabSearching.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GenericMemberReferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 606);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(256, 128);
            this.Name = "GenericMemberReferenceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please select a ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GenericMemberReferenceForm_FormClosing);
            this.Load += new System.EventHandler(this.MemberReferenceForm_Load);
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.TreeView TreeView;
		internal System.Windows.Forms.ImageList ImageList;
		internal System.Windows.Forms.Panel BottomPanel;
		internal System.Windows.Forms.Button ButCancel;
		internal System.Windows.Forms.Button ButOk;
		internal System.Windows.Forms.Panel MainPanel;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.TextBox Search;
        private System.Windows.Forms.Label LabSearching;
	}
}

