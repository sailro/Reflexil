namespace Reflexil.Editors
{
    partial class TypeAttributesControl
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
            this.LabBaseType = new System.Windows.Forms.Label();
            this.BaseTypePanel = new System.Windows.Forms.Panel();
            this.BaseType = new Reflexil.Editors.TypeReferenceEditor();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.BaseTypePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.LabBaseType);
            this.SplitContainer.Panel2.Controls.Add(this.BaseTypePanel);
            // 
            // LabBaseType
            // 
            this.LabBaseType.AutoSize = true;
            this.LabBaseType.Location = new System.Drawing.Point(7, 16);
            this.LabBaseType.Name = "LabBaseType";
            this.LabBaseType.Size = new System.Drawing.Size(54, 13);
            this.LabBaseType.TabIndex = 13;
            this.LabBaseType.Text = "Base type";
            // 
            // BaseTypePanel
            // 
            this.BaseTypePanel.BackColor = System.Drawing.SystemColors.Info;
            this.BaseTypePanel.Controls.Add(this.BaseType);
            this.BaseTypePanel.Location = new System.Drawing.Point(67, 12);
            this.BaseTypePanel.Name = "BaseTypePanel";
            this.BaseTypePanel.Size = new System.Drawing.Size(223, 21);
            this.BaseTypePanel.TabIndex = 12;
            // 
            // BaseType
            // 
            this.BaseType.AssemblyRestriction = null;
            this.BaseType.BackColor = System.Drawing.SystemColors.Window;
            this.BaseType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseType.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.BaseType.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.BaseType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BaseType.Location = new System.Drawing.Point(0, 0);
            this.BaseType.Name = "BaseType";
            this.BaseType.SelectedOperand = null;
            this.BaseType.Size = new System.Drawing.Size(223, 21);
            this.BaseType.TabIndex = 4;
            this.BaseType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BaseType.UseVisualStyleBackColor = true;
            this.BaseType.Validated += new System.EventHandler(this.BaseType_Validated);
            // 
            // TypeAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "TypeAttributesControl";
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            this.SplitContainer.ResumeLayout(false);
            this.BaseTypePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabBaseType;
        internal System.Windows.Forms.Panel BaseTypePanel;
        private TypeReferenceEditor BaseType;
    }
}
