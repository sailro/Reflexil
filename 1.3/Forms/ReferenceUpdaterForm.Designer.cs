namespace Reflexil.Forms
{
    partial class ReferenceUpdaterForm
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
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.Cancel = new System.Windows.Forms.Button();
            this.AssemblyLab = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Directory = new System.Windows.Forms.Label();
            this.Assembly = new System.Windows.Forms.Label();
            this.WaitLab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerReportsProgress = true;
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(379, 91);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // AssemblyLab
            // 
            this.AssemblyLab.AutoSize = true;
            this.AssemblyLab.Location = new System.Drawing.Point(13, 13);
            this.AssemblyLab.Name = "AssemblyLab";
            this.AssemblyLab.Size = new System.Drawing.Size(26, 13);
            this.AssemblyLab.TabIndex = 2;
            this.AssemblyLab.Text = "File:";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(16, 59);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(438, 23);
            this.ProgressBar.TabIndex = 3;
            // 
            // Directory
            // 
            this.Directory.Location = new System.Drawing.Point(71, 13);
            this.Directory.Name = "Directory";
            this.Directory.Size = new System.Drawing.Size(383, 23);
            this.Directory.TabIndex = 4;
            // 
            // Assembly
            // 
            this.Assembly.Location = new System.Drawing.Point(71, 13);
            this.Assembly.Name = "Assembly";
            this.Assembly.Size = new System.Drawing.Size(383, 43);
            this.Assembly.TabIndex = 5;
            // 
            // WaitLab
            // 
            this.WaitLab.AutoSize = true;
            this.WaitLab.Location = new System.Drawing.Point(16, 97);
            this.WaitLab.Name = "WaitLab";
            this.WaitLab.Size = new System.Drawing.Size(251, 13);
            this.WaitLab.TabIndex = 6;
            this.WaitLab.Text = "Please wait while updating referencing assemblies...";
            // 
            // ReferenceUpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(466, 123);
            this.ControlBox = false;
            this.Controls.Add(this.WaitLab);
            this.Controls.Add(this.Assembly);
            this.Controls.Add(this.Directory);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.AssemblyLab);
            this.Controls.Add(this.Cancel);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReferenceUpdaterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reference updater";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label AssemblyLab;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label Directory;
        private System.Windows.Forms.Label Assembly;
        private System.Windows.Forms.Label WaitLab;
	}
}