namespace Reflexil.Editors
{
	partial class ConstantEditor
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
            this.ConstantPanel = new System.Windows.Forms.Panel();
            this.LabConstant = new System.Windows.Forms.Label();
            this.LabConstantType = new System.Windows.Forms.Label();
            this.ConstantTypes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ConstantPanel
            // 
            this.ConstantPanel.BackColor = System.Drawing.SystemColors.Info;
            this.ConstantPanel.Location = new System.Drawing.Point(73, 29);
            this.ConstantPanel.Name = "ConstantPanel";
            this.ConstantPanel.Size = new System.Drawing.Size(310, 21);
            this.ConstantPanel.TabIndex = 20;
            // 
            // LabConstant
            // 
            this.LabConstant.AutoSize = true;
            this.LabConstant.Location = new System.Drawing.Point(-2, 32);
            this.LabConstant.Name = "LabConstant";
            this.LabConstant.Size = new System.Drawing.Size(49, 13);
            this.LabConstant.TabIndex = 19;
            this.LabConstant.Text = "Constant";
            // 
            // LabConstantType
            // 
            this.LabConstantType.AutoSize = true;
            this.LabConstantType.Location = new System.Drawing.Point(-2, 3);
            this.LabConstantType.Name = "LabConstantType";
            this.LabConstantType.Size = new System.Drawing.Size(60, 13);
            this.LabConstantType.TabIndex = 18;
            this.LabConstantType.Text = "Const. type";
            // 
            // ConstantTypes
            // 
            this.ConstantTypes.DisplayMember = "Label";
            this.ConstantTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConstantTypes.FormattingEnabled = true;
            this.ConstantTypes.Location = new System.Drawing.Point(73, 0);
            this.ConstantTypes.Name = "ConstantTypes";
            this.ConstantTypes.Size = new System.Drawing.Size(310, 21);
            this.ConstantTypes.TabIndex = 17;
            this.ConstantTypes.ValueMember = "Label";
            this.ConstantTypes.SelectedIndexChanged += new System.EventHandler(this.ConstantTypes_SelectedIndexChanged);
            // 
            // ConstantEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ConstantPanel);
            this.Controls.Add(this.LabConstant);
            this.Controls.Add(this.LabConstantType);
            this.Controls.Add(this.ConstantTypes);
            this.Name = "ConstantEditor";
            this.Size = new System.Drawing.Size(383, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        internal System.Windows.Forms.Panel ConstantPanel;
        internal System.Windows.Forms.Label LabConstant;
        internal System.Windows.Forms.Label LabConstantType;
        internal System.Windows.Forms.ComboBox ConstantTypes;

    }
}
