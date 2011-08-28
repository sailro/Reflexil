namespace Reflexil.Forms
{
	partial class OverrideForm
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
            this.LabMethodReference = new System.Windows.Forms.Label();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.MethodReferenceEditor = new Reflexil.Editors.MethodReferenceEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // LabMethodReference
            // 
            this.LabMethodReference.AutoSize = true;
            this.LabMethodReference.Location = new System.Drawing.Point(8, 17);
            this.LabMethodReference.Name = "LabMethodReference";
            this.LabMethodReference.Size = new System.Drawing.Size(61, 13);
            this.LabMethodReference.TabIndex = 9;
            this.LabMethodReference.Text = "Method ref.";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // MethodReferenceEditor
            // 
            this.MethodReferenceEditor.BackColor = System.Drawing.SystemColors.Window;
            this.MethodReferenceEditor.Location = new System.Drawing.Point(82, 12);
            this.MethodReferenceEditor.Name = "MethodReferenceEditor";
            this.MethodReferenceEditor.SelectedOperand = null;
            this.MethodReferenceEditor.Size = new System.Drawing.Size(310, 20);
            this.MethodReferenceEditor.TabIndex = 10;
            this.MethodReferenceEditor.Validating += new System.ComponentModel.CancelEventHandler(this.MethodReferenceEditor_Validating);
            this.MethodReferenceEditor.Dock = System.Windows.Forms.DockStyle.None;
            // 
            // OverrideForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 147);
            this.Controls.Add(this.MethodReferenceEditor);
            this.Controls.Add(this.LabMethodReference);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OverrideForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OverrideForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        internal System.Windows.Forms.Label LabMethodReference;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected Reflexil.Editors.MethodReferenceEditor MethodReferenceEditor;
	}
}