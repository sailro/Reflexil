namespace Reflexil.Editors
{
	partial class ModuleDefinitionControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Reflexil.Editors.CheckBoxProperties checkBoxProperties1 = new Reflexil.Editors.CheckBoxProperties();
			Reflexil.Editors.CheckBoxProperties checkBoxProperties2 = new Reflexil.Editors.CheckBoxProperties();
			this.TargetRuntime = new System.Windows.Forms.ComboBox();
			this.Kind = new System.Windows.Forms.ComboBox();
			this.TargetRuntimeLab = new System.Windows.Forms.Label();
			this.KindLab = new System.Windows.Forms.Label();
			this.ArchitectureLab = new System.Windows.Forms.Label();
			this.Architecture = new System.Windows.Forms.ComboBox();
			this.CharacteristicsLab = new System.Windows.Forms.Label();
			this.AttributesLab = new System.Windows.Forms.Label();
			this.Attributes = new Reflexil.Editors.CheckBoxComboBox();
			this.Characteristics = new Reflexil.Editors.CheckBoxComboBox();
			this.SuspendLayout();
			// 
			// TargetRuntime
			// 
			this.TargetRuntime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TargetRuntime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TargetRuntime.FormattingEnabled = true;
			this.TargetRuntime.Location = new System.Drawing.Point(86, 4);
			this.TargetRuntime.Name = "TargetRuntime";
			this.TargetRuntime.Size = new System.Drawing.Size(99, 21);
			this.TargetRuntime.TabIndex = 3;
			this.TargetRuntime.Validated += new System.EventHandler(this.TargetRuntime_Validated);
			// 
			// Kind
			// 
			this.Kind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Kind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Kind.FormattingEnabled = true;
			this.Kind.Location = new System.Drawing.Point(86, 31);
			this.Kind.Name = "Kind";
			this.Kind.Size = new System.Drawing.Size(99, 21);
			this.Kind.TabIndex = 4;
			this.Kind.Validated += new System.EventHandler(this.Kind_Validated);
			// 
			// TargetRuntimeLab
			// 
			this.TargetRuntimeLab.AutoSize = true;
			this.TargetRuntimeLab.Location = new System.Drawing.Point(5, 7);
			this.TargetRuntimeLab.Name = "TargetRuntimeLab";
			this.TargetRuntimeLab.Size = new System.Drawing.Size(75, 13);
			this.TargetRuntimeLab.TabIndex = 6;
			this.TargetRuntimeLab.Text = "Target runtime";
			// 
			// KindLab
			// 
			this.KindLab.AutoSize = true;
			this.KindLab.Location = new System.Drawing.Point(5, 34);
			this.KindLab.Name = "KindLab";
			this.KindLab.Size = new System.Drawing.Size(28, 13);
			this.KindLab.TabIndex = 7;
			this.KindLab.Text = "Kind";
			// 
			// ArchitectureLab
			// 
			this.ArchitectureLab.AutoSize = true;
			this.ArchitectureLab.Location = new System.Drawing.Point(5, 61);
			this.ArchitectureLab.Name = "ArchitectureLab";
			this.ArchitectureLab.Size = new System.Drawing.Size(64, 13);
			this.ArchitectureLab.TabIndex = 9;
			this.ArchitectureLab.Text = "Architecture";
			// 
			// Architecture
			// 
			this.Architecture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Architecture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Architecture.FormattingEnabled = true;
			this.Architecture.Location = new System.Drawing.Point(86, 58);
			this.Architecture.Name = "Architecture";
			this.Architecture.Size = new System.Drawing.Size(99, 21);
			this.Architecture.TabIndex = 8;
			this.Architecture.Validated += new System.EventHandler(this.Architecture_Validated);
			// 
			// CharacteristicsLab
			// 
			this.CharacteristicsLab.AutoSize = true;
			this.CharacteristicsLab.Location = new System.Drawing.Point(5, 89);
			this.CharacteristicsLab.Name = "CharacteristicsLab";
			this.CharacteristicsLab.Size = new System.Drawing.Size(76, 13);
			this.CharacteristicsLab.TabIndex = 11;
			this.CharacteristicsLab.Text = "Characteristics";
			// 
			// AttributesLab
			// 
			this.AttributesLab.AutoSize = true;
			this.AttributesLab.Location = new System.Drawing.Point(4, 116);
			this.AttributesLab.Name = "AttributesLab";
			this.AttributesLab.Size = new System.Drawing.Size(51, 13);
			this.AttributesLab.TabIndex = 13;
			this.AttributesLab.Text = "Attributes";
			// 
			// Attributes
			// 
			this.Attributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Attributes.CheckBoxProperties = checkBoxProperties1;
			this.Attributes.DisplayMemberSingleItem = "";
			this.Attributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Attributes.FormattingEnabled = true;
			this.Attributes.Location = new System.Drawing.Point(86, 114);
			this.Attributes.Name = "Attributes";
			this.Attributes.Size = new System.Drawing.Size(99, 21);
			this.Attributes.TabIndex = 12;
			this.Attributes.Validated += new System.EventHandler(this.Attributes_Validated);
			// 
			// Characteristics
			// 
			this.Characteristics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Characteristics.CheckBoxProperties = checkBoxProperties2;
			this.Characteristics.DisplayMemberSingleItem = "";
			this.Characteristics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Characteristics.FormattingEnabled = true;
			this.Characteristics.Location = new System.Drawing.Point(86, 86);
			this.Characteristics.Name = "Characteristics";
			this.Characteristics.Size = new System.Drawing.Size(99, 21);
			this.Characteristics.TabIndex = 10;
			this.Characteristics.Validated += new System.EventHandler(this.Characteristics_Validated);
			// 
			// ModuleDefinitionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.AttributesLab);
			this.Controls.Add(this.Attributes);
			this.Controls.Add(this.CharacteristicsLab);
			this.Controls.Add(this.Characteristics);
			this.Controls.Add(this.ArchitectureLab);
			this.Controls.Add(this.Architecture);
			this.Controls.Add(this.KindLab);
			this.Controls.Add(this.TargetRuntimeLab);
			this.Controls.Add(this.Kind);
			this.Controls.Add(this.TargetRuntime);
			this.MaximumSize = new System.Drawing.Size(500, 140);
			this.MinimumSize = new System.Drawing.Size(192, 140);
			this.Name = "ModuleDefinitionControl";
			this.Size = new System.Drawing.Size(192, 140);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ComboBox TargetRuntime;
        private System.Windows.Forms.ComboBox Kind;
        private System.Windows.Forms.Label TargetRuntimeLab;
        private System.Windows.Forms.Label KindLab;
        private System.Windows.Forms.Label ArchitectureLab;
        private System.Windows.Forms.ComboBox Architecture;
		private System.Windows.Forms.Label CharacteristicsLab;
		private Editors.CheckBoxComboBox Characteristics;
		private Editors.CheckBoxComboBox Attributes;
		private System.Windows.Forms.Label AttributesLab;
	}
}
