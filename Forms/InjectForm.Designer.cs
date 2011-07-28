namespace Reflexil.Forms
{
	partial class InjectForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.LabOwnerType = new System.Windows.Forms.Label();
            this.OwnerType = new System.Windows.Forms.ComboBox();
            this.ItemName = new System.Windows.Forms.TextBox();
            this.LabItemName = new System.Windows.Forms.Label();
            this.LabItemType = new System.Windows.Forms.Label();
            this.LabOwner = new System.Windows.Forms.Label();
            this.ItemType = new System.Windows.Forms.ComboBox();
            this.OwnerPanel = new System.Windows.Forms.Panel();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.ExtraTypePanel = new System.Windows.Forms.Panel();
            this.ExtraType = new Reflexil.Editors.TypeReferenceEditor();
            this.ExtraTypeList = new System.Windows.Forms.ComboBox();
            this.LabExtraType = new System.Windows.Forms.Label();
            this.ExtraTypePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabOwnerType
            // 
            this.LabOwnerType.AutoSize = true;
            this.LabOwnerType.Location = new System.Drawing.Point(12, 9);
            this.LabOwnerType.Name = "LabOwnerType";
            this.LabOwnerType.Size = new System.Drawing.Size(61, 13);
            this.LabOwnerType.TabIndex = 1;
            this.LabOwnerType.Text = "Owner type";
            // 
            // OwnerType
            // 
            this.OwnerType.DisplayMember = "ShortLabel";
            this.OwnerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OwnerType.FormattingEnabled = true;
            this.OwnerType.Location = new System.Drawing.Point(102, 6);
            this.OwnerType.Name = "OwnerType";
            this.OwnerType.Size = new System.Drawing.Size(310, 21);
            this.OwnerType.TabIndex = 0;
            this.OwnerType.SelectedIndexChanged += new System.EventHandler(this.OwnerType_SelectedIndexChanged);
            // 
            // ItemName
            // 
            this.ItemName.Location = new System.Drawing.Point(102, 89);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(310, 20);
            this.ItemName.TabIndex = 3;
            // 
            // LabItemName
            // 
            this.LabItemName.AutoSize = true;
            this.LabItemName.Location = new System.Drawing.Point(12, 92);
            this.LabItemName.Name = "LabItemName";
            this.LabItemName.Size = new System.Drawing.Size(56, 13);
            this.LabItemName.TabIndex = 5;
            this.LabItemName.Text = "Item name";
            // 
            // LabItemType
            // 
            this.LabItemType.AutoSize = true;
            this.LabItemType.Location = new System.Drawing.Point(12, 65);
            this.LabItemType.Name = "LabItemType";
            this.LabItemType.Size = new System.Drawing.Size(50, 13);
            this.LabItemType.TabIndex = 6;
            this.LabItemType.Text = "Item type";
            // 
            // LabOwner
            // 
            this.LabOwner.AutoSize = true;
            this.LabOwner.Location = new System.Drawing.Point(12, 38);
            this.LabOwner.Name = "LabOwner";
            this.LabOwner.Size = new System.Drawing.Size(38, 13);
            this.LabOwner.TabIndex = 7;
            this.LabOwner.Text = "Owner";
            // 
            // ItemType
            // 
            this.ItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ItemType.FormattingEnabled = true;
            this.ItemType.Location = new System.Drawing.Point(102, 62);
            this.ItemType.Name = "ItemType";
            this.ItemType.Size = new System.Drawing.Size(310, 21);
            this.ItemType.TabIndex = 2;
            this.ItemType.SelectedIndexChanged += new System.EventHandler(this.InjectContextChanged);
            // 
            // OwnerPanel
            // 
            this.OwnerPanel.BackColor = System.Drawing.SystemColors.Info;
            this.OwnerPanel.Location = new System.Drawing.Point(102, 35);
            this.OwnerPanel.Name = "OwnerPanel";
            this.OwnerPanel.Size = new System.Drawing.Size(310, 21);
            this.OwnerPanel.TabIndex = 1;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(337, 142);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(256, 142);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 5;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // ExtraTypePanel
            // 
            this.ExtraTypePanel.BackColor = System.Drawing.SystemColors.Info;
            this.ExtraTypePanel.Controls.Add(this.ExtraType);
            this.ExtraTypePanel.Controls.Add(this.ExtraTypeList);
            this.ExtraTypePanel.Location = new System.Drawing.Point(102, 115);
            this.ExtraTypePanel.Name = "ExtraTypePanel";
            this.ExtraTypePanel.Size = new System.Drawing.Size(310, 21);
            this.ExtraTypePanel.TabIndex = 4;
            // 
            // ExtraType
            // 
            this.ExtraType.AssemblyRestriction = null;
            this.ExtraType.BackColor = System.Drawing.SystemColors.Window;
            this.ExtraType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExtraType.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.ExtraType.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.ExtraType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExtraType.Location = new System.Drawing.Point(0, 0);
            this.ExtraType.Name = "ExtraType";
            this.ExtraType.SelectedOperand = null;
            this.ExtraType.Size = new System.Drawing.Size(310, 21);
            this.ExtraType.TabIndex = 4;
            this.ExtraType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExtraType.UseVisualStyleBackColor = true;
            // 
            // ExtraTypeList
            // 
            this.ExtraTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExtraTypeList.FormattingEnabled = true;
            this.ExtraTypeList.Location = new System.Drawing.Point(0, 0);
            this.ExtraTypeList.Name = "ExtraTypeList";
            this.ExtraTypeList.Size = new System.Drawing.Size(310, 21);
            this.ExtraTypeList.TabIndex = 10;
            this.ExtraTypeList.Visible = false;
            // 
            // LabExtraType
            // 
            this.LabExtraType.AutoSize = true;
            this.LabExtraType.Location = new System.Drawing.Point(12, 119);
            this.LabExtraType.Name = "LabExtraType";
            this.LabExtraType.Size = new System.Drawing.Size(54, 13);
            this.LabExtraType.TabIndex = 9;
            this.LabExtraType.Text = "Extra type";
            // 
            // InjectForm
            // 
            this.AcceptButton = this.Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(423, 173);
            this.Controls.Add(this.LabExtraType);
            this.Controls.Add(this.ExtraTypePanel);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OwnerPanel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.ItemType);
            this.Controls.Add(this.LabOwner);
            this.Controls.Add(this.LabItemType);
            this.Controls.Add(this.LabItemName);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.OwnerType);
            this.Controls.Add(this.LabOwnerType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InjectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inject";
            this.Load += new System.EventHandler(this.InjectForm_Load);
            this.ExtraTypePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label LabOwnerType;
        private System.Windows.Forms.ComboBox OwnerType;
        private System.Windows.Forms.TextBox ItemName;
        private System.Windows.Forms.Label LabItemName;
        private System.Windows.Forms.Label LabItemType;
        private System.Windows.Forms.Label LabOwner;
        private System.Windows.Forms.ComboBox ItemType;
        internal System.Windows.Forms.Panel OwnerPanel;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        internal System.Windows.Forms.Panel ExtraTypePanel;
        private System.Windows.Forms.Label LabExtraType;
        private Reflexil.Editors.TypeReferenceEditor ExtraType;
        private System.Windows.Forms.ComboBox ExtraTypeList;
	}
}