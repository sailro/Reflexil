namespace Reflexil.Forms
{
    partial class EditOverrideForm
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
            this.ButUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButUpdate
            // 
            this.ButUpdate.Location = new System.Drawing.Point(407, 11);
            this.ButUpdate.Name = "ButUpdate";
            this.ButUpdate.Size = new System.Drawing.Size(124, 23);
            this.ButUpdate.TabIndex = 18;
            this.ButUpdate.Text = "Update";
            this.ButUpdate.UseVisualStyleBackColor = true;
            this.ButUpdate.Click += new System.EventHandler(this.ButUpdate_Click);
            // 
            // EditOverrideForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 95);
            this.Controls.Add(this.ButUpdate);
            this.Name = "EditOverrideForm";
            this.Text = "Edit existing override";
            this.Load += new System.EventHandler(this.EditOverrideForm_Load);
            this.Controls.SetChildIndex(this.MethodReferenceEditor, 0);
            this.Controls.SetChildIndex(this.ButUpdate, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button ButUpdate;
    }
}
