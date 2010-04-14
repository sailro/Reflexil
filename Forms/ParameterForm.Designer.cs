namespace Reflexil.Forms
{
    partial class ParameterForm
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
            this.Attributes = new Reflexil.Editors.AttributesControl();
            this.ConstantEditor = new Reflexil.Editors.ConstantEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // Attributes
            // 
            this.Attributes.Item = null;
            this.Attributes.Location = new System.Drawing.Point(10, 177);
            this.Attributes.Name = "Attributes";
            this.Attributes.Size = new System.Drawing.Size(382, 66);
            this.Attributes.TabIndex = 16;
            // 
            // ConstantEditor
            // 
            this.ConstantEditor.Location = new System.Drawing.Point(9, 119);
            this.ConstantEditor.Name = "ConstantEditor";
            this.ConstantEditor.Size = new System.Drawing.Size(383, 52);
            this.ConstantEditor.TabIndex = 17;
            this.ConstantEditor.Validating += new System.ComponentModel.CancelEventHandler(this.Constant_Validating);
            // 
            // ParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 248);
            this.Controls.Add(this.ConstantEditor);
            this.Controls.Add(this.Attributes);
            this.Name = "ParameterForm";
            this.Controls.SetChildIndex(this.TypeSpecificationEditor, 0);
            this.Controls.SetChildIndex(this.Attributes, 0);
            this.Controls.SetChildIndex(this.ConstantEditor, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected Reflexil.Editors.AttributesControl Attributes;
        protected Reflexil.Editors.ConstantEditor ConstantEditor;
    }
}
