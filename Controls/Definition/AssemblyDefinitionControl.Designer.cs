namespace Reflexil.Editors
{
	partial class AssemblyDefinitionControl
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
            this.MainModuleLab = new System.Windows.Forms.Label();
            this.MainModule = new System.Windows.Forms.ComboBox();
            this.EntryPointLab = new System.Windows.Forms.Label();
            this.TargetRuntime = new System.Windows.Forms.ComboBox();
            this.Kind = new System.Windows.Forms.ComboBox();
            this.TargetRuntimeLab = new System.Windows.Forms.Label();
            this.KindLab = new System.Windows.Forms.Label();
            this.MethodDefinitionEditor = new Reflexil.Editors.MethodDefinitionEditor();
            this.ResetEntryPoint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainModuleLab
            // 
            this.MainModuleLab.AutoSize = true;
            this.MainModuleLab.Location = new System.Drawing.Point(4, 7);
            this.MainModuleLab.Name = "MainModuleLab";
            this.MainModuleLab.Size = new System.Drawing.Size(67, 13);
            this.MainModuleLab.TabIndex = 0;
            this.MainModuleLab.Text = "Main module";
            // 
            // MainModule
            // 
            this.MainModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainModule.Enabled = false;
            this.MainModule.FormattingEnabled = true;
            this.MainModule.Location = new System.Drawing.Point(86, 4);
            this.MainModule.Name = "MainModule";
            this.MainModule.Size = new System.Drawing.Size(299, 21);
            this.MainModule.TabIndex = 0;
            // 
            // EntryPointLab
            // 
            this.EntryPointLab.AutoSize = true;
            this.EntryPointLab.Location = new System.Drawing.Point(4, 34);
            this.EntryPointLab.Name = "EntryPointLab";
            this.EntryPointLab.Size = new System.Drawing.Size(57, 13);
            this.EntryPointLab.TabIndex = 2;
            this.EntryPointLab.Text = "Entry point";
            // 
            // TargetRuntime
            // 
            this.TargetRuntime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetRuntime.FormattingEnabled = true;
            this.TargetRuntime.Location = new System.Drawing.Point(86, 58);
            this.TargetRuntime.Name = "TargetRuntime";
            this.TargetRuntime.Size = new System.Drawing.Size(121, 21);
            this.TargetRuntime.TabIndex = 3;
            this.TargetRuntime.Validated += new System.EventHandler(this.TargetRuntime_Validated);
            // 
            // Kind
            // 
            this.Kind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Kind.FormattingEnabled = true;
            this.Kind.Location = new System.Drawing.Point(86, 85);
            this.Kind.Name = "Kind";
            this.Kind.Size = new System.Drawing.Size(121, 21);
            this.Kind.TabIndex = 4;
            this.Kind.Validated += new System.EventHandler(this.Kind_Validated);
            // 
            // TargetRuntimeLab
            // 
            this.TargetRuntimeLab.AutoSize = true;
            this.TargetRuntimeLab.Location = new System.Drawing.Point(5, 61);
            this.TargetRuntimeLab.Name = "TargetRuntimeLab";
            this.TargetRuntimeLab.Size = new System.Drawing.Size(75, 13);
            this.TargetRuntimeLab.TabIndex = 6;
            this.TargetRuntimeLab.Text = "Target runtime";
            // 
            // KindLab
            // 
            this.KindLab.AutoSize = true;
            this.KindLab.Location = new System.Drawing.Point(5, 88);
            this.KindLab.Name = "KindLab";
            this.KindLab.Size = new System.Drawing.Size(28, 13);
            this.KindLab.TabIndex = 7;
            this.KindLab.Text = "Kind";
            // 
            // MethodDefinitionEditor
            // 
            this.MethodDefinitionEditor.AssemblyRestriction = null;
            this.MethodDefinitionEditor.BackColor = System.Drawing.SystemColors.Window;
            this.MethodDefinitionEditor.Cursor = System.Windows.Forms.Cursors.Default;
            this.MethodDefinitionEditor.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.MethodDefinitionEditor.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.MethodDefinitionEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MethodDefinitionEditor.Location = new System.Drawing.Point(86, 31);
            this.MethodDefinitionEditor.Margin = new System.Windows.Forms.Padding(0);
            this.MethodDefinitionEditor.Name = "MethodDefinitionEditor";
            this.MethodDefinitionEditor.SelectedOperand = null;
            this.MethodDefinitionEditor.Size = new System.Drawing.Size(299, 21);
            this.MethodDefinitionEditor.TabIndex = 1;
            this.MethodDefinitionEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MethodDefinitionEditor.UseVisualStyleBackColor = false;
            this.MethodDefinitionEditor.Validated += new System.EventHandler(this.MethodDefinitionEditor_Validated);
            this.MethodDefinitionEditor.Dock = System.Windows.Forms.DockStyle.None;
            // 
            // ResetEntryPoint
            // 
            this.ResetEntryPoint.Location = new System.Drawing.Point(388, 32);
            this.ResetEntryPoint.Name = "ResetEntryPoint";
            this.ResetEntryPoint.Size = new System.Drawing.Size(20, 20);
            this.ResetEntryPoint.TabIndex = 2;
            this.ResetEntryPoint.Text = "X";
            this.ResetEntryPoint.UseVisualStyleBackColor = true;
            this.ResetEntryPoint.Click += new System.EventHandler(this.ResetEntryPoint_Click);
            // 
            // AssemblyDefinitionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ResetEntryPoint);
            this.Controls.Add(this.MethodDefinitionEditor);
            this.Controls.Add(this.KindLab);
            this.Controls.Add(this.TargetRuntimeLab);
            this.Controls.Add(this.Kind);
            this.Controls.Add(this.TargetRuntime);
            this.Controls.Add(this.EntryPointLab);
            this.Controls.Add(this.MainModule);
            this.Controls.Add(this.MainModuleLab);
            this.Name = "AssemblyDefinitionControl";
            this.Size = new System.Drawing.Size(411, 115);
            this.ResumeLayout(false);
            this.PerformLayout();
		}

		#endregion

        private System.Windows.Forms.Label MainModuleLab;
        private System.Windows.Forms.ComboBox MainModule;
        private System.Windows.Forms.Label EntryPointLab;
        private System.Windows.Forms.ComboBox TargetRuntime;
        private System.Windows.Forms.ComboBox Kind;
        private System.Windows.Forms.Label TargetRuntimeLab;
        private System.Windows.Forms.Label KindLab;
        private MethodDefinitionEditor MethodDefinitionEditor;
        private System.Windows.Forms.Button ResetEntryPoint;
	}
}
