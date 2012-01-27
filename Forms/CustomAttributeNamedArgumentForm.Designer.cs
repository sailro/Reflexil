namespace Reflexil.Forms
{
    partial class CustomAttributeNamedArgumentForm
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
            this.ItemName = new System.Windows.Forms.TextBox();
            this.LabName = new System.Windows.Forms.Label();
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
            this.AttributeArgumentEditor.Location = new System.Drawing.Point(6, 30);
            this.AttributeArgumentEditor.Name = "AttributeArgumentEditor";
            this.AttributeArgumentEditor.Size = new System.Drawing.Size(386, 108);
            this.AttributeArgumentEditor.TabIndex = 1;
            this.AttributeArgumentEditor.Validating += new System.ComponentModel.CancelEventHandler(this.AttributeArgumentEditor_Validating);
            // 
            // ItemName
            // 
            this.ItemName.Location = new System.Drawing.Point(81, 6);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(310, 20);
            this.ItemName.TabIndex = 0;
            this.ItemName.Validating += new System.ComponentModel.CancelEventHandler(this.ItemName_Validating);
            // 
            // LabName
            // 
            this.LabName.AutoSize = true;
            this.LabName.Location = new System.Drawing.Point(6, 9);
            this.LabName.Name = "LabName";
            this.LabName.Size = new System.Drawing.Size(35, 13);
            this.LabName.TabIndex = 17;
            this.LabName.Text = "Name";
            // 
            // CustomAttributeNamedArgumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 140);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.LabName);
            this.Controls.Add(this.AttributeArgumentEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomAttributeNamedArgumentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomAttributeArgumentForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected Editors.CustomAttributeArgumentEditor AttributeArgumentEditor;
        protected System.Windows.Forms.TextBox ItemName;
        protected System.Windows.Forms.Label LabName;
	}
}