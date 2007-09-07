namespace Reflexil.Forms
{
	partial class ConfigureForm
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
            this.LabInputBase = new System.Windows.Forms.Label();
            this.LabRowBase = new System.Windows.Forms.Label();
            this.LabOperandBase = new System.Windows.Forms.Label();
            this.InputBase = new System.Windows.Forms.ComboBox();
            this.RowBase = new System.Windows.Forms.ComboBox();
            this.OperandBase = new System.Windows.Forms.ComboBox();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabInputBase
            // 
            this.LabInputBase.AutoSize = true;
            this.LabInputBase.Location = new System.Drawing.Point(12, 15);
            this.LabInputBase.Name = "LabInputBase";
            this.LabInputBase.Size = new System.Drawing.Size(99, 13);
            this.LabInputBase.TabIndex = 0;
            this.LabInputBase.Text = "Prefered input base";
            // 
            // LabRowBase
            // 
            this.LabRowBase.AutoSize = true;
            this.LabRowBase.Location = new System.Drawing.Point(12, 42);
            this.LabRowBase.Name = "LabRowBase";
            this.LabRowBase.Size = new System.Drawing.Size(118, 13);
            this.LabRowBase.TabIndex = 1;
            this.LabRowBase.Text = "Row index display base";
            // 
            // LabOperandBase
            // 
            this.LabOperandBase.AutoSize = true;
            this.LabOperandBase.Location = new System.Drawing.Point(12, 69);
            this.LabOperandBase.Name = "LabOperandBase";
            this.LabOperandBase.Size = new System.Drawing.Size(109, 13);
            this.LabOperandBase.TabIndex = 2;
            this.LabOperandBase.Text = "Operand display base";
            // 
            // InputBase
            // 
            this.InputBase.FormattingEnabled = true;
            this.InputBase.Location = new System.Drawing.Point(130, 12);
            this.InputBase.Name = "InputBase";
            this.InputBase.Size = new System.Drawing.Size(121, 21);
            this.InputBase.TabIndex = 3;
            // 
            // RowBase
            // 
            this.RowBase.FormattingEnabled = true;
            this.RowBase.Location = new System.Drawing.Point(130, 39);
            this.RowBase.Name = "RowBase";
            this.RowBase.Size = new System.Drawing.Size(121, 21);
            this.RowBase.TabIndex = 4;
            // 
            // OperandBase
            // 
            this.OperandBase.FormattingEnabled = true;
            this.OperandBase.Location = new System.Drawing.Point(130, 66);
            this.OperandBase.Name = "OperandBase";
            this.OperandBase.Size = new System.Drawing.Size(121, 21);
            this.OperandBase.TabIndex = 5;
            // 
            // Ok
            // 
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(95, 100);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(176, 100);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 135);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.OperandBase);
            this.Controls.Add(this.RowBase);
            this.Controls.Add(this.InputBase);
            this.Controls.Add(this.LabOperandBase);
            this.Controls.Add(this.LabRowBase);
            this.Controls.Add(this.LabInputBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ConfigureForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label LabInputBase;
        private System.Windows.Forms.Label LabRowBase;
        private System.Windows.Forms.Label LabOperandBase;
        private System.Windows.Forms.ComboBox InputBase;
        private System.Windows.Forms.ComboBox RowBase;
        private System.Windows.Forms.ComboBox OperandBase;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
	}
}