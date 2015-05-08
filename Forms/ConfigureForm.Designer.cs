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
			this.CacheFiles = new System.Windows.Forms.CheckBox();
			this.DisplayWarning = new System.Windows.Forms.CheckBox();
			this.ShowSymbols = new System.Windows.Forms.CheckBox();
			this.AutoDetectObfuscators = new System.Windows.Forms.CheckBox();
			this.OptimizeAndFixIL = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// LabInputBase
			// 
			this.LabInputBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LabInputBase.AutoSize = true;
			this.LabInputBase.Location = new System.Drawing.Point(12, 139);
			this.LabInputBase.Name = "LabInputBase";
			this.LabInputBase.Size = new System.Drawing.Size(99, 13);
			this.LabInputBase.TabIndex = 0;
			this.LabInputBase.Text = "Prefered input base";
			// 
			// LabRowBase
			// 
			this.LabRowBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LabRowBase.AutoSize = true;
			this.LabRowBase.Location = new System.Drawing.Point(12, 166);
			this.LabRowBase.Name = "LabRowBase";
			this.LabRowBase.Size = new System.Drawing.Size(118, 13);
			this.LabRowBase.TabIndex = 1;
			this.LabRowBase.Text = "Row index display base";
			// 
			// LabOperandBase
			// 
			this.LabOperandBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LabOperandBase.AutoSize = true;
			this.LabOperandBase.Location = new System.Drawing.Point(12, 193);
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
			this.InputBase.Location = new System.Drawing.Point(150, 136);
			this.InputBase.Name = "InputBase";
			this.InputBase.Size = new System.Drawing.Size(101, 21);
			this.InputBase.TabIndex = 5;
			// 
			// RowBase
			// 
			this.RowBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RowBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RowBase.FormattingEnabled = true;
			this.RowBase.Location = new System.Drawing.Point(150, 163);
			this.RowBase.Name = "RowBase";
			this.RowBase.Size = new System.Drawing.Size(101, 21);
			this.RowBase.TabIndex = 6;
			// 
			// OperandBase
			// 
			this.OperandBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.OperandBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OperandBase.FormattingEnabled = true;
			this.OperandBase.Location = new System.Drawing.Point(150, 190);
			this.OperandBase.Name = "OperandBase";
			this.OperandBase.Size = new System.Drawing.Size(101, 21);
			this.OperandBase.TabIndex = 7;
			// 
			// Ok
			// 
			this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Ok.Location = new System.Drawing.Point(95, 256);
			this.Ok.Name = "Ok";
			this.Ok.Size = new System.Drawing.Size(75, 23);
			this.Ok.TabIndex = 9;
			this.Ok.Text = "Ok";
			this.Ok.UseVisualStyleBackColor = true;
			this.Ok.Click += new System.EventHandler(this.Ok_Click);
			// 
			// Cancel
			// 
			this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(176, 256);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 10;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// LabLanguage
			// 
			this.LabLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LabLanguage.AutoSize = true;
			this.LabLanguage.Location = new System.Drawing.Point(12, 219);
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
			this.Language.Location = new System.Drawing.Point(150, 216);
			this.Language.Name = "Language";
			this.Language.Size = new System.Drawing.Size(101, 21);
			this.Language.TabIndex = 8;
			// 
			// CacheFiles
			// 
			this.CacheFiles.AutoSize = true;
			this.CacheFiles.Checked = global::Reflexil.Properties.Settings.Default.CacheFiles;
			this.CacheFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CacheFiles.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Reflexil.Properties.Settings.Default, "CacheFiles", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.CacheFiles.Location = new System.Drawing.Point(12, 58);
			this.CacheFiles.Name = "CacheFiles";
			this.CacheFiles.Size = new System.Drawing.Size(244, 17);
			this.CacheFiles.TabIndex = 2;
			this.CacheFiles.Text = "Cache intellisense/documentation files on disk";
			this.CacheFiles.UseVisualStyleBackColor = true;
			// 
			// DisplayWarning
			// 
			this.DisplayWarning.AutoSize = true;
			this.DisplayWarning.Checked = global::Reflexil.Properties.Settings.Default.DisplayWarning;
			this.DisplayWarning.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DisplayWarning.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Reflexil.Properties.Settings.Default, "DisplayWarning", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.DisplayWarning.Location = new System.Drawing.Point(12, 35);
			this.DisplayWarning.Name = "DisplayWarning";
			this.DisplayWarning.Size = new System.Drawing.Size(239, 17);
			this.DisplayWarning.TabIndex = 1;
			this.DisplayWarning.Text = "Display warning after inject/rename/delete (*)";
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
			// AutoDetectObfuscators
			// 
			this.AutoDetectObfuscators.AutoSize = true;
			this.AutoDetectObfuscators.Checked = global::Reflexil.Properties.Settings.Default.AutoDetectObfuscators;
			this.AutoDetectObfuscators.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Reflexil.Properties.Settings.Default, "AutoDetectObfuscators", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.AutoDetectObfuscators.Location = new System.Drawing.Point(12, 81);
			this.AutoDetectObfuscators.Name = "AutoDetectObfuscators";
			this.AutoDetectObfuscators.Size = new System.Drawing.Size(220, 17);
			this.AutoDetectObfuscators.TabIndex = 3;
			this.AutoDetectObfuscators.Text = "Autodetect obfuscators on assembly load";
			this.AutoDetectObfuscators.UseVisualStyleBackColor = true;
			// 
			// OptimizeAndFixIL
			// 
			this.OptimizeAndFixIL.AutoSize = true;
			this.OptimizeAndFixIL.Checked = global::Reflexil.Properties.Settings.Default.OptimizeAndFixIL;
			this.OptimizeAndFixIL.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Reflexil.Properties.Settings.Default, "OptimizeAndFixIL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.OptimizeAndFixIL.Location = new System.Drawing.Point(12, 104);
			this.OptimizeAndFixIL.Name = "OptimizeAndFixIL";
			this.OptimizeAndFixIL.Size = new System.Drawing.Size(112, 17);
			this.OptimizeAndFixIL.TabIndex = 4;
			this.OptimizeAndFixIL.Text = "Optimize and fix IL";
			this.OptimizeAndFixIL.UseVisualStyleBackColor = true;
			// 
			// ConfigureForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(263, 291);
			this.Controls.Add(this.OptimizeAndFixIL);
			this.Controls.Add(this.AutoDetectObfuscators);
			this.Controls.Add(this.CacheFiles);
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
        private System.Windows.Forms.CheckBox CacheFiles;
        private System.Windows.Forms.CheckBox AutoDetectObfuscators;
		private System.Windows.Forms.CheckBox OptimizeAndFixIL;
	}
}