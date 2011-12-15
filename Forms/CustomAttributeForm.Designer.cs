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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabCtorArgs = new System.Windows.Forms.TabPage();
            this.ConstructorArguments = new Reflexil.Editors.CustomAttributeArgumentGridControl();
            this.TabFields = new System.Windows.Forms.TabPage();
            this.Fields = new Reflexil.Editors.CustomAttributeNamedArgumentGridControl();
            this.TabProperties = new System.Windows.Forms.TabPage();
            this.Properties = new Reflexil.Editors.CustomAttributeNamedArgumentGridControl();
            this.TabAttributes = new System.Windows.Forms.TabPage();
            this.LabAttribute = new System.Windows.Forms.Label();
            this.LabConstructor = new System.Windows.Forms.Label();
            this.AttributeTypePanel = new System.Windows.Forms.Panel();
            this.AttributeType = new Reflexil.Editors.TypeReferenceEditor();
            this.ConstructorPanel = new System.Windows.Forms.Panel();
            this.Constructor = new Reflexil.Editors.MethodReferenceEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.TabControl.SuspendLayout();
            this.TabCtorArgs.SuspendLayout();
            this.TabFields.SuspendLayout();
            this.TabProperties.SuspendLayout();
            this.TabAttributes.SuspendLayout();
            this.AttributeTypePanel.SuspendLayout();
            this.ConstructorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabCtorArgs);
            this.TabControl.Controls.Add(this.TabFields);
            this.TabControl.Controls.Add(this.TabProperties);
            this.TabControl.Controls.Add(this.TabAttributes);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(395, 221);
            this.TabControl.TabIndex = 16;
            // 
            // TabCtorArgs
            // 
            this.TabCtorArgs.Controls.Add(this.ConstructorArguments);
            this.TabCtorArgs.Location = new System.Drawing.Point(4, 22);
            this.TabCtorArgs.Name = "TabCtorArgs";
            this.TabCtorArgs.Padding = new System.Windows.Forms.Padding(3);
            this.TabCtorArgs.Size = new System.Drawing.Size(387, 195);
            this.TabCtorArgs.TabIndex = 0;
            this.TabCtorArgs.Text = "Constructor arguments";
            this.TabCtorArgs.UseVisualStyleBackColor = true;
            // 
            // ConstructorArguments
            // 
            this.ConstructorArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConstructorArguments.Location = new System.Drawing.Point(3, 3);
            this.ConstructorArguments.Name = "ConstructorArguments";
            this.ConstructorArguments.ReadOnly = false;
            this.ConstructorArguments.Size = new System.Drawing.Size(381, 189);
            this.ConstructorArguments.TabIndex = 0;
            this.ConstructorArguments.GridUpdated += new Reflexil.Editors.GridControl<System.Nullable<Mono.Cecil.CustomAttributeArgument>, Mono.Cecil.CustomAttribute>.GridUpdatedEventHandler(this.ConstructorArguments_GridUpdated);
            // 
            // TabFields
            // 
            this.TabFields.Controls.Add(this.Fields);
            this.TabFields.Location = new System.Drawing.Point(4, 22);
            this.TabFields.Name = "TabFields";
            this.TabFields.Padding = new System.Windows.Forms.Padding(3);
            this.TabFields.Size = new System.Drawing.Size(387, 195);
            this.TabFields.TabIndex = 1;
            this.TabFields.Text = "Fields";
            this.TabFields.UseVisualStyleBackColor = true;
            // 
            // Fields
            // 
            this.Fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Fields.Location = new System.Drawing.Point(3, 3);
            this.Fields.Name = "Fields";
            this.Fields.ReadOnly = false;
            this.Fields.Size = new System.Drawing.Size(381, 189);
            this.Fields.TabIndex = 0;
            this.Fields.UseFields = true;
            this.Fields.GridUpdated += new Reflexil.Editors.GridControl<System.Nullable<Mono.Cecil.CustomAttributeNamedArgument>, Mono.Cecil.CustomAttribute>.GridUpdatedEventHandler(this.Fields_GridUpdated);
            // 
            // TabProperties
            // 
            this.TabProperties.Controls.Add(this.Properties);
            this.TabProperties.Location = new System.Drawing.Point(4, 22);
            this.TabProperties.Name = "TabProperties";
            this.TabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.TabProperties.Size = new System.Drawing.Size(387, 195);
            this.TabProperties.TabIndex = 2;
            this.TabProperties.Text = "Properties";
            this.TabProperties.UseVisualStyleBackColor = true;
            // 
            // Properties
            // 
            this.Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Properties.Location = new System.Drawing.Point(3, 3);
            this.Properties.Name = "Properties";
            this.Properties.ReadOnly = false;
            this.Properties.Size = new System.Drawing.Size(381, 189);
            this.Properties.TabIndex = 0;
            this.Properties.UseFields = false;
            this.Properties.GridUpdated += new Reflexil.Editors.GridControl<System.Nullable<Mono.Cecil.CustomAttributeNamedArgument>, Mono.Cecil.CustomAttribute>.GridUpdatedEventHandler(this.Properties_GridUpdated);
            // 
            // TabAttributes
            // 
            this.TabAttributes.Controls.Add(this.LabAttribute);
            this.TabAttributes.Controls.Add(this.LabConstructor);
            this.TabAttributes.Controls.Add(this.AttributeTypePanel);
            this.TabAttributes.Controls.Add(this.ConstructorPanel);
            this.TabAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabAttributes.Name = "TabAttributes";
            this.TabAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabAttributes.Size = new System.Drawing.Size(387, 195);
            this.TabAttributes.TabIndex = 3;
            this.TabAttributes.Text = "Attributes";
            this.TabAttributes.UseVisualStyleBackColor = true;
            // 
            // LabAttribute
            // 
            this.LabAttribute.AutoSize = true;
            this.LabAttribute.Location = new System.Drawing.Point(9, 37);
            this.LabAttribute.Name = "LabAttribute";
            this.LabAttribute.Size = new System.Drawing.Size(69, 13);
            this.LabAttribute.TabIndex = 27;
            this.LabAttribute.Text = "Attribute type";
            // 
            // LabConstructor
            // 
            this.LabConstructor.AutoSize = true;
            this.LabConstructor.Location = new System.Drawing.Point(9, 10);
            this.LabConstructor.Name = "LabConstructor";
            this.LabConstructor.Size = new System.Drawing.Size(61, 13);
            this.LabConstructor.TabIndex = 26;
            this.LabConstructor.Text = "Constructor";
            // 
            // AttributeTypePanel
            // 
            this.AttributeTypePanel.BackColor = System.Drawing.SystemColors.Info;
            this.AttributeTypePanel.Controls.Add(this.AttributeType);
            this.AttributeTypePanel.Location = new System.Drawing.Point(84, 33);
            this.AttributeTypePanel.Name = "AttributeTypePanel";
            this.AttributeTypePanel.Size = new System.Drawing.Size(280, 23);
            this.AttributeTypePanel.TabIndex = 25;
            // 
            // AttributeType
            // 
            this.AttributeType.AssemblyRestriction = null;
            this.AttributeType.BackColor = System.Drawing.SystemColors.Window;
            this.AttributeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttributeType.Enabled = false;
            this.AttributeType.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.AttributeType.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.AttributeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttributeType.Location = new System.Drawing.Point(0, 0);
            this.AttributeType.Name = "AttributeType";
            this.AttributeType.SelectedOperand = null;
            this.AttributeType.Size = new System.Drawing.Size(280, 23);
            this.AttributeType.TabIndex = 0;
            this.AttributeType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AttributeType.UseVisualStyleBackColor = false;
            // 
            // ConstructorPanel
            // 
            this.ConstructorPanel.BackColor = System.Drawing.SystemColors.Info;
            this.ConstructorPanel.Controls.Add(this.Constructor);
            this.ConstructorPanel.Location = new System.Drawing.Point(84, 6);
            this.ConstructorPanel.Name = "ConstructorPanel";
            this.ConstructorPanel.Size = new System.Drawing.Size(280, 23);
            this.ConstructorPanel.TabIndex = 25;
            // 
            // Constructor
            // 
            this.Constructor.AssemblyRestriction = null;
            this.Constructor.BackColor = System.Drawing.SystemColors.Window;
            this.Constructor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Constructor.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.Constructor.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.Constructor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Constructor.Location = new System.Drawing.Point(0, 0);
            this.Constructor.Name = "Constructor";
            this.Constructor.SelectedOperand = null;
            this.Constructor.Size = new System.Drawing.Size(280, 23);
            this.Constructor.TabIndex = 0;
            this.Constructor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Constructor.UseVisualStyleBackColor = false;
            this.Constructor.SelectedOperandChanged += new System.EventHandler(this.Constructor_SelectedOperandChanged);
            this.Constructor.Validating += new System.ComponentModel.CancelEventHandler(this.Constructor_Validating);
            // 
            // CustomAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 221);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomAttributeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomAttributeForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.TabCtorArgs.ResumeLayout(false);
            this.TabFields.ResumeLayout(false);
            this.TabProperties.ResumeLayout(false);
            this.TabAttributes.ResumeLayout(false);
            this.TabAttributes.PerformLayout();
            this.AttributeTypePanel.ResumeLayout(false);
            this.ConstructorPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected Editors.CustomAttributeArgumentGridControl ConstructorArguments;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabCtorArgs;
        private System.Windows.Forms.TabPage TabFields;
        private System.Windows.Forms.TabPage TabProperties;
        private System.Windows.Forms.TabPage TabAttributes;
        protected Editors.CustomAttributeNamedArgumentGridControl Fields;
        protected Editors.CustomAttributeNamedArgumentGridControl Properties;
        internal System.Windows.Forms.Panel AttributeTypePanel;
        internal System.Windows.Forms.Panel ConstructorPanel;
        private System.Windows.Forms.Label LabAttribute;
        private System.Windows.Forms.Label LabConstructor;
        protected Editors.MethodReferenceEditor Constructor;
        protected Editors.TypeReferenceEditor AttributeType;
	}
}