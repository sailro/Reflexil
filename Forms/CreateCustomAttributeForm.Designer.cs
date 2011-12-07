namespace Reflexil.Forms
{
    partial class CreateCustomAttributeForm
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
            this.ButInsertAfter = new System.Windows.Forms.Button();
            this.ButInsertBefore = new System.Windows.Forms.Button();
            this.ButAppend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConstructorArguments
            // 
            this.ConstructorArguments.Size = new System.Drawing.Size(381, 189);
            // 
            // ButInsertAfter
            // 
            this.ButInsertAfter.Location = new System.Drawing.Point(407, 64);
            this.ButInsertAfter.Name = "ButInsertAfter";
            this.ButInsertAfter.Size = new System.Drawing.Size(124, 23);
            this.ButInsertAfter.TabIndex = 22;
            this.ButInsertAfter.Text = "Insert after selection";
            this.ButInsertAfter.UseVisualStyleBackColor = true;
            this.ButInsertAfter.Click += new System.EventHandler(this.ButInsertAfter_Click);
            // 
            // ButInsertBefore
            // 
            this.ButInsertBefore.Location = new System.Drawing.Point(407, 37);
            this.ButInsertBefore.Name = "ButInsertBefore";
            this.ButInsertBefore.Size = new System.Drawing.Size(124, 23);
            this.ButInsertBefore.TabIndex = 21;
            this.ButInsertBefore.Text = "Insert before selection";
            this.ButInsertBefore.UseVisualStyleBackColor = true;
            this.ButInsertBefore.Click += new System.EventHandler(this.ButInsertBefore_Click);
            // 
            // ButAppend
            // 
            this.ButAppend.Location = new System.Drawing.Point(407, 11);
            this.ButAppend.Name = "ButAppend";
            this.ButAppend.Size = new System.Drawing.Size(124, 23);
            this.ButAppend.TabIndex = 20;
            this.ButAppend.Text = "Append";
            this.ButAppend.UseVisualStyleBackColor = true;
            this.ButAppend.Click += new System.EventHandler(this.ButAppend_Click);
            // 
            // CreateCustomAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 221);
            this.Controls.Add(this.ButInsertAfter);
            this.Controls.Add(this.ButInsertBefore);
            this.Controls.Add(this.ButAppend);
            this.Name = "CreateCustomAttributeForm";
            this.Text = "Create new custom attribute";
            this.Load += new System.EventHandler(this.CreateOverrideForm_Load);
            this.Controls.SetChildIndex(this.ButAppend, 0);
            this.Controls.SetChildIndex(this.ButInsertBefore, 0);
            this.Controls.SetChildIndex(this.ButInsertAfter, 0);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button ButInsertAfter;
        internal System.Windows.Forms.Button ButInsertBefore;
        internal System.Windows.Forms.Button ButAppend;
    }
}
