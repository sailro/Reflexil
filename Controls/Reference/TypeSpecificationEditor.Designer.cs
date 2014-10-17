namespace Reflexil.Editors
{
	sealed partial class TypeSpecificationEditor
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
			this.TypeSpecificationL1 = new System.Windows.Forms.ComboBox();
			this.TypPanel = new System.Windows.Forms.Panel();
			this.LabOperand = new System.Windows.Forms.Label();
			this.LabTypeScope = new System.Windows.Forms.Label();
			this.TypeScope = new System.Windows.Forms.ComboBox();
			this.LabSpecification = new System.Windows.Forms.Label();
			this.TypeSpecificationL2 = new System.Windows.Forms.ComboBox();
			this.TypeSpecificationL3 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// TypeSpecificationL1
			// 
			this.TypeSpecificationL1.CausesValidation = false;
			this.TypeSpecificationL1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeSpecificationL1.FormattingEnabled = true;
			this.TypeSpecificationL1.Location = new System.Drawing.Point(73, 56);
			this.TypeSpecificationL1.Name = "TypeSpecificationL1";
			this.TypeSpecificationL1.Size = new System.Drawing.Size(100, 21);
			this.TypeSpecificationL1.TabIndex = 2;
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
			// TypeSpecificationL2
			// 
			this.TypeSpecificationL2.CausesValidation = false;
			this.TypeSpecificationL2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeSpecificationL2.FormattingEnabled = true;
			this.TypeSpecificationL2.Location = new System.Drawing.Point(178, 56);
			this.TypeSpecificationL2.Name = "TypeSpecificationL2";
			this.TypeSpecificationL2.Size = new System.Drawing.Size(100, 21);
			this.TypeSpecificationL2.TabIndex = 17;
			// 
			// TypeSpecificationL3
			// 
			this.TypeSpecificationL3.CausesValidation = false;
			this.TypeSpecificationL3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeSpecificationL3.FormattingEnabled = true;
			this.TypeSpecificationL3.Location = new System.Drawing.Point(283, 56);
			this.TypeSpecificationL3.Name = "TypeSpecificationL3";
			this.TypeSpecificationL3.Size = new System.Drawing.Size(100, 21);
			this.TypeSpecificationL3.TabIndex = 18;
			// 
			// TypeSpecificationEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TypeSpecificationL3);
			this.Controls.Add(this.TypeSpecificationL2);
			this.Controls.Add(this.LabSpecification);
			this.Controls.Add(this.TypPanel);
			this.Controls.Add(this.LabOperand);
			this.Controls.Add(this.LabTypeScope);
			this.Controls.Add(this.TypeScope);
			this.Controls.Add(this.TypeSpecificationL1);
			this.Name = "TypeSpecificationEditor";
			this.Size = new System.Drawing.Size(383, 77);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ComboBox TypeSpecificationL1;
        internal System.Windows.Forms.Panel TypPanel;
        internal System.Windows.Forms.Label LabOperand;
        internal System.Windows.Forms.Label LabTypeScope;
        internal System.Windows.Forms.ComboBox TypeScope;
        internal System.Windows.Forms.Label LabSpecification;
		private System.Windows.Forms.ComboBox TypeSpecificationL2;
		private System.Windows.Forms.ComboBox TypeSpecificationL3;
	}
}
