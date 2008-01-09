namespace Reflexil.Forms
{
	partial class StrongNameRemoverForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrongNameRemoverForm));
            this.SNAssemblyLab = new System.Windows.Forms.Label();
            this.SNAssembly = new System.Windows.Forms.TextBox();
            this.SelectSNAssembly = new System.Windows.Forms.Button();
            this.ReferencingBox = new System.Windows.Forms.GroupBox();
            this.ReferencingAssemblies = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.AutoScan = new System.Windows.Forms.Button();
            this.Process = new System.Windows.Forms.Button();
            this.Note = new System.Windows.Forms.Label();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ReferencingBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SNAssemblyLab
            // 
            this.SNAssemblyLab.AutoSize = true;
            this.SNAssemblyLab.Location = new System.Drawing.Point(12, 18);
            this.SNAssemblyLab.Name = "SNAssemblyLab";
            this.SNAssemblyLab.Size = new System.Drawing.Size(87, 13);
            this.SNAssemblyLab.TabIndex = 0;
            this.SNAssemblyLab.Text = "Signed assemby:";
            // 
            // SNAssembly
            // 
            this.SNAssembly.Location = new System.Drawing.Point(107, 15);
            this.SNAssembly.Name = "SNAssembly";
            this.SNAssembly.Size = new System.Drawing.Size(192, 20);
            this.SNAssembly.TabIndex = 1;
            // 
            // SelectSNAssembly
            // 
            this.SelectSNAssembly.Location = new System.Drawing.Point(305, 13);
            this.SelectSNAssembly.Name = "SelectSNAssembly";
            this.SelectSNAssembly.Size = new System.Drawing.Size(75, 23);
            this.SelectSNAssembly.TabIndex = 2;
            this.SelectSNAssembly.Text = "Select ...";
            this.SelectSNAssembly.UseVisualStyleBackColor = true;
            // 
            // ReferencingBox
            // 
            this.ReferencingBox.Controls.Add(this.panel1);
            this.ReferencingBox.Controls.Add(this.ReferencingAssemblies);
            this.ReferencingBox.Location = new System.Drawing.Point(12, 39);
            this.ReferencingBox.Name = "ReferencingBox";
            this.ReferencingBox.Size = new System.Drawing.Size(368, 238);
            this.ReferencingBox.TabIndex = 3;
            this.ReferencingBox.TabStop = false;
            this.ReferencingBox.Text = "Referencing assemblies to update";
            // 
            // ReferencingAssemblies
            // 
            this.ReferencingAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReferencingAssemblies.FormattingEnabled = true;
            this.ReferencingAssemblies.Location = new System.Drawing.Point(3, 16);
            this.ReferencingAssemblies.Name = "ReferencingAssemblies";
            this.ReferencingAssemblies.Size = new System.Drawing.Size(362, 212);
            this.ReferencingAssemblies.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AutoScan);
            this.panel1.Controls.Add(this.Remove);
            this.panel1.Controls.Add(this.Add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 202);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 33);
            this.panel1.TabIndex = 1;
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(4, 4);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(105, 23);
            this.Add.TabIndex = 0;
            this.Add.Text = "Add ...";
            this.Add.UseVisualStyleBackColor = true;
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(115, 4);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(105, 23);
            this.Remove.TabIndex = 1;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            // 
            // AutoScan
            // 
            this.AutoScan.Location = new System.Drawing.Point(227, 4);
            this.AutoScan.Name = "AutoScan";
            this.AutoScan.Size = new System.Drawing.Size(132, 23);
            this.AutoScan.TabIndex = 2;
            this.AutoScan.Text = "Auto scan directory ...";
            this.AutoScan.UseVisualStyleBackColor = true;
            // 
            // Process
            // 
            this.Process.Location = new System.Drawing.Point(19, 281);
            this.Process.Name = "Process";
            this.Process.Size = new System.Drawing.Size(355, 23);
            this.Process.TabIndex = 4;
            this.Process.Text = "Remove Strong Name and update referencing assemblies ";
            this.Process.UseVisualStyleBackColor = true;
            // 
            // Note
            // 
            this.Note.Location = new System.Drawing.Point(12, 314);
            this.Note.Name = "Note";
            this.Note.Size = new System.Drawing.Size(368, 55);
            this.Note.TabIndex = 5;
            this.Note.Text = resources.GetString("Note.Text");
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // StrongNameRemoverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 372);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.Process);
            this.Controls.Add(this.ReferencingBox);
            this.Controls.Add(this.SelectSNAssembly);
            this.Controls.Add(this.SNAssembly);
            this.Controls.Add(this.SNAssemblyLab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StrongNameRemoverForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Strong Name Remover";
            this.ReferencingBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label SNAssemblyLab;
        private System.Windows.Forms.TextBox SNAssembly;
        private System.Windows.Forms.Button SelectSNAssembly;
        private System.Windows.Forms.GroupBox ReferencingBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AutoScan;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.ListBox ReferencingAssemblies;
        private System.Windows.Forms.Button Process;
        private System.Windows.Forms.Label Note;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
	}
}