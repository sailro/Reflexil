namespace Reflexil.Editors
{
	partial class TextComboUserControl
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
			this.ItemSplitContainer = new System.Windows.Forms.SplitContainer();
			this.TextBox = new System.Windows.Forms.TextBox();
			this.BaseCombo = new System.Windows.Forms.ComboBox();
			this.ItemSplitContainer.Panel1.SuspendLayout();
			this.ItemSplitContainer.Panel2.SuspendLayout();
			this.ItemSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// ItemSplitContainer
			// 
			this.ItemSplitContainer.BackColor = System.Drawing.SystemColors.Control;
			this.ItemSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.ItemSplitContainer.IsSplitterFixed = true;
			this.ItemSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.ItemSplitContainer.Margin = new System.Windows.Forms.Padding(4);
			this.ItemSplitContainer.Name = "ItemSplitContainer";
			// 
			// ItemSplitContainer.Panel1
			// 
			this.ItemSplitContainer.Panel1.Controls.Add(this.TextBox);
			// 
			// ItemSplitContainer.Panel2
			// 
			this.ItemSplitContainer.Panel2.Controls.Add(this.BaseCombo);
			this.ItemSplitContainer.Size = new System.Drawing.Size(161, 26);
			this.ItemSplitContainer.SplitterDistance = 98;
			this.ItemSplitContainer.SplitterWidth = 5;
			this.ItemSplitContainer.TabIndex = 0;
			// 
			// TextBox
			// 
			this.TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBox.Location = new System.Drawing.Point(0, 0);
			this.TextBox.Margin = new System.Windows.Forms.Padding(4);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(98, 22);
			this.TextBox.TabIndex = 0;
			// 
			// BaseCombo
			// 
			this.BaseCombo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BaseCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BaseCombo.FormattingEnabled = true;
			this.BaseCombo.Location = new System.Drawing.Point(0, 0);
			this.BaseCombo.Margin = new System.Windows.Forms.Padding(4);
			this.BaseCombo.Name = "BaseCombo";
			this.BaseCombo.Size = new System.Drawing.Size(58, 24);
			this.BaseCombo.TabIndex = 1;
			this.BaseCombo.SelectionChangeCommitted += new System.EventHandler(this.BaseCombo_SelectionChangeCommitted);
			// 
			// TextComboUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ItemSplitContainer);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "TextComboUserControl";
			this.Size = new System.Drawing.Size(161, 26);
			this.ItemSplitContainer.Panel1.ResumeLayout(false);
			this.ItemSplitContainer.Panel1.PerformLayout();
			this.ItemSplitContainer.Panel2.ResumeLayout(false);
			this.ItemSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer ItemSplitContainer;
        private System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.ComboBox BaseCombo;
	}
}
