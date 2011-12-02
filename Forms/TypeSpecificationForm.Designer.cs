namespace Reflexil.Forms
{
	partial class TypeSpecificationForm
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
            this.LabName = new System.Windows.Forms.Label();
            this.ItemName = new System.Windows.Forms.TextBox();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.TypeSpecificationEditor = new Reflexil.Editors.TypeSpecificationEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // LabName
            // 
            this.LabName.AutoSize = true;
            this.LabName.Location = new System.Drawing.Point(7, 15);
            this.LabName.Name = "LabName";
            this.LabName.Size = new System.Drawing.Size(35, 13);
            this.LabName.TabIndex = 1;
            this.LabName.Text = "Name";
            // 
            // ItemName
            // 
            this.ItemName.Location = new System.Drawing.Point(82, 12);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(310, 20);
            this.ItemName.TabIndex = 0;
            this.ItemName.Validating += new System.ComponentModel.CancelEventHandler(this.ItemName_Validating);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // TypeSpecificationEditor
            // 
            this.TypeSpecificationEditor.AllowArray = true;
            this.TypeSpecificationEditor.AllowPointer = true;
            this.TypeSpecificationEditor.AllowReference = true;
            this.TypeSpecificationEditor.Location = new System.Drawing.Point(9, 37);
            this.TypeSpecificationEditor.MethodDefinition = null;
            this.TypeSpecificationEditor.Name = "TypeSpecificationEditor";
            this.TypeSpecificationEditor.SelectedTypeReference = null;
            this.TypeSpecificationEditor.Size = new System.Drawing.Size(383, 78);
            this.TypeSpecificationEditor.TabIndex = 1;
            this.TypeSpecificationEditor.Validating += new System.ComponentModel.CancelEventHandler(this.TypeSpecificationEditor_Validating);
            // 
            // TypeSpecificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 127);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.LabName);
            this.Controls.Add(this.TypeSpecificationEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TypeSpecificationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TypeSpecificationForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        protected System.Windows.Forms.ErrorProvider ErrorProvider;
        protected Reflexil.Editors.TypeSpecificationEditor TypeSpecificationEditor;
        protected System.Windows.Forms.Label LabName;
        protected System.Windows.Forms.TextBox ItemName;

    }
}