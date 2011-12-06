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
            this.TypeReferenceEditorPanel = new System.Windows.Forms.Panel();
            this.TypeReferenceEditor = new Reflexil.Editors.TypeReferenceEditor();
            this.LabTypeReference = new System.Windows.Forms.Label();
            this.ConstantEditor = new Reflexil.Editors.ConstantEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.TypeReferenceEditorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // TypeReferenceEditorPanel
            // 
            this.TypeReferenceEditorPanel.Controls.Add(this.TypeReferenceEditor);
            this.TypeReferenceEditorPanel.Location = new System.Drawing.Point(82, 12);
            this.TypeReferenceEditorPanel.Name = "TypeReferenceEditorPanel";
            this.TypeReferenceEditorPanel.Size = new System.Drawing.Size(310, 20);
            this.TypeReferenceEditorPanel.TabIndex = 13;
            // 
            // TypeReferenceEditor
            // 
            this.TypeReferenceEditor.AssemblyRestriction = null;
            this.TypeReferenceEditor.BackColor = System.Drawing.SystemColors.Window;
            this.TypeReferenceEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TypeReferenceEditor.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.TypeReferenceEditor.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.TypeReferenceEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TypeReferenceEditor.Location = new System.Drawing.Point(0, 0);
            this.TypeReferenceEditor.Name = "TypeReferenceEditor";
            this.TypeReferenceEditor.SelectedOperand = null;
            this.TypeReferenceEditor.Size = new System.Drawing.Size(310, 20);
            this.TypeReferenceEditor.TabIndex = 10;
            this.TypeReferenceEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TypeReferenceEditor.UseVisualStyleBackColor = false;
            // 
            // LabTypeReference
            // 
            this.LabTypeReference.AutoSize = true;
            this.LabTypeReference.Location = new System.Drawing.Point(8, 17);
            this.LabTypeReference.Name = "LabTypeReference";
            this.LabTypeReference.Size = new System.Drawing.Size(49, 13);
            this.LabTypeReference.TabIndex = 12;
            this.LabTypeReference.Text = "Type ref.";
            // 
            // ConstantEditor
            // 
            this.ConstantEditor.Location = new System.Drawing.Point(9, 38);
            this.ConstantEditor.Name = "ConstantEditor";
            this.ConstantEditor.Size = new System.Drawing.Size(383, 52);
            this.ConstantEditor.TabIndex = 14;
            // 
            // CustomAttributeArgumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 147);
            this.Controls.Add(this.ConstantEditor);
            this.Controls.Add(this.TypeReferenceEditorPanel);
            this.Controls.Add(this.LabTypeReference);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomAttributeArgumentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomAttributeArgumentForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.TypeReferenceEditorPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected System.Windows.Forms.Panel TypeReferenceEditorPanel;
        protected Editors.TypeReferenceEditor TypeReferenceEditor;
        internal System.Windows.Forms.Label LabTypeReference;
        protected Editors.ConstantEditor ConstantEditor;
	}
}