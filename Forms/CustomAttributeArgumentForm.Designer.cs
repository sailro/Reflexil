namespace Reflexil.Forms
{
	partial class CustomAttributeArgumentForm
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
            this.AttributeArgumentEditor = new Reflexil.Editors.CustomAttributeArgumentEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // AttributeArgumentEditor
            // 
            this.AttributeArgumentEditor.AllowArray = true;
            this.AttributeArgumentEditor.Location = new System.Drawing.Point(6, 12);
            this.AttributeArgumentEditor.Name = "AttributeArgumentEditor";
            this.AttributeArgumentEditor.Size = new System.Drawing.Size(386, 108);
            this.AttributeArgumentEditor.TabIndex = 15;
            this.AttributeArgumentEditor.Validating += new System.ComponentModel.CancelEventHandler(this.AttributeArgumentEditor_Validating);
            // 
            // CustomAttributeArgumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 126);
            this.Controls.Add(this.AttributeArgumentEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomAttributeArgumentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomAttributeArgumentForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected Editors.CustomAttributeArgumentEditor AttributeArgumentEditor;
	}
}