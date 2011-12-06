namespace Reflexil.Forms
{
	partial class CustomAttributeForm
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
            this.components = new System.ComponentModel.Container();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ConstructorArguments = new Reflexil.Editors.CustomAttributeArgumentGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // ConstructorArguments
            // 
            this.ConstructorArguments.Location = new System.Drawing.Point(12, 12);
            this.ConstructorArguments.Name = "ConstructorArguments";
            this.ConstructorArguments.ReadOnly = false;
            this.ConstructorArguments.Size = new System.Drawing.Size(361, 123);
            this.ConstructorArguments.TabIndex = 0;
            // 
            // CustomAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 147);
            this.Controls.Add(this.ConstructorArguments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomAttributeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomAttributeForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected Editors.CustomAttributeArgumentGridControl ConstructorArguments;
	}
}