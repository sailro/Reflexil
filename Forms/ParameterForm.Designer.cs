namespace Reflexil.Forms
{
    partial class ParameterForm
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
            this.ConstantPanel = new System.Windows.Forms.Panel();
            this.LabConstant = new System.Windows.Forms.Label();
            this.LabConstantType = new System.Windows.Forms.Label();
            this.ConstantTypes = new System.Windows.Forms.ComboBox();
            this.Attributes = new Reflexil.Editors.AttributesControl();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ConstantPanel
            // 
            this.ConstantPanel.BackColor = System.Drawing.SystemColors.Info;
            this.ConstantPanel.Location = new System.Drawing.Point(82, 150);
            this.ConstantPanel.Name = "ConstantPanel";
            this.ConstantPanel.Size = new System.Drawing.Size(310, 21);
            this.ConstantPanel.TabIndex = 15;
            // 
            // LabConstant
            // 
            this.LabConstant.AutoSize = true;
            this.LabConstant.Location = new System.Drawing.Point(7, 150);
            this.LabConstant.Name = "LabConstant";
            this.LabConstant.Size = new System.Drawing.Size(49, 13);
            this.LabConstant.TabIndex = 14;
            this.LabConstant.Text = "Constant";
            // 
            // LabConstantType
            // 
            this.LabConstantType.AutoSize = true;
            this.LabConstantType.Location = new System.Drawing.Point(7, 124);
            this.LabConstantType.Name = "LabConstantType";
            this.LabConstantType.Size = new System.Drawing.Size(60, 13);
            this.LabConstantType.TabIndex = 13;
            this.LabConstantType.Text = "Const. type";
            // 
            // ConstantTypes
            // 
            this.ConstantTypes.DisplayMember = "Label";
            this.ConstantTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConstantTypes.FormattingEnabled = true;
            this.ConstantTypes.Location = new System.Drawing.Point(82, 121);
            this.ConstantTypes.Name = "ConstantTypes";
            this.ConstantTypes.Size = new System.Drawing.Size(310, 21);
            this.ConstantTypes.TabIndex = 12;
            this.ConstantTypes.ValueMember = "Label";
            this.ConstantTypes.SelectedIndexChanged += new System.EventHandler(this.ConstantTypes_SelectedIndexChanged);
            // 
            // Attributes
            // 
            this.Attributes.Location = new System.Drawing.Point(10, 177);
            this.Attributes.Name = "Attributes";
            this.Attributes.Size = new System.Drawing.Size(382, 66);
            this.Attributes.TabIndex = 16;
            // 
            // ParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 248);
            this.Controls.Add(this.Attributes);
            this.Controls.Add(this.ConstantPanel);
            this.Controls.Add(this.LabConstant);
            this.Controls.Add(this.LabConstantType);
            this.Controls.Add(this.ConstantTypes);
            this.Name = "ParameterForm";
            this.Controls.SetChildIndex(this.TypeSpecificationEditor, 0);
            this.Controls.SetChildIndex(this.ConstantTypes, 0);
            this.Controls.SetChildIndex(this.LabConstantType, 0);
            this.Controls.SetChildIndex(this.LabConstant, 0);
            this.Controls.SetChildIndex(this.ConstantPanel, 0);
            this.Controls.SetChildIndex(this.Attributes, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel ConstantPanel;
        internal System.Windows.Forms.Label LabConstant;
        internal System.Windows.Forms.Label LabConstantType;
        internal System.Windows.Forms.ComboBox ConstantTypes;
        protected Reflexil.Editors.AttributesControl Attributes;
    }
}
