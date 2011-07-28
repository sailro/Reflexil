namespace Reflexil.Editors
{
    partial class LinkedResourceAttributesControl
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
            this.components = new System.ComponentModel.Container();
            this.Hash = new System.Windows.Forms.TextBox();
            this.LabHash = new System.Windows.Forms.Label();
            this.LabFilename = new System.Windows.Forms.Label();
            this.Filename = new System.Windows.Forms.TextBox();
            this.ButFromFile = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.ButFromFile);
            this.SplitContainer.Panel2.Controls.Add(this.Filename);
            this.SplitContainer.Panel2.Controls.Add(this.LabFilename);
            this.SplitContainer.Panel2.Controls.Add(this.Hash);
            this.SplitContainer.Panel2.Controls.Add(this.LabHash);
            // 
            // Hash
            // 
            this.Hash.Location = new System.Drawing.Point(58, 35);
            this.Hash.Name = "Hash";
            this.Hash.Size = new System.Drawing.Size(258, 20);
            this.Hash.TabIndex = 13;
            this.Hash.Validating += new System.ComponentModel.CancelEventHandler(this.StringToByte_Validating);
            this.Hash.Validated += new System.EventHandler(this.Hash_Validated);
            // 
            // LabHash
            // 
            this.LabHash.AutoSize = true;
            this.LabHash.Location = new System.Drawing.Point(3, 38);
            this.LabHash.Name = "LabHash";
            this.LabHash.Size = new System.Drawing.Size(32, 13);
            this.LabHash.TabIndex = 10;
            this.LabHash.Text = "Hash";
            // 
            // LabFilename
            // 
            this.LabFilename.AutoSize = true;
            this.LabFilename.Location = new System.Drawing.Point(3, 12);
            this.LabFilename.Name = "LabFilename";
            this.LabFilename.Size = new System.Drawing.Size(49, 13);
            this.LabFilename.TabIndex = 12;
            this.LabFilename.Text = "Filename";
            // 
            // Filename
            // 
            this.Filename.Location = new System.Drawing.Point(58, 9);
            this.Filename.Name = "Filename";
            this.Filename.Size = new System.Drawing.Size(258, 20);
            this.Filename.TabIndex = 12;
            this.Filename.Validating += new System.ComponentModel.CancelEventHandler(this.Filename_Validating);
            this.Filename.Validated += new System.EventHandler(this.Filename_Validated);
            // 
            // ButFromFile
            // 
            this.ButFromFile.Location = new System.Drawing.Point(58, 61);
            this.ButFromFile.Name = "ButFromFile";
            this.ButFromFile.Size = new System.Drawing.Size(258, 23);
            this.ButFromFile.TabIndex = 14;
            this.ButFromFile.Text = "Get hash and filename from ...";
            this.ButFromFile.UseVisualStyleBackColor = true;
            this.ButFromFile.Click += new System.EventHandler(this.ButFromFile_Click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "All files|*.*";
            this.OpenFileDialog.Title = "Choose file...";
            // 
            // LinkedResourceAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "LinkedResourceAttributesControl";
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            this.SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButFromFile;
        protected System.Windows.Forms.TextBox Filename;
        protected System.Windows.Forms.Label LabFilename;
        protected System.Windows.Forms.TextBox Hash;
        protected System.Windows.Forms.Label LabHash;
        protected System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;

    }
}
