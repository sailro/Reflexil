namespace Reflexil.Editors
{
	partial class AssemblyDefinitionEditor
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
            this.CbxAssemblies = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CbxAssemblies
            // 
            this.CbxAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CbxAssemblies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbxAssemblies.FormattingEnabled = true;
            this.CbxAssemblies.Location = new System.Drawing.Point(0, 0);
            this.CbxAssemblies.Name = "CbxAssemblies";
            this.CbxAssemblies.Size = new System.Drawing.Size(121, 21);
            this.CbxAssemblies.TabIndex = 0;
            // 
            // AssemblyDefinitionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CbxAssemblies);
            this.Name = "AssemblyDefinitionEditor";
            this.Size = new System.Drawing.Size(121, 21);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ComboBox CbxAssemblies;
	}
}
