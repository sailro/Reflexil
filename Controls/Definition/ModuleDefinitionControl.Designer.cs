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
            this.TargetRuntime = new System.Windows.Forms.ComboBox();
            this.Kind = new System.Windows.Forms.ComboBox();
            this.TargetRuntimeLab = new System.Windows.Forms.Label();
            this.KindLab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TargetRuntime
            // 
            this.TargetRuntime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetRuntime.FormattingEnabled = true;
            this.TargetRuntime.Location = new System.Drawing.Point(86, 4);
            this.TargetRuntime.Name = "TargetRuntime";
            this.TargetRuntime.Size = new System.Drawing.Size(121, 21);
            this.TargetRuntime.TabIndex = 3;
            this.TargetRuntime.Validated += new System.EventHandler(this.TargetRuntime_Validated);
            // 
            // Kind
            // 
            this.Kind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Kind.FormattingEnabled = true;
            this.Kind.Location = new System.Drawing.Point(86, 31);
            this.Kind.Name = "Kind";
            this.Kind.Size = new System.Drawing.Size(121, 21);
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
            // ModuleDefinitionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.KindLab);
            this.Controls.Add(this.TargetRuntimeLab);
            this.Controls.Add(this.Kind);
            this.Controls.Add(this.TargetRuntime);
            this.Name = "ModuleDefinitionControl";
            this.Size = new System.Drawing.Size(257, 92);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ComboBox TargetRuntime;
        private System.Windows.Forms.ComboBox Kind;
        private System.Windows.Forms.Label TargetRuntimeLab;
        private System.Windows.Forms.Label KindLab;
	}
}
