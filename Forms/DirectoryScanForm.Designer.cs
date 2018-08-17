namespace Reflexil.Forms
{
	partial class DirectoryScanForm
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
			this.DirectoryLab = new System.Windows.Forms.Label();
			this.FileLab = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.Directory = new System.Windows.Forms.Label();
			this.File = new System.Windows.Forms.Label();
			this.WaitLab = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BackgroundWorker
			// 
			this.BackgroundWorker.WorkerReportsProgress = true;
			this.BackgroundWorker.WorkerSupportsCancellation = true;
			this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
			this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
			this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(505, 140);
			this.Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(100, 28);
			this.Cancel.TabIndex = 0;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// DirectoryLab
			// 
			this.DirectoryLab.AutoSize = true;
			this.DirectoryLab.Location = new System.Drawing.Point(17, 16);
			this.DirectoryLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DirectoryLab.Name = "DirectoryLab";
			this.DirectoryLab.Size = new System.Drawing.Size(69, 17);
			this.DirectoryLab.TabIndex = 1;
			this.DirectoryLab.Text = "Directory:";
			// 
			// FileLab
			// 
			this.FileLab.AutoSize = true;
			this.FileLab.Location = new System.Drawing.Point(17, 44);
			this.FileLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FileLab.Name = "FileLab";
			this.FileLab.Size = new System.Drawing.Size(34, 17);
			this.FileLab.TabIndex = 2;
			this.FileLab.Text = "File:";
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(21, 101);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(584, 28);
			this.ProgressBar.TabIndex = 3;
			// 
			// Directory
			// 
			this.Directory.Location = new System.Drawing.Point(95, 16);
			this.Directory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Directory.Name = "Directory";
			this.Directory.Size = new System.Drawing.Size(511, 28);
			this.Directory.TabIndex = 4;
			// 
			// File
			// 
			this.File.Location = new System.Drawing.Point(95, 44);
			this.File.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.File.Name = "File";
			this.File.Size = new System.Drawing.Size(511, 53);
			this.File.TabIndex = 5;
			// 
			// WaitLab
			// 
			this.WaitLab.AutoSize = true;
			this.WaitLab.Location = new System.Drawing.Point(21, 148);
			this.WaitLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.WaitLab.Name = "WaitLab";
			this.WaitLab.Size = new System.Drawing.Size(417, 17);
			this.WaitLab.TabIndex = 6;
			this.WaitLab.Text = "Please wait while scanning directory for referencing assemblies...";
			// 
			// DirectoryScanForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(621, 186);
			this.ControlBox = false;
			this.Controls.Add(this.WaitLab);
			this.Controls.Add(this.File);
			this.Controls.Add(this.Directory);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.FileLab);
			this.Controls.Add(this.DirectoryLab);
			this.Controls.Add(this.Cancel);
			this.Cursor = System.Windows.Forms.Cursors.AppStarting;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DirectoryScanForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Directory Scanner";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label DirectoryLab;
        private System.Windows.Forms.Label FileLab;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label Directory;
        private System.Windows.Forms.Label File;
        private System.Windows.Forms.Label WaitLab;
	}
}