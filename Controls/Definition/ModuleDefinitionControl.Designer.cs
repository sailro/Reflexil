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
			this.Characteristics = new Reflexil.Editors.CheckBoxComboBox();
			this.Attributes = new Reflexil.Editors.CheckBoxComboBox();
			this.AttributesLab = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TargetRuntime
			// 
			this.TargetRuntime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TargetRuntime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TargetRuntime.FormattingEnabled = true;
			this.TargetRuntime.Location = new System.Drawing.Point(115, 5);
			this.TargetRuntime.Margin = new System.Windows.Forms.Padding(4);
			this.TargetRuntime.Name = "TargetRuntime";
			this.TargetRuntime.Size = new System.Drawing.Size(384, 24);
			this.TargetRuntime.TabIndex = 3;
			this.TargetRuntime.Validated += new System.EventHandler(this.TargetRuntime_Validated);
			// 
			// Kind
			// 
			this.Kind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Kind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Kind.FormattingEnabled = true;
			this.Kind.Location = new System.Drawing.Point(115, 38);
			this.Kind.Margin = new System.Windows.Forms.Padding(4);
			this.Kind.Name = "Kind";
			this.Kind.Size = new System.Drawing.Size(384, 24);
			this.Kind.TabIndex = 4;
			this.Kind.Validated += new System.EventHandler(this.Kind_Validated);
			// 
			// TargetRuntimeLab
			// 
			this.TargetRuntimeLab.AutoSize = true;
			this.TargetRuntimeLab.Location = new System.Drawing.Point(7, 9);
			this.TargetRuntimeLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TargetRuntimeLab.Name = "TargetRuntimeLab";
			this.TargetRuntimeLab.Size = new System.Drawing.Size(101, 17);
			this.TargetRuntimeLab.TabIndex = 6;
			this.TargetRuntimeLab.Text = "Target runtime";
			// 
			// KindLab
			// 
			this.KindLab.AutoSize = true;
			this.KindLab.Location = new System.Drawing.Point(7, 42);
			this.KindLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.KindLab.Name = "KindLab";
			this.KindLab.Size = new System.Drawing.Size(36, 17);
			this.KindLab.TabIndex = 7;
			this.KindLab.Text = "Kind";
			// 
			// ArchitectureLab
			// 
			this.ArchitectureLab.AutoSize = true;
			this.ArchitectureLab.Location = new System.Drawing.Point(7, 75);
			this.ArchitectureLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ArchitectureLab.Name = "ArchitectureLab";
			this.ArchitectureLab.Size = new System.Drawing.Size(84, 17);
			this.ArchitectureLab.TabIndex = 9;
			this.ArchitectureLab.Text = "Architecture";
			// 
			// Architecture
			// 
			this.Architecture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Architecture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Architecture.FormattingEnabled = true;
			this.Architecture.Location = new System.Drawing.Point(115, 71);
			this.Architecture.Margin = new System.Windows.Forms.Padding(4);
			this.Architecture.Name = "Architecture";
			this.Architecture.Size = new System.Drawing.Size(384, 24);
			this.Architecture.TabIndex = 8;
			this.Architecture.Validated += new System.EventHandler(this.Architecture_Validated);
			// 
			// CharacteristicsLab
			// 
			this.CharacteristicsLab.AutoSize = true;
			this.CharacteristicsLab.Location = new System.Drawing.Point(7, 110);
			this.CharacteristicsLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CharacteristicsLab.Name = "CharacteristicsLab";
			this.CharacteristicsLab.Size = new System.Drawing.Size(101, 17);
			this.CharacteristicsLab.TabIndex = 11;
			this.CharacteristicsLab.Text = "Characteristics";
			// 
			// Characteristics
			// 
			this.Characteristics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Characteristics.CheckBoxProperties = checkBoxProperties1;
			this.Characteristics.DisplayMemberSingleItem = "";
			this.Characteristics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Characteristics.FormattingEnabled = true;
			this.Characteristics.Location = new System.Drawing.Point(115, 106);
			this.Characteristics.Margin = new System.Windows.Forms.Padding(4);
			this.Characteristics.Name = "Characteristics";
			this.Characteristics.Size = new System.Drawing.Size(384, 24);
			this.Characteristics.TabIndex = 10;
			this.Characteristics.Validated += new System.EventHandler(this.Characteristics_Validated);
			// 
			// Attributes
			// 
			this.Attributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Attributes.CheckBoxProperties = checkBoxProperties2;
			this.Attributes.DisplayMemberSingleItem = "";
			this.Attributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Attributes.FormattingEnabled = true;
			this.Attributes.Location = new System.Drawing.Point(115, 140);
			this.Attributes.Margin = new System.Windows.Forms.Padding(4);
			this.Attributes.Name = "Attributes";
			this.Attributes.Size = new System.Drawing.Size(384, 24);
			this.Attributes.TabIndex = 12;
			// 
			// AttributesLab
			// 
			this.AttributesLab.AutoSize = true;
			this.AttributesLab.Location = new System.Drawing.Point(6, 143);
			this.AttributesLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.AttributesLab.Name = "AttributesLab";
			this.AttributesLab.Size = new System.Drawing.Size(68, 17);
			this.AttributesLab.TabIndex = 13;
			this.AttributesLab.Text = "Attributes";
			// 
			// ModuleDefinitionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
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
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ModuleDefinitionControl";
			this.Size = new System.Drawing.Size(510, 177);
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
