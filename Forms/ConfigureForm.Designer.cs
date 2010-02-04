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
            this.LabLanguage = new System.Windows.Forms.Label();
            this.Language = new System.Windows.Forms.ComboBox();
            this.DisplayWarning = new System.Windows.Forms.CheckBox();
            this.ShowSymbols = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LabInputBase
            // 
            this.LabInputBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabInputBase.AutoSize = true;
            this.LabInputBase.Location = new System.Drawing.Point(12, 64);
            this.LabInputBase.Name = "LabInputBase";
            this.LabInputBase.Size = new System.Drawing.Size(99, 13);
            this.LabInputBase.TabIndex = 0;
            this.LabInputBase.Text = "Prefered input base";
            // 
            // LabRowBase
            // 
            this.LabRowBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabRowBase.AutoSize = true;
            this.LabRowBase.Location = new System.Drawing.Point(12, 91);
            this.LabRowBase.Name = "LabRowBase";
            this.LabRowBase.Size = new System.Drawing.Size(118, 13);
            this.LabRowBase.TabIndex = 1;
            this.LabRowBase.Text = "Row index display base";
            // 
            // LabOperandBase
            // 
            this.LabOperandBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabOperandBase.AutoSize = true;
            this.LabOperandBase.Location = new System.Drawing.Point(12, 118);
            this.LabOperandBase.Name = "LabOperandBase";
            this.LabOperandBase.Size = new System.Drawing.Size(109, 13);
            this.LabOperandBase.TabIndex = 2;
            this.LabOperandBase.Text = "Operand display base";
            // 
            // InputBase
            // 
            this.InputBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InputBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputBase.FormattingEnabled = true;
            this.InputBase.Location = new System.Drawing.Point(150, 61);
            this.InputBase.Name = "InputBase";
            this.InputBase.Size = new System.Drawing.Size(101, 21);
            this.InputBase.TabIndex = 2;
            // 
            // RowBase
            // 
            this.RowBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RowBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RowBase.FormattingEnabled = true;
            this.RowBase.Location = new System.Drawing.Point(150, 88);
            this.RowBase.Name = "RowBase";
            this.RowBase.Size = new System.Drawing.Size(101, 21);
            this.RowBase.TabIndex = 3;
            // 
            // OperandBase
            // 
            this.OperandBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OperandBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperandBase.FormattingEnabled = true;
            this.OperandBase.Location = new System.Drawing.Point(150, 115);
            this.OperandBase.Name = "OperandBase";
            this.OperandBase.Size = new System.Drawing.Size(101, 21);
            this.OperandBase.TabIndex = 4;
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(95, 181);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(176, 181);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // LabLanguage
            // 
            this.LabLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabLanguage.AutoSize = true;
            this.LabLanguage.Location = new System.Drawing.Point(12, 144);
            this.LabLanguage.Name = "LabLanguage";
            this.LabLanguage.Size = new System.Drawing.Size(55, 13);
            this.LabLanguage.TabIndex = 8;
            this.LabLanguage.Text = "Language";
            // 
            // Language
            // 
            this.Language.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Language.FormattingEnabled = true;
            this.Language.Location = new System.Drawing.Point(150, 141);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(101, 21);
            this.Language.TabIndex = 5;
            // 
            // DisplayWarning
            // 
            this.DisplayWarning.AutoSize = true;
            this.DisplayWarning.Checked = global::Reflexil.Properties.Settings.Default.ShowSymbols;
            this.DisplayWarning.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Reflexil.Properties.Settings.Default, "DisplayWarning", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DisplayWarning.Location = new System.Drawing.Point(12, 35);
            this.DisplayWarning.Name = "DisplayWarning";
            this.DisplayWarning.Size = new System.Drawing.Size(226, 17);
            this.DisplayWarning.TabIndex = 1;
            this.DisplayWarning.Text = "Display warning after inject/rename/delete";
            this.DisplayWarning.UseVisualStyleBackColor = true;
            // 
            // ShowSymbols
            // 
            this.ShowSymbols.AutoSize = true;
            this.ShowSymbols.Checked = global::Reflexil.Properties.Settings.Default.ShowSymbols;
            this.ShowSymbols.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Reflexil.Properties.Settings.Default, "ShowSymbols", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowSymbols.Location = new System.Drawing.Point(12, 12);
            this.ShowSymbols.Name = "ShowSymbols";
            this.ShowSymbols.Size = new System.Drawing.Size(166, 17);
            this.ShowSymbols.TabIndex = 0;
            this.ShowSymbols.Text = "Show PDB and MDB symbols";
            this.ShowSymbols.UseVisualStyleBackColor = true;
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 216);
            this.Controls.Add(this.DisplayWarning);
            this.Controls.Add(this.ShowSymbols);
            this.Controls.Add(this.Language);
            this.Controls.Add(this.LabLanguage);
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
        private System.Windows.Forms.Label LabLanguage;
        private System.Windows.Forms.ComboBox Language;
        private System.Windows.Forms.CheckBox ShowSymbols;
        private System.Windows.Forms.CheckBox DisplayWarning;
	}
}