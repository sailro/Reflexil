namespace Reflexil.Editors
{
	partial class TypeSpecificationEditor
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
            this.TypeSpecification = new System.Windows.Forms.ComboBox();
            this.TypPanel = new System.Windows.Forms.Panel();
            this.LabOperand = new System.Windows.Forms.Label();
            this.LabTypeScope = new System.Windows.Forms.Label();
            this.TypeScope = new System.Windows.Forms.ComboBox();
            this.LabSpecification = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TypeSpecification
            // 
            this.TypeSpecification.CausesValidation = false;
            this.TypeSpecification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeSpecification.FormattingEnabled = true;
            this.TypeSpecification.Location = new System.Drawing.Point(73, 56);
            this.TypeSpecification.Name = "TypeSpecification";
            this.TypeSpecification.Size = new System.Drawing.Size(310, 21);
            this.TypeSpecification.TabIndex = 2;
            // 
            // TypPanel
            // 
            this.TypPanel.BackColor = System.Drawing.SystemColors.Info;
            this.TypPanel.Location = new System.Drawing.Point(73, 28);
            this.TypPanel.Name = "TypPanel";
            this.TypPanel.Size = new System.Drawing.Size(310, 21);
            this.TypPanel.TabIndex = 1;
            // 
            // LabOperand
            // 
            this.LabOperand.AutoSize = true;
            this.LabOperand.Location = new System.Drawing.Point(-2, 32);
            this.LabOperand.Name = "LabOperand";
            this.LabOperand.Size = new System.Drawing.Size(31, 13);
            this.LabOperand.TabIndex = 14;
            this.LabOperand.Text = "Type";
            // 
            // LabTypeScope
            // 
            this.LabTypeScope.AutoSize = true;
            this.LabTypeScope.Location = new System.Drawing.Point(-2, 3);
            this.LabTypeScope.Name = "LabTypeScope";
            this.LabTypeScope.Size = new System.Drawing.Size(65, 13);
            this.LabTypeScope.TabIndex = 13;
            this.LabTypeScope.Text = "Type Scope";
            // 
            // TypeScope
            // 
            this.TypeScope.CausesValidation = false;
            this.TypeScope.DisplayMember = "Label";
            this.TypeScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeScope.FormattingEnabled = true;
            this.TypeScope.Location = new System.Drawing.Point(73, 0);
            this.TypeScope.Name = "TypeScope";
            this.TypeScope.Size = new System.Drawing.Size(310, 21);
            this.TypeScope.TabIndex = 0;
            this.TypeScope.ValueMember = "Label";
            this.TypeScope.SelectedIndexChanged += new System.EventHandler(this.Operands_SelectedIndexChanged);
            // 
            // LabSpecification
            // 
            this.LabSpecification.AutoSize = true;
            this.LabSpecification.Location = new System.Drawing.Point(-2, 59);
            this.LabSpecification.Name = "LabSpecification";
            this.LabSpecification.Size = new System.Drawing.Size(68, 13);
            this.LabSpecification.TabIndex = 16;
            this.LabSpecification.Text = "Specification";
            // 
            // TypeSpecificationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabSpecification);
            this.Controls.Add(this.TypPanel);
            this.Controls.Add(this.LabOperand);
            this.Controls.Add(this.LabTypeScope);
            this.Controls.Add(this.TypeScope);
            this.Controls.Add(this.TypeSpecification);
            this.Name = "TypeSpecificationEditor";
            this.Size = new System.Drawing.Size(383, 77);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ComboBox TypeSpecification;
        internal System.Windows.Forms.Panel TypPanel;
        internal System.Windows.Forms.Label LabOperand;
        internal System.Windows.Forms.Label LabTypeScope;
        internal System.Windows.Forms.ComboBox TypeScope;
        internal System.Windows.Forms.Label LabSpecification;
	}
}
