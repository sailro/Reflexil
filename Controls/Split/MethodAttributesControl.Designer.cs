namespace Reflexil.Editors
{
	partial class MethodAttributesControl
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
            this.components = new System.ComponentModel.Container();
            this.RVA = new System.Windows.Forms.TextBox();
            this.LabRVA = new System.Windows.Forms.Label();
            this.LabCallingConvention = new System.Windows.Forms.Label();
            this.CallingConvention = new System.Windows.Forms.ComboBox();
            this.GbxReturnType = new System.Windows.Forms.GroupBox();
            this.ReturnType = new Reflexil.Editors.TypeSpecificationEditor();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.Attributes = new Reflexil.Editors.AttributesControl();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.GbxReturnType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.Attributes);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.GbxReturnType);
            this.SplitContainer.Panel2.Controls.Add(this.RVA);
            this.SplitContainer.Panel2.Controls.Add(this.LabRVA);
            this.SplitContainer.Panel2.Controls.Add(this.LabCallingConvention);
            this.SplitContainer.Panel2.Controls.Add(this.CallingConvention);
            this.SplitContainer.Size = new System.Drawing.Size(647, 416);
            // 
            // RVA
            // 
            this.RVA.Location = new System.Drawing.Point(92, 32);
            this.RVA.Name = "RVA";
            this.RVA.ReadOnly = true;
            this.RVA.Size = new System.Drawing.Size(100, 20);
            this.RVA.TabIndex = 4;
            // 
            // LabRVA
            // 
            this.LabRVA.AutoSize = true;
            this.LabRVA.Location = new System.Drawing.Point(15, 35);
            this.LabRVA.Name = "LabRVA";
            this.LabRVA.Size = new System.Drawing.Size(29, 13);
            this.LabRVA.TabIndex = 3;
            this.LabRVA.Text = "RVA";
            // 
            // LabCallingConvention
            // 
            this.LabCallingConvention.AutoSize = true;
            this.LabCallingConvention.Location = new System.Drawing.Point(15, 7);
            this.LabCallingConvention.Name = "LabCallingConvention";
            this.LabCallingConvention.Size = new System.Drawing.Size(61, 13);
            this.LabCallingConvention.TabIndex = 2;
            this.LabCallingConvention.Text = "Convention";
            // 
            // CallingConvention
            // 
            this.CallingConvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CallingConvention.FormattingEnabled = true;
            this.CallingConvention.Location = new System.Drawing.Point(92, 4);
            this.CallingConvention.Name = "CallingConvention";
            this.CallingConvention.Size = new System.Drawing.Size(100, 21);
            this.CallingConvention.TabIndex = 1;
            this.CallingConvention.SelectionChangeCommitted += new System.EventHandler(this.CallingConvention_SelectionChangeCommitted);
            // 
            // GbxReturnType
            // 
            this.GbxReturnType.Controls.Add(this.ReturnType);
            this.GbxReturnType.Location = new System.Drawing.Point(12, 72);
            this.GbxReturnType.Name = "GbxReturnType";
            this.GbxReturnType.Size = new System.Drawing.Size(408, 119);
            this.GbxReturnType.TabIndex = 6;
            this.GbxReturnType.TabStop = false;
            this.GbxReturnType.Text = "Return type";
            // 
            // ReturnType
            // 
            this.ReturnType.AllowArray = true;
            this.ReturnType.AllowPointer = true;
            this.ReturnType.AllowReference = false;
            this.ReturnType.Location = new System.Drawing.Point(6, 19);
            this.ReturnType.MethodDefinition = null;
            this.ReturnType.Name = "ReturnType";
            this.ReturnType.SelectedTypeReference = null;
            this.ReturnType.Size = new System.Drawing.Size(383, 77);
            this.ReturnType.TabIndex = 5;
            this.ReturnType.Validating += new System.ComponentModel.CancelEventHandler(this.ReturnType_Validating);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // Attributes
            // 
            this.Attributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Attributes.Item = null;
            this.Attributes.Location = new System.Drawing.Point(0, 0);
            this.Attributes.Name = "Attributes";
            this.Attributes.Size = new System.Drawing.Size(220, 416);
            this.Attributes.TabIndex = 0;
            // 
            // MethodAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MethodAttributesControl";
            this.Size = new System.Drawing.Size(647, 416);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            this.SplitContainer.ResumeLayout(false);
            this.GbxReturnType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TextBox RVA;
        private System.Windows.Forms.Label LabRVA;
        private System.Windows.Forms.Label LabCallingConvention;
        private System.Windows.Forms.ComboBox CallingConvention;
        private Reflexil.Editors.AttributesControl Attributes;
        private TypeSpecificationEditor ReturnType;
        private System.Windows.Forms.GroupBox GbxReturnType;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
	}
}
