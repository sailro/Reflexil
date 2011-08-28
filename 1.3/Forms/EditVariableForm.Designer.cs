namespace Reflexil.Forms
{
    partial class EditVariableForm
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
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ButUpdate
            // 
            this.ButUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButUpdate.Location = new System.Drawing.Point(407, 11);
            this.ButUpdate.Name = "ButUpdate";
            this.ButUpdate.Size = new System.Drawing.Size(124, 23);
            this.ButUpdate.TabIndex = 2;
            this.ButUpdate.Text = "Update";
            this.ButUpdate.UseVisualStyleBackColor = true;
            this.ButUpdate.Click += new System.EventHandler(this.ButUpdate_Click);
            // 
            // EditVariableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 127);
            this.Controls.Add(this.ButUpdate);
            this.Name = "EditVariableForm";
            this.Text = "Edit existing variable";
            this.Load += new System.EventHandler(this.EditVariableForm_Load);
            this.Controls.SetChildIndex(this.TypeSpecificationEditor, 0);
            this.Controls.SetChildIndex(this.ButUpdate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button ButUpdate;
    }
}
