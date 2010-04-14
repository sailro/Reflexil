namespace Reflexil.Editors
{
    partial class PropertyAttributesControl
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
            this.PropertyType = new Reflexil.Editors.TypeSpecificationEditor();
            this.GbxPropertyType = new System.Windows.Forms.GroupBox();
            this.Constant = new Reflexil.Editors.ConstantEditor();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.GbxPropertyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.GbxPropertyType);
            // 
            // PropertyType
            // 
            this.PropertyType.AllowArray = true;
            this.PropertyType.AllowPointer = true;
            this.PropertyType.AllowReference = false;
            this.PropertyType.Location = new System.Drawing.Point(6, 19);
            this.PropertyType.MethodDefinition = null;
            this.PropertyType.Name = "PropertyType";
            this.PropertyType.SelectedTypeReference = null;
            this.PropertyType.Size = new System.Drawing.Size(383, 77);
            this.PropertyType.TabIndex = 5;
            this.PropertyType.Validating += new System.ComponentModel.CancelEventHandler(this.PropertyType_Validating);
            // 
            // GbxPropertyType
            // 
            this.GbxPropertyType.Controls.Add(this.Constant);
            this.GbxPropertyType.Controls.Add(this.PropertyType);
            this.GbxPropertyType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GbxPropertyType.Location = new System.Drawing.Point(0, 0);
            this.GbxPropertyType.Name = "GbxPropertyType";
            this.GbxPropertyType.Size = new System.Drawing.Size(491, 452);
            this.GbxPropertyType.TabIndex = 7;
            this.GbxPropertyType.TabStop = false;
            this.GbxPropertyType.Text = "Property type";
            // 
            // Constant
            // 
            this.Constant.Location = new System.Drawing.Point(6, 102);
            this.Constant.Name = "Constant";
            this.Constant.Size = new System.Drawing.Size(383, 52);
            this.Constant.TabIndex = 6;
            this.Constant.Validating += new System.ComponentModel.CancelEventHandler(this.Constant_Validating);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // PropertyAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "PropertyAttributesControl";
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.GbxPropertyType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbxPropertyType;
        private TypeSpecificationEditor PropertyType;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private ConstantEditor Constant;
    }
}
